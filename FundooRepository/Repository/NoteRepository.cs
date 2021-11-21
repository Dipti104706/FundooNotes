using FundooModels;
using FundooRepository.Context;
using Microsoft.Extensions.Configuration;
using System;

namespace FundooRepository.Repository
{
    public class NoteRepository
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
        public string Note(NoteModel noteModel)
        {
            try
            {
                if (noteModel != null)
                {
                    // Add the data to the database
                    this.userContext.Notes.Add(noteModel);
                    // Save the change in database
                    this.userContext.SaveChanges();
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
    }
}
