using FundooModels;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        string Register(RegisterModel userData);
        string LogIn(LoginModel login);
    }
}