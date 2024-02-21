using MyNotepad.Domain.DTO;

namespace MyNotepad.Application.Interfaces;
public interface ITokenService
{
    public string GenerateToken(UserDTO user);
}
