namespace UnityBank.Models
{


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class MailingAddress
    {
        public int canPerformAuth { get; set; }
        public int canPerformUpdate { get; set; }
        public int canPerformVerify { get; set; }
        public int canPerformAuth_SinglePage { get; set; }
        public int authorizedCount { get; set; }
        public int addressTypeCode { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public int stateCode { get; set; }
        public int cityCode { get; set; }
        public int pinCode { get; set; }
        public string phoneOff { get; set; }
        public string stdOff { get; set; }
        public string emailId { get; set; }
        public object faxNo { get; set; }
        public string stdFax { get; set; }
        public string phoneRes { get; set; }
        public string stdRes { get; set; }
        public object mobileNo { get; set; }
        public string boardLineNo { get; set; }
        public string extensionNo { get; set; }
    }

    public class PermenantAddress
    {
        public int canPerformAuth { get; set; }
        public int canPerformUpdate { get; set; }
        public int canPerformVerify { get; set; }
        public int canPerformAuth_SinglePage { get; set; }
        public int authorizedCount { get; set; }
        public int addressTypeCode { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public int stateCode { get; set; }
        public int cityCode { get; set; }
        public int pinCode { get; set; }
        public string phoneOff { get; set; }
        public string stdOff { get; set; }
        public string emailId { get; set; }
        public object faxNo { get; set; }
        public string stdFax { get; set; }
        public string phoneRes { get; set; }
        public string stdRes { get; set; }
        public object mobileNo { get; set; }
        public string boardLineNo { get; set; }
        public string extensionNo { get; set; }
    }

    public class CustomerDetails
    {
        public int customer_id { get; set; }
        public int homeBranchCode { get; set; }
        public string fullName { get; set; }
        public int restrictedFlag { get; set; }
        public int customerBlockStatus { get; set; }
        public int blockReasonCode { get; set; }
        public int ageGroup { get; set; }
        public int customerSubTypeCode { get; set; }
        public string registeredMobileNo { get; set; }
        public int customerTypeCode { get; set; }
        public int customerProfileCode { get; set; }
        public string dob { get; set; }
        public PermenantAddress permenantAddress { get; set; }
        public MailingAddress mailingAddress { get; set; }
        public string aadhaarNumber { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string panNumber { get; set; }
        public string salutation { get; set; }
        public string maritalStatus { get; set; }
        public int staff { get; set; }
        public double threshold { get; set; }
        public int minorityCode { get; set; }
        public int weakerCode { get; set; }
        public int customerStatus { get; set; }
        public string ckycrNo { get; set; }
    }

    
}
