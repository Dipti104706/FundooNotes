using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class NoteModel
    {
        [Key]
        public int NoteId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")] //As it creates relationship with Users table that this note should belongs to perticular userid
        public RegisterModel registerModel { get; set; }
        public string Title { get; set; }

        public string YourNotes { get; set; }

        public string Remainder { get; set; }

        public string Color { get; set; }

        public string Image { get; set; }

        [DefaultValue(false)]
        public bool Archive { get; set; }

        [DefaultValue(false)]
        public bool Trash { get; set; }

        [DefaultValue(false)]
        public bool Pinned { get; set; }
    }
}
