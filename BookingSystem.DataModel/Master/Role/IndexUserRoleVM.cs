using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master.Role
{
    public class IndexUserRoleVM
    {
        public IEnumerable<UserRoleVM> list { get; set; } = new List<UserRoleVM>();
    }
}
