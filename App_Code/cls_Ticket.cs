using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CalldeskServices;
using System.IO;
using System.Web.Hosting;
using System.Net;
using System.Text;


public class cls_Ticket
{
    #region Connection String
    private string CallDeskConnectionString = ConfigurationManager.ConnectionStrings["CallDeskDB123"].ToString();
    #endregion

    DataTable dt;
    DataSet ds;
    Cls_validation objValidation = new Cls_validation();


    public List<string> GetApplication(UserDetail objUserDetail)
    {
        try
        {
            dt = new DataTable();

            List<string> lstApplicationDetail = new List<string>();

            SqlParameter[] objParam = new SqlParameter[4];

            objParam[0] = new SqlParameter("@UserId", SqlDbType.VarChar, 20);
            objParam[0].Value = objUserDetail.UserId;
            objParam[0].Direction = ParameterDirection.Input;

            objParam[1] = new SqlParameter("@UserType", SqlDbType.VarChar, 20);
            objParam[1].Value = objUserDetail.UserType;
            objParam[1].Direction = ParameterDirection.Input;

            objParam[2] = new SqlParameter("@ApplicationName", SqlDbType.VarChar, 50);
            objParam[2].Value = objUserDetail.ApplicationName;
            objParam[2].Direction = ParameterDirection.Input;

            objParam[3] = new SqlParameter("@IssueType", SqlDbType.VarChar, 500);
            objParam[3].Value = objUserDetail.IssueType;
            objParam[3].Direction = ParameterDirection.Input;

            dt = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.usp_MobileService_GetApplicationDetail, objParam);

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string ListName = dt.Rows[i]["ListName"].ToString();
                    lstApplicationDetail.Add(ListName);
                }
            }
            else
            {
                lstApplicationDetail = null;
            }

            return lstApplicationDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<Ticket> InsertTicketDetail(Ticket objTicket)
    {
        try
        {

            List<Ticket> lstTicketDetails = new List<Ticket>();

            DataSet dsResult = new DataSet();

            DataTable dtValidation = new DataTable();

            dtValidation = objValidation.TicketCreation_CheckMandatoryFields(objTicket);
            dsResult.Tables.Add(dtValidation);
            dsResult.Tables[0].TableName = "Validation";


            DataTable dtTicket = new DataTable();

            if (dtValidation != null && dtValidation.Rows.Count > 0 && dtValidation.Rows[0]["ValidationText"].ToString() == "Data Validated")
            //if(true) //uncomment for insert without validation
            {
                //Generate Ticket No Start
                string strTicketNo = string.Empty;
                strTicketNo = objValidation.GenerateTicketNo();
                //Generate Ticke No End

                //Upload File Start
                objTicket.TicketNO = strTicketNo;

                if (objTicket.FileBytes != null && objTicket.FileName != null)
                {
                    if (!string.IsNullOrEmpty(Encoding.Default.GetString(objTicket.FileBytes)) && !string.IsNullOrEmpty(objTicket.FileName))
                    {
                        objTicket.UploadScreenPath = UploadFile(objTicket);
                    }
                }
                //Upload File End

                SqlParameter[] objParam = new SqlParameter[9];

                objParam[0] = new SqlParameter("@TicketNumber", SqlDbType.VarChar, 20);
                objParam[0].Value = strTicketNo;
                objParam[0].Direction = ParameterDirection.Input;


                objParam[1] = new SqlParameter("@ApplicationId", SqlDbType.Int);
                objParam[1].Value = objTicket.ApplicationId;
                objParam[1].Direction = ParameterDirection.Input;

                objParam[2] = new SqlParameter("@IssueTypeId", SqlDbType.Int);
                objParam[2].Value = objTicket.IssueTypeId;
                objParam[2].Direction = ParameterDirection.Input;

                objParam[3] = new SqlParameter("@IssueSubTypeId", SqlDbType.Int);
                objParam[3].Value = objTicket.IssueSubTypeId;
                objParam[3].Direction = ParameterDirection.Input;

                objParam[4] = new SqlParameter("@CallCreatedBy", SqlDbType.VarChar, 50);
                objParam[4].Value = objTicket.CreatedBy;
                objParam[4].Direction = ParameterDirection.Input;

                objParam[5] = new SqlParameter("@ContactNumber", SqlDbType.VarChar, 20);
                objParam[5].Value = objTicket.ContactNumber;
                objParam[5].Direction = ParameterDirection.Input;

                objParam[6] = new SqlParameter("@UserRemark", SqlDbType.VarChar, 1000);
                objParam[6].Value = objTicket.UserRemark;
                objParam[6].Direction = ParameterDirection.Input;

                objParam[7] = new SqlParameter("@UploadedScreen", SqlDbType.VarChar, 1000);
                objParam[7].Value = objTicket.UploadScreenPath;
                objParam[7].Direction = ParameterDirection.Input;

                objParam[8] = new SqlParameter("@TicketValue", SqlDbType.VarChar, 1000);
                objParam[8].Value = objTicket.Ticketvalue;
                objParam[8].Direction = ParameterDirection.Input;

                dtTicket = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.usp_MobileService_AddCallDetails, objParam);

                string strReturnValue = dtTicket.Rows[0]["ReturnValue"].ToString();
                //Savvion Service Start
                bool IsSuccess = false;
                if (strReturnValue.Length == 13)
                {
                    IsSuccess = objValidation.SavvionService(strTicketNo);

                    if (IsSuccess == false)
                    {
                        lstTicketDetails.Add(new Ticket { ErrorStatus = true, TicketMessage = dtValidation.Rows[0]["ValidationText"].ToString() });
                        return lstTicketDetails;
                    }
                    else
                    {
                        lstTicketDetails.Add(new Ticket
                        {
                            ReturnValue = strReturnValue,
                            TicketNO = strTicketNo,
                            ErrorStatus = false
                        });
                    }

                }
                else
                {
                    lstTicketDetails.Add(new Ticket { ErrorStatus = true, TicketMessage = strReturnValue });
                    return lstTicketDetails;
                }
            }
            //Savvion Service End

                //dsResult.Tables.Add(dtTicket);
            //dsResult.Tables[1].TableName = "ResponseResult";
            else
            {
                lstTicketDetails.Add(new Ticket { ErrorStatus = true, TicketMessage = dtValidation.Rows[0]["ValidationText"].ToString() });
                return lstTicketDetails;
            }
            return lstTicketDetails;

        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public List<UserDetail> GetLastLoginUserDetail(string UserId)
    {
        try
        {
            dt = new DataTable();

            List<UserDetail> lstLastLoginUserDetail = new List<UserDetail>();

            SqlParameter[] objParam = new SqlParameter[1];

            objParam[0] = new SqlParameter("@UserId", SqlDbType.VarChar, 20);
            objParam[0].Value = UserId;
            objParam[0].Direction = ParameterDirection.Input;


            dt = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.usp_MobileServiceGetLastLoginUserDetail, objParam);

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lstLastLoginUserDetail.Add(new UserDetail
                    {
                        UserId = dt.Rows[i]["UserName"].ToString(),
                        UserLastLogin = dt.Rows[i]["UserLastLogin"].ToString()

                    });
                }
            }
            else
            {
                lstLastLoginUserDetail = null;
            }

            return lstLastLoginUserDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<Ticket> GetApprovalInbox(UserDetail objUserDetail)
    {
        List<Ticket> lstTicketDetails = new List<Ticket>();

        try
        {
            
            TechDeskObjectService.WS_TechDeskService objReq = new TechDeskObjectService.WS_TechDeskService();

            TechDeskObjectService.TechDeskObject objTechDeskService = new TechDeskObjectService.TechDeskObject();
            TechDeskObjectService.ResponseObject objResponse = new TechDeskObjectService.ResponseObject();
            TechDeskObjectService.RequestObject objRequestService = new TechDeskObjectService.RequestObject();

            objRequestService.userId = objUserDetail.UserId;

            objResponse = objReq.getTaskList(objRequestService);


            if (objResponse.responseCode == "5000")
            {

                for (int i = 0; i < objResponse.resultworkItemArray.Length; i++)
                {
                    string TicketNo = objResponse.resultworkItemArray[i].ticketNo;

                    SqlParameter[] objParam = new SqlParameter[1];
                    objParam[0] = new SqlParameter("@TicketNumberPK", SqlDbType.VarChar, 20);
                    objParam[0].Value = TicketNo;
                    objParam[0].Direction = ParameterDirection.Input;

                    DataTable dtTicketDetails = new DataTable();
                    dtTicketDetails = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.usp_MobileService_GetMyRaiseTicketByTicketNo, objParam);


                    string strTicketStatus = dtTicketDetails.Rows[0]["CallStatus"].ToString();
                    string strAppSupportPerformer = null;

                    if (strTicketStatus == "Open")
                    {
                        TechDeskObjectService.WS_TechDeskService objTDS = new TechDeskObjectService.WS_TechDeskService();

                        strAppSupportPerformer = objTDS.getPerformer(TicketNo, "AppSupport");

                    }
                    else
                    {
                        strAppSupportPerformer = dtTicketDetails.Rows[0]["AppSuportPerformer"].ToString();
                    }

                    if (dtTicketDetails != null && dtTicketDetails.Rows.Count > 0)
                    {

                        lstTicketDetails.Add(new Ticket
                        {
                            TicketNO = dtTicketDetails.Rows[0]["TicketNumberPK"].ToString(),
                            TicketDate = dtTicketDetails.Rows[0]["CallDate"].ToString(),
                            UserRemark = dtTicketDetails.Rows[0]["UserRemark"].ToString(),
                            ApproverId = dtTicketDetails.Rows[0]["ApproverID"].ToString(),
                            ApplicationName = dtTicketDetails.Rows[0]["ApplicationName"].ToString(),
                            IssueType = dtTicketDetails.Rows[0]["IssueRequestType"].ToString(),
                            IssueSubType = dtTicketDetails.Rows[0]["IssueRequestSubType"].ToString(),
                            ApplicationId = Convert.ToInt32(dtTicketDetails.Rows[0]["ApplicationID_PK"]),
                            IssueTypeId = Convert.ToInt32(dtTicketDetails.Rows[0]["IssueRequestType_PK"]),
                            IssueSubTypeId = Convert.ToInt32(dtTicketDetails.Rows[0]["IssueRequestSubType_PK"]),
                            ContactNumber = dtTicketDetails.Rows[0]["ContactNumber"].ToString(),
                            Ticketvalue = Convert.ToDecimal(dtTicketDetails.Rows[0]["TicketValue"]),
                            UploadScreenPath = dtTicketDetails.Rows[0]["UploadedScreen"].ToString(),
                            CreatedBy = dtTicketDetails.Rows[0]["CallCreatedBy"].ToString(),
                            TicketStatus = dtTicketDetails.Rows[0]["CallStatus"].ToString(),
                            ExpectedApprovedDate = dtTicketDetails.Rows[0]["ExpectedApprovedDate"].ToString(),
                            ExpectedCloseDate = dtTicketDetails.Rows[0]["ExpectedCloseDate"].ToString(),
                            EscalationRemark = dtTicketDetails.Rows[0]["EscalationRemark"].ToString(),
                            CallStartDate = dtTicketDetails.Rows[0]["CallDate"].ToString(),
                            CallEndDate = dtTicketDetails.Rows[0]["AppSupportCloseDate"].ToString(),
                            CallLoggedBranch = dtTicketDetails.Rows[0]["CallLoggedBranch"].ToString(),
                            SecondaryApproverId = dtTicketDetails.Rows[0]["SApproverID"].ToString(),
                            ApproverRemark = dtTicketDetails.Rows[0]["ApproverRemark"].ToString(),
                            ApproverCloseDate = dtTicketDetails.Rows[0]["ApproverClosedDate"].ToString(),
                            AppSuportRemark = dtTicketDetails.Rows[0]["AppSupportRemark"].ToString(),
                            AppSuportCloseDate = dtTicketDetails.Rows[0]["AppSupportCloseDate"].ToString(),
                            BranchName = dtTicketDetails.Rows[0]["BranchName"].ToString(),
                            ApproverStatus = dtTicketDetails.Rows[0]["ApproverStatus"].ToString(),
                            AppSupportStatus = dtTicketDetails.Rows[0]["AppSupportStatus"].ToString(),
                            UserMail = dtTicketDetails.Rows[0]["UserMail"].ToString(),
                            ApporverName = dtTicketDetails.Rows[0]["ApproverName"].ToString(),
                            ApproverMail = dtTicketDetails.Rows[0]["ApproverMail"].ToString(),
                            ApproverDesignation = dtTicketDetails.Rows[0]["ApproverDesignation"].ToString(),
                            ApproverContactno = dtTicketDetails.Rows[0]["ApproverContactno"].ToString(),
                            SecondaryApproverName = dtTicketDetails.Rows[0]["SApproverName"].ToString(),
                            SecondaryApproverMail = dtTicketDetails.Rows[0]["SApproverMail"].ToString(),
                            SecondaryApproverDesignation = dtTicketDetails.Rows[0]["SApproverDesignation"].ToString(),
                            SecondaryApproverContactno = dtTicketDetails.Rows[0]["SApproverContactno"].ToString(),
                            SMChannel = dtTicketDetails.Rows[0]["SMChannel"].ToString(),
                            UserDesignation = dtTicketDetails.Rows[0]["UserDesignation"].ToString(),
                            Priority = dtTicketDetails.Rows[0]["Priority"].ToString(),
                            Groups = dtTicketDetails.Rows[0]["Groups"].ToString(),
                            ServiceCenterName = dtTicketDetails.Rows[0]["ServiceCenterName"].ToString(),
                            GroupType = dtTicketDetails.Rows[0]["GroupType"].ToString(),
                            RegionId = dtTicketDetails.Rows[0]["scRegionID"].ToString(),
                            ZoneId = dtTicketDetails.Rows[0]["scZoneID"].ToString(),
                            CallTAT = dtTicketDetails.Rows[0]["CallTAT"].ToString(),
                            ApproverTAT = dtTicketDetails.Rows[0]["ApproverTAT"].ToString(),
                            AppSupportTAT = dtTicketDetails.Rows[0]["AppSupportTAT"].ToString(),
                            AppSupportPerformer = strAppSupportPerformer,
                            AppSupportClosureCategory = dtTicketDetails.Rows[0]["AppSupportClosureCategory"].ToString(),
                            AppSupportOtherRemark = dtTicketDetails.Rows[0]["AppSupportOtherRemark"].ToString(),
                            AppSupportPhoneRemark = dtTicketDetails.Rows[0]["AppSupportPhoneRemark"].ToString(),
                            FileName = dtTicketDetails.Rows[0]["FileName"].ToString(),
                            ActivityName = objResponse.resultworkItemArray[i].workStepName,
                            //FileBytes = GetFileBytesWeb(dtTicketDetails.Rows[0]["UploadedScreen"].ToString()),
                            ErrorStatus = false
                        });
                    }
                }

                //List<Ticket> SortedList = lstTicketDetails.OrderByDescending(o => Convert.ToDateTime(o.TicketDate)).ToList();

                return lstTicketDetails;
            }
            else
            {
                lstTicketDetails.Add(new Ticket { ErrorStatus = true, TicketMessage = MessageConstants.SavvionError + "[" + objResponse.responseCode + "]" + objResponse.message });
                return lstTicketDetails;
            }


        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public List<Ticket> UpdateTicket(Ticket objTicket)
    {
        List<Ticket> lstTicketDetails = new List<Ticket>();

        try
        {
            DataTable dtValidation = new DataTable();

            dtValidation = objValidation.TicketUpdation_CheckMandatoryFields(objTicket);

            if (dtValidation != null && dtValidation.Rows.Count > 0 && dtValidation.Rows[0]["ValidationText"].ToString() == "Data Validated")
            //if(true) //uncomment for update without validation
            {

                TechDeskObjectService.WS_TechDeskService objReq = new TechDeskObjectService.WS_TechDeskService();

                TechDeskObjectService.TechDeskObject objTechDeskService = new TechDeskObjectService.TechDeskObject();
                TechDeskObjectService.ResponseObject objResponse = new TechDeskObjectService.ResponseObject();
                TechDeskObjectService.RequestObject objRequestService = new TechDeskObjectService.RequestObject();

                objRequestService.techdesk = objTechDeskService;

                AuthKey objAuthKey = new AuthKey();
                objAuthKey = CallDeskServiceRequest.DecryptAuthKey(objTicket.AuthKey);

                objRequestService.fileBytes = objTicket.FileBytes;
                objRequestService.assignToUserId = objTicket.AssignToUserId;
                objRequestService.remarks = objTicket.UserRemark;
                objRequestService.status = objTicket.TicketStatus;
                objRequestService.status_Other = objTicket.StatusOther;
                objRequestService.status_Sub = objTicket.StatusSub;
                objRequestService.ticketNumber = objTicket.TicketNO;
                objRequestService.userId = objAuthKey.UserId;
                //objRequestService.userId = "parshu";

                objResponse = objReq.UpdateTicket(objRequestService);


                if (objResponse.responseCode == "5000")
                {

                    lstTicketDetails.Add(new Ticket
                    {
                        TicketNO = objTicket.TicketNO,
                        TicketMessage = objResponse.message,
                        ErrorStatus = false

                    });



                }
                else
                {
                    lstTicketDetails.Add(new Ticket { TicketNO = objTicket.TicketNO, ErrorStatus = true, TicketMessage = MessageConstants.SavvionError + "[" + objResponse.responseCode + "]" + objResponse.message });
                    return lstTicketDetails;
                }
            }

            else
            {
                lstTicketDetails.Add(new Ticket { ErrorStatus = true, TicketMessage = dtValidation.Rows[0]["ValidationText"].ToString() });
                return lstTicketDetails;
            }
            return lstTicketDetails;

        }
        catch (Exception ex)
        {

            return null;
        }
    }

    public List<Ticket> GetTicketDetails(string strTicketNo)
    {
        List<Ticket> lstTicketDetails = new List<Ticket>();

        DataTable dtTicketDetails = new DataTable();

        try
        {
            SqlParameter[] objParam = new SqlParameter[1];

            objParam[0] = new SqlParameter("@TicketNumberPK", SqlDbType.VarChar, 20);
            objParam[0].Value = strTicketNo;
            objParam[0].Direction = ParameterDirection.Input;


            dtTicketDetails = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.usp_MobileService_GetMyRaiseTicketByTicketNo, objParam);

            if (dtTicketDetails != null && dtTicketDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dtTicketDetails.Rows.Count; i++)
                {

                    string strTicketStatus = dtTicketDetails.Rows[i]["CallStatus"].ToString();
                    string strTicketNumber = dtTicketDetails.Rows[i]["TicketNumberPK"].ToString();
                    string strAppSupportPerformer = null;

                    if (strTicketStatus == "Open")
                    {
                        TechDeskObjectService.WS_TechDeskService objTDS = new TechDeskObjectService.WS_TechDeskService();

                        strAppSupportPerformer = objTDS.getPerformer(strTicketNumber, "AppSupport");
                    }
                    else
                    {
                        strAppSupportPerformer = dtTicketDetails.Rows[i]["AppSuportPerformer"].ToString();
                    }

                    lstTicketDetails.Add(new Ticket
                    {
                        TicketNO = dtTicketDetails.Rows[i]["TicketNumberPK"].ToString(),
                        TicketDate = dtTicketDetails.Rows[i]["CallDate"].ToString(),
                        UserRemark = dtTicketDetails.Rows[i]["UserRemark"].ToString(),
                        ApproverId = dtTicketDetails.Rows[i]["ApproverID"].ToString(),
                        ApplicationName = dtTicketDetails.Rows[i]["ApplicationName"].ToString(),
                        IssueType = dtTicketDetails.Rows[i]["IssueRequestType"].ToString(),
                        IssueSubType = dtTicketDetails.Rows[i]["IssueRequestSubType"].ToString(),
                        ApplicationId = Convert.ToInt32(dtTicketDetails.Rows[i]["ApplicationID_PK"]),
                        IssueTypeId = Convert.ToInt32(dtTicketDetails.Rows[i]["IssueRequestType_PK"]),
                        IssueSubTypeId = Convert.ToInt32(dtTicketDetails.Rows[i]["IssueRequestSubType_PK"]),
                        ContactNumber = dtTicketDetails.Rows[i]["ContactNumber"].ToString(),
                        Ticketvalue = Convert.ToDecimal(dtTicketDetails.Rows[i]["TicketValue"]),
                        UploadScreenPath = dtTicketDetails.Rows[i]["UploadedScreen"].ToString(),
                        CreatedBy = dtTicketDetails.Rows[i]["CallCreatedBy"].ToString(),
                        TicketStatus = dtTicketDetails.Rows[i]["CallStatus"].ToString(),
                        ExpectedApprovedDate = dtTicketDetails.Rows[i]["ExpectedApprovedDate"].ToString(),
                        ExpectedCloseDate = dtTicketDetails.Rows[i]["ExpectedCloseDate"].ToString(),
                        EscalationRemark = dtTicketDetails.Rows[i]["EscalationRemark"].ToString(),
                        CallStartDate = dtTicketDetails.Rows[i]["CallDate"].ToString(),
                        CallEndDate = dtTicketDetails.Rows[i]["AppSupportCloseDate"].ToString(),
                        CallLoggedBranch = dtTicketDetails.Rows[i]["CallLoggedBranch"].ToString(),
                        SecondaryApproverId = dtTicketDetails.Rows[i]["SApproverID"].ToString(),
                        ApproverRemark = dtTicketDetails.Rows[i]["ApproverRemark"].ToString(),
                        ApproverCloseDate = dtTicketDetails.Rows[i]["ApproverClosedDate"].ToString(),
                        AppSuportRemark = dtTicketDetails.Rows[i]["AppSupportRemark"].ToString(),
                        AppSuportCloseDate = dtTicketDetails.Rows[i]["AppSupportCloseDate"].ToString(),
                        BranchName = dtTicketDetails.Rows[i]["BranchName"].ToString(),
                        ApproverStatus = dtTicketDetails.Rows[i]["ApproverStatus"].ToString(),
                        AppSupportStatus = dtTicketDetails.Rows[i]["AppSupportStatus"].ToString(),
                        UserMail = dtTicketDetails.Rows[i]["UserMail"].ToString(),
                        ApporverName = dtTicketDetails.Rows[i]["ApproverName"].ToString(),
                        ApproverMail = dtTicketDetails.Rows[i]["ApproverMail"].ToString(),
                        ApproverDesignation = dtTicketDetails.Rows[i]["ApproverDesignation"].ToString(),
                        ApproverContactno = dtTicketDetails.Rows[i]["ApproverContactno"].ToString(),
                        SecondaryApproverName = dtTicketDetails.Rows[i]["SApproverName"].ToString(),
                        SecondaryApproverMail = dtTicketDetails.Rows[i]["SApproverMail"].ToString(),
                        SecondaryApproverDesignation = dtTicketDetails.Rows[i]["SApproverDesignation"].ToString(),
                        SecondaryApproverContactno = dtTicketDetails.Rows[i]["SApproverContactno"].ToString(),
                        SMChannel = dtTicketDetails.Rows[i]["SMChannel"].ToString(),
                        UserDesignation = dtTicketDetails.Rows[i]["UserDesignation"].ToString(),
                        Priority = dtTicketDetails.Rows[i]["Priority"].ToString(),
                        Groups = dtTicketDetails.Rows[i]["Groups"].ToString(),
                        ServiceCenterName = dtTicketDetails.Rows[i]["ServiceCenterName"].ToString(),
                        GroupType = dtTicketDetails.Rows[i]["GroupType"].ToString(),
                        RegionId = dtTicketDetails.Rows[i]["scRegionID"].ToString(),
                        ZoneId = dtTicketDetails.Rows[i]["scZoneID"].ToString(),
                        CallTAT = dtTicketDetails.Rows[i]["CallTAT"].ToString(),
                        ApproverTAT = dtTicketDetails.Rows[i]["ApproverTAT"].ToString(),
                        AppSupportTAT = dtTicketDetails.Rows[i]["AppSupportTAT"].ToString(),
                        AppSupportPerformer = strAppSupportPerformer,
                        AppSupportClosureCategory = dtTicketDetails.Rows[i]["AppSupportClosureCategory"].ToString(),
                        AppSupportOtherRemark = dtTicketDetails.Rows[i]["AppSupportOtherRemark"].ToString(),
                        AppSupportPhoneRemark = dtTicketDetails.Rows[i]["AppSupportPhoneRemark"].ToString(),
                        FileName = dtTicketDetails.Rows[i]["FileName"].ToString(),
                        //FileBytes = GetFileBytesWeb(dtTicketDetails.Rows[i]["UploadedScreen"].ToString()),
                        ErrorStatus = false

                    });
                }
            }
            else
            {
                lstTicketDetails = null;
            }

            return lstTicketDetails;
        }

        catch (Exception ex)
        {

            throw ex;
        }
    }

    public List<Ticket> GetMyTickets(string UserId)
    {
        List<Ticket> lstTicketDetails = new List<Ticket>();

        DataTable dtTicketDetails = new DataTable();

        try
        {
            SqlParameter[] objParam = new SqlParameter[1];

            objParam[0] = new SqlParameter("@UserId", SqlDbType.VarChar, 20);
            objParam[0].Value = UserId;
            objParam[0].Direction = ParameterDirection.Input;


            dtTicketDetails = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.Usp_MobileService_GetInbox, objParam);


            if (dtTicketDetails != null && dtTicketDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dtTicketDetails.Rows.Count; i++)
                {
                    string strTicketStatus = dtTicketDetails.Rows[i]["CallStatus"].ToString();
                    string strTicketNumber = dtTicketDetails.Rows[i]["TicketNumberPK"].ToString();
                    string strAppSupportPerformer = null;

                    if (strTicketStatus == "Open")
                    {
                        TechDeskObjectService.WS_TechDeskService objTDS = new TechDeskObjectService.WS_TechDeskService();

                        strAppSupportPerformer = objTDS.getPerformer(strTicketNumber, "AppSupport");

                    }
                    else
                    {
                        strAppSupportPerformer = dtTicketDetails.Rows[i]["AppSuportPerformer"].ToString();
                    }

                    lstTicketDetails.Add(new Ticket
                    {
                        TicketNO = Convert.ToString(dtTicketDetails.Rows[i]["TicketNumberPK"]),
                        TicketDate = Convert.ToString(dtTicketDetails.Rows[i]["CallDate"]),
                        UserRemark = Convert.ToString(dtTicketDetails.Rows[i]["UserRemark"]),
                        ApproverId = Convert.ToString(dtTicketDetails.Rows[i]["ApproverID"]),
                        ApplicationName = Convert.ToString(dtTicketDetails.Rows[i]["ApplicationName"]),
                        IssueType = Convert.ToString(dtTicketDetails.Rows[i]["IssueRequestType"]),
                        IssueSubType = Convert.ToString(dtTicketDetails.Rows[i]["IssueRequestSubType"]),
                        ApplicationId = Convert.ToInt32(dtTicketDetails.Rows[i]["ApplicationID_PK"]),
                        IssueTypeId = Convert.ToInt32(dtTicketDetails.Rows[i]["IssueRequestType_PK"]),
                        IssueSubTypeId = Convert.ToInt32(dtTicketDetails.Rows[i]["IssueRequestSubType_PK"]),
                        ContactNumber = Convert.ToString(dtTicketDetails.Rows[i]["ContactNumber"]),
                        Ticketvalue = Convert.ToDecimal(dtTicketDetails.Rows[i]["TicketValue"]),
                        UploadScreenPath = Convert.ToString(dtTicketDetails.Rows[i]["UploadedScreen"]),
                        CreatedBy = Convert.ToString(dtTicketDetails.Rows[i]["CallCreatedBy"]),
                        TicketStatus = Convert.ToString(dtTicketDetails.Rows[i]["CallStatus"]),
                        ExpectedApprovedDate = Convert.ToString(dtTicketDetails.Rows[i]["ExpectedApprovedDate"]),
                        ExpectedCloseDate = Convert.ToString(dtTicketDetails.Rows[i]["ExpectedCloseDate"]),
                        EscalationRemark = Convert.ToString(dtTicketDetails.Rows[i]["EscalationRemark"]),
                        CallStartDate = Convert.ToString(dtTicketDetails.Rows[i]["CallDate"]),
                        CallEndDate = Convert.ToString(dtTicketDetails.Rows[i]["AppSupportCloseDate"]),
                        CallLoggedBranch = Convert.ToString(dtTicketDetails.Rows[i]["CallLoggedBranch"]),
                        SecondaryApproverId = Convert.ToString(dtTicketDetails.Rows[i]["SApproverID"]),
                        ApproverRemark = Convert.ToString(dtTicketDetails.Rows[i]["ApproverRemark"]),
                        ApproverCloseDate = Convert.ToString(dtTicketDetails.Rows[i]["ApproverClosedDate"]),
                        AppSuportRemark = Convert.ToString(dtTicketDetails.Rows[i]["AppSupportRemark"]),
                        AppSuportCloseDate = Convert.ToString(dtTicketDetails.Rows[i]["AppSupportCloseDate"]),
                        BranchName = Convert.ToString(dtTicketDetails.Rows[i]["BranchName"]),
                        ApproverStatus = Convert.ToString(dtTicketDetails.Rows[i]["ApproverStatus"]),
                        AppSupportStatus = Convert.ToString(dtTicketDetails.Rows[i]["AppSupportStatus"]),
                        UserMail = Convert.ToString(dtTicketDetails.Rows[i]["UserMail"]),
                        ApporverName = Convert.ToString(dtTicketDetails.Rows[i]["ApproverName"]),
                        ApproverMail = Convert.ToString(dtTicketDetails.Rows[i]["ApproverMail"]),
                        ApproverDesignation = Convert.ToString(dtTicketDetails.Rows[i]["ApproverDesignation"]),
                        ApproverContactno = Convert.ToString(dtTicketDetails.Rows[i]["ApproverContactno"]),
                        SecondaryApproverName = Convert.ToString(dtTicketDetails.Rows[i]["SApproverName"]),
                        SecondaryApproverMail = Convert.ToString(dtTicketDetails.Rows[i]["SApproverMail"]),
                        SecondaryApproverDesignation = Convert.ToString(dtTicketDetails.Rows[i]["SApproverDesignation"]),
                        SecondaryApproverContactno = Convert.ToString(dtTicketDetails.Rows[i]["SApproverContactno"]),
                        SMChannel = Convert.ToString(dtTicketDetails.Rows[i]["SMChannel"]),
                        UserDesignation = Convert.ToString(dtTicketDetails.Rows[i]["UserDesignation"]),
                        Priority = Convert.ToString(dtTicketDetails.Rows[i]["Priority"]),
                        Groups = Convert.ToString(dtTicketDetails.Rows[i]["Groups"]),
                        ServiceCenterName = Convert.ToString(dtTicketDetails.Rows[i]["ServiceCenterName"]),
                        GroupType = Convert.ToString(dtTicketDetails.Rows[i]["GroupType"]),
                        RegionId = Convert.ToString(dtTicketDetails.Rows[i]["scRegionID"]),
                        ZoneId = Convert.ToString(dtTicketDetails.Rows[i]["scZoneID"]),
                        CallTAT = Convert.ToString(dtTicketDetails.Rows[i]["CallTAT"]),
                        ApproverTAT = Convert.ToString(dtTicketDetails.Rows[i]["ApproverTAT"]),
                        AppSupportTAT = Convert.ToString(dtTicketDetails.Rows[i]["AppSupportTAT"]),
                        AppSupportPerformer = strAppSupportPerformer,
                        AppSupportClosureCategory = Convert.ToString(dtTicketDetails.Rows[i]["AppSupportClosureCategory"]),
                        AppSupportOtherRemark = Convert.ToString(dtTicketDetails.Rows[i]["AppSupportOtherRemark"]),
                        AppSupportPhoneRemark = Convert.ToString(dtTicketDetails.Rows[i]["AppSupportPhoneRemark"]),
                        FileName = Convert.ToString(dtTicketDetails.Rows[i]["FileName"]),
                        //FileBytes = GetFileBytesWeb(dtTicketDetails.Rows[i]["UploadedScreen"].ToString()),
                        ErrorStatus = false

                    });
                }
            }
            else
            {
                lstTicketDetails = null;
            }

            return lstTicketDetails;
        }

        catch (Exception ex)
        {

            throw ex;
        }
    }

    public string UploadFile(Ticket objTicket)
    {
        //string fileName = Guid.NewGuid().ToString().Split('-')[0] + "-" + objTicket.FileName;
        string fileName = objTicket.TicketNO + "-" + objTicket.FileName;
        Stream stream = new MemoryStream(objTicket.FileBytes);
        //Stream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(objTicket.FileBytesAsString));
        string FilePath = AppSettingsUtility.GetAppSettingsKeyValue(AppSettingsConstants.UploadSavePath);
        //FilePath = FilePath + objTicket.TicketNO + "\\";
        if (!Directory.Exists(FilePath))
        {
            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory(FilePath);
        }

        //FilePath = Path.Combine(HostingEnvironment.MapPath(FilePath), fileName);
        //int length = 0;
        //using (FileStream writer = new FileStream(FilePath, FileMode.Create))
        //{
        //    int readCount;
        //    byte[] buffer = new byte[8192];
        //    while ((readCount = stream.Read(buffer, 0, buffer.Length)) != 0)
        //    {
        //        writer.Write(buffer, 0, readCount);
        //        length += readCount;
        //    }
        //}

        using (FileStream fileStream = System.IO.File.Create(FilePath + fileName, (int)stream.Length))
        {
            // Fill the bytes[] array with the stream data
            byte[] bytesInStream = new byte[stream.Length];
            stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

            // Use FileStream object to write to the specified file
            fileStream.Write(bytesInStream, 0, bytesInStream.Length);
        }

        string strSavePath = null;
        string WebsiteFilePath = AppSettingsUtility.GetAppSettingsKeyValue(AppSettingsConstants.UploadsavePathWebsite);
        //strSavePath = WebsiteFilePath + objTicket.TicketNO + "/" + fileName;
        strSavePath = WebsiteFilePath + "/" + fileName;

        return strSavePath;
    }


    public List<Ticket> DownloadFile(Ticket objTicket)
    {
        List<Ticket> lstTicketDetails = new List<Ticket>();

        try
        {

            lstTicketDetails.Add(new Ticket
            {
                //Get File Bytes
                //FileBytes = GetFileBytesWeb(objTicket.UploadScreenPath),
                FileBytes = GetFileBytes(objTicket.UploadScreenPath),
                //FileBytesAsString = System.Text.Encoding.UTF8.GetString(GetFileBytes(objTicket.UploadScreenPath)),
                FileBytesAsString = Convert.ToBase64String(GetFileBytes(objTicket.UploadScreenPath)),
                ErrorStatus = false

            });

            return lstTicketDetails;
        }
        catch (Exception ex)
        {

            lstTicketDetails.Add(new Ticket { ErrorStatus = true, TicketMessage = MessageConstants.DownloadFileNotAvailable });
            return lstTicketDetails;
        }

    }

    //public List<Ticket> DownloadFile(Ticket objTicket)
    //{
    //    List<Ticket> lstTicketDetails = new List<Ticket>();
    //    DataTable dtTicketDetails = new DataTable();
    //    try
    //    {

    //        SqlParameter[] objParam = new SqlParameter[1];

    //        objParam[0] = new SqlParameter("@TicketNumberPK", SqlDbType.VarChar, 20);
    //        objParam[0].Value = objTicket.TicketNO;
    //        objParam[0].Direction = ParameterDirection.Input;


    //        dtTicketDetails = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.Usp_MobileService_GetUploadScreenPath, objParam);

    //        if (dtTicketDetails != null && dtTicketDetails.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dtTicketDetails.Rows.Count; i++)
    //            {
    //                lstTicketDetails.Add(new Ticket
    //                {
    //                    TicketNO = dtTicketDetails.Rows[i]["TicketNumberPK"].ToString(),
    //                    UploadScreenPath = dtTicketDetails.Rows[i]["UploadedScreen"].ToString(),
    //                    //Get File Bytes
    //                    FileBytes = GetFileBytesWeb(dtTicketDetails.Rows[i]["UploadedScreen"].ToString())

    //                });
    //            }
    //        }
    //        else
    //        {
    //            lstTicketDetails = null;
    //        }

    //        return lstTicketDetails;
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }

    //}

    public byte[] GetFileBytesWeb(string FilePath)
    {
        if (string.IsNullOrEmpty(FilePath))
        {
            return null;

        }
        else
        {
            //byte[] Filebytes = File.ReadAllBytes(FilePath);
            WebClient webClient = new WebClient();
            byte[] Filebytes = webClient.DownloadData(FilePath);
            return Filebytes;
        }

    }

    public byte[] GetFileBytesForTemplates(string FilePath)
    {

        if (string.IsNullOrEmpty(FilePath))
        {
            return null;
        }
        else
        {
            //string filename = Path.GetFileName(FilePath);

            //string LocalFilePath = AppSettingsUtility.GetAppSettingsKeyValue(AppSettingsConstants.UploadSavePath);

            //FilePath = LocalFilePath + filename;

            //string path = "C://hello//world";
            //int pos = path.LastIndexOf("/") + 1;
            //Console.WriteLine(path.Substring(pos, path.Length - pos));


            byte[] Filebytes = File.ReadAllBytes(FilePath);
            //WebClient webClient = new WebClient();
            //byte[] Filebytes = webClient.DownloadData(FilePath);
            return Filebytes;

        }

    }

    public byte[] GetFileBytes(string FilePath)
    {

        if (string.IsNullOrEmpty(FilePath))
        {
            return null;
        }
        else
        {
            string filename = Path.GetFileName(FilePath);

            string LocalFilePath = AppSettingsUtility.GetAppSettingsKeyValue(AppSettingsConstants.UploadSavePath);

            FilePath = LocalFilePath + filename;

            //string path = "C://hello//world";
            //int pos = path.LastIndexOf("/") + 1;
            //Console.WriteLine(path.Substring(pos, path.Length - pos));


            byte[] Filebytes = File.ReadAllBytes(FilePath);
            //WebClient webClient = new WebClient();
            //byte[] Filebytes = webClient.DownloadData(FilePath);
            return Filebytes;



        }

    }

    public List<Ticket> GetFileTemplate(Ticket objTicket)
    {
        List<Ticket> lstTicketDetails = new List<Ticket>();
        DataTable dtTicketDetails = new DataTable();
        try
        {

            SqlParameter[] objParam = new SqlParameter[3];

            objParam[0] = new SqlParameter("@ApplicationId", SqlDbType.Int);
            objParam[0].Value = objTicket.ApplicationId;
            objParam[0].Direction = ParameterDirection.Input;

            objParam[1] = new SqlParameter("@IssueTypeId", SqlDbType.Int);
            objParam[1].Value = objTicket.IssueTypeId;
            objParam[1].Direction = ParameterDirection.Input;

            objParam[2] = new SqlParameter("@IssueSubTypeId", SqlDbType.Int);
            objParam[2].Value = objTicket.IssueSubTypeId;
            objParam[2].Direction = ParameterDirection.Input;


            dtTicketDetails = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.Usp_MobileService_GetFileTemplate, objParam);

            if (dtTicketDetails != null && dtTicketDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dtTicketDetails.Rows.Count; i++)
                {
                    string filePath = null;
                    if (!string.IsNullOrEmpty(dtTicketDetails.Rows[i]["FileTemplateName"].ToString()))
                    {
                        filePath = AppSettingsUtility.GetAppSettingsKeyValue(AppSettingsConstants.FileTemplatePath);
                        filePath = filePath + dtTicketDetails.Rows[i]["FileTemplateName"].ToString();
                    }

                    lstTicketDetails.Add(new Ticket
                    {
                        TicketMessage = dtTicketDetails.Rows[i]["TicketMessage"].ToString(),
                        FileName = dtTicketDetails.Rows[i]["FileTemplateName"].ToString(),
                        //Get File Bytes
                        FileBytes = GetFileBytesForTemplates(filePath)

                    });
                }
            }
            else
            {
                lstTicketDetails = null;
            }

            return lstTicketDetails;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public List<Ticket> GetApplicationMaster()
    {
        List<Ticket> lstTicketDetails;
        lstTicketDetails = new List<Ticket>();

        DataTable dtApplicationMaster = new DataTable();

        try
        {

            dtApplicationMaster = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.Usp_MobileService_GetApplicationMaster);


            if (dtApplicationMaster != null && dtApplicationMaster.Rows.Count > 0)
            {
                for (int i = 0; i < dtApplicationMaster.Rows.Count; i++)
                {
                    lstTicketDetails.Add(new Ticket
                    {
                        ApplicationId = Convert.ToInt32(dtApplicationMaster.Rows[i]["ApplicationID_PK"]),
                        ApplicationName = dtApplicationMaster.Rows[i]["ApplicationName"].ToString(),
                        DepartmentId = Convert.ToInt32(dtApplicationMaster.Rows[i]["Departmentid"]),
                        DepartmentName = dtApplicationMaster.Rows[i]["DepartmentName"].ToString()


                    });
                }
            }
            else
            {
                lstTicketDetails = null;
            }

            return lstTicketDetails;
        }

        catch (Exception ex)
        {

            throw ex;
        }
    }

    public List<Ticket> GetIssueTypeMaster()
    {
        List<Ticket> lstTicketDetails = new List<Ticket>();

        DataTable dtIssueTypeMaster = new DataTable();

        try
        {

            dtIssueTypeMaster = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.Usp_MobileService_GetIssueTypeMaster);


            if (dtIssueTypeMaster != null && dtIssueTypeMaster.Rows.Count > 0)
            {
                for (int i = 0; i < dtIssueTypeMaster.Rows.Count; i++)
                {
                    lstTicketDetails.Add(new Ticket
                    {
                        IssueTypeId = Convert.ToInt32(dtIssueTypeMaster.Rows[i]["IssueRequestType_PK"]),
                        IssueType = dtIssueTypeMaster.Rows[i]["IssueRequestType"].ToString(),
                        ApplicationId = Convert.ToInt32(dtIssueTypeMaster.Rows[i]["ApplicationID_FK"]),


                    });
                }
            }
            else
            {
                lstTicketDetails = null;
            }

            return lstTicketDetails;
        }

        catch (Exception ex)
        {

            throw ex;
        }
    }

    public List<Ticket> GetIssueSubTypeMaster()
    {
        List<Ticket> lstTicketDetails = new List<Ticket>();

        DataTable dtIssueSubTypeMaster = new DataTable();

        try
        {

            dtIssueSubTypeMaster = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.Usp_MobileService_GetIssueSubTypeMaster);


            if (dtIssueSubTypeMaster != null && dtIssueSubTypeMaster.Rows.Count > 0)
            {
                for (int i = 0; i < dtIssueSubTypeMaster.Rows.Count; i++)
                {
                    lstTicketDetails.Add(new Ticket
                    {
                        IssueSubTypeId = Convert.ToInt32(dtIssueSubTypeMaster.Rows[i]["IssueRequestSubType_PK"]),
                        IssueSubType = dtIssueSubTypeMaster.Rows[i]["IssueRequestSubType"].ToString(),
                        ApplicationId = Convert.ToInt32(dtIssueSubTypeMaster.Rows[i]["ApplicationID_FK"]),
                        IssueTypeId = Convert.ToInt32(dtIssueSubTypeMaster.Rows[i]["IssueRequestID_FK"]),
                        TicketMessage = dtIssueSubTypeMaster.Rows[i]["TicketMessage"].ToString()

                    });
                }
            }
            else
            {
                lstTicketDetails = null;
            }

            return lstTicketDetails;
        }

        catch (Exception ex)
        {

            throw ex;
        }
    }

    public List<AppSupportCategory> GetAppSupportCategory()
    {
        List<AppSupportCategory> lstTicketDetails;
        lstTicketDetails = new List<AppSupportCategory>();

        DataTable dtAppSupportCategory = new DataTable();

        try
        {

            dtAppSupportCategory = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.Usp_MobileService_GetAppSuportCategory);


            if (dtAppSupportCategory != null && dtAppSupportCategory.Rows.Count > 0)
            {
                for (int i = 0; i < dtAppSupportCategory.Rows.Count; i++)
                {
                    lstTicketDetails.Add(new AppSupportCategory
                    {
                        Id = Convert.ToInt32(dtAppSupportCategory.Rows[i]["ID"]),
                        Category = dtAppSupportCategory.Rows[i]["CATEGORY"].ToString(),
                        TeamName = dtAppSupportCategory.Rows[i]["teamname"].ToString()

                    });
                }
            }
            else
            {
                lstTicketDetails = null;
            }

            return lstTicketDetails;
        }

        catch (Exception ex)
        {

            throw ex;
        }
    }

    //public Stream DownloadFile(string fileName)
    //{
    //    string downloadFilePath = Path.Combine(HostingEnvironment.MapPath("~/Files/Downloads"), fileName);
    //    String headerInfo = "attachment; filename=" + fileName;
    //    WebOperationContext.Current.OutgoingResponse.Headers["Content-Disposition"] = headerInfo;
    //    WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
    //    return File.OpenRead(downloadFilePath);
    //}

    #region File Bytes validation
    //public static bool IsValidImage(byte[] bytes)
    //{
    //    try
    //    {
    //        using (MemoryStream ms = new MemoryStream(bytes))
    //            Image.FromStream(ms);
    //    }
    //    catch (ArgumentException)
    //    {
    //        return false;
    //    }
    //    return true;
    //}
    #endregion

    public List<Ticket> GetCalldeskReport(CalldeskReport objCallDeskReport)
    {
        List<Ticket> lstTicketDetails = new List<Ticket>();

        AuthKey objAuthKey = new AuthKey();
        objAuthKey = CallDeskServiceRequest.DecryptAuthKey(objCallDeskReport.AuthKey);

        string strStartDate = null;
        string strEndDate = null;

        strStartDate = DateTime.ParseExact(objCallDeskReport.StartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy HH:mm");
        strEndDate = DateTime.ParseExact(objCallDeskReport.EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy HH:mm");



        DataTable dtTicketDetails = new DataTable();

        try
        {
            SqlParameter[] objParam = new SqlParameter[3];

            objParam[0] = new SqlParameter("@StartDate", SqlDbType.VarChar, 50);
            objParam[0].Value = strStartDate;
            objParam[0].Direction = ParameterDirection.Input;

            objParam[1] = new SqlParameter("@EndDate", SqlDbType.VarChar, 50);
            objParam[1].Value = strEndDate;
            objParam[1].Direction = ParameterDirection.Input;

            objParam[2] = new SqlParameter("@UserId", SqlDbType.VarChar, 20);
            objParam[2].Value = objAuthKey.UserId;
            objParam[2].Direction = ParameterDirection.Input;


            dtTicketDetails = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.Usp_MobileService_GetCallDeskReport, objParam);

            if (dtTicketDetails != null && dtTicketDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dtTicketDetails.Rows.Count; i++)
                {

                    string strTicketStatus = dtTicketDetails.Rows[i]["CallStatus"].ToString();
                    string strTicketNumber = dtTicketDetails.Rows[i]["TicketNumberPK"].ToString();
                    string strAppSupportPerformer = null;

                    if (strTicketStatus == "Open")
                    {
                        TechDeskObjectService.WS_TechDeskService objTDS = new TechDeskObjectService.WS_TechDeskService();

                        strAppSupportPerformer = objTDS.getPerformer(strTicketNumber, "AppSupport");
                    }
                    else
                    {
                        strAppSupportPerformer = dtTicketDetails.Rows[i]["AppSuportPerformer"].ToString();
                    }

                    lstTicketDetails.Add(new Ticket
                    {
                        TicketNO = dtTicketDetails.Rows[i]["TicketNumberPK"].ToString(),
                        TicketDate = dtTicketDetails.Rows[i]["CallDate"].ToString(),
                        UserRemark = dtTicketDetails.Rows[i]["UserRemark"].ToString(),
                        ApproverId = dtTicketDetails.Rows[i]["ApproverID"].ToString(),
                        ApplicationName = dtTicketDetails.Rows[i]["ApplicationName"].ToString(),
                        IssueType = dtTicketDetails.Rows[i]["IssueRequestType"].ToString(),
                        IssueSubType = dtTicketDetails.Rows[i]["IssueRequestSubType"].ToString(),
                        ApplicationId = Convert.ToInt32(dtTicketDetails.Rows[i]["ApplicationID_PK"]),
                        IssueTypeId = Convert.ToInt32(dtTicketDetails.Rows[i]["IssueRequestType_PK"]),
                        IssueSubTypeId = Convert.ToInt32(dtTicketDetails.Rows[i]["IssueRequestSubType_PK"]),
                        ContactNumber = dtTicketDetails.Rows[i]["ContactNumber"].ToString(),
                        Ticketvalue = Convert.ToDecimal(dtTicketDetails.Rows[i]["TicketValue"]),
                        UploadScreenPath = dtTicketDetails.Rows[i]["UploadedScreen"].ToString(),
                        CreatedBy = dtTicketDetails.Rows[i]["CallCreatedBy"].ToString(),
                        TicketStatus = dtTicketDetails.Rows[i]["CallStatus"].ToString(),
                        ExpectedApprovedDate = dtTicketDetails.Rows[i]["ExpectedApprovedDate"].ToString(),
                        ExpectedCloseDate = dtTicketDetails.Rows[i]["ExpectedCloseDate"].ToString(),
                        EscalationRemark = dtTicketDetails.Rows[i]["EscalationRemark"].ToString(),
                        CallStartDate = dtTicketDetails.Rows[i]["CallDate"].ToString(),
                        CallEndDate = dtTicketDetails.Rows[i]["AppSupportCloseDate"].ToString(),
                        CallLoggedBranch = dtTicketDetails.Rows[i]["CallLoggedBranch"].ToString(),
                        SecondaryApproverId = dtTicketDetails.Rows[i]["SApproverID"].ToString(),
                        ApproverRemark = dtTicketDetails.Rows[i]["ApproverRemark"].ToString(),
                        ApproverCloseDate = dtTicketDetails.Rows[i]["ApproverClosedDate"].ToString(),
                        AppSuportRemark = dtTicketDetails.Rows[i]["AppSupportRemark"].ToString(),
                        AppSuportCloseDate = dtTicketDetails.Rows[i]["AppSupportCloseDate"].ToString(),
                        BranchName = dtTicketDetails.Rows[i]["BranchName"].ToString(),
                        ApproverStatus = dtTicketDetails.Rows[i]["ApproverStatus"].ToString(),
                        AppSupportStatus = dtTicketDetails.Rows[i]["AppSupportStatus"].ToString(),
                        UserMail = dtTicketDetails.Rows[i]["UserMail"].ToString(),
                        ApporverName = dtTicketDetails.Rows[i]["ApproverName"].ToString(),
                        ApproverMail = dtTicketDetails.Rows[i]["ApproverMail"].ToString(),
                        ApproverDesignation = dtTicketDetails.Rows[i]["ApproverDesignation"].ToString(),
                        ApproverContactno = dtTicketDetails.Rows[i]["ApproverContactno"].ToString(),
                        SecondaryApproverName = dtTicketDetails.Rows[i]["SApproverName"].ToString(),
                        SecondaryApproverMail = dtTicketDetails.Rows[i]["SApproverMail"].ToString(),
                        SecondaryApproverDesignation = dtTicketDetails.Rows[i]["SApproverDesignation"].ToString(),
                        SecondaryApproverContactno = dtTicketDetails.Rows[i]["SApproverContactno"].ToString(),
                        SMChannel = dtTicketDetails.Rows[i]["SMChannel"].ToString(),
                        UserDesignation = dtTicketDetails.Rows[i]["UserDesignation"].ToString(),
                        Priority = dtTicketDetails.Rows[i]["Priority"].ToString(),
                        Groups = dtTicketDetails.Rows[i]["Groups"].ToString(),
                        ServiceCenterName = dtTicketDetails.Rows[i]["ServiceCenterName"].ToString(),
                        GroupType = dtTicketDetails.Rows[i]["GroupType"].ToString(),
                        RegionId = dtTicketDetails.Rows[i]["scRegionID"].ToString(),
                        ZoneId = dtTicketDetails.Rows[i]["scZoneID"].ToString(),
                        CallTAT = dtTicketDetails.Rows[i]["CallTAT"].ToString(),
                        ApproverTAT = dtTicketDetails.Rows[i]["ApproverTAT"].ToString(),
                        AppSupportTAT = dtTicketDetails.Rows[i]["AppSupportTAT"].ToString(),
                        AppSupportPerformer = strAppSupportPerformer,
                        AppSupportClosureCategory = dtTicketDetails.Rows[i]["AppSupportClosureCategory"].ToString(),
                        AppSupportOtherRemark = dtTicketDetails.Rows[i]["AppSupportOtherRemark"].ToString(),
                        AppSupportPhoneRemark = dtTicketDetails.Rows[i]["AppSupportPhoneRemark"].ToString(),
                        FileName = dtTicketDetails.Rows[i]["FileName"].ToString(),
                        //FileBytes = GetFileBytesWeb(dtTicketDetails.Rows[i]["UploadedScreen"].ToString()),
                        ErrorStatus = false

                    });
                }
            }
            else
            {
                lstTicketDetails = null;
            }

            return lstTicketDetails;
        }

        catch (Exception ex)
        {

            throw ex;
        }
    }

}

