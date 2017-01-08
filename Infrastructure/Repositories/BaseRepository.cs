using System.Data.Entity;
using System.Linq;
using Domain.Interfaces;
using Domain.Models.Entities;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected DbContext _context;
        protected IUnitOfWork _unitOfWork;
        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = unitOfWork.Context;
        }

        public virtual TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual IQueryable<TEntity> Query(bool readOnly = false)
        {
            return readOnly ? _context.Set<TEntity>().AsNoTracking() : _context.Set<TEntity>();
        }

        public virtual int Save(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                if (entity.Id == 0)
                    _context.Set<TEntity>().Add(entity);
                else
                {
                    _context.Set<TEntity>().Attach(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                }
            }
            _context.SaveChanges();
            return entity.Id;
        }

        public virtual int Delete(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            _context.Set<TEntity>().Remove(entity);
            int writtenEntries =_context.SaveChanges();
            return writtenEntries > 0 ? id : 0;
        }

        public virtual int Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _context.Set<TEntity>().Attach(entity);
            _context.Set<TEntity>().Remove(entity);
            int writtenEntries = _context.SaveChanges();
            return writtenEntries > 0 ? entity.Id : 0;
        }
    }
}
