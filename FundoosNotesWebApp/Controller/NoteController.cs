// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoteController.cs" company="Bridgelabz">
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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Notes controller class for API related to Notes
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class NoteController : ControllerBase
    {
        /// <summary>
        /// Declaring a object for note manager
        /// </summary>
        private readonly INoteManager noteManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteController"/> class
        /// </summary>
        /// <param name="noteManager">passing a notes manager</param>
        public NoteController(INoteManager noteManager)
        {
            this.noteManager = noteManager;
        }

        /// <summary>
        /// Add notes API
        /// </summary>
        /// <param name="notesModel">passing a note model </param>
        /// <returns>Returns a IAction Result</returns>
        [HttpPost]
        [Route("addNote")]
        public async Task<IActionResult> Notes([FromBody] NoteModel notesModel)
        {
            try
            {
                string result = await this.noteManager.CreateNote(notesModel);
                if (result == "Note is Added")
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
        /// API for change color
        /// </summary>
        /// <param name="noteId">passing noteId as integer</param>
        /// <param name="color">passing color as string</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpPut]
        [Route("changecolor")]
        public async Task<IActionResult> ChangeColour(int noteId, string color)
        {
            try
            {
                string result = await this.noteManager.ChangeColour(noteId, color);
                if (result.Equals("Choosen color is Added"))
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
        /// Archive Notes API
        /// </summary>
        /// <param name="notesId">passing a note id as integer</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpPut]
        [Route("archive")]
        public async Task<IActionResult> NoteArchive(int notesId)
        {
            try
            {
                string result = await this.noteManager.NoteArchive(notesId);
                if (result.Equals("This note does not exist"))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
                else
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// updating title and description of existing notes
        /// </summary>
        /// <param name="note">passing as Note model</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpPut]
        [Route("editnote")]
        public async Task<IActionResult> EditNote([FromBody] NoteModel note)
        {
            try
            {
                string result = await this.noteManager.EditNotes(note);
                if (result.Equals("Note is Updated Succssfully"))
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
        /// Pin notes API 
        /// </summary>
        /// <param name="notesId">passing a note id as integer</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpPut]
        [Route("pinnigNote")]
        public async Task<IActionResult> AddNoteAsPinned(int notesId)
        {
            try
            {
                string result = await this.noteManager.AddNoteAsPinned(notesId);
                if (result.Equals("This note does not exist"))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
                else
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Trash notes API
        /// </summary>
        /// <param name="notesId">passing a note id as integer</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpPut]
        [Route("Notetrashed")]
        public async Task<IActionResult> DeleteNote(int notesId)
        {
            try
            {
                string result = await this.noteManager.DeleteNote(notesId);
                if (result.Equals("This note does not exist"))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
                else
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// API for restore note from trash
        /// </summary>
        /// <param name="notesId">passing a note id as integer</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpPut]
        [Route("restoreNote")]
        public async Task<IActionResult> RetrieveNoteFromTrash(int notesId)
        {
            try
            {
                string result = await this.noteManager.RetrieveNoteFromTrash(notesId);
                if (result.Equals("Note restored"))
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
        /// API for delete note permanently from trash
        /// </summary>
        /// <param name="notesId">passing a note id as integer</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpDelete]
        [Route("deletepermanately")]
        public async Task<IActionResult> DeleteNoteFromTrash(int notesId)
        {
            try
            {
                string result = await this.noteManager.DeleteNoteFromTrash(notesId);
                if (result.Equals("This note does not exist"))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
                else
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// API for adding reminder to notes
        /// </summary>
        /// <param name="noteId">passing a note id as integer</param>
        /// <param name="remind">passing a remainder as string</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpPut]
        [Route("addReminder")]
        public async Task<IActionResult> AddReminder([FromBody] int noteId, string remind)
        {
            try
            {
                string result = await this.noteManager.AddReminder(noteId, remind);
                if (result.Equals("Remind me"))
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
        /// Delete Remainder API
        /// </summary>
        /// <param name="noteId">passing a note id as integer</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpPut]
        [Route("deleteReminder")]
        public async Task<IActionResult> DeleteReminder(int noteId)
        {
            try
            {
                string result = await this.noteManager.DeleteReminder(noteId);
                if (result.Equals("Reminder Deleted"))
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
        /// API for getting all archive notes
        /// </summary>
        /// <param name="userId">passing a user id as integer</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpGet]
        [Route("getArchieveNotes")]
        public IActionResult GetArchieveNotes(int userId)
        {
            try
            {
                IEnumerable<NoteModel> result = this.noteManager.GetArchiveNotes(userId);

                if (result.Equals(null))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No notes in Archive" });
                }
                else
                {
                    return this.Ok(new ResponseModel<IEnumerable<NoteModel>>() { Status = true, Message = "Successfully Retrieved", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// API for getting notes with reminder
        /// </summary>
        /// <param name="userId">passing userId as integer</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpGet]
        [Route("getReminderNotes")]
        public IActionResult ShowReminderNotes(int userId)
        {
            try
            {
                IEnumerable<NoteModel> result = this.noteManager.ShowReminderNotes(userId);

                if (result.Equals(null))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No note has reminder" });
                }
                else
                {
                    return this.Ok(new ResponseModel<IEnumerable<NoteModel>>() { Status = true, Message = "Successfully Retrieved", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Get notes from Trash API
        /// </summary>
        /// <param name="userId">passing a user id as integer</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpGet]
        [Route("getTrashedNotes")]
        public IActionResult GetTrashNotes(int userId)
        {
            try
            {
                IEnumerable<NoteModel> result = this.noteManager.GetTrashNotes(userId);

                if (result.Equals(null))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Trash is empty" });
                }
                else
                {
                    return this.Ok(new ResponseModel<IEnumerable<NoteModel>>() { Status = true, Message = "Successfully Retrieved", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// API for getting a notes
        /// </summary>
        /// <param name="userId">passing a user id</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpGet]
        [Route("getAllNotes")]
        public IActionResult GetNotes(int userId)
        {
            try
            {
                IEnumerable<NoteModel> result = this.noteManager.GetNotes(userId);

                if (result.Equals(null))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Create a note" });
                }
                else
                {
                    return this.Ok(new ResponseModel<IEnumerable<NoteModel>>() { Status = true, Message = "Successfully Retrieved", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// API for adding image to note
        /// </summary>
        /// <param name="notesId">passing a note id as integer</param>
        /// <param name="image">passing a image in the IForm File</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpPut]
        [Route("addImage")]
        public async Task<IActionResult> AddImage(int notesId, IFormFile image)
        {
            try
            {
                string result = await this.noteManager.AddImage(notesId, image);
                if (result == "Image added Successfully")
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = true, Message = ex.Message });
            }
        }

        /// <summary>
        /// Remove image from notes API
        /// </summary>
        /// <param name="noteId">Passing noteId as integer</param>
        /// <returns>Returns a IAction Result</returns>
        [HttpPut]
        [Route("removImage")]
        public async Task<IActionResult> RemoveImage(int noteId)
        {
            try
            {
                string result = await this.noteManager.RemoveImage(noteId);
                if (result.Equals("Removed Image successfully"))
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