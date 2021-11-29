using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface ILabelRepository
    {
        Task<string> AddLabelUserid(LabelModel labelModel);
        Task<string> AddLabelNoteid(LabelModel labelModel);
        Task<string> DeleteLabel(int userId, string labelName);
        Task<string> RemoveLabel(int labelId);
        Task<string> EditLabel(LabelModel labelModel);
        IEnumerable<string> GetLabelUserid(int userId);
        IEnumerable<LabelModel> GetLabelByNote(int notesId);
    }
}