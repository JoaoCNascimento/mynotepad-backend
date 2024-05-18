using MyNotepad.Domain.DTOs;

namespace MyNotepad.Domain.Interfaces.Services
{
    public interface IUserService
    {
        public UserDTO ValidateAndSignUpUser(UserDTO user);
        public Dictionary<string, string> Login(UserDTO user);
        public UserDTO GetUserData(int id);
        public UserDTO Update(UserDTO user);
        public UserDTO Delete(string id);
    }
}
