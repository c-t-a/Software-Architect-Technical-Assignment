using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;

namespace Web.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IRestService restService;

        public InvoiceRepository(IRestService restService)
        {
            this.restService = restService;
        }

        public async Task UploadInvoices(List<Rawarr> invoices)
        {
            JArray invoicseArray = new JArray();
            foreach (var invoice in invoices)
            {
                invoicseArray.Add(new JObject
                {
                    {"transaction_id", invoice.TransactionID},
                    {"amount", invoice.Amount},
                    {"currency_code", invoice.CurrencyCode},
                    {"transaction_date", invoice.TransactionDate},
                    {"status", invoice.Status},
                });
            }

            JObject data = new JObject { { "invoice", invoicseArray } };

            await restService.PostAsync(Constants.RestInvoiceUpload, data);
        }
    }
}
