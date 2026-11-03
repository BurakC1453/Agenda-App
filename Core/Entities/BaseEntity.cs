// Core/Entities/BaseEntity.cs
namespace Core.Entities;

/// <summary>
/// Tüm veri modelleri (Entity) için ortak alanları barındıran temel sınıftır.
/// </summary>
public abstract class BaseEntity
{
    // Benzersiz kayıt kimliği (Primary Key)
    public int Id { get; set; }

    // Kaydın oluşturulma zamanı (UTC formatında)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Soft Delete (Yumuşak Silme) bayrağı. 
    // True olduğunda veri veritabanından fiziksel olarak silinmez, silindi olarak işaretlenir.
    public bool IsDeleted { get; set; } = false;
}
