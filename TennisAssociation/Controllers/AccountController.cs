using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using TennisAssociation.Models;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace TennisAssociation.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private UserManager<MyUser> userManager;
        private SignInManager<MyUser> signInManager;

        public AccountController(UserManager<MyUser> userManager, SignInManager<MyUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        // GET api/account
        /// <summary>
        /// Return all users.
        /// </summary>
        /// <returns>json containing information about users</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var users = userManager.Users;
            return Json(users);
        }

        // POST api/account/register
        /// <summary>
        /// Register new users.
        /// </summary>
        /// <param name="user">User to be registered.</param>
        /// <returns>Returns true if register succeeded</returns>
        [HttpPost("register")]
        public async Task<bool> RegisterUser(User user)
        {
            MyUser myUser = new MyUser
            {
                UserName = user.Username,
                Email = user.Email
            };

            IdentityResult result = await userManager.CreateAsync(myUser, user.Password);
            bool success = result.Succeeded;

            if (success)
            {
                await userManager.AddToRoleAsync(myUser, "Basic");
            }

            return success;
        }

        /// POST api/account/login
        /// <summary>
        /// Log in a user.
        /// </summary>
        /// <param name="logInInfo">User to be logged in.</param>
        /// <returns>Returns true if log in succeeded</returns>
        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LogInInfo logInInfo)
        {
            MyUser myUser = await userManager.FindByNameAsync(logInInfo.Username);

            if (myUser != null)
            {
                await signInManager.SignOutAsync();
                var result = await signInManager.PasswordSignInAsync(myUser, logInInfo.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok(myUser);
                }
                else
                {
                    return NotFound(myUser);
                }
            }  

            return NotFound(myUser);
        }

        /// GET api/account/signout
        /// <summary>
        /// Sign out the active users.
        /// </summary>
        /// <returns></returns>
        [HttpGet("signout")]
        public async Task<bool> SignOut()
        {
            await signInManager.SignOutAsync();
            return true;
        }

        // POST api/account/remove
        /// <summary>
        /// Removes the user with a given id.
        /// </summary>
        /// <param name="id">User's id to be removed.</param>
        /// <returns>Returns true removal succeeded</returns>
        [HttpPost("remove")]
        public async Task<bool> RemoveUser([FromBody]string id)
        {
            MyUser myUser = await userManager.FindByIdAsync(id);

            if (myUser != null)
            {
                IdentityResult result = await userManager.DeleteAsync(myUser);
                return result.Succeeded;

            }
            
            return false;
        }
    }
}