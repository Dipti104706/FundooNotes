// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee Behura"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controller
{
    using System;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using StackExchange.Redis;

    /// <summary>
    /// User controller class for register,login,reset password,forgot password
    /// </summary>
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        ////Creating reference for Interface

        /// <summary>
        /// IUserManager object
        /// </summary>
        private readonly IUserManager manager;

        /// <summary>
        /// ILogger object
        /// </summary>
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class
        /// </summary>
        /// <param name="manager">IUserManager manager</param>
        /// <param name="logger">ILogger logger</param>
        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.logger = logger;
        }

        /// <summary>
        /// Performs the Registration of a new user
        /// </summary>
        /// <param name="userData">passing a register model data</param>
        /// <returns>This method returns the IAction Result according to Http</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel userData) ////frombody attribute says value read from body of the request
        {
            try
            {
                string result = await this.manager.Register(userData);
                this.logger.LogInformation("New user added successfully with userid " + userData.UserId + " & firstname:" + userData.FirstName);
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
                this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        ////Api for Log in functionality
        ////Async will not work for httpGet

        /// <summary>
        /// login API for already existing user 
        /// </summary>
        /// <param name="login">LoginModel data</param>
        /// <returns>returns http status if logged in successfully</returns>
        [HttpPost]
        [Route("Login")]
        public IActionResult LogIn([FromBody] LoginModel login)
        {
            try
            {
                var result = this.manager.LogIn(login);
                this.logger.LogInformation(login.Email + "Trying to log in");
                if (result.Equals("Login Successful"))
                {
                    ////HttpContext.Session.SetString("User Email", login.Email);
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
                this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// reset password  API
        /// </summary>
        /// <param name="reset">passing a ResetPsModel model</param>
        /// <returns>returns action result according to http request</returns>
        [HttpPut]
        [Route("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPsModel reset)
        {
            try
            {
                string result = await this.manager.ResetPassword(reset);
                this.logger.LogInformation(reset.Email + "is trying to reset password");
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
                this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// forgot password data
        /// </summary>
        /// <param name="email">email as string</param>
        /// <returns>returns http status for forgot password IActionResult </returns>
        [HttpPost]
        [Route("forgot")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                string result = this.manager.ForgotPassword(email);
                this.logger.LogInformation(email + "trying to access forgot password");
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
                this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
