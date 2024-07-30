using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master.Room
{
    public class CreateEditRoomResVM
    {
        public int ID { get; set; }
        public int ResourceId { get; set; }
        public string? ResourceName {  get; set; }
    }
}
