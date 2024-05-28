using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace ElysionOrder.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
         private readonly ElysionOrderDbContext _dbcontext;
        private readonly DbSet<T> _dbSet;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        
        public Repository(ElysionOrderDbContext elysionOrderDbContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _dbcontext = elysionOrderDbContext;
            _dbSet = _dbcontext.Set<T>();
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
    

        }

        

        public async Task<bool> AddAsync(T entity)
        {
            var cok = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault().Value.Replace("Id","").Trim();
            entity.CreateTime = DateTime.Now;
            entity.CreateUserId = Guid.Parse(cok);
            entity.Status = true;
            entity.TenatId = Guid.Parse(_configuration.GetSection("TenatId").Value);
            var res =  await _dbSet.AddAsync(entity);
            
            return res.State == EntityState.Added;

        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            var cok = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault().Value.Replace("Id", "").Trim();
            entities.ToList().ForEach(x => x.CreateTime = DateTime.Now );
            entities.ToList().ForEach(x => x.CreateUserId = Guid.Parse(cok));
            entities.ToList().ForEach(x => x.TenatId = Guid.Parse(_configuration.GetSection("TenatId").Value));
           
          
           await _dbSet.AddRangeAsync(entities);
        }

        public bool Delete(T entity)
        {
            EntityEntry<T> entityEntry = _dbSet.Remove(entity);
            return entityEntry.State== EntityState.Deleted;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return Delete(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IQueryable<T> GetAll()
        {
           return _dbSet.AsQueryable();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetFirstAsync()
        {
            return await _dbSet.FirstOrDefaultAsync();
        }

        public async Task<T> GetFirstWhereAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
        {
           return _dbSet.Where(expression);
        }

        public bool Update(T entity)
        {
            
         
            entity.UpdateTime= DateTime.Now;
            var cok = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault().Value.Replace("Id", "").Trim();
            entity.UpdateUserId =Guid.Parse(cok);
          
            EntityEntry<T> entityEntry = _dbSet.Update(entity);
            entityEntry.Property(e => e.CreateTime).IsModified = false;
            entityEntry.Property(e => e.CreateUserId).IsModified = false;
            entityEntry.Property(e => e.TenatId).IsModified = false;
            return entityEntry.State == EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            var cok = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault().Value.Replace("Id", "").Trim();
          

            foreach (var item in entities)
            {
                item.UpdateTime = DateTime.Now;
               
                item.UpdateUserId = Guid.Parse(cok);

                EntityEntry<T> entityEntry = _dbSet.Update(item);
                entityEntry.Property(e => e.CreateTime).IsModified = false;
                entityEntry.Property(e => e.CreateUserId).IsModified = false;
                entityEntry.Property(e => e.TenatId).IsModified = false;

              
            }
            _dbSet.UpdateRange();
        }
    }
}
