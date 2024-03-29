﻿using System.Linq.Expressions;

namespace ArtyfyBackend.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        //Get the entity by id
        Task<T> GetByIdAsync(int id);

        //Get all entities
        IQueryable<T> GetAll();
        //Get entities by expression
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        IQueryable<T> Queryable();

        //Check if entity exists by expression
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        //Add entity
        Task AddAsync(T entity);

        //Add entities
        Task AddRangeAsync(IEnumerable<T> entities);

        //Update entity
        void Update(T entity);

        //Remove entity
        void Remove(T entity);

        //Remove entities
        void RemoveRange(IEnumerable<T> entities);
    }
}