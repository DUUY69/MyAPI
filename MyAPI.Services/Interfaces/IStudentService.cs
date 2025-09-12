using MyAPI.Services.Models;
namespace MyAPI.Services.Interfaces
{
    public interface IStudentService
    {
        Task<StudentResponse> GetByIdAsync(string id);
        Task<List<StudentResponse>> GetAllAsync();
        Task<StudentResponse> AddStudentAsync(AddStudentRequest request);
        Task<StudentResponse> UpdateStudentAsync(UpdateStudentRequest request);
        Task<bool> DeleteStudentAsync(string id);
    }
}