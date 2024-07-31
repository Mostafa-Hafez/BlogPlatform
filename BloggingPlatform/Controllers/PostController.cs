using BloggingPlatform.Models;
using BloggingPlatform.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger _logger;

        public PostController(AppDbContext dbContext,ILogger<PostController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public ActionResult Get()
        {
            
            return Ok(_dbContext.Posts.ToList());
        }
        [HttpGet]
        [Route("GetMyPosts")]
        public ActionResult MyPosts(int id)
        {
            var posts= _dbContext.Posts.Where(x=>x.UserId==id).OrderByDescending(z=>z.Creation_Date).ToList();
            var searchedposts = new List<SearchedPost>();
            foreach (var post in posts)
            {
                var serpost = new SearchedPost();
                serpost.Title = post.Title;
                serpost.Content = post.Content;
                serpost.Creation_Date = post.Creation_Date;
                serpost.PostId = post.P_Id;
                serpost.PosterId = id;
                searchedposts.Add(serpost);
            }
            return Ok(searchedposts);
        }

        [HttpGet]
        [Route("searchBytitle")]
        public ActionResult Searchtitle(string title)
        {
            var posts= _dbContext.Posts.Where(z=>z.Title.Contains(title)).OrderByDescending(x=>x.Creation_Date);
            var searchedposts=new List<SearchedPost>();
            foreach(var post in posts)
            {
                var serpost = new SearchedPost();
                serpost.Title = post.Title;
                serpost.Content = post.Content;
                serpost.Creation_Date=post.Creation_Date;
                serpost.PostId = post.P_Id;
                serpost.PosterId = post.UserId;
                var user = _dbContext.Users.Find(serpost.PosterId);
                serpost.PosterName = user.User_Name;
                serpost.PosterEmail = user.Email;
                searchedposts.Add(serpost);
                }
            return Ok(searchedposts);
        }
        [HttpGet]
        [Route("searchByAurhor")]
        public ActionResult Searchauthor(string title)
        {
            var posts = _dbContext.Posts.Where(z => z.User.User_Name.Contains(title)).OrderByDescending(x => x.Creation_Date);
            var searchedposts = new List<SearchedPost>();
            foreach (var post in posts)
            {
                var serpost = new SearchedPost();
                serpost.Title = post.Title;
                serpost.Content = post.Content;
                serpost.Creation_Date = post.Creation_Date;
                serpost.PostId = post.P_Id;
                serpost.PosterId = post.UserId;
                var user=_dbContext.Users.Find(serpost.PosterId);
                serpost.PosterName = user.User_Name;
                serpost.PosterEmail = user.Email;
                searchedposts.Add(serpost) ;
            }
            return Ok(searchedposts);
        }
        [Route("getpost")]
        [HttpGet]
        public ActionResult GetaPost(int userid, int postid)
        {
            var post = _dbContext.Posts.Find(postid);
            if (post == null) {
                _logger.LogWarning("Invalid Post");
                return BadRequest(); 
            }
            var p=new AddPost() { Content=post.Content,PosterId=postid,Title=post.Title,Creation_Date=post.Creation_Date};
            return Ok(p);
        }


        [HttpPost]
        public ActionResult NewPost([FromBody] AddPost getpost) 
        {
            var post = new Post();
            var poster=_dbContext.Users.Find(getpost.PosterId);
            if(poster == null ) 
            {
                _logger.LogWarning("Invalid User");
                return BadRequest(); 
            }
            post.Title =getpost.Title;
            post.Content= getpost.Content;
            post.Creation_Date= DateTime.Now;
            post.UserId=getpost.PosterId;
            post.User = poster;
            getpost.Creation_Date= DateTime.Now;
            _dbContext.Posts.Add(post);
            poster.Posts.Add(post);
            _dbContext.SaveChanges();
            return Ok(getpost);
        }
        [HttpPut]
        public ActionResult EditPost(int id, [FromBody] EditPost editPost)
        {
            var post = _dbContext.Posts.Find(id);
            if (post == null) {
                _logger.LogWarning("InvalidPost");
                return NotFound();
            }
            post.Title=editPost.Title;
            post.Content= editPost.Content;
            _dbContext.SaveChanges();
            var postedited = new AddPost() { Title=editPost.Title,Content=editPost.Content,PosterId=post.P_Id,Creation_Date=post.Creation_Date};
            
            return Ok(postedited);
        }
        [HttpDelete]
        public ActionResult DeletePost(int id) 
        {
            var post = _dbContext.Posts.Find(id);
            if (post == null) { return NotFound(); }
            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();
            return Ok(); 
        }

    }
}
