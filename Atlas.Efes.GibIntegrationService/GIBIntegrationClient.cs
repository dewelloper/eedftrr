using Atlas.Efes.GibIntegrationService.GIBServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Atlas.Efes.GibIntegrationService
{
    public class GIBIntegrationClient
    {
        private EFaturaPortTypeClient CreateClient()
        {
            EFaturaPortTypeClient client = new EFaturaPortTypeClient();
            client.Endpoint.Address = new EndpointAddress(new Uri("https://merkeztest.efatura.gov.tr/EFaturaMerkez/services/EFatura?wsdl"));
            return client;
        }

        private static GIBIntegrationClient _instance = new GIBIntegrationClient();

        private static object _lock = new object();

        public static GIBIntegrationClient CreateInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new GIBIntegrationClient();
                }
            }
            return _instance;
        }

        public documentReturnType SendDocument(documentType documentType)
        {
            var client = CreateClient();
            documentReturnType response = client.sendDocument(documentType);
            return response;
        }

        public getAppRespResponseType GetApplicationResponse(getAppRespRequestType getAppRespRequest)
        {
            var client = CreateClient();
            return client.getApplicationResponse(getAppRespRequest);
        }

    }
}
