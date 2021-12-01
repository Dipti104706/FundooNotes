// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserContext.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooRepository.Context
{
    using FundooModels;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// UserContext class
    /// </summary>
    public class UserContext : DbContext ////class -helps to connect with the DB
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserContext"/> class
        /// </summary>
        /// <param name="options">options reference for DatabaseContextOptions</param>
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets DatabaseSet for Users stores all user registration data
        /// </summary>
        public DbSet<RegisterModel> Users { get; set; }

        /// <summary>
        ///  Gets or sets DatabaseSet for Notes all note functionalities
        /// </summary>
        public DbSet<NoteModel> Notes { get; set; }

        /// <summary>
        ///  Gets or sets DatabaseSet for Collabs stores all collaborators
        /// </summary>
        public DbSet<CollaboratorModel> Collabs { get; set; }

        /// <summary>
        /// Gets or sets DatabaseSet for Labels to store all label model columns
        /// </summary>
        public DbSet<LabelModel> Labels { get; set; }
    }
}
