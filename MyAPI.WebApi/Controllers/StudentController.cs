// ========================================
// TEMPLATE CHO CONTROLLER MỚI
// ========================================
// Khi tạo API mới, copy file này và đổi tên class + service
// Ví dụ: ProductController, IProductService, ProductService

using Microsoft.AspNetCore.Mvc; // Import để sử dụng ControllerBase, IActionResult, HttpGet, HttpPost, etc.
using MyAPI.Services.Interfaces; // Import interface của Service layer
using MyAPI.Services.Models; // Import các DTO models (Request/Response)

namespace MyAPI.WebApi.Controllers
{
    // [ApiController] - Attribute báo hiệu đây là API Controller
    // [Route("api/[controller]")] - Định nghĩa route base: api/Student
    // [controller] sẽ tự động lấy tên class (Student) làm route
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase // Kế thừa ControllerBase để có các method HTTP
    {
        // Dependency Injection - inject Service interface vào Controller
        // readonly: chỉ có thể gán giá trị trong constructor
        private readonly IStudentService _svc;
        
        // Constructor - DI container sẽ tự động inject IStudentService implementation
        public StudentController(IStudentService svc) { _svc = svc; }

        // ========================================
        // GET ALL - Lấy tất cả records
        // ========================================
        // [HttpGet("GetAllStudent")] - Route: GET /api/Student/GetAllStudent
        [HttpGet("GetAllStudent")]
        public async Task<IActionResult> GetAllStudent() // async Task cho phép xử lý bất đồng bộ
        {
            // Gọi Service layer để lấy dữ liệu
            var students = await _svc.GetAllAsync();
            // Trả về HTTP 200 OK với dữ liệu
            return Ok(students);
        }

        // ========================================
        // GET BY ID - Lấy record theo ID
        // ========================================
        // [HttpGet("GetByMssv")] - Route: GET /api/Student/GetByMssv?mssv=SE182073
        [HttpGet("GetByMssv")]
        public async Task<IActionResult> GetByMssv(string mssv) // Parameter từ query string
        {
            // Gọi Service để lấy student theo MSSV
            var s = await _svc.GetByIdAsync(mssv);
            // Nếu không tìm thấy trả về 404, nếu có trả về 200 với dữ liệu
            return s == null ? NotFound() : Ok(s);
        }

        // ========================================
        // CREATE - Tạo record mới
        // ========================================
        // [HttpPost("Create")] - Route: POST /api/Student/Create
        [HttpPost("Create")] // Tạo mới
        public async Task<IActionResult> Create([FromBody] AddStudentRequest req) // [FromBody] lấy data từ request body
        {
            // Validation - kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(req.Mssv) || string.IsNullOrWhiteSpace(req.Name))
                return BadRequest("Mssv và Name là bắt buộc."); // Trả về HTTP 400 Bad Request

            // Gọi Service để tạo student mới
            var created = await _svc.AddStudentAsync(req);
            // Trả về HTTP 201 Created với location header
            return CreatedAtAction(nameof(GetByMssv), new { mssv = created.Mssv }, created);
        }

        // ========================================
        // UPDATE - Cập nhật record
        // ========================================
        // [HttpPut("Update")] - Route: PUT /api/Student/Update
        [HttpPut("Update")] // Cập nhật
        public async Task<IActionResult> Update([FromBody] UpdateStudentRequest req)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(req.Mssv))
                return BadRequest("Mssv là bắt buộc.");

            // Gọi Service để cập nhật
            var updated = await _svc.UpdateStudentAsync(req);
            // Nếu không tìm thấy trả về 404, nếu thành công trả về 200
            return updated == null ? NotFound() : Ok(updated);
        }

        // ========================================
        // DELETE - Xóa record
        // ========================================
        // [HttpDelete("Delete")] - Route: DELETE /api/Student/Delete?mssv=SE182073
        [HttpDelete("Delete")] // Xóa
        public async Task<IActionResult> Delete(string mssv)
        {
            // Gọi Service để xóa
            await _svc.DeleteStudentAsync(mssv);
            // Trả về HTTP 204 No Content (thành công nhưng không có dữ liệu trả về)
            return NoContent();
        }
        
        // ========================================
        // CÁC HTTP STATUS CODES THƯỜNG DÙNG:
        // ========================================
        // Ok() - 200: Thành công
        // Created() - 201: Tạo mới thành công
        // NoContent() - 204: Thành công, không có dữ liệu trả về
        // BadRequest() - 400: Dữ liệu đầu vào không hợp lệ
        // NotFound() - 404: Không tìm thấy
        // Unauthorized() - 401: Chưa đăng nhập
        // Forbidden() - 403: Không có quyền
        // InternalServerError() - 500: Lỗi server
        }
}
