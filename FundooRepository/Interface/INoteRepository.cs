using FundooModels;
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
    }
}