using ArtyfyBackend.Core.IServices;
using ArtyfyBackend.Core.Models.SubComment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtyfyBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCommentsController : BaseController
    {
        private readonly ISubCommentService _subCommentService;

        public SubCommentsController(ISubCommentService subCommentService)
        {
            _subCommentService = subCommentService;
        }

        /// <summary>
        /// This method is used to get all sub comments.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _subCommentService.GetAllAsync());
        }

        /// <summary>
        /// This method is used to get sub comment by id.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add(SubCommentModel model)
        {
            return CreateActionResult(await _subCommentService.AddAsync(model));
        }
    }
}
