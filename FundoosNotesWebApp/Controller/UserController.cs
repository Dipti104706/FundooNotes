using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controller
{
    public class UserController : ControllerBase
    {
        //Creating reference for Interface
        private readonly IUserManager manager;

        //Constructor
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


    }
}