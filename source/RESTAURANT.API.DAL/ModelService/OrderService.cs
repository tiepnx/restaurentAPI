using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace RESTAURANT.API.DAL.Services
{
    public class OrderService : GenericService<Order>
    {

        public List<Order> GetList(Guid ofs)
        {
            List<Order> listView = null;
            try
            {
                bool flag = _db.Configuration.ProxyCreationEnabled;
                _db.Configuration.ProxyCreationEnabled = false;
                listView = _db.Orders.Where(x=>x.OfsKey==ofs && x.Deleted != true).ToList();
                _db.Configuration.ProxyCreationEnabled = flag;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return listView;
        }
        public List<Order> GetList(Guid ofs, DateTime from, DateTime to)
        {
            List<Order> listView = null;
            try
            {
                bool flag = _db.Configuration.ProxyCreationEnabled;
                _db.Configuration.ProxyCreationEnabled = false;
                listView = _db.Orders.Where(x => x.OfsKey == ofs && x.Deleted != true && x.Created > from && x.Created < to).ToList();
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
                itm = _db.Orders.Where(x=>x.Deleted != true && x.RowGuid == rowId).Include(x => x.Details).SingleOrDefault();
                itm.Details.RemoveAll(x => x.Deleted == true);
                _db.Configuration.ProxyCreationEnabled = flag;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return itm;
        }
        public Order Insert(Order item, string userName=null)
        {
            try
            {
                var from = DateTime.Now.Date;
                var to = DateTime.Now.AddDays(1).Date;
                var orderToday = GetList(item.OfsKey.Value, from, to);

                bool flag = _db.Configuration.ProxyCreationEnabled;
                _db.Configuration.ProxyCreationEnabled = false;
                item.Title = String.Format("OFS{0}/{1:00000}", from.ToString("yyyyMMdd"), orderToday.Count+1);
                AddUserProperty(ref item, userName);
                _db.Entry(item.Table).State = EntityState.Unchanged;
                _db.Entry(item.Status).State = EntityState.Unchanged;
                //_db.Entry(item.Details).State = EntityState.Unchanged;               
                //https://itq.nl/code-first-entity-framework-additional-properties-on-many-to-many-join-tables/
                //List<Detail> details = item.Details.ToList();
                //item.Details.Clear();
                //_db.Entry(item.Details).State = EntityState.Unchanged;
                foreach(var dt in item.Details)
                {
                    dt.RowGuid = Guid.NewGuid();
                    dt.OfsKey = item.OfsKey;
                    dt.Created = DateTime.Now;
                    dt.CreatedBy = userName;
                }
                SaveChanges();             
                _db.Configuration.ProxyCreationEnabled = flag;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return item;
        }
        public bool Update(Order item,string userName=null)
        {
            bool rs = false;
            try
            {
                if (item == null)
                    return false;
                var obj = ParseToItem(item.OfsKey.Value, item.RowGuid.Value, userName);
                    SaveChanges();
                    rs = true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return rs;
        }
        public bool Delete(Guid ofs, Guid rowId, string userName)
        {
            bool rs = false;
            try
            {
                Order od = ParseToItem(ofs, rowId, userName);
                od.Deleted = true;
                base.Insert(ref od, userName);
                _db.Entry(od).State = EntityState.Modified;
                SaveChanges();
                rs = true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return rs;
        }
        internal Order ParseToItem(Guid ofs, Guid rowId, string userName)
        {
            Order entity = _db.Orders.SingleOrDefault(x => x.OfsKey == ofs && x.RowGuid == rowId);
            return entity;
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
