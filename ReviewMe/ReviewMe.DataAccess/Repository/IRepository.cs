using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace ReviewMe.DataAccess.Repository
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> List { get; }

        List<T> GetAll();

        T SaveOrUpdate(T entity);

        T Find(Int64? id);

        T GetById(Int64 id);

        T Single(Func<T, bool> predicate);

        T First(Func<T, bool> predicate);

        T Add(T entity);

        void Delete(T entity);

        bool Delete(Int64 id);

        void Attach(T entity);

        void SaveChanges();

        void SaveChanges(SaveOptions options);
    }
}