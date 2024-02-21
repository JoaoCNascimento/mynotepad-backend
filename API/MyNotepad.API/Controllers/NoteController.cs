using Microsoft.AspNetCore.Mvc;
using MyNotepad.Application.Interfaces;
using MyNotepad.Domain.DTO;

namespace MyNotepad.API.Controllers;
[ApiController]
[Route("[controller]")]
public class NoteController : ControllerBase
{
    private readonly ILogger<NoteController> _logger;
    private readonly INoteService _noteService;

    public NoteController(ILogger<NoteController> logger, INoteService userService)
    {
        _logger = logger;
        _noteService = userService;
    }

    [HttpPost]
    public ActionResult CreateOne(NoteDTO note)
    {
        try
        {
            var userId = "";
            var notes = _noteService.CreateOne(userId, note);
            return Ok(notes);
        }
        catch (Exception ex)
        {
            _logger.LogError($"There was an error while trying to retrieve all the notes. Error: {ex.Message}", ex);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    public ActionResult GetAll()
    {
        try
        {
            var userId = "";
            var notes = _noteService.GetAll(userId);
            return Ok(notes);
        }
        catch (Exception ex)
        {
            _logger.LogError($"There was an error while trying to retrieve all the notes. Error: {ex.Message}", ex);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{noteId}")]
    public ActionResult GetOne(string noteId)
    {
        try
        {
            var userId = "";
            var notes = _noteService.GetOne(userId, noteId);
            return Ok(notes);
        }
        catch (Exception ex)
        {
            _logger.LogError($"There was an error while trying to retrieve all the notes. Error: {ex.Message}", ex);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{noteId}")]
    public ActionResult RemoveOne(string noteId)
    {
        try
        {
            var userId = "";
            _noteService.RemoveOne(userId, noteId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"There was an error while trying to remove the note. Error: {ex.Message}", ex);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    public ActionResult UpdateOne(NoteDTO note)
    {
        try
        {
            var userId = "";
            var updatedNote = _noteService.UpdateOne(userId, note);
            return Ok(updatedNote);
        }
        catch (Exception ex)
        {
            _logger.LogError($"There was an error while trying to update the note. Error: {ex.Message}", ex);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}