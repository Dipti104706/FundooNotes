// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Interface
{
    using System.Threading.Tasks;
    using FundooModels;

    /// <summary>
    /// Interface IUserManager
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Register new user to the database
        /// </summary>
        /// <param name="userData">RegisterModel userData</param>
        /// <returns>Returns string if Registration is successful</returns>
        Task<string> Register(RegisterModel userData);

        /// <summary>
        /// Login checks for the user in database and allows him to login
        /// </summary>
        /// <param name="login">LoginModel login</param>
        /// <returns>Returns string if login is successful</returns>
        string LogIn(LoginModel login);

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="reset">ResetModel reset</param>
        /// <returns>Returns string if the password is successfully resetting</returns>
        Task<string> ResetPassword(ResetPsModel reset);

        /// <summary>
        /// Forgot password method performs sending mail to user to reset their password
        /// </summary>
        /// <param name="email">string email</param>
        /// <returns>Returns string if mail sent successfully</returns>
        string ForgotPassword(string email);

        /// <summary>
        /// JWT token generation
        /// </summary>
        /// <param name="email">email as string</param>
        /// <returns>Returns string of token</returns>
        string JWTTokenGeneration(string email);
    }
}