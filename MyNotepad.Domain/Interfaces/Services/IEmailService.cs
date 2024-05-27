namespace MyNotepad.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        public bool EmailIsAlreadyInUse(string email);
        public void SendEmailConfirmation(string email);
    }
}
