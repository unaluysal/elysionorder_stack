using ElysionOrder.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ElysionOrder.Infrastructure.Data.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        
        Task<bool> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        bool Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

        bool Delete(T entity);
        Task< bool> DeleteByIdAsync(Guid id);
        void DeleteRange(IEnumerable<T> entities);

        IQueryable<T> GetAll();
        IQueryable<T> GetWhere(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetFirstWhereAsync(Expression<Func<T, bool>> expression);
        Task<T> GetFirstAsync();
    }
}
