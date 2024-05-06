using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoWonderBitsBackend.Domain.Models;
using ToDoWonderBitsBackend.Infrastructure.Persistence;
using ToDoWonderBitsBackend.Infrastructure.Persistence.Repositories;
using Xunit;

public class UserRepositoryTests
{
    private readonly DbContextOptions<TodoContext> _dbContextOptions;

    public UserRepositoryTests()
    {
        // Configura las opciones del DbContext para usar una base de datos en memoria
        _dbContextOptions = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TestDb") // Asegúrate de que el nombre de la base de datos sea único para evitar colisiones entre pruebas
            .Options;
    }

    [Fact]
    public async Task CreateUserAsync_UserIsAdded()
    {
        using (var context = new TodoContext(_dbContextOptions))
        {
            var repository = new UserRepository(context);
            var user = new User { Username = "newuser", Password = "password123" };

            await repository.CreateAsync(user);
            var retrievedUser = await context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

            Assert.NotNull(retrievedUser);
            Assert.Equal("newuser", retrievedUser.Username);
        }
    }

    [Fact]
    public async Task GetUserByIdAsync_UserExists()
    {
        using (var context = new TodoContext(_dbContextOptions))
        {
            context.Users.Add(new User { Username = "testuser", Password = "password123" });
            context.SaveChanges();

            var repository = new UserRepository(context);
            var user = await repository.GetByIdAsync(1); // Asumiendo que el ID es 1

            Assert.NotNull(user);
            Assert.Equal("testuser", user.Username);
        }
    }

    [Fact]
    public async Task UpdateUserAsync_UserIsUpdated()
    {
        using (var context = new TodoContext(_dbContextOptions))
        {
            var user = new User { Username = "updatable", Password = "password123" };
            context.Users.Add(user);
            context.SaveChanges();

            var repository = new UserRepository(context);
            user.Username = "updatedUser";
            await repository.UpdateAsync(user);

            var updatedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            Assert.NotNull(updatedUser);
            Assert.Equal("updatedUser", updatedUser.Username);
        }
    }

    [Fact]
    public async Task DeleteUserAsync_UserIsDeleted()
    {
        using (var context = new TodoContext(_dbContextOptions))
        {
            var user = new User { Username = "deletable", Password = "password123" };
            context.Users.Add(user);
            context.SaveChanges();

            var repository = new UserRepository(context);
            await repository.DeleteAsync(user.Id);

            var deletedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            Assert.Null(deletedUser);
        }
    }
}
