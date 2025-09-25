using System.ComponentModel.DataAnnotations;

namespace MyAPI.Services.Models;

public class AddScheduleRequest
{
    [Required(ErrorMessage = "MSSV is required")]
    [StringLength(20, ErrorMessage = "MSSV cannot exceed 20 characters")]
    public string Mssv { get; set; } = string.Empty;

    [Required(ErrorMessage = "Class name is required")]
    [StringLength(100, ErrorMessage = "Class name cannot exceed 100 characters")]
    public string ClassName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Subject name is required")]
    [StringLength(100, ErrorMessage = "Subject name cannot exceed 100 characters")]
    public string SubjectName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Schedule time is required")]
    public DateTime ScheduleTime { get; set; }
}
