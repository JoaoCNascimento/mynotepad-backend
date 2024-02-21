using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyNotepad.Application.Interfaces;
using MyNotepad.Domain.DTO;

namespace MyNotepad.API.Controllers;
[ApiController]
[Route("/user/[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult Login(UserDTO user)
    {
        try
        {
            var result = _authenticationService.Login(user);
            var token = result.FirstOrDefault().Value;
            return Ok(token);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
