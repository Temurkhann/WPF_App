using ExamApp.Data.Contexts;
using ExamApp.Data.IRepositories;
using ExamApp.Domain.Entities.Attachments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExamApp.Data.Repositories
{
    public class AttachmentRepository : IAttachmentRepositry
    {

        protected readonly ExamAppDbContext dbContext;
        protected readonly DbSet<Attachment> dbSet;

        public AttachmentRepository()
        {
            dbContext = new ExamAppDbContext();
            dbSet = dbContext.Set<Attachment>();
        }

        public async Task<Attachment> AddAsync(Attachment entity)
            => (await dbSet.AddAsync(entity)).Entity;

        public async Task<bool> DeleteAsync(Expression<Func<Attachment, bool>> expression)
        {
            var entity = await GetAsync(expression);

            if (entity == null)
                return false;

            dbSet.Remove(entity);
            return true;
        }

        public async Task<IQueryable<Attachment>> GetAllAsync(Expression<Func<Attachment, bool>> predicate = null)
            => predicate is null ? dbSet : dbSet.Where(predicate);

        public Task<Attachment> GetAsync(Expression<Func<Attachment, bool>> predicate)
            => dbSet.FirstOrDefaultAsync(predicate);

        public async Task<Attachment> UpdateAsync(Attachment entity)
            => dbSet.Update(entity).Entity;

        public Task SaveAsync() => dbContext.SaveChangesAsync();
    }
}
