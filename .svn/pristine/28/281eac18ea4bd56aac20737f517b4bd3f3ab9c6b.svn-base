
using Atlas.Efes.Common.ServiceModel;
using Atlas.Efes.GibIntegrationService;
using GIB = Atlas.Efes.GibIntegrationService.GIBServiceProxy;
using System;
using System.Xml;
using Atlas.Efes.Common.GIB;
using Atlas.Efes.Core;
using Atlas.Efes.Core.Extensions;
using System.IO;
using Atlas.Efes.Common.GIB.Enums;
using System.Text;
using Atlas.Efes.SenderIntegrationService.SignatureServiceProxy;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

namespace Atlas.Efes.SenderIntegrationService
{

    public class SenderIntegrationService : ISenderIntegrationService
    {
        public DocumentResponse SendDocument(DocumentInfo documentInfo)
        {
            DocumentResponse documentResult = new DocumentResponse();

            GIB.documentReturnType response = null;

            try
            {
                GIBIntegrationClient instance = GIBIntegrationClient.CreateInstance();

                var documentType = new GIB.documentType()
                {
                    binaryData = new GIB.base64Binary()
                    {
                        contentType = "application/zip",
                        Value = documentInfo.BinaryData,
                    },
                    fileName = documentInfo.FileName,
                    hash = documentInfo.Hash,
                };

                response = instance.SendDocument(documentType);

                if (response != null)
                {
                    documentResult.Hash = response.hash;
                    documentResult.Message = response.msg;
                }
            }
            catch (Exception ex)
            {
                documentResult.Hash = null;
                documentResult.Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
            }

            return documentResult;
        }

        public ApplicationResponse GetApplicationResponse(string instanceIdentifier)
        {
            GIBIntegrationClient instance = GIBIntegrationClient.CreateInstance();
            string result = string.Empty;
            GIB.getAppRespRequestType request = new GIB.getAppRespRequestType()
            {
                instanceIdentifier = instanceIdentifier,
            };
            GIB.getAppRespResponseType response = null;
            string responseXml = string.Empty;
            ApplicationResponse applicationResponse = new ApplicationResponse();

            try
            {
                GIBIntegrationClient client = GIBIntegrationClient.CreateInstance();
                response = client.GetApplicationResponse(request);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response.applicationResponse);
                XmlNamespaceManager namespaces = new XmlNamespaceManager(doc.NameTable);
                namespaces.AddNamespace("sh", "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader");
                namespaces.AddNamespace("ef", "http://www.efatura.gov.tr/package-namespace");

                var elementListNode = doc.SelectSingleNode("//sh:StandardBusinessDocument/ef:Package/Elements/ElementList", namespaces);
                var applicationXml = elementListNode.InnerXml;
                doc.LoadXml(applicationXml);
                namespaces = new XmlNamespaceManager(doc.NameTable);
                namespaces.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                XmlNodeList responseNode = doc.SelectNodes("//cac:DocumentResponse/cac:LineResponse/cac:Response", namespaces);

                string applicationCode = responseNode[0].ChildNodes[2].InnerText;
                string description = responseNode[0].ChildNodes[3].InnerText;
                applicationResponse.ApplicationCode = applicationCode;
                applicationResponse.Description = description;
                applicationResponse.ResponseXml = response.applicationResponse;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    applicationResponse.Description = ex.InnerException.Message;
                }
                else
                {
                    applicationResponse.Description = ex.Message;
                }

                applicationResponse.ApplicationCode = "ERR-1";
            }


            return applicationResponse;
        }

        public DocumentResponse SendDocumentVS2(StandardBusinessDocumentInfo standartBusiness)
        {
            DocumentResponse documentResponse = new DocumentResponse();
            string errMessage = string.Empty;

            try
            {

                XmlDocument document = new XmlDocument();
                string baseApplicationPath = AppDomain.CurrentDomain.BaseDirectory;

                string senderIdentification = standartBusiness.StandardBusinessDocumentHeader.Sender.ContactInformations.Where(f => f.ContactTypeIdentifier == "VKN_TCKN").FirstOrDefault().Contact;
                string instanceIdentifier = standartBusiness.StandardBusinessDocumentHeader.DocumentIdentification.InstanceIdentifier;

                string fileDirectory = baseApplicationPath + senderIdentification;

                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }

                ElementsInfo elementsInfo = standartBusiness.Package.Elements;
                InvoiceInfo invoiceInfo = null;
                ApplicationResponseInfo applicationResponse = null;

                string elementXml = string.Empty;

                string fileInnerDirectory = fileDirectory;

                if (elementsInfo.ElementType == ElementTypeInfo.APPLICATIONRESPONSE.ToString())
                {
                    fileInnerDirectory = fileDirectory + @"\ApplicationResponse\";

                    if (!Directory.Exists(fileInnerDirectory))
                    {
                        Directory.CreateDirectory(fileInnerDirectory);
                    }

                    applicationResponse = elementsInfo.ElementList[0].ApplicationResponse;

                    if (applicationResponse.xmlns == null)
                    {
                        applicationResponse.xmlns = new System.Xml.Serialization.XmlSerializerNamespaces();
                    }

                    applicationResponse.CreateNamespace();

                    if (applicationResponse.UBLExtensions == null)
                    {
                        applicationResponse.UBLExtensions = new UBLExtensionsInfo();
                        applicationResponse.UBLExtensions.UBLExtension = new UBLExtensionInfo();
                        applicationResponse.UBLExtensions.UBLExtension.ExtensionContent = new ExtensionContentInfo();
                    }

                    string applicationXML = applicationResponse.SerializeObject<ApplicationResponseInfo>();
                    applicationXML = FixXML(applicationXML);
                    elementXml = CreateSignature(applicationXML, applicationResponse.ID);
                }

                if (elementsInfo.ElementType == ElementTypeInfo.INVOICE.ToString())
                {
                    fileInnerDirectory = fileDirectory + @"\Invoice\";

                    if (!Directory.Exists(fileInnerDirectory))
                    {
                        Directory.CreateDirectory(fileInnerDirectory);
                    }

                    invoiceInfo = elementsInfo.ElementList[0].Invoice;

                    if (invoiceInfo.xmlns == null)
                    {
                        invoiceInfo.xmlns = new System.Xml.Serialization.XmlSerializerNamespaces();
                    }

                    invoiceInfo.CreateNamespace();

                    if (invoiceInfo.UBLExtensions == null)
                    {
                        invoiceInfo.UBLExtensions = new UBLExtensionsInfo();
                        invoiceInfo.UBLExtensions.UBLExtension = new UBLExtensionInfo();
                        invoiceInfo.UBLExtensions.UBLExtension.ExtensionContent = new ExtensionContentInfo();
                    }

                    //LOOKAFTER
                    if (invoiceInfo.Signature == null)
                    {
                        invoiceInfo.Signature = new SignatureInfo()
                        {
                            DigitalSignatureAttachment = new DigitalSignatureAttachmentInfo()
                            {
                                ExternalReference = new ExternalReferenceInfo()
                                {
                                    URI = string.Format("#{0}", invoiceInfo.ID),
                                }
                            },
                            SignatoryParty = new SignatoryPartyInfo()
                            {
                                Contact = invoiceInfo.AccountingSupplierPartyInfo.Party.Contact,
                                PostalAddress = invoiceInfo.AccountingSupplierPartyInfo.Party.PostalAddress,
                                PartyIdentification = invoiceInfo.AccountingSupplierPartyInfo.Party.PartyIdentifications[0],
                                PartyTaxScheme = invoiceInfo.AccountingSupplierPartyInfo.Party.PartyTaxScheme,
                            },
                        };

                        invoiceInfo.Signature.ID = new IDContainerInfo();
                        invoiceInfo.Signature.ID.schemeID = "VKN_TCKN";
                        invoiceInfo.Signature.ID.Value = invoiceInfo.AccountingSupplierPartyInfo.Party.PartyIdentifications[0].ID.Value;
                    }

                    string invoiceXML = invoiceInfo.SerializeObject<InvoiceInfo>();
                    invoiceXML = FixXML(invoiceXML);
                    elementXml = CreateSignature(invoiceXML, invoiceInfo.ID);
                }


                if (standartBusiness.xmlns == null)
                {
                    standartBusiness.xmlns = new System.Xml.Serialization.XmlSerializerNamespaces();
                }

                standartBusiness.CreateNamespaces();

                string xmlFilePath = fileInnerDirectory + string.Format("{0}.xml", instanceIdentifier);
                string zipFilePath = fileInnerDirectory + string.Format("{0}.zip", instanceIdentifier);

                standartBusiness.schemaLocation = "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader PackageProxy_1_2.xsd";
                standartBusiness.StandardBusinessDocumentHeader.DocumentIdentification.Standard = "UBLTR";

                standartBusiness.Package.Elements.ElementList = new List<ElementListInfo>();
                standartBusiness.Package.Elements.ElementList.Add(new ElementListInfo()
                {

                });

                string standartBusinessXML = standartBusiness.SerializeObject<StandardBusinessDocumentInfo>(true);
                standartBusinessXML = standartBusinessXML.Replace("<ElementList />", elementXml);
                document.LoadXml(standartBusinessXML);
                document.Save(xmlFilePath);

                byte[] zipByteArray = FileProcess.CreateZipFile(xmlFilePath, zipFilePath);

                string fileName = string.Format("{0}.zip", instanceIdentifier);
                GIBIntegrationClient instance = GIBIntegrationClient.CreateInstance();
                GIB.documentReturnType gibResponse = null;
                var documentType = new GIB.documentType()
                {
                    binaryData = new GIB.base64Binary()
                    {
                        contentType = "application/zip",
                        Value = zipByteArray,
                    },
                    fileName = fileName,
                    hash = CryptoHelper.GetChecksum(zipFilePath),
                };

                gibResponse = instance.SendDocument(documentType);

                if (gibResponse != null)
                {
                    documentResponse.Hash = gibResponse.hash;
                    documentResponse.Message = gibResponse.msg;
                    documentResponse.InstanceIdentifier = instanceIdentifier;
                }
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(ex, true);
                string line = string.Empty;
                if (st != null)
                {
                    if (st.GetFrame(0) != null)
                    {
                        line = st.GetFrame(0).GetFileLineNumber().ToString();
                    }
                }
                errMessage = errMessage + ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source + "\r\n" + line;

                documentResponse.Message = errMessage;
            }

            return documentResponse;
        }


        private string CreateSignature(string xml, string id)
        {
            byte[] array = Encoding.UTF8.GetBytes(xml);
            SignatureWSClient client = new SignatureWSClient();
            var response = client.sign(array, "", id, 1);
            string signatureXml = Encoding.UTF8.GetString(response.signedFile);

            XmlDocument document = new XmlDocument();
            document.LoadXml(signatureXml);

            var declarations = document.ChildNodes.OfType<XmlNode>().Where(x => x.NodeType == XmlNodeType.XmlDeclaration).ToList();
            declarations.ForEach(x => document.RemoveChild(x));
            return "<ElementList >" + document.InnerXml + "</ElementList>";
        }

        private string FixXML(string xml)
        {
            return xml.Replace("cac:ID", "cbc:ID");
        }

        private void WriteLog(string mes)
        {
            string file = AppDomain.CurrentDomain.BaseDirectory + string.Format("{0}.{1}", AppHelper.GetGuid());

            File.Create(file).Close();
            File.AppendAllText(file, mes);
        }
    }
}
