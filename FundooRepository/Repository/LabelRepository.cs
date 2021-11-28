using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class LabelRepository : ILabelRepository
    {
        private readonly UserContext userContext;
        public LabelRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }

        public IConfiguration Configuration { get; }

        //api for adding label wrt userid
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

        //api for adding label wrt noteid
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

        //Api for delete labels
        public async Task<string> DeleteLabel(int userId, string labelName)
        {
            try
            {
                var validLabel = this.userContext.Labels.Where(x => x.LabelName == labelName && x.UserId == userId).ToList();
                //for (i = 0;if<= validLabel.Count;i++)

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

        //Remove label from note only not from userid
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

        //Edit label api
        public async Task<string> EditLabel(LabelModel labelModel)
        {
            try
            {
                var validLabel = this.userContext.Labels.Where(x=>x.LabelId == labelModel.LabelId).Select(x => x.LabelName).SingleOrDefault();//select that label name and store it in a variable
                var oldLabelname = this.userContext.Labels.Where(x => x.LabelName == validLabel).ToList();//check and get all model object with same label name from database
                oldLabelname.ForEach(x => x.LabelName = labelModel.LabelName);//update all old name with new
                this.userContext.Labels.UpdateRange(oldLabelname);
                await this.userContext.SaveChangesAsync();
                return "Label updated";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Api for retrieve all labels
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
    }
}
