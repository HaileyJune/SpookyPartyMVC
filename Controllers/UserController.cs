using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpookyPartyMVC.Models;

namespace SpookyPartyMVC.Controllers
{
    public class UserController : Controller
    {

        private SpookyContext dbContext;
        public UserController(SpookyContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                // If a User exists with provided email
                if(dbContext.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Username already in use!");
                    return View("Index");
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);

                    user.Votes = new List<Vote>();
                    //Save user object to the database
                    dbContext.Add(user);
                    dbContext.SaveChanges();
                    HttpContext.Session.SetInt32("userid", user.UserId);
                    return Redirect("/success"); //This doesn't exist yet
                }
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLogin userSubmission)
        {
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Username == userSubmission.LogUsername);
                if(userInDb == null)
                {
                    ModelState.AddModelError("Username", "Yeah, I've never seen this username before.");
                    return View("Index");
                }
                
                var hasher = new PasswordHasher<UserLogin>();
                
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LogPassword);
                
                if(result == 0)
                {
                    ModelState.AddModelError("LogPassword", "How did you forget your password?");
                    return View("Index");
                }
                var user = dbContext.Users.SingleOrDefault(u => u.Username == userSubmission.LogUsername);
                HttpContext.Session.SetInt32("userid", user.UserId);
                return Redirect("/success");
            }
            else
            {
                return View ("Index");
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                return Redirect("/vote");
            }
            else
            {
                return Redirect("/");
            }
        }

    }
}
