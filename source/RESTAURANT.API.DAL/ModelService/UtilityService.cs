using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace RESTAURANT.API.DAL.Services
{
    public class UtilityService : GenericService<Utility>
    {

        public List<Utility> GetList(Guid ofs)
        {
            List<Utility> items = null;
            try
            {
                items = _db.Utility.Where(x=>x.OfsKey==ofs).ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return items;
        }

        public int? Insert(Utility item, string userName=null)
        {
            int? id = null;
            try
            {
                AddUserProperty(ref item, userName);
                SaveChanges();
                id = item.ID;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return id;
        }
        public bool Update(Utility item,string userName=null)
        {
            bool rs = false;
            try
            {
                if (item == null) return false;
                var obj = ParseToItem(item, userName);
                obj = (Utility)MapData(obj, item);
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
        public bool Delete(Utility item, string userName)
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
        internal Utility ParseToItem(Utility dto, string userName)
        {
            if (dto == null) return null;
            Utility entity = null;
            if (dto.ID != 0)
            {
                entity = _db.Utility.SingleOrDefault(x => x.ID == dto.ID);
                if (entity == null)
                {
                    return dto;
                }
            }
            return entity;
        }
        
    }
}
