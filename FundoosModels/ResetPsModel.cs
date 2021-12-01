// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResetPsModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class for reset password model
    /// </summary>
    public class ResetPsModel
    {
        /// <summary>
        /// Gets or sets the email as string
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the new password as string
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
