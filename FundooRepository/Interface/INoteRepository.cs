// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INoteRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooRepository.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Interface for INoteRepository
    /// </summary>
    public interface INoteRepository
    {
        /// <summary>
        /// Adding new notes to the database
        /// </summary>
        /// <param name="notesModel">NotesModel notesModel passed</param>
        /// <returns>returns a string when note added successful</returns>
        Task<string> CreateNote(NoteModel notesModel);

        /// <summary>
        /// Change the color of note
        /// </summary>
        /// <param name="noteId">integer noteId</param>
        /// <param name="color">string color</param>
        /// <returns>returns string on successfully changing of color</returns>
        Task<string> ChangeColour(int noteId, string color);

        /// <summary>
        /// Make the note archive
        /// </summary>
        /// <param name="noteId">integer notesId</param>
        /// <returns>returns the string after successfully archive a note</returns>
        Task<string> NoteArchive(int noteId);

        /// <summary>
        /// Edit the title or description of existing note
        /// </summary>
        /// <param name="note">NotesModel note passed</param>
        /// <returns>returns string when successfully update the title or YourNotes</returns>
        Task<string> EditNotes(NoteModel note);

        /// <summary>
        /// Make a note as pinned
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns>returns a string after making note pinned</returns>
        Task<string> AddNoteAsPinned(int notesId);

        /// <summary>
        /// Make a note moved to trash
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns> returns string on delete notes to trash</returns>
        Task<string> DeleteNote(int notesId);

        /// <summary>
        /// Retrieve note from trash
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns> returns string on retrieve notes from trash</returns>
        Task<string> RetrieveNoteFromTrash(int notesId);

        /// <summary>
        /// Delete note permanently from trash
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns> returns string after deleting a note permanently from trash</returns>
        Task<string> DeleteNoteFromTrash(int notesId);

        /// <summary>
        /// Adding reminder to notes
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <param name="remind">string remainder</param>
        /// <returns>returns string when successfully add a reminder</returns>
        Task<string> AddReminder(int notesId, string remind);

        /// <summary>
        /// Delete reminder for a note
        /// </summary>
        /// <param name="noteId">integer notesId</param>
        /// <returns>returns string after deleting a reminder</returns>
        Task<string> DeleteReminder(int noteId);

        /// <summary>
        /// Get Archive Notes
        /// </summary>
        /// <param name="userId">integer userId</param>
        /// <returns>returns list as all archived note</returns>
        IEnumerable<NoteModel> GetArchiveNotes(int userId);

        /// <summary>
        /// Get notes whose reminder added
        /// </summary>
        /// <param name="userId">integer userId</param>
        /// <returns>returns list as all reminders</returns>
        IEnumerable<NoteModel> ShowReminderNotes(int userId);

        /// <summary>
        /// Get all trash Notes
        /// </summary>
        /// <param name="userId">integer userId</param>
        /// <returns>returns list as result</returns>
        IEnumerable<NoteModel> GetTrashNotes(int userId);

        /// <summary>
        /// Gets all notes for a UserID
        /// </summary>
        /// <param name="userId">integer UserId</param>
        /// <returns>Returns a list of get all notes</returns>
        IEnumerable<NoteModel> GetNotes(int userId);

        /// <summary>
        /// Adding Image to note
        /// </summary>
        /// <param name="noteId">integer notesId</param>
        /// <param name="form">IFormFile image</param>
        /// <returns>returns string after successfully adding image</returns>
        Task<string> AddImage(int noteId, IFormFile form);

        /// <summary>
        /// Remove image from a note
        /// </summary>
        /// <param name="noteId">integer noteId</param>
        /// <returns>returns string after successfully removing image</returns>
        Task<string> RemoveImage(int noteId);
    }
}