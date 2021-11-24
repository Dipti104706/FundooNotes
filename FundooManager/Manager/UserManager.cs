using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class UserManager : IUserManager
    {
        //Creating reference for IUserRepository
        private readonly IUserRepository repository;

        //Declaring parametrized constructor
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        //Register functionality
        public async Task<string> Register(RegisterModel userData)
        {
            try
            {
                return await this.repository.Register(userData);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Login functionality
        public string LogIn(LoginModel login)
        {
            try
            {
                return this.repository.LogIn(login);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Reset password functionality
        public async Task<string> ResetPassword(ResetPsModel reset)
        {
            try
            {
                return await this.repository.ResetPassword(reset);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Forgot password functionality
        public string ForgotPassword(string email)
        {
            try
            {
                return this.repository.ForgotPassword(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Generate JWt token
        public string JWTTokenGeneration(string email)
        {
            try
            {
                return this.repository.JWTTokenGeneration(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
