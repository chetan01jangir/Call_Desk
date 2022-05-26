using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CallDeskBAL;
using System.IO;
using TechDeskService;

public partial class User_CallDetails : System.Web.UI.Page
{
    #region Page Load Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindCallDetailsGridByTicketNumber();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        DisableButtons();
    }

    #endregion

    #region Bind Call Details Grid By Ticket Number

    public void BindCallDetailsGridByTicketNumber()
    {
        try
        {
            string strTicketNumber = Convert.ToString(Session["TicketNumber"]);
            string strLoggedBranch = Convert.ToString(Session["LoggedBranch"]);

           // string strUserName = Membership.GetUser().UserName;
            string strUserName = Convert.ToString(Session["AgentUserID"]);

            TrackCallBAL objTCBAL = new TrackCallBAL();
            DataSet dsCallDetails = new DataSet();
            DataSet dsApproverFiles = new DataSet();
            DataTable dtUserFiles = new DataTable();
            dsCallDetails = objTCBAL.GetCallDetailsByTicketNumberForAdmin(strTicketNumber);

            Session["CallDetailsData"] = dsCallDetails;

            lblTicketNumber.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["TicketNumberPK"]);
            lblApplicationType.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApplicationName"]);
            lblIssueRequsetType.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["IssueRequestType"]);
            lblIssueRequsetSubType.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["IssueRequestSubType"]);
            lblCallDate.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallDate"]).ToString();
            lblCallStatus.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]);
            lblTicketValue.Text = dsCallDetails.Tables[0].Rows[0]["TicketValue"].ToString() == "" ? "0.00" : Convert.ToString(dsCallDetails.Tables[0].Rows[0]["TicketValue"]);
            lblCallLoggedUser.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallCreatedBy"]);

            if (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["BranchName"]) == "Agent Branch")
            {
                lblCallLoggedLocation.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SMBranchName"]);
            }
            else
            {             
                lblCallLoggedLocation.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["BranchName"]);
            }

            lblUserRemark.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UserRemark"]);
            lblApproverStatus.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverStatus"]);

            lblApproverName.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverName"]);
            lblApproverEMail.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverMail"]);
            lblApproverDesignation.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverDesignation"]);

            lblSecondApproverName.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverName"]);
            lblSecondApproverDesignation.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverDesignation"]);
            lblSecondApproverEMail.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverMail"]);

            lblApproverRemark.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverRemark"]);
            lblApproverClosedDate.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverClosedDate"]);

            lblAppSupportStatus.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSupportStatus"]);

            if (lblAppSupportStatus.Text == "In Progress")
            {
                tdCloseDate.InnerText = "Forwarded Date";
            }

            //lblRemarkforUser.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSupportRemark"]);

            lblAppSupportCloseDate.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSupportCloseDate"]);
            lblAppSupportRemark.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSupportRemark"]);
            lblApproverexpectedCloseDate.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ExpectedApprovedDate"]);
            lblAppSupportExpectedCloseDate.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ExpectedCloseDate"]);

            lblTicketProGroup.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Groups"]);


            //Get User files Code Starts Here

            string strFirstUpload;
            strFirstUpload = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UploadedScreen"]);

            if (strFirstUpload != "")
            {
                DataRow dr = dsCallDetails.Tables[1].NewRow();
                dr["UploadedFile"] = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UploadedScreen"]); ;
                dsCallDetails.Tables[1].Rows.Add(dr);
                dsCallDetails.AcceptChanges();
            }

            dtUserFiles = dsCallDetails.Tables[1];
            if (dtUserFiles.Rows.Count > 0)
            {
                gvUploadedFiles.DataSource = dtUserFiles;
                gvUploadedFiles.DataBind();
            }

            //Get User files Code Starts Here

            //Get Approver files Code Starts Here

            dsApproverFiles = objTCBAL.GetFilesByApprover(lblTicketNumber.Text);
            if (dsApproverFiles.Tables[0].Rows.Count > 0)
            {
                gvFilesUploadedbyApprover.DataSource = dsApproverFiles;
                gvFilesUploadedbyApprover.DataBind();
            }

            //Get Approver files Code Ends Here

            string strCallType = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallType"]);
            string strPending = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]);
            string strRowID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["FKRowID"]);

            //Get AppSupport files Code Starts Here
            if (strRowID != "")
            {
                DataSet dsAppSupportFile = new DataSet();
                dsAppSupportFile = objTCBAL.GetAppSupportFileByRowID(strRowID);
                gvFilesUploadedforUser.DataSource = dsAppSupportFile;
                gvFilesUploadedforUser.DataBind();
            }
            //Get AppSupport files Code Ends Here

            int ReopenTime = Convert.ToInt32(dsCallDetails.Tables[0].Rows[0]["DD"]);

            if (strCallType == "Request")
            {
                trRequest.Visible = true;
                TrackCallBAL objBAL = new TrackCallBAL();
                DataSet dsApprover = objBAL.GetReopenCallApprover(strTicketNumber);
                if (dsApprover.Tables.Count > 0)
                {
                    lblApproverName.Text = Convert.ToString(dsApprover.Tables[0].Rows[0]["ApproverName"]);
                    lblApproverEMail.Text = Convert.ToString(dsApprover.Tables[0].Rows[0]["ApproverMail"]);
                    lblApproverDesignation.Text = Convert.ToString(dsApprover.Tables[0].Rows[0]["ApproverDesignation"]);

                    lblSecondApproverName.Text = Convert.ToString(dsApprover.Tables[0].Rows[0]["SApproverName"]);
                    lblSecondApproverDesignation.Text = Convert.ToString(dsApprover.Tables[0].Rows[0]["SApproverDesignation"]);
                    lblSecondApproverEMail.Text = Convert.ToString(dsApprover.Tables[0].Rows[0]["SApproverMail"]);
                }
            }
            else
            {
                trRequest.Visible = false;
            }

            if (ReopenTime <= 72)
            {
                if ((lblApproverStatus.Text.Equals("Approved") && lblAppSupportStatus.Text.Equals("Resolved")) || (lblApproverStatus.Text.Equals("") && lblAppSupportStatus.Text.Equals("Resolved")))
                {
                    trReopen.Visible = true;
                    btnReopen.Visible = true;
                }

                else
                {
                    trReopen.Visible = false;
                    btnReopen.Visible = false;
                }
            }
            else
            {
                trReopen.Visible = false;
                btnReopen.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Submit button code

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        /* Pending Code
        TrackCallBAL objTCBAL = new TrackCallBAL();
        int intCheckException = 0;

        try
        {
            DataSet dsCallDetails = (DataSet)Session["CallDetailsData"];
            DataSet dsPendingDetails = (DataSet)Session["PendingDetailsData"];

            string strTicketNumber = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["TicketNumberPK"]);
            string strCallType = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallType"]);
            string strCallStatus = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]);
            string strRowID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["FKRowID"]);
            string strOldRemark = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UserRemark"]);
            string strNewRemark = txtRemark.Text;
            string strApproverStatus = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverStatus"]);
            string strAppSupportStatus = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSupportStatus"]);

            ViewState["strCallStatus"] = strCallStatus;
            ViewState["strRowID"] = strRowID;
            ViewState["strOldRemark"] = strOldRemark;

            string strPIName = dsPendingDetails.Tables[0].Rows[0]["PIName"].ToString();
            string strWIName = dsPendingDetails.Tables[0].Rows[0]["WIName"].ToString();

            string strTotalRemark = strOldRemark + "$" + strNewRemark;

            int intRemarkLength = strTotalRemark.Length;

            string fileName, savePath, strUploadPath = null;
            string UploadSavePath = ConfigurationManager.AppSettings["UploadSavePath"].ToString();

            if (intRemarkLength <= 2000)
            {
                if (fu.HasFile == true && fu.PostedFile != null)
                {
                    if (fu.FileContent.Length < 5632000)
                    {
                        DateTime dtn = System.DateTime.Now;
                        string strN = dtn.Hour.ToString() + dtn.Minute.ToString() + dtn.Second.ToString();

                        fileName = System.IO.Path.GetFileName(fu.PostedFile.FileName);
                        savePath = Server.MapPath("..\\Files") + "\\" + strN + fileName;
                        fu.PostedFile.SaveAs(savePath);
                        strUploadPath = UploadSavePath + strN + "_" + fileName;
                    }
                    else
                    {
                        lblMessage.Text = "File size should be less than 5 MB.";
                        return;
                    }
                }


                objTCBAL.AddNewUserRemarks(strRowID, strNewRemark, strTicketNumber, strCallStatus, strUploadPath);

                intCheckException = 1;

                CallDeskService objCDS = new CallDeskService();
                string strSession = objCDS.connect("rgicl", "rgicl");

                if (strCallType == "Request")
                {
                    if (strApproverStatus == "Approved")
                    {
                        //objCDS.APPSUPPORTWAITING(strTotalRemark, strUploadPath, strSession, strPIName, strWIName);
                    }
                    else
                    {
                        //objCDS.APPROVERWAITING(strTotalRemark, strUploadPath, strSession, strPIName, strWIName);
                    }
                }
                else if (strCallType == "Issue")
                {
                    //objCDS.APPSUPPORTWAITING(strTotalRemark, strUploadPath, strSession, strPIName, strWIName);
                }

                PopupCloseFunction();
            }
            else
            {
                lblMessage.Text = "User remark exceeded 2000 characters in User Remark.";
            }
        }
        catch (Exception ex)
        {
            if (intCheckException == 1)
            {
                string strTicketNumber = Convert.ToString(Session["TicketNumber"]);
                string strRowID =  Convert.ToString(ViewState["strRowID"]);                
                string strOldRemark = Convert.ToString(ViewState["strOldRemark"]);
                string strCallStatus = Convert.ToString(ViewState["strCallStatus"]);
                string strRemarks = DBNull.Value.ToString();
                int intReturnValue = objTCBAL.UpdateCallStatusFail(strRowID, strRemarks, strTicketNumber, strCallStatus, strOldRemark);
                lblMessage.Text = "Error occurred while saving data to the Server, please try after some time.";
            }
            FileStream fs1 = new FileStream(Server.MapPath("..\\Files") + "\\" + "CallDetailsFail.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw1 = new StreamWriter(fs1);
            sw1.Write("\r\n =======================================================================================");
            sw1.Write("\r\n Log Entry On : " + System.DateTime.Now);
            sw1.Write("\n " + ex.Message);
            sw1.Close();
            fs1.Close();
        }
         */
    }

    #endregion

    #region Button OK Code

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            PopupCloseFunction();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Close Function Code

    public void PopupCloseFunction()
    {
        StringBuilder sbCloseScript = new StringBuilder();
        sbCloseScript.Append("<script language='javascript'>");
        sbCloseScript.Append("self.close();");
        sbCloseScript.Append("</script>");

        Page.RegisterClientScriptBlock("PopUpClose", sbCloseScript.ToString());
    }

    #endregion

    #region Reopen Button Code

    protected void btnReopen_Click(object sender, EventArgs e)
    {
        RegisterNewCallBAL objRNCBAL = new RegisterNewCallBAL();
        TrackCallBAL objTCBAL = new TrackCallBAL();
        int intCheckException = 0;
        bool isReopen = false;
        try
        {
           
            //s-TechDeskService objCDS = new TechDeskService();
            TechDeskService.TechDeskService objCDS = new TechDeskService.TechDeskService();
            string sUserRemark, sApproverEmail, sApproverDesignation, sApproverName, sUserMail,
                    sContactNumber, sApplication, sApproverID, sSApproverID, sSApproverEmail, sSApproverDesignation, sSApproverName, sReopenID, sBranchName, sCallDate,
                    sUploadPath = null, sUserName, sCallType, sTypeofIR, sTicketNumber, sChannel, sIRSubType,scRegionID,scZoneID,
                    sGroups, sPriority, sCallTAT, sOldRemark, sAppSupportPerformer,sTicketValue,sServiceCenterName,
                    strDefaultApprover = null, strDefaultApproverName = null,
                    strDefaultApproverEmail = null, strDefaultApproverDesignation = null, sUserDesignation, sApproverTAT, sAppSupportTAT;
            int intTAT = 0;
            string UploadSavePath = ConfigurationManager.AppSettings["UploadSavePath"].ToString();

            DataSet dsCallDetails = (DataSet)Session["CallDetailsData"];

            sTicketNumber = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["TicketNumberPK"]);
            sApplication = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApplicationName"]);
            sTypeofIR = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["IssueRequestType"]);
            sIRSubType = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["IssueRequestSubType"]);
            sCallDate = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallDate"]).ToString();
            sUserName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallCreatedBy"]);
            sUserName = Convert.ToString(Session["EmployeeName"]) + " (" + sUserName + ")";
            sBranchName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["BranchName"]);

            sApproverID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverID"]);
            sApproverName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverName"]);
            sApproverEmail = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverMail"]);
            sApproverDesignation = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverDesignation"]);

            sSApproverID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverID"]);
            sSApproverName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverName"]);
            sSApproverEmail = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverMail"]);
            sSApproverDesignation = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverDesignation"]);

            sCallType = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallType"]);
            sUserMail = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UserMail"]);
            sContactNumber = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ContactNumber"]);
            sTicketValue = dsCallDetails.Tables[0].Rows[0]["TicketValue"].ToString() == "" ? "0.00" : Convert.ToString(dsCallDetails.Tables[0].Rows[0]["TicketValue"]);

            sOldRemark = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UserRemark"]);
            sChannel = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SMChannel"]);
            sUserDesignation = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UserDesignation"]);
            sUserRemark = txtReopenRemark.Text;
            sGroups = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Groups"]);
            sServiceCenterName = dsCallDetails.Tables[0].Rows[0]["ServiceCenterName"] == null ? "" : Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ServiceCenterName"]);
			scRegionID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["scRegionID"] == null ? "0" : Convert.ToString(dsCallDetails.Tables[0].Rows[0]["scRegionID"]));
			scZoneID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["scZoneID"] == null ? "0" : Convert.ToString(dsCallDetails.Tables[0].Rows[0]["scZoneID"]));
			sApproverTAT = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverTAT"]);
			sAppSupportTAT = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSupportTAT"]);
            if (sGroups == "SCU" && sServiceCenterName != string.Empty)
            {
                //sGroups = sGroups + "$" + sServiceCenterName;
                sGroups = sServiceCenterName;
            }
			if (sGroups == "SCD" && sServiceCenterName != string.Empty)
			{
				//sGroups = sGroups + "$" + sServiceCenterName;
				sGroups = sGroups + "-" + sServiceCenterName;
			}
//            if (sGroups == "SCUD" && sServiceCenterName != string.Empty)
//            {
//                sServiceCenterName = sServiceCenterName.Replace("SC", "SCD");
//                sGroups = sServiceCenterName;
//            }
			if (sGroups == "SCUD")
			{
				sGroups = sGroups + scRegionID;
			}
			if (sGroups == "LegalClaim")
			{
				sGroups = sGroups + scRegionID;
			}
			if (sGroups == "HUB")
			{
				sGroups = sGroups + scZoneID;
			}
			if (sGroups == "ZAM")
			{
				sGroups = sGroups + scRegionID;
			}
			if (sGroups == "ROC")
			{
				sGroups = sGroups + scRegionID;
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
            sPriority = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Priority"]);
            intTAT = Convert.ToInt32(dsCallDetails.Tables[0].Rows[0]["CallTAT"]);
            sAppSupportPerformer = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSuportPerformer"]);

            // If Approver is blocked Code Ends

            // Set this as High as reqopened calls priority should be high
            sPriority = "High";
            string strN, strReopenTicketNo;
            strReopenTicketNo = strN = objRNCBAL.GetTicketNumber();

            sCallTAT = Convert.ToString(intTAT * 60 * 60);

            if (fuUpLoadFile.HasFile == true && fuUpLoadFile.PostedFile != null)
            {
                int fileSize = fuUpLoadFile.PostedFile.ContentLength / 1024;

                if (fileSize > 5120) // 5 MB
                {
                    lblMessage.Text = "File size should be less than 5 MB";
                    return;
                }
                else
                {
                    string fileName;
                    string savePath;

                    fileName = System.IO.Path.GetFileName(fuUpLoadFile.PostedFile.FileName);
                    savePath = Server.MapPath("..\\Files") + "\\" + strN + "_" + fileName;
                    fuUpLoadFile.PostedFile.SaveAs(savePath);
                    sUploadPath = UploadSavePath + strN + "_" + fileName;
                }
            }

            if (sCallType == "Request")
            {
                TrackCallBAL objBAL = new TrackCallBAL();
                DataSet dsApproverID = new DataSet();
                dsApproverID = objBAL.GetApproverForReopenTicket(sTicketNumber,0,0,null);
                sApproverID = Convert.ToString(dsApproverID.Tables[0].Rows[0]["FApproverID"]);
                sSApproverID = Convert.ToString(dsApproverID.Tables[0].Rows[0]["SApproverID"]);

                //Check if Approver is blocked Code Starts

                EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
                DataTable dtLED = new DataTable();
                dtLED = objEMBAL.GetLoggedEmployeeDetails(sApproverID);

                if (dtLED.Rows.Count == 0)
                {
                    sApproverID = strDefaultApprover = CommonUtility.GetValueByKey("DefaultApproverID");
                    sApproverName = strDefaultApproverName = CommonUtility.GetValueByKey("DefaultApproverName");
                    sApproverEmail = strDefaultApproverEmail = CommonUtility.GetValueByKey("DefaultApproverEmail");
                    sApproverDesignation = strDefaultApproverDesignation = CommonUtility.GetValueByKey("DefaultApproverDesignation");
                    isReopen = true;
                }

                //Check if Approver is blocked Code Ends
            }
            sReopenID = objTCBAL.UpdateCallStatusforReopen(sTicketNumber, sApproverID, sSApproverID, sUserRemark, sUploadPath, strReopenTicketNo,0);
            ViewState["sReopenID"] = sReopenID;

            intCheckException = 1;

            sUserRemark = sUserRemark + " $ " + sOldRemark;

            string strReturnValue = objCDS.STARTCALLDESK(sUserRemark, sApproverEmail, sApproverDesignation, sApproverName, sUserMail, sContactNumber, sApplication, sApproverID, sReopenID, sBranchName, sCallDate, sUploadPath, sUserName, sCallType, sTypeofIR, sTicketNumber, sUserDesignation, sGroups, sIRSubType, sCallTAT, sSApproverID, sAppSupportPerformer, sSApproverDesignation, sSApproverEmail, sSApproverName, sTicketValue, "rgicl", "rgicl", "", sPriority,""
                //[CR-1]  CQR CR Add Fields 06052019 Start
               , "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""
                //[CR-1]  CQR CR Add Fields 06052019 EN=nd
                );
            objTCBAL.UpdateSavvionReopenResponse(sReopenID, strReturnValue);

            if (isReopen == true)
            {
                string strFirstApproverID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverID"]);
                string strFirstApproverName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverName"]);
                Response.Write("<script language=javascript>alert('As First Approver " + strFirstApproverName + " (" + strFirstApproverID + " ) is blocked by HR,, the call is assinged to Admin " + strDefaultApproverName + " (" + sApproverID + ")" + "');</script>");
            }
            PopupCloseFunction();
        }
        catch (Exception ex)
        {
            if (intCheckException == 1)
            {
                string strTicketNumber = Convert.ToString(Session["TicketNumber"]);
                string sReopenID = Convert.ToString(ViewState["sReopenID"]).ToString();
                objTCBAL.UpdateCallStatusReOpenFail(strTicketNumber, sReopenID);
            }
            FileStream fs1 = new FileStream(Server.MapPath("..\\Files") + "\\" + "ReOpenCallFail.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw1 = new StreamWriter(fs1);
            sw1.Write("\r\n =======================================================================================");
            sw1.Write("\r\n Log Entry On : " + System.DateTime.Now);
            sw1.Write("\n " + ex.Message);
            sw1.Close();
            fs1.Close();
        }
    }

    #endregion

    #region Download File Code

    public void DownloadFile(string strFileName)
    {
        try
        {
            FileInfo file;
            string filename = Server.MapPath("..\\Files\\") + strFileName;
            file = new FileInfo(filename);
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(filename);
            Response.End();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Row Command Code User files Grid

    protected void gvUploadedFiles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "btnUploadedFile")
            {
                string strUploadSavePath = ConfigurationManager.AppSettings["UploadSavePath"].ToString();
                string strFileName = e.CommandArgument.ToString();
                strFileName = strFileName.Substring(strUploadSavePath.Length);
                int intVal = strFileName.IndexOf("_");
                DownloadFile(strFileName);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Row Data Bound Code for User files Grid

    protected void gvUploadedFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkFileName = (LinkButton)e.Row.FindControl("lnkFileName");
                string strFileName = lnkFileName.Text;
                int intIndex = strFileName.IndexOf("_");
                strFileName = strFileName.Substring(intIndex + 1);
                lnkFileName.Text = strFileName;
            }
        }
        catch (Exception ex)
        {

            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Files Uploaded for User Grid Row Command Event

    protected void gvFilesUploadedforUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string strFileName = Convert.ToString(e.CommandArgument);
            if (e.CommandName == "btnUploadedFile")
            {
                DownloadFile(strFileName);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Approver Grid Row Command

    protected void gvFilesUploadedbyApprover_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string strFileName = Convert.ToString(e.CommandArgument);
            if (e.CommandName == "btnUploadedFile")
            {
                DownloadFile(strFileName);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Disable the Buttons for Double Click

    public void DisableButtons()
    {
        try
        {
            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
            sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ");
            sbValid.Append("if (Page_ClientValidate() == false) { return false; }} ");
            sbValid.Append("document.getElementById('" + btnReopen.ClientID.ToString().Replace('$', '_') + "').disabled = true;");
            sbValid.Append("document.getElementById('" + btnOK.ClientID.ToString().Replace('$', '_') + "').disabled = true;");
            sbValid.Append(this.Page.GetPostBackEventReference(this.btnReopen));
            sbValid.Append(";");
            this.btnReopen.Attributes.Add("onclick", sbValid.ToString());
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion
}
