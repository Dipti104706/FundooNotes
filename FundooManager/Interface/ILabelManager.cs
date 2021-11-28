using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ILabelManager
    {
        Task<string> AddLabelUserid(LabelModel labelModel);
        Task<string> AddLabelNoteid(LabelModel labelModel);
        Task<string> DeleteLabel(int userId, string labelName);
        Task<string> RemoveLabel(int labelId);
        Task<string> EditLabel(LabelModel labelModel);
        IEnumerable<string> GetLabelUserid(int userId);
    }
}