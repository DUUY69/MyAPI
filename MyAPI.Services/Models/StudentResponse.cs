// ========================================
// TEMPLATE CHO RESPONSE MODEL MỚI
// ========================================
// Khi tạo API mới, copy file này và đổi tên class + properties
// Ví dụ: ProductResponse, ProductName, ProductPrice, etc.

namespace MyAPI.Services.Models
{
    // DTO (Data Transfer Object) cho việc trả về dữ liệu Student cho client
    // Response model chứa dữ liệu từ server gửi về client
    // Thường chỉ chứa các field cần thiết, không expose toàn bộ Entity
    public class StudentResponse
    {
        // Property Mssv - Mã số sinh viên
        // Primary key, unique identifier của student
        public string Mssv { get; set; }
        
        // Property Name - Tên sinh viên
        // Thông tin cơ bản của student
        public string Name { get; set; }
        
        // ========================================
        // CÁC PATTERN THƯỜNG DÙNG TRONG RESPONSE MODEL:
        // ========================================
        // 1. Read-Only Data: Chỉ chứa dữ liệu để hiển thị, không chứa sensitive data
        // 2. Computed Properties: Có thể thêm computed properties
        // 3. Nested Objects: Có thể chứa các object con
        // 4. JSON Serialization: Tự động serialize thành JSON khi trả về API
        // 5. Nullable Types: string? nếu có thể null
        
        // Ví dụ với computed properties:
        // public string DisplayName => $"{Mssv} - {Name}";
        
        // Ví dụ với nested objects:
        // public List<ScheduleResponse> Schedules { get; set; }
        
        // Ví dụ với additional properties:
        // public DateTime CreatedDate { get; set; }
        // public bool IsActive { get; set; }
    }
}