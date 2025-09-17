// ========================================
// TEMPLATE CHO SERVICE MỚI
// ========================================
// Khi tạo API mới, copy file này và đổi tên class + repository
// Ví dụ: ProductService, IProductRepository, ProductRepository

using Microsoft.EntityFrameworkCore; // Import để sử dụng ToListAsync()
using MyAPI.Repositories.Interfaces; // Import interface của Repository layer
using MyAPI.Repositories.Entities; // Import Entity models
using MyAPI.Services.Interfaces; // Import interface của Service layer
using MyAPI.Services.Models; // Import DTO models

namespace MyAPI.Services
{
    // Service class implement interface IStudentService
    // Service layer chứa business logic, validation, và chuyển đổi Entity ↔ DTO
    public class StudentService : IStudentService
    {
        // Dependency Injection - inject Repository interface vào Service
        private readonly IStudentRepository _studentRepository;

        // Constructor - DI container sẽ tự động inject IStudentRepository implementation
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // ========================================
        // GET BY ID - Lấy record theo ID
        // ========================================
        public async Task<StudentResponse> GetByIdAsync(string id)
        {
            // Gọi Repository để lấy Entity từ database
            var s = await _studentRepository.GetByMssvAsync(id);
            // Chuyển đổi Entity → DTO (Data Transfer Object)
            // Nếu không tìm thấy trả về null, nếu có thì tạo DTO mới
            return s == null ? null : new StudentResponse { Mssv = s.Mssv, Name = s.Name };
        }

        // ========================================
        // GET ALL - Lấy tất cả records
        // ========================================
        public async Task<List<StudentResponse>> GetAllAsync()
        {
            // Gọi Repository để lấy tất cả Entity từ database
            var list = await _studentRepository.GetAll().ToListAsync();
            // Chuyển đổi List<Entity> → List<DTO> bằng LINQ Select
            return list.Select(s => new StudentResponse { Mssv = s.Mssv, Name = s.Name }).ToList();
        }

        // ========================================
        // CREATE - Tạo record mới
        // ========================================
        public async Task<StudentResponse> AddStudentAsync(AddStudentRequest request)
        {
            // Chuyển đổi DTO → Entity
            var entity = new Student { Mssv = request.Mssv, Name = request.Name };
            // Gọi Repository để insert vào database
            var added = await _studentRepository.InsertAsync(entity);
            // Chuyển đổi Entity → DTO để trả về
            return new StudentResponse { Mssv = added.Mssv, Name = added.Name };
        }

        // ========================================
        // UPDATE - Cập nhật record
        // ========================================
        public async Task<StudentResponse> UpdateStudentAsync(UpdateStudentRequest request)
        {
            // Tìm Entity cần update
            var entity = await _studentRepository.GetByMssvAsync(request.Mssv);
            // Nếu không tìm thấy trả về null
            if (entity == null) return null;
            
            // Cập nhật dữ liệu Entity
            entity.Name = request.Name;
            // Gọi Repository để update vào database
            var updated = await _studentRepository.UpdateAsync(entity);
            // Chuyển đổi Entity → DTO để trả về
            return new StudentResponse { Mssv = updated.Mssv, Name = updated.Name };
        }

        // ========================================
        // DELETE - Xóa record
        // ========================================
        public async Task<bool> DeleteStudentAsync(string id)
        {
            // Gọi Repository để xóa khỏi database
            await _studentRepository.DeleteAsync(id);
            // Trả về true để báo thành công
            return true;
        }
        
        // ========================================
        // CÁC PATTERN THƯỜNG DÙNG TRONG SERVICE:
        // ========================================
        // 1. Validation: Kiểm tra dữ liệu đầu vào
        // 2. Business Logic: Xử lý logic nghiệp vụ
        // 3. Entity ↔ DTO Conversion: Chuyển đổi giữa Entity và DTO
        // 4. Repository Calls: Gọi Repository để truy cập database
        // 5. Exception Handling: Xử lý lỗi và throw custom exceptions
        // 6. Logging: Ghi log các hoạt động quan trọng
    }
}
