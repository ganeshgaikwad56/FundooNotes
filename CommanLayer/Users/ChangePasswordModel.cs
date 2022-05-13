using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommanLayer.Users
{
    public class ChangePasswordModel
    {
        [Required]
        [RegularExpression("^([A-Z][a-z]{3,}[@][0-9]{3,})$", ErrorMessage = "Please enter valid Password")]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        

    }
}
