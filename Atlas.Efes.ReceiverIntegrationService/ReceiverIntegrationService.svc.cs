using Atlas.Efes.Common.GIB;
using Atlas.Efes.Common.GIB.Enums;
using Atlas.Efes.Common.Model;
using Atlas.Efes.Common.Resources;
using Atlas.Efes.Core;
using Atlas.Efes.DataContext;
using Atlas.Efes.Engine;
using Atlas.Efes.ReceiverIntegrationService.SignatureServiceProxy;
using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Xml;


namespace Atlas.Efes.ReceiverIntegrationService
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReceiverIntegrationService : IReceiverIntegrationService
    {
        private string applicationActivityLogPath = string.Empty;
        public sendDocumentResponse sendDocument(sendDocument request)
        {
            sendDocumentResponse sendResponse = new sendDocumentResponse();
            string baseAppPath = AppDomain.CurrentDomain.BaseDirectory;

            StringBuilder builder = new StringBuilder();
            string hash = string.Empty;
            string applicationInstanceIdentifier = string.Empty;
            try
            {

                string fileName = request.documentRequest.fileName;
                applicationInstanceIdentifier = fileName.ToString().ToUpper();

                if (applicationInstanceIdentifier.EndsWith(".ZIP"))
                {
                    applicationInstanceIdentifier = applicationInstanceIdentifier.Replace(".ZIP", "");
                }

                applicationActivityLogPath = baseAppPath + "ActivityLogs";

                if (!Directory.Exists(applicationActivityLogPath))
                {
                    Directory.CreateDirectory(applicationActivityLogPath);
                }

                applicationActivityLogPath = applicationActivityLogPath + "\\" + string.Format("{0}_ActivityLogs.txt", applicationInstanceIdentifier.Replace("-", ""));

                ActivitiyLog(ReceiverIntegrationActivities.Start);

                hash = request.documentRequest.hash;
                base64Binary binaryData = request.documentRequest.binaryData;
                string appPath = AppDomain.CurrentDomain.BaseDirectory + string.Format("{0}", DateTime.Now.ToString("ddMMyyyy"));

                if (!Directory.Exists(appPath))
                {
                    Directory.CreateDirectory(appPath);
                }

                if (!appPath.EndsWith("\\"))
                {
                    appPath = appPath + "\\";
                }

                string zipReferenceFilePath = appPath + string.Format("{0}.zip", applicationInstanceIdentifier);


                //BinaryData Process...
                ActivitiyLog(ReceiverIntegrationActivities.BinaryData_Process_Started);
                FileProcess.SaveByteArray(zipReferenceFilePath, binaryData.Value);
                ActivitiyLog(ReceiverIntegrationActivities.BinaryData_Process_Ended);

                //XmlLoad Process...
                ActivitiyLog(ReceiverIntegrationActivities.Xml_Load_Process_Started);
                XmlDocument document = FileProcess.LoadXmlFromZip(zipReferenceFilePath);
                string envelopeXml = document.InnerXml;
                ActivitiyLog(ReceiverIntegrationActivities.Xml_Load_Process_Ended);

                StandardBusinessDocumentInfo documentInfo = XmlSchemaParser.ParseStandardBusiness(document);

                GIBUserInfo senderInfo = XmlSchemaParser.GetGIBUserInfo<SenderInfo>(documentInfo.StandardBusinessDocumentHeader.Sender);
                GIBUserInfo receiverInfo = XmlSchemaParser.GetGIBUserInfo<ReceiverInfo>(documentInfo.StandardBusinessDocumentHeader.Receiver);

                if (documentInfo.Package.Elements.ElementType == ElementTypeInfo.APPLICATIONRESPONSE.ToString())
                {
                    string appResponseXML = XmlSchemaParser.GetApplicationNodeXML(envelopeXml);

                    ApplicationResponseInfo responseInfo = XmlSchemaParser.ParseApplicationResponse(appResponseXML);
                    string responseXmlString = string.Empty;

                    using (XmlStringBuilder instance = new XmlStringBuilder())
                    {
                        responseXmlString = instance.CreateResponseString(applicationInstanceIdentifier, documentInfo, responseInfo);
                    }

                    DatabaseResult<bool> responseDBResult = InvoiceDataContext.Instance.SaveApplicationResponse(responseXmlString, envelopeXml, appResponseXML);
                    if (!responseDBResult.Result)
                    {
                        throw new NotImplementedException(string.Format("DB_SAVE_APPRESPONSERROR : {0}", applicationInstanceIdentifier) + responseDBResult.Message);
                    }

                    sendResponse.documentResponse = new documentReturnType();
                    sendResponse.documentResponse.msg = "Döküman Alındı";
                    sendResponse.documentResponse.hash = hash;
                }
                else
                {
                    string invoiceXML = XmlSchemaParser.GetInvoiceNodeXML(envelopeXml);
                    InvoiceInfo invoiceInfo = XmlSchemaParser.ParseInvoice(invoiceXML);


                    string applicationStatusCode = "1100";

                    builder.AppendFormat("<InboxHeader>");
                    builder.AppendFormat("<InstanceIdentifier>{0}</InstanceIdentifier>", documentInfo.StandardBusinessDocumentHeader.DocumentIdentification.InstanceIdentifier);
                    builder.AppendFormat("<UUID>{0}</UUID>", invoiceInfo.UUID);
                    builder.AppendFormat("<ReceiverIdentification>{0}</ReceiverIdentification>", receiverInfo.Identification);
                    builder.AppendFormat("<InvoiceNumber>{0}</InvoiceNumber>", invoiceInfo.ID);
                    builder.AppendFormat("<SenderIdentification>{0}</SenderIdentification>", senderInfo.Identification);
                    builder.AppendFormat("<SenderTitle>{0}</SenderTitle>", senderInfo.Title);
                    builder.AppendFormat("<InvoiceDate>{0}</InvoiceDate>", invoiceInfo.IssueDate);
                    builder.AppendFormat("<CreationDateAndTime>{0}</CreationDateAndTime>", documentInfo.StandardBusinessDocumentHeader.DocumentIdentification.CreationDateAndTime);
                    builder.AppendFormat("<ProfileID>{0}</ProfileID>", invoiceInfo.ProfileID);
                    builder.AppendFormat("<InvoiceTypeCode>{0}</InvoiceTypeCode>", invoiceInfo.InvoiceTypeCode);
                    builder.AppendFormat("<DocumentStatus>{0}</DocumentStatus>", applicationStatusCode);
                    builder.AppendFormat("</InboxHeader>");

                    DatabaseResult result = InvoiceDataContext.Instance.SaveDocumentInbox(builder.ToString(), envelopeXml, invoiceXML);

                    if (!result.IsSuccess)
                    {
                        throw new NotImplementedException(string.Format("DB_POST_ERROR : {0}", result.Message));
                    }

                }

                ActivitiyLog(ReceiverIntegrationActivities.Document_Parsing_Ended);
                ActivitiyLog(ReceiverIntegrationActivities.End);

            }
            catch (Exception ex)
            {
                if (ex.Message == "DATABASE_ERROR")
                {

                }

                if (ex.Message == "NO_INVOICE")
                {

                }

                if (ex.Message == "INVALID_INVOICE")
                {

                }


                string logFile = string.Format("Log_{0}.txt", DateTime.Now.ToString("ddMMyyyy"));
                string message = "---------------------------------------";
                message = message + "\n" + ex.Message + "\n\r" + ex.StackTrace + "\n";
                message = message + string.Format("FileID : {0}", applicationInstanceIdentifier);
                message = message + "\n";
                message = message + string.Format("Process Date : {0}", DateTime.Now.ToString());
                message = message + "\n";
                message = message + "---------------------------------------";
                var logPath = baseAppPath + "Logs";

                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                if (!logPath.EndsWith("\\"))
                {
                    logPath = logPath + "\\";
                }

                logPath = logPath + logFile;

                if (!File.Exists(logPath))
                {
                    File.Create(logPath).Close();
                }
                else
                {
                    message = message + "\n";
                }

                File.AppendAllText(logPath, message);
            }
            finally
            {

            }

            sendResponse.documentResponse = new documentReturnType();
            sendResponse.documentResponse.msg = "Döküman Alındı";
            sendResponse.documentResponse.hash = hash;

            return sendResponse;
        }

        private string CreateSignatureElement(string xml, string id = "")
        {
            byte[] signArray = UTF8Encoding.UTF8.GetBytes(xml);
            string certificateID = "1020409972";
            string signatureID = string.Format("{0}", id);
            short value = 1;
            SignatureServiceProxy.SignatureWSClient client = new SignatureWSClient();
            signatureResponse singResponse = client.sign(signArray, certificateID, signatureID, value);
            string signXml = UTF8Encoding.UTF8.GetString(singResponse.signedFile);

            return "<ElementList > " + signXml + "</ElementList>";
        }

        private void ActivitiyLog(string message)
        {
            string basicLogMessage = string.Format("[{0}] ------- [Process Date] : {1}\r\n", message, DateTime.Now.ToString());
            FileProcess.WriteBasicLog(applicationActivityLogPath, basicLogMessage);
        }

    }
}