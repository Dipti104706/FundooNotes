using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FundooModels
{
    public class CollaboratorModel
    {
        [Key]
        public int CollaborationId { get; set; }

        public int NoteId { get; set; }

        [ForeignKey("NoteId")]
        public NoteModel noteModel { get; set; }

        public string SharedEmail { get; set; }
    }
}
