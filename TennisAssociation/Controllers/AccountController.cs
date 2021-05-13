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
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using TennisAssociation.Interfaces;
using TennisAssociation.Services;
using TennisAssociation.Utils;


namespace TennisAssociation.Controllers
{
    /// <summary>
    /// Controller for registration and sign in/out.
    /// </summary>
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private UserManager<MyUser> userManager;
        private SignInManager<MyUser> signInManager;
        private IEmailSender _emailSenderService;

        public AccountController(UserManager<MyUser> userManager, SignInManager<MyUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _emailSenderService = new EmailSender();
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
        public async Task<IActionResult> RegisterUser(User user)
        {
            MyUser myUser = new MyUser
            {
                UserName = user.Username,
                Email = user.Email
            };

            IdentityResult result = await userManager.CreateAsync(myUser, user.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(myUser, "Basic");
                //Send mail!
                var mail = new Email
                {
                    Body = "You registered in TennisAssociation app with this email!\n \n Your admin team!",
                    Subject = "Registration using this email"
                };
                _emailSenderService.Send(myUser.Email, mail);
                return Ok(user);
            }

            return StatusCode(409, "Register failed.");
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
                    var isAdmin = await userManager.IsInRoleAsync(myUser, "Admin");
                    myUser.IsAdmin = isAdmin;
                    return Ok(myUser);
                }
                else
                {
                    return StatusCode(401, "Password incorrect.");
                }
            }  

            return StatusCode(401, "Username incorrect.");
        }


        /// POST api/account/changepass
        /// <summary>
        /// Change password for a user.
        /// </summary>
        /// <param name="logInInfo">User for which we need to change the password.</param>
        /// <returns>Returns true if succeeded</returns>
        [HttpPost("changepass")]
        public async Task<IActionResult> ChangePass(UserUpdateInfo updateInfo)
        {
            MyUser myUser = await userManager.FindByNameAsync(updateInfo.Username);

            if (myUser != null)
            {
                var result = await signInManager.PasswordSignInAsync(myUser, updateInfo.Password, false, false);

                if (result.Succeeded)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(myUser);
                    var changePasswordResult = await userManager.ResetPasswordAsync(myUser, token, updateInfo.NewPassword);
                    if (changePasswordResult.Succeeded)
                    {
                        var mail = new Email
                        {
                            Body = "Your password in TennisAssociation app is changed!\n \nYour admin team!",
                            Subject = "Password changed"
                        };
                        _emailSenderService.Send(myUser.Email, mail);
                        return Ok(myUser);
                    }

                    return StatusCode(409, "Someting went wrong. Try later.");
                }
                else
                {
                    return StatusCode(401, "Password incorrect.");
                }
            }

            return StatusCode(401, "Username incorrect.");
        }

        /// GET api/account/signout
        /// <summary>
        /// Sign out the active users.
        /// </summary>
        /// <returns></returns>
        [HttpGet("signout")]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        // POST api/account/remove
        /// <summary>
        /// Removes the user with a given id.
        /// </summary>
        /// <param name="id">User's id to be removed.</param>
        /// <returns>Returns true removal succeeded</returns>
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveUser([FromBody]string id)
        {
            MyUser myUser = await userManager.FindByIdAsync(id);

            if (myUser != null)
            {
                IdentityResult result = await userManager.DeleteAsync(myUser);
                if (result.Succeeded)
                {
                    var mail = new Email
                    {
                        Body =
                            "User in TennisAssocation app connected with this email is removed from system!\n \nYour admin team!",
                        Subject = "User removing"
                    };
                    _emailSenderService.Send(myUser.Email, mail);
                    return Ok(myUser);
                }

                return StatusCode(409, "Someting went wrong. Try later.");
            }
            
            return StatusCode(401, "Username incorrect.");
        }
    }
}