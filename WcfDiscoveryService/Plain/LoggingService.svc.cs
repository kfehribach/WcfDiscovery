using System;

namespace WcfDiscoveryService.Plain
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PlainLoggingService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PlainLoggingService.svc or PlainLoggingService.svc.cs at the Solution Explorer and start debugging.
    public class LoggingService : IPlainLoggingService
    {
        public LoggingService()
        {

        }

        public LoggingService(ILoggingRepository repository)
        {

        }

        public string Log(LoggingData data)
        {
            return "Plain";
        }
    }
}
