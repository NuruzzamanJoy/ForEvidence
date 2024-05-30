using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ForEvidence.Models
{
    public class BookDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }
        public string? Title { get; set; }
        public int? StoreID { get; set; }
        public int TypeID { get; set; }
        public virtual StoreMaster? StoreMaster { get; set; }
        public virtual BType? BType { get; set; }
    }
}
