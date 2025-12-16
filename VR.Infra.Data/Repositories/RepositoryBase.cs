using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VR.Domain.Interfaces;
using VR.Infra.Data.Context;

namespace VR.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly VRContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(VRContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public void Add(TEntity obj)
        {
            _dbSet.Add(obj);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Remove(TEntity obj)
        {
            _dbSet.Remove(obj);
        }

        public void Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
