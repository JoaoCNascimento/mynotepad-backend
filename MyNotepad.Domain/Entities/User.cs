using MyNotepad.Domain.Enums;

namespace MyNotepad.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public List<Note> Notes { get; private set; } = new();
        public UserAccountStatus AccountStatus { get; private set; } = UserAccountStatus.Pending;
        public UserRoles Role { get; private set; } = UserRoles.User;

        public User() { }

        public User(string name, string email, string password) 
        {
            Name = name;
            Email = email;
            Password = password;
            Role = UserRoles.User;
            AccountStatus = UserAccountStatus.Pending;
        }

        public void UpdateName(string name) => Name = name;
        public void UpdateEmail(string email) => Email = email;
        public void UpdateRole(UserRoles role) => Role = role;
        public void UpdatePassword(string password) => Password = password;
        public void UpdateAccountStatus(UserAccountStatus accountStatus) => AccountStatus = accountStatus;
    }
}
