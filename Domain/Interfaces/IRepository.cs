using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Entities;

namespace Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity GetById(int id);
        IQueryable<TEntity> Query(bool readOnly = false);
        int Save(TEntity entity);
        int Delete(int id);
        int Delete(TEntity entity);
    }
}
