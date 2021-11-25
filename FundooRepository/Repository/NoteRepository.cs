using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class NoteRepository : INoteRepository
    {
        //Creating object for Usercontext
        private readonly UserContext userContext;

        //Declaring parameterized constructor
        public NoteRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }

        public IConfiguration Configuration { get; }

        //Method for adding notes in the fundoo note application
        public async Task<string> CreateNote(NoteModel note)
        {
            try
            {
                if (note != null)
                {
                    // Add the data to the database
                    this.userContext.Notes.Add(note);
                    //Save changes to database
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

        //Api for change color of the existing note
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

        //API for making note as archieve
        public async Task<string> NoteArchive(int noteId)
        {
            try
            {
                string res;
                var availNote = this.userContext.Notes.Where(x => x.NoteId == noteId).SingleOrDefault();
                if (availNote != null) //checking this note exist or not
                {
                    if (availNote.Archieve == false)
                    {
                        availNote.Archieve = true; //Making note stored in archeive
                        if (availNote.Pinned == true) //Now checking is that note is pinned or not
                        {
                            availNote.Pinned = false;//making it unpinned , then stored in archieve
                            res = "Notes unpinned and moved to Archived";
                        }
                        else
                        {
                            //If that note is not pinned , then directly note can be archieved
                            res = "Note archived";
                        }
                    }
                    else
                    {
                        //if the note is already archieved , then make that unarchieved
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

        //Api for Edit title and description of notes 
        public async Task<string> EditNotes(NoteModel note)
        {
            try
            {
                var availNoteId = this.userContext.Notes.Where(x => x.NoteId == note.NoteId).FirstOrDefault();
                if (availNoteId != null)
                {
                    if (note != null)
                    {
                        availNoteId.Title = note.Title; //updating title
                        availNoteId.YourNotes = note.YourNotes;//updating description of note
                        // Add the data to the database
                        this.userContext.Notes.Update(availNoteId);
                        //Save changes to database
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

        //Api for Making note as pinned
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
                        if (availNoteId.Archieve == true) //Now checking is that note is pinned or not
                        {
                            availNoteId.Archieve = false;//making it unpinned , then stored in archieve
                            res = "Notes unarchieved and pinned";
                        }
                        else
                            res = "Note pinned";
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

        //Api for adding image
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
                    return "This note doesn't Exists";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Api fot deleting a note to trash
        public async Task<string> DeleteNote(int notesId)
        {
            try
            {
                var availNoteId = this.userContext.Notes.Where(x => x.NoteId == notesId).SingleOrDefault();
                if (availNoteId != null)
                {
                    availNoteId.Trash = true; //Note deleted and added to trash bin
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
    }
}
