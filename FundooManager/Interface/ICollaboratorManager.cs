using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ICollaboratorManager
    {
        Task<string> Collaborator(CollaboratorModel collaborator);
        Task<string> DeleteCollab(int noteId, string collabMail);
        IEnumerable<string> GetCollaborator(int noteId);
    }
}