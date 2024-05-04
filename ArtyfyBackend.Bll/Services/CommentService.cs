using ArtyfyBackend.Core.IServices;
using ArtyfyBackend.Core.Models.Comment;
using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Core.Responses;
using ArtyfyBackend.Core.UnitOfWork;
using ArtyfyBackend.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ArtyfyBackend.Bll.Services
{
    public class CommentService : GenericService<Comment, CommentModel>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IGenericRepository<Comment> repository, IMapper mapper,
            ICommentRepository commentRepository) : base(repository, mapper, unitOfWork)
        {
            _commentRepository = commentRepository;
        }
    }
}
//todo createdDate problem