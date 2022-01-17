using API.Models.Entities;
using Model;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Models.Providers
{
    public interface IInvoiceAccessProvider
    {
        List<Invoice> GetInvoices();
        Task UploadInvoice(JObject data);
    }
}
