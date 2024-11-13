# EducationPortal

Eğitim Portalı Uygulaması - Restful API


## Kullanılan Teknolojiler ve Araçlar
Bu projede kullanılan başlıca teknolojiler ve yapı özellikleri aşağıdaki gibidir:

### ORM ve Veri Modelleme
- **Entity Framework**: ORM aracı olarak Entity Framework tercih edilmiştir ve **Code-First** yaklaşımıyla geliştirmeler yapılmıştır.

### Kimlik Doğrulama ve Yetkilendirme
- **JWT Bearer Token**: Kullanıcı doğrulama işlemleri JWT Bearer Token ile sağlanmıştır.
- **Asp.Net Core Identity**: Rol bazlı üyelik için Asp.Net Core'un sağladığı Identity altyapısı kullanılmıştır.

### Mimari Yapı
- **Onion Architecture**: Onion mimarisi sayesinde bütün katmanlar bağımsız ve sorumluluklarına odaklıdır. Ayrıca **Read** ve **Write** işlemleri ayrılarak **SOLID** prensipleri doğrultusunda (Single Responsibility ve Separation of Concerns prensipleri) geliştirilmiştir.
- **Interface ve Abstraction Kullanımı**: Sürdürülebilirliği artırmak amacıyla Interface ve abstractionlar kullanılarak, **Interface Segregation Prensipleri**'ne uygun bir yapı kurulmuştur. Servislerde **Dependency Injection** kullanarak bağımlılıklar minimuma indirilmiştir.
- **Repository Design Pattern**: **Read** ve **Write** işlemlerini ayıran Generic bir Repository tasarım deseni kullanılmıştır.
- **CQRS Design Pattern**: **CQRS** (Command Query Responsibility Segregation) tasarım deseni uygulanarak, proje daha kolay test edilebilir ve okunabilir hale getirilmiştir.
- **MediatR**: Request/Response sürecini ayrıştırmak ve uygulamayı daha modüler hale getirmek için **MediatR** kütüphanesi entegre edilmiştir.
- **DTO**: Veritabanı katmanı dış dünyadan izole edilmek için DTO (Data Transfer Object) kullanılmıştır.

### Validasyon ve Performans Optimizasyonu
- **Fluent Validation**: Veri tutarlılığını sağlamak amacıyla **Fluent Validation** kütüphanesi entegre edilmiştir.
- **Benchmark**: LinQ sorgularının performans analizleri için **Benchmark** kütüphanesi kullanılmış, veritabanı erişim işlemleri optimize edilmiştir.

### Arkaplan İşlemleri
- **Hangfire**: **Hangfire** ile arka planda operasyonlar zamanlanmış ve kullanıcılara e-posta gönderimi sağlanmıştır.

### Testler
- **xUnit**: Projede birim testleri **xUnit** kullanılarak yazılmıştır.
