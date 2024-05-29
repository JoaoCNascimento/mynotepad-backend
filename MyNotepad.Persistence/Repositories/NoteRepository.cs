using MyNotepad.Domain.Entities;
using MyNotepad.Domain.Interfaces.Repositories;

namespace MyNotepad.Persistence.Repositories
{
    public class NoteRepository : INoteRepository
    {
        MyNotepadDbContext _context;
        
        public NoteRepository(MyNotepadDbContext context) 
        {
            _context = context;
        }

        public Note Create(Note entity)
        {
            var _ = _context.Notes.Add(entity);
            _context.SaveChanges();
            return _.Entity;
        }

        public List<Note> GetAllByUserId(int userId)
        {
            return _context.Notes
                .Where(n => n.UserId.Equals(userId))
                .ToList();
        }

        public Note GetById(int id)
        {
            return _context.Notes.Find(id)!;
        }

        public Note Update(Note entity)
        {
            var _entity = GetById(entity.Id);

            _entity.UpdateTitle(entity.Title);
            _entity.UpdateContent(entity.Content);
            _entity.UpdateColor(entity.Color);
            
            _context.SaveChanges();
            return _entity;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            _context.Notes.Remove(entity);
            _context.SaveChanges();
        }
    }
}
