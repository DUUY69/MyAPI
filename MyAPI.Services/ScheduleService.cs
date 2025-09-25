using Microsoft.EntityFrameworkCore;
using MyAPI.Repositories.Interfaces;
using MyAPI.Repositories.Entities;
using MyAPI.Services.Interfaces;
using MyAPI.Services.Models;

namespace MyAPI.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleService(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<ScheduleResponse?> GetByIdAsync(int id)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(id);
        return schedule == null ? null : MapToResponse(schedule);
    }

    public async Task<IEnumerable<ScheduleResponse>> GetAllAsync()
    {
        var schedules = await _scheduleRepository.GetAllAsync();
        return schedules.Select(MapToResponse);
    }

    public async Task<IEnumerable<ScheduleResponse>> GetByMssvAsync(string mssv)
    {
        var schedules = await _scheduleRepository.GetByMssvAsync(mssv);
        return schedules.Select(MapToResponse);
    }

    public async Task<IEnumerable<ScheduleResponse>> GetByClassNameAsync(string className)
    {
        var schedules = await _scheduleRepository.GetByClassNameAsync(className);
        return schedules.Select(MapToResponse);
    }

    public async Task<IEnumerable<ScheduleResponse>> GetBySubjectNameAsync(string subjectName)
    {
        var schedules = await _scheduleRepository.GetBySubjectNameAsync(subjectName);
        return schedules.Select(MapToResponse);
    }

    public async Task<IEnumerable<ScheduleResponse>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var schedules = await _scheduleRepository.GetByDateRangeAsync(startDate, endDate);
        return schedules.Select(MapToResponse);
    }

    public async Task<ScheduleResponse> AddAsync(AddScheduleRequest request)
    {
        var entity = new Schedule
        {
            Mssv = request.Mssv,
            ClassName = request.ClassName,
            SubjectName = request.SubjectName,
            ScheduleTime = request.ScheduleTime
        };

        var added = await _scheduleRepository.InsertAsync(entity);
        return MapToResponse(added);
    }

    public async Task<ScheduleResponse> UpdateAsync(UpdateScheduleRequest request)
    {
        var entity = await _scheduleRepository.GetByIdAsync(request.Id);
        if (entity == null) 
            throw new ArgumentException($"Schedule with ID {request.Id} not found");

        entity.Mssv = request.Mssv;
        entity.ClassName = request.ClassName;
        entity.SubjectName = request.SubjectName;
        entity.ScheduleTime = request.ScheduleTime;

        var updated = await _scheduleRepository.UpdateAsync(entity);
        return MapToResponse(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _scheduleRepository.GetByIdAsync(id);
        if (entity == null) 
            return false;

        await _scheduleRepository.DeleteAsync(entity);
        return true;
    }

    private static ScheduleResponse MapToResponse(Schedule schedule)
    {
        return new ScheduleResponse
        {
            Id = schedule.Id,
            Mssv = schedule.Mssv,
            StudentName = schedule.MssvNavigation?.Name ?? string.Empty,
            ClassName = schedule.ClassName,
            SubjectName = schedule.SubjectName,
            ScheduleTime = schedule.ScheduleTime
        };
    }
}
