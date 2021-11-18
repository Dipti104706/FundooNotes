using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        //Creating object for Userontext
        private readonly UserContext userContext;

        //Declaring Constructor
        public UserRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }

        public IConfiguration Configuration { get; }

        //Method for user registration
        public string Register(RegisterModel userData)
        {
            try
            {
                var validEmail = this.userContext.Users.Where(x => x.Email == userData.Email).FirstOrDefault();
                if (validEmail == null)
                {
                    if (userData != null)
                    {
                        // Encrypting the password
                        userData.Password = this.EncryptPassword(userData.Password);
                        // Add the data to the database
                        this.userContext.Add(userData);
                        // Save the change in database
                        this.userContext.SaveChanges();
                        return "Registration Successful";
                    }
                    return "Registration UnSuccessful";
                }
                return "Email Id Already Exists";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Encrption of password
        public string EncryptPassword(string password)
        {
            //byte[] used , as GetBytes returns it
            byte[] encrypPassword = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encrypPassword);
        }

        //Method for login functionality
        public string LogIn(LoginModel logIn)//here class is used as datatype
        {
            try
            {
                var validEmail = this.userContext.Users.Where(x => x.Email == logIn.Email).FirstOrDefault();
                var validPassword = this.userContext.Users.Where(x => x.Password == logIn.Password).FirstOrDefault();
                if (validEmail == null && validPassword == null)
                {
                    return "Login UnSuccessful";
                }
                else
                {
                    return "Login Successful";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}   
