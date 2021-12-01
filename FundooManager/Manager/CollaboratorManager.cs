// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorManager.cs" company="Bridgelabz">
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

    /// <summary>
    /// class for CollaboratorManager
    /// </summary>
    public class CollaboratorManager : ICollaboratorManager
    {
        /// <summary>
        /// creating instance of ICollabRepository as repository
        /// </summary>
        private readonly ICollaboratorRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorManager"/> class
        /// </summary>
        /// <param name="repository">ICollaboratorRepository repository</param>
        public CollaboratorManager(ICollaboratorRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Adding new collaborator to notes
        /// </summary>
        /// <param name="collaborator">CollaboratorModel collaborator</param>
        /// <returns>returns string for adding collaborator</returns>
        public async Task<string> Collaborator(CollaboratorModel collaborator)
        {
            try
            {
                return await this.repository.AddCollaborator(collaborator);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                return await this.repository.DeleteCollab(noteId, collabMail);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                return this.repository.GetCollaborator(noteId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}