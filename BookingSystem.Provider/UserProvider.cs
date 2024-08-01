using BookingSystem.DataAccsess.Models;
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
            var user = new MstUser
            {
                LoginName = model.LoginName,
                Password = model.Password,
                RoleId = model.RoleId,
                CreatedBy = 1,
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
                user.Password = model.Password;
                user.RoleId = model.RoleId;
                _context.SaveChanges();
            }
        }
    }
}
