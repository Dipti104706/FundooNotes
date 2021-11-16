using FundooModels;
using Microsoft.Extensions.Configuration;

namespace FundooRepository.Repository
{
    public interface IUserRepository
    {
        IConfiguration Configuration { get; }

        string Register(RegisterModel userData);
    }
}