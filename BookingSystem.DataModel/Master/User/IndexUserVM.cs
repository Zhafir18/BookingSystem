using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master.User
{
    public class IndexUserVM
    {
        public IEnumerable<ListUserVM> list { get; set; } = new List<ListUserVM>();

    }
}
