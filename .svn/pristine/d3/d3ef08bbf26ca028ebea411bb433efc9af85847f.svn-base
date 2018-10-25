using Atlas.Efes.Common.GIB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Atlas.Efes.Common.GIB
{
    [XmlRoot("Invoice", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2")]
    public class InvoiceInfo : BasePropertyChanged,IElementInfo
    {

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();

        public void CreateNamespace()
        {
            xmlns.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            xmlns.Add("xades", "http://uri.etsi.org/01903/v1.3.2#");
            xmlns.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
            xmlns.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            xmlns.Add("ccts", "urn:un:unece:uncefact:documentation:2");
            xmlns.Add("ubltr", "urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents");
            xmlns.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
            xmlns.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            xmlns.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            xmlns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
        }

        public InvoiceInfo()
        {
            CreateNamespace();
            /*
            xmlns.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            xmlns.Add("xades", "http://uri.etsi.org/01903/v1.3.2#");
            xmlns.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
            xmlns.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            xmlns.Add("ccts", "urn:un:unece:uncefact:documentation:2");
            xmlns.Add("ubltr", "urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents");
            xmlns.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
            xmlns.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            xmlns.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            xmlns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            */
        }


        [XmlElement(ElementName = "UBLExtensions", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
        [DataMember]
        public UBLExtensionsInfo UBLExtensions { get; set; }

        [XmlAttribute(Namespace = XmlSchema.InstanceNamespace)]
        [DataMember]
        public string schemaLocation = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2 UBL-Invoice-2.1.xsd";


        [XmlElement(ElementName = "UBLVersionID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string UBLVersionID { get; set; }

        [XmlElement(ElementName = "CustomizationID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string CustomizationID { get; set; }

        [XmlElement(ElementName = "ProfileID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string ProfileID { get; set; }

        [XmlElement(ElementName = "ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string ID { get; set; }

        [XmlElement(ElementName = "CopyIndicator", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public bool CopyIndicator { get; set; }

        [XmlElement(ElementName = "UUID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string UUID { get; set; }

        [XmlElement(ElementName = "IssueDate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string IssueDate { get; set; }

        [XmlElement(ElementName = "InvoiceTypeCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string InvoiceTypeCode { get; set; }

        [XmlElement(ElementName = "Note", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public List<string> Note { get; set; }


        [XmlElement(ElementName = "DocumentCurrencyCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public DocumentCurrencyCodeInfo DocumentCurrencyCode { get; set; }

        [XmlElement(ElementName = "LineCountNumeric", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public int LineCountNumeric { get; set; }


        [XmlElement(ElementName = "IssueTime", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string IssueTime { get; set; }

        [XmlElement(ElementName = "TaxCurrencyCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string TaxCurrencyCode { get; set; }

        [XmlElement(ElementName = "PricingCurrencyCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string PricingCurrencyCode { get; set; }

        [XmlElement(ElementName = "PaymentCurrencyCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string PaymentCurrencyCode { get; set; }

        [XmlElement(ElementName = "PaymentAlternativeCurrencyCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        [DataMember]
        public string PaymentAlternativeCurrencyCode { get; set; }


        [XmlElement(ElementName = "AdditionalDocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public AdditionalDocumentReferenceInfo AdditionalDocumentReference { get; set; }

        [XmlElement(ElementName = "Signature", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public SignatureInfo Signature { get; set; }

        [XmlElement(ElementName = "AccountingSupplierParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public AccountingSupplierPartyInfo AccountingSupplierPartyInfo { get; set; }

        private AccountingCustomerPartyInfo customerPartyInfo;


        [XmlElement(ElementName = "AccountingCustomerParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public AccountingCustomerPartyInfo AccountingCustomerPartyInfo
        {
            get { return customerPartyInfo; }
            set
            {
                customerPartyInfo = value;
                RaisePropertyChanged("AccountingCustomerPartyInfo");
            }
        }



        [XmlElement(ElementName = "TaxTotal", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public TaxTotalInfo TaxTotalInfo { get; set; }

        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public LegalMonetaryTotalInfo LegalMonetaryTotal { get; set; }


        private List<InvoiceLineInfo> invoiceLineInfos;

        [XmlElement("InvoiceLine", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public List<InvoiceLineInfo> InvoiceLineInfos
        {
            get
            {
                if (invoiceLineInfos == null)
                {
                    invoiceLineInfos = new List<InvoiceLineInfo>();
                }
                return invoiceLineInfos;
            }
            set { invoiceLineInfos = value; }
        }


        [XmlElement(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public PaymentMeansInfo PaymentMeans { get; set; }

        [XmlElement(ElementName = "PaymentTerms", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public PaymentTermsInfo PaymentTerms { get; set; }

        [XmlElement(ElementName = "TaxExchangeRate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public TaxExchangeRateInfo TaxExchangeRate { get; set; }

        [XmlElement(ElementName = "PricingExchangeRate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public PriceExchangeRateInfo PricingExchangeRate { get; set; }

        [XmlElement(ElementName = "PaymentExchangeRate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public PaymentExchangeRateInfo PaymentExchangeRate { get; set; }

        [XmlElement(ElementName = "PaymentAlternativeExchangeRate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        [DataMember]
        public PaymentAlternativeExchangeRateInfo PaymentAlternativeExchangeRate { get; set; }

        [XmlText]
        public string DocumentID { get; set; }
    }
}
