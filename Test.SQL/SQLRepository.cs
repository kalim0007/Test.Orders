using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core;
using Test.Core.Contracts;

namespace Test.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext context;
        internal DbSet<T> dbset;

        public SQLRepository(DataContext context)
        {
            this.context = context;
            dbset = context.Set<T>();
        }
        public IQueryable<T> Collection()
        {
            return dbset;
        }

        public void Comit()
        {
            context.SaveChanges();
        }

        public T Find(string Id)
        {
            return dbset.Find(Id);
        }

        public void Insert(T item)
        {
            dbset.Add(item);
        }

        public void Remove(string Id)
        {
            var item = Find(Id);
            if (context.Entry(item).State == EntityState.Detached)
            {
                dbset.Attach(item);
            }
            dbset.Remove(item);
        }

        public void Update(T item)
        {
            dbset.Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
