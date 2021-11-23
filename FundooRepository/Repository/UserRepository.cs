using Experimental.System.Messaging;
using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<string> Register(RegisterModel userData)
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
                        //Using await 
                        await this.userContext.SaveChangesAsync();
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
            //Using hashing technique of cryptography
            SHA256 encrypt = SHA256.Create();//SHA256-hashing
            byte[] bytes = encrypt.ComputeHash(Encoding.UTF8.GetBytes(password));//computeHash() computes hash 
            return BitConverter.ToString(bytes);//retuns a hexadecimal pairs(like 7F-G6)
        }

        //Method for login functionality
        public string LogIn(LoginModel logIn)//here class is used as datatype and its parameter //Async cant applied for httpget
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
                        //return "Login Successful ";
                        return "Loggedin as" + existEmail.FirstName;
                    }
                }
                return "Email not Exist,Please Register first";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Api for reset password
        public async Task<string> ResetPassword(ResetPsModel reset)
        {
            try
            {
                var validEmail = this.userContext.Users.Where(x => x.Email == reset.Email).FirstOrDefault(); //checking the email present in the DB or not
                if (reset != null)
                {
                    // Encrypting the password
                    validEmail.Password = this.EncryptPassword(reset.Password);
                    // Update the data in the database
                    this.userContext.Update(validEmail);
                    // Save the change in database
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

        //Api for forgot password
        public string ForgotPassword(string email)
        {
            try
            {
                var existEmail = this.userContext.Users.Where(x => x.Email == email).FirstOrDefault(); //checking the email present in the DB or not
                if (existEmail != null)
                {
                    //calling SMTP method to sent mail to the user 
                    SMTPmail(email);
                    return "Email sent to user";
                }
                return "Sending Email failed";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Method for SMTP(Simple mail trasfer protocol)
        public void SMTPmail(string email)
        {
            MailMessage mailId = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");//allow App to sent email using SMTP 
            mailId.From = new MailAddress(this.Configuration["Credentials:Email"]);//contain mail id from where maill will send
            mailId.To.Add(email);// the user mail to which maill will be send
            mailId.Subject = "Regarding forgot password issue";
            SendMSMQ();
            mailId.Body = ReceiveMSMQ();
            SmtpServer.Port = 587;//Port no 
            SmtpServer.Credentials = new System.Net.NetworkCredential(this.Configuration["Credentials:Email"], this.Configuration["Credentials:Password"]);
            SmtpServer.EnableSsl = true; //specify smtpserver use ssl or not, default setting is false
            SmtpServer.Send(mailId);
        }

        //Sending MSMQ(Microsoft messaging queue) method
        public void SendMSMQ()
        {
            MessageQueue msgQueue; //provide access to a queue in MSMQ
            if (MessageQueue.Exists(@".\Private$\fundooNote"))//hecking this private queue exists or not
            {
                msgQueue = new MessageQueue(@".\Private$\fundooNote"); //Path for queue
            }
            else
            {
                msgQueue = MessageQueue.Create(@".\Private$\fundooNote");
            }
            string body = "Please checkout the below url to create your new password";
            msgQueue.Label = "MailBody"; //Adding label to queue
            //Sending msg
            msgQueue.Send(body);
        }

        //Receiving msmq 
        public string ReceiveMSMQ()
        {
            var queue = new MessageQueue(@".\Private$\fundooNote");
            var received = queue.Receive(); //To receive the meassage send by msmq with this format
            return received.ToString();
        }

        //Generating JWT(Json web token) used for xfer information securely
        //
        public string JWTTokenGeneration(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.Configuration["SecretKey"]); //Encrypting secret key
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, email)
            }),
                Expires = DateTime.UtcNow.AddMinutes(30), //expiry time
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler(); //creating and validating JWT
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}   
