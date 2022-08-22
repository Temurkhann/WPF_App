using ExamApp.Domain.Entities.Students;
using ExamApp.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExamApp.Service.Interfaces
{
    public interface IStudentService : IDisposable
    {
        Task<Student> CreateAsync(StudentForCreationDTO entity);
        Task<Student> UpdateAsync(long id, StudentForCreationDTO entity);
        Task<Student> GetAsync(long id);
        Task<IEnumerable<Student>> GetAllAsync(Expression<Func<Student, bool>> expression = null, Tuple<int, int> pagination = null);
        Task<bool> DeleteAsync(long id);
        Task UploadPicturesAsync(long id, string imagePath, string passportPath);
    }
}
