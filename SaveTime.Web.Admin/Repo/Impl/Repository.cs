using SaveTime.DataAccess;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SaveTime.Web.Admin.Repo.Impl
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _context;
        private DbSet<T> _db;

        public Repository()
        {
            _context = new SaveTimeModel();
            _db = _context.Set<T>();
        }
        public void Create(T item)
        {
            _db.Add(item);
            _context.SaveChanges();
        }

        public void Delete(T item)
        {
            _db.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _db.ToList();
        }

        public T GetEntity(int id)
        {
            return _db.Find(id);
        } 
        public void Update(T item)
        {
            if (item != null)
            {
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}