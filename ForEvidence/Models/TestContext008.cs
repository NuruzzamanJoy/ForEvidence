using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ForEvidence.Models
{
    public class TestContext008 : DbContext
    {
        public TestContext008()
        {
        }
        public TestContext008(DbContextOptions<TestContext008> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false; // to enable lazy load include this line
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<StoreMaster>().HasOne(s => s.BType).WithMany(s => s.StoreMasters).HasForeignKey(s => s.TypeID).OnDelete(DeleteBehavior.Cascade);
        //    modelBuilder.Entity<BType>().HasData(
        //        new BType { TypeID = 6, TName = "War" },
        //         new BType { TypeID = 7, TName = "Horror" },
        //         new BType { TypeID = 8, TName = "Comedy" }
        //        );
        //}
        public virtual DbSet<BType> Types { get; set; }
        public virtual DbSet<BookDetail> BookDetails { get; set; }
        public virtual DbSet<StoreMaster> StoreMasters { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ForEvidenceDB;Trusted_Connection=True;");
        }
    }
}
