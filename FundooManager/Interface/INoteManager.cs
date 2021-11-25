using FundooModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
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
        Task<string> DeleteNote(int notesId);
        Task<string> RetrieveNoteFromTrash(int notesId);
        Task<string> DeleteNoteFromTrash(int notesId);
        Task<string> AddReminder(int notesId, string remind);
        Task<string> DeleteReminder(int noteId);
        IEnumerable<NoteModel> GetArchiveNotes(int userId);
        IEnumerable<NoteModel> ShowReminderNotes(int userId);
    }
}