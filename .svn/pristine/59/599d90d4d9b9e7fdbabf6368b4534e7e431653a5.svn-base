using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.GIBModel
{
    public class SenderPartyInfo
    {
        [XmlIgnore]
        public string Alias;


        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string WebsiteURI { get; set; }

        private List<PartyIdentificationInfo> partyIdentifications;
        [XmlElement(ElementName = "PartyIdentification", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public List<PartyIdentificationInfo> PartyIdentifications
        {
            get
            {
                if (partyIdentifications == null)
                {
                    partyIdentifications = new List<PartyIdentificationInfo>();
                }
                return partyIdentifications;
            }
            set { partyIdentifications = value; }
        }



        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public PartyNameInfo PartyName { get; set; }


        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public PostalAddressInfo PostalAddress { get; set; }

        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public PartyTaxSchemeInfo PartyTaxScheme { get; set; }

        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public ContactIndicatorInfo Contact { get; set; }

        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public PersonInfo Person { get; set; }
    }
}
