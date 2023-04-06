using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace QLSV.Data.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private StudentDBContext _context = null;
        private DbSet<T> dbSet = null;

        public GenericRepository(StudentDBContext _context)
        {
            this._context = _context;
            this.dbSet = _context.Set<T>();
        }
        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includes = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includes != null)
            {
                foreach (var include in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include);
                }
            }
            return query.ToList();
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry<T>(entity).State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            var entity = dbSet.Find(id);
            if (entity == null) return;
            dbSet.Remove(entity);
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> entities = dbSet.Where<T>(where).AsEnumerable();
            foreach (T entity in entities)
                dbSet.Remove(entity);
        }

        public virtual T GetSingleById(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void Save()
        {
            throw new NotImplementedException();
        }
    }
}