namespace MyNotepad.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        T Create(T entity);
        T Update(T entity);
        void Delete(int id);
        T GetById(int id);
    }
}
