using CommanLayer.Users;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entities;
using RepositoryLayer.FundooContext;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        FundooContextDB fundooContext;


        public IConfiguration Configuration { get; }

        public UserRL(FundooContextDB fundooContext, IConfiguration Configuration)
        {
            this.fundooContext = fundooContext;
           this.Configuration = Configuration;

        }
        public void AddUser(UserPostModel user)
        {
            string passwordToEncript = string.Empty;
            try
            {
                User userdata = new User();
                userdata.Firstname = user.Firstname;
                userdata.Lastname = user.Lastname;
                userdata.Email = user.Email;
                //userdata.Password = EncryptPassword(user.Password);
                passwordToEncript = EncodePasswordToBase64(user.Password);
                userdata.Password = passwordToEncript;
                fundooContext.Add(userdata);
                fundooContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EncodePasswordToBase64(string Password)
        {
            try
            {
                byte[] encData_byte = new byte[Password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(Password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public string LoginUser(string Email, string Password)
        {
            try
            {
                var AllRecords = fundooContext.User.ToList();
                var existingRecord = AllRecords.Where(x => x.Email == Email).FirstOrDefault();

                if (existingRecord != null)
                {
                    var decriptedPassword = DecodeFrom64(existingRecord.Password);
                    bool conditionCheck = decriptedPassword == Password ? true : false;
                    if (conditionCheck == false)
                    {
                        return "Invalid credentials";
                    }
                    else
                    {
                        return GenerateJWTToken(existingRecord.Email, existingRecord.UserID);
                    }
                }
                else
                {
                    return null;
                }

                ////var user = fundooContext.User.FirstOrDefault(u => u.Email == Email && u.Password == Password);
                //var user = fundooContext.User.Where(u => u.Email == Email).FirstOrDefault();
                //string pass = (user.Password);
                //if (user == null)
                //{
                //    return null;
                //}
                ////string decryptedPass = DecrypString(user.Password);
                ////if (decryptedPass == Password)
                //    return GenerateJWTToken(Email, user.UserID);
                ////throw new Exception("Incorrect Password");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string GenerateJWTToken(string email, int userId)
        {
            //generate token

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email),
                    new Claim("userId",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ForgetPassword(string Email)
        {
            try
            {
                var userdata = fundooContext.User.FirstOrDefault(u => u.Email == Email);
                if (userdata == null)
                {
                    return false;
                }
                MessageQueue queue;

                //Add message to Queue
                if (MessageQueue.Exists(@".\private$\FundooQueue"))
                {
                    queue = new MessageQueue(@".\private$\FundooQueue");
                }
                else
                {
                    queue = MessageQueue.Create(@".\private$\FundooQueue");
                }
                Message Mymessage = new Message();
                Mymessage.Formatter = new BinaryMessageFormatter();
                Mymessage.Body = GenerateJWTToken(Email, userdata.UserID);
                Mymessage.Label = "Forgot Password email";
                queue.Send(Mymessage);

                Message msg = queue.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendMail(Email, Mymessage.Body.ToString());
                queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();

            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied." + "Queue might be a system queue.");
                }
            }
        }

        private string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ChangePassword(ChangePasswordModel changePassword, string Email)
        {
            try
            {
                var user = fundooContext.User.FirstOrDefault(u => u.Email == Email);
                if (changePassword.Password.Equals(changePassword.ConfirmPassword))
                {

                    user.Password = EncodePasswordToBase64(changePassword.Password);
                    fundooContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
