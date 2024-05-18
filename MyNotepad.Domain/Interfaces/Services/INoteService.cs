using MyNotepad.Domain.DTOs;

namespace MyNotepad.Domain.Interfaces.Services
{
    public interface INoteService
    {
        public NoteDTO CreateOne(NoteDTO note);
        public NoteDTO UpdateOne(NoteDTO note);
        public void DeleteById(int id);
        public NoteDTO GetById(int id);
        public List<NoteDTO> GetAll(int userId);
    }
}
