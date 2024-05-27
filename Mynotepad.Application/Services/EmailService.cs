using MyNotepad.Domain.Interfaces.Repositories;
using MyNotepad.Domain.Interfaces.Services;

namespace MyNotepad.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IUserRepository _userRepository;

        public EmailService(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }

        public bool EmailIsAlreadyInUse(string email)
        {
            return _userRepository.Exists(email);
        }
    }
}
