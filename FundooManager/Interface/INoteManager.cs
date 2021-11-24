using FundooModels;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface INoteManager
    {
        Task<string> CreateNote(NoteModel notesModel);
    }
}