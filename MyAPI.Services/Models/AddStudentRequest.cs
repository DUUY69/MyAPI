// ========================================
// TEMPLATE CHO REQUEST MODEL MỚI
// ========================================
// Khi tạo API mới, copy file này và đổi tên class + properties
// Ví dụ: AddProductRequest, ProductName, ProductPrice, etc.

namespace MyAPI.Services.Models
{
    // DTO (Data Transfer Object) cho việc tạo mới Student
    // Request model chứa dữ liệu từ client gửi lên server
    // Tách biệt với Entity model để bảo mật và linh hoạt
    public class AddStudentRequest
    {
        // Property Name - Tên sinh viên
        // public string - access modifier public, kiểu dữ liệu string
        // { get; set; } - auto-implemented property (tự động tạo getter/setter)
        public string Name { get; set; }
        
        // Property Mssv - Mã số sinh viên
        // MSSV thường là primary key, unique identifier
        public string Mssv { get; set; }
        
        // ========================================
        // CÁC PATTERN THƯỜNG DÙNG TRONG REQUEST MODEL:
        // ========================================
        // 1. Auto Properties: { get; set; } cho simple properties
        // 2. Data Annotations: [Required], [StringLength], [EmailAddress], etc.
        // 3. Validation: Có thể thêm validation attributes
        // 4. Naming Convention: PascalCase cho property names
        // 5. Nullable Types: string? nếu có thể null
        // 6. Default Values: = "default" nếu cần giá trị mặc định
        
        // Ví dụ với Data Annotations:
        // [Required(ErrorMessage = "Name is required")]
        // [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        // public string Name { get; set; }
        
        // [Required(ErrorMessage = "MSSV is required")]
        // [StringLength(20, ErrorMessage = "MSSV cannot exceed 20 characters")]
        // public string Mssv { get; set; }
    }
}