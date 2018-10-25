using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    [DataContract]
    public class UserRoleInfo
    {
        [DataMember]
        [XmlElement("RoleCode")]
        public string RoleCode { get; set; }

        [DataMember]
        [XmlElement("RoleName")]
        public string RoleName { get; set; }
    }
}
