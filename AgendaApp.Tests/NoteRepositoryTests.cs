// AgendaApp.Tests/NoteRepositoryTests.cs
using Core.Entities;
using Core.Persistence;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AgendaApp.Tests;

public class NoteRepositoryTests : IDisposable
{
    private readonly AppDbContext _context;
    private readonly NoteRepository _repository;

    public NoteRepositoryTests()
    {
        // SQLite In-Memory bağlantısı
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        _context = new AppDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _repository = new NoteRepository(_context);
    }


    [Fact]
    public async Task AddAsync_ShouldAddNoteAndRetrieveIt()
    {
        // Arrange
        var note = new Note
        {
            Title = "Test Notu",
            Content = "Test İçeriği"
        };

        // Act
        await _repository.AddAsync(note);
        var notes = await _repository.GetAllAsync();

        // Assert
        var retrievedNote = Assert.Single(notes);
        Assert.Equal("Test Notu", retrievedNote.Title);
        Assert.False(retrievedNote.IsDeleted);
    }

    [Fact]
    public async Task SoftDeleteAsync_ShouldMarkNoteAsDeletedAndHideFromGet()
    {
        // Arrange
        var note = new Note
        {
            Title = "Silinecek Not",
            Content = "İçerik"
        };
        await _repository.AddAsync(note);

        // Act
        await _repository.SoftDeleteAsync(note.Id);
        var notes = await _repository.GetAllAsync();

        // Assert
        // Global Query Filter nedeniyle IsDeleted = true olanlar gelmemelidir
        Assert.Empty(notes);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}

