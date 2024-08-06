using BookingSystem.DataAccsess.Models;
using BookingSystem.DataModel.Master.Location;
using BookingSystem.DataModel.Master.Role;
using BookingSystem.DataModel.Master.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Provider
{
    public class RoleProvider
    {
        private readonly BookingDatabaseContext _context;

        public RoleProvider(BookingDatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<MstRole> ALlRole()
        {
            return _context.MstRoles.Where(a => !a.DeletedDate.HasValue);
        }

        public MstRole GetRole(int id)
        {
            return _context.MstRoles.SingleOrDefault(a => a.RoleId == id);
        }

        public void InsertRole (CreateEditRoleVM model)
        {
            var newRole = new MstRole
            {
                RoleId = model.ID,
                RoleName = model.Name,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            };
            _context.MstRoles.Add(newRole);
            _context.SaveChanges();
        }

        public void UpdateRole (CreateEditRoleVM model)
        {
            var role = GetRole(model.ID);
            if (role != null)
            {
                role.RoleName = model.Name;
                role.UpdatedBy = 2;
                role.UpdatedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void DeleteRole (int id)
        {
            var role = GetRole(id);
            if (role != null )
            {
                role.DeletedBy = 3;
                role.DeletedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public CreateEditRoleVM GetOne(int id)
        {
            var list = GetRole(id);
            var role = new CreateEditRoleVM();
            role.ID = id;
            role.Name = list.RoleName;
            return role;
        }

        public List<RoleDropdown> GetRoleDropdown()
        {
            return _context.MstRoles.Select(l => new RoleDropdown
            {
                Id = l.RoleId,
                Name = l.RoleName
            }).ToList();
        }
    }
}
