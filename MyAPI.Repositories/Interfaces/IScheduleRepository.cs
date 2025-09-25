using MyAPI.Repositories.Entities;

namespace MyAPI.Repositories.Interfaces;

public interface IScheduleRepository
{
    Task<Schedule?> GetByIdAsync(int id);
    Task<IEnumerable<Schedule>> GetAllAsync();
    Task<IEnumerable<Schedule>> GetByMssvAsync(string mssv);
    Task<IEnumerable<Schedule>> GetByClassNameAsync(string className);
    Task<IEnumerable<Schedule>> GetBySubjectNameAsync(string subjectName);
    Task<IEnumerable<Schedule>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<Schedule> InsertAsync(Schedule schedule);
    Task<Schedule> UpdateAsync(Schedule schedule);
    Task DeleteAsync(Schedule schedule);
}
