using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UlakNot.Core;

namespace UlakNot.DataLayer.EntityFramework
{
    public class Repository<T> : Singleton, IDataAccess<T> where T : class
    {
        private DbSet<T> dbset;

        public Repository()
        {
            dbset = context.Set<T>();
        }

        public List<T> List()
        {
            return dbset.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public int Insert(T obj)
        {
            dbset.Add(obj);
            return Save();
        }

        public int Update(T obj)
        {
            return Save();
        }

        public int Delete(T obj)
        {
            dbset.Remove(obj);
            return Save();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return dbset.FirstOrDefault(where);
        }
    }
}