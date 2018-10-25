using Atlas.Efes.Common.GIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Atlas.Efes.Core.Extensions;
using Atlas.Efes.Common.Model;

namespace Atlas.Efes.Engine
{
    public class XmlSchemaParser
    {
        public static void ExecuteParseXML<T>(Action<T> ExecuteAction)
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("INVALID XML.DOCUMENT PARSE ERROR");
            }
        }

        public static PartyInfo ParsePartyInfo(string xml, string nodeType, string executeType = "Service")
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);

            XmlNamespaceManager namespaces = new XmlNamespaceManager(document.NameTable);
            namespaces.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            namespaces.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");

            XmlNode supplierPartyNode = document.ChildNodes[0];

            AccountingSupplierPartyInfo accountingSupplierParty = new AccountingSupplierPartyInfo();
            accountingSupplierParty.Party = new PartyInfo();
            string webSiteUri = supplierPartyNode.GetNodeValueByName("cbc:WebsiteURI");

            if (webSiteUri != null)
            {
                accountingSupplierParty.Party.WebsiteURI = webSiteUri;
            }

            string xPath = string.Empty;
            if (executeType == "Service")
            {
                xPath = string.Format("//cac:Party//cac:PartyIdentification");
            }
            else
            {
                xPath = string.Format("cac:{0}//cac:Party//cac:PartyIdentification", nodeType);
            }

            XmlNodeList partyIdentifications = supplierPartyNode.SelectNodes(xPath, namespaces);

            List<PartyIdentificationInfo> parties = new List<PartyIdentificationInfo>();
            foreach (XmlNode item in partyIdentifications)
            {
                string identification = item.ChildNodes[0].InnerText;
                string schemeID = item.ChildNodes[0].Attributes["schemeID"].Value;

                parties.Add(new PartyIdentificationInfo()
                {
                    ID = new IDContainerInfo()
                    {
                        schemeID = schemeID,
                        Value = identification,
                    }
                });
            }
            accountingSupplierParty.Party.PartyIdentifications = parties;
            XmlNode partyNameNode = supplierPartyNode.SelectSingleNode("//cac:PartyName", namespaces);

            if (partyNameNode != null)
            {
                accountingSupplierParty.Party.PartyName = new PartyNameInfo();
                if (partyNameNode.ChildNodes[0] != null)
                {
                    accountingSupplierParty.Party.PartyName.Name = partyNameNode.ChildNodes[0].InnerText;
                }
            }

            //POSTAL ADDRESS PARSING....
            XmlNode postalAddressNode = supplierPartyNode.SelectSingleNode("//cac:PostalAddress", namespaces);
            if (postalAddressNode != null)
            {
                accountingSupplierParty.Party.PostalAddress = new PostalAddressInfo()
                {
                    BuildingName = postalAddressNode.GetNodeValueByName("cbc:BuildingName"),
                    StreetName = postalAddressNode.GetNodeValueByName("cbc:BuildingName"),
                    CitySubdivisionName = postalAddressNode.GetNodeValueByName("cbc:CitySubdivisionName"),
                    CityName = postalAddressNode.GetNodeValueByName("cbc:CityName"),
                    Region = postalAddressNode.GetNodeValueByName("cbc:Region"),
                    PostalZone = postalAddressNode.GetNodeValueByName("cbc:PostalZone"),
                    BuildingNumber = postalAddressNode.GetNodeValueByName("cbc:BuildingNumber"),
                    Room = postalAddressNode.GetNodeValueByName("cbc:Room"),
                };

                if (postalAddressNode.GetSingleNode("cac:Country", namespaces) != null)
                {
                    accountingSupplierParty.Party.PostalAddress.Country = new CountryIndicatorInfo();
                    var countryNode = postalAddressNode.GetSingleNode("cac:Country", namespaces);
                    if (countryNode.ChildNodes.Count > 0)
                    {
                        accountingSupplierParty.Party.PostalAddress.Country.Name = countryNode.ChildNodes[0].InnerText;
                    }
                }
            }

            XmlNode partyTaxSchemeNode = supplierPartyNode.SelectSingleNode("cac:PartyTaxScheme", namespaces);

            if (partyTaxSchemeNode != null)
            {
                accountingSupplierParty.Party.PartyTaxScheme = new PartyTaxSchemeInfo();
                accountingSupplierParty.Party.PartyTaxScheme.TaxScheme = new TaxSchemeInfo();
                if (partyTaxSchemeNode.ChildNodes[0] != null)
                {
                    if (partyTaxSchemeNode.ChildNodes[0].ChildNodes[0] != null)
                    {
                        accountingSupplierParty.Party.PartyTaxScheme.TaxScheme.Name = partyTaxSchemeNode.ChildNodes[0].ChildNodes[0].InnerText;
                    }
                }
            }

            XmlNode contactNode = supplierPartyNode.SelectSingleNode("cac:Contact", namespaces);

            if (contactNode != null)
            {
                accountingSupplierParty.Party.Contact = new ContactIndicatorInfo()
                {
                    ElectronicMail = contactNode.GetNodeValueByName("cbc:ElectronicMail"),
                    Telefax = contactNode.GetNodeValueByName("cbc:Telefax"),
                    Telephone = contactNode.GetNodeValueByName("cbc:Telephone"),
                };
            }

            XmlNode personNode = supplierPartyNode.SelectSingleNode("cac:Person", namespaces);

            if (personNode != null)
            {
                accountingSupplierParty.Party.Person = new PersonInfo()
                {
                    FamilyName = personNode.GetNodeValueByName("cbc:FamilyName"),
                    FirstName = personNode.GetNodeValueByName("cbc:FirstName"),
                };
            }

            return accountingSupplierParty.Party;
        }
        public static ApplicationResponseInfo ParseApplicationResponse(string xml)
        {
            ApplicationResponseInfo response = null;

            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);

            XmlNamespaceManager namespaces = new XmlNamespaceManager(document.NameTable);
            namespaces.AddNamespace("sh", "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader");
            namespaces.AddNamespace("ef", "http://www.efatura.gov.tr/package-namespace");
            namespaces.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            namespaces.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            namespaces.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");

            XmlNodeList nodeList = document.ChildNodes;
            XmlNode node = nodeList[0];

            response = new ApplicationResponseInfo()
            {
                CustomizationID = node.GetNodeValueByName("cbc:CustomizationID"),
                ID = node.GetNodeValueByName("cbc:ID").ToUpper(),
                IssueDate = node.GetNodeValueByName("cbc:IssueDate"),
                IssueTime = node.GetNodeValueByName("cbc:IssueTime"),
                UUID = node.GetNodeValueByName("cbc:UUID").ToUpper(),
                ProfileID = node.GetNodeValueByName("cbc:ProfileID"),
                UBLVersionID = node.GetNodeValueByName("cbc:UBLVersionID"),
                DocumentResponse = new DocumentResponseInfo()
                {
                    DocumentReference = new DocumentReferenceInfo()
                    {
                        ID = node.GetSingleNode("//cac:DocumentResponse/cac:DocumentReference", namespaces).GetNodeValueByName("cbc:ID").ToUpper(),
                        DocumentType = node.GetSingleNode("//cac:DocumentResponse/cac:DocumentReference", namespaces).GetNodeValueByName("cbc:DocumentType"),
                        DocumentTypeCode = node.GetSingleNode("//cac:DocumentResponse/cac:DocumentReference", namespaces).GetNodeValueByName("cbc:DocumentTypeCode"),
                        IssueDate = node.GetSingleNode("//cac:DocumentResponse/cac:DocumentReference", namespaces).GetNodeValueByName("cbc:IssueDate"),
                    },
                    LineResponse = new LineResponseInfo()
                    {
                        Response = new ResponseInfo()
                        {
                            Description = node.GetSingleNode("//cac:DocumentResponse/cac:LineResponse/cac:Response", namespaces).GetNodeValueByName("cbc:Description"),
                            ReferenceID = node.GetSingleNode("//cac:DocumentResponse/cac:LineResponse/cac:Response", namespaces).GetNodeValueByName("cbc:ReferenceID").ToUpper(),
                            ResponseCode = node.GetSingleNode("//cac:DocumentResponse/cac:LineResponse/cac:Response", namespaces).GetNodeValueByName("cbc:ResponseCode"),
                        }
                    }
                }
            };

            return response;

        }

        public static StandardBusinessDocumentInfo ParseStandardBusiness(XmlDocument document)
        {
            return CreateStandardBusiness(document);
        }
        public static StandardBusinessDocumentInfo ParseStandardBusiness(string xml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            return CreateStandardBusiness(document);
        }

        private static StandardBusinessDocumentInfo CreateStandardBusiness(XmlDocument document)
        {
            XmlNamespaceManager namespaces = new XmlNamespaceManager(document.NameTable);
            namespaces.AddNamespace("sh", "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader");
            namespaces.AddNamespace("ef", "http://www.efatura.gov.tr/package-namespace");
            XmlNode stardardBusinesNode = document.GetSingleNode("//sh:StandardBusinessDocument", namespaces);
            XmlNode headerInfoNode = stardardBusinesNode.GetSingleNode("//sh:StandardBusinessDocumentHeader", namespaces);
            XmlNode documentIdentificationNode = headerInfoNode.GetSingleNode("//sh:DocumentIdentification", namespaces);


            StandardBusinessDocumentInfo standardBusiness = new StandardBusinessDocumentInfo();
            standardBusiness.StandardBusinessDocumentHeader = new StandardBusinessDocumentHeaderInfo()
            {
                HeaderVersion = "1.0",
                DocumentIdentification = new DocumentIdentificationInfo()
                {
                    CreationDateAndTime = documentIdentificationNode.GetNodeValueByName("sh:CreationDateAndTime"),
                    Standard = documentIdentificationNode.GetNodeValueByName("sh:Standard"),
                    InstanceIdentifier = documentIdentificationNode.GetNodeValueByName("sh:InstanceIdentifier"),
                    Type = documentIdentificationNode.GetNodeValueByName("sh:Type"),
                    TypeVersion = documentIdentificationNode.GetNodeValueByName("sh:TypeVersion"),
                }
            };

            XmlNode senderInfo = headerInfoNode.GetSingleNode("sh:Sender", namespaces);


            standardBusiness.StandardBusinessDocumentHeader.Sender = new SenderInfo();
            standardBusiness.StandardBusinessDocumentHeader.Sender.Identifier = senderInfo.GetNodeValueByName("sh:Identifier");
            standardBusiness.StandardBusinessDocumentHeader.Sender.ContactInformations = new List<ContactInformation>();
            XmlNodeList contactsNode = senderInfo.SelectNodes("//sh:Sender//sh:ContactInformation", namespaces);

            foreach (XmlNode item in contactsNode)
            {
                var contactTypeIdentifier = item.OfType<XmlElement>().Where(f => f.Name == "sh:ContactTypeIdentifier").FirstOrDefault();
                var contact = item.OfType<XmlElement>().Where(f => f.Name == "sh:Contact").FirstOrDefault();

                if (contactTypeIdentifier != null && contact != null)
                {
                    ContactInformation contactInformation = new ContactInformation();
                    contactInformation.Contact = contact.InnerText;
                    contactInformation.ContactTypeIdentifier = contactTypeIdentifier.InnerText;
                    standardBusiness.StandardBusinessDocumentHeader.Sender.ContactInformations.Add(contactInformation);
                }
            }

            XmlNode receiverInfo = headerInfoNode.GetSingleNode("sh:Receiver", namespaces);

            standardBusiness.StandardBusinessDocumentHeader.Receiver = new ReceiverInfo();
            standardBusiness.StandardBusinessDocumentHeader.Receiver.Identifier = receiverInfo.GetNodeValueByName("sh:Identifier");
            standardBusiness.StandardBusinessDocumentHeader.Receiver.ContactInformations = new List<ContactInformation>();
            contactsNode = receiverInfo.SelectNodes("//sh:Receiver//sh:ContactInformation", namespaces);

            foreach (XmlNode item in contactsNode)
            {
                var contactTypeIdentifier = item.OfType<XmlElement>().Where(f => f.Name == "sh:ContactTypeIdentifier").FirstOrDefault();
                var contact = item.OfType<XmlElement>().Where(f => f.Name == "sh:Contact").FirstOrDefault();

                if (contactTypeIdentifier != null && contact != null)
                {
                    ContactInformation contactInformation = new ContactInformation();
                    contactInformation.Contact = contact.InnerText;
                    contactInformation.ContactTypeIdentifier = contactTypeIdentifier.InnerText;
                    standardBusiness.StandardBusinessDocumentHeader.Receiver.ContactInformations.Add(contactInformation);
                }
            }

            standardBusiness.Package = new PackageInfo();
            standardBusiness.Package.Elements = new ElementsInfo();
            standardBusiness.Package.Elements.ElementType = stardardBusinesNode.GetSingleNode("//sh:StandardBusinessDocument/ef:Package/Elements", namespaces).GetNodeValueByName("ElementType");
            standardBusiness.Package.Elements.ElementCount = int.Parse(stardardBusinesNode.GetSingleNode("//sh:StandardBusinessDocument/ef:Package/Elements", namespaces).GetNodeValueByName("ElementCount"));

            return standardBusiness;
        }

        public static string GetPartyNodeXML(string invoiceXML, string nodeType)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(invoiceXML);
            XmlNamespaceManager namespaces = new XmlNamespaceManager(document.NameTable);
            namespaces.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            namespaces.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            var node = document.ChildNodes[0];
            string xPath = string.Format("//cac:{0}//cac:Party", nodeType);
            return node.GetSingleNodeXml(xPath, namespaces, false);
        }

        public static string GetInvoiceNodeXML(string responseXML)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(responseXML);
            XmlNamespaceManager namespaces = new XmlNamespaceManager(document.NameTable);
            namespaces.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            namespaces.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            namespaces.AddNamespace("sh", "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader");
            namespaces.AddNamespace("ef", "http://www.efatura.gov.tr/package-namespace");
            var elementNode = document.GetSingleNode("//sh:StandardBusinessDocument/ef:Package/Elements/ElementList", namespaces);
            return elementNode.InnerXml;
        }

        public static string GetApplicationNodeXML(string responseXML)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(responseXML);
            XmlNamespaceManager namespaces = new XmlNamespaceManager(document.NameTable);
            namespaces.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            namespaces.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            namespaces.AddNamespace("sh", "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader");
            namespaces.AddNamespace("ef", "http://www.efatura.gov.tr/package-namespace");
            var elementNode = document.GetSingleNode("//sh:StandardBusinessDocument/ef:Package/Elements/ElementList", namespaces);

            return elementNode.InnerXml;
        }

        public static InvoiceInfo ParseInvoice(string invoiceXML)
        {

            XmlDocument document = new XmlDocument();
            document.LoadXml(invoiceXML);
            XmlNode node = document.ChildNodes[0];

            if (node == null)
            {
                throw new NotImplementedException("INVALID_INVOICE");
            }

            if (node.ChildNodes.Count == 0)
            {
                throw new NotImplementedException("INVALID_INVOICE");
            }


            InvoiceInfo invoiceInfo = new InvoiceInfo()
            {
                ID = node.GetNodeValueByName("cbc:ID"),
                InvoiceTypeCode = node.GetNodeValueByName("cbc:InvoiceTypeCode"),
                IssueDate = node.GetNodeValueByName("cbc:IssueDate"),
                ProfileID = node.GetNodeValueByName("cbc:ProfileID"),
                CustomizationID = node.GetNodeValueByName("cbc:CustomizationID"),
                //CopyIndicator = document.GetNodeValueByName("cbc:CopyIndicator"),
                IssueTime = node.GetNodeValueByName("cbc:IssueTime"),
                UUID = node.GetNodeValueByName("cbc:UUID"),
            };


            string accountingPartyXML = GetPartyNodeXML(invoiceXML, "AccountingCustomerParty");
            invoiceInfo.AccountingCustomerPartyInfo = new AccountingCustomerPartyInfo();
            invoiceInfo.AccountingCustomerPartyInfo.Party = ParsePartyInfo(accountingPartyXML, "AccountingCustomerParty");


            string supplierPartyXML = GetPartyNodeXML(invoiceXML, "AccountingSupplierParty");
            invoiceInfo.AccountingCustomerPartyInfo = new AccountingCustomerPartyInfo();
            invoiceInfo.AccountingCustomerPartyInfo.Party = ParsePartyInfo(supplierPartyXML, "AccountingSupplierParty");

            return invoiceInfo;
        }

        public static GIBUserInfo GetGIBUserInfo<T>(ContactInfoBase instance)
        {
            ContactInformation contact = instance.ContactInformations.Where(f => f.ContactTypeIdentifier == "VKN_TCKN").FirstOrDefault();
            ContactInformation contactTitle = instance.ContactInformations.Where(f => f.ContactTypeIdentifier == "UNVAN").FirstOrDefault();
            GIBUserInfo userInfo = new GIBUserInfo()
            {
                Address = instance.Identifier,
                Identification = contact.Contact,
                Title = contactTitle == null ? "" : contactTitle.Contact
            };
            return userInfo;
        }
    }
}
