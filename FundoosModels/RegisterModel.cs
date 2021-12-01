// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// register model class
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Gets or sets the user id as primary key
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "Please enter a valid First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "Please enter valid a Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email id
        /// </summary>
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+[._+-]{0,1}[a-zA-Z0-9]*@[a-zA-Z0-9]{1,10}.[a-zA-Z]{2,10}[.]*[a-zA-Z]*$", ErrorMessage = "Please enter a valid Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]{1,}[A-Z]*[0-9]*[@&#%$*_-]+[a-zA-Z0-9]*$", ErrorMessage = "Please enter a valid Password")]
        public string Password { get; set; }
    }
}
