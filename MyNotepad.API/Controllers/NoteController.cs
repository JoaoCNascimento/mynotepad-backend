using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Interfaces.Services;

namespace MyNotepad.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly ILogger<NoteController> _logger;
        private readonly INoteService _service;

        public NoteController(ILogger<NoteController> logger, INoteService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public IActionResult CreateNote(NoteDTO note)
        {
            try
            {
                var result = _service.CreateOne(note, int.Parse(User.Identity?.Name!));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred when trying to create a new note for the user of id: {User.Identity?.Name!}. Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _service.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred when trying to get note of id: '{id}' for the user of id {User.Identity?.Name!}. Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _service.GetAll(int.Parse(User.Identity?.Name!));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred when trying to get the notes for the user of Id: '{User.Identity?.Name!}'. Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _service.DeleteById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred when trying to get note of id: '{id}' for the user of id {User.Identity?.Name!}. Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOne(int id, NoteDTO note)
        {
            try
            {
                var result = _service.UpdateOne(note);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred when trying to update the note of id: '{id}' for the user of id {User.Identity?.Name!}. Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
