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

        /// <summary>
        /// Lấy danh sách tất cả sinh viên
        /// </summary>
        /// <returns>Danh sách tất cả sinh viên</returns>
        /// <response code="200">Trả về danh sách sinh viên thành công</response>
        [HttpGet("GetAllStudent")]
        public async Task<IActionResult> GetAllStudent()
        {
            var students = await _svc.GetAllAsync();
            return Ok(students);
        }

        /// <summary>
        /// Lấy thông tin sinh viên theo MSSV
        /// </summary>
        /// <param name="mssv">Mã số sinh viên (ví dụ: SE182073)</param>
        /// <returns>Thông tin sinh viên</returns>
        /// <response code="200">Trả về thông tin sinh viên thành công</response>
        /// <response code="404">Không tìm thấy sinh viên với MSSV đã cho</response>
        [HttpGet("GetByMssv")]
        public async Task<IActionResult> GetByMssv(string mssv)
        {
            var s = await _svc.GetByIdAsync(mssv);
            return s == null ? NotFound() : Ok(s);
        }

        /// <summary>
        /// Tạo sinh viên mới
        /// </summary>
        /// <param name="req">Thông tin sinh viên cần tạo</param>
        /// <returns>Thông tin sinh viên đã được tạo</returns>
        /// <response code="201">Tạo sinh viên thành công</response>
        /// <response code="400">Dữ liệu đầu vào không hợp lệ</response>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] AddStudentRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Mssv) || string.IsNullOrWhiteSpace(req.Name))
                return BadRequest("Mssv và Name là bắt buộc.");

            var created = await _svc.AddStudentAsync(req);
            return CreatedAtAction(nameof(GetByMssv), new { mssv = created.Mssv }, created);
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên
        /// </summary>
        /// <param name="req">Thông tin sinh viên cần cập nhật</param>
        /// <returns>Thông tin sinh viên đã được cập nhật</returns>
        /// <response code="200">Cập nhật sinh viên thành công</response>
        /// <response code="400">Dữ liệu đầu vào không hợp lệ</response>
        /// <response code="404">Không tìm thấy sinh viên cần cập nhật</response>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateStudentRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Mssv))
                return BadRequest("Mssv là bắt buộc.");

            var updated = await _svc.UpdateStudentAsync(req);
            return updated == null ? NotFound() : Ok(updated);
        }

        /// <summary>
        /// Xóa sinh viên theo MSSV
        /// </summary>
        /// <param name="mssv">Mã số sinh viên cần xóa</param>
        /// <returns>Không có nội dung trả về</returns>
        /// <response code="204">Xóa sinh viên thành công</response>
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string mssv)
        {
            await _svc.DeleteStudentAsync(mssv);
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
