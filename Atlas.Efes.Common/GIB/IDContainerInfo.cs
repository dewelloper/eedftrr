using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    [XmlRoot(ElementName = "ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
    public class IDContainerInfo : BasePropertyChanged
    {

        private string _value;
        [XmlText]
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }



        [XmlAttribute("schemeID")]
        public string schemeID = "VKN_TCKN";


       
    }
}
