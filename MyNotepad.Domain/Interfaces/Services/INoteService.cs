using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Requests;

namespace MyNotepad.Domain.Interfaces.Services
{
    public interface INoteService
    {
        public NoteDTO CreateOne(NoteRequest note, int UserId);
        public NoteDTO UpdateOne(NoteRequest note);
        public void DeleteById(int id);
        public NoteDTO GetById(int id);
        public List<NoteDTO> GetAll(int userId);
    }
}
