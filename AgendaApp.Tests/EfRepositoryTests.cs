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

    [Fact]
    public void GeneratePhysicalDatabaseFileForInspection()
    {
        // Bu test, proje klasöründe fiziksel bir 'agenda.db' dosyası oluşturur 
        // ve içine örnek bir Not / Todo ekler ki DB Browser ile inceleyebilesin.
        var dbPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName, "agenda.db");
        
        // Eğer eski varsa silip yeniden oluşturalım
        if (File.Exists(dbPath)) File.Delete(dbPath);

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite($"Data Source={dbPath}")
            .Options;

        using var db = new AppDbContext(options);
        db.Database.EnsureCreated();

        db.Notes.Add(new Note { Title = "Merhaba Dünya", Content = "Bu fiziksel SQLite dosyasıdır." });
        db.TodoItems.Add(new TodoItem { Title = "Örnek Görev", Description = "DB Browser ile inceleniyor", Priority = 2 });
        db.SaveChanges();

        Assert.True(File.Exists(dbPath));
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}

