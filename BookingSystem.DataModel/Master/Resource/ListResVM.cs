using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master.Resource
{
    public class ListResVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Icon { get; set; }
    }
}
