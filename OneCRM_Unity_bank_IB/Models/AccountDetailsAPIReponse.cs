﻿using System.Collections.Generic;

namespace UnityBank.Models
{
    public class AccountDetailsAPIReponse
    {
        public string action { get; set; }
        public int response_code { get; set; }
        public string response_message { get; set; }
        public int total_size { get; set; }
        public int total_pages { get; set; }
        public List<AccountData> results { get; set; }
    }
    public class AccountData
    {
        public int accountId { get; set; }
        public int customerId { get; set; }
        public string mmid { get; set; }
        public string ifscCode { get; set; }
        public int classificationId { get; set; }
        public string mobileNo { get; set; }
        public string fullName { get; set; }
        public string emailId { get; set; }
        public int branchCode { get; set; }
        public string branchName { get; set; }
        public double availableBalance { get; set; }
        public double checkerClearBalance { get; set; }
        public double lienAmount { get; set; }
        public string accountStatus { get; set; }
        public int productCode { get; set; }
        public string productName { get; set; }
    }

}
