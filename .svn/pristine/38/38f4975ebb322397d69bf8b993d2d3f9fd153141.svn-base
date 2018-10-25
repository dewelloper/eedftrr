using Atlas.Efes.Common.GIB;
using Atlas.Efes.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.Efes.Engine
{
    public class XmlStringBuilder : IDisposable
    {
        public string CreateResponseString(string instanceIdentifier, StandardBusinessDocumentInfo documentInfo,
                                          ApplicationResponseInfo responseInfo, bool isDocumentListened = false, string responseType = "Inbox")
        {
            StringBuilder builder = new StringBuilder();

            GIBUserInfo senderInfo = XmlSchemaParser.GetGIBUserInfo<SenderInfo>(documentInfo.StandardBusinessDocumentHeader.Sender);
            GIBUserInfo receiverInfo = XmlSchemaParser.GetGIBUserInfo<ReceiverInfo>(documentInfo.StandardBusinessDocumentHeader.Receiver);

            builder.Append("<AppResponseHeader>");
            builder.AppendFormat("<SenderIdentification>{0}</SenderIdentification>", senderInfo.Identification);
            builder.AppendFormat("<ReceiverIdentification>{0}</ReceiverIdentification>", receiverInfo.Identification);
            builder.AppendFormat("<DocumentRefInstanceIdentifier>{0}</DocumentRefInstanceIdentifier>", instanceIdentifier);
            builder.AppendFormat("<InstanceIdentifier>{0}</InstanceIdentifier>", responseInfo.DocumentResponse.DocumentReference.ID);
            builder.AppendFormat("<DocumentReferenceID>{0}</DocumentReferenceID>", documentInfo.StandardBusinessDocumentHeader.DocumentIdentification.InstanceIdentifier);

            builder.AppendFormat("<ResponseCode>{0}</ResponseCode>", responseInfo.DocumentResponse.LineResponse.Response.ResponseCode);
            builder.AppendFormat("<ResponseDesc>{0}</ResponseDesc>", responseInfo.DocumentResponse.LineResponse.Response.Description);
            builder.AppendFormat("<DocumentType>{0}</DocumentType>", responseInfo.DocumentResponse.DocumentReference.DocumentType);
            builder.AppendFormat("<DocumentTypeCode>{0}</DocumentTypeCode>", responseInfo.DocumentResponse.DocumentReference.DocumentTypeCode);
            builder.AppendFormat("<ReferenceID>{0}</ReferenceID>", responseInfo.DocumentResponse.LineResponse.Response.ReferenceID);
            builder.AppendFormat("<ProfileID>{0}</ProfileID>", responseInfo.ProfileID);
            builder.AppendFormat("<UUID>{0}</UUID>", responseInfo.UUID);
            builder.AppendFormat("<InvoiceID>{0}</InvoiceID>", responseInfo.ID);
            builder.AppendFormat("<IssueTime>{0}</IssueTime>", responseInfo.IssueTime);
            builder.AppendFormat("<Notes>{0}</Notes>", responseInfo.Notes);
            builder.AppendFormat("<IssueDate>{0}</IssueDate>", responseInfo.IssueDate);
            builder.AppendFormat("<ResponseType>{0}</ResponseType>", responseType);
            builder.AppendFormat("<IsDocumentListened>{0}</IsDocumentListened>", isDocumentListened ? "Listened" : "NotYet");
            builder.Append("</AppResponseHeader>");

            return builder.ToString();
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
