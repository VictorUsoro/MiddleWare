using Microsoft.EntityFrameworkCore;
using MW.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MW.Services
{
   public class Repository<T> : IDisposable, IRepository<T> where T : class, IBaseEntity, new()
   {

      private readonly MiddleWareDBContext _context;
      private readonly DbSet<T> _entities;
      private bool _disposed;

      public Repository(MiddleWareDBContext context)
      {
         _context = context;
         _entities = context.Set<T>();
         _disposed = false;
      }

      public IQueryable<T> Get(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>>
         orderBy, bool includeDeleted = false, params Expression<Func<T, object>>[] properties)
      {
         IQueryable<T> query;

         query = properties.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(_entities, (current, expr) => current.Include(expr));

         query = query.Where(x => !x.IsDeleted || includeDeleted);

         if (filter != null)
         {
            query = query.Where(filter);
         }

         if (orderBy != null)
         {
            return orderBy(query);
         }
         else
         {
            return query;
         }
      }

      public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter, Func<IQueryable<T>,
         IOrderedQueryable<T>> orderBy, bool includeDeleted = false)
      {
         IQueryable<T> query = _entities.AsQueryable();
         query = query.Where(x => x.IsDeleted == false || includeDeleted);

         if (filter != null)
         {
            query = query.Where(filter);
         }
         if (orderBy != null)
         {
            return orderBy(query);
         }
         return query;
      }

      public virtual bool Exists(Expression<Func<T, bool>> filter = null, bool includeDeleted = false)
      {
         bool any = _entities.Where(filter).Any();
         return any;
      }

      public virtual T GetBy(Expression<Func<T, bool>> filter = null, params Expression<Func<T,
         object>>[] properties)
      {
         IQueryable<T> query;

         query = properties.Aggregate<Expression<Func<T, object>>,
            IQueryable<T>>(_entities, (current, expr) => current.Include(expr));

         if (filter != null)
         {
            query = query.Where(filter);
         }

         return query.FirstOrDefault();

      }

      public T GetById(Guid id)
      {
         T result = _entities.Find(id);
         return result;
      }

      public T Update(T entity)
      {
         if (entity == null)
         {
            throw new ArgumentNullException("entity");
         }
         entity.ModifiedDate = DateTime.Now;
         var result = _entities.Update(entity);
         _context.SaveChanges();
         return result.Entity;
      }

      public void UpdateRange(List<T> entities)
      {
         if (entities == null)
         {
            throw new ArgumentNullException("entities");
         }
         _entities.UpdateRange(entities);
         _context.SaveChanges();
      }

      public T Insert(T entity)
      {
         entity.CreatedDate = DateTime.Now;
         entity.ModifiedDate = DateTime.Now;
         entity.RecordStatus = RecordStatus.Active;
         var result = _entities.Add(entity);
         _context.SaveChanges();
         return result.Entity;
      }

      public void InsertRange(List<T> entities)
      {
         _entities.AddRange(entities);
         _context.SaveChanges();
      }

      public void Remove(Guid id)
      {
         _entities.Remove(GetById(id));
         _context.SaveChanges();
      }

      public T Delete(Guid id)
      {
         T result = null;
         var t = GetById(id);
         if (t != null)
         {
            t.IsDeleted = true;
            t.ModifiedDate = DateTime.Now;
            result = Update(t);
         }
         return result;
      }

      public T Deactivate(Guid id)
      {
         T result = null;
         var t = GetById(id);
         if (t != null)
         {
            t.RecordStatus = RecordStatus.Inactive;
            t.ModifiedDate = DateTime.Now;
            result = Update(t);
         }
         return result;
      }

      public T Activate(Guid id)
      {
         T result = null;
         var entity = GetById(id);
         if (entity != null)
         {
            entity.RecordStatus = RecordStatus.Active;
            entity.ModifiedDate = DateTime.Now;
            result = Update(entity);
         }
         return result;
      }

      #region IDisposable

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      protected virtual void Dispose(bool disposing)
      {
         if (!_disposed && disposing)
         {
            _context.Dispose();
         }
         _disposed = true;
      }

      #endregion
   }
}