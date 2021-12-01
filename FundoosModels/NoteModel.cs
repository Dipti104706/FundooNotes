// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoteModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Notes model class
    /// </summary>
    public class NoteModel
    {
        /// <summary>
        /// Gets or sets the note id(primary key) 
        /// </summary>
        [Key]
        public int NoteId { get; set; }

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user id as foreign key
        /// </summary>
        [ForeignKey("UserId")] ////As it creates relationship with Users table that this note should belongs to perticular userid
        public RegisterModel registerModel { get; set; }

        /// <summary>
        ///  Gets or sets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Notes
        /// </summary>
        public string YourNotes { get; set; }

        /// <summary>
        /// Gets or sets the Remainder
        /// </summary>
        public string Remainder { get; set; }

        /// <summary>
        /// Gets or sets the Color for note
        /// </summary>
        public string Colour { get; set; }

        /// <summary>
        /// Gets or sets the Image for note
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether status as true or false, taken default value as false only
        /// </summary>
        [DefaultValue(false)]
        public bool Archieve { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether status as true or false, taken default value as false only
        /// </summary>
        [DefaultValue(false)]
        public bool Trash { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether status as true or false
        /// </summary>
        [DefaultValue(false)]
        public bool Pinned { get; set; }
    }
}
