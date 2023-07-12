using System.Collections.Generic;

using OrganikHaberlesme.Mvc.ExternalServices.Base;

namespace OrganikHaberlesme.Mvc.ExternalServices.Model.OrganikAPI
{
    public class SendSMSRequest
    {
        //public SendSMSRequest()
        //{
        //    recipients = new List<string>();
        //}
        public string message { get; set; }
        public List<string> recipients { get; set; }
        public string header { get; set; }
        public string type { get; set; }
        public bool otp { get; set; }
        public bool appeal { get; set; }
        public int validity { get; set; }
        public string date { get; set; }
    }
}
