using FundooModels;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        Task<string> Register(RegisterModel userData);
        string LogIn(LoginModel login);
        Task<string> ResetPassword(ResetPsModel reset);
        string ForgotPassword(string email);
        string JWTTokenGeneration(string email);
    }
}