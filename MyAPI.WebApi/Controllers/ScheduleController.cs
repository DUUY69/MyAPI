using Microsoft.AspNetCore.Mvc;
using MyAPI.Services.Interfaces;
using MyAPI.Services.Models;

namespace MyAPI.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    /// <summary>
    /// Lấy tất cả lịch học trong hệ thống
    /// </summary>
    /// <returns>Danh sách tất cả lịch học</returns>
    /// <response code="200">Trả về danh sách lịch học thành công</response>
    [HttpGet("GetAllSchedule")]
    public async Task<IActionResult> GetAllSchedule()
    {
        var schedules = await _scheduleService.GetAllAsync();
        return Ok(schedules);
    }

    /// <summary>
    /// Lấy thông tin lịch học theo ID
    /// </summary>
    /// <param name="id">ID của lịch học cần lấy</param>
    /// <returns>Thông tin lịch học</returns>
    /// <response code="200">Trả về thông tin lịch học thành công</response>
    /// <response code="404">Không tìm thấy lịch học với ID đã cho</response>
    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(int id)
    {
        var schedule = await _scheduleService.GetByIdAsync(id);
        return schedule == null ? NotFound() : Ok(schedule);
    }

    /// <summary>
    /// Tạo lịch học mới
    /// </summary>
    /// <param name="request">Thông tin lịch học cần tạo</param>
    /// <returns>Thông tin lịch học đã được tạo</returns>
    /// <response code="201">Tạo lịch học thành công</response>
    /// <response code="400">Dữ liệu đầu vào không hợp lệ</response>
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] AddScheduleRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Mssv) || string.IsNullOrWhiteSpace(request.ClassName) || 
            string.IsNullOrWhiteSpace(request.SubjectName))
            return BadRequest("Mssv, ClassName và SubjectName là bắt buộc.");

        try
        {
            var created = await _scheduleService.AddAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Cập nhật thông tin lịch học
    /// </summary>
    /// <param name="request">Thông tin lịch học cần cập nhật</param>
    /// <returns>Thông tin lịch học đã được cập nhật</returns>
    /// <response code="200">Cập nhật lịch học thành công</response>
    /// <response code="400">Dữ liệu đầu vào không hợp lệ</response>
    /// <response code="404">Không tìm thấy lịch học cần cập nhật</response>
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateScheduleRequest request)
    {
        if (request.Id <= 0)
            return BadRequest("ID là bắt buộc.");

        try
        {
            var updated = await _scheduleService.UpdateAsync(request);
            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Xóa lịch học theo ID
    /// </summary>
    /// <param name="id">ID của lịch học cần xóa</param>
    /// <returns>Không có nội dung trả về</returns>
    /// <response code="204">Xóa lịch học thành công</response>
    /// <response code="404">Không tìm thấy lịch học cần xóa</response>
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _scheduleService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
