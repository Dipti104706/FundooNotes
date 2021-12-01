// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelRepository.cs" company="Bridgelabz">
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
    /// LabelRepository class contains methods off add,delete view and edit labels 
    /// </summary>
    public class LabelRepository : ILabelRepository
    {
        /// <summary>
        /// User Context Objects
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRepository"/> class
        /// </summary>
        /// <param name="configuration">IConfiguration configuration</param>
        /// <param name="userContext">UserContext userContext</param>
        public LabelRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }

        /// <summary>
        /// Gets the Configuration object of IConfiguration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Adding label to notes w.r.t UserId
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns string after successfully adding label</returns>
        public async Task<string> AddLabelUserid(LabelModel labelModel)
        {
            try
            {
                var validLabel = this.userContext.Labels.Where(x => x.UserId == labelModel.UserId && x.LabelName != labelModel.LabelName && x.NoteId == null).SingleOrDefault();
                if (validLabel == null)
                {
                    this.userContext.Labels.Add(labelModel);
                    await this.userContext.SaveChangesAsync();
                    return "Added Label";
                }

                return "Label Already Exists";
            }
            catch (ArgumentNullException ex)
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
                var validLabel = this.userContext.Labels.Where(x => x.UserId == labelModel.UserId && x.NoteId == labelModel.NoteId).SingleOrDefault();
                if (validLabel == null)
                {
                    this.userContext.Labels.Add(labelModel);
                    await this.userContext.SaveChangesAsync();
                    return "Added Label";
                }

                return "Label Already Exists";
            }
            catch (ArgumentNullException ex)
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
                var validLabel = this.userContext.Labels.Where(x => x.LabelName == labelName && x.UserId == userId).ToList();
                if (validLabel != null)
                {
                    this.userContext.Labels.RemoveRange(validLabel);
                    await this.userContext.SaveChangesAsync();
                    return "Label Deleted";
                }

                return "Label not exist";
            }
            catch (ArgumentNullException ex)
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
                var validLabel = this.userContext.Labels.Where(x => x.LabelId == labelId).SingleOrDefault();
                if (validLabel != null)
                {
                    this.userContext.Labels.Remove(validLabel);
                    await this.userContext.SaveChangesAsync();
                    return "Deleted Label From Note";
                }

                return "No label present";
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
                var validLabel = this.userContext.Labels.Where(x => x.UserId == labelModel.UserId && x.LabelId == labelModel.LabelId).Select(x => x.LabelName).SingleOrDefault();
                var oldLabelname = this.userContext.Labels.Where(x => x.LabelName == validLabel).ToList();
                oldLabelname.ForEach(x => x.LabelName = labelModel.LabelName);
                this.userContext.Labels.UpdateRange(oldLabelname);
                await this.userContext.SaveChangesAsync();
                return "Label updated";
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
                IEnumerable<string> validLabel = this.userContext.Labels.Where(x => x.UserId == userId).Select(x => x.LabelName);
                if (validLabel != null)
                {
                    return validLabel;
                }

                return null;
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
                IEnumerable<LabelModel> validLabel = this.userContext.Labels.Where(x => x.NoteId == notesId);
                if (validLabel != null)
                {
                    return validLabel;
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
