using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface ILabelRepository
    {
        IConfiguration Configuration { get; }

        Task<string> AddLabelUserid(LabelModel labelModel);
        Task<string> AddLabelNoteid(LabelModel labelModel);
    }
}