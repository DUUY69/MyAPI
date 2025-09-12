using Microsoft.AspNetCore.Mvc;
using MyAPI.Services.Interfaces;
using MyAPI.Services.Models;

namespace MyAPI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _svc;
        public StudentController(IStudentService svc) { _svc = svc; }

        [HttpGet("GetAllStudent")]
        public async Task<IActionResult> GetAllStudent()
        {
            var students = await _svc.GetAllAsync();
            return Ok(students);
        }


        [HttpGet("GetByMssv")]
        public async Task<IActionResult> GetByMssv(string mssv)
        {
            var s = await _svc.GetByIdAsync(mssv);
            return s == null ? NotFound() : Ok(s);
        }

        [HttpPost("Create")] // Tạo mới
        public async Task<IActionResult> Create([FromBody] AddStudentRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Mssv) || string.IsNullOrWhiteSpace(req.Name))
                return BadRequest("Mssv và Name là bắt buộc.");

            var created = await _svc.AddStudentAsync(req);
            return CreatedAtAction(nameof(GetByMssv), new { mssv = created.Mssv }, created);
        }

        [HttpPut("Update")] // Cập nhật
        public async Task<IActionResult> Update([FromBody] UpdateStudentRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Mssv))
                return BadRequest("Mssv là bắt buộc.");

            var updated = await _svc.UpdateStudentAsync(req);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("Delete")] // Xóa
        public async Task<IActionResult> Delete(string mssv)
        {
            await _svc.DeleteStudentAsync(mssv);
            return NoContent();
        }
        }
}
