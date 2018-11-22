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

      private Repository<TEntity> Repository
      {
         get { return new Repository<TEntity>(_context); }
      }

      #region Crud

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