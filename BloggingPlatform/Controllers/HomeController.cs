using BloggingPlatform.Models;
using BloggingPlatform.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppDbContext _dbcontext;

        public HomeController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public ActionResult News(int userid)
        {
            var user = _dbcontext.Users.Find(userid);
            if (user == null) { return BadRequest(); }
            var followers = _dbcontext.Followers.Where(z => z.UserId == userid).ToList();
            var AllFollowerPosts = new List<SearchedPost>();
            foreach (var item in followers)
            {
                var pts=_dbcontext.Posts.Where(z=>z.UserId==item.FollowerID).ToList();
                foreach (var p in pts)
                {
                    var post = new SearchedPost();
                    post.PosterId = p.UserId;
                    post.PostId=p.P_Id;
                    post.Title = p.Title;
                    post.Content=p.Content;
                    post.Creation_Date=p.Creation_Date;
                    var user2 = _dbcontext.Users.Find(post.PosterId);
                    post.PosterName = user2.User_Name;
                    post.PosterEmail = user2.Email;

                    AllFollowerPosts.Add(post);
                }
                

            }
            var all = AllFollowerPosts.OrderByDescending(x => x.Creation_Date).ToList();
            return Ok(all);
        }
    }
}
