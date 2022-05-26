using CommanLayer.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public void AddUser(UserPostModel user);
        public string LoginUser(string Email, string Password);
        public bool ForgetPassword(string Email);
        public bool ChangePassword(ChangePasswordModel changePassword, string Email);
    }
}
