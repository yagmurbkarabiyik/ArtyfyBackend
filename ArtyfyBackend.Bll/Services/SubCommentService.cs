using ArtyfyBackend.Core.IRepositories;
using ArtyfyBackend.Core.IServices;
using ArtyfyBackend.Core.Models.SubComment;
using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Core.UnitOfWork;
using ArtyfyBackend.Domain.Entities;
using AutoMapper;

namespace ArtyfyBackend.Bll.Services
{
    public class SubCommentService : GenericService<SubComment, SubCommentModel>, ISubCommentService
    {
        private readonly ISubCommentRepository _subCommentRepository;
        private readonly IMapper _mapper;

        public SubCommentService(IUnitOfWork unitOfWork, IGenericRepository<SubComment> repository, IMapper mapper,
            ISubCommentRepository subCommentRepository) : base(repository, mapper, unitOfWork)
        {
            _subCommentRepository = subCommentRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// This method get sub comments for server.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<List<SubCommentModel>> GetSubCommentsByCommentIdAsync(int commentId)
        {
            var subComments = await _subCommentRepository.GetSubCommentsByCommentId(commentId);

            var mappedSubComments = _mapper.Map<List<SubCommentModel>>(subComments);

            return mappedSubComments;
        }
    }
}
