namespace MyAPI.Services.Models;

public class ScheduleResponse
{
    public int Id { get; set; }
    public string Mssv { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public DateTime ScheduleTime { get; set; }
}
