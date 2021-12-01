// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using System;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;

    /// <summary>
    /// Class user manager
    /// </summary>
    public class UserManager : IUserManager
    {
        /// <summary>
        /// declaring instance of IUserRepository as repository
        /// </summary>
        private readonly IUserRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class
        /// </summary>
        /// <param name="repository">taking repository as parameter</param>
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Register method for manager class
        /// </summary>
        /// <param name="userData">passing register model</param>
        /// <returns>Returns string if Registration is successful</returns>
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

        /// <summary>
        /// Login method finds user in database and permit him to login
        /// </summary>
        /// <param name="login">LoginModel loginDetails</param>
        /// <returns>returns string if login is successful</returns>
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

        /// <summary>
        /// Method for Resetting new Password
        /// </summary>
        /// <param name="reset">ResetModel userData</param>
        /// <returns>Returns string if the password is successfully reset</returns>
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

        /// <summary>
        /// Forgot password method performs sending mail to user,for creating new password
        /// </summary>
        /// <param name="email">string email</param>
        /// <returns>Returns a string value as mail sent successfully</returns>
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

        /// <summary>
        /// Generating a JWT token
        /// </summary>
        /// <param name="email">passing email as string</param>
        /// <returns>Returns a string of token</returns>
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
