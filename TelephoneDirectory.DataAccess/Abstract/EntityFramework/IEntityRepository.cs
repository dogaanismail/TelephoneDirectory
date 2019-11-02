using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TelephoneDirectory.DataAccess.Abstract.EntityFramework
{
    public interface IEntityRepository<T> where T : class, new() 
    {
        List<T> GetList();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T FirstOrDefault();

        List<T> Query(Expression<Func<T, bool>> where); 
        List<T> GetAllLazyLoad(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children); 
        List<T> TolistInclude(params Expression<Func<T, object>>[] children);
        T GetFirstOrDefaultInclude(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children);
        List<T> Include(string includeTable);
        T Find(Expression<Func<T, bool>> filter);
    }
}
