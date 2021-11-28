using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRepository repository;
        public CollaboratorManager(ICollaboratorRepository repository)
        {
            this.repository = repository;
        }

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
    }
}
