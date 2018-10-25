using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GibModel.UserProcess
{
    public class DataArea
    {
        [XmlElement("Process", Namespace = "http://www.openapplications.org/oagis/9")]
        public string Process { get; set; }

        private List<UserAccount> userAccounts;

        [XmlElement("UserAccount")]
        public List<UserAccount> UserAccount
        {
            get
            {
                if (userAccounts == null)
                {
                    userAccounts = new List<UserAccount>();
                }
                return userAccounts;
            }
            set { userAccounts = value; }
        }

    }
}


