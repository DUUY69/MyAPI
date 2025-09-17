// ========================================
// TEMPLATE CHO REPOSITORY MỚI
// ========================================
// Khi tạo API mới, copy file này và đổi tên class + entity
// Ví dụ: ProductRepository, IProductRepository, Product entity

using Microsoft.EntityFrameworkCore; // Import để sử dụng Entity Framework methods
using MyAPI.Repositories.Entities; // Import Entity models
using MyAPI.Repositories.Interfaces; // Import interface của Repository layer

namespace MyAPI.Repositories
{
    // Repository class implement interface IStudentRepository
    // Repository layer chứa logic truy cập database, thực hiện CRUD operations
    public class StudentRepository : IStudentRepository
    {
        // Dependency Injection - inject DbContext để truy cập database
        private readonly MyAPIContext _ctx;

        // Constructor - DI container sẽ tự động inject MyAPIContext
        public StudentRepository(MyAPIContext dbContext)
        {
            _ctx = dbContext;
        }

        // ========================================
        // GET ALL - Lấy tất cả records (IQueryable)
        // ========================================
        // Trả về IQueryable để có thể thêm Where, OrderBy, etc. ở Service layer
        public IQueryable<Student> GetAll()
        {
            // AsQueryable() cho phép thực hiện LINQ queries
            return _ctx.Students.AsQueryable();
        }

        // ========================================
        // INSERT - Thêm record mới
        // ========================================
        public async Task<Student> InsertAsync(Student student)
        {
            // AddAsync() thêm entity vào DbSet (chưa lưu vào database)
            await _ctx.Students.AddAsync(student);
            // SaveChangesAsync() thực sự lưu thay đổi vào database
            await _ctx.SaveChangesAsync();
            // Trả về entity đã được lưu (có thể có ID được generate)
            return student;
        }

        // ========================================
        // UPDATE - Cập nhật record
        // ========================================
        public async Task<Student> UpdateAsync(Student student)
        {
            // Update() đánh dấu entity là đã thay đổi
            _ctx.Students.Update(student);
            // SaveChangesAsync() lưu thay đổi vào database
            await _ctx.SaveChangesAsync();
            // Trả về entity đã được cập nhật
            return student;
        }

        // ========================================
        // DELETE - Xóa record
        // ========================================
        public async Task DeleteAsync(string mssv)
        {
            // Tìm entity cần xóa
            var student = await GetByMssvAsync(mssv);
            // Nếu tìm thấy thì xóa
            if (student != null)
            {
                // Remove() đánh dấu entity để xóa
                _ctx.Students.Remove(student);
                // SaveChangesAsync() thực sự xóa khỏi database
                await _ctx.SaveChangesAsync();
            }
        }

        // ========================================
        // GET BY ID - Lấy record theo ID
        // ========================================
        public async Task<Student> GetByMssvAsync(string mssv)
        {
            // FirstOrDefaultAsync() tìm record đầu tiên thỏa mãn điều kiện
            // Nếu không tìm thấy trả về null
            return await _ctx.Students.FirstOrDefaultAsync(x => x.Mssv == mssv);
        }
        
        // ========================================
        // CÁC PATTERN THƯỜNG DÙNG TRONG REPOSITORY:
        // ========================================
        // 1. CRUD Operations: Create, Read, Update, Delete
        // 2. Query Methods: GetAll(), GetById(), GetByCondition()
        // 3. Async/Await: Tất cả database operations đều async
        // 4. Entity Framework: Sử dụng EF Core để truy cập database
        // 5. SaveChanges: Luôn gọi SaveChangesAsync() sau khi thay đổi
        // 6. Error Handling: Có thể thêm try-catch để xử lý database errors
    }
}
