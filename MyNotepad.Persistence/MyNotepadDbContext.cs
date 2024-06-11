using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyNotepad.Domain.Entities;
using MyNotepad.Domain.Enums;
using Npgsql;
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

            var dbType = Environment.GetEnvironmentVariable("DB_TYPE");
            var dbConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            if (string.IsNullOrEmpty(dbType) || string.IsNullOrEmpty(dbConnectionString))
                throw new InvalidOperationException("Database type or connection string is not set.");

            if (dbType.Equals("Postgres", StringComparison.OrdinalIgnoreCase))
            {
                var dbConn = new NpgsqlConnection(dbConnectionString);
                optionsBuilder.UseNpgsql(dbConn);
            }
            else if (dbType.Equals("Sqlite", StringComparison.OrdinalIgnoreCase))
            {
                var dbConn = new SqliteConnection(@$"Data Source={dbConnectionString}");
                optionsBuilder.UseSqlite(dbConn);
            }
            else
                throw new InvalidOperationException("Unsupported database type.");

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
                .Property(n => n.Content)
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
