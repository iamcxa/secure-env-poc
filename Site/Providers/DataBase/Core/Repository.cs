using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Providers.DataBase
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly ApplicationDbContext context;
        public Repository(ApplicationDbContext context) => this.context = context;
        public IQueryable<TEntity> GetAll() => context.Set<TEntity>().AsNoTracking();
        public async Task<TEntity> GetById(int sn) => await context.Set<TEntity>().FirstOrDefaultAsync(o => o.SN == sn && !o.IsDeleted);
        public async Task<int> Create(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ModifyDate = DateTime.Now;
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.SN;
        }
        public async Task Update(int sn, TEntity entity)
        {
            entity.ModifyDate = DateTime.Now;
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task Delete(int sn)
        {
            var entity = await GetById(sn);
            entity.IsDeleted = true;
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task Drop(int sn)
        {
            var entity = await GetById(sn);
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }
        public IEnumerable<TEntity> GetList(Func<TEntity, bool> expression = null, Func<TEntity, object> orderby = null, bool isdesc = false, int take = 0, int skip = 0)
        {
            IEnumerable<TEntity> query = context.Set<TEntity>().Where(o => !o.IsDeleted);
            if (expression != null) query = query.Where(expression);
            if (orderby != null)
                if (!isdesc) query = query.OrderBy(orderby);
                else query = query.OrderByDescending(orderby);
            if (skip != 0)
                query = query.Skip(skip);
            if (take != 0)
                query = query.Take(take);
            return query;
        }
        public async Task<int> Count(Func<TEntity, bool> expression = null)
        {
            if (expression == null)
                return await context.Set<TEntity>().CountAsync<TEntity>(o => !o.IsDeleted);
            else
                return await context.Set<TEntity>().CountAsync<TEntity>(o => !o.IsDeleted && expression.Invoke(o));
        }
        public async Task Truncate()
        {
            await context.Database.ExecuteSqlCommandAsync("DELETE FROM " + typeof(TEntity).Name);
            await context.Database.ExecuteSqlCommandAsync("VACUUM");
        }
        public async Task<TEntity> Find(Func<TEntity, bool> expression)
            => await context.Set<TEntity>().FirstOrDefaultAsync(o => !o.IsDeleted && expression.Invoke(o));
        public async Task<bool> Exist(int sn)
        => await context.Set<TEntity>().AnyAsync(o => o.SN == sn && !o.IsDeleted);
        public async Task<bool> Exist(Func<TEntity, bool> expression)
             => await context.Set<TEntity>().AnyAsync(o => !o.IsDeleted && expression.Invoke(o));
    }

}
