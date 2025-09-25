using Microsoft.EntityFrameworkCore;
using MyAPI.Repositories.Entities;
using MyAPI.Repositories.Interfaces;

namespace MyAPI.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly MyAPIContext _context;

    public ScheduleRepository(MyAPIContext context)
    {
        _context = context;
    }

    public async Task<Schedule?> GetByIdAsync(int id)
    {
        return await _context.Schedules
            .Include(s => s.MssvNavigation)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Schedule>> GetByMssvAsync(string mssv)
    {
        return await _context.Schedules
            .Include(s => s.MssvNavigation)
            .Where(s => s.Mssv == mssv)
            .OrderBy(s => s.ScheduleTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Schedule>> GetByClassNameAsync(string className)
    {
        return await _context.Schedules
            .Include(s => s.MssvNavigation)
            .Where(s => s.ClassName.Contains(className))
            .OrderBy(s => s.ScheduleTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Schedule>> GetBySubjectNameAsync(string subjectName)
    {
        return await _context.Schedules
            .Include(s => s.MssvNavigation)
            .Where(s => s.SubjectName.Contains(subjectName))
            .OrderBy(s => s.ScheduleTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Schedule>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Schedules
            .Include(s => s.MssvNavigation)
            .Where(s => s.ScheduleTime >= startDate && s.ScheduleTime <= endDate)
            .OrderBy(s => s.ScheduleTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Schedule>> GetAllAsync()
    {
        return await _context.Schedules
            .Include(s => s.MssvNavigation)
            .OrderBy(s => s.ScheduleTime)
            .ToListAsync();
    }

    public async Task<Schedule> InsertAsync(Schedule schedule)
    {
        await _context.Schedules.AddAsync(schedule);
        await _context.SaveChangesAsync();
        return schedule;
    }

    public async Task<Schedule> UpdateAsync(Schedule schedule)
    {
        _context.Schedules.Update(schedule);
        await _context.SaveChangesAsync();
        return schedule;
    }

    public async Task DeleteAsync(Schedule schedule)
    {
        _context.Schedules.Remove(schedule);
        await _context.SaveChangesAsync();
    }
}
