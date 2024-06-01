using MyNotepad.Domain.Requests;
using MyNotepad.Domain.Responses;

namespace MyNotepad.Identity.Interfaces
{
    public interface IAuthorizationService
    {
        public TokenResponse Login(LoginRequest user);
        public void ValidateUser(UserRegisterRequest user);
        public bool AuthenticateUserPassword(string enteredPassword, string userPassword);
        string HashUserPassword(string password);
    }
}
