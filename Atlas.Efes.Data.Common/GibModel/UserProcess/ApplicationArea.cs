using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GibModel.UserProcess
{
    [Serializable]
    public class ApplicationArea
    {
       
        [XmlElement("Sender", Namespace = "http://www.openapplications.org/oagis/9")]
        public UserSenderWorkspace UserSenderWorkspace { get; set; }

        [XmlElement("Receiver", Namespace = "http://www.openapplications.org/oagis/9")]
        public UserReceiverWorkspace UserReceiverWorkspace { get; set; }

        [XmlElement("CreationDateTime", Namespace = "http://www.openapplications.org/oagis/9")]
        public string CreationDateTime { get; set; }

        [XmlElement("Signature", Namespace = "http://www.openapplications.org/oagis/9")]
        public string Signature { get; set; }

    }
}
