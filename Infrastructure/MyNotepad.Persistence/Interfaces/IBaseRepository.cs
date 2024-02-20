namespace MyNotepad.Persistence.Interfaces;
public interface IBaseRepository<T>
{
    public List<T> GetAll();
    public T GetOne(int id);
    public T UpdateOne(T obj);
    public bool RemoveOne(int id);
    public T CreateOne(T obj);
}