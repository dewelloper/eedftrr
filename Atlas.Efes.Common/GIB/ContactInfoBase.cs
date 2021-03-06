﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    [DataContract]
    public abstract class ContactInfoBase
    {
        [DataMember]
        public string Identifier { get; set; }



        private List<ContactInformation> contactInformations;

        [DataMember]
        [XmlElement(ElementName = "ContactInformation")]
        public List<ContactInformation> ContactInformations
        {
            get
            {
                if (contactInformations == null)
                {
                    contactInformations = new List<ContactInformation>();
                }
                return contactInformations;
            }
            set { contactInformations = value; }
        }

    }
}
