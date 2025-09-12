using Microsoft.EntityFrameworkCore;
using MyAPI.Repositories.Entities;
using MyAPI.Repositories.Interfaces;

namespace MyAPI.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly MyAPIContext _ctx;

        public StudentRepository(MyAPIContext dbContext)
        {
            _ctx = dbContext;
        }

        public IQueryable<Student> GetAll()
        {
            return _ctx.Students.AsQueryable();
        }

        public async Task<Student> InsertAsync(Student student)
        {
            await _ctx.Students.AddAsync(student);
            await _ctx.SaveChangesAsync();
            return student;
        }

        public async Task<Student> UpdateAsync(Student student)
        {
            _ctx.Students.Update(student);
            await _ctx.SaveChangesAsync();
            return student;
        }

        public async Task DeleteAsync(string mssv)
        {
            var student = await GetByMssvAsync(mssv);
            if (student != null)
            {
                _ctx.Students.Remove(student);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<Student> GetByMssvAsync(string mssv)
        {
            return await _ctx.Students.FirstOrDefaultAsync(x => x.Mssv == mssv);
        }
        
    }
}
