using MyAPI.Services.Models;

namespace MyAPI.Services.Interfaces;

public interface IScheduleService
{
    Task<ScheduleResponse?> GetByIdAsync(int id);
    Task<IEnumerable<ScheduleResponse>> GetAllAsync();
    Task<IEnumerable<ScheduleResponse>> GetByMssvAsync(string mssv);
    Task<IEnumerable<ScheduleResponse>> GetByClassNameAsync(string className);
    Task<IEnumerable<ScheduleResponse>> GetBySubjectNameAsync(string subjectName);
    Task<IEnumerable<ScheduleResponse>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<ScheduleResponse> AddAsync(AddScheduleRequest request);
    Task<ScheduleResponse> UpdateAsync(UpdateScheduleRequest request);
    Task<bool> DeleteAsync(int id);
}
