using Atlas.Efes.Common.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    public class TaxTotalInfo
    {
        [XmlElement(ElementName = "TaxAmount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public AmountContainerInfo TaxAmount { get; set; }



        private List<TaxSubTotalInfo> taxSubTotals;
        [XmlElement(ElementName = "TaxSubtotal", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public List<TaxSubTotalInfo> TaxSubTotals
        {
            get { return taxSubTotals; }
            set
            {
                taxSubTotals = value;
            }
        }

    }
}
