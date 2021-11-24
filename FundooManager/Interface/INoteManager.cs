using FundooModels;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface INoteManager
    {
        Task<string> CreateNote(NoteModel notesModel);
        Task<string> ChangeColour(int noteId, string color);
        Task<string> NoteArchive(int noteId);
    }
}