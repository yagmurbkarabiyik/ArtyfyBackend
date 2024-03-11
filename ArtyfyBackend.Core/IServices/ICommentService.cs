using ArtyfyBackend.Core.Models.Comment;
using ArtyfyBackend.Core.Responses;
using ArtyfyBackend.Core.Services;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Core.IServices
{
    public interface ICommentService : IGenericService<Comment, CommentModel>
    {
        public Task<Response<List<CommentWithSubCommentModel>>> GetCommentsWithSubCommentsAsync();
    }
}