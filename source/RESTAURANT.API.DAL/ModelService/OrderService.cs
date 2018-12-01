using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace RESTAURANT.API.DAL.Services
{
    public class OrderService : GenericService<Order>
    {

        public List<Order> GetList()
        {
            List<Order> listView = null;
            try
            {
                bool flag = _db.Configuration.ProxyCreationEnabled;
                _db.Configuration.ProxyCreationEnabled = false;
                //listView = _db.Orders.Include(x => x.Details).ToList();
                listView = _db.Orders.ToList();
                _db.Configuration.ProxyCreationEnabled = flag;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return listView;
        }
        public Order Get(Guid rowId)
        {
            Order itm = null;
            try
            {
                bool flag = _db.Configuration.ProxyCreationEnabled;
                _db.Configuration.ProxyCreationEnabled = false;
                itm = _db.Orders.Where(x=>x.RowGuid == rowId).Include(x => x.Details).Single();                
                _db.Configuration.ProxyCreationEnabled = flag;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return itm;
        }
        public Guid? Insert(Order item, string userName=null)
        {
            Guid? rowGuid = null;
            try
            {
                AddUserProperty(ref item, userName);
                _db.Entry(item.Table).State = EntityState.Unchanged;
                _db.Entry(item.Status).State = EntityState.Unchanged;
                //_db.Entry(item.Details).State = EntityState.Unchanged;
                SaveChanges();
                rowGuid = item.RowGuid;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return rowGuid;
        }
        public bool Update(Order item,string userName=null)
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
        internal Order ParseToItem(Order dto, string userName)
        {
            if (dto == null) return null;
            Order entity = null;
            if (dto.ID != 0)
            {
                entity = _db.Orders.SingleOrDefault(x => x.ID == dto.ID);
                if (entity == null)
                {
                    return dto;
                }
            }
            return entity;
        }

        public bool  Submit(List<Order> lst, string userName)
        {
            foreach(var item in lst)
            {
                if (!_db.Orders.Any(x => x.ID == item.ID))
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
