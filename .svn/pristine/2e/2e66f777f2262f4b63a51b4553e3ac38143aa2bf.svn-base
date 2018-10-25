using Atlas.Efes.Common.GIB;
using Atlas.Efes.Common.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Atlas.Efes.SenderIntegrationService
{
    [ServiceContract]
    public interface ISenderIntegrationService
    {
        [OperationContract]
        DocumentResponse SendDocument(DocumentInfo documentInfo);

        [OperationContract]
        DocumentResponse SendDocumentVS2(StandardBusinessDocumentInfo standartBusiness);

        [OperationContract]
        ApplicationResponse GetApplicationResponse(string instanceIdentifier);
    }
}
