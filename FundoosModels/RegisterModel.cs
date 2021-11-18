using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooModels
{
    public class RegisterModel
    {
        [Key]
        public string UserId { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "Please enter a valid First Name")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "Please enter valid a Last Name")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9] +[._+-]{0, 1}[a-zA-Z0-9]*@[a-zA-Z0-9]{1,10}.[a-zA-Z]{ 2,10}[.]*[a-zA-Z]*$", ErrorMessage = "Please enter a valid Email")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]{1,}[A-Z]*[0-9]*[@&#%$*_-]+[a-zA-Z0-9]*$", ErrorMessage = "Please enter a valid Password")]
        public string Password { get; set; }
    }
}
