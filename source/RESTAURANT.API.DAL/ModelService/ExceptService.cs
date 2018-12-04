using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace RESTAURANT.API.DAL.Services
{
    public class ExceptService : GenericService<Except>
    {

        public List<Except> GetList(Guid ofs)
        {
            List<Except> listView = null;
            try
            {
                listView = _db.Except.Where(x=>x.OfsKey==ofs).ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return listView;
        }

        public int? Insert(Except item, string userName=null)
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
        public bool Update(Except item,string userName=null)
        {
            bool rs = false;
            try
            {
                if (item == null) return false;
                var obj = ParseToItem(item, userName);
                obj = (Except)MapData(obj, item);
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
        public bool Delete(Except item, string userName)
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
        internal Except ParseToItem(Except dto, string userName)
        {
            if (dto == null) return null;
            Except entity = null;
            if (dto.ID != 0)
            {
                entity = _db.Except.SingleOrDefault(x => x.ID == dto.ID);
                if (entity == null)
                {
                    return dto;
                }
            }
            return entity;
        }

        public bool  Submit(List<Except> lst, string userName)
        {
            foreach(var item in lst)
            {
                if (!_db.Except.Any(x => x.ID == item.ID))
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
