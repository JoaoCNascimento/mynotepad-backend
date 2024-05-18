using MyNotepad.Domain.DTOs;

namespace MyNotepad.Identity.Interfaces
{
    public interface IAuthorizationService
    {
        public Dictionary<string, string> Login(UserDTO user);
        public void ValidateUser(UserDTO user);
        public bool AuthenticateUserPassword(string enteredPassword, string userPassword);
        string HashUserPassword(string password);
    }
}
