# Üye 4 - Veri Kalıcılığı ve EF Core Mimarisi Notları

Bu doküman, Üye 4 sorumluluğunda geliştirilen SQLite ve Entity Framework Core altyapısının ne amaçla yazıldığını ve nasıl çalıştığını özetlemektedir.

## 1. Temel Sınıf (`BaseEntity.cs`)
- Projedeki tüm veritabanı tablolarının (Entity) atasıdır.
- `Id` (Primary Key), `CreatedAt` (Kayıt tarihi) ve `IsDeleted` (Soft Delete bayrağı) alanlarını ortak olarak sağlar.

## 2. Örnek Model (`Note.cs`)
- Garanti modüllerden biri olan "Not" modülünün veri modelidir. `BaseEntity` sınıfından türer.

## 3. Veritabanı Bağlantısı ve Soft Delete (`AppDbContext.cs`)
- **SQLite:** Dosya tabanlı hafif veritabanı olan SQLite bağlantısını (`agenda.db`) yapılandırır.
- **Global Query Filter (Soft Delete):** EF Core üzerinde `HasQueryFilter(n => !n.IsDeleted)` kuralı tanımlanmıştır. Bu sayede bir veri silindiğinde (`IsDeleted = true` yapıldığında), yapılan `Get` / `Listeleme` sorgularında bu veriler otomatik olarak gizlenir; veritabanından tamamen silinmemiş olur.
