using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepository noteRepository;

        public NoteManager(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        //Api for creating new note
        public async Task<string> CreateNote(NoteModel notesModel)
        {
            try
            {
                return await this.noteRepository.CreateNote(notesModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Api for changing color of existing note
        public async Task<string> ChangeColour(int noteId, string color)
        {
            try
            {
                return await this.noteRepository.ChangeColour(noteId, color);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Api for changing existing note colour
        public async Task<string> NoteArchive(int noteId)
        {
            try
            {
                return await this.noteRepository.NoteArchive(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Api for edit notes title and yournotes
        public async Task<string> EditNotes(NoteModel note)
        {
            try
            {
                return await this.noteRepository.EditNotes(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Api for pinning notes
        public async Task<string> AddNoteAsPinned(int notesId)
        {
            try
            {
                return await this.noteRepository.AddNoteAsPinned(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Api for adding image
        public Task<string> AddImage(int noteId, IFormFile form)
        {
            try
            {
                return this.noteRepository.AddImage(noteId, form);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
