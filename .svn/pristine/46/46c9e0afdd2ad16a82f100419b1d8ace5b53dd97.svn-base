using Atlas.Efes.Common.GIB;
using Atlas.Efes.Common.ServiceModel;
using System.ServiceModel;

namespace Atlas.Efes.IntegrationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEfesIntegrationService" in both code and config file together.
    [ServiceContract()]
    //[ServiceContract(CallbackContract = typeof(IAsyncIntegrationService))]
    public interface IEfesIntegrationService
    {

        [OperationContract]
        DocumentResponse SendDocument(InvoiceInfo invoice);

        [OperationContract]
        ApplicationResponse GetApplicationResponse(string instanceIdentifier);
    }
}
