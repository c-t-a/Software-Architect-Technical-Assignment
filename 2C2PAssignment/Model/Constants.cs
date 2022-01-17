using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Constants
    {
        public static string RestBaseUrl { get; set; }

        public static string RestDatabaseCreate { get => $"{RestBaseUrl}database/create"; }

        public static string RestInvoiceUpload { get => $"{RestBaseUrl}invoices/upload"; }
    }
}
