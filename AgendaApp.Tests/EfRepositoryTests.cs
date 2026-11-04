// AgendaApp.Tests/EfRepositoryTests.cs
using Core.Entities;
using Core.Persistence;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AgendaApp.Tests;

public class EfRepositoryTests : IDisposable
{
    private readonly AppDbContext _context;
    private readonly EfRepository<TodoItem> _todoRepository;

    public EfRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        _context = new AppDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _todoRepository = new EfRepository<TodoItem>(_context);
    }

    [Fact]
    public async Task AddAsync_ShouldAddTodoItemAndRetrieveIt()
    {
        var todo = new TodoItem
        {
            Title = "Proje Görevi",
            Description = "EF Core ve SQLite entegrasyonu tamamlanacak",
            Priority = 3
        };

        await _todoRepository.AddAsync(todo);
        var todos = await _todoRepository.GetAllAsync();

        var retrieved = Assert.Single(todos);
        Assert.Equal("Proje Görevi", retrieved.Title);
        Assert.Equal(3, retrieved.Priority);
        Assert.False(retrieved.IsDeleted);
    }

    [Fact]
    public async Task SoftDeleteAsync_ShouldHideTodoItemFromGetAll()
    {
        var todo = new TodoItem
        {
            Title = "Geçici Görev",
            Description = "Silinecek"
        };
        await _todoRepository.AddAsync(todo);

        await _todoRepository.SoftDeleteAsync(todo.Id);
        var todos = await _todoRepository.GetAllAsync();

        Assert.Empty(todos);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
