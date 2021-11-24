using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesWebApp.Controller
{
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
    }
}
