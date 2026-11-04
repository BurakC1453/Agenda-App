// Core/Entities/HabitItem.cs
namespace Core.Entities;

/// <summary>
/// Zincir (Alışkanlık) modülü için veri modeli. (Günlük alışkanlık, mevcut ve en uzun seri)
/// </summary>
public class HabitItem : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int CurrentStreak { get; set; } = 0; // Mevcut seri
    public int BestStreak { get; set; } = 0; // En uzun seri
    public DateTime? LastCompletedDate { get; set; }
}
