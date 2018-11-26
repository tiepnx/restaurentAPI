using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace RESTAURANT.API.DAL.Services
{
    public class TableService : GenericService<Table>
    {

        public List<Table> GetList()
        {
            List<Table> items = null;
            try
            {
                items = _db.Table.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return items;
        }

        public int? Insert(Table item, string userName=null)
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
        public bool Update(Table item,string userName=null)
        {
            bool rs = false;
            try
            {
                if (item == null) return false;
                var obj = ParseToItem(item, userName);
                obj = (Table)MapData(obj, item);
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
        public bool Delete(Table item, string userName)
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
        internal Table ParseToItem(Table dto, string userName)
        {
            if (dto == null) return null;
            Table entity = null;
            if (dto.ID != 0)
            {
                entity = _db.Table.SingleOrDefault(x => x.ID == dto.ID);
                if (entity == null)
                {
                    return dto;
                }
            }
            return entity;
        }
    }
}
