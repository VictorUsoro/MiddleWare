using MW.Application;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MW.Services
{
   public class BaseServices<TEntity, TDataObject> where TEntity : class, IBaseEntity, new()
   {
      private readonly MiddleWareDBContext _context;
      public BaseServices(MiddleWareDBContext context)
      {
         _context = context;
      }

      public MappingHandlers Factories=> new MappingHandlers();

      public ServiceFactory SFactory => new ServiceFactory();

      public class ServiceFactory : BaseMapper<TEntity, TDataObject> { }

      public UnitOfWork UnitOfWork => new UnitOfWork(_context);

      public ServiceInit CoreServices => new ServiceInit(_context);

      #region Entity Crud

      private Repository<TEntity> Repository
      {
         get { return new Repository<TEntity>(_context); }
      }

      public IQueryable<TEntity> GetManyEntity()
      {
         return Repository.Get(null, null, false);
      }

      public TEntity GetEntitySingle(Guid id)
      {
         TEntity _entity = Repository.GetById(id);
         return _entity;
      }

      public Task<bool> CreateManyAsync(List<TEntity> entities)
      {
         return Task.Run(() =>
         {
            try
            {
               Repository.InsertRange(entities);
               return true;
            }
            catch (Exception)
            {
               return false;
            }
         });
      }

      public Task<TEntity> CreateAsync(TEntity entity)
      {
         return Task.Run(() =>
         {
            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            TEntity result = Repository.Insert(entity: entity);
            return result;
         });
      }

      public TEntity Create(TEntity entity)
      {
         entity.Id = Guid.NewGuid();
         entity.CreatedDate = DateTime.Now;
         entity.ModifiedDate = DateTime.Now;
         TEntity result = Repository.Insert(entity: entity);
         return result;
      }

      public TEntity Update(TEntity entity)
      {
         TEntity result = Repository.Update(entity: entity);
         return result;
      }

      public Task<TEntity> DeleteAsync(Guid id)
      {
         return Task.Run(() =>
         {
            try
            {
               if (id != Guid.Empty)
               {
                  var _entity = Repository.GetById(id);
                  if (_entity != null)
                  {
                     TEntity result = Repository.Delete(id);
                     return result;
                  }
               }

               throw new InvalidOperationException("Internal Server Error!");
            }
            catch (Exception)
            {
               throw;
            }
         });
      }
        
      public Task<TEntity> DeactivateAsync(Guid id)
      {
         return Task.Run(() =>
         {
            try
            {
               if (id != Guid.Empty)
               {
                  var _entity = Repository.GetById(id);
                  if (_entity != null)
                  {
                     TEntity result = Repository.Deactivate(id);
                     return result;
                  }
               }

               throw new InvalidOperationException("Invalid Entity Id");
            }
            catch (Exception)
            {
               throw;
            }
         });
      }
      
      public Task<TEntity> ActivateAsync(Guid id)
      {
         return Task.Run(() =>
         {
            try
            {
               if (id != Guid.Empty)
               {
                  var _entity = Repository.GetById(id);
                  if (_entity != null)
                  {
                     TEntity result = Repository.Activate(id);
                     return result;
                  }
               }
               throw new InvalidOperationException("Internal Server Error!");
            }
            catch (Exception)
            {
               throw;
            }
         });
      }
            
      public PaginationExtension<TEntity> SearchEntityPaginate(int pageSize, int page)
      {

         IEnumerable<TEntity> entities = Repository.Get(null, x => x.OrderByDescending(r => r.CreatedDate), false);

         int Count = entities.Count();

         entities = entities.Skip((page - 1) * pageSize).Take(pageSize);
         int totalPages = (int)Math.Ceiling(Count / (double)pageSize);

         int itemEnd = pageSize * page;
         int itemStart = (itemEnd - pageSize) + 1;
         if (itemEnd > Count)
         {
            itemEnd = Count;
         }

         var msg = string.Format("Showing {0} to {1} of {2} Records | Total Pages: {3}",
                itemStart, itemEnd, Count, totalPages);

         PaginationExtension<TEntity> extension = new PaginationExtension<TEntity>();

         extension.Summary = Count > 0 ? msg : string.Empty;
         extension.TotalCount = Count;
         extension.ReturnedList = entities;
         extension.StaticPagedList = new StaticPagedList<TEntity>(entities, page, pageSize, Count);
         return extension;
      }

      public PaginationExtension<TEntity> SearchEntityPaginate(IQueryable<TEntity> listData, int pageSize, int page)
      {
         int Count = listData.Count();

         listData = listData.Skip((page - 1) * pageSize).Take(pageSize);
         int totalPages = (int)Math.Ceiling(Count / (double)pageSize);

         int itemEnd = pageSize * page;
         int itemStart = (itemEnd - pageSize) + 1;
         if (itemEnd > Count)
         {
            itemEnd = Count;
         }

         var msg = string.Format("Showing {0} to {1} of {2} Records | Total Pages: {3}",
                itemStart, itemEnd, Count, totalPages);

         PaginationExtension<TEntity> extension = new PaginationExtension<TEntity>();

         extension.Summary = Count > 0 ? msg : string.Empty;
         extension.TotalCount = Count;
         extension.ReturnedList = listData;
         extension.StaticPagedList = new StaticPagedList<TEntity>(listData, page, pageSize, Count);

         return extension;
      }

      public PaginationExtension<object> SearchEntityPaginate(IQueryable<object> listData, int pageSize, int page)
      {
         int Count = listData.Count();

         listData = listData.Skip((page - 1) * pageSize).Take(pageSize);
         int totalPages = (int)Math.Ceiling(Count / (double)pageSize);

         int itemEnd = pageSize * page;
         int itemStart = (itemEnd - pageSize) + 1;
         if (itemEnd > Count)
         {
            itemEnd = Count;
         }

         var msg = string.Format("Showing {0} to {1} of {2} Records | Total Pages: {3}",
                itemStart, itemEnd, Count, totalPages);

         PaginationExtension<object> extension = new PaginationExtension<object>();

         extension.Summary = Count > 0 ? msg : string.Empty;
         extension.TotalCount = Count;
         extension.ReturnedList = listData;
         extension.StaticPagedList = new StaticPagedList<object>(listData, page, pageSize, Count);

         return extension;
      }

      #endregion

      #region Data Model Crud

      public TDataObject GetSingle(Guid id)
      {
         TEntity _entity = Repository.GetById(id);
         var _obj = SFactory.SingleMappingEntityToObject(_entity);
         return _obj;
      }

      public IQueryable<TDataObject> GetMany()
      {
         var entities = Repository.Get(null, null, false);
         var _objs = SFactory.ListMappingEntitiesToObjects(entities);
         return _objs;
      }

      public async Task CreateManyAsync(IQueryable<TDataObject> obj)
      {
         IQueryable<TEntity> entities = SFactory.ListMappingObjectToEntitiess(obj);
         await _context.AddRangeAsync(entities);
         _context.SaveChanges();
      }

      public Task<TDataObject> CreateAsync(TDataObject obj)
      {
         return Task.Run(() =>
         {
            var entity = SFactory.SingleMappingObjectToEntity(obj);
            entity.Id = Guid.NewGuid();
            TEntity result = Repository.Insert(entity: entity);
            var _resp = SFactory.SingleMappingEntityToObject(result);
            return _resp;
         });
      }

      public TDataObject Create(TDataObject obj)
      {
         var entity = SFactory.SingleMappingObjectToEntity(obj);
         entity.Id = Guid.NewGuid();
         entity.CreatedDate = DateTime.Now;
         Repository.Insert(entity: entity);
         var _resp = SFactory.SingleMappingEntityToObject(entity);
         return _resp;
      }

      public bool CreateNoReturn(TDataObject obj)
      {
         try
         {
            var entity = SFactory.SingleMappingObjectToEntity(obj);
            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;
            Repository.Insert(entity: entity);
            return true;
         }
         catch (Exception)
         {
            return false;
         }
      }

      public TDataObject Update(TDataObject _obj)
      {
         try
         {
            var _entity = SFactory.SingleMappingObjectToEntity(_obj);
            Update(_entity);
            return _obj;
         }
         catch (Exception)
         {
            throw;
         }
      }

      public PaginationExtension<TDataObject> SearchPaginate(int pageSize, int page)
      {
         var entities = Repository.Get(null, x => x.OrderBy(r => r.CreatedDate), false);
         var _objs = SFactory.ListMappingEntitiesToObjects(entities);

         int Count = entities.Count();

         _objs = _objs.Skip((page - 1) * pageSize).Take(pageSize);
         int totalPages = (int)Math.Ceiling(Count / (double)pageSize);

         int itemEnd = pageSize * page;
         int itemStart = (itemEnd - pageSize) + 1;
         if (itemEnd > Count)
         {
            itemEnd = Count;
         }

         var msg = string.Format("Showing {0} to {1} of {2} Records | Total Pages: {3}",
                itemStart, itemEnd, Count, totalPages);

         var extension = new PaginationExtension<TDataObject>();

         extension.Summary = Count > 0 ? msg : string.Empty;
         extension.TotalCount = Count;
         extension.ReturnedList = _objs;
         extension.StaticPagedList = new StaticPagedList<TDataObject>(_objs, page, pageSize, Count);
         return extension;
      }

      public PaginationExtension<TDataObject> SearchPaginate(IQueryable<TDataObject> listData, int pageSize, int page)
      {
         int Count = listData.Count();

         listData = listData.Skip((page - 1) * pageSize).Take(pageSize);
         int totalPages = (int)Math.Ceiling(Count / (double)pageSize);

         int itemEnd = pageSize * page;
         int itemStart = (itemEnd - pageSize) + 1;
         if (itemEnd > Count)
         {
            itemEnd = Count;
         }

         var msg = string.Format("Showing {0} to {1} of {2} Records | Total Pages: {3}",
                itemStart, itemEnd, Count, totalPages);

         var extension = new PaginationExtension<TDataObject>();

         extension.Summary = Count > 0 ? msg : string.Empty;
         extension.TotalCount = Count;
         extension.ReturnedList = listData;
         extension.StaticPagedList = new StaticPagedList<TDataObject>(listData, page, pageSize, Count);

         return extension;
      }

      #endregion
   }
}