using BookingSystem.DataAccsess.Models;
using BookingSystem.DataModel.Master.BookingCode;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem.Provider
{
    public class BookingCodeProvider
    {
        private readonly BookingDatabaseContext _context;

        public BookingCodeProvider(BookingDatabaseContext context)
        {
            this._context = context;
        }

        public IQueryable<MstBooking> AllBookingCodes()
        {
            return _context.MstBookings.Where(a => !a.DeletedDate.HasValue);
        }

        private MstBooking Get(int id)
        {
            return _context.MstBookings.SingleOrDefault(a => a.BookingId == id);
        }

        public void InsertBc(CreateEditBCVM model)
        {
            var newBc = new MstBooking
            {
                BookingCode = model.BookingCode,
                Status = model.IsActive,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            };
            _context.MstBookings.Add(newBc);
            _context.SaveChanges();
        }

        public void UpdateBc(CreateEditBCVM model)
        {
            var bc = Get(model.ID);
            if (bc != null)
            {
                bc.Status = model.IsActive;
                bc.BookingCode = model.BookingCode;
                bc.UpdatedBy = 2;
                bc.UpdatedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void DeleteBC(int id)
        {
            var bc = Get(id);
            if (bc != null)
            {
                bc.DeletedBy = 3;
                bc.DeletedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public IndexBCVM GetIndexBC(int page)
        {
            var indexBc = new IndexBCVM();
            var listBc = from a in AllBookingCodes()
                          select new ListBCVM
                          {
                              ID = a.BookingId,
                              BookingCode = a.BookingCode,
                              IsActive = a.Status
                          };
            if(page > 0)
            {
                listBc=listBc.Skip((page-1)*10).Take(10);
            }
            indexBc.list = listBc.ToList();
            return indexBc;
        }
        public CreateEditBCVM GetOne(int id)
        {
            var list = Get(id);
            var bc=new CreateEditBCVM();
            bc.IsActive = list.Status;
            bc.BookingCode = list.BookingCode;
            bc.ID = id;
            return bc;
        }
    }
}
