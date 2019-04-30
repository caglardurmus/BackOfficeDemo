using CaglarDurmus.BackOffice.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglarDurmus.BackOffice.DataAccess.Concrete.EntityFramework
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private NorthwindContext _context = null;
        private DbSet<T> table = null;

        public GenericRepository()
        {
            this._context = new NorthwindContext();
            table = _context.Set<T>();
        }

        public GenericRepository(NorthwindContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
