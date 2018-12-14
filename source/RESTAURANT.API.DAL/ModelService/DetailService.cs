using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace RESTAURANT.API.DAL.Services
{
    public class DetailService : GenericService<Detail>
    {

        public List<Detail> GetList()
        {
            List<Detail> listView = null;
            try
            {
                listView = _db.Details.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return listView;
        }

        public int? Insert(Detail item, string userName=null)
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
        public bool Update(Detail item,string userName=null)
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
                Detail dt = ParseToItem(ofs, rowId, userName);
                dt.Deleted = true;
                base.Insert(ref dt, userName);
                _db.Entry(dt).State = EntityState.Modified;
                SaveChanges();
                rs = true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return rs;
        }
        internal Detail ParseToItem(Guid ofs, Guid rowId, string userName)
        {
            if (rowId == Guid.Empty) return null;
            Detail entity = _db.Details.SingleOrDefault(x => x.OfsKey == ofs && x.RowGuid == rowId);
            return entity;
        }
    }
}
