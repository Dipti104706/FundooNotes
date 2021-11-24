using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
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

        //Api for Edit notes 
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
    }
}
