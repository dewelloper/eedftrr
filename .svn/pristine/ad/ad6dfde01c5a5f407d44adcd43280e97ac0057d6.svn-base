using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.GIBModel
{
    [DataContract]
    [XmlRoot(ElementName = "Package", Namespace = "http://www.efatura.gov.tr/package-namespace")]
    [Serializable]
    public class PackageInfo
    {
        [XmlElement(ElementName = "Elements", Namespace = "")]
        public ElementsInfo Elements { get; set; }

    }
}
