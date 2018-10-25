using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.Efes.Common.Model
{
    public class CustomerInfo
    {
        public int ID { get; set; }

        public CustomerTypeInfo CustomerType { get; set; }
        public string Identification { get; set; }
        public string Title { get; set; }
        public string MersisNo { get; set; }
        public string CommercialNumber { get; set; }
        public string TaxOffice { get; set; }
        public string SenderUnitAddress { get; set; }
        public string ReceiverUnitAddress { get; set; }
        public string DocumentShourtcut { get; set; }
        public string WebsiteURI { get; set; }

        public AddressInfo AddressInfo { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}
