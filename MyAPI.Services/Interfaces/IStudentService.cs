// ========================================
// TEMPLATE CHO SERVICE INTERFACE MỚI
// ========================================
// Khi tạo API mới, copy file này và đổi tên interface + models
// Ví dụ: IProductService, ProductResponse, AddProductRequest, UpdateProductRequest

using MyAPI.Services.Models; // Import DTO models (Request/Response classes)

namespace MyAPI.Services.Interfaces
{
    // Interface định nghĩa contract cho Service layer
    // Tất cả Service classes phải implement interface này
    // Service layer chứa business logic và chuyển đổi Entity ↔ DTO
    public interface IStudentService
    {
        // ========================================
        // GET BY ID - Lấy record theo ID
        // ========================================
        // Task<StudentResponse> - async method trả về DTO hoặc null
        // string id - ID của record cần lấy
        Task<StudentResponse> GetByIdAsync(string id);
        
        // ========================================
        // GET ALL - Lấy tất cả records
        // ========================================
        // Task<List<StudentResponse>> - async method trả về danh sách DTOs
        Task<List<StudentResponse>> GetAllAsync();
        
        // ========================================
        // CREATE - Tạo record mới
        // ========================================
        // Task<StudentResponse> - async method trả về DTO của record đã tạo
        // AddStudentRequest request - DTO chứa dữ liệu cần tạo
        Task<StudentResponse> AddStudentAsync(AddStudentRequest request);
        
        // ========================================
        // UPDATE - Cập nhật record
        // ========================================
        // Task<StudentResponse> - async method trả về DTO của record đã update hoặc null
        // UpdateStudentRequest request - DTO chứa dữ liệu cần update
        Task<StudentResponse> UpdateStudentAsync(UpdateStudentRequest request);
        
        // ========================================
        // DELETE - Xóa record
        // ========================================
        // Task<bool> - async method trả về true nếu xóa thành công
        // string id - ID của record cần xóa
        Task<bool> DeleteStudentAsync(string id);
        
        // ========================================
        // CÁC PATTERN THƯỜNG DÙNG TRONG SERVICE INTERFACE:
        // ========================================
        // 1. DTO Parameters: Nhận Request DTOs làm input
        // 2. DTO Returns: Trả về Response DTOs
        // 3. Async Methods: Tất cả methods đều async
        // 4. Business Logic: Chứa validation, business rules
        // 5. Entity ↔ DTO Conversion: Chuyển đổi giữa Entity và DTO
        // 6. Repository Calls: Gọi Repository layer để truy cập database
    }
}