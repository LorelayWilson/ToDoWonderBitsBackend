using Microsoft.EntityFrameworkCore;
using ToDoWonderBitsBackend.Domain.Models;

namespace ToDoWonderBitsBackend.Infrastructure.Persistence
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<TodoItem> TodoItems { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mylocaldb;Database=TodoWonderBits;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().ToTable("TodoItems");
            modelBuilder.Entity<TodoItem>().HasKey(t => t.Id);
            modelBuilder.Entity<TodoItem>().Property(t => t.Description).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<TodoItem>()
                .HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId);

            modelBuilder.Entity<TodoItem>()
                .HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.StatusId);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Code = "clientSupp", Name = "Client support" },
                new Category { Id = 2, Code = "internalSupp", Name = "Internal Support" },
                new Category { Id = 3, Code = "dev", Name = "Development" },
                new Category { Id = 4, Code = "test", Name = "Testing" },
                new Category { Id = 5, Code = "internalTasks", Name = "Internal Tasks" }
            );
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Code = "open", Name = "Open" },
                new Status { Id = 2, Code = "closed", Name = "Closed" }
            );
        }
    }
}
