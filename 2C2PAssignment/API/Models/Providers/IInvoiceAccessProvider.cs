using API.Models.Entities;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Models.Providers
{
    public interface IInvoiceAccessProvider
    {
        List<InvoiceEntity> GetInvoices();
        void UploadInvoice(JObject data);
    }
}
