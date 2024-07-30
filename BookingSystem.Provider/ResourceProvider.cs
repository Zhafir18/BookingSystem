using BookingSystem.DataAccsess.Models;
using BookingSystem.DataModel.Master.BookingCode;
using BookingSystem.DataModel.Master.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Provider
{
    public class ResourceProvider
    {
        private readonly BookingDatabaseContext _context;

        public ResourceProvider(BookingDatabaseContext context)
        {
            this._context = context;
        }

        public IQueryable<MstResource> AllResource()
        {
            return _context.MstResources.Where(a => !a.DeletedDate.HasValue);
        }
        public IQueryable<MstResourceCode> AllResourceCode()
        {
            return _context.MstResourceCodes.Where(a => !a.DeletedDate.HasValue);
        }

        private MstResource GetRes(int id)
        {
            return _context.MstResources.SingleOrDefault(a => a.ResourceId == id);
        }
        private MstResourceCode GetResCod(int id)
        {
            return _context.MstResourceCodes.SingleOrDefault(a => a.Id == id);
        }
        public void InsertRes(CreateEditResVM model)
        {
            var res = new MstResource
            {
                ResourceName = model.Name,
                Status = model.Status,
                ResourceIcon = model.Icon,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            };
            _context.MstResources.Add(res);
            _context.SaveChanges();
            foreach (var item in model.code)
            {
                if (item.ID > 0)
                {
                    UpdateResCod(item, model.ID);
                }
                else
                {
                    InsertResCod(item, model.ID);
                }
            }
        }

        public void UpdateRes(CreateEditResVM model)
        {
            var res = GetRes(model.ID);
            if (res != null)
            {
                res.ResourceName = model.Name;
                res.Status = model.Status;
                res.ResourceIcon = model.Icon;
                res.UpdatedBy = 2;
                res.UpdatedDate = DateTime.Now;
                _context.SaveChanges();
            }
            foreach (var item in model.code)
            {
                if (item.ID > 0)
                {
                    UpdateResCod(item, model.ID);
                }
                else
                {
                    InsertResCod(item, model.ID);
                }
            }
        }

        public void DeleteRes(int id)
        {
            var res = GetRes(id);
            if (res != null)
            {
                res.DeletedBy = 3;
                res.DeletedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public IndexResVM GetIndexBC(int page)
        {
            var indexRes = new IndexResVM();
            var listRes = from a in AllResource()
                          select new ListResVM
                          {
                              ID = a.ResourceId,
                              Name = a.ResourceName,
                              Status = a.Status,
                              Icon = a.ResourceIcon,
                          };
            if (page > 0)
            {
                listRes = listRes.Skip((page - 1) * 10).Take(10);
            }
            indexRes.list = listRes.ToList();
            return indexRes;
        }
        public CreateEditResVM GetOne(int id)
        {
            var getOne = GetRes(id);
            var res = new CreateEditResVM();
            res.Status = getOne.Status;
            res.Icon = getOne.ResourceIcon;
            res.Name = getOne.ResourceName;
            res.ID = id;
            res.code = GetListResCode(res.ID);
            return res;
        }
        public List<CreateEditResCodMV> GetListResCode(int resourceId)
        {
            var list = from a in AllResourceCode()
                       where a.ResourceId == resourceId
                       select new CreateEditResCodMV
                       {
                           ID = a.Id,
                           IsActive = a.Status,
                           ResourceCode = a.ResourceCode
                       };
            return list.ToList();
        }
        public void InsertResCod(CreateEditResCodMV model, int resourceId)
        {
            var resCod = new MstResourceCode
            {
                ResourceId = resourceId,
                ResourceCode = model.ResourceCode,
                Status = model.IsActive,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            };
            _context.MstResourceCodes.Add(resCod);
            _context.SaveChanges();
        }
        public void UpdateResCod(CreateEditResCodMV model, int resourceId)
        {
            var resCod = GetResCod(model.ID);
            if (resCod != null)
            {
                resCod.ResourceCode = model.ResourceCode;
                resCod.ResourceId = resourceId;
                resCod.Status = model.IsActive;
                resCod.UpdatedBy = 2;
                resCod.UpdatedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void DeleteResCod(int id)
        {
            var resCod = GetResCod(id);
            if (resCod != null)
            {
                resCod.DeletedBy = 3;
                resCod.DeletedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

    }
}
