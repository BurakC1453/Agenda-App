// Core/Persistence/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Core.Persistence;

/// <summary>
/// Uygulama genelinde veritabanı işlemlerini yöneten DbContext sınıfı.
/// SQLite bağlantısı ve tüm garanti modüller için Soft Delete (Global Query Filter) yapılandırmasını içerir.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Garanti Modül Tabloları
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<EventItem> EventItems => Set<EventItem>();
    public DbSet<HabitItem> HabitItems => Set<HabitItem>();
    public DbSet<ReminderItem> ReminderItems => Set<ReminderItem>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Uygulama klasöründe 'agenda.db' adında SQLite veritabanı dosyası oluşturur
            optionsBuilder.UseSqlite("Data Source=agenda.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // GLOBAL QUERY FILTER: Tüm varlıklar için Soft Delete (IsDeleted = true olanlar otomatik gizlenir)
        modelBuilder.Entity<Note>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<TodoItem>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<EventItem>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<HabitItem>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ReminderItem>().HasQueryFilter(e => !e.IsDeleted);
    }
}


