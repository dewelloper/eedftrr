using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    [DataContract]
    public class DocumentIdentificationInfo
    {
        private string standard = "UBLTR";
        public string Standard
        {
            get { return standard; }
            set { standard = value; }
        }
        
        
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
