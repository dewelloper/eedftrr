using Atlas.Efes.Common.GIB;
using Atlas.Efes.Common.GIB.Enums;
using Atlas.Efes.Common.Model;
using Atlas.Efes.Core;
using System;
using System.Collections.Generic;
using Atlas.Efes.Core.Extensions;

namespace Atlas.Efes.Engine
{
    public class AppIntegrationEngine
    {
        
        public StandardBusinessDocumentInfo CreateBusinessDocument(GIBUserInfo receiverInformation,
                                                                   GIBUserInfo senderInformation,
                                                                   DocumentTypeInfo typeInfo, ElementTypeInfo elementTypeInfo)
        {
            StandardBusinessDocumentInfo businessDocument = new StandardBusinessDocumentInfo();
            businessDocument.StandardBusinessDocumentHeader = new StandardBusinessDocumentHeaderInfo();
            businessDocument.StandardBusinessDocumentHeader.DocumentIdentification = new DocumentIdentificationInfo()
            {
                InstanceIdentifier = AppHelper.GetGuid(),
                TypeVersion = "1.2",
                Type = typeInfo.ToString(),
                CreationDateAndTime = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"),
                Standard = "UBLTR",
            };

            businessDocument.StandardBusinessDocumentHeader.Sender = new SenderInfo();
            businessDocument.StandardBusinessDocumentHeader.Sender.Identifier = senderInformation.Address;
            businessDocument.StandardBusinessDocumentHeader.Sender.ContactInformations = new List<ContactInformation>();

            ContactInformation contactInformation = new ContactInformation()
            {
                ContactTypeIdentifier = "UNVAN",
                Contact = senderInformation.Title,
            };

            businessDocument.StandardBusinessDocumentHeader.Sender.ContactInformations.Add(contactInformation);
            contactInformation = new ContactInformation()
            {
                ContactTypeIdentifier = "VKN_TCKN",
                Contact = senderInformation.Identification,
            };
            businessDocument.StandardBusinessDocumentHeader.Sender.ContactInformations.Add(contactInformation);

            businessDocument.StandardBusinessDocumentHeader.Receiver = new ReceiverInfo();
            businessDocument.StandardBusinessDocumentHeader.Receiver.Identifier = receiverInformation.Address;

            contactInformation = new ContactInformation()
            {
                ContactTypeIdentifier = "UNVAN",
                Contact = receiverInformation.Title,
            };
            businessDocument.StandardBusinessDocumentHeader.Receiver.ContactInformations.Add(contactInformation);

            contactInformation = new ContactInformation()
            {
                ContactTypeIdentifier = "VKN_TCKN",
                Contact = receiverInformation.Identification,
            };
            businessDocument.StandardBusinessDocumentHeader.Receiver.ContactInformations.Add(contactInformation);

            businessDocument.Package = new PackageInfo();
            businessDocument.Package.Elements = new ElementsInfo();
            businessDocument.Package.Elements.ElementCount = 1;
            businessDocument.Package.Elements.ElementType = elementTypeInfo.ToString();
            businessDocument.Package.Elements.ElementList = new List<ElementListInfo>();
            return businessDocument;
        }


        public ApplicationResponseInfo CreateApplicationResponse(string documentID, string profileID, string docRefIdentifier, PartyInfo senderPartyInfo,
                                                                PartyInfo receiverPartyInfo, 
                                                                string applicationStatusMessage, string applicationStatusCode)
        {
            ApplicationResponseInfo responseInfo = new ApplicationResponseInfo()
            {
                CustomizationID = "TR1.2",
                ID = documentID,
                IssueDate = DateTime.Now.ToString("yyyy-MM-dd"),
                IssueTime = DateTime.Now.ToString("hh:mm:ss"),
                ProfileID = profileID,
                Notes =
                {
                    "Uygulama Yanıtı",
                    string.Format("{0}-{1}",applicationStatusMessage,applicationStatusCode),
                },
                UBLVersionID = "2.1",
                UUID = AppHelper.GetGuid(),
            };

            responseInfo.SenderParty = new SenderPartyInfo()
            {
                Contact = senderPartyInfo.Contact,
                PartyIdentifications = senderPartyInfo.PartyIdentifications,
                PartyName = senderPartyInfo.PartyName,
                PartyTaxScheme = senderPartyInfo.PartyTaxScheme,
                Person = senderPartyInfo.Person,
                PostalAddress = senderPartyInfo.PostalAddress,
                WebsiteURI = senderPartyInfo.WebsiteURI,
            };

            responseInfo.ReceiverParty = new ReceiverPartyInfo()
            {
                Contact = receiverPartyInfo.Contact,
                PartyIdentifications = receiverPartyInfo.PartyIdentifications,
                PartyName = receiverPartyInfo.PartyName,
                PartyTaxScheme = receiverPartyInfo.PartyTaxScheme,
                Person = receiverPartyInfo.Person,
                PostalAddress = receiverPartyInfo.PostalAddress,
                WebsiteURI = receiverPartyInfo.WebsiteURI,
            };

            responseInfo.Signature = new SignatureInfo()
            {
                DigitalSignatureAttachment = new DigitalSignatureAttachmentInfo()
                {
                    ExternalReference = new ExternalReferenceInfo()
                    {
                        URI = string.Format("#{0}", responseInfo.ID),
                    },
                },
                ID = new IDContainerInfo()
                {
                    Value = responseInfo.SenderParty.PartyIdentifications[0].ID.Value,
                    schemeID = "VKN_TCKN",
                },
                SignatoryParty = new SignatoryPartyInfo()
                {
                    Contact = responseInfo.SenderParty.Contact,
                    PostalAddress = responseInfo.SenderParty.PostalAddress,
                    PartyIdentification = responseInfo.SenderParty.PartyIdentifications[0],
                    PartyTaxScheme = responseInfo.SenderParty.PartyTaxScheme,
                }
            };



            responseInfo.DocumentResponse = new DocumentResponseInfo()
            {
                DocumentReference = new DocumentReferenceInfo()
                {
                    DocumentType = "SENDERENVELOPE",
                    DocumentTypeCode = "SENDERENVELOPE",
                    ID = docRefIdentifier,
                    IssueDate = DateTime.Now.ToString().ToGibDocumentDate(),
                },
                Response = new ResponseInfo()
                {
                    Description = applicationStatusMessage,
                    ReferenceID = AppHelper.GetGuid(),
                    ResponseCode = applicationStatusCode,
                },
                LineResponse = new LineResponseInfo()
                {
                    LineReference = new LineReferenceInfo()
                    {
                        LineID = "0",
                        DocumentReference = new DocumentReferenceInfo()
                        {
                            ID = docRefIdentifier,
                            IssueDate = DateTime.Now.ToString().ToGibDocumentDate(),
                        },
                    },
                    Response = new ResponseInfo()
                    {
                        Description = applicationStatusMessage,
                        ReferenceID = AppHelper.GetGuid(),
                        ResponseCode = applicationStatusCode,
                    }
                }
            };

            return responseInfo;
        }
    }
}
