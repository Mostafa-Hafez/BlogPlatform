using BloggingPlatform.Authentication;
using BloggingPlatform.Models;
using BloggingPlatform.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BloggingPlatform.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly AppDbContext _dbcontext;
        private readonly jwt _jwt;
        private readonly ILogger _logger;

        public UserManagerController(AppDbContext dbcontext,jwt jwt ,ILogger<UserManagerController> logger)
        {
            _dbcontext = dbcontext;
            _jwt = jwt;
            _logger = logger;
        }



        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginModel login )
        {
            var user =_dbcontext.Users.FirstOrDefault(z=>z.User_Name==login.userName&&z.Password==login.Password); ;
            if (user == null)
            {
                _logger.LogWarning("inalid Login Credentials");
                return NotFound(); 
            } 
            var tokenhandeler = new JwtSecurityTokenHandler();
            var tokendescription = new SecurityTokenDescriptor
            {
                Issuer=_jwt.Issuer,
                Audience=_jwt.Aduence,
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.key)),
                SecurityAlgorithms.HmacSha256),
                Subject=new ClaimsIdentity(
                    new Claim[]
                    {
                        new (ClaimTypes.NameIdentifier,login.userName),
                        new (ClaimTypes.Email,user.Email)
                    }
                    )
            };
            var securitytoken =  tokenhandeler.CreateToken(tokendescription);
            var accestoken = tokenhandeler.WriteToken(securitytoken);
            return Ok(accestoken);
        }



        [HttpPost]
        [Route("CreateAccount")]
        public ActionResult CraeteAccount([FromBody] CreateAcount acc)
        {
            var em = new EmailAddressAttribute().IsValid(acc.Email);
            if (!(em && acc.User_Name.Length < 50))
            {
                return BadRequest(acc);
            }
            var user = new User();
            user.User_Name = acc.User_Name;
            user.Email = acc.Email;
            user.Password = acc.Password;
            _dbcontext.Users.Add(user);
            _dbcontext.SaveChanges();
            var loginmodel = new LoginModel() { userName=user.User_Name,Password=user.Password};
            return RedirectToAction("Login", loginmodel) ;
        }
    }
}
