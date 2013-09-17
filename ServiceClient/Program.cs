using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;
using WcfDiscoveryService;

namespace ServiceClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ShowResults(GetServiceFromDiscovery<WcfDiscoveryService.Plain.IPlainLoggingService>());
            ShowResults(GetServiceFromDiscovery<WcfDiscoveryService.Ninject.INinjectLoggingService>());
            // ShowResults(GetServiceFromDiscovery<WcfDiscoveryService.Custom.ICustomLoggingService>());

            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }

        private static T GetServiceFromDiscovery<T>()
        {
            var proxy = default(T);

            try
            {
                var endPoint = new DynamicEndpoint(
                    ContractDescription.GetContract(typeof(T)),
                    new WSHttpBinding());

                var channelFactory = new ChannelFactory<T>(endPoint);

                proxy = channelFactory.CreateChannel();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return proxy;
        }

        private static void ShowResults(IBaseLoggingService service)
        {
            try
            {
                var loggingData = new LoggingData() { Application = "test" };
                var result = service.Log(loggingData);
                Console.WriteLine(string.Format("Logged - {0}", result));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
