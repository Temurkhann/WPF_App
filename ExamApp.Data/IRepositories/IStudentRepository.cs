using ExamApp.Domain.Entities.Students;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExamApp.Data.IRepositories
{
    public interface IStudentRepository
    {
        IQueryable<Student> GetAll(Expression<Func<Student, bool>> predicate = null);

        Task<Student> GetAsync(Expression<Func<Student, bool>> predicate);

        Task<Student> AddAsync(Student entity);

        Student Update(Student entity);

        Task<bool> DeleteAsync(Expression<Func<Student, bool>> predicate);

        Task SaveAsync();
    }
}
