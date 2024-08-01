using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master.User
{
    public class ListUserVM
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public int RoleId { get; set; }
        public int CreatedBy { get; set; }
        public DateOnly CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateOnly UpdatedDate { get; set; }
    }
}
