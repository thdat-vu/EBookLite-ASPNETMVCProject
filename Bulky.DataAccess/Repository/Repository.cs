using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        //ctor
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //add a dbSet
            this.dbSet = _db.Set<T>(); //~ similar to _db.Categories==dbSet
        }

        public void Add(T entity)
        {
            //throw new NotImplementedException();
            //_db.Categories.Add this is what we often do, but we're using generic + we cannot add T as a class.
            //solution: create an internal DbSet with generic type and call that as a dbset.

            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)  
        {
            
            //step1: create a dbset
            IQueryable<T> query = dbSet;
            //step2: filer this dbset
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
           
            //step1: create a dbset
            IQueryable<T> query = dbSet;
            //step2: convert dbSet to a List.
            return query.ToList();
        }

        public void Remove(T entity)
        {
            //just remove an entity
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
           //remove based on range
           dbSet.RemoveRange(entity);
        }
        //why we dont have Update method in repository????
        //Bcause updating an entity is very various way. So keep them in controller or somewhere else.
        //public void Update(T entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
