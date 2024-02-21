using MongoDB.Bson;
using MyNotepad.Domain.Entities;

namespace MyNotepad.Domain.Interfaces;
public interface INoteRepository : IBaseRepository<Note>
{
    public Note CreateOne(ObjectId userId, Note note);
    public Note UpdateOne(ObjectId userId, Note note);
    public List<Note> GetAll(ObjectId userId);
    public Note GetOne(ObjectId userId, ObjectId id);
    public bool RemoveOne(ObjectId userId, ObjectId id);
}
