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

        //Api for deletelable for user id
        [HttpDelete]
        [Route("api/deletelabel")]
        public async Task<IActionResult> DeleteLabel(int userId, string labelName)
        {
            try
            {
                string result = await this.labelManager.DeleteLabel(userId, labelName);

                if (result.Equals("Label Deleted"))
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

        //Api for remove labwl for noteid
        [HttpDelete]
        [Route("api/removelable")]
        public async Task<IActionResult> RemoveLabel(int labelId)
        {
            try
            {
                string result = await this.labelManager.RemoveLabel(labelId);

                if (result.Equals("Deleted Label From Note"))
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

        //Edit lable api
        [HttpPut]
        [Route("api/editLabel")]
        public async Task<IActionResult> EditLabel([FromBody] LabelModel label)
        {
            try
            {
                string result = await this.labelManager.EditLabel(label);

                if (result.Equals("Label updated"))
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

        //Api for getting all labels wrt userid
        [HttpGet]
        [Route("api/getlabelbyUserid")]
        public IActionResult GetLabelUserid(int userId)
        {
            try
            {
                IEnumerable<string> result = this.labelManager.GetLabelUserid(userId);

                if (result.Equals(null))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No label found" });
                }
                else
                {
                    return this.Ok(new ResponseModel<IEnumerable<string>>() { Status = true, Message = "Successfully Retrieved", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        //Api for getting all labels by noteid
        [HttpGet]
        [Route("api/getlabelbynotes")]
        public IActionResult GetLabelByNote(int notesId)
        {
            try
            {
                IEnumerable<LabelModel> result = this.labelManager.GetLabelByNote(notesId);

                if (result.Equals(null))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No label found" });
                }
                else
                {
                    return this.Ok(new ResponseModel<IEnumerable<LabelModel>>() { Status = true, Message = "Successfully Retrieved", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }
    }
}
