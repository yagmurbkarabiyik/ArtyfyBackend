using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Models.Product;
using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Core.Responses;
using ArtyfyBackend.Core.Services;
using ArtyfyBackend.Core.UnitOfWork;
using ArtyfyBackend.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ArtyfyBackend.Bll.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserApp> _userManager;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<UserApp> userManager, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response<NoDataModel>> Create(ProductModel model)
        {
            var product = new Product
            {
                ProductName = model.ProductName,
                ProductDescription = model.ProductDescription,
                Stock = model.Stock,
                Price = model.Price,
                IsSellable = model.IsSellable,
                UserAppId = model.UserAppId,
                CategoryId = (int)model.CategoryId,
                
            };
            
            await _productRepository.AddAsync(product);

            await _unitOfWork.CommitAsync();

            return Response<NoDataModel>.Success("Yeni ürün oluşturuldu!", 200);
        }

        public async Task<Response<List<ProductModel>>> ListSellableProduct()
        {
            var sellableProducts = await _productRepository.GetSellableProductsAsync();

            var products = _mapper.Map<List<ProductModel>>(sellableProducts);

            return Response<List<ProductModel>>.Success(products, 200);
        }
    }
}