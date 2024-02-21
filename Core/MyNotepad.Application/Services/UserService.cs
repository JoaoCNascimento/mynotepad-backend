using AutoMapper;
using MongoDB.Bson;
using MyNotepad.Application.Interfaces;
using MyNotepad.Domain.DTO;
using MyNotepad.Domain.Interfaces;

namespace MyNotepad.Application.Services;
public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public UserDTO CreateOne(UserDTO obj)
    {
        throw new NotImplementedException();
    }

    public List<UserDTO> GetAll()
    {
        throw new NotImplementedException();
    }

    public UserDTO GetOne(int id)
    {
        throw new NotImplementedException();
    }

    public UserDTO GetOne(string id)
    {
        return _mapper.Map<UserDTO>(_userRepository.GetOne(new ObjectId(id)));
    }


    public bool RemoveOne(int id)
    {
        throw new NotImplementedException();
    }

    public UserDTO UpdateOne(UserDTO obj)
    {
        throw new NotImplementedException();
    }
}