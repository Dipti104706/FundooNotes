using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesWebApp.Controller
{
    public class LabelController : ControllerBase
    {
        //Creating reference for Interface
        private readonly ILabelManager labelManager;

        //Declaring parameterized constructor
        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }

        //Api for adding lebel for userid
        [HttpPost]
        [Route("api/addlabelbyuserid")]
        public async Task<IActionResult> AddLabelUserid([FromBody] LabelModel label)
        {
            try
            {
                string result = await this.labelManager.AddLabelUserid(label);

                if (result.Equals("Added Label"))
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

        //Api for adding lebel for userid
        [HttpPost]
        [Route("api/addlabelbynoteid")]
        public async Task<IActionResult> AddLabelNoteid([FromBody] LabelModel label)
        {
            try
            {
                string result = await this.labelManager.AddLabelNoteid(label);

                if (result.Equals("Added Label"))
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
