using BusinessLayer.Interfaces;
using CommanLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundooContext;
using System;
using System.Linq;
using System.Security.Claims;

namespace FundooNotes.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        FundooContextDB fundooContext;
        IUserBL userBL;
        public UserController(FundooContextDB fundoos, IUserBL userBL)
        {
            this.fundooContext = fundoos;
            this.userBL = userBL;
        }
        [HttpPost("register")]

        public ActionResult AddUser(UserPostModel user)
        {
            try
            {
                this.userBL.AddUser(user);
                return this.Ok(new { success = true, message = "User Added Successfully " });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("login/{Email}/{Password}")]
        public ActionResult LoginUser(string Email, string Password)
        {
            try
            {
                //var userdata = fundooContext.User.FirstOrDefault(u => u.Email == Email && u.Password == Password);//linq
                //if (userdata == null)
                //{
                //    return this.BadRequest(new { Success = false, message = "Email and Password Invalid." });
                //}
                //var check = this.userBL.LoginUser(Email, Password);
                //return this.Ok(new { Success = true, message = $"User Login Successfully {check} " });
                string token = this.userBL.LoginUser(Email, Password);
                if (token == null)
                {
                    return this.BadRequest(new { success = false, message = $"Email or Password is invalid" });
                }
                return this.Ok(new { success = true, message = $"Token Generated is", data=token });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("ForgotPassword/{email}")]
        public ActionResult ForgetPassword(string email)
        {
            try
            {
                var result = this.userBL.ForgetPassword(email);
                if (result != false)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = $"Mail Sent Successfully " +
                        $" token:  {result}"
                    });

                }
                return this.BadRequest(new { success = false, message = "mail not sent" });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [Authorize]
        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordModel changePassword)
        {
            try
            {
                string email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                bool res = this.userBL.ChangePassword(changePassword,email);
                if(res==false)
                {
                    return BadRequest(new { Success = false, message = "Enter Valid Password." });
                }
                //var check = this.userBL.ChangePassword(changePassword);
                return this.Ok(new { Success = true, message = $"Password change Successfully." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
