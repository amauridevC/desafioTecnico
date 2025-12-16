using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VR.Application.Interfaces;
using VR.Domain.Interfaces;

namespace VR.Application.Services
{
    public class AppServiceBase<TEntity> : IAppServiceBase<TEntity> where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        public AppServiceBase(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public virtual void Add(TEntity entity)
        {
            _repository.Add(entity);
            _unitOfWork.Commit();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Find(predicate);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual TEntity GetById(int id)
        {
           return _repository.GetById(id);
        }

        public virtual void Remove(TEntity entity, string usuario = "Sistema")
        {
            _repository.Remove(entity);
            _unitOfWork.Commit(usuario);
        }

        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
            _unitOfWork.Commit();
        }
    }
}
