using MyNotepad.Domain.Entities;

namespace MyNotepad.Domain.Interfaces;
public interface IUserRepository : IBaseRepository<User>
{
    public User GetOneByEmail(string email);
}