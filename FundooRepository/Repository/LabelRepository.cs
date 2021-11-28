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
    }
}
