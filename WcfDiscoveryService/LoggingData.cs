using System;
using System.Runtime.Serialization;
using UL.Framework.Logging.API.DTO;

namespace WcfDiscoveryService
{
    [DataContract]
    [Serializable]
    public class LoggingData
    {
        [DataMember]
        public int TenantID { get; set; }

        [DataMember]
        public int LoggingRequestID { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public LoggingException Exception { get; set; }

        [DataMember]
        public string Application { get; set; }

        [DataMember]
        public string Module { get; set; }

        [DataMember]
        public string Tenant { get; set; }

        [DataMember]
        public string ServerName { get; set; }

        [DataMember]
        public string IpAddress { get; set; }

        [DataMember]
        public string SessionId { get; set; }

        [DataMember]
        public DateTimeOffset Instant { get; set; }

        [DataMember]
        public string TimeZone { get; set; }

        [DataMember]
        public long UserID { get; set; }

        public override string ToString()
        {
            return string.Format("Title:{0}\nMessage:{1}\nException:{2}\nApplicaton:{3}\nModule:{4}\nTenent:{5}\nServerName:{6}\nIpAddress:{7}\nSessionId:{8}\nInstant:{9}\nTimezone:{10}", (object)this.Title, (object)this.Message, (object)this.Exception, (object)this.Application, (object)this.Module, (object)this.Tenant, (object)this.ServerName, (object)this.IpAddress, (object)this.SessionId, (object)this.Instant, (object)this.TimeZone);
        }
    }
}
