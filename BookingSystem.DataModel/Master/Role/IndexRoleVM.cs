using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master.Role
{
    public class IndexRoleVM
    {
        public IEnumerable<ListRoleVM> List { get; set; } = new List<ListRoleVM>();
    }
}
