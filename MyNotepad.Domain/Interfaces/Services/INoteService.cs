using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Requests;

namespace MyNotepad.Domain.Interfaces.Services
{
    public interface INoteService
    {
        public NoteDTO CreateOne(NoteRequest note, int userId);
        public NoteDTO UpdateOne(NoteRequest note, int userId);
        public void DeleteById(int id, int userId);
        public NoteDTO GetById(int id, int userId);
        public List<NoteDTO> GetAll(int userId);
    }
}
