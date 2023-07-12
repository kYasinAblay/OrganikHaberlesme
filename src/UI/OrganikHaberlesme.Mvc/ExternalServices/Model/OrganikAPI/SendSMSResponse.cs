using OrganikHaberlesme.Mvc.ExternalServices.Base;

namespace OrganikHaberlesme.Mvc.ExternalServices.Model.OrganikAPI
{

    public class Data
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public bool appeal { get; set; }
        public object type { get; set; }
        public bool otp { get; set; }
        public int count { get; set; }
        public double amount { get; set; }
        public string date { get; set; }
        public object vars { get; set; }
        public string waiting { get; set; }
        public string delivered { get; set; }
        public string failure { get; set; }
        public string status { get; set; }
    }

    public class SendSMSResponse
    {
        public bool result { get; set; }
        public Data data { get; set; }
    }

}
