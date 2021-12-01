// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoteManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Note manager class
    /// </summary>
    public class NoteManager : INoteManager
    {
        /// <summary>
        /// instance of INoteRepository as noteRepository;
        /// </summary>
        private readonly INoteRepository noteRepository;

        /// <summary>
        ///  Initializes a new instance of the <see cref="NoteManager"/> class 
        /// </summary>
        /// <param name="noteRepository">INoteRepository noteRepository</param>
        public NoteManager(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        /// <summary>
        /// Adding notes
        /// </summary>
        /// <param name="notesModel">NoteModel notesModel</param>
        /// <returns>returns a string as note added successful</returns>
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

        /// <summary>
        /// Change color of existing note
        /// </summary>
        /// <param name="noteId">integer noteId</param>
        /// <param name="color">string color</param>
        /// <returns>returns string on adding color successfully</returns>
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

        /// <summary>
        /// Adding note to archive
        /// </summary>
        /// <param name="noteId">integer notesId</param>
        /// <returns>returns the string after note added to archive</returns>
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

        /// <summary>
        /// Updates the title or description of existing notes
        /// </summary>
        /// <param name="note">NotesModel notesModel</param>
        /// <returns>returns string on successful update of data for title and your Note</returns>
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

        /// <summary>
        /// Updates the boolean value for Pin
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns>returns a string after updating pin</returns>
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

        /// <summary>
        /// Updates the boolean value for Trash
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns> returns string on adding notes to trash after deletion</returns>
        public async Task<string> DeleteNote(int notesId)
        {
            try
            {
                return await this.noteRepository.DeleteNote(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Restore to home from trash
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns>returns a string on successful restore</returns>
        public async Task<string> RetrieveNoteFromTrash(int notesId)
        {
            try
            {
                return await this.noteRepository.RetrieveNoteFromTrash(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete data from trash
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns>returns a string on successful delete</returns>
        public async Task<string> DeleteNoteFromTrash(int notesId)
        {
            try
            {
                return await this.noteRepository.DeleteNoteFromTrash(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adding remainder
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <param name="remind">string remainder</param>
        /// <returns>returns string on adding successful remainder</returns>
        public async Task<string> AddReminder(int notesId, string remind)
        {
            try
            {
                return await this.noteRepository.AddReminder(notesId, remind);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete remainder
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns>returns string after removing the remainder</returns>
        public async Task<string> DeleteReminder(int notesId)
        {
            try
            {
                return await this.noteRepository.DeleteReminder(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all archived notes
        /// </summary>
        /// <param name="userId">integer userId</param>
        /// <returns>returns list of all archived</returns>
        public IEnumerable<NoteModel> GetArchiveNotes(int userId)
        {
            try
            {
                return this.noteRepository.GetArchiveNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all Reminder Notes
        /// </summary>
        /// <param name="userId">integer userId</param>
        /// <returns>returns list of reminders</returns>
        public IEnumerable<NoteModel> ShowReminderNotes(int userId)
        {
            try
            {
                return this.noteRepository.ShowReminderNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all trashed Notes
        /// </summary>
        /// <param name="userId">integer userId</param>
        /// <returns>returns list of trashed</returns>
        public IEnumerable<NoteModel> GetTrashNotes(int userId)
        {
            try
            {
                return this.noteRepository.GetTrashNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all Notes of user Id
        /// </summary>
        /// <param name="userId">integer userId</param>
        /// <returns>returns list of all notes</returns>
        public IEnumerable<NoteModel> GetNotes(int userId)
        {
            try
            {
                return this.noteRepository.GetNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adding Image to notes
        /// </summary>
        /// <param name="noteId">integer notesId</param>
        /// <param name="form">IFormFile image</param>
        /// <returns>returns string after successfully adding image</returns>
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

        /// <summary>
        /// API for remove image from notes
        /// </summary>
        /// <param name="noteId">noteId passed as integer</param>
        /// <returns>returns string after successfully removing image</returns>
        public async Task<string> RemoveImage(int noteId)
        {
            try
            {
                return await this.noteRepository.RemoveImage(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
