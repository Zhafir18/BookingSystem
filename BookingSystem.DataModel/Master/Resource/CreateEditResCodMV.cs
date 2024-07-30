using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master.Resource
{
    public class CreateEditResCodMV
    {
        public int ID { get; set; }
        public string? ResourceCode { get; set; }
        public bool IsActive { get; set; }
    }
}
