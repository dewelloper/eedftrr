using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    [DataContract]
    [XmlRoot("ProcessUserAccount", Namespace = "http://www.hr-xml.org/3")]
    public class ProcessUserAccountInfo
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();

        public void CreateNamespaces()
        {
            xmlns.Add("xades", "http://uri.etsi.org/01903/v1.3.2#");
            xmlns.Add("oa", "http://www.openapplications.org/oagis/9");
            xmlns.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            xmlns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
        }

        [XmlAttribute]
        public string releaseID = "releaseID0";

        //http://www.hr-xml.org/3 ../HRXML/UserAccount.xsd         http://www.w3.org/2000/09/xmldsig# xmldsig-core-schema.xsd         http://uri.etsi.org/01903/v1.3.2# XAdES.xsd

        [XmlAttribute(Namespace = XmlSchema.InstanceNamespace)]
        [DataMember]
        public string schemaLocation = "http://www.hr-xml.org/3 ../HRXML/UserAccount.xsd         http://www.w3.org/2000/09/xmldsig# xmldsig-core-schema.xsd         http://uri.etsi.org/01903/v1.3.2# XAdES.xsd";
        public ProcessUserAccountInfo()
        {
            CreateNamespaces();
        }

        [DataMember]
        [XmlElement(ElementName = "ApplicationArea", Namespace = "http://www.openapplications.org/oagis/9")]
        public ApplicationAreaInfo ApplicationArea { get; set; }

        [DataMember]
        [XmlElement(ElementName = "DataArea")]
        public DataAreaInfo DataArea { get; set; }
    }
}
