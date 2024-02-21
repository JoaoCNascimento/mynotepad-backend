namespace MyNotepad.Application.Interfaces;
public interface IBaseService<T>
{
    public List<T> GetAll();
    public T GetOne(string id);
    public T UpdateOne(T obj);
    public bool RemoveOne(string id);
    public T CreateOne(T obj);
}