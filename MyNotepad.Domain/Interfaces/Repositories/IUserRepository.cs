using MyNotepad.Domain.Entities;

namespace MyNotepad.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public User GetByEmail(string email);
        public bool Exists(string email);
    }
}
