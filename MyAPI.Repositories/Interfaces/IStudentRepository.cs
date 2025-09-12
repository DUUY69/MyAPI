using System.Linq;
using System.Threading.Tasks;
using MyAPI.Repositories.Entities;
namespace MyAPI.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        IQueryable<Student> GetAll();
        Task<Student> GetByMssvAsync(string mssv);
        Task<Student> InsertAsync(Student student);
        Task<Student> UpdateAsync(Student student);
        Task DeleteAsync(string mssv);
    }
}