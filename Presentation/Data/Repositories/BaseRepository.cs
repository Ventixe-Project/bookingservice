using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Presentation.Data.Contexts;
using Presentation.Models;

namespace Presentation.Data.Repositories
{
    public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly DataContext _context = context;
        protected readonly DbSet<TEntity> _table = context.Set<TEntity>();

        // Create
        public virtual async Task<RepositoryResult> AddAsync(TEntity entity)
        {
            try
            {
                _table.Add(entity);
                await _context.SaveChangesAsync();

                return new RepositoryResult { Success = true };
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Success = false, Error = ex.Message };
            }
        }

        // Read
        public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync()
        {
            try
            {
                var entities = await _table.ToListAsync();
                return new RepositoryResult<IEnumerable<TEntity>>
                {
                    Success = true,
                    Result = entities,
                };
            }
            catch (Exception ex)
            {
                return new RepositoryResult<IEnumerable<TEntity>>
                {
                    Success = false,
                    Error = ex.Message,
                };
            }
        }

        public virtual async Task<RepositoryResult<TEntity?>> GetAsync(
            Expression<Func<TEntity, bool>> expression
        )
        {
            try
            {
                var entity =
                    await _table.FirstOrDefaultAsync(expression)
                    ?? throw new Exception("Not found");
                return new RepositoryResult<TEntity?> { Success = true, Result = entity };
            }
            catch (Exception ex)
            {
                return new RepositoryResult<TEntity?> { Success = false, Error = ex.Message };
            }
        }

        public virtual async Task<RepositoryResult> AlreadyExistsAsync(
            Expression<Func<TEntity, bool>> expression
        )
        {
            var result = await _table.AnyAsync(expression);
            return result
                ? new RepositoryResult { Success = true }
                : new RepositoryResult { Success = false, Error = "Not found" };
        }

        // Update
        public virtual async Task<RepositoryResult> UpdateAsync(TEntity entity)
        {
            try
            {
                _table.Update(entity);
                await _context.SaveChangesAsync();

                return new RepositoryResult { Success = true };
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Success = false, Error = ex.Message };
            }
        }

        // Delete
        public virtual async Task<RepositoryResult> DeleteAsync(TEntity entity)
        {
            try
            {
                _table.Remove(entity);
                await _context.SaveChangesAsync();

                return new RepositoryResult { Success = true };
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Success = false, Error = ex.Message };
            }
        }
    }
}
