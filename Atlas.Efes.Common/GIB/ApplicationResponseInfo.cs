using Atlas.Efes.Common.GIB.Interfaces;
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
    [XmlRoot("ApplicationResponse", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2")]
    public class ApplicationResponseInfo : IElementInfo
    {
        [XmlNamespaceDeclarations]
        [DataMember]
        public XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();


        public void CreateNamespace()
        {
            xmlns.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            xmlns.Add("xades", "http://uri.etsi.org/01903/v1.3.2#");
            xmlns.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
            xmlns.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            xmlns.Add("ccts", "urn:un:unece:uncefact:documentation:2");
            xmlns.Add("ubltr", "urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents");
            xmlns.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
            xmlns.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            xmlns.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            xmlns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
        }

        public ApplicationResponseInfo()
        {
            CreateNamespace();
        }

        [XmlAttribute(Namespace = XmlSchema.InstanceNamespace)]
        [DataMember]
        public string schemaLocation = "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2 ../xsd/maindoc/UBL-ApplicationResponse-2.1.xsd";


        [XmlElement(ElementName = "UBLExtensions", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
        [DataMember]
        public UBLExtensionsInfo UBLExtensions { get; set; }

        [XmlElement(ElementName = "UBLVersionID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string UBLVersionID { get; set; }

        [XmlElement(ElementName = "CustomizationID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string CustomizationID { get; set; }

        [XmlElement(ElementName = "ProfileID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string ProfileID { get; set; }

        [XmlElement(ElementName = "ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string ID { get; set; }

        [XmlElement(ElementName = "UUID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string UUID { get; set; }

        [XmlElement(ElementName = "IssueDate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string IssueDate { get; set; }

        [XmlElement(ElementName = "IssueTime", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string IssueTime { get; set; }

        private List<string> _notes;

        [XmlElement(ElementName = "Note", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public List<string> Notes
        {
            get
            {
                if (_notes == null)
                {
                    _notes = new List<string>();
                }
                return _notes;
            }
            set { _notes = value; }
        }



        [XmlElement(ElementName = "Signature", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public SignatureInfo Signature { get; set; }




        [XmlElement(ElementName = "SenderParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public SenderPartyInfo SenderParty { get; set; }

        [XmlElement(ElementName = "ReceiverParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public ReceiverPartyInfo ReceiverParty { get; set; }

        [XmlElement(ElementName = "DocumentResponse", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public DocumentResponseInfo DocumentResponse { get; set; }

    }
}
