using GestionEvenement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GestionEvenement.DataAccess.Repositories.Contracts
{
    public interface IBaseRepository<TEntity, TEntityKey> where TEntity : BaseEntity<TEntityKey>
    {
        TEntity GetById(TEntityKey id);

        IEnumerable<TEntity> GetAll();

        IQueryable<TEntity> Get( Expression<Func<TEntity, bool>> fliter = null,
           string includeProperties = null);

        void Add(TEntity entity);

        void Delete(TEntityKey id);
    }
}
