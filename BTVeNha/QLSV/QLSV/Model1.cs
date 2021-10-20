using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QLSV
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<Lop> Lops { get; set; }
        public virtual DbSet<Sinhvien> Sinhviens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lop>()
                .Property(e => e.Malop)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Lop>()
                .HasMany(e => e.Sinhviens)
                .WithRequired(e => e.Lop)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sinhvien>()
                .Property(e => e.MaSV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sinhvien>()
                .Property(e => e.Hocbong)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Sinhvien>()
                .Property(e => e.Malop)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
