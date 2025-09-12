using Microsoft.EntityFrameworkCore;
using MyAPI.Repositories.Interfaces;
using MyAPI.Repositories.Entities;
using MyAPI.Services.Interfaces;
using MyAPI.Services.Models;

namespace MyAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentResponse> GetByIdAsync(string id)
        {
            var s = await _studentRepository.GetByMssvAsync(id);
            return s == null ? null : new StudentResponse { Mssv = s.Mssv, Name = s.Name };
        }

        public async Task<List<StudentResponse>> GetAllAsync()
        {
            var list = await _studentRepository.GetAll().ToListAsync();
            return list.Select(s => new StudentResponse { Mssv = s.Mssv, Name = s.Name }).ToList();
        }

        public async Task<StudentResponse> AddStudentAsync(AddStudentRequest request)
        {
            var entity = new Student { Mssv = request.Mssv, Name = request.Name };
            var added = await _studentRepository.InsertAsync(entity);
            return new StudentResponse { Mssv = added.Mssv, Name = added.Name };
        }

        public async Task<StudentResponse> UpdateStudentAsync(UpdateStudentRequest request)
        {
            var entity = await _studentRepository.GetByMssvAsync(request.Mssv);
            if (entity == null) return null;
            entity.Name = request.Name;
            var updated = await _studentRepository.UpdateAsync(entity);
            return new StudentResponse { Mssv = updated.Mssv, Name = updated.Name };
        }

        public async Task<bool> DeleteStudentAsync(string id)
        {
            await _studentRepository.DeleteAsync(id);
            return true;
        }
    }
}
