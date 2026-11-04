// Core/Entities/TodoItem.cs
namespace Core.Entities;

/// <summary>
/// To-do modülü için veri modeli. (Ekle, düzenle, sil, tamamla, öncelik, alt görev vb.)
/// </summary>
public class TodoItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
    public int Priority { get; set; } = 1; // 1: Düşük, 2: Orta, 3: Yüksek
    public Guid? ParentId { get; set; } // Alt görevler için üst görev referansı
    public DateTime? DueDate { get; set; } // Son teslim tarihi
}
