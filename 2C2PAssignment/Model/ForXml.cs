using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    [XmlRoot(ElementName = "Transcations")]
    public class ForXml
    {
        public ForXml()
        {
            Transcations = new List<Transcation>();
        }

        [XmlElement(ElementName = "Transaction", IsNullable = false)]
        public List<Transcation> Transcations { get; set; }

        public Transcation this[string id]
        {
            get { return Transcations.FirstOrDefault(s => string.Equals(s.ID, id, StringComparison.OrdinalIgnoreCase)); }
        }
    }

    public class Transcation
    {
        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlElement(ElementName = "PaymentDetails", IsNullable = false)]
        public PaymentDetail PaymentDetails { get; set; }

        [XmlElement(ElementName = "TranscationDate", IsNullable = false)]
        public DateTime TransactionDate { get; set; }

        [XmlElement(ElementName = "Status", IsNullable = false)]
        public string Status { get; set; }
    }

    public class PaymentDetail {
        [XmlElement(ElementName = "Amount", IsNullable = false)]
        public decimal Amount { get; set; }

        [XmlElement(ElementName = "CurrencyCode", IsNullable = false)]
        public string CurrencyCode { get; set; }
    }
}
