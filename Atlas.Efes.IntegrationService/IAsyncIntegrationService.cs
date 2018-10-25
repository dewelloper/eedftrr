using Atlas.Efes.Common.GIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Atlas.Efes.IntegrationService
{
    interface IAsyncIntegrationService
    {
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginSendDocument(InvoiceInfo invoiceInfo, DateTime timestamp, AsyncCallback callback, object asyncState);
        void EndSendDocument(IAsyncResult result);
    }
}
