// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Experimental.System.Messaging;
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using StackExchange.Redis;

    /// <summary>
    /// User Repository class has functions for registration,log in,reset password and forget password
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// User Context Objects
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class
        /// </summary>
        /// <param name="configuration">IConfiguration configuration</param>
        /// <param name="userContext">UserContext userContext</param>
        public UserRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }

        /// <summary>
        /// Gets the Configuration object of IConfiguration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Adding the user to the database
        /// </summary>
        /// <param name="userData">RegisterModel userData</param>
        /// <returns>Returns true if Registration is successful</returns>
        public async Task<string> Register(RegisterModel userData)
        {
            try
            {
                var validEmail = this.userContext.Users.Where(x => x.Email == userData.Email).FirstOrDefault();
                if (validEmail == null)
                {
                    //// Encrypting the password
                    userData.Password = this.EncryptPassword(userData.Password);
                    //// Add the data to the database
                    this.userContext.Add(userData);
                    //// Save the change in database
                    //// Using await 
                    await this.userContext.SaveChangesAsync();
                    return "Registration Successful";
                }

                return "Email Id Already Exists";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Encryption of password
        /// </summary>
        /// <param name="password">passes password as string</param>
        /// <returns>returns of encrypted password in the form of hexadecimal pairs</returns>
        public string EncryptPassword(string password)
        {
            ////Using hashing technique of cryptography
            SHA256 encrypt = SHA256.Create(); ////SHA256-hashing
            byte[] bytes = encrypt.ComputeHash(Encoding.UTF8.GetBytes(password)); ////computeHash() computes hash 
            return BitConverter.ToString(bytes); ////retuns a hexadecimal pairs(like 7F-G6)
        }

        /// <summary>
        /// Login method finds user in database and permit him to login
        /// </summary>
        /// <param name="logIn">LoginModel loginDetails</param>
        /// <returns>returns string if login is successful</returns>
        public string LogIn(LoginModel logIn) ////here class is used as datatype and its parameter and Async cant applied for httpget
        {
            try
            {
                var existEmail = this.userContext.Users.Where(x => x.Email == logIn.Email).FirstOrDefault();
                if (existEmail != null)
                {
                    logIn.Password = this.EncryptPassword(logIn.Password);
                    var existingPassword = this.userContext.Users.Where(x => x.Password == logIn.Password).FirstOrDefault();
                    if (existingPassword == null)
                    {
                        return "Login UnSuccessful";
                    }
                    else
                    {
                        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                        IDatabase database = connectionMultiplexer.GetDatabase();
                        database.StringSet(key: "First Name", existingPassword.FirstName);
                        database.StringSet(key: "Last Name", existingPassword.LastName);
                        database.StringSet(key: "User Id", existingPassword.UserId.ToString());
                        return "Login Successful";
                    }
                }

                return "Email not Exist,Please Register first";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
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
                var existEmail = this.userContext.Users.Where(x => x.Email == reset.Email).FirstOrDefault(); ////checking the email present in the DB or not
                if (reset != null)
                {
                    ////Encrypting the password
                    existEmail.Password = this.EncryptPassword(reset.Password);
                    ////Update the data in the database
                    this.userContext.Update(existEmail);
                    ////Save the change in database
                    await this.userContext.SaveChangesAsync();
                    return "Password Updated Successfully";
                }

                return "Reset Password is Unsuccssful";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
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
                var existEmail = this.userContext.Users.Where(x => x.Email == email).FirstOrDefault(); ////checking the email present in the DB or not
                if (existEmail != null)
                {
                    ////calling SMTP method to sent mail to the user 
                    this.SMTPmail(email);
                    return "Email sent to user";
                }

                return "Sending Email failed";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method for SMTP(Simple mail transfer protocol)
        /// </summary>
        /// <param name="email">passing email as string</param>
        public void SMTPmail(string email)
        {
            MailMessage mailId = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com"); ////allow App to sent email using SMTP 
            mailId.From = new MailAddress(this.Configuration["Credentials:Email"]); ////contain mail id from where maill will send
            mailId.To.Add(email); //// the user mail to which maill will be send
            mailId.Subject = "Regarding forgot password issue";
            this.SendMSMQ();
            mailId.Body = this.ReceiveMSMQ();
            SmtpServer.Port = 587; ////Port no 
            SmtpServer.Credentials = new System.Net.NetworkCredential(this.Configuration["Credentials:Email"], this.Configuration["Credentials:Password"]);
            SmtpServer.EnableSsl = true;  ////specify smtpserver use ssl or not, default setting is false
            SmtpServer.Send(mailId);
        }

        /// <summary>
        /// Sending message through MSMQ
        /// </summary>
        public void SendMSMQ()
        {
            MessageQueue msgQueue; ////provide access to a queue in MSMQ
            ////checking this private queue exists or not
            if (MessageQueue.Exists(@".\Private$\fundooNote"))
            {
                msgQueue = new MessageQueue(@".\Private$\fundooNote"); ////Path for queue
            }
            else
            {
                msgQueue = MessageQueue.Create(@".\Private$\fundooNote");
            }

            string body = "Please checkout the below url to create your new password";
            msgQueue.Label = "MailBody"; ////Adding label to queue
            ////Sending msg
            msgQueue.Send(body);
        }

        /// <summary>
        /// For reading message from MSMQ
        /// </summary>
        /// <returns>Returns the message in the queue is sent successfully</returns>
        public string ReceiveMSMQ()
        {
            var receivequeue = new MessageQueue(@".\Private$\fundooNote");
            var receivemsg = receivequeue.Receive();
            return receivemsg.ToString();
        }

        /// <summary>
        /// Generating a JWT token
        /// </summary>
        /// <param name="email">passing email as string</param>
        /// <returns>Returns a string of token</returns>
        public string JWTTokenGeneration(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.Configuration["SecretKey"]); ////Encrypting secret key
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, email)
            }),
                Expires = DateTime.UtcNow.AddMinutes(30), ////expiry time
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature) ////creating a digital signature
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler(); ////creating and validating JWT
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token); ////write- serialize security token to web token
        }
    }
}   
