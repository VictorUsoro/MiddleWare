using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MW.Application
{
   public interface IRepository<T>
   {
      IQueryable<T> Get(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, 
         bool includeDeleted = false, params Expression<Func<T, object>>[] properties);
      IQueryable<T> Get(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
         bool includeDeleted = false);
      bool Exists(Expression<Func<T, bool>> filter = null, bool includeDeleted = false);
      T GetBy(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] properties);
      T GetById(Guid id);
      T Update(T entity);
      void UpdateRange(List<T> entities);
      T Insert(T entity);
      void InsertRange(List<T> entities);
      void Remove(Guid id);
      T Delete(Guid id);
   }
}
