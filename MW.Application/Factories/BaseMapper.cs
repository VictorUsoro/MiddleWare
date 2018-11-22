using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MW.Application
{
   /// <summary>
   /// MapStar factory mapping
   /// </summary>
   /// <typeparam name="T1"> Domain Entity</typeparam>
   /// <typeparam name="T2"> Data Object</typeparam>
   public class BaseMapper<T1, T2>
   {      
      public T2 SingleMappingEntityToObject(T1 entity)
      {
         var result = entity.Adapt<T1, T2>();
         return result;
      }

      public T1 SingleMappingObjectToEntity(T2 obj)
      {
         var result = obj.Adapt<T2, T1>();
         return result;
      }

      public IQueryable<T2> ListMappingEntitiesToObjects(IQueryable<T1> entities)
      {
         List<T2> result = new List<T2>();
         foreach(var item in entities)
         {
            result.Add(SingleMappingEntityToObject(item));
         }
         return result.AsQueryable();
      }

      public IQueryable<T1> ListMappingObjectToEntitiess(IQueryable<T2> entities)
      {
         List<T1> result = new List<T1>();
         foreach (var item in entities)
         {
            result.Add(SingleMappingObjectToEntity(item));
         }
         return result.AsQueryable();
      }
          
      public virtual T2 DefaultModel()
      {
         return (T2)Activator.CreateInstance(typeof(T2));
      }

      public virtual T1 DefaultEntity()
      {
         return (T1)Activator.CreateInstance(typeof(T1));
      }
   }
}
