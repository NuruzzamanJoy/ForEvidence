using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ForEvidence.Models
{
    public class StoreMaster
    {
        public StoreMaster()
        {
        }
        #region intialize  lazy loading
        //private readonly ILazyLoader _lazyLoader;
        //public StoreMaster(ILazyLoader lazyLoader)
        //{
        //    _lazyLoader = lazyLoader;
        //}
        #endregion

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreID { get; set; }
        [Required]
        [MaxLength(55)]
        [Column(TypeName = "varchar(55)")]
        public string StoreName { get; set; }
        public DateTime? SellDate { get; set; }
        public string StoreType { get; set; }
        public decimal TotalDue { get; set; }
        public int TypeID { get; set; }
        public string ImageName { get; set; }
        //public virtual BType? BType { get; set; }
        public IList<BookDetail> bookDetails { get; set; }
        #region define property for lazy loading
        //private List<BookDetail> _booksDetail;
        //[BackingField(nameof(_booksDetail))]
        //public List<BookDetail> bookDetails
        //{
        //    get => _lazyLoader.Load(this, ref _booksDetail);
        //    set => _booksDetail = value;
        //}
        #endregion
    }
}
