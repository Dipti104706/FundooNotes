// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabelRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;

    /// <summary>
    /// ILabelRepository Inteface
    /// </summary>
    public interface ILabelRepository
    {
        /// <summary>
        /// Adding label to the note w.r.t userID
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns string after successfully adding label w.r.t UserId</returns>
        Task<string> AddLabelUserid(LabelModel labelModel);

        /// <summary>
        /// Adding label to the note w.r.t noteID
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns string after successfully adding label w.r.t NoteId</returns>
        Task<string> AddLabelNoteid(LabelModel labelModel);

        /// <summary>
        /// Delete a Label name from userID
        /// </summary>
        /// <param name="userId">integer userId</param>
        /// <param name="labelName">string labelName</param>
        /// <returns>returns a string after deleting from home</returns>
        Task<string> DeleteLabel(int userId, string labelName);

        /// <summary>
        /// Remove a Label from Notes
        /// </summary>
        /// <param name="labelId">integer labelId</param>
        /// <returns>returns a string after removing from notes</returns>
        Task<string> RemoveLabel(int labelId);

        /// <summary>
        /// Edit label name for user Id
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns a string after updating label name</returns>
        Task<string> EditLabel(LabelModel labelModel);

        /// <summary>
        /// Gets Label Based on userId
        /// </summary>
        /// <param name="userId">integer userId</param>
        /// <returns>returns a list for all labels for userID</returns>
        IEnumerable<string> GetLabelUserid(int userId);

        /// <summary>
        /// Gets Label Based on noteId
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns>returns a list for labels based on notesId</returns>
        IEnumerable<LabelModel> GetLabelByNote(int notesId);
    }
}