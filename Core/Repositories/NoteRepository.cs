// Core/Repositories/NoteRepository.cs
using Core.Entities;
using Core.Interfaces;
using Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories;

/// <summary>
/// Note varlığı için veritabanı işlemlerini (CRUD + Soft Delete) gerçekleştiren Repository sınıfı.
/// </summary>
public class NoteRepository : IRepository<Note>
{
    private readonly AppDbContext _context;

    public NoteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Note>> GetAllAsync()
    {
        // Global Query Filter sayesinde IsDeleted = true olanlar otomatik olarak gelmeyecektir.
        return await _context.Notes.ToListAsync();
    }

    public async Task<Note?> GetByIdAsync(Guid id)
    {
        return await _context.Notes.FindAsync(id);
    }

    public async Task AddAsync(Note entity)
    {
        await _context.Notes.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Note entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Notes.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        // FindAsync global filtreye takılabilir, o yüzden IgnoreQueryFilters ile arıyoruz
        var entity = await _context.Notes.IgnoreQueryFilters().FirstOrDefaultAsync(n => n.Id == id);
        if (entity != null)
        {
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Notes.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
