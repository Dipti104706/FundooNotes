// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelManager.cs" company="Bridgelabz">
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
    /// LabelManager class
    /// </summary>
    public class LabelManager : ILabelManager
    {
        /// <summary>
        /// instance of ILabelRepository as labelRepository;
        /// </summary>
        private readonly ILabelRepository labelRepository;

        /// <summary>
        ///  Initializes a new instance of the <see cref="LabelManager"/> class 
        /// </summary>
        /// <param name="labelRepository">ILabelRepository labelRepository</param>
        public LabelManager(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }

        /// <summary>
        /// Adding label to notes w.r.t UserId
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns string after successfully adding label</returns>
        public async Task<string> AddLabelUserid(LabelModel labelModel)
        {
            try
            {
                return await this.labelRepository.AddLabelUserid(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adding label to notes w.r.t NoteId
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns string after successfully adding label</returns>
        public async Task<string> AddLabelNoteid(LabelModel labelModel)
        {
            try
            {
                return await this.labelRepository.AddLabelNoteid(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete a Label from UserId
        /// </summary>
        /// <param name="userId">integer userId</param>
        /// <param name="labelName">string labelName</param>
        /// <returns>returns a string after deleting successfully</returns>
        public async Task<string> DeleteLabel(int userId, string labelName)
        {
            try
            {
                return await this.labelRepository.DeleteLabel(userId, labelName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Remove Label name from UserId
        /// </summary>
        /// <param name="labelId">integer labelId</param>
        /// <returns>returns a string after removing successfully</returns>
        public async Task<string> RemoveLabel(int labelId)
        {
            try
            {
                return await this.labelRepository.RemoveLabel(labelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Edit label name for userId
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns a string after editing label name successfully</returns>
        public async Task<string> EditLabel(LabelModel labelModel)
        {
            try
            {
                return await this.labelRepository.EditLabel(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets Label Based on userId
        /// </summary>
        /// <param name="userId">integer userId</param>
        /// <returns>returns a list for all labels for userID</returns>
        public IEnumerable<string> GetLabelUserid(int userId)
        {
            try
            {
                return this.labelRepository.GetLabelUserid(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets Label Based on noteId
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns>returns a list for labels based on notesId</returns>
        public IEnumerable<LabelModel> GetLabelByNote(int notesId)
        {
            try
            {
                return this.labelRepository.GetLabelByNote(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
