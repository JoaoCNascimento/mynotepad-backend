using AutoMapper;
using Microsoft.Extensions.Logging;
using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Entities;
using MyNotepad.Domain.Enums;
using MyNotepad.Domain.Exceptions;
using MyNotepad.Domain.Interfaces.Repositories;
using MyNotepad.Domain.Interfaces.Services;
using MyNotepad.Domain.Requests;
using MyNotepad.External.Handlers.Interfaces;
using MyNotepad.Identity.Interfaces;

namespace MyNotepad.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IRabbitMQHandler _producer;
        private readonly IEmailService _emailService;
        private readonly ILogger<IUserService> _logger;

        public UserService(IUserRepository repository, IMapper mapper, IAuthorizationService authorizationService, 
            IRabbitMQHandler producer, ILogger<IUserService> logger, IEmailService emailService) 
        {
            _repository = repository;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _producer = producer;
            _logger = logger;
            _emailService = emailService;
        }

        public Dictionary<string, string> Login(LoginRequest login)
        {
            var result = _authorizationService.Login(login);
            return result;
        }

        public UserDTO ValidateAndSignUpUser(UserRegisterRequest user)
        {
            if (_emailService.EmailIsAlreadyInUse(user.Email))
            {
                _logger.LogInformation($"The entered email: '{user.Email}' is already in use.");
                throw new InvalidFieldException("The entered email is already in use", "email", user.Email);
            }

            user.Password = _authorizationService.HashUserPassword(user.Password);

            var _ = new User(user.Name, user.Email, user.Password);
            // Remove the line below when the microservice responsible
            // for email validation be working
            _.UpdateAccountStatus(UserAccountStatus.Active);

            var result = _repository.Create(_);
            // Implement the next line when the email validation microsservice be working
            // Task.Factory.StartNew(() => SendUserEmailConfirmation(user.Email));

            return _mapper.Map<User, UserDTO>(result);
        }

        private void SendUserEmailConfirmation(string email)
        {
            _producer.SendMessage(email, QueuesNames.UserEmailValidation.GetQueueName());
            _producer.ReceiveMessage(RegisterUserConfirmationWasSent, QueuesNames.EmailConfirmationWasSentToUser.GetQueueName());
        }

        public UserDTO Delete(string password, string id)
        {
            // Authenticate user before changing the account status
            var user = _repository.GetById(int.Parse(id));
            if (_authorizationService.AuthenticateUserPassword(password, user.Password));
            {
                user.UpdateAccountStatus(UserAccountStatus.Disabled);
            }

            return _mapper.Map<UserDTO>(_repository.Update(user));
        }

        public UserDTO Update(UserDTO user)
        {
            var result = _repository.Update(_mapper.Map<User>(user));
            return _mapper.Map<UserDTO>(result);
        }

        public UserDTO GetUserData(int id)
        {
            return _mapper.Map<UserDTO>(_repository.GetById(id));
        }


        // TODO
        // Implement when the microsservice responsible for the email validation be working
        private void RegisterUserConfirmationWasSent(string email)
        {
            throw new NotImplementedException();
            // Should update in the database that the email confirmation was sent to the user
        }

        public UserDTO Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
