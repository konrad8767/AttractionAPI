using AttractionAPI.Models;
using AttractionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

            return Created($"api/attraction/{attractionId}/comment/{newCommentId}", null);
        }

        [HttpGet("{commentId}")]
        public ActionResult<CommentDto> Get([FromRoute] int attractionId, [FromRoute] int commentId)
        {
            CommentDto comment = _commentService.GetById(attractionId, commentId);
            return Ok(comment);
        }

        [HttpGet]
        public ActionResult<List<CommentDto>> GetAll([FromRoute] int attractionId, [FromRoute] int commentId)
        {
            var comments = _commentService.GetAll(attractionId);
            return Ok(comments);
        }

        [HttpDelete("{commentId}")]
        public ActionResult Delete([FromRoute] int attractionId, [FromRoute] int commentId)
        {
            _commentService.Remove(attractionId, commentId);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult DeleteAll([FromRoute] int attractionId)
        {
            _commentService.RemoveAll(attractionId);

            return NoContent();
        }
    }
}
