using System.Collections.Generic;

namespace RESTAURANT.API.DAL.Services
{
    public interface IGenericService<T> where T : class
    {
        IEnumerable<T> SelectAll();
        T SelectById(object id);
        void Insert(T obj);
        void Insert(ref T obj, string userName);
        void Update(T obj);
        void Update(T obj, string userName);
        void Delete(object id);
        void Delete(object id, string userName);
        void DeleteMark(object id, string userName);
        void SaveChanges();
    }
}
