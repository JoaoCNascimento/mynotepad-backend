using AutoMapper;
using MongoDB.Bson;
using MyNotepad.Domain.DTO;
using MyNotepad.Domain.Entities;

namespace MyNotepad.Application.Mappings;
public class EntityToDTOMappingProfile : Profile
{
    public EntityToDTOMappingProfile()
    {
        CreateMap<Note, NoteDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        CreateMap<NoteDTO, Note>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => new ObjectId(src.Id)));
        CreateMap<User, UserDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        CreateMap<UserDTO, User>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => new ObjectId(src.Id)));
    }
}