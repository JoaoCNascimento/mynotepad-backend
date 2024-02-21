using AutoMapper;
using MyNotepad.Application.Interfaces;
using MyNotepad.Domain.DTO;
using MyNotepad.Domain.Interfaces;

namespace MyNotepad.Application.Services;
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AuthenticationService(IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public Dictionary<UserDTO, string> Login(UserDTO user)
    {
        var savedUser = _userRepository.GetOneByEmail(user.Email);

        var result = new Dictionary<UserDTO, string>();

        if(BCrypt.Net.BCrypt.Verify(user.Password, savedUser.Password))
        {
            user = _mapper.Map<UserDTO>(savedUser);
            user.Password = string.Empty;
            var token = _tokenService.GenerateToken(user);
            result.Add(user, token);
        }
        else
        {
            result.Add(user, "");
        }

        return result;
    }
}