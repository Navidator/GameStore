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
        public async Task<IActionResult> GetAllCommentsByGame(int gameId)
        {
            return null;
        }

        [Authorize]
        [HttpPost, Route("AddComment/{id}")]
        public async Task<IActionResult> AddCommentToGame([FromBody] AddCommentDto comment)
        {
            return null;
        }

        [Authorize]
        [HttpPost, Route("EditComment/{id}")]
        public async Task<IActionResult> EditComment([FromBody] EditCommentDto editComment, string commentId)
        {
            return null;
        }

        [Authorize]
        [HttpDelete, Route("RemoveComment/{id}")]
        public async Task<IActionResult> RemoveComment(string commentId)
        {
            return null;
        }

        [Authorize]
        [HttpPost, Route("RestoreComment/{id}")]
        public async Task<IActionResult> RestoreComment(string commentId)
        {
            return null;
        }
    }
}
