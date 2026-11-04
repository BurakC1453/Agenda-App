// Core/Entities/EventItem.cs
namespace Core.Entities;

/// <summary>
/// Etkinlik modülü için veri modeli. (Saat aralıklı kayıtlar, kategori, çakışma kontrolü)
/// </summary>
public class EventItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
