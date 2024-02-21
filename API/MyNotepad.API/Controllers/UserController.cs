namespace MyNotepad.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using MyNotepad.Application.Interfaces;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    public ActionResult GetUserData()
    {
        try
        {
            var result = _userService.GetOne(User.Identity?.Name!);
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogError($"There was an error while trying to retrieve the user data. Error: {ex.Message}", ex);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

