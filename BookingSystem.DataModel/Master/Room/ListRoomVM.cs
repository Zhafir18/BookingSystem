using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master.Room
{
    public class ListRoomVM
    {
        public int ID {  get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public string? Description { get; set; }
        public int LocationID { get; set; }
        public int Capasity { get; set; }
        public string? ColorRoom { get; set; }
        public string LocationName { get; set; }
    }
}
