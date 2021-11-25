using FundooModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface INoteRepository
    {
        IConfiguration Configuration { get; }

        Task<string> CreateNote(NoteModel note);
        Task<string> ChangeColour(int noteId, string color);
        Task<string> NoteArchive(int noteId);
        Task<string> EditNotes(NoteModel note);
        Task<string> AddNoteAsPinned(int notesId);
        Task<string> AddImage(int noteId, IFormFile form);
        Task<string> DeleteNote(int notesId);
        Task<string> RetrieveNoteFromTrash(int notesId);
        Task<string> DeleteNoteFromTrash(int notesId);
    }
}