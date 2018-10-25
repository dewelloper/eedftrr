using System.ServiceModel;

namespace Atlas.Efes.ReceiverIntegrationService
{
    [ServiceContract(Namespace = "http://gib.gov.tr/vedop3/eFatura")]
    public interface IReceiverIntegrationService
    {
        [OperationContract(Action = "sendDocument", ReplyAction = "http://gib.gov.tr/vedop3/eFatura/EFaturaPortType/sendDocumentResponse")]
        [FaultContract(typeof(gib.gov.tr.vedop3.eFatura.EFaturaFaultType), Action = "sendDocument", Name = "EFaturaFault")]
        [XmlSerializerFormat()]
        sendDocumentResponse sendDocument(sendDocument request);
    }
}
