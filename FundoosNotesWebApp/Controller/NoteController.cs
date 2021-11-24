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
    }
}
