﻿using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Models.Product;
using ArtyfyBackend.Core.Responses;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Core.Services
{
    public interface IProductService 
    {
        Task<Response<NoDataModel>> Create(ProductModel model);
    }
}