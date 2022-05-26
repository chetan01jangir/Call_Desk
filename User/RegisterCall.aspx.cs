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
        //AntiforgeryChecker.Check(this, antiforgery);
        if (!Page.IsPostBack)
        {
            try
            {
                Session["ProcessGroup"] = rdoProcessing.SelectedValue; //"1";
                ClearControls();
                BindApplications();
                rfvUpload.Enabled = false;
                if (Session["BranchName"] != null && Session["BranchName"].ToString() == "Agent Branch")
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
            string strOfficeType = Session["OfficeType"].ToString().ToUpper();
            string strBranchCode = Session["BranchCode"].ToString();
            string strBranchName = Session["BranchName"].ToString();
            if (ddlIssueRequestSubType.SelectedValue != "")
            {
                // VO flow change start
                if (strOfficeType == "VIRTUAL OFFICE")
                {
                    DataSet ds = new DataSet();
                    ds = GetParentBranch(strBranchCode, 1);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["OfficeType"].ToString().ToUpper() == "VERTICAL OFFICE")
                        {
                            //Session["TempBranchCode"] = Session["BranchCode"];
                            //Session["TempOfficeType"] = Session["OfficeType"];
                            strBranchCode = ds.Tables[0].Rows[0]["BranchCode"].ToString();
                            strOfficeType = ds.Tables[0].Rows[0]["OfficeType"].ToString().ToUpper();
                            strBranchName = ds.Tables[0].Rows[0]["BranchName"].ToString();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "As branch not available in Call desk please contact Rgicl.Applnsupport@relianceada.com";
                        btnRegisterCall.Enabled = false;
                    }
                }


                //vo end
                //if (LargeMarketHideShow(Convert.ToInt32(ddlIssueRequestSubType.SelectedValue)) && (IsLargeMarket(Session["OfficeType"].ToString().ToUpper()) || Session["OfficeType"].ToString().ToUpper() == "SERVICE CENTER"))
                //if (LargeMarketHideShow(Convert.ToInt32(ddlIssueRequestSubType.SelectedValue)) && (IsLargeMarket(strOfficeType) || strOfficeType == "SERVICE CENTER"))
                //{

                //    tbllargemarket.Visible = true;
                //    BindChannel();

                //    //if (IsLargeMarket(Session["OfficeType"].ToString().ToUpper()))
                //    if (IsLargeMarket(strOfficeType))
                //    {

                //        BindBranch(Session["OfficeType"].ToString().ToUpper(), Session["BranchCode"].ToString());
                //        //BindBranch(strOfficeType, strBranchCode);
                //        ListItem oListBranchItem = ddlbranchname.Items.FindByText(Session["BranchName"].ToString());
                //        //ListItem oListBranchItem = ddlbranchname.Items.FindByText(strBranchName);
                //        if (oListBranchItem != null)
                //            ddlbranchname.SelectedValue = oListBranchItem.Value.ToString();
                //        if (ddlbranchname.SelectedIndex > 0)
                //            ViewState["BranchID"] = ddlbranchname.SelectedItem.Value.ToString();
                //        if (Session["EmployeeFunction"].ToString() == "Sales Manager")
                //        {
                //            ListItem oListChannelItem = ddlchannel.Items.FindByText(Session["SMChannel"].ToString());
                //            if (oListChannelItem != null)
                //                ddlchannel.SelectedValue = oListChannelItem.Value.ToString();
                //            ddlchannelselectedindexchanged(sender, e);
                //            ddlbranchname.Enabled = false;
                //            ddlchannel.Enabled = false;
                //            ddlsm.Enabled = false;
                //        }
                //    }
                //    else
                //        BindBranch("SERVICE CENTER", Session["BranchCode"].ToString());
                //    //BindBranch("SERVICE CENTER", strBranchCode);


                //}

                //[CR-07] Large Market start commet by JD
                //if (LargeMarketHideShowNew(Convert.ToInt32(ddlIssueRequestSubType.SelectedValue), Membership.GetUser().UserName))
                //{
                //    BindChannelNew(Membership.GetUser().UserName);
                //    tbllargemarket.Visible = true;
                //} 
                //[CR-07] Large Market end

                //[CR-22-2-2022]  New CR JD
                if (ChannelMarketHideShowNew(Convert.ToInt32(ddlIssueRequestSubType.SelectedValue)))
                {
                    BindChannelMarket(Membership.GetUser().UserName);

                    BindChannelMarket_Prepopulated(Membership.GetUser().UserName, strBranchCode, null);

                    PnlChMr.Visible = true;
                    tbllargemarket.Visible = false;
                }
                else
                {
                    PnlChMr.Visible = false;
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
                    //hfRemark.Value = strComments;
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

            ///[CR-02] Added for Smart Zone User ID Creation ---START
            string strsubrequest = "";
            if (ddlIssueRequestType.SelectedItem.Text.ToLower().ToString() == "userid creation" && ddlApplicationType.SelectedItem.Text.ToLower().ToString() == "smartzone")
            {
                if (ddlIssueRequestSubType.SelectedItem.Text.ToLower().ToString() == "bsm id creation")
                {
                    strsubrequest = ddlIssueRequestSubType.SelectedItem.Text.ToString();
                }
                else
                {

                    strsubrequest = "-";
                }
            }
            else
            {
                strsubrequest = ddlIssueRequestSubType.SelectedItem.Text.ToString();
            }

            ///[CR-02] Added for Smart Zone User ID Creation ---END
            dsDescription = GetDescription(ddlApplicationType.SelectedItem.Text.ToString(), ddlIssueRequestType.SelectedItem.Text.ToString(), strsubrequest);
            if (dsDescription.Tables[0].Rows.Count > 0)
            {

                if (strsubrequest == "-")
                {
                    lblVerifyDesc.Text = dsDescription.Tables[0].Rows[0]["Description"].ToString();
                    popup_Verify.Show();
                    popupconfirm.Hide();
                }
                else
                {
                    lbldesc.Text = dsDescription.Tables[0].Rows[0]["Description"].ToString();
                    popup_Verify.Hide();
                    popupconfirm.Show();
                }

            }
            else
            {
                popup_Verify.Hide();
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
        lblMessage.Text = null;
        RegisterNewCallBAL objRNCBAL = new RegisterNewCallBAL();
        try
        {
            int intTAT = 0;
            string UploadSavePath = ConfigurationManager.AppSettings["UploadSavePath"].ToString();
            string sUserRemark, sApproverEmail, sApproverDesignation, sApproverName, sUserMail, sContactNumber, sApplication
                , sApproverID, sSApproverID, sSApproverEmail, sSApproverDesignation, sSApproverName, sReopenID, sBranchName, sCallDate, sUploadPath,
                sUserName, sCallType, sTypeofIR, sTicketNumber, sChannel, sIRSubType, sPriority,
                sGroups, sCallTAT, sAppSupportPerformer, sUserDesignation, sTicketValue, sServiceCenterName, sGroupType, scRegionID, scZoneID, sApproverTAT, sAppSupportTAT;

            //CR Claim Number Validation moter claims Start
            if (ddlApplicationType.SelectedItem.Text == "Motor Claims")
            {
                if (!string.IsNullOrEmpty(txtProposalNo.Text))
                {
                    if (txtProposalNo.Text.Length != 10 || !txtProposalNo.Text.StartsWith("3"))
                    {
                        lblMessage.Text = "Invalid Claim number";
                        return;
                    }
                }
                else
                {
                    lblMessage.Text = "Please enter claim number.";
                    return;
                }
            }
            //CR Claim Number Validation moter claims End

            //[CR-34] - Proposal No field add -Start
            string strProposalNo = string.Empty;
            strProposalNo = txtProposalNo.Text;
            //[CR-34] - Proposal No field add -End

            //[CR-1] CQR CR Add Fields 06052019---START
            string strTotalInsured = string.Empty;
            string strInsurerName = string.Empty;
            string strOccupancy = string.Empty;
            string strIMDCode = string.Empty;
            string strAgentCode = string.Empty;

            if (!string.IsNullOrEmpty(txttotalsuminsured.Text))
            {
                strTotalInsured = txttotalsuminsured.Text;
            }

            if (!string.IsNullOrEmpty(txtinsurename.Text))
            {
                strInsurerName = txtinsurename.Text;
            }

            //if (hdnoccupancy.Value)
            //{
            //    strOccupancy = ddloccupany.SelectedValue;
            //}

            if (!string.IsNullOrEmpty(txtimdcode.Text))
            {
                strIMDCode = txtimdcode.Text;
            }

            if (!string.IsNullOrEmpty(txtagentcode.Text))
            {
                strAgentCode = txtagentcode.Text;
            }


            if (hdnoccupancy.Value.ToLower() == "select occupany")
            {
                hdnoccupancy.Value = "";
            }
            //[CR-1] CQR CR Add Fields 06052019---END


            string strPortalIDLocked = string.Empty;
            string strPolicyNoRN = string.Empty;

            // CR- File Template validation start
            if (!string.IsNullOrEmpty(lnkFileTemplate.Text) && fuUpLoadFile.HasFile == false)
            {
                lblMessage.Text = "Please upload the file.";
                return;
            }
            // CR- File Template validation end

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


            //[CR-03] Addition of dropdown for admin start

            //objRNCBO.UserBranch = Session["LoggedBranch"].ToString();
            if (ddlApplicationType.SelectedItem.Text == "Admin")
            {
                objRNCBO.UserBranch = ddlBranch.SelectedValue;
            }
            else
            {
                objRNCBO.UserBranch = Session["LoggedBranch"].ToString();
            }

            string Vertical_Name = string.Empty;
            if (ddlApplicationType.SelectedItem.Text == "Commercial Quote Request" || ddlApplicationType.SelectedItem.Text == "Liability Quote Request")
            {
                Vertical_Name = ddlVertical_Name.SelectedItem.Text;
            }
            else
            {
                Vertical_Name = null;
            }

            string Slip_Received_Date = string.Empty, Client_Name = string.Empty, Location_Of_Client = string.Empty, Direct_Broker_Agent = string.Empty,
           DirectBrokerAgent_Name = string.Empty, Policy_Inception_Date = string.Empty, Expiry_Date = string.Empty, Expiry_TPA = string.Empty, Insurance_Company = string.Empty,
           Expiring_Broker = string.Empty, EE_Relationship = string.Empty;

            if ((ddlApplicationType.SelectedItem.Text == "GMC" || ddlApplicationType.SelectedItem.Text == "GPA"))
            {
                Slip_Received_Date = txt_slip_rdate.Text;
                Client_Name = txt_Clientname.Text;
                Location_Of_Client = txt_location_client.Text;
                Direct_Broker_Agent = txt_DirectBrokerAgent.Text;
                DirectBrokerAgent_Name = txt_DirectBrokerAgent_name.Text;
                Policy_Inception_Date = txt_policy_inception_date.Text;
                Expiry_Date = txt_expiry_date.Text;
                Expiry_TPA = txt_expiry_tpa.Text;
                Insurance_Company = txt_insurance_company.Text;
                Expiring_Broker = txt_expiring_broker.Text;
                EE_Relationship = ddlEmpRelation.SelectedItem.Text;
            }
            else
            {
                Slip_Received_Date = null;
                Client_Name = null;
                Location_Of_Client = null;
                Direct_Broker_Agent = null;
                DirectBrokerAgent_Name = null;
                Policy_Inception_Date = null;
                Expiry_Date = null;
                Expiry_TPA = null;
                Insurance_Company = null;
                Expiring_Broker = null;
                EE_Relationship = null;
            }

            objRNCBO.TicketValue = txtTicketValue.Text.Trim() == string.Empty ? 0 : System.Convert.ToDecimal(txtTicketValue.Text.Trim());

            //new code CR-25-03-2022 JD

            string strChannelName = string.Empty;
            if (ddlchannelnew.SelectedIndex > 0)
            {
                strChannelName = ddlchannelnew.SelectedValue;
            }
            else
            {
                strChannelName = null;
            }

            string strMarketName = string.Empty;
            if (ddlmarket.SelectedIndex > 0)
            {
                strMarketName = ddlmarket.SelectedValue;
            }
            else
            {
                strMarketName = null;
            }

            DataSet ds_Approver = new DataSet();

            string FirstApprover = string.Empty, SecondApprover = string.Empty, TicketType = string.Empty, Closure_Group = string.Empty;

            ds_Approver = GetApproversDataByAIRSMID(objRNCBO.IssueRequestSubTypeID, objRNCBO.UserName, objRNCBO.UserBranch, strChannelName, strMarketName);
            if (ds_Approver.Tables[0].Rows.Count > 0)
            {
                int err_code = Convert.ToInt32(ds_Approver.Tables[0].Rows[0]["err_code"]);
                if (err_code == 1)
                {
                    lblMessage.Text = Convert.ToString(ds_Approver.Tables[0].Rows[0]["error_msg"]);
                    return;
                }
                else if (err_code == 0)
                {
                    TicketType = Convert.ToString(ds_Approver.Tables[0].Rows[0]["TicketType"]);
                    FirstApprover = Convert.ToString(ds_Approver.Tables[0].Rows[0]["FirstApprover"]);
                    SecondApprover = Convert.ToString(ds_Approver.Tables[0].Rows[0]["SecondApprover"]);
                    Closure_Group = Convert.ToString(ds_Approver.Tables[0].Rows[0]["Closure_Group"]);
                }
                //else
                //{
                //    lblMessage.Text = "Please contact Administrator.";
                //    return;
                //}
            }

            //new code CR-25-03-2022 END

            //s  string strN = objRNCBO.TicketNumber = objRNCBAL.GetTicketNumber();
            string strN = objRNCBO.TicketNumber = GetTicketNumberByBranchCode(objRNCBO.UserBranch.ToString());


            if (strN == "0")
            {
                lblMessage.Text = "Ticket could not be generated";
                return;
            }

            //if (fuUpLoadFile.HasFile == true && fuUpLoadFile.PostedFile != null)
            if (fuUpLoadFile.PostedFile.FileName.Length > 0)
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

                    // [CR-18] Vulnaribility file extension check Start  
                    string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "txt", "jpeg", "zip", "rar" };
                    string ext = System.IO.Path.GetExtension(fuUpLoadFile.PostedFile.FileName);
                    bool isValidFile = false;

                    for (int i = 0; i < validFileTypes.Length; i++)
                    {
                        if (ext == "." + validFileTypes[i])
                        {
                            isValidFile = true;
                            break;
                        }
                    }
                    if (!isValidFile)
                    {

                        lblMessage.Text = "Invalid File. Please upload a File with extension " +
                                       string.Join(",", validFileTypes);
                        return;
                    }

                    if (fileName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) != -1)
                    {
                        lblMessage.Text = "The filename is invalid";
                        return;
                    }

                    string strFileNameWithoutExtension = fileName.Remove(fileName.LastIndexOf("."));
                    if (strFileNameWithoutExtension.Contains(".") == true)
                    {
                        lblMessage.Text = "In the filename dot is not allowed";
                        return;
                    }

                    // [CR-18] Vulnaribility File extension check End 

                    fuUpLoadFile.PostedFile.SaveAs(savePath);
                    objRNCBO.UploadFile = UploadSavePath + strN + "_" + fileName;
                }
            }

            //[CR-07] Large Market START Commet by JD
            //string strChannelName;

            //if (ddlchannel.SelectedIndex > 0)
            //{
            //    strChannelName = ddlchannel.SelectedValue;
            //}
            //else
            //{
            //    strChannelName = null;
            //}
            //[CR-07] Large Market END

            //strReturnVal = objRNCBAL.AddCallDetails(objRNCBO.IssueRequestSubTypeID, objRNCBO.UserName, objRNCBO.ContactNumber, objRNCBO.UserRemark, objRNCBO.UploadFile, objRNCBO.UserBranch, objRNCBO.TicketNumber, objRNCBO.TicketValue, strPortalIDLocked, strPolicyNoRN, Convert.ToInt32(ViewState["SMID"]), Convert.ToInt32(ViewState["BranchID"])/*[CR-06] start*/, rdoProcessing.SelectedValue/*[CR-06] end*/ /*[CR-07] start*/, strChannelName/*[CR-07] end*//*[CR-34] start*/, strProposalNo/*[CR-34] end*/);

           // strReturnVal = objRNCBAL.AddCallDetails(objRNCBO.IssueRequestSubTypeID, objRNCBO.UserName, objRNCBO.ContactNumber, objRNCBO.UserRemark, objRNCBO.UploadFile, objRNCBO.UserBranch, objRNCBO.TicketNumber, objRNCBO.TicketValue, strPortalIDLocked, strPolicyNoRN, Convert.ToInt32(ViewState["SMID"]), Convert.ToInt32(ViewState["BranchID"])/*[CR-06] start*/, rdoProcessing.SelectedValue/*[CR-06] end*/ /*[CR-07] start*/, strChannelName/*[CR-07] end*//*[CR-34] start*/, strProposalNo/*[CR-34] end*//*[CR-1] start*/, strTotalInsured, strInsurerName, hdnoccupancy.Value, strIMDCode, strAgentCode, hdnimdcode.Value/*[CR-1] end*/);
            //if (txtProposalNo12.Text.ToString() == "")
            //{
            //    strReturnVal = objRNCBAL.AddCallDetails(objRNCBO.IssueRequestSubTypeID, objRNCBO.UserName, objRNCBO.ContactNumber, objRNCBO.UserRemark, objRNCBO.UploadFile, objRNCBO.UserBranch, objRNCBO.TicketNumber, objRNCBO.TicketValue, strPortalIDLocked, strPolicyNoRN, Convert.ToInt32(ViewState["SMID"]), Convert.ToInt32(ViewState["BranchID"])/*[CR-06] start*/, rdoProcessing.SelectedValue/*[CR-06] end*/ /*[CR-07] start*/, strChannelName/*[CR-07] end*//*[CR-34] start*/, strProposalNo/*[CR-34] end*//*[CR-1] start*/, strTotalInsured, strInsurerName, hdnoccupancy.Value, strIMDCode, strAgentCode, hdnimdcode.Value/*[CR-1] end*/,FirstApprover, SecondApprover);
            //}
            //else
            //{
            //    strReturnVal = objRNCBAL.AddCallDetails(objRNCBO.IssueRequestSubTypeID, objRNCBO.UserName, objRNCBO.ContactNumber, objRNCBO.UserRemark, objRNCBO.UploadFile, objRNCBO.UserBranch, objRNCBO.TicketNumber, objRNCBO.TicketValue, strPortalIDLocked, strPolicyNoRN, Convert.ToInt32(ViewState["SMID"]), Convert.ToInt32(ViewState["BranchID"])/*[CR-06] start*/, rdoProcessing.SelectedValue/*[CR-06] end*/ /*[CR-07] start*/, strChannelName/*[CR-07] end*//*[CR-34] start*/, txtProposalNo12.Text.ToString()/*[CR-34] end*//*[CR-1] start*/, strTotalInsured, strInsurerName, hdnoccupancy.Value, strIMDCode, strAgentCode, hdnimdcode.Value/*[CR-1] end*/, FirstApprover, SecondApprover);
            //}
            if (txtProposalNo12.Text.ToString() == "")
            {
                strReturnVal = AddCallDetails(objRNCBO.IssueRequestSubTypeID, objRNCBO.UserName, objRNCBO.ContactNumber, objRNCBO.UserRemark, objRNCBO.UploadFile, objRNCBO.UserBranch, objRNCBO.TicketNumber, objRNCBO.TicketValue, strPortalIDLocked, strPolicyNoRN, Convert.ToInt32(ViewState["SMID"]), Convert.ToInt32(ViewState["BranchID"])/*[CR-06] start*/, rdoProcessing.SelectedValue/*[CR-06] end*/ /*[CR-07] start*/, strChannelName/*[CR-07] end*//*[CR-34] start*/, strProposalNo/*[CR-34] end*//*[CR-1] start*/, strTotalInsured, strInsurerName, hdnoccupancy.Value, strIMDCode, strAgentCode, hdnimdcode.Value/*[CR-1] end*/, FirstApprover, SecondApprover, strMarketName, Slip_Received_Date, Client_Name, Location_Of_Client, Direct_Broker_Agent, DirectBrokerAgent_Name, Policy_Inception_Date, Expiry_Date, Expiry_TPA, Insurance_Company, Expiring_Broker, EE_Relationship, Vertical_Name);
            }
            else
            {
                strReturnVal = AddCallDetails(objRNCBO.IssueRequestSubTypeID, objRNCBO.UserName, objRNCBO.ContactNumber, objRNCBO.UserRemark, objRNCBO.UploadFile, objRNCBO.UserBranch, objRNCBO.TicketNumber, objRNCBO.TicketValue, strPortalIDLocked, strPolicyNoRN, Convert.ToInt32(ViewState["SMID"]), Convert.ToInt32(ViewState["BranchID"])/*[CR-06] start*/, rdoProcessing.SelectedValue/*[CR-06] end*/ /*[CR-07] start*/, strChannelName/*[CR-07] end*//*[CR-34] start*/, txtProposalNo12.Text.ToString()/*[CR-34] end*//*[CR-1] start*/, strTotalInsured, strInsurerName, hdnoccupancy.Value, strIMDCode, strAgentCode, hdnimdcode.Value/*[CR-1] end*/, FirstApprover, SecondApprover, strMarketName, Slip_Received_Date, Client_Name, Location_Of_Client, Direct_Broker_Agent, DirectBrokerAgent_Name, Policy_Inception_Date, Expiry_Date, Expiry_TPA, Insurance_Company, Expiring_Broker, EE_Relationship, Vertical_Name);
            }

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

            //[CR-03] Addition of dropdown for admin start

            //string strLoggedBranch = Convert.ToString(Session["LoggedBranch"]);
            string strLoggedBranch = objRNCBO.UserBranch;

            //[CR-03] Addition of dropdown for admin end

            dsCallDetails = objTCBAL.GetCallDetailsByTicketNumber(strReturnVal, strLoggedBranch, sUserName);

            //[CR-09] Group Mapping Start
            int AIRSMID_FK = Convert.ToInt32(dsCallDetails.Tables[0].Rows[0]["AIRSMID_FK"]);
            //[CR-09] Group Mapping End

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
                // [I-01] ZAM Issue Start
                DataSet dsZAM = new DataSet();
                dsZAM = GetZAMCode(strLoggedBranch);
                if (dsZAM != null && dsZAM.Tables[0] != null)
                {
                    if (dsZAM.Tables[0].Rows.Count >= 1)
                    {
                        scRegionID = dsZAM.Tables[0].Rows[0]["regionid"].ToString();
                        //scRegionID = dsZAM.Tables[0].Rows[0]["RegionName"].ToString();
                    }
                }
                // [I-01] ZAM Issue End
                //sGroups = sGroups + scRegionID;
                sGroups = "TECHDESK_" + sGroups + "_" + scRegionID;
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

            if (sGroups == "OPSKit_HUB")
            {
                if (sServiceCenterName == "Okhla Hub")
                {
                    sGroups = "OPSKit_HUB_OKHLA";
                }
                else if (sServiceCenterName == "Indore Hub")
                {
                    sGroups = "OPSKit_HUB_INDORE";
                }
                else if (sServiceCenterName == "")
                {
                    sGroups = "OPSKit_HUB_BRANCH";
                }

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

            //[I-02] Chandigarh branch closure change Start
            if (sApplication == "KIT" && sTypeofIR == "Mapping / Remapping" && sIRSubType == "Covernote Book Re-mapping" && strLoggedBranch == "2004")
            {
                sGroups = "OPSKit_Chandigarh";
            }
            //[I-02] Chandigarh branch closure change End

            //[CR-09] Group Mapping Start

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

            //[CR-09] Group Mapping End

            //[CR-22] Multi Group Approval Start

            DataSet dsGroupMapped = new DataSet();
            dsGroupMapped = GetGroupMapped(AIRSMID_FK, strLoggedBranch, strCallCreatedBy);
            if (dsGroupMapped != null && dsGroupMapped.Tables[0] != null)
            {
                if (dsGroupMapped.Tables[0].Rows.Count >= 1)
                {
                    string GroupMapped = dsGroupMapped.Tables[0].Rows[0]["GroupMapped"].ToString();

                    if (!string.IsNullOrEmpty(GroupMapped))
                    {
                        sGroups = GroupMapped;
                    }
                }
            }

            //[CR-22] Multi Group Approval End

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

            //[CR-11] Insert Group Assign start
            InsertGroupAssign(sTicketNumber, sGroups);
            //[CR-11] Insert Group Assign end

            //[CR-34] Proposal No field add Start
            strProposalNo = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ProposalNo"]);
            //[CR-34] Proposal No field add End

            //[New CR 5-4-2022] Start
            Slip_Received_Date = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Slip_Received_Date"]);
            Client_Name = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Client_Name"]);
            Location_Of_Client = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Location_Of_Client"]);
            Direct_Broker_Agent = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Direct_Broker_Agent"]);
            DirectBrokerAgent_Name = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["DirectBrokerAgent_Name"]);
            Policy_Inception_Date = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Policy_Inception_Date"]);
            Expiry_Date = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Expiry_Date"]);
            Expiry_TPA = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Expiry_TPA"]);
            Insurance_Company = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Insurance_Company"]);
            Expiring_Broker = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Expiring_Broker"]);
            EE_Relationship = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["EE_Relationship"]);
            //[New CR 5-4-2022] End

            //[New CR 003 12/05/2022] Vertical Name Start
            Vertical_Name = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Vertical_Name"]);
            //[New CR 003 12/05/2022] Vertical Name End

            string strResponseVal = objService.STARTCALLDESK(sUserRemark, sApproverEmail, sApproverDesignation, sApproverName, sUserMail, sContactNumber, sApplication, sApproverID, sReopenID, sBranchName, sCallDate, sUploadPath, sUserName, sCallType, sTypeofIR, sTicketNumber, sUserDesignation, sGroups, sIRSubType, sCallTAT, sSApproverID, sAppSupportPerformer, sSApproverDesignation, sSApproverEmail, sSApproverName, sTicketValue, "rgicl", "rgicl", "", sPriority
                //[CR-34] Proposal No field add Start
                , strProposalNo
                //[CR-34] Proposal No field add End
                //[CR-1]  CQR CR Add Fields 06052019 Start
               , strTotalInsured, strInsurerName, hdnoccupancy.Value, strIMDCode, strAgentCode, hdnimdcode.Value
                //[CR-1]  CQR CR Add Fields 06052019 EN=nd
                , Slip_Received_Date, Client_Name, Location_Of_Client, Direct_Broker_Agent, DirectBrokerAgent_Name, Policy_Inception_Date, Expiry_Date, Expiry_TPA, Insurance_Company,
                Expiring_Broker, EE_Relationship, Vertical_Name);
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
            //strSMSText = GetSMStextCallLog(sTicketNumber, Session["EmployeeName"].ToString());
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
            //if (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallType"]).ToLower() == "issue")
            //{
            //	stPopupScript.Append("var newWin = window.open('PopUpTicketDetails.aspx?TicketNo='$" + strReturnVal + "'$','PopUpWindowName','width=420,left=300,top=250,height=150,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up
            //
            //}
            //else
            //{
            //	if (string.IsNullOrEmpty(Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverID"])))
            //	{
            //	stPopupScript.Append("var newWin = window.open('PopUpTicketDetails.aspx?TicketNo='$" + strReturnVal + "'$','PopUpWindowName','width=420,left=300,top=250,height=180,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up

            //	}
            //	else
            //	{
            //		stPopupScript.Append("var newWin = window.open('PopUpTicketDetails.aspx?TicketNo='$" + strReturnVal + "'$','PopUpWindowName','width=420,left=300,top=250,height=210,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up
            //	}

            ////}

            if (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallType"]).ToLower() == "issue")
            {
                stPopupScript.Append("var newWin = setTimeout(function(){window.open('PopUpTicketDetails.aspx?TicketNo='$" + strReturnVal + "'$','PopUpTicWindowName','width=420,left=300,top=250,height=150,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');},5000);");//opens the pop up

            }
            else
            {
                if (string.IsNullOrEmpty(Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverID"])))
                {
                    stPopupScript.Append("var newWin = setTimeout(function(){window.open('PopUpTicketDetails.aspx?TicketNo='$" + strReturnVal + "'$','PopUpTicWindowName','width=420,left=300,top=250,height=180,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');},5000);");//opens the pop up

                }
                else
                {
                    stPopupScript.Append("var newWin = setTimeout(function(){window.open('PopUpTicketDetails.aspx?TicketNo='$" + strReturnVal + "'$','PopUpTicWindowName','width=420,left=300,top=250,height=210,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');},5000);");//opens the pop up
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
            sw1.Write("\n " + ex.StackTrace);
            //sw1.Write("\n " + ex.InnerException);
            //sw1.Write("\n " + strReturnVal);
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

        //[CR-03] Additional dropdown for admin start
        ddlzone.SelectedIndex = 0;
        ddlRegion.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        //[CR-03] Additional dropdown for admin end

        //[CR-34] Proposal No field add -Start
        txtProposalNo.Text = string.Empty;
        //[CR-34] Proposal No field add -End
        // [CR-1.0]Add field start 11 apr 2019
        txttotalsuminsured.Text = "";
        txtinsurename.Text = "";
        txtimdcode.Text = "";
        txtagentcode.Text = "";
        ddloccupany.SelectedIndex = 0;
        // [CR-1.0]Add field End 11 apr 2019
        // [CR JD] Start 12-04-2022
        txt_slip_rdate.Text = "";
        txt_Clientname.Text = "";
        txt_location_client.Text = "";
        txt_DirectBrokerAgent.Text = "";
        txt_DirectBrokerAgent_name.Text = "";
        txt_policy_inception_date.Text = "";
        txt_expiry_date.Text = "";
        txt_expiry_tpa.Text = "";
        txt_insurance_company.Text = "";
        txt_expiring_broker.Text = "";
        ddlEmpRelation.SelectedIndex = 0;
        pnlGMCGPA.Visible = false;
        // [CR JD] END 12-04-2022
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
            lblMessage.Text = str;
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

    #region Get Approvers data by AIRSMID and New CR JD
    public DataSet GetApproversDataByAIRSMID(params object[] param)
    {
        DataSet dsgetFSA = new DataSet();
        try
        {
            dsgetFSA = DataUtils.ExecuteDataset("usp_getApprovers", param);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return dsgetFSA;
    }

    //[CR] new Channel and Market start 22-03-2022
    public bool ChannelMarketHideShowNew(params object[] param)
    {
        DataSet ds = new DataSet();
        bool result = false;
        try
        {
            ds = DataUtils.ExecuteDataset("usp_ChannelMarketVisibilityNew", param);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "YES")
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

    public void BindChannelMarket(params object[] param)
    {
        DataSet oDSBindChanel = new DataSet();
        try
        {
            oDSBindChanel = DataUtils.ExecuteDataset("usp_GetChannelMarketNew", param);
            ddlchannelnew.DataSource = oDSBindChanel.Tables[0];
            ddlchannelnew.DataTextField = "SubChannelName";
            ddlchannelnew.DataValueField = "SubChannelName";
            ddlchannelnew.DataBind();
            ddlchannelnew.Items.Insert(0, "--Select--");

            ddlmarket.DataSource = oDSBindChanel.Tables[1];
            ddlmarket.DataTextField = "SubMarketName";
            ddlmarket.DataValueField = "SubMarketName";
            ddlmarket.DataBind();
            ddlmarket.Items.Insert(0, "--Select--");

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    public void BindChannelMarket_Prepopulated(params object[] param)
    {
        DataSet oDSBindChanelPre = new DataSet();
        try
        {
            oDSBindChanelPre = DataUtils.ExecuteDataset("usp_getEmpMarkets_prepopulated", param);
            string markettype = oDSBindChanelPre.Tables[0].Rows[0]["Market_Type"].ToString();
            string channel = oDSBindChanelPre.Tables[0].Rows[0]["Channel"].ToString();

            if (channel != "")
            {
                //ddlchannelnew.SelectedValue = channel.ToString();

                ddlchannelnew.SelectedValue = channel.ToString();
            }
            if (markettype != "")
            {
                //ddlmarket.Text = markettype;
                //if (ddlmarket.Items.FindByValue(markettype.ToString().Trim()) != null)
                //{
                //    ddlmarket.SelectedValue = markettype.ToString();
                //}
                ddlmarket.SelectedValue = markettype.ToString();
            }
            
        }
        catch (Exception ex)
        {
            lblMessage.Text = "check your Channel and Market before Call Register";
        }
    }

    #region Add Call Details
    public string AddCallDetails(params object[] param)
    {
        try
        {
            return DataUtils.ExecuteScalar(USP_ADDCALLDETAILS, param).ToString();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    private const string USP_ADDCALLDETAILS = "usp_AddCallDetails";
    #endregion
    

    //[CR] new Channel and Market end 22-03-2022

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
        //strSMS = @"Dear <"+strEmpName.ToUpper()+">, you have successfully registered a call in Reliance Call desk. Your Ticket no is  <"+strTicketNo+"> will be taken care shortly."; 
        // strSMS = @"Dear <" + strEmpName.ToUpper() + ">, Ticket no<" + strTicketNo + "> for<" + application + "> is registered successfully with RGICL and will be taken care shortly."; 
        DataSet ds = new DataSet();
        ds = GetSMSTEXT(strTicketNo, strEmpName);
        if (ds.Tables[0].Rows.Count > 0)
        {

            strSMS = ds.Tables[0].Rows[0]["SMSTEXT"].ToString();

        }

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

    //[CR-07] Large market Start
    #region Get and Bind Branch For Large market
    //public void BindBranch(params object[] param)
    //{
    //    DataSet oDSBindBranch = new DataSet();
    //    try
    //    {
    //        oDSBindBranch = DataUtils.ExecuteDataset("usp_GetBranchForLargeMarket", param);
    //        ddlbranchname.DataSource = oDSBindBranch.Tables[0];
    //        ddlbranchname.DataTextField = "BranchName";
    //        ddlbranchname.DataValueField = "BranchID_PK";
    //        ddlbranchname.DataBind();
    //        ddlbranchname.Items.Insert(0, "--Select--");
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //    }

    //}
    #endregion
    //[CR-07] Large Market End

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

    //[CR-07] Large Market Start
    public void BindChannelNew(params object[] param)
    {
        DataSet oDSBindChanel = new DataSet();
        try
        {
            oDSBindChanel = DataUtils.ExecuteDataset("usp_GetChannelforLargeMarketNew", param);
            ddlchannel.DataSource = oDSBindChanel.Tables[0];
            ddlchannel.DataTextField = "ChannelName";
            ddlchannel.DataValueField = "ChannelName";
            ddlchannel.DataBind();
            ddlchannel.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

    }
    //[CR-07] Large Market End

    #endregion

    //[CR-07] Large Market Start
    #region Get and Bind SM For Large market
    //public void BindSM(params object[] param)
    //{
    //    DataSet oDSBindSM = new DataSet();
    //    try
    //    {
    //        oDSBindSM = DataUtils.ExecuteDataset("usp_GetSMforLargeMarket", param);
    //        ddlsm.DataSource = oDSBindSM.Tables[0];
    //        ddlsm.DataTextField = "Employee_Name";
    //        ddlsm.DataValueField = "Employee_ID_PK";
    //        ddlsm.DataBind();
    //        ddlsm.Items.Insert(0, "--Select--");
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //    }

    //}
    #endregion
    //[CR-07] Large Market End

    #region large market hide show

    public bool LargeMarketHideShow(params object[] param)
    {
        DataSet oDSHideShow = new DataSet();
        bool result = false;
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

    //[CR-07] Large Market start
    public bool LargeMarketHideShowNew(params object[] param)
    {
        DataSet oDSHideShow = new DataSet();
        bool result = false;
        try
        {
            oDSHideShow = DataUtils.ExecuteDataset("usp_LargeMarketVisibility", param);
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
    //[CR-07] Large Market end

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
            //[CR-07] Large Market Start            
            //if ((ddlbranchname.SelectedIndex > 0) && (ddlchannel.SelectedIndex > 0))
            //{
            //    BindSM(Convert.ToInt32(ddlbranchname.SelectedItem.Value), Convert.ToInt32(ddlchannel.SelectedItem.Value));
            //    ListItem oListSMItem = ddlsm.Items.FindByText(Session["EmployeeName"].ToString());
            //    if (oListSMItem != null)
            //        ddlsm.SelectedValue = oListSMItem.Value.ToString();
            //    if (ddlsm.SelectedIndex > 0)
            //        //SMID = Convert.ToInt32(ddlsm.SelectedItem.Value);
            //        ViewState["SMID"] = ddlsm.SelectedItem.Value.ToString();
            //}

            //CR-07] Large Market ENd
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    //[CR-07] Large Market Start
    //protected void ddlbranchnameselectedindexchanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ddlchannel.SelectedIndex = 0;
    //        if (ddlbranchname.SelectedIndex > 0)
    //            //BranchID = Convert.ToInt32(ddlbranchname.SelectedItem.Value);
    //            ViewState["BranchID"] = ddlbranchname.SelectedItem.Value.ToString();
    //        // ddlsm.SelectedIndex = 0;
    //        BindSM(0, 0);

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //    }
    //}
    //[CR-07] Large Market End

    //[CR-07] Large Market start
    //protected void ddlsmselectedindexchanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlsm.SelectedIndex > 0)
    //            //SMID = Convert.ToInt32(ddlsm.SelectedItem.Value);
    //            ViewState["SMID"] = ddlsm.SelectedItem.Value.ToString();

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //    }
    //}
    //[CR-07] Large Market end

    #region Get VirtualParentBranch
    public DataSet GetParentBranch(params object[] param)
    {
        DataSet oDSParentBranch = new DataSet();
        try
        {
            oDSParentBranch = DataUtils.ExecuteDataset("sp_GetVirtualParentBranch", param);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return oDSParentBranch;
    }
    #endregion

    #region Get SMSTEXT

    public DataSet GetSMSTEXT(params object[] param)
    {
        DataSet oDSParentBranch = new DataSet();
        try
        {
            oDSParentBranch = DataUtils.ExecuteDataset("USP_GetSMStextCallLog", param);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return oDSParentBranch;
    }

    #endregion

    #region Get ApplicationType By Branch
    public DataSet GetApplicationTypeByBranch(params object[] param)
    {
        DataSet oDSApplicationTypes = new DataSet();
        try
        {

            oDSApplicationTypes = DataUtils.ExecuteDataset("usp_GetApplicationTypeByBranch_Processing", param);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return oDSApplicationTypes;
    }
    #endregion

    #region Bind Applications
    public void BindApplications()
    {
        string strBranch = Convert.ToString(Session["LoggedBranch"]);
        string strAppType = Convert.ToString(Session["AppType"]);
        string strItType = rdoProcessing.SelectedValue; //Convert.ToString("1");
        DataSet oDSApp = GetApplicationTypeByBranch(strBranch, strAppType, strItType);
        DataTable oDTApp = oDSApp.Tables[0];
        ddlApplicationType.Items.Clear();
        ddlApplicationType.DataSource = oDTApp;
        ddlApplicationType.DataTextField = "ApplicationName";
        ddlApplicationType.DataValueField = "ApplicationID_PK";
        ddlApplicationType.DataBind();
        ddlApplicationType.Items.Insert(0, new ListItem("All Applications", "0"));
    }
    #endregion

    protected void rdoProcessing_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["ProcessGroup"] = rdoProcessing.SelectedValue; // "1";
        BindApplications();
    }

    // [I-01] ZAM Issue Start
    public DataSet GetZAMCode(string branchcode)
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
            cmd.CommandText = "USP_GETZAMCODE";
            cmd.Parameters.AddWithValue("@branchcode", branchcode);
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

    // [I-01] ZAM Issue End

    // [CR-03] Additional dropdown for admin start

    public DataTable GetZone()
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
            cmd.CommandText = "usp_GetZones";

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

    public DataTable GetRegion(int ZoneId)
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
            cmd.CommandText = "usp_GetRegion";
            cmd.Parameters.AddWithValue("@ZoneID_PK", ZoneId);
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

    public void BindZone()
    {
        DataTable dtZone = new DataTable();

        dtZone = GetZone();
        ddlzone.Items.Clear();
        ddlzone.DataSource = dtZone;
        ddlzone.DataTextField = "ZoneName";
        ddlzone.DataValueField = "ZoneID_PK";
        ddlzone.DataBind();
        ddlzone.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    #region New CR [CR 3] Vertical Name by JD
    public void BindVertical_Name(int select_Applicationid)
    {
        DataTable dtVertical_Name = new DataTable();

        dtVertical_Name = GetVertical_Name(select_Applicationid);
        ddlVertical_Name.Items.Clear();
        ddlVertical_Name.DataSource = dtVertical_Name;
        ddlVertical_Name.DataTextField = "vertical_name";
        ddlVertical_Name.DataValueField = "id";
        ddlVertical_Name.DataBind();
        ddlVertical_Name.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public DataTable GetVertical_Name(int select_Applicationid)
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
            cmd.CommandText = "usp_GetVertical_Name";
            cmd.Parameters.AddWithValue("@Applicationid", select_Applicationid);
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
    #endregion

    public void BindRegion(int ZoneId)
    {
        DataTable dtRegion = new DataTable();

        dtRegion = GetRegion(ZoneId);
        ddlRegion.Items.Clear();
        ddlRegion.DataSource = dtRegion;
        ddlRegion.DataTextField = "RegionName";
        ddlRegion.DataValueField = "RegionCode";
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("--Select--", "0"));
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

    protected void ddlApplicationType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlApplicationType.SelectedItem.Text == "Admin")
        {
            BindZone();
            pnlAdmin.Visible = true;
            lblRemark.Text = "Call Ticket will be raised against selected branch";
            lblRemark.ForeColor = System.Drawing.Color.Blue;
            lblRemark.Font.Bold = true;
        }
        else if (ddlApplicationType.SelectedItem.Text == "GMC" || ddlApplicationType.SelectedItem.Text == "GPA")
        {
            pnlGMCGPA.Visible = true;
            pnlAdmin.Visible = false;
            upCommercial_Liability.Visible = false;
        }
        else if (ddlApplicationType.SelectedItem.Text == "Commercial Quote Request" || ddlApplicationType.SelectedItem.Text == "Liability Quote Request")
        {
            BindVertical_Name(Convert.ToInt32(ddlApplicationType.SelectedValue));
            upCommercial_Liability.Visible = true;
            pnlAdmin.Visible = false;
            pnlGMCGPA.Visible = false;
        }
        else
        {
            pnlAdmin.Visible = false;
            pnlGMCGPA.Visible = false;
            upCommercial_Liability.Visible = false;
            lblRemark.Text = "";
        }
    }

    protected void ddlzone_SelectedIndexChanged(object sender, EventArgs e)
    {
        int zoneid = Convert.ToInt32(ddlzone.SelectedValue);
        BindRegion(zoneid);
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        int RegionId = Convert.ToInt32(ddlRegion.SelectedValue);
        BindBranch(RegionId);
    }

    // [CR-03] Additional dropdown for admin End

    //[CR-09] Group Mapping Start
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
            return null;
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
    //[CR-09] Group Mapping End

    //[CR-11] Insert Group Assign Start
    public DataSet InsertGroupAssign(string TicketNo, string sGroup)
    {
        DataSet oDSApplicationTypes = new DataSet();
        try
        {
            SqlParameter[] Params = 
			{   
                new SqlParameter("@TicketNo", TicketNo),
				new SqlParameter("@sGroup", sGroup)	
			};
            oDSApplicationTypes = DataUtils.ExecuteDataset("usp_InsertGroupAssign", Params);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return oDSApplicationTypes;
    }
    //[CR-11] Insert Group Assign End

    //[CR-22] Multi Group Approval Start
    public DataSet GetGroupMapped(int AIRSMID, string BranchCode, string UserId)
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
            cmd.CommandText = "usp_GetGroupMapped";
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
    //[CR-22] Multi Group Approval End

    protected void btnnoVerify_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "rsz", "fnRedirectSZ();", true);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
}
