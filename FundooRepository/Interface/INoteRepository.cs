using FundooModels;
using Microsoft.Extensions.Configuration;

namespace FundooRepository.Interface
{
    public interface INoteRepository
    {
        IConfiguration Configuration { get; }

        string CreateNote(NoteModel noteModel);
    }
}