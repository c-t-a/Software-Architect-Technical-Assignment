using API.Models.Entities;
using API.Models.Services;
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

        public List<InvoiceEntity> GetInvoices() 
        {
            List<InvoiceEntity> invoices = appDBContext.DbContext.Invoices.ToList();
            return invoices;
        }

        public void UploadInvoice(JObject data)
        {
            try
            {
                var invoicesArray = (JArray)data["invoice"];
                List<Entities.InvoiceEntity> invoices = new List<Entities.InvoiceEntity>();
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
                    InvoiceEntity inovice = new InvoiceEntity { 
                        TransactionID = (string)item["transaction_id"],
                        Amount = (decimal)item["amount"],
                        CurrencyCode = (string)item["currency_code"],
                        TransactionDate = (DateTime)item["transaction_date"],
                        Status = strToChar
                    };
                    invoices.Add(inovice);
                }
                Task.Delay(500);
                appDBContext.DbContext.Invoices.AddRange(invoices);
                appDBContext.DbContext.SaveChanges();
            }
            catch (Exception e) {
                appDBContext.DbContext.Database.RollbackTransaction();
                throw new Exception(e.Message);
            }
        }

        public List<InvoiceEntity> GetInvoicesByStatus(char status)
        {
            List<InvoiceEntity> invoices = appDBContext.DbContext.Invoices.Where(item => item.Status == status).ToList();
            return invoices;
        }

        public List<InvoiceEntity> GetInvoicesByCurrencyCode(string cCode)
        {
            List<InvoiceEntity> invoices = appDBContext.DbContext.Invoices.Where(item => item.CurrencyCode == cCode).ToList();
            return invoices;
        }
    }
}
