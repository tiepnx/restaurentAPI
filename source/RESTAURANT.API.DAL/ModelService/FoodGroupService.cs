using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace RESTAURANT.API.DAL.Services
{
    public class FoodGroupService : GenericService<FoodGroup>
    {
        public List<FoodGroup> GetList(Guid ofs)
        {
            List<FoodGroup> listView = null;
            try
            {
                bool flag = _db.Configuration.ProxyCreationEnabled;
                _db.Configuration.ProxyCreationEnabled = false;
                listView = _db.FoodGroup.Where(x=>x.OfsKey == ofs && x.Deleted != true).ToList();
                _db.Configuration.ProxyCreationEnabled = flag;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return listView;
        }

        public int? Insert(FoodGroup item, string userName=null)
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
        public bool Update(FoodGroup item,string userName=null)
        {
            bool rs = false;
            try
            {
                if (item == null) return false;
                var obj = ParseToItem(item, userName);
                obj = (FoodGroup)MapData(obj, item);
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
        public bool Delete(FoodGroup item, string userName)
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
        internal FoodGroup ParseToItem(FoodGroup dto, string userName)
        {
            if (dto == null) return null;
            FoodGroup entity = null;
            if (dto.ID != 0)
            {
                entity = _db.FoodGroup.SingleOrDefault(x => x.ID == dto.ID);
                if (entity == null)
                {
                    return dto;
                }
            }
            return entity;
        }
    }
}
