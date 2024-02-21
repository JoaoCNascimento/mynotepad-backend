using MongoDB.Bson;

namespace MyNotepad.Domain.Interfaces;
public interface IBaseRepository<T>
{
    public List<T> GetAll();
    public T GetOne(ObjectId id);
    public T UpdateOne(T obj);
    public bool RemoveOne(ObjectId id);
    public T CreateOne(T obj);
}