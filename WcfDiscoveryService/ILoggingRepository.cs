using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfDiscoveryService
{
    public interface ILoggingRepository
    {
        void Log(string message);
    }
}
