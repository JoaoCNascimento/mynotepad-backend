using MongoDB.Bson;
using MyNotepad.Domain.Entities;

namespace MyNotepad.Persistence.Interfaces;
public interface IUserRepository : IBaseRepository<User>
{
    public User GetOne(ObjectId id);
    public User RemoveOne(ObjectId id);
}