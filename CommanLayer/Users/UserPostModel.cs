using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommanLayer.Users
{
    public class UserPostModel
    {
        [Required]
        [RegularExpression("^[A-Z][a-z]{3,}$", ErrorMessage = "Name should contain minimum four character,First latter should be character")]
        public string Firstname { get; set; }
        [Required]
        [RegularExpression("^[A-Z][a-z]{3,}$", ErrorMessage = "Last Name should contain minimum four character,First latter should be character")]
        public string Lastname { get; set; }
        [Required]
        [RegularExpression("^[a-z0-9]{3,}[a-z]{2,}[0-9]{2,}[@][a-z]{5}[.][c-o]{3}$", ErrorMessage = "Please type valid email address")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^([A-Z][a-z]{3,}[@][0-9]{3,})$", ErrorMessage = "Please enter valid Password")]
        public string Password { get; set; }

    }
    
}
