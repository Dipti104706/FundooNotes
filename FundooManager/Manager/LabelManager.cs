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
    }
}
