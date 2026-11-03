// Core/Entities/Note.cs
namespace Core.Entities;

/// <summary>
/// Not modülü için veri modeli. BaseEntity'den türemiştir (Id, CreatedAt, IsDeleted içerir).
/// </summary>
public class Note : BaseEntity
{
    // Notun başlığı
    public string Title { get; set; } = string.Empty;

    // Notun içeriği
    public string Content { get; set; } = string.Empty;
}
