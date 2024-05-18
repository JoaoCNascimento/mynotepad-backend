using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Exceptions;
using MyNotepad.Domain.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNotepad.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SignUp(UserDTO user)
        {
            try
            {
                var result = _service.ValidateAndSignUpUser(user);
                return Ok(result);
            }
            catch (InvalidFieldException ex)
            {
                _logger.LogError($"An error ocurred when trying to validate and sign up a new user: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error ocurred when trying to validate and sign up a new user: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(UserDTO user)
        {
            try
            {
                var result = _service.Login(user);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"An error ocurred when trying to login in the application: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("/api/[controller]/")]
        public IActionResult GetUserData()
        {
            try
            {
                var result = _service.GetUserData(int.Parse(User.Identity?.Name!));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred when trying to fetch user data: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("/api/[controller]/")]
        public IActionResult UpdateUser(UserDTO user)
        {
            try
            {
                var result = _service.Update(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred when trying to fetch user data: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
