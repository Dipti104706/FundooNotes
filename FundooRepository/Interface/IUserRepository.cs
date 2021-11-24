using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface IUserRepository
    {
        IConfiguration Configuration { get; }
        Task<string> Register(RegisterModel userData);
        string LogIn(LoginModel logIn);
        Task<string> ResetPassword(ResetPsModel reset);
        string ForgotPassword(string email);
        string JWTTokenGeneration(string email);
    }
}