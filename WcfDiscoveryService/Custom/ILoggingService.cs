using System.ServiceModel;

namespace WcfDiscoveryService.Custom
{
    [ServiceContract]
    public interface ICustomLoggingService : IBaseLoggingService
    {
    }
}