using System.Collections.Generic;

namespace UnityBank.Models
{
    public class AccountResponse
    {
        public string action { get; set; }
        public int response_code { get; set; }
        public string response_message { get; set; }
        public int total_size { get; set; }
        public int total_pages { get; set; }
        public List<AccountDetails> results { get; set; }
        
    }
   
}
