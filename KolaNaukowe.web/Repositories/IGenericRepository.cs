using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KolaNaukowe.web.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties);
        TEntity Get(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties);
        TEntity Add(TEntity item);
        void Update(TEntity item);
        void Remove(int id);
    }
}
