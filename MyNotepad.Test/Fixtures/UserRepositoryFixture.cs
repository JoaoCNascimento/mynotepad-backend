using MyNotepad.Persistence;
using MyNotepad.Persistence.Repositories;
using Xunit;

namespace MyNotepad.Test.Fixtures
{
    public class UserRepositoryFixture : IClassFixture<MyNotepadDbContext>
    {
        public UserRepository UserRepository { get; }

        public UserRepositoryFixture(MyNotepadDbContext dbContext)
        {
            UserRepository = new UserRepository(dbContext);
        }
    }
}