using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MyNotepad.API.Controllers;
using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Exceptions;
using MyNotepad.Domain.Interfaces.Services;
using MyNotepad.Domain.Requests;
using MyNotepad.Domain.Responses;
using System.Security.Claims;

namespace MyNotepad.Test.Presentation.API
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockService;
        private readonly Mock<ILogger<UserController>> _mockLogger;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<UserController>>();
            _controller = new UserController(_mockLogger.Object, _mockService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.Name, "1"),
                new (ClaimTypes.Role, "User")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [Fact]
        public void SignUp_ShouldReturnOk_WhenUserIsCreated()
        {
            // Arrange
            var userRegisterRequest = new UserRegisterRequest { Email = "test@example.com", Password = "Password123" };
            var userDto = new UserDTO { Id = 1, Email = "test@example.com" };

            _mockService.Setup(service => service.ValidateAndSignUpUser(It.IsAny<UserRegisterRequest>()))
                        .Returns(userDto);

            // Act
            var result = _controller.SignUp(userRegisterRequest);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<UserDTO>(okResult.Value);
            Assert.Equal(userDto, returnValue);
        }

        [Fact]
        public void SignUp_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Email", "Required");

            // Act
            var result = _controller.SignUp(new UserRegisterRequest());

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public void SignUp_ShouldReturnConflict_WhenInvalidFieldExceptionIsThrown()
        {
            // Arrange
            var userRegisterRequest = new UserRegisterRequest { Email = "test@example.com", Password = "Password123" };

            _mockService.Setup(service => service.ValidateAndSignUpUser(It.IsAny<UserRegisterRequest>()))
                        .Throws(new InvalidFieldException(string.Empty, "Email", string.Empty));

            // Act
            var result = _controller.SignUp(userRegisterRequest);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var conflictResult = Assert.IsType<StatusCodeResult>(actionResult.Result);
            Assert.Equal(StatusCodes.Status409Conflict, conflictResult.StatusCode);
        }

        [Fact]
        public void Login_ShouldReturnOk_WhenLoginIsSuccessful()
        {
            // Arrange
            var loginRequest = new LoginRequest { Email = "test@example.com", Password = "Password123" };
            var loginResponse = new TokenResponse { Token = "fake-jwt-token" };

            _mockService.Setup(service => service.Login(It.IsAny<LoginRequest>()))
                        .Returns(loginResponse);

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            var actionResult = Assert.IsType<ActionResult<TokenResponse>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<TokenResponse>(okResult.Value);
            Assert.Equal(loginResponse, returnValue);
        }

        [Fact]
        public void Login_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            var loginRequest = new LoginRequest { Email = "test@example.com", Password = "Password123" };

            _mockService.Setup(service => service.Login(It.IsAny<LoginRequest>()))
                        .Throws(new Exception("Unexpected error"));

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            var actionResult = Assert.IsType<ActionResult<TokenResponse>>(result);
            var exceptionResult = Assert.IsType<StatusCodeResult>(actionResult.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, exceptionResult.StatusCode);
        }

        [Fact]
        public void GetData_ShouldReturnOk_WhenUserDataIsFetched()
        {
            // Arrange
            var userDto = new UserDTO { Id = 1, Email = "test@example.com" };

            _mockService.Setup(service => service.GetUserData(It.IsAny<int>()))
                        .Returns(userDto);

            // Act
            var result = _controller.GetData();

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<UserDTO>(okResult.Value);
            Assert.Equal(userDto, returnValue);
        }

        [Fact]
        public void GetData_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            _mockService.Setup(service => service.GetUserData(It.IsAny<int>()))
                        .Throws(new Exception("Unexpected error"));

            // Act
            var result = _controller.GetData();

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [Fact]
        public void Update_ShouldReturnOk_WhenUserIsUpdated()
        {
            // Arrange
            var userDto = new UserDTO { Id = 1, Email = "test@example.com" };

            _mockService.Setup(service => service.Update(It.IsAny<UserDTO>()))
                        .Returns(userDto);

            // Act
            var result = _controller.Update(userDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<UserDTO>(okResult.Value);
            Assert.Equal(userDto, returnValue);
        }

        [Fact]
        public void Update_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            var userDto = new UserDTO { Id = 1, Email = "test@example.com" };

            _mockService.Setup(service => service.Update(It.IsAny<UserDTO>()))
                        .Throws(new Exception("Unexpected error"));

            // Act
            var result = _controller.Update(userDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var exceptionResult = Assert.IsType<StatusCodeResult>(actionResult.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, exceptionResult.StatusCode);
        }


        [Fact]
        public void Delete_ShouldReturnOk_WhenUserIsDeleted()
        {
            // Arrange
            var userDto = new UserDTO { Id = 1, Email = "test@example.com" };
            var password = "Password123";

            _mockService.Setup(service => service.Delete(It.IsAny<string>(), It.IsAny<int>()))
                        .Returns(userDto);

            // Act
            var result = _controller.Delete(password);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<UserDTO>(okResult.Value);
            Assert.Equal(userDto, returnValue);
        }

        [Fact]
        public void Delete_ShouldReturnForbidden_WhenUnauthorizedOperationExceptionIsThrown()
        {
            // Arrange
            var password = "Password123";

            _mockService.Setup(service => service.Delete(It.IsAny<string>(), It.IsAny<int>()))
                        .Throws(new UnauthorizedOperationException("Unauthorized"));

            // Act
            var result = _controller.Delete(password);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult.Result);
            Assert.Equal(StatusCodes.Status403Forbidden, statusCodeResult.StatusCode);
        }

        [Fact]
        public void Delete_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            var password = "Password123";

            _mockService.Setup(service => service.Delete(It.IsAny<string>(), It.IsAny<int>()))
                        .Throws(new Exception("Unexpected error"));

            // Act
            var result = _controller.Delete(password);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var statusCodeResult = Assert.IsType<StatusCodeResult>(actionResult.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }
    }
}
