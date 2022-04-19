using AttractionAPI.Models;
using AttractionAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AttractionAPI.Controllers
{
    [Route("api/attraction/{attractionId}/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService _commentService)
        {
            this._commentService = _commentService;
        }

        [HttpPost]
        public ActionResult PostComment([FromRoute] int attractionId, [FromBody] CreateCommentDto dto)
        {
            var newCommentId = _commentService.CreateComment(attractionId, dto);

            return Created($"api/{attractionId}/comment/{newCommentId}", null);
        }
    }
}
