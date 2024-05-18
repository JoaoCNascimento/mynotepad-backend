using AutoMapper;
using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Entities;

namespace MyNotepad.Domain.MapperProfile
{
    public class MyNotepadProfile : Profile
    {
        public MyNotepadProfile() 
        {
            CreateMap<UserDTO, User>()
                .ConstructUsing(u => new User(u.Name, u.Email, u.Password));

            CreateMap<User, UserDTO>()
                .ForMember(src => src.Password, dest => dest.Condition(src => false));

            CreateMap<Note, NoteDTO>()
                .ReverseMap();

            CreateMap<NoteDTO, Note>()
                .ConstructUsing(n => new Note(n.Title, n.Description, n.Color, n.UserId));
        }
    }
}
