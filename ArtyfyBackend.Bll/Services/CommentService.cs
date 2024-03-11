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
        private readonly ISubCommentService _subCommentService;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IGenericRepository<Comment> repository, IMapper mapper,
            ICommentRepository commentRepository, ISubCommentService subCommentService) : base(repository, mapper, unitOfWork)
        {
            _commentRepository = commentRepository;
            _subCommentService = subCommentService;
        }

        /// <summary>
        /// This method returns comments with sub comments.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response<List<CommentWithSubCommentModel>>> GetCommentsWithSubCommentsAsync()
        {
            List<CommentWithSubCommentModel> commentWithSubCommentsModel = new List<CommentWithSubCommentModel>();
            var subComments = await _commentRepository.GetAll().ToListAsync();

            foreach (var comment in subComments)
            {
                var commentWithSubCommentModel = new CommentWithSubCommentModel
                {
                    Id = comment.Id,
                    UserId = comment.UserApp != null ? comment.UserApp.Id : "0",
                    Content = comment.Content,
                    SubComments = await _subCommentService.GetSubCommentsByCommentIdAsync(comment.Id),
                    CreatedDate = comment.CreatedDate.ToLocalTime(),
                };

                commentWithSubCommentsModel.Add(commentWithSubCommentModel);
            }


            return Response<List<CommentWithSubCommentModel>>.Success(commentWithSubCommentsModel, 200);
        }
    }
}
//todo createdDate have a problem