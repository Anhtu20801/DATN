using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Infrastructure
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? include = null);
        T GetSingleById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void Delete(T entity);
        void DeleteMulti(Expression<Func<T, bool>> where);
    }
}
