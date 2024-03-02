using ArtyfyBackend.Core.Models.Category;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Core.Services
{
    public interface ICategoryService : IGenericService<Category, CategoryModel>
    {
    }
}
