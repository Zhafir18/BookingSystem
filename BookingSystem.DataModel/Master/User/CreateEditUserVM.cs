using BookingSystem.DataModel.Master.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master.User
{
    public class CreateEditUserVM
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public int RoleId { get; set; }
        public List<RoleDropdown> RoleDropdown {  get; set; }
    }
}
