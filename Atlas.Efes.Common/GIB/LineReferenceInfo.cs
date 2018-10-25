using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    public class LineReferenceInfo
    {
        [XmlElement(ElementName = "LineID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string LineID { get; set; }

        public DocumentReferenceInfo DocumentReference { get; set; }
    }
}
