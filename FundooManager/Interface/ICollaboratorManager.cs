using FundooModels;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ICollaboratorManager
    {
        Task<string> Collaborator(CollaboratorModel collaborator);
    }
}