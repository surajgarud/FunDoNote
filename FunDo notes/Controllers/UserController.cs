using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunDo_notes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IuserBL userBL;
        public UserController(IuserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult Registration(UserRegistration user)
        {
            try
            {
                var result = userBL.Registrartion(user);
                if (result != null)
                    return this.Ok(new { success = true, message = "Registration Successful", data = result });
                else
                    return this.BadRequest(new { success = false, message = "Registration UnSuccessful", data = result });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("login")]
        public IActionResult login(UserLogin userLogin)
        {
            try
            {
                var result = userBL.login(userLogin);
                if (result != null)
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
                else
                    return this.BadRequest(new { success = false, message = "Login UnSuccessful", data = result });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                var result = userBL.ForgetPassword(Email);
                if (result != null)
                    return this.Ok(new { success = true, message = "Mail Sent Successful"});
                else
                    return this.BadRequest(new { success = false, message = "Mail Sent UnSuccessful"});
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
