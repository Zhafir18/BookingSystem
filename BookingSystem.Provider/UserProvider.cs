using BookingSystem.DataAccsess.Models;
using BookingSystem.DataModel.Master.Role;
using BookingSystem.DataModel.Master.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Provider
{
    public class UserProvider
    {
        private readonly BookingDatabaseContext _context;

        public UserProvider(BookingDatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<MstUser> AllUser()
        {
            return _context.MstUsers.Where(a => !a.DeletedDate.HasValue);
        }

        public MstUser GetUser(int id)
        {
            return _context.MstUsers.SingleOrDefault(a => a.UserId == id);
        }

        public void InsertUser(CreateEditUserVM model)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new MstUser
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                LoginName = model.LoginName,
                Password = hashedPassword,
                RoleId = model.RoleId,
                CreatedBy = 2,
                CreatedDate = DateTime.Now
            };
            _context.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(CreateEditUserVM model)
        {
            var user = GetUser(model.UserId);
            if (user != null)
            {
                user.LoginName = model.LoginName;

                if (!string.IsNullOrEmpty(model.Password))
                {
                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                    user.Password = hashedPassword;
                }

                user.RoleId = model.RoleId;
                user.UpdatedBy = 3;
                user.UpdatedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var user = GetUser(id);
            if (user != null)
            {
                user.DeletedBy = 3;
                user.DeletedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public IndexUserVM GetIndexUser()
        {
            var indexUser = new IndexUserVM();
            var listUser = from a in AllUser()
                           join creator in _context.MstUsers on a.CreatedBy equals creator.UserId into creatorGroup
                           from creator in creatorGroup.DefaultIfEmpty()
                           join updater in _context.MstUsers on a.UpdatedBy equals updater.UserId into updaterGroup
                           from updater in updaterGroup.DefaultIfEmpty()
                           select new ListUserVM
                           {
                               UserId = a.UserId,
                               LoginName = a.LoginName,
                               RoleName = a.Role.RoleName,
                               CreatedBy = (creator == null)
                                   ? ""
                                   : (string.IsNullOrEmpty(creator.MiddleName) && string.IsNullOrEmpty(creator.LastName))
                                       ? creator.FirstName
                                       : creator.FirstName + (string.IsNullOrEmpty(creator.MiddleName) ? "" : " " + creator.MiddleName) + (string.IsNullOrEmpty(creator.LastName) ? "" : " " + creator.LastName),
                               CreatedDate = a.CreatedDate,
                               UpdatedBy = (updater == null)
                                   ? ""
                                   : (string.IsNullOrEmpty(updater.MiddleName) && string.IsNullOrEmpty(updater.LastName))
                                       ? updater.FirstName
                                       : updater.FirstName + (string.IsNullOrEmpty(updater.MiddleName) ? "" : " " + updater.MiddleName) + (string.IsNullOrEmpty(updater.LastName) ? "" : " " + updater.LastName),
                               UpdatedDate = a.UpdatedDate
                           };

            indexUser.list = listUser.ToList();
            return indexUser;
        }

        public CreateEditUserVM GetOne(int id)
        {
            var user = GetUser(id);
            var listUser = new CreateEditUserVM();
            listUser.UserId = user.UserId;
            listUser.FirstName = user.FirstName;
            listUser.MiddleName = user.MiddleName;
            listUser.LastName = user.LastName;
            listUser.Password = user.Password;
            listUser.LoginName = user.LoginName;
            listUser.RoleId = user.RoleId;
            return listUser;
        }

        public IndexUserRoleVM GetUserRoleIndex()
        {
            var indexUser = new IndexUserRoleVM();

            var listUser = from user in _context.MstUsers
                           join role in _context.MstRoles on user.RoleId equals role.RoleId
                           join creator in _context.MstUsers on user.CreatedBy equals creator.UserId into creatorGroup
                           from creator in creatorGroup.DefaultIfEmpty()
                           join updater in _context.MstUsers on user.UpdatedBy equals updater.UserId into updaterGroup
                           from updater in updaterGroup.DefaultIfEmpty()
                           where !user.DeletedDate.HasValue
                           select new UserRoleVM
                           {
                               Id = role.RoleId,
                               UserId = user.UserId,
                               Name = (string.IsNullOrEmpty(user.MiddleName) && string.IsNullOrEmpty(user.LastName))
                                   ? user.FirstName
                                   : user.FirstName + (string.IsNullOrEmpty(user.MiddleName) ? "" : " " + user.MiddleName) + (string.IsNullOrEmpty(user.LastName) ? "" : " " + user.LastName),
                               CreatedBy = (creator == null)
                                   ? ""
                                   : (string.IsNullOrEmpty(creator.MiddleName) && string.IsNullOrEmpty(creator.LastName))
                                       ? creator.FirstName
                                       : creator.FirstName + (string.IsNullOrEmpty(creator.MiddleName) ? "" : " " + creator.MiddleName) + (string.IsNullOrEmpty(creator.LastName) ? "" : " " + creator.LastName),
                               UpdatedBy = (updater == null)
                                   ? ""
                                   : (string.IsNullOrEmpty(updater.MiddleName) && string.IsNullOrEmpty(updater.LastName))
                                       ? updater.FirstName
                                       : updater.FirstName + (string.IsNullOrEmpty(updater.MiddleName) ? "" : " " + updater.MiddleName) + (string.IsNullOrEmpty(updater.LastName) ? "" : " " + updater.LastName),
                               CreatedDate = user.CreatedDate,
                               UpdatedDate = user.UpdatedDate
                           };

            indexUser.list = listUser.ToList();
            return indexUser;
        }
    }
}
