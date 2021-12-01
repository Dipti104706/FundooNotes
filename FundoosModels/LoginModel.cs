// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Login model class
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets email and it is required field
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets password and it is required field
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
