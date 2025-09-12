# MyAPI - .NET 8 Web API Project

## 📋 Mô tả
Dự án MyAPI được tạo dựa trên kiến trúc 3-Layer Architecture tương tự như dự án SMMS, sử dụng .NET 8 và Entity Framework Core.

## 🏗️ Kiến trúc
```
MyAPI/
├── MyAPI.Repositories/     # Data Access Layer
├── MyAPI.Services/         # Business Logic Layer  
└── MyAPI.WebApi/          # Presentation Layer (API)
```

## 🚀 Cài đặt và chạy

### 1. Cài đặt dependencies
```bash
dotnet restore
```

### 2. Cấu hình database
- Cập nhật connection string trong `appsettings.json`
- Tạo database mới hoặc sử dụng database hiện có

### 3. Chạy migrations (nếu cần)
```bash
dotnet ef migrations add InitialCreate --project MyAPI.Repositories --startup-project MyAPI.WebApi
dotnet ef database update --project MyAPI.Repositories --startup-project MyAPI.WebApi
```

### 4. Chạy ứng dụng
```bash
dotnet run --project MyAPI.WebApi
```

## 📁 Cấu trúc thư mục

### MyAPI.Repositories
- `Entities/`: Database models
- `Interfaces/`: Repository interfaces
- `Infrastructure/`: Base repository pattern
- `MyAPIContext.cs`: Entity Framework DbContext

### MyAPI.Services
- `Models/`: DTOs (Data Transfer Objects)
- `Interfaces/`: Service interfaces
- `Common/`: Utilities (HashHelper, AppSettings)
- `Exceptions/`: Custom exception handling

### MyAPI.WebApi
- `Controllers/`: REST API endpoints
- `Middlewares/`: Error handling middleware
- `Program.cs`: Application configuration

## 🔧 Các công nghệ sử dụng
- .NET 8.0
- Entity Framework Core 9.0.1
- SQL Server
- Swagger/OpenAPI
- CORS

## 📝 API Endpoints

### User Controller
- `POST /api/user/login` - Đăng nhập
- `GET /api/user/{id}` - Lấy thông tin user theo ID
- `GET /api/user` - Lấy danh sách tất cả users
- `POST /api/user/add` - Thêm user mới
- `PUT /api/user/update` - Cập nhật user
- `DELETE /api/user/{id}` - Xóa user

## 🛠️ Thêm entity mới

### Bước 1: Tạo Entity
```csharp
// MyAPI.Repositories/Entities/Product.cs
public partial class Product : IEntityBase
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

### Bước 2: Cập nhật DbContext
```csharp
// MyAPIContext.cs
public virtual DbSet<Product> Products { get; set; }
```

### Bước 3: Tạo Repository
```csharp
// MyAPI.Repositories/Interfaces/IProductRepository.cs
public interface IProductRepository : IRepository<Product>
{
    // Custom methods
}

// MyAPI.Repositories/ProductRepository.cs
public class ProductRepository : Repository<Product>, IProductRepository
{
    // Implementation
}
```

### Bước 4: Tạo Service
```csharp
// MyAPI.Services/Interfaces/IProductService.cs
public interface IProductService
{
    Task<ProductResponse> GetByIdAsync(int id);
    // Other methods
}

// MyAPI.Services/ProductService.cs
public class ProductService : IProductService
{
    // Implementation
}
```

### Bước 5: Tạo Controller
```csharp
// MyAPI.WebApi/Controllers/ProductController.cs
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    // API endpoints
}
```

### Bước 6: Đăng ký DI
```csharp
// Program.cs
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
```

## 🔐 Bảo mật
- Password được hash bằng SHA256
- CORS được cấu hình cho frontend
- Error handling middleware

## 📞 Hỗ trợ
Nếu có vấn đề gì, vui lòng tạo issue hoặc liên hệ team phát triển.
