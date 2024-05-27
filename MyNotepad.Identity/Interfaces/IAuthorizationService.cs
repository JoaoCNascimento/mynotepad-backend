using MyNotepad.Domain.Requests;

namespace MyNotepad.Identity.Interfaces
{
    public interface IAuthorizationService
    {
        public Dictionary<string, string> Login(LoginRequest user);
        public void ValidateUser(UserRegisterRequest user);
        public bool AuthenticateUserPassword(string enteredPassword, string userPassword);
        string HashUserPassword(string password);
    }
}
