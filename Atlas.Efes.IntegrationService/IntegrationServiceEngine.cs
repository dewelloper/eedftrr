﻿using Atlas.Efes.Common.GIB;
using Atlas.Efes.Common.Model;
using Atlas.Efes.Common.ServiceModel;
using Atlas.Efes.Core;
using Atlas.Efes.DataContext;
using Atlas.Efes.IntegrationService.SenderIntegrationProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.Efes.IntegrationService
{
    public class IntegrationServiceEngine : IDisposable
    {
        private string EnvelopeLogService = "ServiceEngine";
        private string EnvelopeLogType = "SENDERENVELOPE";

        private string Engine_Process1 = "Engine-0001";
        private string Engine_Process2 = "Engine-0002";

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        internal DocumentResponse SendDocument(InvoiceInfo invoice, PartyInfo supplierPartyInfo)
        {
            DocumentResponse response = new DocumentResponse();
            string instanceIdentifier = AppHelper.GetGuid();

            if (invoice == null)
            {
                response.Message = "object has no invoice";
                return response;
            }

            invoice.xmlns = new System.Xml.Serialization.XmlSerializerNamespaces();
            invoice.CreateNamespace();
            invoice.schemaLocation = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2 UBL-Invoice-2.1.xsd";

            if (string.IsNullOrEmpty(invoice.ID))
            {
                invoice.ID = string.Format("{0}{1}", "ATL2015000", DateTime.Now.ToString("hhmmss"));
            }

            if (string.IsNullOrEmpty(invoice.UUID))
            {
                invoice.UUID = AppHelper.GetGuid();
            }

            if (string.IsNullOrEmpty(invoice.InvoiceTypeCode))
            {
                response.Message = "Check InvoiceTypeCode.Invalid InvoiceTypeCode";
                return response;
            }
            else
            {
                if (!(invoice.InvoiceTypeCode == "SATIS" || invoice.InvoiceTypeCode == "IADE"))
                {
                    response.Message = "Check InvoiceTypeCode.Invalid InvoiceTypeCode";
                    return response;
                }
            }

            invoice.AccountingSupplierPartyInfo = new AccountingSupplierPartyInfo()
            {
                Party = supplierPartyInfo,
            };

            /*
            string appPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            string schemaPath = appPath + "\\maindoc\\" + "UBL-Invoice-2.1.xsd";

            string xml = invoice.SerializeObject<InvoiceInfo>();
            xml = xml.Replace("cac:ID", "cbc:ID");

            XmlSchemaValidator validator = new XmlSchemaValidator();
            bool schemaValidation = validator.ValidXmlDoc(xml, "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2", schemaPath);
            if (schemaValidation == false)
            {

                EnvelopeLogDataContext.Instance.EnvelopeLog(instanceIdentifier, EnvelopeLogService, EnvelopeLogType,
                                                            "Schematron Failed", Engine_Process1);

                EnvelopeLogDataContext.Instance.LogDetails(instanceIdentifier, validator.ValidationError);
                response.Message = validator.ValidationError;
                return response;
            }
            

            EnvelopeLogDataContext.Instance.EnvelopeLog(instanceIdentifier, EnvelopeLogService, EnvelopeLogType,
                                                        "Schematron Success", "Engine0001");

            invoice.UBLExtensions = new UBLExtensionsInfo();
            invoice.UBLExtensions.UBLExtension = new UBLExtensionInfo();
            invoice.UBLExtensions.UBLExtension.ExtensionContent = new ExtensionContentInfo();

            if (invoice.Signature == null)
            {
                invoice.Signature = new SignatureInfo();
                invoice.Signature.DigitalSignatureAttachment = new DigitalSignatureAttachmentInfo();
                invoice.Signature.DigitalSignatureAttachment.ExternalReference = new ExternalReferenceInfo();
                invoice.Signature.DigitalSignatureAttachment.ExternalReference.URI = string.Format("{0}{1}", "#", invoice.ID);

                invoice.Signature.ID = new IDContainerInfo()
                {
                    schemeID = "VKN_TCKN",
                    Value = supplierPartyInfo.PartyIdentifications[0].ID.Value,
                };

                invoice.Signature.SignatoryParty = new SignatoryPartyInfo()
                {
                    Contact = supplierPartyInfo.Contact,
                    PostalAddress = supplierPartyInfo.PostalAddress,
                    PartyIdentification = supplierPartyInfo.PartyIdentifications[0],
                    PartyTaxScheme = supplierPartyInfo.PartyTaxScheme,
                };
            }
             */

            StandardBusinessDocumentInfo businesDocument = new StandardBusinessDocumentInfo();
            businesDocument.StandardBusinessDocumentHeader = new StandardBusinessDocumentHeaderInfo();
            businesDocument.StandardBusinessDocumentHeader.DocumentIdentification = new DocumentIdentificationInfo();
            businesDocument.StandardBusinessDocumentHeader.DocumentIdentification.CreationDateAndTime = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
            businesDocument.StandardBusinessDocumentHeader.DocumentIdentification.InstanceIdentifier = instanceIdentifier;
            businesDocument.StandardBusinessDocumentHeader.DocumentIdentification.Type = "SENDERENVELOPE";
            businesDocument.StandardBusinessDocumentHeader.DocumentIdentification.TypeVersion = "1.2";


            string receiverIdentification = invoice.AccountingCustomerPartyInfo.Party.
                                            PartyIdentifications.Where((f => f.ID.schemeID == "VKN")).FirstOrDefault().ID.Value;

            businesDocument.StandardBusinessDocumentHeader.Receiver = new ReceiverInfo();
            DatabaseResult<GIBUserInfo> databaseResult = PartyInfoDataContext.Instance.GetGibUserByIdentification(receiverIdentification, "GIB_PK_USERS");

            if (databaseResult.Result == null)
            {
                response.Message = "Oracle Server Has An Error";
                return response;
            }

            GIBUserInfo gibUser = databaseResult.Result;

            businesDocument.StandardBusinessDocumentHeader.Receiver.Identifier = gibUser.Address;
            businesDocument.StandardBusinessDocumentHeader.Receiver.ContactInformations = new List<ContactInformation>();
            businesDocument.StandardBusinessDocumentHeader.Receiver.ContactInformations.Add(new ContactInformation()
            {
                Contact = gibUser.Title,
                ContactTypeIdentifier = "UNVAN",
            });

            businesDocument.StandardBusinessDocumentHeader.Receiver.ContactInformations.Add(new ContactInformation()
            {
                Contact = gibUser.Identification,
                ContactTypeIdentifier = "VKN_TCKN",
            });

            string senderIdentifier = supplierPartyInfo.PartyIdentifications.Where(f => f.ID.schemeID == "VKN").FirstOrDefault().ID.Value;
            databaseResult = PartyInfoDataContext.Instance.GetGibUserByIdentification(senderIdentifier, "GIB_GB_USERS");

            if (databaseResult.Result == null)
            {
                response.Message = "Oracle Server Has An Error";
                return response;
            }

            gibUser = databaseResult.Result;

            businesDocument.StandardBusinessDocumentHeader.Sender = new SenderInfo();
            businesDocument.StandardBusinessDocumentHeader.Sender.Identifier = gibUser.Address;
            businesDocument.StandardBusinessDocumentHeader.Sender.ContactInformations = new List<ContactInformation>();
            businesDocument.StandardBusinessDocumentHeader.Sender.ContactInformations.Add(new ContactInformation()
            {
                Contact = gibUser.Title,
                ContactTypeIdentifier = "UNVAN",
            });

            businesDocument.StandardBusinessDocumentHeader.Sender.ContactInformations.Add(new ContactInformation()
            {
                Contact = gibUser.Identification,
                ContactTypeIdentifier = "VKN_TCKN",
            });

            businesDocument.Package = new PackageInfo();
            businesDocument.Package.Elements = new ElementsInfo();
            businesDocument.Package.Elements.ElementCount = 1;
            businesDocument.Package.Elements.ElementType = "INVOICE";
            businesDocument.Package.Elements.ElementList = new List<ElementListInfo>();
            businesDocument.Package.Elements.ElementList.Add(new ElementListInfo()
            {
                Invoice = invoice,
            });

            DocumentResponse documentResponse = null;

            using (SenderIntegrationServiceClient client = new SenderIntegrationServiceClient())
            {
                documentResponse = client.SendDocumentVS2(businesDocument);
            }

            if (documentResponse != null)
            {
                ApplicationResponse applicationResponse = GetApplicationResponse(instanceIdentifier);
                response.Message = applicationResponse.Description;
                response.InstanceIdentifier = instanceIdentifier;
                response.Hash = documentResponse.Hash;
            }

            return response;
        }


        private ApplicationResponse GetAppResponse(string instanceIdentifier)
        {
            ApplicationResponse response = null;

            using (SenderIntegrationServiceClient client = new SenderIntegrationServiceClient())
            {
                response = client.GetApplicationResponse(instanceIdentifier);
            }

            EnvelopeLogDataContext.Instance.EnvelopeLog(instanceIdentifier, EnvelopeLogService, "SYSTEMENVELOPE",
                                                        response.Description, response.ApplicationCode);
            return response;
        }

        internal ApplicationResponse GetApplicationResponse(string instanceIdentifier)
        {
            ApplicationResponse response = new ApplicationResponse();
            string responseXml = string.Empty;

            try
            {
                using (SenderIntegrationServiceClient client = new SenderIntegrationServiceClient())
                {
                    response = client.GetApplicationResponse(instanceIdentifier);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    response.Description = ex.InnerException.Message;
                }
                else
                {
                    response.Description = ex.Message;
                }
                response.ApplicationCode = "ERR-1";
            }

            return response;
        }
    }
}