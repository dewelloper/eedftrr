using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GibModel
{
    //listAgencyName="United Nations Economic Commission for Europe" listID="ISO 4217 Alpha" listName="Currency" listVersionID="2001"
    public class DocumentCurrencyCodeInfo
    {
        [XmlAttribute("listAgencyName")]
        public string listAgencyName = "United Nations Economic Commission for Europe";

        [XmlAttribute("listID")]
        public string listID = "ISO 4217 Alpha";

        [XmlAttribute("listName")]
        public string listName = "Currency";

        [XmlAttribute("listVersionID")]
        public string listVersionID = "2001";

        [XmlText]
        public string Value { get; set; }
    }
}
