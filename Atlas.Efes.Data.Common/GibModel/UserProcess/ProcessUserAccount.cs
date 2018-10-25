using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GibModel.UserProcess
{
    public class ProcessUserAccount
    {

        /*
        * 
        * xmlns="http://www.hr-xml.org/3"
        * xmlns:xades="http://uri.etsi.org/01903/v1.3.2#" 
        * xmlns:oa="http://www.openapplications.org/oagis/9" 
        * xmlns:ds="http://www.w3.org/2000/09/xmldsig#" 
        * xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
        * releaseID="releaseID0" 
        * xsi:schemaLocation=""
        * 
        * 
        */

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

        //[XmlAttribute(Namespace = XmlSchema.InstanceNamespace)]
        //public string schemaLocation = "http://www.hr-xml.org/3 ../HRXML/UserAccount.xsd         http://www.w3.org/2000/09/xmldsig# xmldsig-core-schema.xsd         http://uri.etsi.org/01903/v1.3.2# XAdES.xsd";


        public ProcessUserAccount()
        {

            namespaces.Add("xades", "http://uri.etsi.org/01903/v1.3.2#");
            namespaces.Add("oa", "http://www.openapplications.org/oagis/9");
            namespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            namespaces.Add("hr", "http://www.hr-xml.org/3");
        }


        [XmlElement(ElementName = "ApplicationArea", Namespace = "http://www.openapplications.org/oagis/9")]
        public ApplicationArea ApplicationArea { get; set; }

        public DataArea DataArea { get; set; }
    }
}
