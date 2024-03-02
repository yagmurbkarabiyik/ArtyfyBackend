using ArtyfyBackend.Core.Models.Category;
using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Core.Services;
using ArtyfyBackend.Core.UnitOfWork;
using ArtyfyBackend.Domain.Entities;
using AutoMapper;

namespace ArtyfyBackend.Bll.Services
{
    public class CategoryService : GenericService<Category, CategoryModel>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IUnitOfWork unitOfWork, IGenericRepository<Category> repository, IMapper mapper, ICategoryRepository categoryRepository)
            : base(repository, mapper, unitOfWork)
        {
            _categoryRepository = categoryRepository;
        }
    }
}