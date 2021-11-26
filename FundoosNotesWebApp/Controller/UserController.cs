using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Register([FromBody] RegisterModel userData) //frombody attribute says value read from body of the request
        {
            try
            {
                string result = await this.manager.Register(userData);

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
        //Async will not work for httpGet
        [HttpGet]
        [Route("api/Login")]
        public IActionResult LogIn([FromBody] LoginModel login)
        {
            try
            {
                var result = this.manager.LogIn(login);

                if (result.Equals("Login Successful"))
                {
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string firstName = database.StringGet("First Name");
                    string lastName = database.StringGet("Last Name");
                    int userId = Convert.ToInt32(database.StringGet("User Id"));
                    RegisterModel data = new RegisterModel
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = login.Email,
                        UserId = userId
                    };
                    string token = this.manager.JWTTokenGeneration(login.Email);
                    return this.Ok(new { Status = true, Message = result, Data = data, Token = token });
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
        public async Task<IActionResult> ResetPassword([FromBody] ResetPsModel reset)
        {
            try
            {
                string result = await this.manager.ResetPassword(reset);

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

        //Forgot password functionality
        [HttpPost]
        [Route("api/forgot")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                string result = this.manager.ForgotPassword(email);

                if (result.Equals("Email sent to user"))
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