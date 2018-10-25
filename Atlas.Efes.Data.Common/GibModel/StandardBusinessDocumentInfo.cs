using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GibModel
{
    [DataContract]
    [XmlRoot(ElementName = "StandardBusinessDocument", Namespace = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader")]
    public class StandardBusinessDocumentInfo
    {
        [DataMember]
        public StandardBusinessDocumentHeaderInfo StandardBusinessDocumentHeader { get; set; }

        [XmlAttribute(Namespace = XmlSchema.InstanceNamespace)]
        public string schemaLocation = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader PackageProxy_1_2.xsd";

        [DataMember]
        [XmlElement(ElementName = "Package", Namespace = "http://www.efatura.gov.tr/package-namespace")]
        public PackageInfo Package { get; set; }

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

        public StandardBusinessDocumentInfo()
        {
            
            namespaces.Add("sh", "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader");
            namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            namespaces.Add("ef", "http://www.efatura.gov.tr/package-namespace");
        }

    }
}
