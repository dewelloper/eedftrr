using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GibModel
{
    [DataContract]
    public class DocumentIdentificationInfo
    {
        [DataMember]
        public string Standard { get; set; }
        
        [DataMember]
        public string TypeVersion { get; set; }
        
        [DataMember]
        public string InstanceIdentifier { get; set; }
        
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string CreationDateAndTime { get; set; }
    }
}
