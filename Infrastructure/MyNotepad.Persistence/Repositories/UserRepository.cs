using MongoDB.Bson;
using MongoDB.Driver;
using MyNotepad.Domain.Entities;
using MyNotepad.Domain.Interfaces;

namespace MyNotepad.Persistence.Repositories;
public class UserRepository : IUserRepository
{    
    IMongoDatabase database = MongoDbConnection.GetDatabaseConnection();
    IMongoCollection<User> collection;

    public UserRepository()
    {
        collection = database.GetCollection<User>("users");
    }

    public User CreateOne(User obj)
    {
        throw new NotImplementedException();
    }

    public List<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public User GetOne(ObjectId id)
    {
        var filter = Builders<User>.Filter.Eq(user => user.Id, id);
        return collection.Find(filter).FirstOrDefault();
    }

    public User GetOneByEmail(string email)
    {
        var filter = Builders<User>.Filter.Eq(user => user.Email, email);
        return collection.Find(filter).FirstOrDefault();
    }

    public bool RemoveOne(ObjectId id)
    {
        throw new NotImplementedException();
    }

    public User UpdateOne(User obj)
    {
        throw new NotImplementedException();
    }
}