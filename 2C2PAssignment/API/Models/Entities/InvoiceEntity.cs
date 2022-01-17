using Model;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Entities
{
    public class InvoiceEntity
    {
        [Key]
        //[Required]
        //[MaxLength(50)]
        public string TransactionID { get; set; }
        //[Required]
        public decimal Amount { get; set; }
        //[Required]
        //[StringLength(3, MinimumLength = 3)]
        public string CurrencyCode { get; set; }
        //[Required]
        public DateTime TransactionDate { get; set; }
        //[Required]
        public char Status { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Invoice toInvoiceModel() {
            return new Invoice {
                TransactionID = TransactionID,
                Amount = Amount,
                CurrencyCode = CurrencyCode,
                TransactionDate = TransactionDate,
                Status = Status
            };
        }
    }
}
