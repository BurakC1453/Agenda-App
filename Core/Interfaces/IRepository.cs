// Core/Interfaces/IRepository.cs
using Core.Entities;

namespace Core.Interfaces;

/// <summary>
/// Tüm Entity türleri için standart CRUD ve veri erişim sözleşmesini (Interface) tanımlar.
/// </summary>
public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task SoftDeleteAsync(Guid id); // Fiziksel silme yerine IsDeleted = true yapar
}
