using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;

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

        public string Register(RegisterModel userData)
        {
            try
            {
                return this.repository.Register(userData);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

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

        public string ResetPassword(ResetPsModel reset)
        {
            try
            {
                return this.repository.ResetPassword(reset);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
