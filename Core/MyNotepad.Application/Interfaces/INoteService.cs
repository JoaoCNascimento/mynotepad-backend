using MyNotepad.Domain.DTO;

namespace MyNotepad.Application.Interfaces;
public interface INoteService : IBaseService<NoteDTO>
{
    public NoteDTO CreateOne(string userId, NoteDTO note);
    public NoteDTO GetOne(string userId, string id);
    public List<NoteDTO> GetAll(string userId);
    public bool RemoveOne(string userId, string id);
    public NoteDTO UpdateOne(string userId, NoteDTO note);
}