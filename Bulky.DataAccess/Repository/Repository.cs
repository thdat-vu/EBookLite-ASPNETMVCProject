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
            _db.Products.Include(u => u.Category).Include(u => u.CategoryId);
        }

        public void Add(T entity)
        {
            //throw new NotImplementedException();
            //_db.Categories.Add this is what we often do, but we're using generic + we cannot add T as a class.
            //solution: create an internal DbSet with generic type and call that as a dbset.

            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            //if (tracked)
            //{

            //    //step1: create a dbset
            //    IQueryable<T> query = dbSet;
            //    //step2: filer this dbset
            //    query = query.Where(filter);
            //    if (!string.IsNullOrEmpty(includeProperties))
            //    {
            //        foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //        {
            //            query = query.Include(includeProp);
            //        }
            //    }
            //    return query.FirstOrDefault();
            //}
            //else
            //{
            //    //step1: create a dbset
            //    IQueryable<T> query = dbSet;
            //    //step2: filer this dbset
            //    query = query.Where(filter);
            //    if (!string.IsNullOrEmpty(includeProperties))
            //    {
            //        foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //        {
            //            query = query.Include(includeProp);
            //        }
            //    }
            //    return query.FirstOrDefault();
            //}

            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        //Category, CoverType
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {

            //step1: create a dbset
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

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
