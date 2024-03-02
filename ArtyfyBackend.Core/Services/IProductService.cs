using ArtyfyBackend.Core.Models.Product;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Core.Services
{
    public interface IProductService : IGenericService<Product, ProductModel>
    {
    }
}
