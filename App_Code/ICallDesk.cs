using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.ServiceModel.Web;
using System.IO;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICallDesk" in both code and config file together.

    [ServiceContract]
    public interface ICallDesk
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetData/{value}")]
        string GetData(string value);


        [OperationContract]
        bool LoginAuthentication(UserDetail objUser);



        [OperationContract]
        DataSet GetMyRaiseTicketByTicketNo(string strTicketNo);

        [OperationContract]
        DataSet GetInbox(string UserId);


        [OperationContract]
        [WebGet(UriTemplate = "RetrieveFile?Path={path}")]
        Stream RetrieveFile(string path);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "UploadFile?Path={path}")]
        //void UploadFile(string path, Stream stream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetInboxTask?UserId={UserId}")]
        Stream GetInboxTask(string UserId);


        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/CreatePerson")]
        string CreatePerson(Person createPerson);



        #region Application List

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetApplicationList")]
        Stream GetApplication(UserDetail objUserDetail);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped,
         UriTemplate = "GetApplication?ApplicationName={ApplicationName}&IssueType={IssueType}&UserId={UserId}&UserType={UserType}"
         )]
        Stream GetApplicationList(string ApplicationName, string IssueType, string UserId, string UserType);

        #endregion

        #region Get Application Master
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetApplicationMaster")]
        Stream GetApplicationMaster(UserDetail objUserDetail);
        #endregion

        #region Get Issue Type Master
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetIssueTypeMaster")]
        Stream GetIssueTypeMaster(UserDetail objUserDetail);
        #endregion

        #region Get Issue Sub Type Master
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetIssueSubTypeMaster")]
        Stream GetIssueSubTypeMaster(UserDetail objUserDetail);
        #endregion

        #region Get Last Login

        [OperationContract]
        List<UserDetail> GetLastLoginUserDetail(string UserId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetLastLogin/{UserId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream GetLastLogin(string UserId);
        #endregion

        #region Create Ticket
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "evals", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DataSet InsertTicketDetail(Ticket objTicket);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/CreateTicket")]
        Stream CreateTicket(Ticket objTicket);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/CreateTicketNew", BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream CreateTicketNew(Stream stream);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/CreateTicketIOS", BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream CreateTicketIOS(Stream stream);

        #endregion

        #region Get Approval Inbox
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetInbox")]
        Stream GetApprovalInbox(UserDetail objUserDetail);

        #endregion

        #region Update ticket

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/UpdateTicket")]
        Stream UpdateTicket(Ticket objTicket);

        #endregion

        #region Get Ticket Details
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetTicketDetail")]

        Stream GetTicketDetails(Ticket objTicket);

        #endregion

        #region Get My Raised Tickets
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetMyTickets")]

        Stream GetMyTickets(UserDetail objUser);
        #endregion

        #region Download File
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/DownloadFile")]

        Stream DownloadFile(Ticket objTicket);

        #endregion

        #region Get Sample File Format
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetFileTemplate")]

        Stream GetFileTemplate(Ticket objTicket);

        #endregion


        #region Get App Support Drop Down category
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetAppSuportCategory")]
        Stream GetAppSuportCategory(UserDetail objUserDetail);
        #endregion

        #region CallDesk Report
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/CallDeskReport")]

        Stream CallDeskReport(CalldeskReport objCalldeskReport);
        #endregion


    }   

