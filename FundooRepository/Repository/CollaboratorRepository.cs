// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollabRepository.cs" company="Bridgelabz">
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
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Class for CollabRepository contains methods for add,delete and get collaborators
    /// </summary>
    public class CollabRepository : ICollaboratorRepository
    {
        /// <summary>
        /// User Context Objects
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollabRepository"/> class
        /// </summary>
        /// <param name="configuration">IConfiguration configuration</param>
        /// <param name="userContext">UserContext userContext</param>
        public CollabRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }

        /// <summary>
        /// Gets the Configuration object of IConfiguration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Adding new collaborator to notes
        /// </summary>
        /// <param name="collab">CollaboratorModel collaborator</param>
        /// <returns>returns string for adding collaborator</returns>
        public async Task<string> AddCollaborator(CollaboratorModel collab)
        {
            try
            {
                var result = this.userContext.Notes.Where(x => x.NoteId == collab.NoteId).SingleOrDefault();
                if (result != null)
                {
                    this.userContext.Collabs.Add(collab);
                    await this.userContext.SaveChangesAsync();
                    return "Collaborator Added";
                }
                else
                {
                    return "Note not exists";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deleting a collaborator
        /// </summary>
        /// <param name="noteId">passing noteId as integer</param>
        /// <param name="collabMail">passing collabMail as string</param>
        /// <returns>returns string for deleting collaborator</returns>
        public async Task<string> DeleteCollab(int noteId, string collabMail)
        {
            try
            {
                ////It delete perticular collab mail wrt note id
                var removeCollab = this.userContext.Collabs.Where(x => x.NoteId == noteId && x.SharedEmail == collabMail).SingleOrDefault();
                this.userContext.Collabs.Remove(removeCollab);
                await this.userContext.SaveChangesAsync();
                return "Removed Collaborator";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all collaborators
        /// </summary>
        /// <param name="noteId">integer noteId</param>
        /// <returns>returns string as getting collaborator</returns>
        public IEnumerable<string> GetCollaborator(int noteId)
        {
            try
            {
                IEnumerable<string> collaborators = from note in this.userContext.Collabs where note.NoteId == noteId select note.SharedEmail;
                if (collaborators != null)
                {
                    return collaborators;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
