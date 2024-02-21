using MyNotepad.Domain.DTO;

namespace MyNotepad.Application.Interfaces;
public interface IUserService : IBaseService<UserDTO>
{
    public UserDTO GetOne(string id);
}