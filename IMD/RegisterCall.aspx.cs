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
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection DBConnection;

    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                ClearControls();
                rfvUpload.Enabled = false;
                if (Session["BranchName"]!=null && Session["BranchName"].ToString() == "Agent Branch")
                {
                    trTicketValue.Visible = false;

                }
                else
                {
                    trTicketValue.Visible = true;
                }

                //[CR-10] IMD Group Creation Start
                BindRegion();
                //[CR-10] IMD Group Creation End

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                FileStream fs1 = new FileStream(Server.MapPath("..\\Files") + "\\" + "RegisterCall.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw1 = new StreamWriter(fs1);
                sw1.Write("\r\n =======================================================================================");
                sw1.Write("\r\n Log Entry On : " + System.DateTime.Now);
                sw1.Write("\n " + ex.Message);
                sw1.Write("\n " + ex.StackTrace);
                //sw1.Write("\n " + ex.InnerException);
                //sw1.Write("\n " + strReturnVal);
                sw1.Close();
                fs1.Close();
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

            if (ddlApplicationType.SelectedItem.Text == "IMD-Portal" &&
                ddlIssueRequestType.SelectedItem.Text == "Portal ID Locked" &&
                ddlIssueRequestSubType.SelectedItem.Text == "Unlock ID")
            {
                tblPortalIDLocked.Visible = true;

            }
            else
            {
                tblPortalIDLocked.Visible = false;
            }
            dsDescription = GetDescription(ddlApplicationType.SelectedItem.Text.ToString(), ddlIssueRequestType.SelectedItem.Text.ToString(), ddlIssueRequestSubType.SelectedItem.Text.ToString());
            if (dsDescription.Tables[0].Rows.Count > 0)
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
                sUserName, sCallType, sTypeofIR, sTicketNumber, sChannel, sIRSubType, sPriority, scRegionID, scZoneID,
                sGroups, sCallTAT, sAppSupportPerformer, sUserDesignation, sTicketValue, sServiceCenterName, sApproverTAT, sAppSupportTAT;

            string strPortalIDLocked = string.Empty;
            string strPolicyNoRN = string.Empty;
            strPolicyNoRN = "NA";

            RegisterNewCallBO objRNCBO = new RegisterNewCallBO();
            objRNCBO.IssueRequestSubTypeID = Convert.ToInt32(ddlIssueRequestSubType.SelectedValue);

            if (ddlApplicationType.SelectedItem.Text == "IMD-Portal" &&
                ddlIssueRequestType.SelectedItem.Text == "Portal ID Locked" &&
                ddlIssueRequestSubType.SelectedItem.Text == "Unlock ID")
            {

                objRNCBO.UserRemark = txtRemark.Text + "  Portal ID - " + txtPortalID.Text;
                strPortalIDLocked = txtPortalID.Text;

            }
            else
            {
                objRNCBO.UserRemark = txtRemark.Text;
                strPortalIDLocked = "NA";

            }


            //objRNCBO.UserRemark = txtRemark.Text;
            objRNCBO.ContactNumber = txtContactNumber.Text;
            //objRNCBO.UserName = sUserName = Membership.GetUser().UserName;
            objRNCBO.UserName = sUserName = Convert.ToString(Session["AgentUserID"]);


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

            //[CR-10] IMD Group Creation Start
            objRNCBO.UserBranch = ddlBranch.SelectedValue;
            //[CR-10] IMD Group Creation End

           // strReturnVal = objRNCBAL.AddCallDetails(objRNCBO.IssueRequestSubTypeID, objRNCBO.UserName, objRNCBO.ContactNumber, objRNCBO.UserRemark, objRNCBO.UploadFile, objRNCBO.UserBranch, objRNCBO.TicketNumber, objRNCBO.TicketValue, strPortalIDLocked, strPolicyNoRN, 0, 0/*[CR-10]Start*/, 0, null/*[CR-10]End*/);
            //strReturnVal = objRNCBAL.AddCallDetails(objRNCBO.IssueRequestSubTypeID, objRNCBO.UserName, objRNCBO.ContactNumber, objRNCBO.UserRemark, objRNCBO.UploadFile, objRNCBO.UserBranch, objRNCBO.TicketNumber, objRNCBO.TicketValue, strPortalIDLocked, strPolicyNoRN, Convert.ToInt32(ViewState["SMID"]), Convert.ToInt32(ViewState["BranchID"]), null, null, null);
            strReturnVal = objRNCBAL.AddCallDetails(objRNCBO.IssueRequestSubTypeID, objRNCBO.UserName, objRNCBO.ContactNumber, objRNCBO.UserRemark, objRNCBO.UploadFile, objRNCBO.UserBranch, objRNCBO.TicketNumber, objRNCBO.TicketValue, strPortalIDLocked, strPolicyNoRN, Convert.ToInt32(ViewState["SMID"]), Convert.ToInt32(ViewState["BranchID"]), null, null, null, null, null, null, null, null, null);
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

            //[CR-10] IMD Group Creation Start
            //string strLoggedBranch = Convert.ToString(Session["LoggedBranch"]);
            string strLoggedBranch = ddlBranch.SelectedValue;

            //[CR-10] IMD Group Creation End

            dsCallDetails = objTCBAL.GetCallDetailsByTicketNumber(strReturnVal, strLoggedBranch, sUserName);

            //[CR-10] IMD Group Creation
            int AIRSMID_FK = Convert.ToInt32(dsCallDetails.Tables[0].Rows[0]["AIRSMID_FK"]);
            //[CR-10] IMD Group Creation

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

            //[CR-10] IMD Group Creation

            DataSet dsGroupAssign = new DataSet();
            dsGroupAssign = GetGroupAssign(AIRSMID_FK, strLoggedBranch, strCallCreatedBy);
            if (dsGroupAssign != null && dsGroupAssign.Tables[0] != null)
            {
                if (dsGroupAssign.Tables[0].Rows.Count >= 1)
                {
                    string GroupAssign = dsGroupAssign.Tables[0].Rows[0]["GroupAssign"].ToString();

                    if (!string.IsNullOrEmpty(GroupAssign))
                    {
                        sGroups = GroupAssign;
                    }
                }
            }

            //[CR-10] MD Group Creation

            intTAT = Convert.ToInt32(dsCallDetails.Tables[0].Rows[0]["CallTAT"]);
            sCallTAT = Convert.ToString(intTAT * 60 * 60);
            sAppSupportPerformer = null;

            sApproverID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverID"]);
            sApproverName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverName"]);
            sApproverEmail = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverMail"]);
            sApproverDesignation = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverDesignation"]);

            sSApproverID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SAppro verID"]);
            sSApproverName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverName"]);
            sSApproverEmail = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverMail"]);
            sSApproverDesignation = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverDesignation"]);
            //            string strIsSendMailToFApprover = string.Empty;
            //            if (!string.IsNullOrEmpty(dsCallDetails.Tables[0].Rows[0]["IsSendEmailFApprover"].ToString()))
            //            {
            //                bool isSendMailFApprover = Convert.ToBoolean(dsCallDetails.Tables[0].Rows[0]["IsSendEmailFApprover"]) == true ? true : false;
            //                strIsSendMailToFApprover = isSendMailFApprover.ToString();
            //            }


            string strResponseVal = objService.STARTCALLDESK(sUserRemark, sApproverEmail, sApproverDesignation, sApproverName, sUserMail, sContactNumber, sApplication, sApproverID, sReopenID, sBranchName, sCallDate, sUploadPath, sUserName, sCallType, sTypeofIR, sTicketNumber, sUserDesignation, sGroups, sIRSubType, sCallTAT, sSApproverID, sAppSupportPerformer, sSApproverDesignation, sSApproverEmail, sSApproverName, sTicketValue, "rgicl", "rgicl", "", sPriority,""
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
            string strFirstName = string.Empty;
            strContactNo = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ContactNumber"]);
            strFirstName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["First_Name"]);
            strSMSText = GetSMStextCallLog(sTicketNumber, strFirstName, sApplication);

            // Added to call new SMS service 06-05-2016

            string App_Process = "Call Register";
            string SMS_Event = "Call Register";
            string Ref_Value = strReturnVal;
            string Ref_Name = "Ticket no";
            string Department = "1";

            // Added to call new SMS service 06-05-2016


            //[CR-21] Mobile No validation start
            if (!string.IsNullOrEmpty(strContactNo))
            {
                string expression = @"^[789]\d{9}$";

                Match match = Regex.Match(strContactNo, expression, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    Set_Message_InServer(strContactNo, strSMSText, "", App_Process, SMS_Event, Ref_Name, Ref_Value, Department);
                }
                else
                {

                }
            }
            //[CR-21] Mobile No validation End


            //lblMessage.Text = "Your Call is Registered and Call Ticket Number is " + strReturnVal;

            StringBuilder stPopupScript = new StringBuilder();
            stPopupScript.Append("<script language='javascript'>");
            if (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallType"]).ToLower() == "issue")
            {
                stPopupScript.Append("var newWin = window.open('PopUpTicketDetails.aspx?TicketNo='$" + strReturnVal + "'$','PopUpWindowName','width=420,left=300,top=250,height=150,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up

            }
            else
            {
                if (string.IsNullOrEmpty(Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverID"])))
                {
                    stPopupScript.Append("var newWin = window.open('PopUpTicketDetails.aspx?TicketNo='$" + strReturnVal + "'$','PopUpWindowName','width=420,left=300,top=250,height=180,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up

                }
                else
                {
                    stPopupScript.Append("var newWin = window.open('PopUpTicketDetails.aspx?TicketNo='$" + strReturnVal + "'$','PopUpWindowName','width=420,left=300,top=250,height=210,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up
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
            Response.Redirect("../IMD/Default.aspx");
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

        //[CR-10] IMD Group Creation Start
        ddlRegion.SelectedIndex = -1;
        ddlBranch.SelectedIndex = -1;
        //[CR-10] IMD Group Creation End
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
    public void SendEmailToUser(string strEmail, string strSubject, string strMailMsg)
    {
        try
        {
            MailMessage msg = new MailMessage();
            msg.BodyFormat = MailFormat.Html;
            msg.To = strEmail;
            msg.From = "calldesk@reliancegeneral.co.in";
            msg.Subject = strSubject;
            msg.Body = strMailMsg;
            SmtpMail.SmtpServer = "10.65.8.45";
            SmtpMail.Send(msg);

        }
        catch
        {

        }
    }
    #endregion

    #region Send SMS To USer

    /// <summary>
    /// Added to log SMS content requirment raised by Neat Gawde
    /// SMS Implementation mail - Provission for Event and Department Id-Start
    /// </summary>


    public void Set_Message_InServer(string PhoneNumber, string MessageText, string SubmittedTime
        , string App_Process, string SMS_Event, string Ref_Name, string Ref_Value, string Department
        )
    {

        try
        {

            string strUnique = Ref_Value + "-" + Guid.NewGuid().ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-"));

            if (strUnique.Length >= 20)
            {
                strUnique = strUnique.Substring(0, 20);
            }



            SMSSendService.SingleMessage msgData = new SMSSendService.SingleMessage();
            SMSSendService.SendMessage sendMsg = new SMSSendService.SendMessage();

            //SingleMessage msg = new SingleMessage();
            msgData.Message = MessageText;            //Message to Send
            msgData.MobileNumber = PhoneNumber;                //Mobile Number - Should not be in DND list
            msgData.UserName = "intranet";
            msgData.Password = "rgiclintra07#";

            //[CR-253]-SMS Implementation mail - Provission for Event and Department Id-Start
            //msg.Department = "IT";
            msgData.App_Process = App_Process;
            msgData.SMS_Event = SMS_Event;
            msgData.Ref_Name = Ref_Name;
            msgData.Ref_Value = Ref_Value;
            msgData.Department = Department;
            msgData.Source_RequestID = strUnique;// Ref_Value;

            //[CR-253]-SMS Implementation mail - Provission for Event and Department Id-End




            string retVal = sendMsg.Send(msgData);

            DBConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CallDeskDB"].ConnectionString);
            SqlCommand DBcommand = new SqlCommand("Insert_SMS_LOG", DBConnection);
            DBcommand.CommandType = CommandType.StoredProcedure;
            DBConnection.Open();
            DBcommand.Parameters.AddWithValue("@System_RequestID", strUnique);
            DBcommand.Parameters.AddWithValue("@App_Process", App_Process);
            DBcommand.Parameters.AddWithValue("@SMS_Event", SMS_Event);
            DBcommand.Parameters.AddWithValue("@Ref_Name", Ref_Name);
            DBcommand.Parameters.AddWithValue("@Ref_Value", Ref_Value);
            DBcommand.Parameters.AddWithValue("@Department", Department);
            DBcommand.Parameters.AddWithValue("@Message", MessageText);
            DBcommand.Parameters.AddWithValue("@MobileNo", PhoneNumber);
            DBcommand.Parameters.AddWithValue("@SMS_TokenID", retVal);

            DBcommand.ExecuteNonQuery();


            //Code changes end
        }
        catch (Exception Ex)
        {
            string str;
            str = Ex.ToString();
        }
        finally
        {
            DBConnection.Close();
        }
    }


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
            ticketNumber = DataUtils.ExecuteScalar("usp_GetTicketNumberByBranchCode", param).ToString();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return ticketNumber;
    }
    #endregion

    #region Get Email text CallLog
    public string GetEmailtextCallLog(string empName, string strTicketNo, string strEmailID, string strCallDate, string strAppName, string strCategory, string strExpClosedDate, string strCallStatus)
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
    public string GetSMStextCallLog(string strTicketNo, string strEmpName, string application)
    {
        string strSMS = string.Empty;
        //strSMS = @"Dear <" + strEmpName.ToUpper() + ">, you have successfully registered a call in Reliance Call desk. Your Ticket no is  <" + strTicketNo + "> will be taken care shortly.";
        //strSMS = @"Dear <" + strEmpName.ToUpper() + ">, Ticket no<" + strTicketNo + "> for<" + application + "> is registered successfully with RGICL and will be taken care shortly."; 

        strSMS = @"Congratulation! You've successfully registered at Reliance Call desk. Your call ticket no. is " + strTicketNo + ". To login visit: http://calldesk.reliancegeneral.co.in";
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


    //[CR-10] IMD Group Creation Start
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        int RegionId = Convert.ToInt32(ddlRegion.SelectedValue);
        BindBranch(RegionId);
    }

    public void BindBranch(int RegionId)
    {
        DataTable dtBranch = new DataTable();

        dtBranch = GetBranch(RegionId);
        ddlBranch.Items.Clear();
        ddlBranch.DataSource = dtBranch;
        ddlBranch.DataTextField = "BranchName";
        ddlBranch.DataValueField = "BranchCode";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public DataTable GetBranch(int RegionId)
    {
        DataTable dt = null;
        SqlConnection con = null;
        string strCon = ConfigurationManager.ConnectionStrings["CallDeskDB"].ConnectionString;

        try
        {
            dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            con = new SqlConnection(strCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetBranchByRegion";
            cmd.Parameters.AddWithValue("@RegionID_FK", RegionId);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(dt);
            con.Close();
        }
        catch (Exception)
        {

        }
        finally
        {
            try
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            catch (Exception ex) { }
        }
        return dt;
    }

    public void BindRegion()
    {
        DataTable dtRegion = new DataTable();

        dtRegion = GetRegion();
        ddlRegion.Items.Clear();
        ddlRegion.DataSource = dtRegion;
        ddlRegion.DataTextField = "RegionName";
        ddlRegion.DataValueField = "RegionCode";
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public DataTable GetRegion()
    {
        DataTable dt = null;
        SqlConnection con = null;
        string strCon = ConfigurationManager.ConnectionStrings["CallDeskDB"].ConnectionString;

        try
        {
            dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            con = new SqlConnection(strCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetRegionForIMD";
            // cmd.Parameters.AddWithValue("@ZoneID_PK", ZoneId);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(dt);
            con.Close();
        }
        catch (Exception)
        {

        }
        finally
        {
            try
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            catch (Exception ex) { }
        }
        return dt;
    }

    public DataSet GetGroupAssign(int AIRSMID, string BranchCode, string UserId)
    {
        DataSet ds = null;
        SqlConnection con = null;
        string strCon = ConfigurationManager.ConnectionStrings["CallDeskDB"].ConnectionString;

        try
        {
            ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            con = new SqlConnection(strCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetGroupAssign";
            cmd.Parameters.AddWithValue("@AIRSMID", AIRSMID);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            con.Close();
        }
        catch (Exception)
        {

        }
        finally
        {
            try
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            catch (Exception ex) { }
        }
        return ds;
    }

    //[CR-10] IMD Group Creation End
}
