using BookingSystem.DataAccsess.Models;
using BookingSystem.DataModel;
using BookingSystem.DataModel.Master.BookingCode;
using BookingSystem.DataModel.Master.Location;
using BookingSystem.DataModel.Master.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Provider
{
    public class LocationProvider
    {
        private BookingDatabaseContext _context;
        public LocationProvider(BookingDatabaseContext context)
        {
            this._context = context;
        }
        public IQueryable AllLocation()
        {
            return _context.MstLocations.Where(a => !a.DeletedDate.HasValue);
        }
        private MstLocation Get(int Id)
        {
            return _context.MstLocations.SingleOrDefault(a => a.LocationId == Id);
        }
        public void InsertLoc(CreatEditLVM model)
        {
            var newLoc = new MstLocation();
            newLoc.LocationName = model.Name;
            newLoc.CreatedBy = 1;
            newLoc.CreatedDate = DateTime.Now;
            _context.MstLocations.Add(newLoc);
            _context.SaveChanges();
        }

        public void UpdateLoc(CreatEditLVM model)
        {
            var loc = Get(model.ID);
            loc.LocationName = model.Name;
            loc.LocationName = model.Name;
            loc.UpdatedBy = 2;
            loc.UpdatedDate = DateTime.Now;
            _context.SaveChanges();
        }
        public void DeleteLoc(int Id)
        {
            var loc = Get(Id);
            loc.DeletedBy = 3;
            loc.DeletedDate = DateTime.Now;
            _context.SaveChanges();
        }

        public List<LocationDropdown> GetLocationDropdown()
        {
            return _context.MstLocations.Select(l => new LocationDropdown
            {
                Id = l.LocationId,
                Name = l.LocationName
            }).ToList();
        }
    }
}
