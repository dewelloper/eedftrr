using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    [DataContract]
    public class ElementsInfo
    {
        [DataMember]
        [XmlElement("ElementType")]
        public string ElementType { get; set; }
        
        [DataMember]
        [XmlElement("ElementCount")]
        public int ElementCount { get; set; }

        private List<ElementListInfo> elementList;
        
        [DataMember]
        [XmlElement("ElementList")]
        public List<ElementListInfo> ElementList
        {
            get
            {
                if (elementList == null)
                {
                    elementList = new List<ElementListInfo>();
                }
                return elementList;
            }
            set { elementList = value; }
        }

    }
}
