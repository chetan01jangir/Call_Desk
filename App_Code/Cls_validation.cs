using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using System.Data.SqlClient;
using CalldeskServices;
using System.Configuration;


public class Cls_validation
{
    #region Connection String
    private string CallDeskConnectionString = ConfigurationManager.ConnectionStrings["CallDeskDB123"].ToString();

    #endregion

    public DataTable TicketCreation_CheckMandatoryFields(Ticket objTicket)
    {
        try
        {

            DataTable dtvalidation = new DataTable();
            dtvalidation.Columns.Add("ValidationText");

            if (string.IsNullOrEmpty(Convert.ToString(objTicket.ApplicationId)))
            {
                DataRow drValidation = dtvalidation.NewRow();
                drValidation["ValidationText"] = "ApplicationId is Mandatory";
                dtvalidation.Rows.Add(drValidation);
                drValidation = null;
            }

            if (string.IsNullOrEmpty(Convert.ToString(objTicket.IssueTypeId)))
            {
                DataRow drValidation = dtvalidation.NewRow();
                drValidation["ValidationText"] = "IssueTypeId is Mandatory";
                dtvalidation.Rows.Add(drValidation);
                drValidation = null;
            }

            if (string.IsNullOrEmpty(Convert.ToString(objTicket.IssueSubTypeId)))
            {
                DataRow drValidation = dtvalidation.NewRow();
                drValidation["ValidationText"] = "Issue SubTypeId is Mandatory";
                dtvalidation.Rows.Add(drValidation);
                drValidation = null;
            }

            if (string.IsNullOrEmpty(Convert.ToString(objTicket.CreatedBy)))
            {
                DataRow drValidation = dtvalidation.NewRow();
                drValidation["ValidationText"] = "Created By is Mandatory";
                dtvalidation.Rows.Add(drValidation);
                drValidation = null;
            }

            if (string.IsNullOrEmpty(Convert.ToString(objTicket.ContactNumber)))
            {
                DataRow drValidation = dtvalidation.NewRow();
                drValidation["ValidationText"] = "Contact No is Mandatory";
                dtvalidation.Rows.Add(drValidation);
                drValidation = null;
            }

            if (dtvalidation.Rows.Count == 0)
            {
                DataRow drValidation = dtvalidation.NewRow();
                drValidation["ValidationText"] = "Data Validated";
                dtvalidation.Rows.Add(drValidation);
                drValidation = null;
            }


            return dtvalidation;
        }
        catch (Exception)
        {

            return null;
        }
    }

    public string GenerateTicketNo()
    {
        string ticketNumber = string.Empty;

        try
        {

            ticketNumber = SqlHelper.ExecuteScalar(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.usp_MobileService_GenerateTicketNumber).ToString();

            return ticketNumber;
        }
        catch (Exception)
        {

            return null;
        }
    }

    public DataSet GetTicketDetailsByTicketNo(string strTicketNo)
    {
        DataSet dsTicketDetails = new DataSet();

        try
        {
            SqlParameter[] objParam = new SqlParameter[1];

            objParam[0] = new SqlParameter("@TicketNumberPK", SqlDbType.VarChar, 20);
            objParam[0].Value = strTicketNo;
            objParam[0].Direction = ParameterDirection.Input;


            dsTicketDetails = SqlHelper.ExecuteDataset(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.usp_MobileService_GetCallDetailsByTicketNumber, objParam);

            return dsTicketDetails;
        }
        catch (Exception)
        {

            return null;
        }
    }

    public bool SavvionService(string strTicketNo)
    {
        try
        {
            DataSet dsTicketdetails = new DataSet();
            dsTicketdetails = GetTicketDetailsByTicketNo(strTicketNo);

            string sUserRemark, sApproverEmail, sApproverDesignation, sApproverName, sUserMail, sContactNumber
                , sApplication, sApproverID, sReopenID, sBranchName, sCallDate, sUploadPath, sUserName, sCallType
                , sTypeofIR, sTicketNumber, sUserDesignation, sGroups, sIRSubType, sCallTAT, sSApproverID
                , sAppSupportPerformer, sSApproverDesignation, sSApproverEmail, sSApproverName
                , sTicketValue, sApproverTAT, sAppSupportTAT, sPriority;


            sUserRemark = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["UserRemark"]);
            sApproverEmail = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["ApproverMail"]);
            sApproverDesignation = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["ApproverDesignation"]);
            sApproverName = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["ApproverName"]);
            sUserMail = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["UserMail"]);
            sContactNumber = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["ContactNumber"]);
            sApplication = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["ApplicationName"]);
            sApproverID = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["ApproverID"]);
            sReopenID = null;

            sBranchName = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["BranchName"]);
            if (sBranchName == "Agent Branch")
            {
                sBranchName = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["SMBranchName"]);
            }

            DateTime dtCallDate = Convert.ToDateTime(dsTicketdetails.Tables[0].Rows[0]["CallDate"]);
            sCallDate = dtCallDate.ToString("yyyy-MM-dd hh:mm:ss");

            sUploadPath = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["UploadedScreen"]);
            sCallType = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["CallType"]);
            string strCallCreatedBy = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["CallCreatedBy"]);

            sUserName = strCallCreatedBy;
            //sUserName = Convert.ToString(Session["EmployeeName"].ToString().ToUpper()) + " (" + strCallCreatedBy + ")";
            //if (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["BranchName"]) == "Agent Branch")
            //{
            //    //string strAgentCode = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AgentCode"]);
            //    sUserName = Convert.ToString(Session["EmployeeName"].ToString().ToUpper()) + " (" + strCallCreatedBy + ")";

            //}

            sTypeofIR = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["IssueRequestType"]);
            sTicketNumber = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["TicketNumberPK"]);
            sUserDesignation = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["UserDesignation"]);


            sGroups = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["Groups"]);
            string sServiceCenterName = dsTicketdetails.Tables[0].Rows[0]["ServiceCenterName"] == null ? "" : Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["ServiceCenterName"]);
            string scRegionID = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["scRegionID"] == null ? "0" : Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["scRegionID"]));
            string scZoneID = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["scZoneID"] == null ? "0" : Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["scZoneID"]));


            if (sGroups == "SCU" && sServiceCenterName != string.Empty)
            {
                //sGroups = sGroups + "$" + sServiceCenterName;
                sGroups = sServiceCenterName;
            }

            //if (sGroups == "SCUD" && sServiceCenterName != string.Empty)
            //{
            //    sServiceCenterName = sServiceCenterName.Replace("SC", "SCD");
            //    sGroups = sServiceCenterName;
            //}

            if (sGroups == "SCUD")
            {
                sGroups = sGroups + scRegionID;
            }
            if (sGroups == "ROC")
            {
                sGroups = sGroups + scRegionID;
            }
            if (sGroups == "ZAM")
            {
                sGroups = sGroups + scRegionID;
            }
            if (sGroups == "HUB")
            {
                sGroups = sGroups + scZoneID;
            }

            if (sGroups == "OPS_POLICY")
            {
                if (sServiceCenterName == "SC-East")
                {
                    sGroups = "OPS_POLICY_East";
                }
                else if (sServiceCenterName == "SC-West")
                {
                    sGroups = "OPS_POLICY_West";
                }
                else if (sServiceCenterName == "SC-North")
                {
                    sGroups = "OPS_POLICY_North";
                }
                else
                {
                    sGroups = "OPS_POLICY_South";
                }
            }

            if (sGroups == "IT_infra")
            {
                sGroups = sGroups + scRegionID;
            }
            if (sGroups == "COP")
            {
                if (sServiceCenterName == "SC-East")
                {
                    sGroups = "COPE";
                }
                else if (sServiceCenterName == "SC-West")
                {
                    sGroups = "COPW";
                }
                else if (sServiceCenterName == "SC-North")
                {
                    sGroups = "COPN";
                }
                else
                {
                    sGroups = "COPS";
                }
            }
            if (sGroups == "GMC")
            {
                if (sServiceCenterName == "SC-East")
                {
                    sGroups = "GMC_East";
                }
                else if (sServiceCenterName == "SC-West")
                {
                    sGroups = "GMC_West";
                }
                else if (sServiceCenterName == "SC-North")
                {
                    sGroups = "GMC_North";
                }
                else if (sServiceCenterName == "SC-South")
                {
                    sGroups = "GMC_South";
                }
                else if (sServiceCenterName == "Corporate Zone")
                {
                    sGroups = "GMC_Corporate";
                }
            }

            sIRSubType = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["IssueRequestSubType"]);

            int intTAT = Convert.ToInt32(dsTicketdetails.Tables[0].Rows[0]["CallTAT"]);
            sCallTAT = Convert.ToString(intTAT * 60 * 60);

            sSApproverID = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["SApproverID"]);
            sAppSupportPerformer = null;
            sSApproverName = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["SApproverName"]);
            sSApproverEmail = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["SApproverMail"]);
            sSApproverDesignation = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["SApproverDesignation"]);

            sTicketValue = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["TicketValue"]);
            sApproverTAT = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["ApproverTAT"]);
            sAppSupportTAT = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["AppSupportTAT"]);
            sPriority = Convert.ToString(dsTicketdetails.Tables[0].Rows[0]["Priority"]);

            //TechDesk.TechDeskService objTechDesk = new TechDesk.TechDeskService();
            //string strResponseVal = objTechDesk.STARTCALLDESK(sUserRemark, sApproverEmail, sApproverDesignation, sApproverName, sUserMail, sContactNumber, sApplication, sApproverID, sReopenID, sBranchName, sCallDate, sUploadPath, sUserName, sCallType, sTypeofIR, sTicketNumber, sUserDesignation, sGroups, sIRSubType, sCallTAT, sSApproverID, sAppSupportPerformer, sSApproverDesignation, sSApproverEmail, sSApproverName, sTicketValue, sApproverTAT, sAppSupportTAT, "rgicl", "rgicl", "", sPriority);

            
            TechDeskObjectService.RequestObject objRequestService = new TechDeskObjectService.RequestObject();

            TechDeskObjectService.TechDeskObject objTechDeskService = new TechDeskObjectService.TechDeskObject();

            objTechDeskService.channelSM = sUserDesignation;
            objTechDeskService.callType = sCallType;
            objTechDeskService.branch = sBranchName;
            objTechDeskService.appSupportTAT = sAppSupportTAT;
            objTechDeskService.reopenId = sReopenID;
            objTechDeskService.ticketNo = sTicketNumber;
            objTechDeskService.callCreatedBy = sUserName;
            objTechDeskService.approverUserID2 = sSApproverID;
            objTechDeskService.userContactNo = sContactNumber;
            objTechDeskService.approverID = sApproverID;
            objTechDeskService.ticketValue = sTicketValue;
            objTechDeskService.issueRequestId = sTypeofIR;
            objTechDeskService.approverName = sApproverName;
            objTechDeskService.approver2Name = sSApproverName;
            objTechDeskService.callLogDate = sCallDate;
            objTechDeskService.issueReqSubTypeID = sIRSubType;
            objTechDeskService.approverTAT = sApproverTAT;
            objTechDeskService.teamTAT = sCallTAT;
            objTechDeskService.priority = sPriority;
            objTechDeskService.userEmailID = sUserMail;
            objTechDeskService.username = "rgicl";
            objTechDeskService.approver2Email = sSApproverEmail;
            objTechDeskService.approverDesignation = sApproverDesignation;
            objTechDeskService.uploadScreen = sUploadPath;
            objTechDeskService.approverEmailID = sApproverEmail;
            objTechDeskService.password = "rgicl";
            objTechDeskService.applicationType = sApplication;
            objTechDeskService.approver2Dsgn = sSApproverDesignation;
            objTechDeskService.piName = "";
            objTechDeskService.appSupportPerformer = sAppSupportPerformer;
            objTechDeskService.teamName = sGroups;
            objTechDeskService.userRemark = sUserRemark;

            objRequestService.techdesk = objTechDeskService;


            //objTechDeskService.applica

            TechDeskObjectService.WS_TechDeskService objReq = new TechDeskObjectService.WS_TechDeskService();

            TechDeskObjectService.ResponseObject objResponse = new TechDeskObjectService.ResponseObject();


            objResponse = objReq.CreateTicket(objRequestService);

            string strResponseVal = objResponse.message;
            string strResponseCode = objResponse.responseCode;

            //update Savvion Response start
            bool IsSuccess = UpdateSavvionResponse(strTicketNo, strResponseVal, strResponseCode);
            //update Savvion Response end

            if (strResponseCode == "5000" && IsSuccess == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    public bool UpdateSavvionResponse(string strTicketNo, string strResponseVal, string strResponseCode)
    {

        try
        {
            SqlParameter[] objParam = new SqlParameter[3];

            objParam[0] = new SqlParameter("@TicketNumberPK", SqlDbType.VarChar, 20);
            objParam[0].Value = strTicketNo;
            objParam[0].Direction = ParameterDirection.Input;


            objParam[1] = new SqlParameter("@SavvionStatus", SqlDbType.VarChar, 50);
            objParam[1].Value = strResponseVal;
            objParam[1].Direction = ParameterDirection.Input;

            objParam[2] = new SqlParameter("@ResponseCode", SqlDbType.VarChar, 50);
            objParam[2].Value = strResponseCode;
            objParam[2].Direction = ParameterDirection.Input;

            int i = SqlHelper.ExecuteNonQuery(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.usp_MobileSerive_UpdateSavvionResponse, objParam);

            return true;
        }
        catch (Exception)
        {

            throw;
        }


    }

    #region Send SMS To USer
    //public void SendSMSToUser(string strMobileNo, string smsMsg)
    //{
    //    try
    //    {
    //        SMSSendService.SingleMessage msgData = new SMSSendService.SingleMessage();
    //        SMSSendService.SendMessage sendMsg = new SMSSendService.SendMessage();
    //        msgData.Department = "IT";
    //        msgData.UserName = "intranet";
    //        msgData.Password = "rgiclintra07#";
    //        msgData.Message = smsMsg;
    //        msgData.MobileNumber = strMobileNo;
    //        sendMsg.Send(msgData);
    //    }
    //    catch
    //    {

    //    }
    //}
    #endregion

    public bool CallDeskAuthentication(string UserId)
    {
        try
        {
            DataTable dt = new DataTable();

            SqlParameter[] objParam = new SqlParameter[1];

            objParam[0] = new SqlParameter("@UserId", SqlDbType.VarChar, 20);
            objParam[0].Value = UserId;
            objParam[0].Direction = ParameterDirection.Input;

            dt = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.usp_MobileServiceGetAuthenticateUser, objParam);

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {

            throw;
        }


    }

    public bool SavvionAuthentication(string UserId)
    {
        try
        {
            TechDeskObjectService.WS_TechDeskService objReq = new TechDeskObjectService.WS_TechDeskService();
            bool IsSavvionUser = objReq.checkUsersExists(UserId);

            return IsSavvionUser;

        }
        catch (Exception ex)
        {

            return false;
        }
    }

    public DataTable TicketUpdation_CheckMandatoryFields(Ticket objTicket)
    {
        DataTable dtvalidation = new DataTable();
        dtvalidation.Columns.Add("ValidationText");

        if (string.IsNullOrEmpty(Convert.ToString(objTicket.TicketNO)))
        {
            DataRow drValidation = dtvalidation.NewRow();
            drValidation["ValidationText"] = "Ticket No is Mandatory";
            dtvalidation.Rows.Add(drValidation);
            drValidation = null;
        }

        if (string.IsNullOrEmpty(Convert.ToString(objTicket.UserRemark)))
        {
            DataRow drValidation = dtvalidation.NewRow();
            drValidation["ValidationText"] = "User Remark is Mandatory";
            dtvalidation.Rows.Add(drValidation);
            drValidation = null;
        }

        if (string.IsNullOrEmpty(Convert.ToString(objTicket.TicketStatus)))
        {
            DataRow drValidation = dtvalidation.NewRow();
            drValidation["ValidationText"] = "Ticket Status is Mandatory";
            dtvalidation.Rows.Add(drValidation);
            drValidation = null;
        }

        if (dtvalidation.Rows.Count == 0)
        {
            DataRow drValidation = dtvalidation.NewRow();
            drValidation["ValidationText"] = "Data Validated";
            dtvalidation.Rows.Add(drValidation);
            drValidation = null;
        }

        return dtvalidation;
    }

}
