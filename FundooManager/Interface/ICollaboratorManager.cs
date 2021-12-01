// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollaboratorManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;

    /// <summary>
    /// It is Interface ICollaboratorManager
    /// </summary>
    public interface ICollaboratorManager
    {
        /// <summary>
        /// Adding new collaborator
        /// </summary>
        /// <param name="collaborator">passing as CollaboratorModel collaborator</param>
        /// <returns>returns string after adding collaborator</returns>
        Task<string> Collaborator(CollaboratorModel collaborator);

        /// <summary>
        /// Delete collaborator
        /// </summary>
        /// /// <param name="noteId">integer noteId</param>
        /// <param name="collabMail">integer colId</param>
        /// <returns>returns string after deleting collaborator</returns>
        Task<string> DeleteCollab(int noteId, string collabMail);

        /// <summary>
        /// Gets all collaborators
        /// </summary>
        /// <param name="noteId">integer noteId</param>
        /// <returns>returns string after getting collaborator</returns>
        IEnumerable<string> GetCollaborator(int noteId);
    }
}