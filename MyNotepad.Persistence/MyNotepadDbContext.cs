using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyNotepad.Domain.Entities;
using MyNotepad.Domain.Enums;
namespace MyNotepad
{

    public class MyNotepadDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        public MyNotepadDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var services = new ServiceCollection();
            var dbConn = new SqliteConnection(@"Data Source=C:\Users\joaon\source\repos\Teste2\MyNotepad.Persistence\Data\Database.db");
            optionsBuilder.UseSqlite(dbConn);
            services.AddDbContext<MyNotepadDbContext>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User
            modelBuilder.Entity<User>()
                .ToTable("users")
                .HasMany(u => u.Notes);

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasDefaultValue(UserRoles.User)
                .IsRequired();

            // Note
            modelBuilder.Entity<Note>()
                .ToTable("notes");

            modelBuilder.Entity<Note>()
                .Property(n => n.Title)
                .HasMaxLength(120)
                .IsRequired();

            modelBuilder.Entity<Note>()
                .Property(n => n.Description)
                .HasMaxLength(2500);

            modelBuilder.Entity<Note>()
                .Property(n => n.Color)
                .HasDefaultValue("blue")
                .IsRequired();
        }

        public override int SaveChanges()
        {
            UpdateTimeStamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimeStamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        void UpdateTimeStamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                }
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}
