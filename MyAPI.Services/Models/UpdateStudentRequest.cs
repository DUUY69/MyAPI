// ========================================
// TEMPLATE CHO UPDATE REQUEST MODEL MỚI
// ========================================
// Khi tạo API mới, copy file này và đổi tên class + properties
// Ví dụ: UpdateProductRequest, ProductName, ProductPrice, etc.

namespace MyAPI.Services.Models
{
    // DTO (Data Transfer Object) cho việc cập nhật Student
    // Update Request model chứa dữ liệu từ client gửi lên để update
    // Thường chứa ID để identify record cần update + các field cần thay đổi
    public class UpdateStudentRequest
    {
        // Property Name - Tên sinh viên cần cập nhật
        // Có thể null nếu không cần update field này
        public string Name { get; set; }
        
        // Property Mssv - Mã số sinh viên (ID)
        // Bắt buộc để identify record cần update
        // Thường không thay đổi trong update operation
        public string Mssv { get; set; }
        
        // ========================================
        // CÁC PATTERN THƯỜNG DÙNG TRONG UPDATE REQUEST MODEL:
        // ========================================
        // 1. ID Field: Luôn cần ID để identify record cần update
        // 2. Optional Fields: Các field có thể null nếu không cần update
        // 3. Partial Update: Chỉ update các field được gửi lên
        // 4. Validation: Validate ID tồn tại, các field hợp lệ
        // 5. Nullable Types: string? cho các field optional
        
        // Ví dụ với nullable fields:
        // public string? Name { get; set; } // Có thể null
        // public string Mssv { get; set; } // Bắt buộc
        
        // Ví dụ với validation attributes:
        // [Required(ErrorMessage = "MSSV is required for update")]
        // public string Mssv { get; set; }
        
        // [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        // public string? Name { get; set; }
        
        // Ví dụ với additional fields:
        // public string? Email { get; set; }
        // public DateTime? BirthDate { get; set; }
        // public bool? IsActive { get; set; }
    }
}