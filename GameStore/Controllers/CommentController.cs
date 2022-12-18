using GameStore.Dtos;
using GameStore.Services.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    [Route("/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet, Route("GetGameComments/{id}")]
        public IActionResult GetAllCommentsByGame(int id)
        {
            var comments = _commentService.GetAllComments(id);

            return Ok(comments);
        }

        //[Authorize]
        [HttpPost, Route("AddComment")]
        public async Task<IActionResult> AddCommentToGame([FromBody] AddCommentDto comment)
        {
            var result = await _commentService.AddComment(comment);

            return Ok(result);
        }

        //[Authorize]
        [HttpPost, Route("EditComment")]
        public async Task<IActionResult> EditComment([FromBody] EditCommentDto editComment)
        {
            var result = await _commentService.EditComment(editComment);

            return Ok(result);
        }

        //[Authorize]
        [HttpDelete, Route("RemoveComment/{id}")]
        public async Task<IActionResult> RemoveComment(int id)
        {
            var result = await _commentService.DeleteComment(id);

            return Ok(result);
        }

        //[Authorize]
        [HttpPost, Route("HideComment/{id}")]
        public async Task<IActionResult> HideComment(int id)
        {
            var result = await _commentService.HideComment(id);

            return Ok(result);
        }
    }
}
