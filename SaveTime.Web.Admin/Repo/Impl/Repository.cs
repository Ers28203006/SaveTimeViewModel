using SaveTime.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveTime.Web.Admin.Repo.Impl
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private SaveTimeModel context = new SaveTimeModel();
        public void Create(T item)
        {
            context.Set<T>().Add(item);
            context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
    }
}