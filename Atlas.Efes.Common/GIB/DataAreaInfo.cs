using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    public class DataAreaInfo
    {
        [DataMember]
        [XmlElement("Process", Namespace = "http://www.openapplications.org/oagis/9")]
        public string Process { get; set; }

        private List<UserAccountInfo> userAccounts;

        [DataMember]
        [XmlElement("UserAccount")]
        public List<UserAccountInfo> UserAccounts
        {
            get
            {
                if (userAccounts == null)
                {
                    userAccounts = new List<UserAccountInfo>();
                }
                return userAccounts;
            }
            set { userAccounts = value; }
        }

    }
}
