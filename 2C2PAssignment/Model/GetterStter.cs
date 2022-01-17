using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    public class GetterStter
    {
        private string cCode;
        private DateTime tDate;
        private string cStatus;

        [Name("id")]
        [MaxLength (50)]
        public string TransactionID { get; set; }

        [Name("amount")]
        public decimal? Amount { get; set; }

        [Name("currency_code")]
        public string CurrencyCode {
            get { return cCode; }
            set {
                RegionInfo Region = CultureInfo
                        .GetCultures(CultureTypes.SpecificCultures)
                        .Select(ct => new RegionInfo(ct.LCID))
                        .Where(ri => ri.ISOCurrencySymbol == value).FirstOrDefault();
                if (Region != null)
                {
                    cCode = value;
                }
                else
                {
                    cCode = null;
                }
            }
        }

        [Name("transaction_date")]
        public DateTime? TransactionDate { get; set; }

        [Name("status")]
        public string Status {
            get { return cStatus; }
            set {
                if(value == "Failed" || value == "Rejected"){
                    cStatus = "A";
                }
                else if (value == "Failed" || value == "Rejected"){
                    cStatus = "R";
                }else if (value == "Finished" || value == "Done")
                {
                    cStatus = "D";
                }
                else
                {
                    cStatus = "N";
                }
            }
        }


    }
}
