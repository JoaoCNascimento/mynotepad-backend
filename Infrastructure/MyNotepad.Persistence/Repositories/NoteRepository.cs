using MongoDB.Bson;
using MongoDB.Driver;
using MyNotepad.Domain.Entities;
using MyNotepad.Domain.Interfaces;

namespace MyNotepad.Persistence.Repositories;
public class NoteRepository : INoteRepository
{
    IMongoDatabase database = MongoDbConnection.GetDatabaseConnection();
    IMongoCollection<User> collection { get; set; }

    public NoteRepository()
    {
        collection = database.GetCollection<User>("users");
    }

    public Note CreateOne(Note obj)
    {
        throw new NotImplementedException();
    }

    public Note CreateOne(Note note, ObjectId userId)
    {
        var filter = Builders<User>.Filter.Eq(user => user.Id, userId);

        var update = Builders<User>.Update.Push(user => user.Notes, note);
        collection.UpdateOne(filter, update);
        return note;
    }

    public List<Note> GetAll()
    {
        throw new NotImplementedException();
    }
    
    public List<Note> GetAll(ObjectId userId)
    {
        var filter = Builders<User>.Filter.Eq(user => user.Id, userId);
        var user = collection.Find(filter).FirstOrDefault();

        if (user != null)
        {
            return user.Notes;
        }
        else
        {
            // Handle case where user is not found
            return new List<Note>();
        }
    }

    public Note GetOne(int id)
    {
        throw new NotImplementedException();
    }

    public Note GetOne(int id, ObjectId userId)
    {
        throw new NotImplementedException();
    }

    public bool RemoveOne(int id)
    {
        throw new NotImplementedException();
    }

    public Note UpdateOne(Note obj, ObjectId userId)
    {
        throw new NotImplementedException();
    }

    public Note UpdateOne(Note obj)
    {
        throw new NotImplementedException();
    }
}