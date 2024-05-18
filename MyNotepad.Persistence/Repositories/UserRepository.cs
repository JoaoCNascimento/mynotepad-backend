using MyNotepad.Domain.Entities;
using MyNotepad.Domain.Interfaces.Repositories;

namespace MyNotepad.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyNotepadDbContext _context;

        public UserRepository(MyNotepadDbContext context) 
        { 
            _context = context;
        }

        public User Create(User entity)
        {
            var _ = _context.Users.Add(entity);
            _context.SaveChanges();
            return _.Entity;
        }

        public bool Exists(string email)
        {
            return _context.Users.Any(u => u.Email.ToLower().Equals(email.ToLower()));
        }

        public User GetByEmail(string email)
        {
            var entity = _context.Users.FirstOrDefault(u => u.Email.Equals(email))!;
            return entity;
        }

        public User GetById(int id)
        {
            var entity = _context.Users.Find(id)!;
            return entity;
        }

        public User Update(User entity)
        {
            var _entity = _context.Users.Find(entity.Id) ?? throw new Exception($"User entity not found in database for id: '{entity.Id}'");
            _entity.UpdateName(entity.Name);
            _entity.UpdateEmail(entity.Email);
            _entity.UpdatePassword(entity.Password);
            _entity.UpdateRole(entity.Role);
            _entity.UpdateAccountStatus(entity.AccountStatus);

            _context.SaveChanges();
            
            return _entity;
        }

        public void Delete(int id)
        {
            var entity = _context.Users.Find(id) ?? throw new Exception($"User entity not found in database for id: '{id}'");
            var _ = _context.Users.Remove(entity);
            _context.SaveChanges();
        }
    }
}
