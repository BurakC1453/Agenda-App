// Core/Entities/ReminderItem.cs
namespace Core.Entities;

/// <summary>
/// Hatırlatıcı modülü için veri modeli. (Zaman tabanlı, uygulama içi uyarılar)
/// </summary>
public class ReminderItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime ReminderTime { get; set; }
    public bool IsTriggered { get; set; } = false;
}
