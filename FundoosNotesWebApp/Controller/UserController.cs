using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooNotes.Controller
{
    public class UserController : ControllerBase
    {
        //Creating reference for Interface
        private readonly IUserManager manager;

        //Parametrized Constructor
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        //Method for User Register Request 
        [HttpPost]
        [Route("api/register")]
        public IActionResult Register([FromBody] RegisterModel userData) //frombody attribute says value read from body of the request
        {
            try
            {
                string result = this.manager.Register(userData);

                if (result.Equals("Registration Successful"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        //Api for Log in functionality
        [HttpGet]
        [Route("api/Login")]
        public IActionResult LogIn([FromBody] LoginModel login)
        {
            try
            {
                string result = this.manager.LogIn(login);

                if (result.Equals("Login Successful"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        //Reset password functionality
        [HttpPut]
        [Route("api/reset")]
        public IActionResult ResetPassword([FromBody] ResetPsModel reset)
        {
            try
            {
                string result = this.manager.ResetPassword(reset);

                if (result.Equals("Password Updated Successfully"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}