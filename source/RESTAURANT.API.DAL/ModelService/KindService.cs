using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace RESTAURANT.API.DAL.Services
{
    public class KindService : GenericService<Kind>
    {

        public List<Kind> GetList(Guid ofs)
        {
            List<Kind> items = null;
            try
            {
                items = _db.Kind.Where(x=>x.OfsKey == ofs).ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return items;
        }

        public int? Insert(Kind item, string userName=null)
        {
            int? id = null;
            try
            {
                AddUserProperty(ref item, userName);
                //Insert(ref item, userName);
                SaveChanges();
                id = item.ID;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return id;
        }
        public bool Update(Kind item,string userName=null)
        {
            bool rs = false;
            try
            {
                if (item == null) return false;
                var obj = ParseToItem(item, userName);
                obj = (Kind)MapData(obj, item);
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
        public bool Delete(Kind item, string userName)
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
        internal Kind ParseToItem(Kind dto, string userName)
        {
            if (dto == null) return null;
            Kind entity = null;
            if (dto.ID != 0)
            {
                entity = _db.Kind.SingleOrDefault(x => x.ID == dto.ID);
                if (entity == null)
                {
                    return dto;
                }
            }
            return entity;
        }

        public bool  Submit(List<Kind> lst, string userName)
        {
            foreach(var item in lst)
            {
                if (!_db.Kind.Any(x => x.ID == item.ID))
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
