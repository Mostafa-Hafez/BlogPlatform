using BloggingPlatform.Models;
using BloggingPlatform.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger _logger;

        public CommentController(AppDbContext dbContext,ILogger<CommentController> Logger)
        {
            _dbContext = dbContext;
            _logger = Logger;
        }

        [HttpGet]
        [Route("AddComment")]
        public IActionResult AddComment(int commenterId,int postId,string comment) 
        {
            var user=_dbContext.Users.Find(commenterId);
            var post=_dbContext.Posts.Find(postId);

            if (user == null || post==null) 
            {
                _logger.LogWarning("Invalid User or Post");
                return RedirectToAction("Login", "UserManager"); 
            }

            var comm = new Comment();
            comm.PostId = postId;
            comm.post=post;
            comm.UserId = commenterId;
            comm.User= user;
            comm.com_text=comment;
            _dbContext.Comments.Add(comm);
            _dbContext.SaveChanges();
            var addedcomment = new Commentdto();
            addedcomment.Text= comment;
            addedcomment.Email=user.Email;
            addedcomment.commenterName = user.User_Name;
            addedcomment.com_id = comm.com_Id;
            addedcomment.user_id=user.User_Id;
            addedcomment.Post_id = postId;
            return Ok(addedcomment);
        }

        [HttpGet]
        [Route("GetPostComments")]
        public ActionResult Comments(int userid,int postid)
        {
            var user = _dbContext.Users.Find(userid);
            var post = _dbContext.Posts.Find(postid);
            if (user == null || post == null) {
                _logger.LogWarning("Invalid User or Post");
                return BadRequest(); 
            }
            var comm = _dbContext.Comments.Where(z=>z.UserId==userid&&z.PostId==postid).ToList();
            var comts= new List<Commentdto>();
            foreach (var item in comm) {
                var Com = new Commentdto();
                Com.commenterName = item.User.User_Name;
                Com.Text = item.com_text;
                Com.user_id = item.UserId;
                Com.Email= item.User.Email;
                Com.com_id = item.com_Id;
                Com.Post_id = item.PostId;
                comts.Add(Com);
            }
            return Ok(comts);
        }

    }
}
