using System.Security.Principal;

namespace UnityBank.Models
{
    public class Customer
    {
        public string MobileNo { get; set; }
        public CustomerDetail customerDetail { get; set; } = new CustomerDetail();
        //public AccountDetail  accountDetail { get; set; } = new AccountDetail();
    }

    public class CustomerDetail
    {
        public string CustomerId { get; set; }
        public string nominee { get; set; }
        
        public string CustomerNo { get; set; }
        public string Permanent_Address { get; set; }
        public string Register_Mobile_No { get; set; }
        public string Email { get; set; }
        

    }
    //public class AccountDetail
    //{
    //    public string? AccountNo { get; set; }
    //    public string? AccountMappedList { get; set; }
    //    public string? LastFiveTransactions { get; set; }
    //    public string? JointAccountHolderName { get; set; }
        
    //    public string? Kyc_Type { get; set; }
    //    public string? Account_Type { get; set; }
    //    public string? Account_Status { get; set; }
    //    public string? Account_Opening_Date { get; set; }
    //    public string? Account_Closing_Date { get; set; }
    //    public string? Home_Branch { get; set; }
    //    public string? Freeze_Status { get; set; }
    //    public string? Account_Balance { get; set; }
    //    public string? Product_Code { get; set; }
    //}

}
