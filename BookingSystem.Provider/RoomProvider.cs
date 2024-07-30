using BookingSystem.DataAccsess.Models;
using BookingSystem.DataModel.Master.Resource;
using BookingSystem.DataModel.Master.Room;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingSystem.Provider
{
    public class RoomProvider
    {
        private readonly BookingDatabaseContext _context;

        public RoomProvider(BookingDatabaseContext context)
        {
            this._context = context;
        }

        public IQueryable<MstRoom> AllRoom()
        {
            return _context.MstRooms.Where(a => !a.DeletedDate.HasValue);
        }
        public IQueryable<MstResource> AllResource()
        {
            return _context.MstResources.Where(a => !a.DeletedDate.HasValue);
        }
        //public IQueryable<MstRoomResource> AllRoomResource()
        //{
        //    return _context.MstRoomResources.Where(a => !a.DelDate.HasValue);
        //}

        private MstRoom Get(int id)
        {
            return _context.MstRooms.SingleOrDefault(a => a.RoomId == id);
        }
        public void InsertRoom(CreateEditRoomVM model)
        {
            var room = new MstRoom
            {
                RoomName = model.Name,
                Capacity = model.Capasity,
                Description = model.Description,
                LocationId = model.LocationID,
                Floor = model.Floor,
                Color = model.ColorRoom,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            };
            _context.MstRooms.Add(room);
            _context.SaveChanges();
        }

        public void UpdateRoom(CreateEditRoomVM model)
        {
            var room = Get(model.ID);
            if (room != null)
            {
                room.RoomName = model.Name;
                room.Capacity = model.Capasity;
                room.Description = model.Description;
                room.LocationId = model.LocationID;
                room.Floor = model.Floor;
                room.Color = model.ColorRoom;
                room.UpdatedBy = 2;
                room.UpdatedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void DeleteRoom(int id)
        {
            var room = Get(id);
            if (room != null)
            {
                room.DeletedBy = 3;
                room.DeletedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public IndexRoomVM GetIndexBC(int page)
        {
            var indexRes = new IndexRoomVM();
            var listRes = from a in AllRoom()
                          join b in _context.MstLocations on a.LocationId equals b.LocationId
                          select new ListRoomVM
                          {
                              ID = a.RoomId,
                              Name = a.RoomName,
                              Description = a.Description,
                              Capasity = a.Capacity,
                              ColorRoom = a.Color,
                              LocationID = a.LocationId,
                              Floor = a.Floor,
                              LocationName = b.LocationName
                          };
            if (page > 0)
            {
                listRes = listRes.Skip((page - 1) * 10).Take(10);
            }
            indexRes.list = listRes.ToList();
            return indexRes;
        }
        public CreateEditRoomVM GetOne(int id)
        {
            var list = Get(id);
            var room = new CreateEditRoomVM();
            room.ID = list.RoomId;
            room.LocationID = list.LocationId;
            room.Floor = list.Floor;
            room.Name = list.RoomName;
            room.Description = list.Description;
            room.Capasity = list.Capacity;
            room.ColorRoom = list.Color;
            //room.RoomRes = GetRoomResource(room.ID);
            //room.Resource = GetResource();
            return room;
        }
        //public List<CreateEditRoomResVM> GetResource()
        //{
        //    var listRes = from a in AllResource()
        //                  select new CreateEditRoomResVM
        //                  {
        //                      ID = a.Id,
        //                      ResourceId = a.Id,
        //                      ResourceName = a.Name,

        //                  };
        //    return listRes.ToList();
        //}
        //public List<CreateEditRoomResVM> GetRoomResource(int roomid)
        //{
        //    var listRes = from a in AllRoomResource()
        //                  where a.RoomId == roomid
        //                  select new CreateEditRoomResVM
        //                  {
        //                      ID = a.Id,
        //                      ResourceId = a.Id

        //                  };
        //    return listRes.ToList();
        //}
    }
}
