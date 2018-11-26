using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace RESTAURANT.API.DAL.Services
{
    public class GenericService<T> : IDisposable, IGenericService<T> where T : class
    {
        protected RestaurentModel _db { get; set; }
        protected DbSet<T> _table = null;

        public GenericService()
        {
            _db = new RestaurentModel();
            _table = _db.Set<T>();
        }

        public GenericService(RestaurentModel db)
        {
            _db = db;
            _table = _db.Set<T>();
        }

        public IEnumerable<T> SelectAll()
        {
            //return _table.AsNoTracking().ToList();
            return _table.ToList();
        }

        public T SelectById(object id)
        {
            try
            {
                return _table.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public void Insert(T obj)
        {
            PropertyInfo info = obj.GetType().GetProperty("CreateDate");
            if (info != null)
                info.SetValue(obj, DateTime.Now);
            _table.Add(obj);
        }

        public void AddUserProperty(ref T obj, string userName)
        {
            PropertyInfo info = obj.GetType().GetProperty("ID");
            int id = (int)info.GetValue(obj, null);
            if (id > 0)
            {
                info = obj.GetType().GetProperty("ModifiedBy");
                if (info != null)
                    info.SetValue(obj, userName);
            }
            else
            {
                info = obj.GetType().GetProperty("CreatedBy");
                if (info != null)
                    info.SetValue(obj, userName);
            }
            if (id <= 0)
                _table.Add(obj);
        }
        public object MapData(object destination, object source)
        {
            PropertyInfo s = source.GetType().GetProperty("Title");
            PropertyInfo d = destination.GetType().GetProperty("Title");
            d.SetValue(destination, s.GetValue(source, null));

            s = source.GetType().GetProperty("Note");
            d = destination.GetType().GetProperty("Note");
            d.SetValue(destination, s.GetValue(source, null));
            return destination;
        }
        public void Insert(ref T obj, string userName)
        {
            PropertyInfo info = obj.GetType().GetProperty("ID");

            if (info != null && (int)info.GetValue(obj, null) > 0)
            {
                info = obj.GetType().GetProperty("ModifiedBy");
                if (info != null)
                    info.SetValue(obj, userName);
            }
            else
            {
                info = obj.GetType().GetProperty("CreatedBy");
                if (info != null)
                    info.SetValue(obj, userName);
            }

            _table.Add(obj);
        }
        public void Update(T obj)
        {
            PropertyInfo info = obj.GetType().GetProperty("UpdateDate");
            if (info != null)
                info.SetValue(obj, DateTime.Now);

            _table.Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
        }

        public void Update(T obj, string userName)
        {
            PropertyInfo info = obj.GetType().GetProperty("UpdateDate");
            if (info != null)
                info.SetValue(obj, DateTime.Now);

            info = obj.GetType().GetProperty("UserUpdate");
            if (info != null)
                info.SetValue(obj, userName);

            _table.Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
        }
       
        public void DeleteMark(object id, string userName)
        {
            T obj = _table.Find(id);
            if (obj == null)
                return;

            PropertyInfo info = obj.GetType().GetProperty("UpdateDate");
            if (info != null)
                info.SetValue(obj, DateTime.Now);

            info = obj.GetType().GetProperty("UserUpdate");
            if (info != null)
                info.SetValue(obj, userName);

            info = obj.GetType().GetProperty("Deleted");
            if (info != null)
                info.SetValue(obj, true);

            _table.Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
        }

        public void Delete(object id, string userName)
        {
            Delete(id);
            //Write log here
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
        
        public void Dispose()
        {
            this._db.Dispose();
        }
    }
}
