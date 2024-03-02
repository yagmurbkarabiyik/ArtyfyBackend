using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Core.Responses;
using ArtyfyBackend.Core.Services;
using ArtyfyBackend.Core.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ArtyfyBackend.Bll.Services
{
    public class GenericService<TEntity, TModel> : IGenericService<TEntity, TModel> where TEntity : class where TModel : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<TModel>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return Response<TModel>.Fail("Id not found", 404, true);
            }

            return Response<TModel>.Success(_mapper.Map<TModel>(entity), 200);
        }

        public async Task<Response<IEnumerable<TModel>>> GetAllAsync()
        {
            var entities = await _repository.GetAll().ToListAsync();

            return Response<IEnumerable<TModel>>.Success(_mapper.Map<IEnumerable<TModel>>(entities), 200);
        }

        public async Task<Response<IQueryable<TModel>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _repository.Where(predicate);

            return Response<IQueryable<TModel>>.Success(_mapper.Map<IQueryable<TModel>>(await list.ToListAsync()), 200);
        }

        public async Task<Response<TModel>>  AddAsync(TModel entity)
        {
            var newEntity = _mapper.Map<TEntity>(entity);

            await _repository.AddAsync(newEntity);

            await _unitOfWork.CommitAsync();

            var newModel = _mapper.Map<TModel>(newEntity);

            return Response<TModel>.Success(newModel, 200);
        }

        public async Task<Response<NoDataModel>> RemoveAsync(int id)
        {
            var isExistEntity = await _repository.GetByIdAsync(id);

            if (isExistEntity == null)
            {
                return Response<NoDataModel>.Fail("Id not found", 404, true);
            }

            _repository.Remove(isExistEntity);

            await _unitOfWork.CommitAsync();

            return Response<NoDataModel>.Success(200);
        }

        public async Task<Response<NoDataModel>> UpdateAsync(TModel entity, int id)
        {
            var isExistEntity = await _repository.GetByIdAsync(id);

            if (isExistEntity == null)
            {
                return Response<NoDataModel>.Fail("Id not found", 404, true);
            }

            var updatedEntity = _mapper.Map<TEntity>(entity);

            _repository.Update(updatedEntity);

            await _unitOfWork.CommitAsync();

            return Response<NoDataModel>.Success(200);
        }
    }
}