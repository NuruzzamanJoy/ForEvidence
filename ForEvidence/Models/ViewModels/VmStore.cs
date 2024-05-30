namespace ForEvidence.Models.ViewModels
{
    public class VmStore
    {
        public int StoreID { get; set; }
        public int TypeID { get; set; }
        public string? StoreName { get; set; }
        public string? StoreType { get; set; }
        public DateTime? SellDate { get; set; }
        public decimal TotalDue { get; set; }
        public string[]? Title { get; set; }
        public string? ImageName { get; set; }
        public List<VmD> details { get; set; } = new List<VmD>();
        public class VmD
        {
            public int BookID { get; set; }
            public string? Title { get; set; }
            public int? StoreID { get; set; }
            public int TypeID { get; set; }
        }
    }
}
