using ArtyfyBackend.Core.Models.Category;
using ArtyfyBackend.Core.Models.Product;
using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Core.Services;
using ArtyfyBackend.Core.UnitOfWork;
using ArtyfyBackend.Domain.Entities;
using AutoMapper;

namespace ArtyfyBackend.Bll.Services
{
    public class ProductService : GenericService<Product, ProductModel>, IProductService
    {
        public ProductService(IGenericRepository<Product> repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork)
        {
        }
    }
}