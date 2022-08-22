using ExamApp.Data.Contexts;
using ExamApp.Data.IRepositories;
using ExamApp.Domain.Entities.Students;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExamApp.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        private readonly ExamAppDbContext dbContext;
        protected readonly DbSet<Student> dbSet;

        public StudentRepository()
        {
            dbContext = new ExamAppDbContext();
            dbSet = dbContext.Set<Student>();
        }

        public async Task<Student> AddAsync(Student entity)
            => (await dbSet.AddAsync(entity)).Entity;

        public async Task<bool> DeleteAsync(Expression<Func<Student, bool>> predicate)
        {
            var entity = await GetAsync(predicate);

            if (entity == null)
                return false;

            dbSet.Remove(entity);
            return true;
        }

        public IQueryable<Student> GetAll(Expression<Func<Student, bool>> predicate = null)
            => predicate is null ? dbSet : dbSet.Where(predicate);

        public Task<Student> GetAsync(Expression<Func<Student, bool>> predicate)
            => dbSet.FirstOrDefaultAsync(predicate);

        public Student Update(Student entity)
            =>dbSet.Update(entity).Entity;

        public Task SaveAsync() => dbContext.SaveChangesAsync();
    }
}
