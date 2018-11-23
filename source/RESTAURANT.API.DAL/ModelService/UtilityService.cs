using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace RESTAURANT.API.DAL.Services
{
    public class UtilityService : GenericService<Utility>
    {

        public List<Utility> GetList()
        {
            List<Utility> listView = null;
            try
            {
                listView = _db.Utility.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return listView;
        }

        public int? Insert(Utility item, string userName=null)
        {
            int? id = null;
            try
            {
                Insert(ref item, userName);
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
                if (item == null)
                    return false;
                var obj = ParseToItem(item, userName);
                    SaveChanges();
                    rs = true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return rs;
        }
        public bool Delete(int itemId, string userName)
        {
            bool rs = false;
            try
            {
                Delete(itemId, userName);
                SaveChanges();
                rs = true;
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

        public bool  Submit(List<Utility> lst, string userName)
        {
            foreach(var item in lst)
            {
                if (!_db.Utility.Any(x => x.ID == item.ID))
                {
                    var ret = item;
                    Insert(ref ret, userName);
                }
            }
            SaveChanges();
            return true;
        }

        public bool ConfirmImportData(string userName)
        {
            try
            {
                var sUserName = new SqlParameter("@userName", userName);
                string strSql = "EXEC storename @userName";
                _db.Database.ExecuteSqlCommand(strSql, sUserName);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
