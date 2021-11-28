using FundooModels;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ILabelManager
    {
        Task<string> AddLabelUserid(LabelModel labelModel);
        Task<string> AddLabelNoteid(LabelModel labelModel);
        Task<string> DeleteLabel(int userId, string labelName);
    }
}