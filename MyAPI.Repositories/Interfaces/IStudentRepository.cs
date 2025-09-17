// ========================================
// TEMPLATE CHO REPOSITORY INTERFACE MỚI
// ========================================
// Khi tạo API mới, copy file này và đổi tên interface + entity
// Ví dụ: IProductRepository, Product entity

using System.Linq; // Import để sử dụng IQueryable
using System.Threading.Tasks; // Import để sử dụng Task, async/await
using MyAPI.Repositories.Entities; // Import Entity models

namespace MyAPI.Repositories.Interfaces
{
    // Interface định nghĩa contract cho Repository layer
    // Tất cả Repository classes phải implement interface này
    // Interface giúp tách biệt implementation và contract (Dependency Inversion Principle)
    public interface IStudentRepository
    {
        // ========================================
        // GET ALL - Lấy tất cả records
        // ========================================
        // Trả về IQueryable để có thể thêm Where, OrderBy, etc. ở Service layer
        // IQueryable cho phép deferred execution (chỉ thực thi khi cần)
        IQueryable<Student> GetAll();
        
        // ========================================
        // GET BY ID - Lấy record theo ID
        // ========================================
        // Task<Student> - async method trả về Student hoặc null
        // string mssv - parameter để tìm kiếm
        Task<Student> GetByMssvAsync(string mssv);
        
        // ========================================
        // INSERT - Thêm record mới
        // ========================================
        // Task<Student> - async method trả về Student đã được insert
        // Student student - entity cần insert
        Task<Student> InsertAsync(Student student);
        
        // ========================================
        // UPDATE - Cập nhật record
        // ========================================
        // Task<Student> - async method trả về Student đã được update
        // Student student - entity cần update
        Task<Student> UpdateAsync(Student student);
        
        // ========================================
        // DELETE - Xóa record
        // ========================================
        // Task - async method không trả về giá trị
        // string mssv - ID của record cần xóa
        Task DeleteAsync(string mssv);
        
        // ========================================
        // CÁC PATTERN THƯỜNG DÙNG TRONG INTERFACE:
        // ========================================
        // 1. CRUD Operations: Create, Read, Update, Delete
        // 2. Async Methods: Tất cả methods đều async để tránh blocking
        // 3. Entity Parameters: Nhận và trả về Entity objects
        // 4. IQueryable: Cho phép flexible querying ở Service layer
        // 5. Nullable Returns: Có thể trả về null nếu không tìm thấy
    }
}