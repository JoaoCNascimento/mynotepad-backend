using MyNotepad.Domain.Enums;
using MyNotepad.Domain.Interfaces.Repositories;
using MyNotepad.Domain.Interfaces.Services;
using MyNotepad.External.Handlers.Interfaces;

namespace MyNotepad.Application.Services
{
    public class EmailService(IUserRepository userRepository, IRabbitMQHandler messageHandler) : IEmailService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRabbitMQHandler _messageHandler = messageHandler;

        public bool EmailIsAlreadyInUse(string email)
        {
            return _userRepository.Exists(email);
        }

        public void SendEmailConfirmation(string email)
        {
            _messageHandler.SendMessage(email, QueuesNames.UserEmailValidation.GetQueueName());
            _messageHandler.ReceiveMessage(RegisterUserEmailConfirmationWasSent, QueuesNames.EmailConfirmationWasSentToUser.GetQueueName());
        }

        void RegisterUserEmailConfirmationWasSent(string email)
        {
            throw new NotImplementedException();
        }
    }
}
