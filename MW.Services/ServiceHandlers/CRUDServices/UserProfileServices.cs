using MW.Application;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MW.Services
{
   public class UserProfileServices : BaseServices<UserProfile, UserProfileModel>
   {
      public UserProfileServices(MiddleWareDBContext context) : base(context) { }

      public UserProfile GetProfileByEmail(string email)
      {
         var profile = UnitOfWork.UserProfileRepository.GetBy(x => x.Email == email, 
            properties: new Expression<Func<UserProfile, object>>[] {
               prop => prop.Church,
               pr => pr.UserRole });
         return profile;
      }

      public Task<UserProfile> GetProfileById(Guid Id)
      {
         return Task.Run(() =>
         {
            var profile = UnitOfWork.UserProfileRepository.GetBy(x => x.Id == Id, prop => prop.Church);
            return profile;
         });
      }

      public IQueryable<UserProfile> Search(string name = null, string phone = null, string email = null, 
         string address = null, int? gender = null, Guid? churchId = null, Guid? ncommereventid = null, 
         int? profileType = null, int? excludeprofileType = null, bool? isActive = null)
      {
         IQueryable<UserProfile> _objs = GetManyEntity();

         _objs = !string.IsNullOrEmpty(name) ? _objs.Where(x => x.FullName.ToLower().Contains(name.ToLower())) : _objs;

         _objs = !string.IsNullOrEmpty(phone) ? _objs.Where(x => !string.IsNullOrEmpty(x.Phone) 
         && x.Phone.Contains(phone)) : _objs;

         _objs = !string.IsNullOrEmpty(email) ? _objs.Where(x => x.Email.ToLower().Contains(email)) : _objs;

         _objs = !string.IsNullOrEmpty(address) ? _objs.Where(x => x.Address != null 
         && x.Address.ToLower().Contains(address.ToLower())) : _objs;

         _objs = gender.HasValue ? _objs.Where(x => x.Gender.HasValue && x.Gender.Value == (Gender)gender.Value) : _objs;

         _objs = ncommereventid.HasValue ? _objs.Where(x => x.ChurchEventId.HasValue 
         && x.ChurchEventId.Value == ncommereventid) : _objs;

         _objs = churchId.HasValue ? _objs.Where(x => x.ChurchId.HasValue && x.ChurchId.Value == churchId.Value) : _objs;

         _objs = profileType.HasValue ? _objs.Where(x => x.ProfileType == (ProfileType)profileType) : _objs;

         _objs = excludeprofileType.HasValue && !profileType.HasValue ? _objs.Where(x => x.ProfileType != (ProfileType)excludeprofileType) : _objs;

         _objs = isActive.HasValue ? _objs.Where(x => x.RecordStatus == RecordStatus.Active) : _objs;

         return _objs.OrderBy(x => x.FullName);
      }

      public Task<IQueryable<UserProfile>> SearchAsync(string name = null, string phone = null, string email = null, string address = null, int? gender = null, Guid? churchId = null, Guid? ncommereventid = null, int? profileType = null, int? excludeprofileType = null, bool? isActive = null)
      {
         return Task.Run(() =>
         {
            return Search(name: name, phone: phone, address: address, email: email, gender: gender, churchId: 
               churchId, ncommereventid: ncommereventid, profileType: profileType, excludeprofileType:
               excludeprofileType, isActive: isActive);
         });
      }

      public Task<PaginationExtension<UserProfile>> SearchPaginateAsync(int? page = 1, int? pageSize = 10,
         string name = null, string phone = null, string email = null, string address = null, 
         int? gender = null, Guid? churchId = null, Guid? ncommerId = null, int? profileType = null, 
         int? excludeprofileType = null, bool? isActive = null)
      {
         return Task.Run(() =>
         {
            var _objs = Search(name: name, phone: phone, address: address, email: email, gender: gender, 
               churchId: churchId, ncommereventid: ncommerId, profileType: profileType,
               excludeprofileType: excludeprofileType, isActive: isActive);

            var paginatedData = SearchEntityPaginate(_objs, page: page.Value, pageSize: pageSize.Value);

            return paginatedData;
         });
      }

      public JSendServiceResponseModel<UserProfileModel> UpdateProfile(UserProfileModel model)
      {
         try
         {
            var user = UnitOfWork.UserProfileRepository.GetById(model.Id);
            if (user == null)
            {
               return new JSendServiceResponseModel<UserProfileModel>
               {
                  Status = false,
                  Data = null,
                  Message = "Invalid user profile ID"
               };
            }

            user.FullName = model.FullName;
            user.Gender = model.Gender;
            user.Occupation = model.Occupation;
            user.Phone = model.Phone;
            user.Address = model.Address;
            user.DOB = model.DOB;
            user.Email = user.ProfileType != ProfileType.Admin ? model.Email : user.Email;

            UnitOfWork.UserProfileRepository.Update(user);

            return new JSendServiceResponseModel<UserProfileModel>
            {
               Status = true,
               Data = model,
               Message = $"{model.FullName}'s profile was updated successfully.."
            };
         }
         catch (Exception)
         {
            return new JSendServiceResponseModel<UserProfileModel>
            {
               Status = false,
               Data = null,
               Message = "An error occured, kindly try again later"
            };
         }
      }

      #region Newcommers Profile

      public IQueryable<UserProfile> NewCommerSearch(DateTime? startDate = null, DateTime? endDate = null, string name = null, string phone = null, string email = null, string address = null, int? gender = null, Guid? churchId = null, bool? isActive = null, Guid? ncommereventid = null)
      {
         var _objs = UnitOfWork.UserProfileRepository.Get(filter: f => f.ProfileType == ProfileType.NewCommer && f.ChurchEventId.HasValue, orderBy: x => x.OrderBy(xx => xx.FullName), includeDeleted: false, properties: y => y.ChurchEvent);

         _objs = ncommereventid.HasValue ? _objs.Where(x => x.ChurchEventId == ncommereventid) : _objs;

         _objs = startDate.HasValue ? _objs.Where(x => x.ChurchEvent.Date >= startDate) : _objs;

         _objs = endDate.HasValue ? _objs.Where(x => x.ChurchEvent.Date <= endDate) : _objs;

         _objs = !string.IsNullOrEmpty(name) ? _objs.Where(x => x.FullName.ToLower().Contains(name.ToLower())) : _objs;

         _objs = !string.IsNullOrEmpty(phone) ? _objs.Where(x => !string.IsNullOrEmpty(x.Phone) && x.Phone.Contains(phone)) : _objs;

         _objs = !string.IsNullOrEmpty(email) ? _objs.Where(x => x.Email.ToLower().Contains(email)) : _objs;

         _objs = !string.IsNullOrEmpty(address) ? _objs.Where(x => x.Address != null && x.Address.ToLower().Contains(address.ToLower())) : _objs;

         _objs = gender.HasValue ? _objs.Where(x => x.Gender.HasValue && x.Gender.Value == (Gender)gender.Value) : _objs;

         _objs = isActive.HasValue ? _objs.Where(x => x.RecordStatus == RecordStatus.Active) : _objs;

         return _objs;
      }

      public Task<IQueryable<UserProfile>> NewCommerSearchAsync(DateTime? startDate = null, DateTime? endDate = null, string name = null, string phone = null, string email = null, string address = null, int? gender = null, Guid? churchId = null, bool? isActive = null, Guid? ncommereventid = null)
      {
         return Task.Run(() =>
         {
            return NewCommerSearch(name: name, phone: phone, address: address, email: email, gender: gender, churchId: churchId, startDate: startDate, endDate: endDate, isActive: isActive, ncommereventid: ncommereventid);
         });
      }

      public Task<PaginationExtension<UserProfile>> SearchPaginateNewcommersAsync(DateTime? startDate = null, DateTime? endDate = null, int? page = 1, int? pageSize = 10, string name = null, string phone = null, string email = null, string address = null, int? gender = null, Guid? churchId = null, bool? isActive = null, Guid? ncommereventid = null)
      {
         return Task.Run(() =>
         {
            var _objs = NewCommerSearch(name: name, phone: phone, address: address, email: email, gender: gender, churchId: churchId, startDate: startDate, endDate: endDate, isActive: isActive, ncommereventid: ncommereventid);

            var paginatedData = SearchEntityPaginate(_objs, page: page.Value, pageSize: pageSize.Value);

            return paginatedData;
         });
      }

      public Task<int> CountNewcommers(Guid ChurchEventId)
      {
         return Task.Run(() =>
         {
            var ncommersCount = UnitOfWork.UserProfileRepository.Get(x => x.ChurchEventId.HasValue && x.ChurchEventId == ChurchEventId, null, includeDeleted: false).Count();
            return ncommersCount;
         });
      }

      public Task<UserProfile> GetUserProfileByEmail(string email)
      {
         return Task.Run(() =>
         {
            var profile = UnitOfWork.UserProfileRepository.GetBy(x => x.Email.ToLower() == email.ToLower());
            return profile;
         });
      }

      public Task<UserProfile> GetUserProfileById(Guid Id)
      {
         return Task.Run(() =>
         {
            var profile = UnitOfWork.UserProfileRepository.GetBy(x => x.Id == Id, prop => prop.Church);
            return profile;
         });
      }

      #endregion

      #region Admin Users

      //public List<UserProfile> SearchAdminUser(Guid controlId, string name = null, string phone = null,
      //   string email = null, string address = null, bool? isActive = null)
      //{
      //   var churchIds = UnitOfWork.ChurchRepository.Get(filter: x => (x.ControlId.HasValue && x.ControlId.Value == controlId )
      //   || (x.DistrictId.HasValue && x.DistrictId.Value == controlId), orderBy: null, includeDeleted: false).Select(x => x.Id);
      //   List<Guid> churchGuids = new List<Guid>();
        
      //   churchGuids.AddRange(churchIds);
      //   churchGuids.Add(controlId);
        
      //   List<UserProfile> _objs = new List<UserProfile>();
      //   if (churchGuids.Any())
      //   {
      //      foreach (var churchId in churchGuids)
      //      {
      //         var profiles = UnitOfWork.UserProfileRepository.Get(filter: x => x.ChurchId.Value == churchId && x.ProfileType == ProfileType.SingleChurchLeader, orderBy: null, 
      //            includeDeleted: false, properties: prop => prop.Church);
      //         _objs.AddRange(profiles);
      //      }

      //      _objs = !string.IsNullOrEmpty(name) ? _objs.Where(x => x.FullName.ToLower().Contains(name.ToLower())).ToList() : _objs;
      //      _objs = !string.IsNullOrEmpty(phone) ? _objs.Where(x => !string.IsNullOrEmpty(x.Phone) && x.Phone.Contains(phone)).ToList() : _objs;
      //      _objs = !string.IsNullOrEmpty(email) ? _objs.Where(x => x.Email.ToLower().Contains(email)).ToList() : _objs;
      //      _objs = isActive.HasValue ? _objs.Where(x => x.RecordStatus == RecordStatus.Active).ToList() : _objs;
      //      return _objs.OrderBy(x => x.FullName).ToList();
      //   }

      //   return _objs;
      //}

      //public Task<PaginationExtension<UserProfile>> SearchPaginateAdminUserAsync(Guid controlId, string name = null,
      //   string phone = null, string email = null, string address = null, bool? isActive = null, int page = 1, 
      //   int pageSize =10)
      //{
      //   return Task.Run(() =>
      //   {
      //      var _objs = SearchAdminUser(controlId: controlId, name: name, phone: phone, email: email, isActive: isActive).AsQueryable();
      //      var paginatedData = SearchEntityPaginate(_objs, page: page, pageSize: pageSize);

      //      return paginatedData;
      //   });
      //}


      #endregion

      public JSendServiceResponseModel<UserProfileModel> UpdateModel(UserProfileModel model)
      {
         try
         {
            var entity = UnitOfWork.UserProfileRepository.GetById(model.Id);
            if (entity == null)
            {
               return new JSendServiceResponseModel<UserProfileModel>
               {
                  Data = null,
                  Message = "Invalid Profile",
                  Status = false
               };
            }

            entity.Address = model.Address;
            entity.ChurchEventId = model.ChurchEventId;
            entity.ChurchId = model.ChurchId;
            entity.Email = model.Email;
            entity.FullName = model.FullName;
            entity.Gender = model.Gender;
            entity.Occupation = model.Occupation;
            entity.Phone = model.Phone;
            entity.ProfileType = model.ProfileType;

            Update(entity);

            return new JSendServiceResponseModel<UserProfileModel>
            {
               Data = model,
               Message = $"{model.FullName}'s profie was updated successfully..",
               Status = true
            };
         }
         catch (Exception ex)
         {
            return new JSendServiceResponseModel<UserProfileModel>
            {
               Data = null,
               Message = ex.Message,
               Status = false
            };
         }
      }

      public DateTime? ConvertDateTime(string data)
      {
         DateTime dateTime;
         return DateTime.TryParseExact(data, "MMddyyyy", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out dateTime) ? Convert.ToDateTime(dateTime.ToString("MMddyyyy")) : dateTime;
      }
   }
}