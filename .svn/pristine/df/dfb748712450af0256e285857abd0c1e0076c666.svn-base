using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace Atlas.Efes.Common.GIB
{
    [DataContract]
    public class ElementListInfo
    {
        [DataMember]
        [XmlElement("Invoice", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2")]
        public InvoiceInfo Invoice { get; set; }

        [DataMember]
        [XmlElement("ApplicationResponse", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2")]
        public ApplicationResponseInfo ApplicationResponse { get; set; }

        private List<ProcessUserAccountInfo> processUserAccounts = new List<ProcessUserAccountInfo>();

        [DataMember]
        [XmlElement("ProcessUserAccount", Namespace = "http://www.hr-xml.org/3")]
        public List<ProcessUserAccountInfo> ProcessUserAccounts
        {
            get
            {
                if (processUserAccounts == null)
                {
                    processUserAccounts = new List<ProcessUserAccountInfo>();
                }
                return processUserAccounts;
            }
            set { processUserAccounts = value; }
        }

    }
}
