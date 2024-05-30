using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ForEvidence.Models
{
    public class BType
    {
        [Key]
        public int TypeID { get; set; }
        [Required]
        [MaxLength(55)]
        [Column(TypeName = "varchar(55)")]
        public string? TName { get; set; }
        public virtual ICollection<StoreMaster> StoreMasters { get; set; } = new List<StoreMaster>();
        public virtual ICollection<BookDetail> BookDetails { get; set; } = new List<BookDetail>();
    }
}
