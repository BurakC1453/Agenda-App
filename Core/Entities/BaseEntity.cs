// Core/Entities/BaseEntity.cs
namespace Core.Entities;

/// <summary>
/// Tüm veri modelleri (Entity) için ortak alanları barındıran temel sınıftır.
/// Doküman Gereksinimi: 2. dönem bulut ve mobil geçişine hazırlık için Id'ler GUID olarak tanımlanmıştır.
/// </summary>
public abstract class BaseEntity
{
    // Benzersiz kayıt kimliği (GUID - Bulut/Mobil senkronizasyonu için)
    public Guid Id { get; set; } = Guid.NewGuid();

    // Kaydın oluşturulma zamanı (UTC formatında)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Kaydın son güncellenme zamanı (UTC formatında)
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Soft Delete (Yumuşak Silme) bayrağı. 
    // True olduğunda veri veritabanından fiziksel olarak silinmez, silindi olarak işaretlenir.
    public bool IsDeleted { get; set; } = false;
}
