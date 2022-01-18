using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;

namespace Model
{
    [Table("invoice")]
    public class InvoiceEntity
    {
        [Key]
        [Column("transaction_id")]
        [Required]
        [MaxLength(50)]
        public string TransactionID { get; set; }

        [Column("amount")]
        [Required]
        public decimal Amount { get; set; }

        [Name("Currency Code")]
        [Column("currency_code")]
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string CurrencyCode { get; set; }
        
        [Column("transaction_date")]
        [Required]
        public DateTime TransactionDate { get; set; }

        [Column("status")]
        [Required]
        public char Status { get; set; }

        //public string TransactionID { get; set; }
        //public decimal Amount { get; set; }
        //public string CurrencyCode { get; set; }
        //public DateTime TransactionDate { get; set; }
        //public char Status { get; set; }
    }
}
