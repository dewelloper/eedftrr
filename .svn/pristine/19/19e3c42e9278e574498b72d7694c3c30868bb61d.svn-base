using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    [DataContract]
    public class ApplicationAreaInfo
    {
        [DataMember]
        [XmlElement("Sender", Namespace = "http://www.openapplications.org/oagis/9")]
        public AreaSenderInfo AreaSenderInfo { get; set; }

        [DataMember]
        [XmlElement("Receiver", Namespace = "http://www.openapplications.org/oagis/9")]
        public AreaReceiverInfo AreaReceiverInfo { get; set; }

        private string creationDateAndTime;



        [DataMember]
        [XmlElement("CreationDateAndTime", Namespace = "http://www.openapplications.org/oagis/9")]
        public string CreationDateAndTime
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
            }
            set { creationDateAndTime = value; }
        }


        [DataMember]
        [XmlElement("Signature", Namespace = "http://www.openapplications.org/oagis/9")]
        public string SignatureText { get; set; }

        [DataMember]
        [XmlElement("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public ApplicationSignatureInfo Signature { get; set; }
    }
}
