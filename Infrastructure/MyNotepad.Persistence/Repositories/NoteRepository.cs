using MongoDB.Bson;
using MongoDB.Driver;
using MyNotepad.Domain.Entities;
using MyNotepad.Domain.Interfaces;

namespace MyNotepad.Persistence.Repositories;
public class NoteRepository : INoteRepository
{
    readonly IMongoDatabase database = MongoDbConnection.GetDatabaseConnection();
    IMongoCollection<User> collection { get; set; }

    public NoteRepository()
    {
        collection = database.GetCollection<User>("users");
    }

    public Note CreateOne(ObjectId userId, Note note)
    {
        var filter = Builders<User>.Filter.Eq(user => user.Id, userId);

        var update = Builders<User>.Update.Push(user => user.Notes, note);
        collection.UpdateOne(filter, update);
        return note;
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
            // TODO: handle case where user is not found
            return new List<Note>();
        }
    }

    public Note UpdateOne(ObjectId userId, Note note)
    {
        var filter = Builders<User>.Filter.And(
            Builders<User>.Filter.Eq(u => u.Id, userId),
            Builders<User>.Filter.ElemMatch(u => u.Notes, n => n.Id.Equals(note.Id))
        );

        var update = Builders<User>.Update
            .Set(u => u.Notes[-1].Title, note.Title)
            .Set(u => u.Notes[-1].Content, note.Content)
            .Set(u => u.Notes[-1].Color, note.Color);

        collection.UpdateOne(filter, update);
        
        return note;
    }

    public Note GetOne(ObjectId userId, ObjectId id)
    {
        var filter = Builders<User>.Filter.Eq(user => user.Id, userId);

        var user = collection.Find(filter).FirstOrDefault();

        var result = user.Notes?.First(note => note.Id.Equals(id))!;
        return result;
    }

    public bool RemoveOne(ObjectId userId, ObjectId id)
    {
        var filter = Builders<User>.Filter.Eq(user => user.Id, userId);
        var update = Builders<User>.Update.PullFilter(user => user.Notes, note => note.Id.Equals(id));
        var result = collection.UpdateOne(filter, update);
        return result.ModifiedCount > 0; 
    }

    #region Not implementend
    public Note CreateOne(Note obj)
    {
        throw new NotImplementedException();
    }

    public List<Note> GetAll()
    {
        throw new NotImplementedException();
    }
    
    public Note GetOne(ObjectId id)
    {
        throw new NotImplementedException();
    }

    public Note UpdateOne(Note obj)
    {
        throw new NotImplementedException();
    }

    public bool RemoveOne(ObjectId id)
    {
        throw new NotImplementedException();
    }
    #endregion
}