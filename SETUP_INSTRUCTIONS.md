# 🚀 Hướng dẫn thiết lập dự án MyAPI

## 📋 Yêu cầu hệ thống
- .NET 8.0 SDK
- Visual Studio 2022 hoặc VS Code
- SQL Server (LocalDB hoặc SQL Server Express)

## 🛠️ Các bước thiết lập

### 1. Mở dự án
```bash
# Mở solution file
MyAPI.sln
```

### 2. Cài đặt dependencies
```bash
dotnet restore
```

### 3. Cấu hình database
- Mở file `MyAPI.WebApi/appsettings.json`
- Cập nhật connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=YOUR_SERVER;Initial Catalog=MyAPI_DB;Persist Security Info=True;Integrated Security=true;Encrypt=False;Trust Server Certificate=True"
}
```

### 4. Tạo database và chạy migrations
```bash
# Tạo migration
dotnet ef migrations add InitialCreate --project MyAPI.Repositories --startup-project MyAPI.WebApi

# Cập nhật database
dotnet ef database update --project MyAPI.Repositories --startup-project MyAPI.WebApi
```

### 5. Chạy ứng dụng
```bash
dotnet run --project MyAPI.WebApi
```

### 6. Kiểm tra API
- Mở trình duyệt: `https://localhost:7024/swagger`
- Hoặc sử dụng file `MyAPI.WebApi.http` để test API

## 📁 Cấu trúc dự án
```
MyAPI_Project/
├── MyAPI.sln                    # Solution file
├── README.md                    # Tài liệu chính
├── SETUP_INSTRUCTIONS.md        # File này
├── MyAPI.Repositories/          # Data Access Layer
├── MyAPI.Services/              # Business Logic Layer
└── MyAPI.WebApi/               # Presentation Layer
```

## 🔧 Các lệnh hữu ích

### Tạo entity mới từ database
```bash
# Sử dụng EF Core Power Tools (khuyến nghị)
# Right-click project → EF Core Power Tools → Reverse Engineer

# Hoặc sử dụng CLI
dotnet ef dbcontext scaffold "ConnectionString" Microsoft.EntityFrameworkCore.SqlServer --output-dir Entities --context MyAPIContext --project MyAPI.Repositories --startup-project MyAPI.WebApi
```

### Thêm migration mới
```bash
dotnet ef migrations add MigrationName --project MyAPI.Repositories --startup-project MyAPI.WebApi
```

### Cập nhật database
```bash
dotnet ef database update --project MyAPI.Repositories --startup-project MyAPI.WebApi
```

## 🐛 Xử lý lỗi thường gặp

### Lỗi connection string
- Kiểm tra SQL Server đang chạy
- Kiểm tra tên database và server
- Đảm bảo có quyền truy cập database

### Lỗi migration
- Xóa thư mục Migrations và tạo lại
- Kiểm tra connection string
- Đảm bảo database tồn tại

### Lỗi build
- Chạy `dotnet clean` và `dotnet restore`
- Kiểm tra .NET 8.0 SDK đã cài đặt
- Kiểm tra references giữa các project

## 📞 Hỗ trợ
Nếu gặp vấn đề, hãy kiểm tra:
1. README.md để hiểu cấu trúc dự án
2. Logs trong console khi chạy ứng dụng
3. Swagger UI để test API endpoints

---
**Lưu ý**: Dự án này được tạo dựa trên kiến trúc 3-Layer Architecture với .NET 8 và Entity Framework Core.
