// Core/Persistence/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Core.Persistence;

/// <summary>
/// Uygulama genelinde veritabanı işlemlerini yöneten DbContext sınıfı.
/// SQLite veritabanı bağlantısını ve Soft Delete filtrelerini yapılandırır.
/// </summary>
public class AppDbContext : DbContext
{
    // Notlar tablosu
    public DbSet<Note> Notes => Set<Note>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Uygulama klasöründe 'agenda.db' adında SQLite veritabanı dosyası oluşturur
        optionsBuilder.UseSqlite("Data Source=agenda.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // GLOBAL QUERY FILTER: Soft Delete (IsDeleted = true olan veriler sorgularda otomatik filtrelenir/gizlenir)
        modelBuilder.Entity<Note>().HasQueryFilter(n => !n.IsDeleted);
    }
}
