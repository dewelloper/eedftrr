using Atlas.Efes.Common.GIB;
using Atlas.Efes.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Atlas.Efes.Core.Extensions;
namespace Atlas.Efes.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            XmlDocument document = new XmlDocument();
            
            string xml = "<cac:Party xmlns:cac='urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2'><cbc:WebsiteURI xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' /><cac:PartyIdentification><cbc:ID schemeID='VKN' xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2'>1111113361</cbc:ID></cac:PartyIdentification><cac:PartyName><cbc:Name xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2'>ATLAS YAZILIM VE BİLİŞİM HİZM. TİC. A.Ş. Test Kullanıcısı</cbc:Name></cac:PartyName><cac:PostalAddress><cbc:Room xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' /><cbc:StreetName xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' /><cbc:BuildingName xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' /><cbc:BuildingNumber xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' /><cbc:CitySubdivisionName xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2'>-</cbc:CitySubdivisionName><cbc:CityName xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2'>İstanbul</cbc:CityName><cbc:PostalZone xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' /><cbc:Region xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' /><cac:Country><cbc:Name xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2'>Türkiye</cbc:Name></cac:Country></cac:PostalAddress><cac:PartyTaxScheme><cac:TaxScheme><cbc:Name xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2'>Bayrampaşa</cbc:Name></cac:TaxScheme></cac:PartyTaxScheme><cac:Contact><cbc:Telephone xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' /><cbc:Telefax xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' /><cbc:ElectronicMail xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2'>a@a.com</cbc:ElectronicMail></cac:Contact></cac:Party>";
            PartyInfo receiverPartyInfo = XmlSchemaParser.ParsePartyInfo(xml, "AccountingSupplierParty");
             * 
             *   document.Load(@"C:\Users\arif\Desktop\UTKU.xml");

            string invoiceXML = XmlSchemaParser.GetInvoiceNodeXML(document.InnerXml);

            XmlSchemaParser.ParseInvoice(invoiceXML);
             */


            SingServiceProxy.SignatureWSClient client = new SingServiceProxy.SignatureWSClient();
            ProcessUserAccountInfo p = new ProcessUserAccountInfo();

            ApplicationAreaInfo a = new ApplicationAreaInfo();
            a.AreaReceiverInfo = null;
            a.AreaSenderInfo = new AreaSenderInfo()
            {
                LogicalID = "999999",
            };

            a.Signature = new ApplicationSignatureInfo();
            p.ApplicationArea = a;

            InvoiceInfo i = new InvoiceInfo();
            i.UBLExtensions = new UBLExtensionsInfo();
            i.UBLExtensions.UBLExtension = new UBLExtensionInfo();
            i.UBLExtensions.UBLExtension.ExtensionContent = new ExtensionContentInfo();

            string xml = i.SerializeObject<InvoiceInfo>();

            var res = client.sign(xml.ToByteArray(), "1020409972", Guid.NewGuid().ToString(), 1);

            string test = Encoding.UTF8.GetString(res.signedFile);

        }
    }
}
