## Migration Anlama Rehberi

1. Entity Modelinde değişiklik yap.
    Product.cs dosyasında git yeni property ekle.

2. Migration oluştur.
    dotnet ef migrations add MigrationName

3. Migrations klasörünün içinde oluşan yeni migration dosyasını incele, özellikle Up metodunu!

4. Migration'ı uygula.
    dotnet ef database update (bu komut Up metodunu çalıştıracak)

5. Artık veri tabanın, C# kodunla uyumlu bir hale geldi yani güncel.

Not: Eğer migration'ı oluşturduktan hemen sonra, uygulamadan hemen önce; bir hata yaptığını fark ettiysen migration'ı geri al
    dotnet ef migrations remove

