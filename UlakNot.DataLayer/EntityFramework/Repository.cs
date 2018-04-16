using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UlakNot.Common;
using UlakNot.Core;
using UlakNot.Entity;

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

        public IQueryable<T> ListQueryable()
        {
            return dbset.AsQueryable<T>();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public int Insert(T obj)
        {
            dbset.Add(obj);

            if (obj is UnBase)
            {
                UnBase b = obj as UnBase;
                DateTime now = DateTime.Now;

                b.CreatedDate = now;
                b.UpdatedDate = now;
                b.UpdatedUserName = App.Common.GetUsername();
            }

            return Save();
        }

        public int Update(T obj)
        {
            if (obj is UnBase)
            {
                UnBase b = new UnBase();
                DateTime now = DateTime.Now;

                b.UpdatedDate = now;
            }
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