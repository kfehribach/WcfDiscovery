using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfDiscoveryService
{
    public class LoggingRespository : ILoggingRepository
    {   
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}