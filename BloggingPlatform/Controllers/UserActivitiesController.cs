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
    public class UserActivitiesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger _logger;

        public UserActivitiesController(AppDbContext dbContext ,ILogger<UserActivitiesController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            return Ok(_dbContext.Users.ToList());
        }

        

        [HttpPost]
        [Route("Follow")]
        public ActionResult AddFollower([FromBody] AddFollower addfollwer)
        {

            var CurrentUser = _dbContext.Users.Find(addfollwer.followerId);
            var FollowUser = _dbContext.Users.FirstOrDefault(z =>
            z.User_Name == addfollwer.User_Name && z.Email == addfollwer.Email);
            var follower = new Follower();
            var getfollowerdata= new GetFollowerData();
            if (FollowUser == null || CurrentUser == null)
            {
                _logger.LogWarning("Inavalid User");
                return NotFound();
            }
            else
            {
                
                follower.user = CurrentUser;
                follower.UserId = CurrentUser.User_Id;
                follower.Followers = FollowUser;
                follower.FollowerID = FollowUser.User_Id;
                _dbContext.Add(follower);
                CurrentUser.Followers.Add(follower);
                getfollowerdata.Email= FollowUser.Email;
                getfollowerdata.UserName = FollowUser.User_Name;
            }

            _dbContext.SaveChanges();
            return Ok(getfollowerdata);
        }

    }
}
