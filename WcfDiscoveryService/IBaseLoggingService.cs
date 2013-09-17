using System.ServiceModel;

namespace WcfDiscoveryService
{
    [ServiceContract]
    public interface IBaseLoggingService
    {
        [OperationContract]
        string Log(LoggingData data);
    }
}
