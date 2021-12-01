// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorController.cs" company="Bridgelabz">
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
    /// Collaborator class for add,delete and edit collaborator
    /// </summary>
    [ApiController]
    [Route("api/[Controller]")]
    public class CollaboratorController : ControllerBase
    {
        /// <summary>
        /// Instance for collaborator manager
        /// </summary>
        private readonly ICollaboratorManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorController"/> class
        /// </summary>
        /// <param name="manager">Collaborator Manager</param>
        public CollaboratorController(ICollaboratorManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// API for adding collaborator to notes
        /// </summary>
        /// <param name="collaborator">Collaborator Model</param>
        /// <returns>Returns a action result</returns>
        [HttpPost]
        [Route("addcollaborator")]////API-Application programming interface
        public async Task<IActionResult> Collaborator([FromBody] CollaboratorModel collaborator)
        {
            try
            {
                string result = await this.manager.Collaborator(collaborator);

                if (result.Equals("Collaborator Added"))
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
        /// API for delete collaborator for notes
        /// </summary>
        /// <param name="noteId">passing noteId as integer</param>
        /// <param name="collabMail">passing collabMail as string</param>
        /// <returns>Returns a action result</returns>
        [HttpDelete]
        [Route("deletecollaborator")]
        public async Task<IActionResult> DeleteCollaborator(int noteId, string collabMail)
        {
            try
            {
                string result = await this.manager.DeleteCollab(noteId, collabMail);
                if (result.Equals("Removed Collaborator"))
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
        /// Get Collaborator API
        /// </summary>
        /// <param name="noteid">passing a note id</param>
        /// <returns>returns a IAction result</returns>
        [HttpGet]
        [Route("getCollab")]
        public IActionResult GetCollaborator(int noteid)
        {
            try
            {
                IEnumerable<string> result = this.manager.GetCollaborator(noteid);

                if (result.Equals(null))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No record found" });
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
    }
}
