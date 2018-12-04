using System;
using System.Collections.Generic;
using System.Linq;

namespace RESTAURANT.API.DAL.Services
{
    public class OFSService : GenericService<OFS>
    {
        public List<OFS> Gets()
        {
            List<OFS> items = null;
            try
            {
                items = _db.OFS.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return items;
        }
        public OFS Get(Guid OFSKey)
        {
            OFS item = null;
            try
            {
                item = _db.OFS.Where(x => x.RowGuid == OFSKey).Single();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return item;
        }
        public int? Insert(OFS item, string userName=null)
        {
            int? id = null;
            try
            {
                Guid tmp = item.RowGuid.Value;
                AddUserProperty(ref item, userName);
                item.RowGuid = tmp;
                SaveChanges();
                id = item.ID;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return id;
        }
        public bool Update(OFS item,string userName=null)
        {
            bool rs = false;
            try
            {
                if (item == null) return false;
                var obj = ParseToItem(item, userName);
                obj = (OFS)MapData(obj, item);
                AddUserProperty(ref obj, userName);
                SaveChanges();
                rs = true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return rs;
        }
        public bool Delete(OFS item, string userName)
        {
            bool rs = false;
            try
            {
                if (item == null) return false;
                var obj = ParseToItem(item, userName);
                obj.Deleted = true;
                AddUserProperty(ref obj, userName);
                SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return rs;
        }
        internal OFS ParseToItem(OFS dto, string userName)
        {
            if (dto == null) return null;
            OFS entity = null;
            if (dto.ID != 0)
            {
                entity = _db.OFS.SingleOrDefault(x => x.ID == dto.ID);
                if (entity == null)
                {
                    return dto;
                }
            }
            return entity;
        }


       
    }
}
