using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.GIBModel
{
    [DataContract]
    public class ElementsInfo
    {
        public string ElementType { get; set; }
        public int ElementCount { get; set; }

        private List<ElementListInfo> elementList;

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
