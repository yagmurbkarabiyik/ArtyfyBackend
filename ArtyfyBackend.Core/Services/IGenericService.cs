using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Responses;
using System.Linq.Expressions;

namespace ArtyfyBackend.Core.Services
{
    public interface IGenericService<TEntity, TModel> where TEntity : class where TModel : class
    { 
        // Get the entity by id
        Task<Response<TModel>> GetByIdAsync(int id);

        // Get all entities
        Task<Response<IEnumerable<TModel>>> GetAllAsync();
        // Get entities by expression
        Task<Response<IQueryable<TModel>>> Where(Expression<Func<TEntity, bool>> predicate);
        // Add entity
        Task<Response<TModel>> AddAsync(TModel entity);
        // Remove entity
        Task<Response<NoDataModel>> RemoveAsync(int id);
        //Update entity
        Task<Response<NoDataModel>> UpdateAsync(TModel entity, int id);
    }
}