using FundooModels;

namespace FundooManager.Manager
{
    public interface IUserManager
    {
        string Register(RegisterModel userData);
    }
}