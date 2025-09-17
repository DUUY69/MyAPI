// ========================================
// GENERIC REPOSITORY PATTERN TEMPLATE
// ========================================
// File này chứa Generic Repository pattern - một cách tiếp cận khác để tạo Repository
// Thay vì tạo từng Repository riêng biệt, có thể kế thừa từ class này
// Ưu điểm: Giảm code duplicate, dễ maintain
// Nhược điểm: Ít linh hoạt hơn, khó customize cho từng entity

using Microsoft.EntityFrameworkCore; // Import Entity Framework Core
using MyAPI.Repositories.Entities; // Import Entity models

namespace MyAPI.Repositories.Infrastructure
{
    // ========================================
    // IEntityBase Interface
    // ========================================
    // Marker interface - không có method nào
    // Dùng để đánh dấu các Entity có thể sử dụng Generic Repository
    // Tất cả Entity classes cần implement interface này nếu muốn dùng Generic Repository
    public interface IEntityBase
    {
        // Interface rỗng - chỉ dùng để đánh dấu
        // Có thể thêm common properties như Id, CreatedDate, etc. nếu cần
    }

    // ========================================
    // IRepository<TEntity> Interface
    // ========================================
    // Generic interface định nghĩa contract cho tất cả Repository
    // TEntity: Generic type parameter - có thể là Student, Product, Category, etc.
    public interface IRepository<TEntity>
    {
        // GET ALL - Lấy tất cả records
        // Trả về IQueryable để có thể thêm Where, OrderBy, etc.
        IQueryable<TEntity> GetAll();
        
        // GET BY ID - Lấy record theo ID
        // abstract method - mỗi Repository con phải implement riêng
        // vì mỗi Entity có thể có primary key khác nhau (int, string, Guid, etc.)
        Task<TEntity> GetById(int id);
        
        // DELETE - Xóa record theo ID
        Task Delete(int id);
        
        // UPDATE - Cập nhật record
        Task<TEntity> Update(TEntity entity);
        
        // INSERT - Thêm record mới
        Task<TEntity> Insert(TEntity entity);
    }

    // ========================================
    // Repository<TEntity> Abstract Class
    // ========================================
    // Abstract class implement IRepository<TEntity>
    // where TEntity : class, IEntityBase - constraint đảm bảo TEntity là class và implement IEntityBase
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntityBase
    {
        // Dependency Injection - inject DbContext để truy cập database
        private readonly DbContext _dbContext;

        // Constructor - nhận DbContext để truy cập database
        public Repository(MyAPIContext dbContext)
        {
            _dbContext = dbContext;
        }

        // ========================================
        // GET ALL - Lấy tất cả records
        // ========================================
        public IQueryable<TEntity> GetAll()
        {
            // Table property trả về DbSet<TEntity> tương ứng
            // AsQueryable() cho phép thực hiện LINQ queries
            return Table.AsQueryable();
        }

        // ========================================
        // GET BY ID - Abstract method
        // ========================================
        // Abstract method - mỗi Repository con PHẢI implement
        // Vì mỗi Entity có thể có primary key khác nhau:
        // - Student: string Mssv
        // - Product: int Id
        // - Category: Guid CategoryId
        public abstract Task<TEntity> GetById(int id);

        // ========================================
        // INSERT - Thêm record mới
        // ========================================
        public async Task<TEntity> Insert(TEntity entity)
        {
            // AddAsync() thêm entity vào DbSet (chưa lưu vào database)
            await Table.AddAsync(entity);

            // SaveChangesAsync() thực sự lưu thay đổi vào database
            await _dbContext.SaveChangesAsync();

            // Trả về entity đã được lưu (có thể có ID được generate)
            return entity;
        }

        // ========================================
        // UPDATE - Cập nhật record
        // ========================================
        public async Task<TEntity> Update(TEntity entity)
        {
            // Update() đánh dấu entity là đã thay đổi
            Table.Update(entity);

            // SaveChangesAsync() lưu thay đổi vào database
            await _dbContext.SaveChangesAsync();

            // Trả về entity đã được cập nhật
            return entity;
        }

        // ========================================
        // DELETE - Xóa record theo ID
        // ========================================
        public async Task Delete(int id)
        {
            // Tìm entity cần xóa bằng GetById()
            var entity = await GetById(id);

            // Nếu tìm thấy thì xóa
            if (entity != null)
            {
                // Remove() đánh dấu entity để xóa
                Table.Remove(entity);

                // SaveChanges() lưu thay đổi vào database
                // Lưu ý: Dùng SaveChanges() thay vì SaveChangesAsync() (có thể là lỗi)
                _dbContext.SaveChanges();
            }
        }

        // ========================================
        // Table Property - Protected
        // ========================================
        // Protected property - chỉ các class con có thể truy cập
        // Trả về DbSet<TEntity> tương ứng với entity type
        protected DbSet<TEntity> Table
        {
            get
            {
                // _dbContext.Set<TEntity>() trả về DbSet cho entity type TEntity
                // Ví dụ: _dbContext.Set<Student>() trả về _dbContext.Students
                return _dbContext.Set<TEntity>();
            }
        }
        
        // ========================================
        // CÁCH SỬ DỤNG GENERIC REPOSITORY:
        // ========================================
        // 1. Entity class phải implement IEntityBase
        // 2. Tạo Repository class kế thừa từ Repository<TEntity>
        // 3. Implement abstract method GetById()
        // 4. Đăng ký trong DI container
        
        // Ví dụ:
        // public class Student : IEntityBase { ... }
        // public class StudentRepository : Repository<Student>
        // {
        //     public StudentRepository(MyAPIContext context) : base(context) { }
        //     
        //     public override async Task<Student> GetById(int id)
        //     {
        //         // Implement logic tìm Student theo ID
        //         return await Table.FirstOrDefaultAsync(x => x.Id == id);
        //     }
        // }
        
        // ========================================
        // SO SÁNH VỚI CÁCH HIỆN TẠI:
        // ========================================
        // Cách hiện tại (StudentRepository riêng biệt):
        // - Linh hoạt hơn, dễ customize
        // - Code rõ ràng, dễ hiểu
        // - Phù hợp với dự án nhỏ-vừa
        
        // Generic Repository:
        // - Ít code duplicate
        // - Dễ maintain khi có nhiều entity
        // - Phù hợp với dự án lớn có nhiều entity tương tự
    }
}
