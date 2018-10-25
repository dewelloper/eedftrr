using Atlas.Efes.Common.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GibModel
{
    public class TaxTotalInfo
    {
        [XmlElement(ElementName = "TaxAmount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public AmountContainerInfo TaxAmount { get; set; }

        [XmlElement(ElementName = "TaxSubtotal", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]

        private List<TaxSubTotalInfo> taxSubTotals;
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
