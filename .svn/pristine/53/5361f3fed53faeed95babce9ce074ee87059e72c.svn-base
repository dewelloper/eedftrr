using Atlas.Efes.Common.GIB;
using Atlas.Efes.Common.GIB.Enums;
using Atlas.Efes.Common.Model;
using Atlas.Efes.Common.Resources;
using Atlas.Efes.Common.ServiceModel;
using Atlas.Efes.DataContext;
using Atlas.Efes.Engine;
using Atlas.Efes.ReceiverListener.SenderIntegrationProxy;
using System;
using System.Data;
using System.Timers;



namespace Atlas.Efes.ReceiverListener
{
    class Program
    {

        private static Timer timer;
        private static Timer timerApp;

        static void Main(string[] args)
        {
            Console.WriteLine("Application Started");
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(TimerCallBack);
            timer.Interval = 1000;
            timer.AutoReset = false;
            timer.Start();
            timer.Enabled = true;







            Console.ReadLine();

        }


        private static void TimerCallBack(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("TimerCallBack: " + DateTime.Now);

            try
            {
                DatabaseResult<DataTable> databaseResult = InvoiceDataContext.Instance.GetQueueItem("INBOX");

                if (databaseResult.IsSuccess && databaseResult.Result != null)
                {
                    DataTable dataTable = databaseResult.Result;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        string instanceIdentifier = item.Get<string>("INSTANCEIDENTIFIER");
                        GIBUserInfo senderInfo = new GIBUserInfo()
                        {
                            Address = item.Get<string>("SENDERIDENTIFIER"),
                            Identification = item.Get<string>("SENDERIDENTIFICATION"),
                            Title = item.Get<string>("SENDERTITLE"),
                        };

                        GIBUserInfo receiverInfo = new GIBUserInfo()
                        {
                            Address = item.Get<string>("RECEIVERIDENTIFIER"),
                            Identification = item.Get<string>("RECEIVERIDENTIFICATION"),
                            Title = item.Get<string>("RECEIVERTITLE"),
                        };

                        string invoiceXML = item.Get<string>("INVOICEXML");
                        string profileID = item.Get<string>("PROFILEID");
                        string senderPartyXML = XmlSchemaParser.GetPartyNodeXML(invoiceXML, "AccountingSupplierParty");
                        PartyInfo senderPartyInfo = XmlSchemaParser.ParsePartyInfo(senderPartyXML, "AccountingSupplierParty");

                        string receiverPartyXML = XmlSchemaParser.GetPartyNodeXML(invoiceXML, "AccountingCustomerParty");
                        PartyInfo receiverPartyInfo = XmlSchemaParser.ParsePartyInfo(receiverPartyXML, "AccountingCustomerParty");

                        StandardBusinessDocumentInfo documentInfo = XmlSchemaCreator.CreateBusinessDocument(receiverInfo, senderInfo, DocumentTypeInfo.POSTBOXENVELOPE, ElementTypeInfo.APPLICATIONRESPONSE);

                        //Schematronnnn.... vs.....//....Imza.....//Other Kontrols....

                        string applicationStatusMessage = ApplicationResponseMessages.STATUS_CODE_1200;
                        string applicationStatusCode = "1200";

                        if (true)
                        {
                            applicationStatusMessage = "Begenmedim Tekrar Kes";
                            applicationStatusCode = ApplicationResponseMessages.STATUS_CODE_REJECTED;
                        }

                        string invoiceTypeCode = string.Empty;
                        ApplicationResponseInfo responseInfo = XmlSchemaCreator.CreateApplicationResponse(instanceIdentifier, receiverPartyInfo, senderPartyInfo, applicationStatusMessage, applicationStatusCode, invoiceTypeCode, profileID);

                        documentInfo.Package.Elements.ElementList.Add(new ElementListInfo()
                        {
                            ApplicationResponse = responseInfo
                        });

                        DocumentResponse documentResult = null;
                        using (SenderIntegrationServiceClient client = new SenderIntegrationServiceClient())
                        {
                            documentResult = client.SendDocumentVS2(documentInfo);
                        }

                        ApplicationResponse response = null;
                        if (documentResult != null && documentResult.InstanceIdentifier != null)
                        {
                            using (SenderIntegrationServiceClient client = new SenderIntegrationServiceClient())
                            {
                                response = client.GetApplicationResponse(documentResult.InstanceIdentifier);
                            }
                        }

                        if (response != null)
                        {
                            string responseXML = response.ResponseXml;
                            var innerDocument = XmlSchemaParser.ParseStandardBusiness(responseXML);
                            string applicationXML = XmlSchemaParser.GetApplicationNodeXML(responseXML);
                            var innerResponse = XmlSchemaParser.ParseApplicationResponse(applicationXML);
                            XmlStringBuilder builder = new XmlStringBuilder();
                            string responseXmlString = builder.CreateResponseString(instanceIdentifier, innerDocument, innerResponse, true);
                            DatabaseResult<bool> dbResult = InvoiceDataContext.Instance.SaveApplicationResponse(responseXmlString, responseXML, applicationXML);
                        }

                        string consoleMessage = string.Format("{0} Process Started, {1}", instanceIdentifier, DateTime.Now.ToString());
                        WriteConsoleMessage(consoleMessage);
                    }
                }



                try
                {
                    databaseResult = InvoiceDataContext.Instance.GetQueueItem("APPLICATIONRESPONSE");

                    if (databaseResult.IsSuccess && databaseResult.Result != null)
                    {
                        DataTable dataTable = databaseResult.Result;

                        foreach (DataRow item in dataTable.Rows)
                        {
                            string instanceIdentifier = item.Get<string>("INSTANCEIDENTIFIER");

                            ApplicationResponse response = null;

                            using (SenderIntegrationProxy.SenderIntegrationServiceClient client = new SenderIntegrationServiceClient())
                            {
                                response = client.GetApplicationResponse(instanceIdentifier);
                            }

                            if (response != null)
                            {
                                string responseXML = response.ResponseXml;
                                var innerDocument = XmlSchemaParser.ParseStandardBusiness(responseXML);
                                string applicationXML = XmlSchemaParser.GetApplicationNodeXML(responseXML);
                                var innerResponse = XmlSchemaParser.ParseApplicationResponse(applicationXML);
                                XmlStringBuilder builder = new XmlStringBuilder();
                                string responseXmlString = builder.CreateResponseString(instanceIdentifier, innerDocument, innerResponse);
                                DatabaseResult<bool> dbResult = InvoiceDataContext.Instance.SaveApplicationResponse(responseXmlString, responseXML, applicationXML);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                timer.Enabled = true;
            }
            GC.Collect();
        }



        private static void WriteConsoleMessage(string message)
        {
            Console.WriteLine("***\n");
            Console.WriteLine(message);
            Console.WriteLine("***\n");
        }

    }
}
