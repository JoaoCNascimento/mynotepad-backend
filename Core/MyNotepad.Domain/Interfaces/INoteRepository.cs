using MongoDB.Bson;
using MyNotepad.Domain.Entities;

namespace MyNotepad.Domain.Interfaces;
public interface INoteRepository : IBaseRepository<Note>
{
    public Note CreateOne(Note obj, ObjectId userId);
    public Note UpdateOne(Note obj, ObjectId userId);
    public List<Note> GetAll(ObjectId userId);
    public Note GetOne(int id, ObjectId userId);
}
