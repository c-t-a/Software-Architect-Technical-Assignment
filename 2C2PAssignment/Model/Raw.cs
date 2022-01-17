using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Raw
    {
        [Name("id")]
        [Required]
        public string TransactionID { get; set; }

        [Name("amount")]
        [Required]
        public decimal Amount { get; set; }

        [Name("currency_code")]
        [Required]
        public string CurrencyCode { get; set; }

        [Name("transaction_date")]
        [Required]
        public DateTime TransactionDate { 
            get;
            set; }

        [Name("status")]
        [Required]
        public string Status { get; set; }

        //private string test;
        //public int t = 3;

        //public string Test {
        //    get {
        //        return test; 
        //    }
        //    set {
        //        RegionInfo Region = CultureInfo
        //            .GetCultures(CultureTypes.SpecificCultures)
        //            .Select(ct => new RegionInfo(ct.LCID))
        //            .Where(ri => ri.ISOCurrencySymbol == value).FirstOrDefault();
        //        if (Region != null)
        //        {
        //            test = value;
        //        }else {
        //            test = null;
        //        }

        //        //if (value.Length < 3)
        //        //{
        //        //    test = value;
        //        //}
        //        //else {
        //        //    test = "Three";
        //        //}
        //    }
        //}

        //private char statusInChar;
        //private string statusinString;

        //public char sss{
        //    get { return statusInChar; }
        //}

        //public void setChar(string _statusInString) {
        //    statusinString = _statusInString;
        //    if (statusinString == "Done")
        //    {
        //        statusInChar = 'D';
        //    }
        //    else
        //    {
        //        statusInChar = 'B';
        //    }
        //}
    }
}
