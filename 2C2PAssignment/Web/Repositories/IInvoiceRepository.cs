using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repositories
{
    public interface IInvoiceRepository
    {
        Task UploadInvoices(List<Rawarr> invoices);
    }
}
