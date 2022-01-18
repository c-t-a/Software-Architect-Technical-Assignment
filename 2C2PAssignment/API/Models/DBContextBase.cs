//using API.Models.Entities;
using API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class DBContextBase : DbContext
    {
        public DBContextBase(DbContextOptions<DBContextBase> options) : base(options)
        {
        }

        public DbSet<InvoiceEntity> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //modelBuilder.Entity<InvoiceEntity>();

            modelBuilder.Entity<InvoiceEntity>(entity =>
            {
                entity.ToTable("invoice");
                entity.Property(e => e.TransactionID).IsRequired().HasColumnName("transaction_id");
                entity.Property(e => e.Amount).IsRequired().HasColumnName("amount").HasPrecision(18, 2);
                entity.Property(e => e.CurrencyCode).IsRequired().HasColumnName("currency_code");
                entity.Property(e => e.TransactionDate).IsRequired().HasColumnName("transaction_date");
                entity.Property(e => e.Status).IsRequired().HasColumnName("status");
            });
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
