﻿using API.Models;
using System.Linq.Expressions;

namespace API.Port.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null,
                                               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                               string includeProperties = "");

        TEntity? GetByID(object id);

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity? entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}