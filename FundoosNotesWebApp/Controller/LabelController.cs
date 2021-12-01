// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee Behura"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// label controller class 
    /// </summary>
    [ApiController]
    [Route("api/[Controller]")]
    public class LabelController : ControllerBase
    {
        /// <summary>
        /// Declaring a label manager
        /// </summary>
        private readonly ILabelManager labelManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class
        /// </summary>
        /// <param name="labelManager">passing a label manager</param>
        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }

        /// <summary>
        /// Adding label with respect to UserId method 
        /// </summary>
        /// <param name="label">passing a label model</param>
        /// <returns>returns a IAction result</returns>
        [HttpPost]
        [Route("addlabelbyuserid")]
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

        /// <summary>
        /// Adding label w.r.t NoteId method 
        /// </summary>
        /// <param name="label">passing a label model</param>
        /// <returns>returns a IAction result</returns>
        [HttpPost]
        [Route("addlabelbynoteid")]
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

        /// <summary>
        /// API for delete label for userId
        /// </summary>
        /// <param name="userId">Passing userId as integer</param>
        /// <param name="labelName">label name as string</param>
        /// <returns>returns a IAction result</returns>
        [HttpDelete]
        [Route("deletelabel")]
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

        /// <summary>
        /// Remove label in notes method
        /// </summary>
        /// <param name="labelId">passing  a label id as integer</param>
        /// <returns>returns a IAction result</returns>
        [HttpDelete]
        [Route("removelable")]
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

        /// <summary>
        /// API for update label name
        /// </summary>
        /// <param name="label">passing label as label model</param>
        /// <returns> return a IAction result</returns>
        [HttpPut]
        [Route("editLabel")]
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

        /// <summary>
        /// API for getting all labels w.r.t userId
        /// </summary>
        /// <param name="userId">passing a userId as integer</param>
        /// <returns>returns a IAction Result</returns>
        [HttpGet]
        [Route("getlabelbyUserid")]
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

        /// <summary>
        /// Getting all labels by notesId
        /// </summary>
        /// <param name="notesId">passing a notesId</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpGet]
        [Route("getlabelbynotes")]
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
