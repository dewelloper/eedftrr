using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.GIBModel
{
    public class EmbeddedDocumentBinaryObjectInfo
    {
        [XmlText]
        public string Value 
        {
            get; 
            set; 
        }

        //characterSetCode="UTF-8" encodingCode="Base64" filename="TST2013000000766.xslt" mimeCode="application/xml"

        [XmlAttribute("characterSetCode")]
        public string characterSetCode = "UTF-8";


        [XmlAttribute("encodingCode")]
        public string encodingCode = "Base64";


        [XmlAttribute("filename")]
        public string filename = string.Empty;

        [XmlAttribute("mimeCode")]
        public string mimeCode = "application/xml";


    }
}
