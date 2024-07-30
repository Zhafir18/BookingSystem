using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master.BookingCode
{
    public class CreateEditBCVM
    {
        public int ID { get; set; }
        public string BookingCode { get; set; }
        public bool IsActive { get; set; }
    }
}
