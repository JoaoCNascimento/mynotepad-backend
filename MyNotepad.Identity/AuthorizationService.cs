using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Exceptions;
using MyNotepad.Domain.Interfaces.Repositories;
using MyNotepad.Domain.Requests;
using MyNotepad.Domain.Responses;
using MyNotepad.Domain.Utils;
using MyNotepad.Identity.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyNotepad.Identity
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthorizationService> _logger;
        private readonly IMapper _mapper;

        public AuthorizationService(IUserRepository userRepository, ILogger<AuthorizationService> logger, IMapper mapper) 
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public void ValidateUser(UserRegisterRequest user)
        {
            try
            {
                //new ValidateUserUtil(user, _userRepository).Validate();
            }
            catch(InvalidFieldException ex)
            {
                _logger.LogError("There was an error while trying to validate the user data." +
                                 $"Details: Field Name = \"{ex.FieldName}\"; Field Value = \"{ex.FieldValue}\"; Error: \"{ex.Message}\"");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred when trying to validate the user data. Error: {ex.Message}");
                throw;
            }
        }

        public bool AuthenticateUserPassword(string enteredPassword, string userPassword) 
                => BCrypt.Net.BCrypt.Verify(enteredPassword, userPassword);

        public TokenResponse Login(LoginRequest login)
        {
            var result = new TokenResponse();

            var _ = _userRepository.GetByEmail(login.Email);

            if (_ == null)
            {
                return result;
            }

            if (AuthenticateUserPassword(login.Password, _.Password))
            {
                var token = GenerateToken(_mapper.Map<UserDTO>(_));
                result.Token = token;
            }

            return result;
        }

        public string HashUserPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private string GenerateToken(UserDTO user)
        {
            var secret = Environment.GetEnvironmentVariable("SECRET")!;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
