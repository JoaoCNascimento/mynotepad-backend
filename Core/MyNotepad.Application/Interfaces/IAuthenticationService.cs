using MyNotepad.Domain.DTO;

namespace MyNotepad.Application.Interfaces;
public interface IAuthenticationService
{
    public Dictionary<UserDTO, string> Login(UserDTO user);
}