using Atlas.Efes.Common.GIB;
using Atlas.Efes.Common.GIB.Enums;
using Atlas.Efes.Common.Model;
using Atlas.Efes.Common.Resources;
using Atlas.Efes.Core;
using Atlas.Efes.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Atlas.Efes.Engine
{
    public class XmlSchemaCreator
    {
        public static ApplicationResponseInfo CreateApplicationResponse(string docRefIdentifier, PartyInfo senderPartyInfo, PartyInfo receiverPartyInfo,
                                                                string applicationStatusMessage, string applicationStatusCode, string invoiceTypeCode, string profileID = "UBL-TR-PROFILE-1")
        {
            ApplicationResponseInfo responseInfo = new ApplicationResponseInfo()
            {
                CustomizationID = "TR1.2",
                ID = AppHelper.GetGuid(),
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
                LineResponse = new LineResponseInfo()
                {
                    LineReference = new LineReferenceInfo()
                    {
                        LineID = "0",
                        DocumentReference = new DocumentReferenceInfo()
                        {
                            ID = docRefIdentifier,
                            IssueDate = DateTime.Now.ToString().ToGibDocumentDate(),
                            DocumentType = "SYSTEMENVELOPE",
                            DocumentTypeCode = "SYSTEMENVELOPE",
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


            if (applicationStatusCode == "KABUL" || applicationStatusCode == "RED")
            {
                responseInfo.DocumentResponse.LineResponse.LineReference = new LineReferenceInfo()
                  {
                      LineID = string.Empty
                  };
                responseInfo.DocumentResponse.Response = new ResponseInfo()
                {
                    Description = applicationStatusMessage,
                    ReferenceID = AppHelper.GetGuid(),
                    ResponseCode = applicationStatusCode,
                };
                responseInfo.ProfileID = profileID;
                responseInfo.DocumentResponse.DocumentReference = new DocumentReferenceInfo()
                {
                    DocumentType = "FATURA",
                    DocumentTypeCode = "FATURA",
                    ID = docRefIdentifier,
                    IssueDate = DateTime.Now.ToString().ToGibDocumentDate(),
                };
            }
            else
            {
                responseInfo.DocumentResponse.DocumentReference = new DocumentReferenceInfo()
                {
                    DocumentType = "SENDERENVELOPE",
                    DocumentTypeCode = "SENDERENVELOPE",
                    ID = docRefIdentifier,
                    IssueDate = DateTime.Now.ToString().ToGibDocumentDate(),
                };
                responseInfo.DocumentResponse.Response = new ResponseInfo()
                {
                    Description = "SystemApplicationResponse",
                    ReferenceID = AppHelper.GetGuid(),
                    ResponseCode = "S_APR",
                };
                responseInfo.ProfileID = "UBL-TR-PROFILE-1";
            }


            return responseInfo;
        }


        public static StandardBusinessDocumentInfo CreateBusinessDocument(GIBUserInfo receiverInformation,
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


        public static ApplicationResponseInfo CreateSystemResponse(string refInstanceIdentifier, PartyInfo senderPartyInfo,
                                                                   PartyInfo receiverPartyInfo, string applicationMessage, string applicationCode)
        {
            ApplicationResponseInfo applicationResponse = new ApplicationResponseInfo()
            {
                CustomizationID = SystemConstants.CUSTOMIZATIONID,
                ProfileID = SystemConstants.PROFILEID,
                UUID = AppHelper.GetGuid(),
                ID = AppHelper.GetGuid(),
                IssueDate = DateTime.Now.ToGibDocumentDate(),
                IssueTime = DateTime.Now.ToGibHour(),
                UBLVersionID = SystemConstants.UBLVERSIONID,
                Notes =
                {
                    "SYSTEM ENVELOPE",
                },
                SenderParty = new SenderPartyInfo()
                {
                    Contact = senderPartyInfo.Contact,
                    PartyIdentifications = senderPartyInfo.PartyIdentifications,
                    PartyName = senderPartyInfo.PartyName,
                    PartyTaxScheme = senderPartyInfo.PartyTaxScheme,
                    Person = senderPartyInfo.Person,
                    PostalAddress = senderPartyInfo.PostalAddress,
                    WebsiteURI = senderPartyInfo.WebsiteURI,
                },
                ReceiverParty = new ReceiverPartyInfo()
                {
                    Contact = receiverPartyInfo.Contact,
                    PartyIdentifications = receiverPartyInfo.PartyIdentifications,
                    PartyName = receiverPartyInfo.PartyName,
                    PartyTaxScheme = receiverPartyInfo.PartyTaxScheme,
                    Person = receiverPartyInfo.Person,
                    PostalAddress = receiverPartyInfo.PostalAddress,
                    WebsiteURI = receiverPartyInfo.WebsiteURI,
                },
                DocumentResponse = new DocumentResponseInfo()
                {
                    DocumentReference = new DocumentReferenceInfo()
                    {
                        ID = refInstanceIdentifier,
                        IssueDate = DateTime.Now.ToGibDocumentDate(),
                        DocumentType = "SENDERENVELOPE",
                        DocumentTypeCode = "SENDERENVELOPE",
                    },
                    LineResponse = new LineResponseInfo()
                    {
                        LineReference = new LineReferenceInfo()
                        {
                            DocumentReference = new DocumentReferenceInfo()
                            {
                                ID = refInstanceIdentifier,
                                IssueDate = DateTime.Now.ToGibDocumentDate(),
                            },
                            LineID = "0",
                        },
                        Response = new ResponseInfo()
                        {
                            Description = applicationMessage,
                            ReferenceID = AppHelper.GetGuid(),
                            ResponseCode = applicationCode,
                        }
                    },
                    Response = new ResponseInfo()
                    {
                        Description = SystemConstants.SYSTEMRESPONSEDESC,
                        ReferenceID = AppHelper.GetGuid(),
                        ResponseCode = SystemConstants.SYSTEMRESPONSECODE,
                    }
                }
            };
            return applicationResponse;
        }

    }
}
