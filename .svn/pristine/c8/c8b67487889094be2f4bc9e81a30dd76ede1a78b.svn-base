using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    [DataContract]
    public class UserAccountInfo
    {
        [DataMember]
        [XmlElement("UserID")]
        public string UserID { get; set; }

        [DataMember]
        [XmlElement("PersonName")]

        public AccountPersonInfo PersonInfo { get; set; }

        [DataMember]
        [XmlElement("UserRole")]
        public UserRoleInfo UserRole { get; set; }

        [DataMember]
        [XmlElement("AuthorizedWorkScope")]
        public AuthorizedWorkscopeInfo AuthorizedWorkscope { get; set; }

        [DataMember]
        [XmlElement("AccountConfiguration")]
        public AccountConfigurationInfo AccountConfiguration { get; set; }
    }
}
