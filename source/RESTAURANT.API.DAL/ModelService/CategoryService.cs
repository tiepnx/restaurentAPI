using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace RESTAURANT.API.DAL.Services
{
    public class CategoryService : GenericService<Category>
    {

        public List<Category> GetList()
        {
            List<Category> listView = null;
            try
            {
                listView = _db.Category.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return listView;
        }

        public int? Insert(Category item, string userName=null)
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
        public bool Update(Category item,string userName=null)
        {
            bool rs = false;
            try
            {
                if (item == null) return false;
                var obj = ParseToItem(item, userName);
                obj = (Category)MapData(obj, item);
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
        public bool Delete(Category item, string userName)
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
        internal Category ParseToItem(Category dto, string userName)
        {
            if (dto == null) return null;
            Category entity = null;
            if (dto.ID != 0)
            {
                entity = _db.Category.SingleOrDefault(x => x.ID == dto.ID);
                if (entity == null)
                {
                    return dto;
                }
            }
            return entity;
        }

        public bool  Submit(List<Category> lst, string userName)
        {
            foreach(var item in lst)
            {
                if (!_db.Category.Any(x => x.ID == item.ID))
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
