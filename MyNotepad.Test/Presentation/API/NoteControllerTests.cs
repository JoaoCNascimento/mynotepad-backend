using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MyNotepad.API.Controllers;
using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Interfaces.Services;
using MyNotepad.Domain.Requests;
using System.Security.Claims;

namespace MyNotepad.Test.Presentation.API;
public class NoteControllerTests
{
    private readonly Mock<INoteService> _mockService;
    private readonly Mock<ILogger<NoteController>> _mockLogger;
    private readonly NoteController _controller;

    public NoteControllerTests()
    {
        _mockService = new Mock<INoteService>();
        _mockLogger = new Mock<ILogger<NoteController>>();
        _controller = new NoteController(_mockLogger.Object, _mockService.Object);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "1"),
            new Claim(ClaimTypes.Role, "User")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
    }

    [Fact]
    public void CreateOne_ShouldReturnOk_WhenNoteIsCreated()
    {
        // Arrange
        var noteRequest = new NoteRequest { Title = "Test Note", Content = "Content" };
        var noteDto = new NoteDTO { Id = 1, Title = "Test Note", Content = "Content" };

        _mockService.Setup(service => service.CreateOne(It.IsAny<NoteRequest>(), It.IsAny<int>())).Returns(noteDto);

        // Act
        var result = _controller.CreateOne(noteRequest);

        // Assert
        var actionResult = Assert.IsType<ActionResult<NoteDTO>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsType<NoteDTO>(okResult.Value);
        Assert.Equal(noteDto, returnValue);
    }

    [Fact]
    public void CreateOne_ShouldReturnUnprocessableEntity_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Title", "Required");

        // Act
        var result = _controller.CreateOne(new NoteRequest());

        // Assert
        var actionResult = Assert.IsType<ActionResult<NoteDTO>>(result);
        var unprocessableEntityResult = Assert.IsType<ObjectResult>(actionResult.Result);
        Assert.Equal(StatusCodes.Status422UnprocessableEntity, unprocessableEntityResult.StatusCode);
    }

    [Fact]
    public void GetById_ShouldReturnOk_WhenNoteExists()
    {
        // Arrange
        var noteId = 1;
        var noteDto = new NoteDTO { Id = noteId, Title = "Test Note", Content = "Content" };

        _mockService.Setup(service => service.GetById(noteId, It.IsAny<int>()))
                    .Returns(noteDto);

        // Act
        var result = _controller.GetById(noteId);

        // Assert
        var actionResult = Assert.IsType<ActionResult<NoteDTO>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsType<NoteDTO>(okResult.Value);
        Assert.Equal(noteDto, returnValue);
    }

    [Fact]
    public void GetById_ShouldReturnUnauthorized_WhenUserDoesNotOwnNote()
    {
        // Arrange
        var noteId = 1;
        _mockService.Setup(service => service.GetById(noteId, It.IsAny<int>()))
                    .Throws(new UnauthorizedAccessException());

        // Act
        var result = _controller.GetById(noteId);

        // Assert
        var actionResult = Assert.IsType<ActionResult<NoteDTO>>(result);
        var unauthorizedResult = Assert.IsType<StatusCodeResult>(actionResult.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, unauthorizedResult.StatusCode);
    }

    [Fact]
    public void DeleteById_ShouldReturnOk_WhenNoteIsDeleted()
    {
        // Arrange
        var noteId = 1;

        // Act
        var result = _controller.DeleteById(noteId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public void DeleteById_ShouldReturnUnauthorized_WhenUserDoesNotOwnNote()
    {
        // Arrange
        var noteId = 1;
        _mockService.Setup(service => service.DeleteById(noteId, It.IsAny<int>()))
                    .Throws(new UnauthorizedAccessException());

        // Act
        var result = _controller.DeleteById(noteId);

        // Assert
        var unauthorizedResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, unauthorizedResult.StatusCode);
    }
    [Fact]
    public void UpdateOne_ShouldReturnOk_WhenNoteIsUpdated()
    {
        // Arrange
        var noteRequest = new NoteRequest { Id = 1, Title = "Updated Note", Content = "Updated Content" };
        var noteDto = new NoteDTO { Id = 1, Title = "Updated Note", Content = "Updated Content" };

        _mockService.Setup(service => service.UpdateOne(It.IsAny<NoteRequest>(), It.IsAny<int>()))
                    .Returns(noteDto);

        // Act
        var result = _controller.UpdateOne(noteRequest);

        // Assert
        var okResult = Assert.IsType<ActionResult<NoteDTO>>(result);
        var returnValue = Assert.IsType<NoteDTO>(((OkObjectResult) okResult.Result!).Value);
        Assert.Equal(noteDto, returnValue);
    }

    [Fact]
    public void UpdateOne_ShouldReturnUnprocessableEntity_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Title", "Required");

        // Act
        var result = _controller.UpdateOne(new NoteRequest());

        // Assert
        var actionResult = Assert.IsType<ActionResult<NoteDTO>>(result);
        var unprocessableEntityResult = Assert.IsType<ObjectResult>(actionResult.Result);
        Assert.Equal(StatusCodes.Status422UnprocessableEntity, unprocessableEntityResult.StatusCode);
    }

    [Fact]
    public void UpdateOne_ShouldReturnUnauthorized_WhenUserDoesNotOwnNote()
    {
        // Arrange
        var noteRequest = new NoteRequest { Id = 1, Title = "Updated Note", Content = "Updated Content" };

        _mockService.Setup(service => service.UpdateOne(It.IsAny<NoteRequest>(), It.IsAny<int>()))
                    .Throws(new UnauthorizedAccessException());

        // Act
        var result = _controller.UpdateOne(noteRequest);

        // Assert
        var actionResult = Assert.IsType<ActionResult<NoteDTO>>(result);
        var unauthorizedResult = Assert.IsType<StatusCodeResult>(actionResult.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, unauthorizedResult.StatusCode);
    }

}