using FileTestWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace FileTestWebApi.DataAccess
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

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? table.ToList() : table.Where(filter).ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
            _context.Entry(obj).State = EntityState.Added;
            this.Save();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return table.SingleOrDefault(filter);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            this.Save();
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            _context.Entry(existing).State = EntityState.Deleted;
            this.Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
