using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Providers.DataBase
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(int sn);
        Task<int> Create(TEntity entity);
        Task Update(int sn, TEntity entity);
        Task Delete(int sn);
        IEnumerable<TEntity> GetList(Func<TEntity, bool> expression = null, Func<TEntity, object> orderby = null, bool isdesc = false, int take = 0, int skip = 0);
        Task<int> Count(Func<TEntity, bool> expression = null);
        Task<TEntity> Find(Func<TEntity, bool> expression);
        Task<bool> Exist(int sn);
        Task<bool> Exist(Func<TEntity, bool> expression);
        Task Truncate();
        Task Drop(int sn);
    }
}

