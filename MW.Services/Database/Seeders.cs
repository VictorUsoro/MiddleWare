//using ch.services.ServiceHandlers.SSO;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ch.services.Database
//{
//   public class Seeders
//   {
//      public readonly IServiceInit _services;
//      private readonly ISsoManager _ssoManager;
//      private readonly IConfiguration _config;
//      private readonly IHostingEnvironment _hostEnv;

//      public Seeders(ISsoManager ssoManager, IConfiguration config,
//         IServiceInit services, IHostingEnvironment hostEnv)
//      {
//         _ssoManager = ssoManager;
//         _config = config;
//         _services = services;
//         _hostEnv = hostEnv;
//      }

//      public bool SeedRoles()
//      {
//         try
//         {
//            var roles = _services.UnitOfWork.UserRoleRepository
//            .Get(filter: null, orderBy: null, includeDeleted: false);
//            if (roles == null || roles.Count() < 1)
//            {
//               var date = DateTime.Now;
//               var userRoles = new List<UserRole>
//                  {
//                     new UserRole{ RoleName = "App Admin", Id = Guid.NewGuid(),
//                        CreatedDate = date, ModifiedDate = date},
//                     new UserRole{ RoleName = "App Sub-Admin", Id = Guid.NewGuid(),
//                        CreatedDate = date, ModifiedDate = date},
//                     new UserRole{ RoleName = "Super Admin", Id = Guid.NewGuid(),
//                        CreatedDate = date, ModifiedDate = date},
//                     new UserRole{ RoleName = "Group Admin", Id = Guid.NewGuid(),
//                        CreatedDate = date, ModifiedDate = date},
//                     new UserRole{ RoleName = "District Admin", Id = Guid.NewGuid(),
//                        CreatedDate = date, ModifiedDate = date},
//                  };
//               _services.UnitOfWork.UserRoleRepository.InsertRange(userRoles);
//            }
//            return true;
//         }
//         catch (Exception)
//         {
//            //Log here
//            return false;
//         }
//      }

//      public bool SeedAdminMenu()
//      {
//         try
//         {
//            var appMenu = _services.UnitOfWork.AppMenuRepository.Get(null, null, false);
//            if (appMenu == null || appMenu.Count() < 1)
//            {
//               var date = DateTime.Now;
//               var topMenu = new AppMenu
//               {
//                  DisplayName = "App Management",
//                  Name = "ApplicationManagement",
//                  MenuType = MenuType.TopMenu,
//                  TopMenu = null,
//                  Id = Guid.NewGuid()
//               };

//               topMenu = _services.UnitOfWork.AppMenuRepository.Insert(topMenu);
//               var menuList = new List<AppMenu>
//               {
//                 new AppMenu
//                  {
//                     Id = Guid.NewGuid(),
//                    CreatedDate = date,
//                    MenuType = MenuType.SubMenu,
//                    ModifiedDate = date,
//                    DisplayName = "App Menu",
//                    Name = "AppMenu",
//                    TopMenu = topMenu.Id
//                  },
//                  new AppMenu
//                  {
//                     Id = Guid.NewGuid(),
//                     CreatedDate = date,
//                     MenuType = MenuType.SubMenu,
//                     ModifiedDate = date,
//                     DisplayName = "Permissions",
//                     Name = "Permissions",
//                     TopMenu = topMenu.Id
//                  }
//               };

//               _services.UnitOfWork.AppMenuRepository.InsertRange(menuList);
//            }
//            return true;
//         }
//         catch (Exception)
//         {
//            //log here
//            return false;
//         }
//      }

//      public async Task<bool> SeedSuperAdmin()
//      {
//         try
//         {
//            var ssoUser = new SsoUserModel();
//            ssoUser.Email = Constant.SuperAdminEmail;
//            ssoUser.FirstName = "Victor";
//            ssoUser.LastName = "Usoro";
//            ssoUser.PhoneNumber = "NoNumber";
//            ssoUser.IsPasswordSet = false;
//            ssoUser.Password = ssoUser.ConfirmPassword = Constant.Reference + "q$";
//            var ssoUserProfile = await _ssoManager.CreateUserAsync(ssoUser,
//               _config["AppSettingLinks:SSOLink"]);
//            if (ssoUserProfile.Status != "00")
//            {
//               return false;
//            }

//            var roles = _services.UnitOfWork.UserRoleRepository.Get(null, null, false);
//            if (roles == null || roles.Count() < 1)
//            {
//               return false;
//            }

//            var adminRole = roles.FirstOrDefault(x => x.RoleName == "Super Admin");
//            if (adminRole == null)
//            {
//               return false;
//            }

//            var adminUser = _services.UnitOfWork.UserProfileRepository
//               .GetBy(c => c.Email == Constant.SuperAdminEmail);

//            if (adminUser == null)
//            {
//               var userprofile = new UserProfile()
//               {
//                  FullName = "Victor Usoro",
//                  Email = Constant.SuperAdminEmail,
//                  ProfileType = ProfileType.Admin,
//                  Phone = "08136277844",
//                  Address = string.Empty,
//                  UserCode = ssoUserProfile.Data,
//                  UserRoleId = adminRole.Id
//               };
//               _services.UnitOfWork.UserProfileRepository.Insert(userprofile);
//               return true;
//            }
//            else
//            {
//               if (string.IsNullOrEmpty(adminUser.UserCode))
//               {
//                  adminUser.UserCode = ssoUserProfile.Data;
//               }
//               if (!adminUser.UserRoleId.HasValue)
//               {
//                  adminUser.UserRoleId = adminRole.Id;
//               }
//               _services.UnitOfWork.UserProfileRepository.Update(adminUser);
//               return true;
//            }
//         }
//         catch (Exception)
//         {
//            return false;
//         }
//      }

//      public async Task<bool> SeedSuperPermisions()
//      {
//         try
//         {
//            var adminUser = _services.UserProfileServices
//               .GetProfileByEmail(Constant.SuperAdminEmail);

//            if (adminUser == null)
//            {
//               //Log here
//               return false;
//            }
//            if (!adminUser.UserRoleId.HasValue)
//            {
//               return false;
//            }

//            var userMenu = _services.AppMenuServices.GetManyEntity();
//            if (userMenu == null || userMenu.Count() < 1)
//            {
//               return false;
//            }

//            var permissions = _services.UserPermissionServices
//               .GetPermittedMenuList(adminUser.UserRoleId.Value);
//            if (permissions == null || permissions.Count < 1)
//            {
//               var appMenu = userMenu.FirstOrDefault(x => x.Name == "AppMenu");
//               var menuPermision = userMenu.FirstOrDefault(x => x.Name == "Permissions");
//               var topMenu = userMenu.FirstOrDefault(x => x.Name == "ApplicationManagement");
//               var date = DateTime.Now;
//               List<UserPermission> userPermissions = new List<UserPermission>
//               {
//                  new UserPermission
//                  {
//                     AppMenuId = appMenu.Id, Id = Guid.NewGuid(),
//                     UserRoleId = adminUser.UserRoleId.Value,
//                     CreatedDate = date, ModifiedDate = date
//                  },
//                  new UserPermission
//                  {
//                     AppMenuId = menuPermision.Id, Id = Guid.NewGuid(),
//                     UserRoleId = adminUser.UserRoleId.Value,
//                     CreatedDate = date, ModifiedDate = date
//                  },
//                   new UserPermission
//                   {
//                      AppMenuId = topMenu.Id, Id = Guid.NewGuid(),
//                      UserRoleId = adminUser.UserRoleId.Value,
//                      CreatedDate = date, ModifiedDate = date
//                   }
//               };
//               await _services.UserPermissionServices.CreateManyAsync(userPermissions);
//            }
//            return true;
//         }
//         catch (Exception)
//         {
//            return false;
//         }
//      }

//      public async Task<bool> SeedCountries()
//      {
//         try
//         {
//            var countryLocations = _services.LocationServices.UnitOfWork.LocationRepository
//            .Exists(filter: f => f.LocationType == LocationType.Country);
//            if (countryLocations)
//            {
//               return true;
//            }

//            string countryJson = System.IO.File.ReadAllText(_hostEnv.WebRootPath + Constant.ListCountriesAddress);
//            List<NameCodeSeederModel> countryList = Utility.DeserializeJson<List<NameCodeSeederModel>>(countryJson);
//            foreach (var i in countryList)
//            {
//               await _services.LocationServices.CreateAsync(new
//                  Location
//               { Name = i.name, Code = i.code, LocationType = LocationType.Country });
//            }

//            return true;
//         }
//         catch (Exception)
//         {
//            return false;
//         }
//      }

//      public async Task<bool> SeedStateForNigeria()
//      {
//         try
//         {
//            var nigeria = await _services.LocationServices.GetLocationByCountryCode(code: "NG");
//            if (nigeria == null)
//            {
//               return true;
//            }

//            var nigerianStates = _services.LocationServices.Search(ControlId: nigeria.Id, locationtype: (int)LocationType.State);
//            if (nigerianStates.Any())
//            {
//               return true;
//            }

//            string stateJson = System.IO.File.ReadAllText(_hostEnv.WebRootPath + Constant.ListNigerianStateAddress);
//            List<NameCodeSeederModel> stateList = Utility.DeserializeJson<List<NameCodeSeederModel>>(stateJson);
//            foreach (var i in stateList)
//            {
//               await _services.LocationServices.CreateAsync(new
//                  Location
//               { Name = i.name, LocationType = LocationType.State, ControlId = nigeria.Id });
//            }

//            return true;
//         }
//         catch (Exception)
//         {
//            return false;
//         }
//      }

//      public async Task<bool> SeedSMSSettings()
//      {
//         try
//         {
//            var smsSettings = _services.SmsSettingServices.GetManyEntity().FirstOrDefault();
//            if (smsSettings == null)
//            {
//               smsSettings = new SmsSetting
//               {
//                  SMSDeliveryRoute = SMSDeliveryRoute.BulkSMSNigeria,
//                  SellingAmount = 2
//               };
//               await _services.SmsSettingServices.CreateAsync(smsSettings);               
//            }
//            return true;
//         }
//         catch (Exception)
//         {
//            return false;
//         }
//      }
//   }
//}
