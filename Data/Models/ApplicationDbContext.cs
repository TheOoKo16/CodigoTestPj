using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<evoucher> Evouchers { get; set; }
        public virtual DbSet<customer> Customers { get; set; }
        public virtual DbSet<purchase> Purchases { get; set; }
        public virtual DbSet<user> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<evoucher>().Ignore(t => t.PhotoUrl);
            //modelBuilder.Entity<evoucher>().ToTable("Evouchers");

            // Configure Primary Keys  

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Blog>()
            //    .Property(b => b.Url)
            //    .IsRequired();

            // modelBuilder.Entity<Cars>()
            // .Property(e => e.Price)
            // .HasPrecision(19, 4);

            // modelBuilder.Entity<Cars>.Property(r => r.TotalScore)
            //.HasColumnType("decimal(5,2)")
            //.IsRequired(true);



            //modelBuilder.Entity<Photos>().Ignore(t => t.PhotoUrl);

        }

        
    }
}
