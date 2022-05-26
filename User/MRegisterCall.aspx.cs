using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CallDeskBO;
using CallDeskBAL;
using System.Diagnostics;
using System.Web.Mail;
using TechDeskService;
using CallDeskDAL;

public partial class User_MRegisterCall : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                ClearControls();
                rfvUpload.Enabled = false;
               // GetLoggedUserDetails();
				if (Session["BranchName"]!=null && Session["BranchName"].ToString() == "Agent Branch")
				{
					trTicketValue.Visible = false;

				}
				else
				{
					trTicketValue.Visible = true;
				}


            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.StackTrace.ToString();
            }
        }
        DisableButtons();    

    }
    #endregion

    #region Issue Request Sub Type Dropdown Selected Index Change

    protected void ddlIssueRequestSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
			// Code For large market start
			if (ddlIssueRequestSubType.SelectedValue!="")
			{
				
				if (LargeMarketHideShow(Convert.ToInt32(ddlIssueRequestSubType.SelectedValue)) && (IsLargeMarket(Session["OfficeType"].ToString().ToUpper()) || Session["OfficeType"].ToString().ToUpper() == "SERVICE CENTER"))
				{
					
					tbllargemarket.Visible = true;
					BindChannel();
					
					if (IsLargeMarket(Session["OfficeType"].ToString().ToUpper()))
					{
						
						BindBranch(Session["OfficeType"].ToString().ToUpper(), Session["BranchCode"].ToString());
						ListItem oListBranchItem = ddlbranchname.Items.FindByText(Session["BranchName"].ToString());
						if (oListBranchItem != null)
							ddlbranchname.SelectedValue = oListBranchItem.Value.ToString();
						if (ddlbranchname.SelectedIndex > 0)
							ViewState["BranchID"] = ddlbranchname.SelectedItem.Value.ToString();
						if (Session["EmployeeFunction"].ToString() == "Sales Manager")
						{
							ListItem oListChannelItem = ddlchannel.Items.FindByText(Session["SMChannel"].ToString());
							if (oListChannelItem != null)
								ddlchannel.SelectedValue = oListChannelItem.Value.ToString();
							ddlchannelselectedindexchanged(sender,e);
							ddlbranchname.Enabled = false;
							ddlchannel.Enabled = false;
							ddlsm.Enabled = false;
						}
					}
					else
						BindBranch("SERVICE CENTER", Session["BranchCode"].ToString());
                    
                    
				}
				else
				{
					
					tbllargemarket.Visible = false;
					ViewState["BranchID"] = 0;
					ViewState["SMID"] = 0;

				}
			}

			// Code For large market end
            RegisterNewCallBAL objRNCBAL = new RegisterNewCallBAL();
            DataSet dsData = new DataSet();
            DataSet dsDescription = new DataSet();
            string strApplicationID, strIssueRequestTypeID, strIssueRequestSubTypeID, strComments, strFileName;

            int intIssueRequestSubTypeID;

            strApplicationID = ddlApplicationType.SelectedValue;
            strIssueRequestTypeID = ddlIssueRequestType.SelectedValue;
            strIssueRequestSubTypeID = ddlIssueRequestSubType.SelectedValue;

            if (strIssueRequestSubTypeID != "")
            {
                intIssueRequestSubTypeID = int.Parse(strIssueRequestSubTypeID);
            }
            else
            {
                intIssueRequestSubTypeID = -1;
            }

            dsData = objRNCBAL.GetTypeSubTypeComment(intIssueRequestSubTypeID);

            if (dsData.Tables[0].Rows.Count > 0)
            {
                strComments = Convert.ToString(dsData.Tables[0].Rows[0]["Description"]);
                if (strComments != "")
                {
                    hfRemark.Value = strComments;
					lblRemark.Text = strComments;
                }
                else
                {
                    hfRemark.Value = "";
					lblRemark.Text = "";
                }
            }

            if (dsData.Tables[1].Rows.Count > 0)
            {
                strFileName = Convert.ToString(dsData.Tables[1].Rows[0]["FileTemplateName"]);
                lnkFileTemplate.Text = strFileName;
            }
            else
            {
                lnkFileTemplate.Text = "";
            }

			if (ddlApplicationType.SelectedItem.Text == "Portal" &&
				ddlIssueRequestType.SelectedItem.Text == "Portal ID Locked" &&
				ddlIssueRequestSubType.SelectedItem.Text == "Portal ID Locked")
			{
				tblPortalIDLocked.Visible = true;
            
			}
			else
			{
				tblPortalIDLocked.Visible = false;
			}
			

			if (ddlApplicationType.SelectedItem.Text == "Renewal Notice")
			{
				tblPolicyNoRN.Visible = true;
			}
			else
			{
				tblPolicyNoRN.Visible = false;

			}
			dsDescription = GetDescription(ddlApplicationType.SelectedItem.Text.ToString(), ddlIssueRequestType.SelectedItem.Text.ToString(), ddlIssueRequestSubType.SelectedItem.Text.ToString());
			if (dsDescription.Tables[0].Rows.Count>0)
			{
				lbldesc.Text = dsDescription.Tables[0].Rows[0]["Description"].ToString();
				popupconfirm.Show();
			}
			else
			{
				popupconfirm.Hide();
			}

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Register Call Button Click Event

    protected void btnRegisterCall_Click(object sender, EventArgs e)
    {
        int intCheckException = 0;
        string strReturnVal = "";
        RegisterNewCallBAL objRNCBAL = new RegisterNewCallBAL();
        try
        {
            int intTAT = 0;
            string UploadSavePath = ConfigurationManager.AppSettings["UploadSavePath"].ToString();
            string sUserRemark, sApproverEmail, sApproverDesignation, sApproverName, sUserMail, sContactNumber, sApplication
                , sApproverID, sSApproverID, sSApproverEmail, sSApproverDesignation, sSApproverName, sReopenID, sBranchName, sCallDate, sUploadPath,
                sUserName, sCallType, sTypeofIR, sTicketNumber, sChannel, sIRSubType, sPriority,
                sGroups, sCallTAT, sAppSupportPerformer, sUserDesignation, sTicketValue, sServiceCenterName, sGroupType, scRegionID,scZoneID, sApproverTAT,sAppSupportTAT;

			string strPortalIDLocked = string.Empty;
			 string strPolicyNoRN = string.Empty;

            RegisterNewCallBO objRNCBO = new RegisterNewCallBO();
            objRNCBO.IssueRequestSubTypeID = Convert.ToInt32(ddlIssueRequestSubType.SelectedValue);

			if (ddlApplicationType.SelectedItem.Text == "Portal" &&
				ddlIssueRequestType.SelectedItem.Text == "Portal ID Locked" &&
				ddlIssueRequestSubType.SelectedItem.Text == "Portal ID Locked")
			{

				objRNCBO.UserRemark = txtRemark.Text + "  Portal ID - " + txtPortalID.Text;
				strPortalIDLocked = txtPortalID.Text;

			}
			else
			{
				objRNCBO.UserRemark = txtRemark.Text;
				strPortalIDLocked = "NA";
			}

			if (ddlApplicationType.SelectedItem.Text == "Renewal Notice")
			{
				objRNCBO.UserRemark = txtRemark.Text + "  Policy No - " + txtPolicyNoRN.Text;
				strPolicyNoRN = txtPolicyNoRN.Text;
			}
			else
			{
				objRNCBO.UserRemark = txtRemark.Text;
				strPolicyNoRN = "NA";

			}





            //objRNCBO.UserRemark = txtRemark.Text;
            objRNCBO.ContactNumber = txtContactNumber.Text;
            objRNCBO.UserName = sUserName = Membership.GetUser().UserName;
            objRNCBO.UserBranch = Session["LoggedBranch"].ToString();
            objRNCBO.TicketValue = txtTicketValue.Text.Trim() == string.Empty ? 0 : System.Convert.ToDecimal(txtTicketValue.Text.Trim());
          //s  string strN = objRNCBO.TicketNumber = objRNCBAL.GetTicketNumber();
            string strN = objRNCBO.TicketNumber = GetTicketNumberByBranchCode(objRNCBO.UserBranch.ToString());


            if (strN == "0")
            {
                lblMessage.Text = "Ticket could not be generated";
                return;
            }

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
					fileName = fileName.Replace("&", "_");
                    savePath = Server.MapPath("..\\Files") + "\\" + strN + "_" + fileName;
                    fuUpLoadFile.PostedFile.SaveAs(savePath);
                    objRNCBO.UploadFile = UploadSavePath + strN + "_" + fileName;
                }
            }

            strReturnVal = objRNCBAL.AddCallDetails(objRNCBO.IssueRequestSubTypeID, objRNCBO.UserName, objRNCBO.ContactNumber, objRNCBO.UserRemark, objRNCBO.UploadFile, objRNCBO.UserBranch, objRNCBO.TicketNumber, objRNCBO.TicketValue,strPortalIDLocked,strPolicyNoRN,Convert.ToInt32(ViewState["SMID"]), Convert.ToInt32(ViewState["BranchID"]));
      

            if (strReturnVal.Length > 13)
            {
                lblMessage.Text = strReturnVal;
                ClearControls();
                return;
            }

            intCheckException = 1;

            
           //s- TechDeskService objService = new TechDeskService();

            TechDeskService.TechDeskService objService = new TechDeskService.TechDeskService();
            TrackCallBAL objTCBAL = new TrackCallBAL();
            DataSet dsCallDetails = new DataSet(); 
            string strLoggedBranch = Convert.ToString(Session["LoggedBranch"]);

            dsCallDetails = objTCBAL.GetCallDetailsByTicketNumber(strReturnVal, strLoggedBranch, sUserName);

            string strCallCreatedBy = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallCreatedBy"]);
            sUserName = Convert.ToString(Session["EmployeeName"].ToString().ToUpper()) + " (" + strCallCreatedBy + ")";
            if (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["BranchName"]) == "Agent Branch")
            {
                //string strAgentCode = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AgentCode"]);                
				sUserName = Convert.ToString(Session["EmployeeName"].ToString().ToUpper()) + " (" + strCallCreatedBy + ")";

            }

            sChannel = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SMChannel"]);
            sUserDesignation = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UserDesignation"]);
            sContactNumber = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ContactNumber"]);
            sUserMail = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UserMail"]);
            sUserRemark = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UserRemark"]);

            sBranchName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["BranchName"]);
            if (sBranchName == "Agent Branch")
            {
                sBranchName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SMBranchName"]);
            }
           //sCallDate = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallDate"]);
            DateTime dtCallDate = Convert.ToDateTime(dsCallDetails.Tables[0].Rows[0]["CallDate"]);
            sCallDate = dtCallDate.ToString("yyyy-MM-dd hh:mm:ss");           
   
            sUploadPath = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UploadedScreen"]);

            sTicketNumber = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["TicketNumberPK"]);

            sTicketValue = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["TicketValue"]);            
            sReopenID = null;

            sCallType = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallType"]);
            sApplication = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApplicationName"]);
            sTypeofIR = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["IssueRequestType"]);
            sIRSubType = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["IssueRequestSubType"]);
            sPriority = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Priority"]);
            sGroups = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Groups"]);
            sServiceCenterName = dsCallDetails.Tables[0].Rows[0]["ServiceCenterName"] ==null?"":Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ServiceCenterName"]);
			sGroupType = dsCallDetails.Tables[0].Rows[0]["GroupType"] == null ? "direct" : Convert.ToString(dsCallDetails.Tables[0].Rows[0]["GroupType"]);
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
				sGroups = sGroups + "-" +sServiceCenterName;
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
			if (sGroups == "HUB")
			{
				sGroups = sGroups + scZoneID;
			}
            if (sGroups == "COP")
            {
				if (sServiceCenterName == "SC-East")
				{
					sGroups = "COPE";
				}
				else if( sServiceCenterName == "SC-West")
				{
					sGroups = "COPW";
				}
				else if(sServiceCenterName == "SC-North")
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

            intTAT = Convert.ToInt32(dsCallDetails.Tables[0].Rows[0]["CallTAT"]);
            sCallTAT = Convert.ToString(intTAT * 60 * 60);
            sAppSupportPerformer = null;

            sApproverID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverID"]);
            sApproverName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverName"]);
            sApproverEmail = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverMail"]);
            sApproverDesignation = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverDesignation"]);

            sSApproverID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverID"]);
            sSApproverName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverName"]);
            sSApproverEmail = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverMail"]);
            sSApproverDesignation = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverDesignation"]);
//            string strIsSendMailToFApprover = string.Empty;
//            if (!string.IsNullOrEmpty(dsCallDetails.Tables[0].Rows[0]["IsSendEmailFApprover"].ToString()))
//            {
//                bool isSendMailFApprover = Convert.ToBoolean(dsCallDetails.Tables[0].Rows[0]["IsSendEmailFApprover"]) == true ? true : false;
//                strIsSendMailToFApprover = isSendMailFApprover.ToString();
//            }

         
            string strResponseVal = objService.STARTCALLDESK(sUserRemark, sApproverEmail, sApproverDesignation, sApproverName, sUserMail, sContactNumber, sApplication, sApproverID, sReopenID, sBranchName, sCallDate, sUploadPath, sUserName, sCallType, sTypeofIR, sTicketNumber, sUserDesignation, sGroups, sIRSubType, sCallTAT, sSApproverID, sAppSupportPerformer, sSApproverDesignation, sSApproverEmail, sSApproverName,sTicketValue,"rgicl", "rgicl","",sPriority,""
                //[CR-1]  CQR CR Add Fields 06052019 Start
               , "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""
                //[CR-1]  CQR CR Add Fields 06052019 EN=nd
                );
            //System.Diagnostics.EventLog.WriteEntry("SavvionTicket", strResponseVal); 
            intCheckException = 2;
            objRNCBAL.UpdateSavvionResponse(strReturnVal, strResponseVal);

         
//			string strEmailText = string.Empty;			
//			string strExpClosedDate = string.Empty;
//			string strCallStatus = "Open";
//			string strEmailSubject = "New Ticket generation ( "+ sTicketNumber+ " )";
//			strExpClosedDate = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ExpectedCloseDate"]);
//			strEmailText = GetEmailtextCallLog(sUserName, sTicketNumber, sUserMail, sCallDate, sApplication, sTypeofIR, strExpClosedDate, strCallStatus);
//			if (sUserMail != string.Empty)
//			{
//				SendEmailToUser(sUserMail, strEmailSubject, strEmailText);
//			}
           
			string strSMSText = string.Empty;
			string strContactNo = string.Empty;
			strContactNo = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ContactNumber"]);
			strSMSText = GetSMStextCallLog(sTicketNumber, Session["EmployeeName"].ToString());
			if (strContactNo != string.Empty)
			{
				SendSMSToUser(strContactNo, strSMSText);
			}


            //lblMessage.Text = "Your Call is Registered and Call Ticket Number is " + strReturnVal;

			StringBuilder stPopupScript = new StringBuilder();
			stPopupScript.Append("<script language='javascript'>");
			if (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallType"]).ToLower() == "issue")
			{
				stPopupScript.Append("var newWin = window.open('/CallDeskOnline/User/PopUpTicketDetails.aspx?TicketNo='$" + strReturnVal + "'$','PopUpWindowName','width=420,left=300,top=250,height=150,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up

			}
			else
			{
				if (string.IsNullOrEmpty(Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverID"])))
				{
					stPopupScript.Append("var newWin = window.open('/CallDeskOnline/User/PopUpTicketDetails.aspx?TicketNo='$" + strReturnVal + "'$','PopUpWindowName','width=420,left=300,top=250,height=180,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up

				}
				else
				{
					stPopupScript.Append("var newWin = window.open('/CallDeskOnline/User/PopUpTicketDetails.aspx?TicketNo='$" + strReturnVal + "'$','PopUpWindowName','width=420,left=300,top=250,height=210,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up
				}

			}
			//stPopupScript.Append("newWin.focus()");
			stPopupScript.Append("</script>");
			stPopupScript = stPopupScript.Replace("'$", "");
			stPopupScript = stPopupScript.Replace("$'", "");
			Page.RegisterClientScriptBlock("PopUpTicketwindowOpen", stPopupScript.ToString());

            ClearControls();
            }
        catch (Exception ex)
        {
           
            //System.Diagnostics.EventLog.WriteEntry("CallDeskError", ex.Message.ToString());
            if (intCheckException == 1)
            {
                int intReturnValue = objRNCBAL.RegisterCallFailedUpdate(strReturnVal);
                lblMessage.Text = ex.Message;
                lblMessage.Text = "Problem occurred while updating data to the server, please try after some time.";
            }
            if (intCheckException == 2)
            {
                lblMessage.Text = "Ticket " + strReturnVal + " generated successfully but savvaion status could not be updated.";
            }          
            FileStream fs1 = new FileStream(Server.MapPath("..\\Files") + "\\" + "RegisterCall.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw1 = new StreamWriter(fs1);
            sw1.Write("\r\n =======================================================================================");
            sw1.Write("\r\n Log Entry On : " + System.DateTime.Now);
            sw1.Write("\n " + ex.Message);
            //sw1.Write("\n " + ex.InnerException);
            sw1.Write("\n " + strReturnVal);
            sw1.Close();
            fs1.Close();
            ClearControls();
             

        }
    }

    #endregion

    #region Cancel Button Code

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Default.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Clear Controls

    public void ClearControls()
    {
        txtContactNumber.Text = "";
        txtRemark.Text = "";
        txtTicketValue.Text = string.Empty;
        ccdApplication.SelectedValue = "Select Application";
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
            sbValid.Append("document.getElementById('" + btnRegisterCall.ClientID.ToString().Replace('$', '_') + "').disabled = true;");
            sbValid.Append("document.getElementById('" + btnCancel.ClientID.ToString().Replace('$', '_') + "').disabled = true;");
            sbValid.Append(this.Page.GetPostBackEventReference(this.btnRegisterCall));
            sbValid.Append(";");
            this.btnRegisterCall.Attributes.Add("onclick", sbValid.ToString());
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Download File Template Code

    protected void lnkFileTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            DownloadFile(lnkFileTemplate.Text);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Download File Code

    public void DownloadFile(string strFileName)
    {
        try
        {
            FileInfo file;
            string filename = Server.MapPath("..\\TemplateFiles\\") + strFileName;
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

//    #region ddlIssueRequestType SelectedIndexChanged
//    protected void ddlIssueRequestType_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        if (!string.IsNullOrEmpty(ddlApplicationType.SelectedValue) && !string.IsNullOrEmpty(ddlIssueRequestType.SelectedValue))
//        {
//            int applicationID = Convert.ToInt32(ddlApplicationType.SelectedValue);
//            int issueRequestTypeID = Convert.ToInt32(ddlIssueRequestType.SelectedValue);
//            if (Session["LocationTypeID"] != null)
//            {
//                int locationID = Convert.ToInt32(Session["LocationTypeID"]);
//                RegisterNewCallBAL objRCBAL = new RegisterNewCallBAL();
//                DataTable dtIssueRequestSubType = objRCBAL.GetIssueRequestSubTypeByIssueRequestTypeRC(applicationID, issueRequestTypeID, locationID);
//                if (dtIssueRequestSubType.Rows.Count == 0)
//                {
//                    lblMessage.Text = "The Application Type and Issue Request Type is not mapped to the Logged Branch.";
//                    btnRegisterCall.Enabled = false;
//                }
//                else
//                {
//                    lblMessage.Text = string.Empty;
//                    btnRegisterCall.Enabled = true;
//                }
//            }
//        }
//        else
//        {
//            lblMessage.Text = string.Empty;
//            btnRegisterCall.Enabled = true;
//        }
//
//    }
//    #endregion

//    #region ddlApplicationType SelectedIndexChanged
//    protected void ddlApplicationType_SelectedIndexChanged(object sender, EventArgs e)
//    {            
//        lblMessage.Text = string.Empty;
//        btnRegisterCall.Enabled = true;        
//    }
//    #endregion

				   #region Send Email To User
				   public void SendEmailToUser(string strEmail,string strSubject,string strMailMsg)
				   {
					   try
					   {
						   MailMessage msg = new MailMessage();
						   msg.BodyFormat = MailFormat.Html; 
						   msg.To = strEmail;
						   msg.From = "calldesk@relianceada.com";
						   msg.Subject = strSubject;          
						   msg.Body = strMailMsg;
						   SmtpMail.SmtpServer = "10.65.8.47";
						   SmtpMail.Send(msg);
            
					   }
					   catch
					   {

					   }
				   }
				   #endregion

				   #region Send SMS To USer
				   public void SendSMSToUser(string strMobileNo, string smsMsg)
				   {
					   try
					   {
						   SMSSendService.SingleMessage msgData = new SMSSendService.SingleMessage();
						   SMSSendService.SendMessage sendMsg = new SMSSendService.SendMessage();
						   msgData.Department = "IT";
						   msgData.UserName = "intranet";
						   msgData.Password = "rgiclintra07#";
						   msgData.Message = smsMsg;
						   msgData.MobileNumber = strMobileNo;
						   sendMsg.Send(msgData);
					   }
					   catch
					   {

					   }
				   }
				   #endregion

				   #region Get TicketNumber By BranchCode
				   public string GetTicketNumberByBranchCode(params object[] param)
				   {
					   string ticketNumber = string.Empty;
					   try
					   {
						   ticketNumber= DataUtils.ExecuteScalar("usp_GetTicketNumberByBranchCode", param).ToString();
					   }
					   catch (Exception ex)
					   {
						   lblMessage.Text = ex.Message;
					   }
					   return ticketNumber;
				   }
				   #endregion

				   #region Get Email text CallLog
				   public string GetEmailtextCallLog(string empName, string strTicketNo, string strEmailID, string strCallDate, string strAppName,string strCategory,string strExpClosedDate,string strCallStatus)
				   {
					   string strEmail = string.Empty;
					   strEmail = @"Dear " + empName + ",<br><br><br>";    
					   strEmail = strEmail + "Thank you for providing us an opportunity to serve you better. We hereby<br>";
					   strEmail = strEmail + "confirm that your call has been logged by us with the following details.<br><br>";
					   strEmail = strEmail + "Your ticket details : <br>";
					   strEmail = strEmail + "<b>Ticket no</b> : " + strTicketNo + "<br>";
					   strEmail = strEmail + "<b>Call created by</b> :" + empName + "<br>";
					   strEmail = strEmail + "<b>User E-Mail</b> :" + strEmailID + "<br>";
					   strEmail = strEmail + "<b>Call Date</b> : " + strCallDate + "<br>";
					   strEmail = strEmail + "<b>Application Name</b>: " + strAppName + "<br>";
					   strEmail = strEmail + "<b>Category</b> : " + strAppName + "<br>";
					   strEmail = strEmail + "<b>Expected Closure Date</b> : " + strExpClosedDate + "<br>";
					   strEmail = strEmail + "<b>Call Status</b> : " + strCallStatus + "<br>";
					   strEmail = strEmail + "In order to serve you better with quick resolution, your call will be Forwarded <br>";
					   strEmail = strEmail + "to the IT Support engineer. You will get the Resolution with the Expected Closer Date and time. <br><br>";
        
					   strEmail = strEmail + "Click <a href='http://calldesk.reliancegeneral.co.in/'>here</a> on this link to login into the system <br><br><br><br><br>";
					   strEmail = strEmail + "Thank you<br>";
					   strEmail = strEmail + "Reliance Application Support Team";
					   return strEmail;
				   }
				   #endregion

				   #region Get SMS text CallLog
				   public string GetSMStextCallLog(string strTicketNo,string strEmpName)
				   {
					   string strSMS = string.Empty;      
					   strSMS = @"Dear <"+strEmpName.ToUpper()+">, you have successfully registered a call in Reliance Call desk. Your Ticket no is  <"+strTicketNo+"> will be taken care shortly."; 
					   return strSMS;       
				   }
				   #endregion

				   #region No Button Code

				   protected void btnno_Click(object sender, EventArgs e)
				   {
					   try
					   {
						   Response.Redirect("../User/RegisterCall.aspx");
					   }
					   catch (Exception ex)
					   {
						   lblMessage.Text = ex.Message;
					   }
				   }

				   #endregion

				   #region Get Description
				   public DataSet GetDescription(params object[] param)
				   {
					   DataSet oDSDescription = new DataSet();
					   try
					   {
						   oDSDescription = DataUtils.ExecuteDataset("sp_popupdescription", param);
					   }
					   catch (Exception ex)
					   {
						   lblMessage.Text = ex.Message;
					   }
					   return oDSDescription;
				   }
				   #endregion

				   #region Get and Bind Branch For Large market
				   public void BindBranch(params object[] param)
				   {
					   DataSet oDSBindBranch = new DataSet();
					   try
					   {
						   oDSBindBranch = DataUtils.ExecuteDataset("usp_GetBranchForLargeMarket", param);
						   ddlbranchname.DataSource = oDSBindBranch.Tables[0];
						   ddlbranchname.DataTextField = "BranchName";
						   ddlbranchname.DataValueField = "BranchID_PK";
						   ddlbranchname.DataBind();
						   ddlbranchname.Items.Insert(0,"--Select--");
					   }
					   catch (Exception ex)
					   {
						   lblMessage.Text = ex.Message;
					   }
       
				   }
				   #endregion

				   #region Get and Bind Channel For Large market
				   public void BindChannel(params object[] param)
				   {
					   DataSet oDSBindChanel = new DataSet();
					   try
					   {
						   oDSBindChanel = DataUtils.ExecuteDataset("usp_GetChannelforLargeMarket", param);
						   ddlchannel.DataSource = oDSBindChanel.Tables[0];
						   ddlchannel.DataTextField = "ChannelName";
						   ddlchannel.DataValueField = "ChannelID_PK";
						   ddlchannel.DataBind();
						   ddlchannel.Items.Insert(0, "--Select--");
					   }
					   catch (Exception ex)
					   {
						   lblMessage.Text = ex.Message;
					   }

				   }
				   #endregion

				   #region Get and Bind SM For Large market
				   public void BindSM(params object[] param)
				   {
					   DataSet oDSBindSM = new DataSet();
					   try
					   {
						   oDSBindSM = DataUtils.ExecuteDataset("usp_GetSMforLargeMarket", param);
						   ddlsm.DataSource = oDSBindSM.Tables[0];
						   ddlsm.DataTextField = "Employee_Name";
						   ddlsm.DataValueField = "Employee_ID_PK";
						   ddlsm.DataBind();
						   ddlsm.Items.Insert(0, "--Select--");
					   }
					   catch (Exception ex)
					   {
						   lblMessage.Text = ex.Message;
					   }

				   }
				   #endregion

				   #region large market hide show
   
				   public bool LargeMarketHideShow(params object[] param)
				   {
					   DataSet oDSHideShow = new DataSet();
					   bool result = false ;
					   try
					   {
						   oDSHideShow = DataUtils.ExecuteDataset("usp_GetIssueRequestTypeforcategory", param);
						   if (oDSHideShow.Tables[0].Rows.Count > 0)
						   {
							   if (oDSHideShow.Tables[0].Rows[0]["status"].ToString() == "Exist")
								   result = true;
							   else
								   result = false;
						   }
            
            
					   }
					   catch (Exception ex)
					   {
						   lblMessage.Text = ex.Message;
					   }
					   return result;

				   }
       
				   #endregion

				   #region Is Large Market

				   public bool IsLargeMarket(params object[] param)
				   {
					   DataSet oDSIslarge = new DataSet();
					   bool result = false;
					   try
					   {
						   oDSIslarge = DataUtils.ExecuteDataset("usp_IsLargeMarket", param);
						   if (oDSIslarge.Tables[0].Rows.Count > 0)
						   {
							   result = true;
						   }


					   }
					   catch (Exception ex)
					   {
						   lblMessage.Text = ex.Message;
					   }
					   return result;

				   }

				   #endregion

				   protected void ddlchannelselectedindexchanged(object sender, EventArgs e)
				   {
					   try
					   {
						   if ((ddlbranchname.SelectedIndex > 0) && (ddlchannel.SelectedIndex > 0))
						   {
							   BindSM(Convert.ToInt32(ddlbranchname.SelectedItem.Value), Convert.ToInt32(ddlchannel.SelectedItem.Value));
							   ListItem oListSMItem = ddlsm.Items.FindByText(Session["EmployeeName"].ToString());
							   if (oListSMItem != null)
								   ddlsm.SelectedValue = oListSMItem.Value.ToString();
							   if (ddlsm.SelectedIndex > 0)
								   //SMID = Convert.ToInt32(ddlsm.SelectedItem.Value);
								   ViewState["SMID"] = ddlsm.SelectedItem.Value.ToString();
						   }
					   }
					   catch (Exception ex)
					   {
						   lblMessage.Text = ex.Message;
					   }
				   }


				   protected void ddlbranchnameselectedindexchanged(object sender, EventArgs e)
				   {
					   try
					   {
						   ddlchannel.SelectedIndex = 0;
						   if (ddlbranchname.SelectedIndex > 0)
							   //BranchID = Convert.ToInt32(ddlbranchname.SelectedItem.Value);
							   ViewState["BranchID"] = ddlbranchname.SelectedItem.Value.ToString();
						   // ddlsm.SelectedIndex = 0;
						   BindSM(0, 0);

					   }
					   catch (Exception ex)
					   {
						   lblMessage.Text = ex.Message;
					   }
				   }


				   protected void ddlsmselectedindexchanged(object sender, EventArgs e)
				   {
					   try
					   {
						   if(ddlsm.SelectedIndex>0)
							   //SMID = Convert.ToInt32(ddlsm.SelectedItem.Value);
							   ViewState["SMID"] = ddlsm.SelectedItem.Value.ToString();

					   }
					   catch (Exception ex)
					   {
						   lblMessage.Text = ex.Message;
					   }
                   }

                   #region Get Logged in User Details

                   public void GetLoggedUserDetails()
                   {
                       EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
                       DataTable dtLED = new DataTable();
                       string UserName;
                       UserName = Membership.GetUser().UserName;
                       if (Session["EmployeeName"] == null)
                       {
                           dtLED = objEMBAL.GetLoggedEmployeeDetailsForASLC(UserName, DateTime.Now.Date);
                           Session["EmployeeName"] = dtLED.Rows[0]["EmployeeName"].ToString();
                           Session["EmployeeDesignation"] = dtLED.Rows[0]["EmployeeDesignation"].ToString();
                           Session["BranchCode"] = dtLED.Rows[0]["BranchCode"].ToString();
                           Session["SMChannel"] = dtLED.Rows[0]["SMChannel"].ToString();
                           Session["LoggedBranch"] = dtLED.Rows[0]["BranchCode"].ToString(); // Same as Default branch
                           Session["LoweredEmail"] = dtLED.Rows[0]["LoweredEmail"].ToString();
                           Session["BranchName"] = dtLED.Rows[0]["BranchName"].ToString();
                           Session["RegionCode"] = dtLED.Rows[0]["RegionCode"].ToString();
                           Session["RegionName"] = dtLED.Rows[0]["RegionName"].ToString();
                           Session["LastPasswordChangedDate"] = dtLED.Rows[0]["LastPasswordChangedDate"].ToString();
                           Session["PasswordChangeDaysAllowed"] = dtLED.Rows[0]["PasswordChangeDaysAllowed"].ToString();
                           Session["LocationTypeID"] = dtLED.Rows[0]["LocationTypeID"].ToString();
                           Session["SMBranchName"] = dtLED.Rows[0]["SMBranchName"].ToString();
                           Session["UserName"] = dtLED.Rows[0]["UserName"].ToString();
                           //LM
                           Session["OfficeType"] = dtLED.Rows[0]["OfficeType"].ToString();
                           Session["EmployeeFunction"] = dtLED.Rows[0]["Employee_Function"].ToString();
                           Session["SMChannel"] = dtLED.Rows[0]["SM_Channel"].ToString();
                           //


                       }
                   }

                   #endregion
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                