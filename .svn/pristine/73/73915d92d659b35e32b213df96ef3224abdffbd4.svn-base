using Atlas.Efes.Common.GIB;
using Atlas.Efes.Common.Model;
using Atlas.Efes.Common.ServiceModel;
using Atlas.Efes.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace Atlas.Efes.IntegrationService
{
    public class ClientCallBack : IAsyncIntegrationService
    {
        public IAsyncResult BeginSendDocument(Common.GIB.InvoiceInfo invoiceInfo, DateTime timestamp, AsyncCallback callback, object asyncState)
        {
            Action<InvoiceInfo, DateTime> action = (value, time) =>
            {
                DocumentResponse response = new DocumentResponse();
                var request = WebOperationContext.Current.IncomingRequest;
                var headers = request.Headers;

                string apiKey = headers["ApiKey"];

                if (string.IsNullOrEmpty(apiKey))
                {
                    response.Message = "Check Your ServiceHeader.Insert ApiKey to ServiceHeader";
                   
                }

                DatabaseResult<PartyInfo> supplierPartyInfo = PartyInfoDataContext.Instance.GetCustomerByApiKey(apiKey);

                if (supplierPartyInfo.Result == null)
                {
                    response.Message = supplierPartyInfo.Message;
                   
                }

                using (IntegrationServiceEngine engine = new IntegrationServiceEngine())
                {
                    response = engine.SendDocument(value, supplierPartyInfo.Result);
                }
            };

            return action.BeginInvoke(invoiceInfo, timestamp, callback, asyncState);
        }

        public void EndSendDocument(IAsyncResult result)
        {
            Action<InvoiceInfo, DateTime> act = (Action<InvoiceInfo, DateTime>)((System.Runtime.Remoting.Messaging.AsyncResult)result).AsyncDelegate;
            act.EndInvoke(result);
        }
    }
}