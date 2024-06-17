namespace UnityBank.Models
{
    public class TransactionDetails
    {
        public int id { get; set; } 
        public object txnRefNo { get; set; }
        public object txnSubRefNo { get; set; }
        public int txnBranch { get; set; }
        public int homeBranch { get; set; }
        public string txnDate { get; set; }
        public int txnType { get; set; }
        public string txnTime { get; set; }
        public object txnCreatedDate { get; set; }
        public string txnPostingDate { get; set; }
        public string txnValueDate { get; set; }
        public int accountId { get; set; }
        public int batchCode { get; set; }
        public int scrollNo { get; set; }
        public int setNo { get; set; }
        public double txnAmount { get; set; }
        public string txnNature { get; set; }
        public string instrNo { get; set; }
        public string instrDate { get; set; }
        public string narration { get; set; }
        public int isActive { get; set; }
        public object createdBy { get; set; }
        public string createdDate { get; set; }
        public object createdByEmployeeCode { get; set; }
        public object lastModifiedByEmployeeCode { get; set; }
        public object txnTypeName { get; set; }
        public int activityId { get; set; }
        public int activitySubTypeId { get; set; }
        public object accountNo { get; set; }
        public object accountBalance { get; set; }
        public object accountMasterMini { get; set; }
        public object homeBranchName { get; set; }
        public object txnBranchName { get; set; }
        public object batchName { get; set; }
        public object denominationList { get; set; }
        public object activityName { get; set; }
        public string fullName { get; set; }
        public double balance { get; set; }
        public object totalPages { get; set; }
        public object principalType { get; set; }
        public object description { get; set; }
        public string fcyCurrency { get; set; }
        public double exchangeRate { get; set; }
        public double valueDateInterest { get; set; }

    }
}
