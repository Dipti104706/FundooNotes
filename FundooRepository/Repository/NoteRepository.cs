// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoteRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Class for NoteRepository with all Note functionalities
    /// </summary>
    public class NoteRepository : INoteRepository
    {
        /// <summary>
        /// User Context Objects
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteRepository"/> class
        /// </summary>
        /// <param name="configuration">IConfiguration configuration</param>
        /// <param name="userContext">UserContext userContext</param>
        public NoteRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }

        /// <summary>
        /// Gets the Configuration object of IConfiguration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Adding notes
        /// </summary>
        /// <param name="note">NoteModel notesModel</param>
        /// <returns>returns a string as note added successful</returns>
        public async Task<string> CreateNote(NoteModel note)
        {
            try
            {
                if (note.Title != null)
                {
                    ////Add the data to the database
                    this.userContext.Notes.Add(note);
                    ////Save changes to database
                    await this.userContext.SaveChangesAsync();
                    return "Note is Added";
                }
                else
                {
                    return "Adding note unsuccessful";
                }
            }
            catch (ArgumentNullException ex)
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
                var availNote = this.userContext.Notes.Where(x => x.NoteId == noteId).SingleOrDefault();
                if (availNote != null)
                {
                    if (color != null)
                    {
                        availNote.Colour = color;
                        this.userContext.Notes.Update(availNote);
                        await this.userContext.SaveChangesAsync();
                        return "Choosen color is Added";
                    }
                    else
                    {
                        return "Adding color is Unsuccessful";
                    }
                }
                else
                {
                    return "This note does not exist";
                }
            }
            catch (ArgumentNullException ex)
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
                string res;
                var availNote = this.userContext.Notes.Where(x => x.NoteId == noteId).SingleOrDefault();
                ////checking this note exist or not
                if (availNote != null)
                {
                    if (availNote.Archieve == false)
                    {
                        availNote.Archieve = true; ////Making note stored in archeive
                        if (availNote.Pinned == true)
                        {
                            availNote.Pinned = false; ////making it unpinned, then stored in archieve
                            res = "Notes unpinned and moved to Archived";
                        }
                        else
                        {
                            ////If that note is not pinned , then directly note can be archieved
                            res = "Note archived";
                        }
                    }
                    else
                    {
                        ////if the note is already archieved , then make that unarchieved
                        availNote.Archieve = false;
                        res = "Note unarchived";
                    }

                    this.userContext.Notes.Update(availNote);
                    await this.userContext.SaveChangesAsync();
                }
                else
                {
                    res = "This note does not exist";
                }

                return res;
            }
            catch (ArgumentNullException ex)
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
                var availNoteId = this.userContext.Notes.Where(x => x.NoteId == note.NoteId).FirstOrDefault();
                if (availNoteId != null)
                {
                    if (note != null)
                    {
                        availNoteId.Title = note.Title; ////updating title
                        availNoteId.YourNotes = note.YourNotes; ////updating description of note
                        ////Add the data to the database
                        this.userContext.Notes.Update(availNoteId);
                        ////Save changes to database
                        await this.userContext.SaveChangesAsync();
                        return "Note is Updated Succssfully";
                    }
                    else
                    {
                        return "Updating note unsuccessful";
                    }
                }
                else
                {
                    return "This note does not exist";
                }
            }
            catch (ArgumentNullException ex)
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
                string res;
                var availNoteId = this.userContext.Notes.Where(x => x.NoteId == notesId).SingleOrDefault();
                if (availNoteId != null)
                {
                    if (availNoteId.Pinned == false)
                    {
                        availNoteId.Pinned = true;
                        ////Now checking is that note is pinned or not
                        if (availNoteId.Archieve == true)
                        {
                            availNoteId.Archieve = false; ////making it unpinned , then stored in archieve
                            res = "Notes unarchieved and pinned";
                        }
                        else
                        {
                            res = "Note pinned";
                        }
                    }
                    else
                    {
                        availNoteId.Pinned = false;
                        res = "Note unpinned";
                    }

                    this.userContext.Notes.Update(availNoteId);
                    await this.userContext.SaveChangesAsync();
                }
                else
                {
                    res = "This note does not exist";
                }

                return res;
            }
            catch (ArgumentNullException ex)
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
                var availNoteId = this.userContext.Notes.Where(x => x.NoteId == notesId).SingleOrDefault();
                if (availNoteId != null)
                {
                    availNoteId.Trash = true; ////Note deleted and added to trash bin
                    if (availNoteId.Pinned == true)
                    {
                        availNoteId.Pinned = false;
                        this.userContext.Notes.Update(availNoteId);
                        await this.userContext.SaveChangesAsync();
                        return "Note unpinned and trashed";
                    }

                    return "Note trashed";
                }
                else
                {
                    return "This note does not exist";
                }
            }
            catch (ArgumentNullException ex)
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
                var availNoteId = this.userContext.Notes.Where(x => x.NoteId == notesId).FirstOrDefault();
                if (availNoteId != null)
                {
                    availNoteId.Trash = false; ////Note retrieved from trash
                    this.userContext.Notes.Update(availNoteId);
                    await this.userContext.SaveChangesAsync();
                    return "Note restored";
                }
                else
                {
                    return "This note does not exist";
                }
            }
            catch (ArgumentNullException ex)
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
                var availNoteId = this.userContext.Notes.Where(x => x.NoteId == notesId).FirstOrDefault();
                if (availNoteId != null)
                {
                    this.userContext.Notes.Remove(availNoteId);
                    await this.userContext.SaveChangesAsync();
                    return "Note Deleted Permanately";
                }
                else
                {
                    return "This note does not exist";
                }
            }
            catch (ArgumentNullException ex)
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
                var availNoteId = this.userContext.Notes.Where(x => x.NoteId == notesId).SingleOrDefault();
                if (availNoteId != null)
                {
                    availNoteId.Remainder = remind;
                    this.userContext.Notes.Update(availNoteId);
                    await this.userContext.SaveChangesAsync();
                    return "Remind me";
                }
                else
                {
                    return "This note does not exist";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete remainder
        /// </summary>
        /// <param name="noteId">integer notesId</param>
        /// <returns>returns string after removing the remainder</returns>
        public async Task<string> DeleteReminder(int noteId)
        {
            try
            {
                var availNoteId = this.userContext.Notes.Where(x => x.NoteId == noteId).SingleOrDefault();
                if (availNoteId != null)
                {
                    availNoteId.Remainder = null;
                    this.userContext.Notes.Update(availNoteId);
                    await this.userContext.SaveChangesAsync();
                    return "Reminder Deleted";
                }

                return "This note does not exist";
            }
            catch (ArgumentNullException ex)
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
                IEnumerable<NoteModel> availUserId = this.userContext.Notes.Where(x => x.UserId == userId && x.Archieve == true);
                if (availUserId != null)
                {
                    return availUserId;
                }

                return null;
            }
            catch (ArgumentNullException ex)
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
                IEnumerable<NoteModel> availRem = this.userContext.Notes.Where(x => x.UserId == userId && x.Remainder != null);
                if (availRem != null)
                {
                    return availRem;
                }

                return null;
            }
            catch (ArgumentNullException ex)
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
                IEnumerable<NoteModel> availTrashed = this.userContext.Notes.Where(x => x.UserId == userId && x.Trash == true);
                if (availTrashed != null)
                {
                    return availTrashed;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all Notes of user Id
        /// </summary>
        /// <param name="userId">integer userId</param>
        /// <returns>returns list of all notes</returns>
        public IEnumerable<NoteModel> GetNotes(int userId) ////IEnumerable used to get elements from the collection
        {
            try
            {
                IEnumerable<NoteModel> allNotes = from notes in this.userContext.Notes
                                                  where notes.UserId == userId && notes.Archieve == false && notes.Trash == false
                                                  select notes;
                if (allNotes != null)
                {
                    return allNotes;
                }

                return null;
            }
            catch (ArgumentNullException ex)
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
        public async Task<string> AddImage(int noteId, IFormFile form)
        {
            try
            {
                var availNote = this.userContext.Notes.Where(x => x.NoteId == noteId).SingleOrDefault();
                if (availNote != null)
                {
                    var cloudinary = new Cloudinary(
                                                new Account(
                                                "denc5oxgu",
                                                "637868744849594",
                                                "BkvR84UOh4LNjRCZaoqFvEhh_dk"));
                    var addingImage = new ImageUploadParams()
                    {
                        File = new FileDescription(form.FileName, form.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(addingImage);
                    var uploadPath = uploadResult.Url;
                    availNote.Image = uploadPath.ToString();
                    this.userContext.Notes.Update(availNote);
                    await this.userContext.SaveChangesAsync();
                    return "Image added Successfully";
                }
                else
                {
                    return "This note doesn't Exists";
                }
            }
            catch (ArgumentNullException ex)
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
                var existNote = this.userContext.Notes.Where(x => x.NoteId == noteId).SingleOrDefault();
                if (existNote != null)
                {
                    existNote.Image = null;
                    this.userContext.Notes.Update(existNote);
                    await this.userContext.SaveChangesAsync();
                    return "Removed Image successfully";
                }

                return "This note does not exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
