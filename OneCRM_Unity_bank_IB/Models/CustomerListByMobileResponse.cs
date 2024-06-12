using System.Collections.Generic;

namespace UnityBank.Models
{
  
    public class CustomerListByMobileResponse
    {
        public string action { get; set; }
        public int response_code { get; set; }
        public string response_message { get; set; }
        public int total_size { get; set; }
        public int total_pages { get; set; }
        public List<CustomerData> results { get; set; }
    }
    public class CustomerData
    {
        public int customerId { get; set; }
        public string fullName { get; set; }
        public string customerTypeStr { get; set; }
        public string customerStatusStr { get; set; }
    }




}
