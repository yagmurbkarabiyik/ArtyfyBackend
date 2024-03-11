using ArtyfyBackend.Core.Models.Category;
using ArtyfyBackend.Core.Models.Comment;
using ArtyfyBackend.Core.Models.Post;
using ArtyfyBackend.Core.Models.Product;
using ArtyfyBackend.Core.Models.SubComment;
using ArtyfyBackend.Core.Models.UserApp;
using ArtyfyBackend.Domain.Entities;
using AutoMapper;

namespace ArtyfyBackend.Bll.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Comment, CommentModel>().ReverseMap();
            CreateMap<Post, PostModel>().ReverseMap();
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<UserApp, UserAppModel>().ReverseMap();
            CreateMap<SubComment, SubCommentModel>().ReverseMap();
        }
    }
}