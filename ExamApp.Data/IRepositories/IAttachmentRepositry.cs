using ExamApp.Domain.Entities.Attachments;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExamApp.Data.IRepositories
{
    public interface IAttachmentRepositry
    {
        Task<IQueryable<Attachment>> GetAllAsync(Expression<Func<Attachment, bool>> predicate = null);

        Task<Attachment> GetAsync(Expression<Func<Attachment, bool>> predicate);

        Task<Attachment> AddAsync(Attachment entity);

        Task<Attachment> UpdateAsync(Attachment entity);

        Task<bool> DeleteAsync(Expression<Func<Attachment, bool>> predicate);

        Task SaveAsync();
    }
}
