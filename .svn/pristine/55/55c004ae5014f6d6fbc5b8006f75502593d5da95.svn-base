using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    [DataContract]
    public class AccountPersonInfo
    {
        [DataMember]
        [XmlElement("FormattedName")]
        public string FormattedName { get; set; }

        [DataMember]
        [XmlElement("GivenName")]
        public string GivenName { get; set; }

        [DataMember]
        [XmlElement("MiddleName", Namespace = "http://www.openapplications.org/oagis/9")]
        public string MiddleName { get; set; }

        [DataMember]
        [XmlElement("FamilyName")]
        public string FamilyName { get; set; }
    }
}
