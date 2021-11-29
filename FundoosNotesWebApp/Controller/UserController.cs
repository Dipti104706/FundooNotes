using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace FundooNotes.Controller
{
    public class UserController : ControllerBase
    {
        //Creating reference for Interface
        private readonly IUserManager manager;

        private readonly ILogger<UserController> logger;

        //Parametrized Constructor
        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.logger = logger;
        }

        //Method for User Register Request 
        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel userData) //frombody attribute says value read from body of the request
        {
            try
            {
                string result = await this.manager.Register(userData);
                logger.LogInformation("New user added successfully with userid " + userData.UserId + " & firstname:" + userData.FirstName);
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
                logger.LogWarning("Exception cought while adding new user" + ex.Message);
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
                logger.LogInformation(login.Email + "Trying to log in");
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
                logger.LogWarning("Exception cought while adding new user" + ex.Message);
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
                logger.LogInformation(reset.Email + "is trying to reset password");
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
                logger.LogWarning("Exception cought while adding new user" + ex.Message);
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
                logger.LogInformation(email + "trying for forget password");
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
                logger.LogWarning("Exception cought while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}