using AutoMapper;
using Microsoft.Extensions.Logging;
using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Entities;
using MyNotepad.Domain.Enums;
using MyNotepad.Domain.Exceptions;
using MyNotepad.Domain.Interfaces.Repositories;
using MyNotepad.Domain.Interfaces.Services;
using MyNotepad.Domain.Requests;
using MyNotepad.Domain.Responses;
using MyNotepad.Identity.Interfaces;

namespace MyNotepad.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<IUserService> _logger;

        public UserService(IUserRepository repository, IMapper mapper, IAuthorizationService authorizationService, ILogger<IUserService> logger, IEmailService emailService) 
        {
            _repository = repository;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _logger = logger;
            _emailService = emailService;
        }

        public TokenResponse Login(LoginRequest login)
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

            var _ = new User(user.Name, user.Email, user.Password, user.BirthDate);
            // Remove the line below when the microservice responsible
            // for email validation be working
            _.UpdateAccountStatus(UserAccountStatus.Active);

            var result = _repository.Create(_);
            // Implement the next line when the email validation microsservice be working
            //Task.Factory.StartNew(() => _emailService.SendEmailConfirmation(user.Email));

            return _mapper.Map<User, UserDTO>(result);
        }

        public UserDTO Delete(string password, int id)
        {
            // Authenticate user before changing the account status
            var user = _repository.GetById(id);
            if (!_authorizationService.AuthenticateUserPassword(password, user.Password))
                throw new UnauthorizedOperationException("Invalid password for the current user, access denied");

            user.UpdateAccountStatus(UserAccountStatus.Disabled);

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
    }
}
