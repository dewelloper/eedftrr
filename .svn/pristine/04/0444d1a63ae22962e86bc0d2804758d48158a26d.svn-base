using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GibModel
{
    public class ContactIndicatorInfo
    {
        /*
         *  <cbc:Telephone>03123021700</cbc:Telephone>
  <cbc:Telefax>03123021539</cbc:Telefax>
  <cbc:ElectronicMail>efatura@gelirler.gov.tr</cbc:ElectronicMail>
         */
        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string Telephone { get; set; }

        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string Telefax { get; set; }

        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string ElectronicMail { get; set; }
    }
}
