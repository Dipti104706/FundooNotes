// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Collaborator model class
    /// </summary>
    public class CollaboratorModel
    {
        /// <summary>
        /// Gets or sets the collaborator id(primary key) 
        /// </summary>
        [Key]
        public int CollaborationId { get; set; }

        /// <summary>
        /// Gets or sets the note  id as integer
        /// </summary>
        public int NoteId { get; set; }

        /// <summary>
        /// Gets or sets the note id 
        /// </summary>
        [ForeignKey("NoteId")]
        public NoteModel noteModel { get; set; }

        /// <summary>
        /// Gets or sets the collaborator email id
        /// </summary>
        public string SharedEmail { get; set; }
    }
}