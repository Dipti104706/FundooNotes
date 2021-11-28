using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository labelRepository;

        public LabelManager(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }

        //Api for creating new label
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

        //Api for adding labels wrt noteids
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

        //Api for delete a label wrt userid
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

        //remove label from notes
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

        //Edit label
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

        //Retrieve all label wrt Userid
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

        //Retrieve all labels wrt note
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
