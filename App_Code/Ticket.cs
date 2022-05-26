using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

    
    [DataContract]
    public class Ticket
    {
        [DataMember]
        public string AuthKey { get; set; }

        [DataMember]
        public string TicketNO { get; set; }

        [DataMember]
        public int ApplicationId { get; set; }

        [DataMember]
        public string ApplicationName { get; set; }

        [DataMember]
        public int IssueTypeId { get; set; }

        [DataMember]
        public string IssueType { get; set; }

        [DataMember]
        public int IssueSubTypeId { get; set; }

        [DataMember]
        public string IssueSubType { get; set; }

        [DataMember]
        public string TicketDate { get; set; }

        [DataMember]
        public string UserRemark { get; set; }

        [DataMember]
        public string ContactNumber { get; set; }

        [DataMember]
        public decimal Ticketvalue { get; set; }

        [DataMember]
        public string UploadScreenPath { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public string ApproverId { get; set; }

        [DataMember]
        public string TicketStatus { get; set; }

        [DataMember]
        public bool UserConfirmation { get; set; }

        [DataMember]
        public string ExpectedApprovedDate { get; set; }

        [DataMember]
        public string ExpectedCloseDate { get; set; }

        [DataMember]
        public string EscalationRemark { get; set; }

        [DataMember]
        public string ReturnValue { get; set; }

        [DataMember]
        public string DueDate { get; set; }

        [DataMember]
        public string CallStartDate { get; set; }

        [DataMember]
        public string CallEndDate { get; set; }

        [DataMember]
        public bool ErrorStatus { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public byte[] FileBytes { get; set; }

        [DataMember]
        public string FileBytesAsString { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string AssignToUserId { get; set;}

        [DataMember]
        public string StatusOther { get; set; }

        [DataMember]
        public string StatusSub { get; set;}

        [DataMember]
        public string TicketMessage { get; set; }

        [DataMember]
        public int DepartmentId { get; set; }

        [DataMember]
        public string DepartmentName { get; set; }

        [DataMember]
        public string CallLoggedBranch { get; set; }

        [DataMember]
        public string SecondaryApproverId { get; set; }

        [DataMember]
        public string ApproverRemark { get; set; }

        [DataMember]
        public string ApproverCloseDate { get; set; }

        [DataMember]
        public string AppSuportRemark { get; set; }

        [DataMember]
        public string AppSuportCloseDate { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public string ApproverStatus { get; set; }

        [DataMember]
        public string AppSupportStatus { get; set; }

        [DataMember]
        public string UserMail { get; set; }

        [DataMember]
        public string ApporverName { get; set; }

        [DataMember]
        public string ApproverMail { get; set; }

        [DataMember]
        public string ApproverDesignation { get; set; }

        [DataMember]
        public string ApproverContactno { get; set; }

        [DataMember]
        public string SecondaryApproverName { get; set; }

        [DataMember]
        public string SecondaryApproverMail { get; set; }

        [DataMember]
        public string SecondaryApproverDesignation { get; set; }

        [DataMember]
        public string SecondaryApproverContactno { get; set; }

        [DataMember]
        public string SMChannel { get; set; }

        [DataMember]
        public string UserDesignation { get; set; }

        [DataMember]
        public string Priority { get; set; }

        [DataMember]
        public string Groups { get; set; }

        [DataMember]
        public string CallTAT { get; set; }

        [DataMember]
        public string ServiceCenterName { get; set; }

        [DataMember]
        public string GroupType { get; set; }

        [DataMember]
        public string RegionId { get; set; }

        [DataMember]
        public string ZoneId { get; set; }

        [DataMember]
        public string ApproverTAT { get; set; }

        [DataMember]
        public string AppSupportTAT { get; set; }

        [DataMember]
        public string AppSupportPerformer { get; set; }

        [DataMember]
        public string AppSupportClosureCategory { get; set; }

        [DataMember]
        public string AppSupportOtherRemark { get; set; }

        [DataMember]
        public string AppSupportPhoneRemark { get; set; }

        [DataMember]
        public string AppSupportFileName { get; set; }

        [DataMember]
        public string AppSupportFilePath { get; set; }

        [DataMember]
        public string ApproverFileName { get; set; }

        [DataMember]
        public string ApproverFilePath { get; set; }

        [DataMember]
        public string ActivityName { get; set; }        

    }   

    [DataContract]
    public class UserDetail
    {
        [DataMember]
        public string AuthKey { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember(EmitDefaultValue=false)]
       // [DataMember(Name = "LabelInJson", IsRequired = false)] 
       // For Ignor null value        
        public string Password { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ApplicationName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int ApplicationId { get; set; }

        [DataMember(EmitDefaultValue = false)]        
        public string IssueType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int IssueTypeId { get; set; }

        [DataMember]
        public string IssueSubType { get; set; }

        [DataMember]
        public int IssueSubTypeId { get; set; }

        [DataMember]
        public string UserType { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Designation { get; set; }

        [DataMember]
        private string SMChannel { get; set; }

        [DataMember]
        private string Email { get; set; }

        [DataMember]
        private string BranchCode { get; set; }

        [DataMember]
        private string BranchName { get; set; }

        [DataMember]
        private string RegionCode { get; set; }

        [DataMember]
        private string RegionName { get; set; }

        [DataMember]
        private string PasswordChangeDateAllowed { get; set; }

        [DataMember]
        private string LastPasswordChangedDate { get; set; }

        [DataMember]
        private string FailedPasswordAttemptCount { get; set; }

        [DataMember]
        public string UserLastLogin { get; set; }

        [DataMember]
        public string UserLastLogOut { get; set; }

    }

    [DataContract]
    public class AuthKey
    {
        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string IMEI { get; set; }

        [DataMember]
        public string TimeSpan { get; set; }
    }

    [DataContract]
    public class AppSupportCategory
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string TeamName { get; set; }
    }

    [DataContract]
    public class CalldeskReport
    {
        [DataMember]
        public string AuthKey { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string ApplicationId { get; set; }        
    }
    
