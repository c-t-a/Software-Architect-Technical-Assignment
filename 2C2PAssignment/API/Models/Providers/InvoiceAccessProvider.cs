using API.Models.Entities;
using API.Models.Services;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Providers
{
    public class InvoiceAccessProvider : IInvoiceAccessProvider
    {
        private readonly IAppDBContext appDBContext;

        public InvoiceAccessProvider(IAppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public List<Invoice> GetInvoices() 
        {
            List<Invoice> invoices = appDBContext.DbContext.Invoices.ToList();
            return invoices;
        }

        public async Task UploadInvoice(JObject data)
        {
            try
            {
                var invoicesArray = (JArray)data["invoice"];
                List<Invoice> invoices = new List<Invoice>();
                string status;
                char strToChar;
                foreach (var item in invoicesArray) {
                    status = (string)item["status"];
                    if (status == "Approved")
                    {
                        strToChar = 'A';
                    }
                    else if (status == "Failed" || status == "Rejected")
                    {
                        strToChar = 'R';
                    }
                    else if (status == "Finished" || status == "Done")
                    {
                        strToChar = 'D';
                    }
                    else {
                        strToChar = 'I';
                    }
                    Invoice inovice = new Invoice { 
                        TransactionID = (string)item["transaction_id"],
                        Amount = (decimal)item["amount"],
                        CurrencyCode = (string)item["currency_code"],
                        TransactionDate = (DateTime)item["transaction_date"],
                        Status = strToChar
                    };
                    invoices.Add(inovice);
                }
                appDBContext.DbContext.Invoices.AddRange(invoices);
                appDBContext.DbContext.SaveChanges();

                appDBContext.DbContext.Database.CommitTransaction();
            }
            catch (Exception e) {
                appDBContext.DbContext.Database.RollbackTransaction();
                throw new Exception(e.Message);
            }
        }
    }
}
