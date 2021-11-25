using FundooModels;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface INoteManager
    {
        Task<string> CreateNote(NoteModel notesModel);
        Task<string> ChangeColour(int noteId, string color);
        Task<string> NoteArchive(int noteId);
        Task<string> EditNotes(NoteModel note);
        Task<string> AddNoteAsPinned(int notesId);
        Task<string> AddImage(int noteId, IFormFile form);
    }
}