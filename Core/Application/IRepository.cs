using Blogpost.Core.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blogpost.Core.Application
{
    public interface IRepository<T> where T : DomainBase
    {
        T Get(long id);
        T Get(Expression<Func<T, bool>> expression);
        void Save(T entity);
        void Delete(T domain);
        void Delete(long id);
        void Delete(Expression<Func<T, bool>> expression);
        IEnumerable<T> Fetch(Expression<Func<T, bool>> expression);
        IQueryable<T> Table { get; }
        IQueryable<T> List { get; }
        IQueryable<T> IncludeMultiple(params Expression<Func<T, object>>[] includes);
    }
}
