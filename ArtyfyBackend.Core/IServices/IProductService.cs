using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Models.Product;
using ArtyfyBackend.Core.Responses;

namespace ArtyfyBackend.Core.Services
{
    public interface IProductService
    {
        Task<Response<NoDataModel>> Create(ProductModel model);
        Task<Response<List<ProductModel>>> ListSellableProduct();
    }
}