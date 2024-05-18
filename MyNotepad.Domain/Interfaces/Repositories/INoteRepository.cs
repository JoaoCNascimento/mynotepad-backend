using MyNotepad.Domain.Entities;

namespace MyNotepad.Domain.Interfaces.Repositories
{
    public interface INoteRepository : IBaseRepository<Note>
    {
        public List<Note> GetAllByUserId(int userId);
    }
}
