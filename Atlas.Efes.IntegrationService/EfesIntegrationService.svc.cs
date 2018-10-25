using Atlas.Efes.Common.GIB;
using Atlas.Efes.Common.Model;
using Atlas.Efes.Common.ServiceModel;
using Atlas.Efes.DataContext;
using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading;

namespace Atlas.Efes.IntegrationService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class EfesIntegrationService : IEfesIntegrationService
    {
        public DocumentResponse SendDocument(InvoiceInfo invoice)
        {
            DocumentResponse response = new DocumentResponse();
            var request = WebOperationContext.Current.IncomingRequest;
            var headers = request.Headers;

            string apiKey = headers["ApiKey"];

            if (string.IsNullOrEmpty(apiKey))
            {
                response.Message = "Check Your ServiceHeader.Insert ApiKey to ServiceHeader";
                return response;
            }

            DatabaseResult<PartyInfo> supplierPartyInfo = PartyInfoDataContext.Instance.GetCustomerByApiKey(apiKey);
            if (supplierPartyInfo.Result == null)
            {
                response.Message = supplierPartyInfo.Message;
                return response;
            }

            using (IntegrationServiceEngine engine = new IntegrationServiceEngine())
            {
                response = engine.SendDocument(invoice, supplierPartyInfo.Result);
            }

            return response;
        }


        public ApplicationResponse GetApplicationResponse(string instanceIdentifier)
        {
            IntegrationServiceEngine engine = new IntegrationServiceEngine();
            return engine.GetApplicationResponse(instanceIdentifier);
        }
    }
}
