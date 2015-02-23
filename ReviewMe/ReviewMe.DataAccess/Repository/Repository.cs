using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using ReviewMe.Model;

namespace ReviewMe.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private EntityContext _context;

        public Repository(EntityContext context)
        {
            _context = context;
        }

        protected DbContext DbContext { get; set; }

        public virtual IEnumerable<T> List
        {
            get { return _context.Set<T>().AsQueryable(); }
        }

        public virtual List<T> GetAll()
        {
            try
            {
                return _context.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public T SaveOrUpdate(T entity)
        {
            try
            {
                //_context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Find(long? id)
        {
            return _context.Set<T>().Find(id);
        }

        public T GetById(long id)
        {
            try
            {
                return _context.Set<T>().Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Single(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public T First(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public T Add(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                var response = _context.Set<T>().Add(entity);
                if (response != null)
                {
                    _context.SaveChanges();
                    return response;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(T entity)
        {
            (((ISoftDelete) entity).IsActive) = false;
            _context.SaveChanges();
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void SaveChanges(SaveOptions options)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Int64 id)
        {
            try
            {
                T model = _context.Set<T>().Find(id);
                if (model != null)
                    (((ISoftDelete) model).IsActive) = false;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}