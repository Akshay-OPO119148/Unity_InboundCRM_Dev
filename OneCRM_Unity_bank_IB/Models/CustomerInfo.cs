using System;
using System.Collections.Generic;

namespace UnityBank.Models
{
    public class CustomerInfo
    {
        public Result results { get; set; }
      
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AccountInfo
    {
        public string accountNo { get; set; }
        public string accountType { get; set; }
        public string accountsCurrency { get; set; }
        public string product { get; set; }
        public string accountStatus { get; set; }
        public bool defaultAccount { get; set; }
        public string ifscCode { get; set; }
        public string branchId { get; set; }
        public string schemeCode { get; set; }
        public bool isPrimary { get; set; }
    }

    public class AccountKitMapping
    {
        public string entityId { get; set; }
        public string accountNo { get; set; }
        public string kitNo { get; set; }
    }

    public class AddressInfo
    {
        public string addressCategory { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public object district { get; set; }
        public object isoCountryCode { get; set; }
        public string pincode { get; set; }
    }

    public class CommunicationInfo
    {
        public string contactNo { get; set; }
        public string emailId { get; set; }
        public bool notification { get; set; }
        public object appId { get; set; }
        public object countryCode { get; set; }
    }

    public class KitInfo
    {
        public string kitNo { get; set; }
        public string cardNumber { get; set; }
        public string cardType { get; set; }
        public string cardCategory { get; set; }
        public string cardStatus { get; set; }
        public DateTime expDate { get; set; }
        public string aliasName { get; set; }
        public string networkType { get; set; }
        public string entityId { get; set; }
        public string bin { get; set; }
        public DateTime createdDate { get; set; }
        public string pinOffset { get; set; }
        public string productName { get; set; }
        public string tierType { get; set; }
        public string productId { get; set; }
    }

    public class KycInfo
    {
        public object kycRefNo { get; set; }
        public string documentType { get; set; }
        public string documentNo { get; set; }
        public object documentPath { get; set; }
        public string documentExpiry { get; set; }
        public object countryOfIssue { get; set; }
        public object documentIssuedBy { get; set; }
        public object documentIssuanceDate { get; set; }
        public object firstName { get; set; }
        public object middleName { get; set; }
        public object lastName { get; set; }
        public object hashDocumentNo { get; set; }
    }

    public class NomineeInfo
    {
        public string nomineeName { get; set; }
        public string nomineeRelationship { get; set; }
        public string nomineeDOB { get; set; }
        public bool isMinor { get; set; }
        public string guardianName { get; set; }
    }

    public class Result
    {
        public string entityId { get; set; }
        public string entityType { get; set; }
        public string businessId { get; set; }
        public string kycStatus { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public bool addOnCard { get; set; }
        public bool isNRICustomer { get; set; }
        public bool isMinor { get; set; }
        public bool isDependant { get; set; }
        public string maritalStatus { get; set; }
        public string countryCode { get; set; }
        public List<KitInfo> kitInfo { get; set; }
        public List<AddressInfo> addressInfo { get; set; }
        public List<CommunicationInfo> communicationInfo { get; set; }
        public List<AccountInfo> accountInfo { get; set; }
        public List<KycInfo> kycInfo { get; set; }
        public string dob { get; set; }
        public List<NomineeInfo> nomineeInfo { get; set; }
        public string businessType { get; set; }
        public string business { get; set; }
        public string cifNumber { get; set; }
        public List<AccountKitMapping> accountKitMapping { get; set; }
    }

}
