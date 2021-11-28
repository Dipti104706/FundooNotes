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
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly UserContext userContext;
        public CollaboratorRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }

        public IConfiguration Configuration { get; }

        //Api for add collaboration
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
                    return "Note not exists";
            }

            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
