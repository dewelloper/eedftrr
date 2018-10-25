using Atlas.Efes.Common.Container;
using System.Xml;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    [XmlRoot(ElementName = "Signature", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class SignatureInfo
    {
        public IDContainerInfo ID { get; set; }


        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public SignatoryPartyInfo SignatoryParty { get; set; }

        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public DigitalSignatureAttachmentInfo DigitalSignatureAttachment { get; set; }

        [XmlText]
        public string EncryptedData { get; set; }
    }
}
