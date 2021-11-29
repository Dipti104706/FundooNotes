using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface ICollaboratorRepository
    {
        Task<string> AddCollaborator(CollaboratorModel collab);
        Task<string> DeleteCollab(int noteId, string collabMail);
        IEnumerable<string> GetCollaborator(int noteId);
    }
}