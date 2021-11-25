using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesWebApp.Controller
{
    [Authorize]
    public class NoteController : ControllerBase
    {
        //Creating reference for Interface
        private readonly INoteManager noteManager;

        //Declaring parameterized constructor
        public NoteController(INoteManager noteManager)
        {
            this.noteManager = noteManager;
        }

        //Api for adding new note
        [HttpPost]
        [Route("api/addNote")]
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

        //Api for change color
        [HttpPut]
        [Route("api/changecolor")]
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

        //Api for make note archieve
        [HttpPut]
        [Route("api/archive")]
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

        //Api for updating title and description of existing notes
        [HttpPut]
        [Route("api/editnote")]
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

        //Api for make note pinned
        [HttpPut]
        [Route("api/pin")]
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

        //api for adding image to note
        [HttpPut]
        [Route("api/addImage")]
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

        //Api for make note delete
        [HttpPut]
        [Route("api/trashed")]
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

        //Api for restore note from trash
        [HttpPut]
        [Route("api/restore")]
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

        //Api for delete note permanately 
        [HttpDelete]
        [Route("api/delete")]
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

        //Api for adding reminder to notes
        [HttpPut]
        [Route("api/addReminder")]
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

        //Api for delete the reminder
        [HttpPut]
        [Route("api/deleteReminder")]
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

        //Api for getting all archieved notes
        [HttpGet]
        [Route("api/getArchieveNotes")]
        public IActionResult GetArchiveNotes(int userId)
        {
            try
            {
                IEnumerable<NoteModel> result = this.noteManager.GetArchiveNotes(userId);

                if (result.Equals(null))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No notes in Archieve" });
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
    }
}
