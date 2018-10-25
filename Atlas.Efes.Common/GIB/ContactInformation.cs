﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    [DataContract]
    public class ContactInformation
    {
        [DataMember]
        public string Contact { get; set; }

        [DataMember]
        public string ContactTypeIdentifier { get; set; }
    }
}
