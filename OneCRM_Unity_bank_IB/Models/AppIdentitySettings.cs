namespace UnityBank.Models
{
    public class AppIdentitySettings
    {
        public ApiSettings Api { get; set; }
        
    }
    public class ApiSettings
    {
        public string  ACC_BY_MOBILENO_API_URL { get; set; }
        public string CUST_BY_MOBILENO_API_URL { get; set; }
        public string MINISTATEMENT_API_URL { get; set; }
        public string ACC_DETAILS_FOR_CHANNEL_API_URL { get; set; }
        public string CUSTOMERDETAILS_BY_CUSTOMERID_API_URL { get; set; }
        public string ACCOUNTLIST_BY_CUSTID_API_URL { get; set; }
        public string PRIVATE_KEY_PATH { get; set; }
        public string PRIVATE_KEY_PASSWORD { get; set; }
        public string APP_KEY { get; set; }
        public string TENANT { get; set; }
        public string ConnectinString { get; set; }
    }
}
