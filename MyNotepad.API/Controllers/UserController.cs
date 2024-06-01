using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Exceptions;
using MyNotepad.Domain.Interfaces.Services;
using MyNotepad.Domain.Requests;
using MyNotepad.Domain.Responses;

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
        public ActionResult<UserDTO> SignUp([FromBody] UserRegisterRequest user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values);

                var result = _service.ValidateAndSignUpUser(user);
                return Ok(result);
            }
            catch (InvalidFieldException ex)
            {
                _logger.LogError($"The field {ex.FieldName} is invalid. Error: {ex.Message}");
                // Returning "Conflict" status code, since it is the only validation that throws this error currently 
                return StatusCode(StatusCodes.Status409Conflict); // Change if additional user validations be necessary in the future
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error ocurred when trying to validate and sign up a new user: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<TokenResponse> Login([FromBody] LoginRequest user)
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
        public ActionResult<UserDTO> GetData()
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
        public ActionResult<UserDTO> Update([FromBody] UserDTO user)
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

        [HttpDelete("/api/[controller]/")]
        public ActionResult<UserDTO> Delete([FromBody] string password)
        {
            try
            {
                var result = _service.Delete(password, int.Parse(User.Identity?.Name!));
                _logger.LogInformation($"User account " +
                    $"'{string.Join("", result.Email.Take(result.Email.Length / 3)) + "*********" 
                    + string.Join("", result.Email.Skip(result.Email.Length / 2))}' " 
                    + $"was disabled sucessfully");
                return Ok(result);
            }
            catch (UnauthorizedOperationException ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred when trying to delete the user: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
