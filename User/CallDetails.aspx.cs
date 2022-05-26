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
using CallDeskDAL;
using TechDeskUtil;
using System.Data.SqlClient;

public partial class User_CallDetails : System.Web.UI.Page
{
    #region Page Load Event

    protected void Page_Load(object sender, EventArgs e)
    {
        //AntiforgeryChecker.Check(this, antiforgery);
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
        string strTicketNumber = null;
        try
        {
            strTicketNumber = Convert.ToString(Session["TicketNumber"]);


            string strLoggedBranch = Convert.ToString(Session["LoggedBranch"]);
            string strUserName = Membership.GetUser().UserName;
            string strOfficeType = Session["OfficeType"].ToString().ToUpper();
            string strBranchCode = Session["BranchCode"].ToString();
            TrackCallBAL objTCBAL = new TrackCallBAL();
            DataSet dsCallDetails = new DataSet();
            DataSet dsApproverFiles = new DataSet();
            DataTable dtUserFiles = new DataTable();
            TechDeskService.TechDeskService objTDS = new TechDeskService.TechDeskService();
            dsCallDetails = objTCBAL.GetCallDetailsByTicketNumberForAdmin(strTicketNumber);
            bool IsAdmin = Roles.IsUserInRole("Admin");
            Session["CallDetailsData"] = dsCallDetails;

            lblTicketNumber.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["TicketNumberPK"]);
            lblApplicationType.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApplicationName"]);
            lblIssueRequsetType.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["IssueRequestType"]);
            lblIssueRequsetSubType.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["IssueRequestSubType"]);
            lblCallDate.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallDate"]).ToString();
            lblCallStatus.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]);
            lblTicketValue.Text = dsCallDetails.Tables[0].Rows[0]["TicketValue"].ToString() == "" ? "0.00" : Convert.ToString(dsCallDetails.Tables[0].Rows[0]["TicketValue"]);
            lblCallLoggedUser.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallCreatedBy"]);

            //[CR-07] Large Market Start
            string strChannel = string.Empty;
            strChannel = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ChannelName"]);
            //[CR-07] Large Market End

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
            lblFirstcontact.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverContactno"]);

            lblSecondApproverName.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverName"]);
            lblSecondApproverDesignation.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverDesignation"]);
            lblSecondApproverEMail.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverMail"]);
            lblSecondcontact.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverContactno"]);

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

            //[CR-01] Service desk application change Start
            lblUserContactNo.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ContactNumber"]);
            lblCallLoggedBy.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UserName"]);
            //[CR-01] Service desk application change End

            TechDeskUtilService obj = new TechDeskUtilService();

            //[CR-09] Calldesk Group Creation Start
            //lblTicketProGroup.Text = obj.getGrpMembers(lblTicketProGroup.Text);
            string strTeamName = objTDS.getTeamName(strTicketNumber);
            lblTicketProGroup.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Groups"]) + " " + obj.getGrpMembers(strTeamName);
            //[CR-09] Calldesk Group Creation End

            //[CR-34] Proposal No add Start
            lblProposalNo.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ProposalNo"]);
            // [CR-34] Proposal No add End


            if ((Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]) == "Open") || (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]) == "In Progress") || (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]) == "Resend to L1") || (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]) == "Forward to L2") || (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]) == "Forward to L3"))
            {
                lblperformer.Text = objTDS.getPerformer(strTicketNumber, "AppSupport");
                //lblPerformerContact.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["PerformerContact"]);
            }
            else
            {
                string AppSuportPerformerName = dsCallDetails.Tables[0].Rows[0]["AppSuportPerformerName"].ToString();
                if (AppSuportPerformerName == null || AppSuportPerformerName == "")
                {
                    lblperformer.Text = dsCallDetails.Tables[0].Rows[0]["AppSuportPerformer"].ToString();
                }
                else
                {
                    lblperformer.Text = AppSuportPerformerName;
                }
                //lblperformer.Text = dsCallDetails.Tables[0].Rows[0]["AppSuportPerformer"].ToString();
                //lblPerformerContact.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["PerformerContact"]);
            }
            lblclosurecategory.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Category"]);
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

                //----------------------Added for resigning issue calls on 07-07-2016----------------------//

                trReassign.Visible = false;
                btnEdit.Visible = false;

                //----------------------Added for resigning issue calls on 07-07-2016----------------------//

                TrackCallBAL objBAL = new TrackCallBAL();
                DataSet dsApprover = objBAL.GetReopenCallApprover(strTicketNumber);
                if (dsApprover.Tables.Count > 0)
                {
                    lblApproverName.Text = Convert.ToString(dsApprover.Tables[0].Rows[0]["ApproverName"]);
                    lblApproverEMail.Text = Convert.ToString(dsApprover.Tables[0].Rows[0]["ApproverMail"]);
                    lblApproverDesignation.Text = Convert.ToString(dsApprover.Tables[0].Rows[0]["ApproverDesignation"]);
                    lblFirstcontact.Text = Convert.ToString(dsApprover.Tables[0].Rows[0]["ApproverContactno"]);

                    lblSecondApproverName.Text = Convert.ToString(dsApprover.Tables[0].Rows[0]["SApproverName"]);
                    lblSecondApproverDesignation.Text = Convert.ToString(dsApprover.Tables[0].Rows[0]["SApproverDesignation"]);
                    lblSecondApproverEMail.Text = Convert.ToString(dsApprover.Tables[0].Rows[0]["SApproverMail"]);
                    lblSecondcontact.Text = Convert.ToString(dsApprover.Tables[0].Rows[0]["SApproverContactno"]);
                }
            }
            else
            {
                trRequest.Visible = false;

                //----------------------Added for resigning issue calls on 07-07-2016----------------------//

                /////Check User for access to edit the details

                int IsUser = CheckUser();

                if (IsUser == 1)
                {
                    if ((Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]) == "Open") || (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]) == "In Progress"))
                    {
                        btnEdit.Visible = true;
                        trReassign.Visible = false;
                        //trReassign.Visible = true;
                    }
                    else
                    {
                        btnEdit.Visible = false;
                        trReassign.Visible = false;
                    }
                }
                else
                {
                    btnEdit.Visible = false;
                    trReassign.Visible = false;
                }

                //----------------------Added for resigning issue calls on 07-07-2016----------------------//
            }


            if (lblApproverStatus.Text.ToLower() == "rejected")
            {
                trIssue.Visible = false;
            }
            else
            {
                trIssue.Visible = true;
            }
            // [CR Anjali by JD]
            if (lblApplicationType.Text == "GMC" || lblApplicationType.Text == "GPA")
            {
                trGMC_GPA.Visible = true;

                lblslipReceivedDate.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Slip_Received_Date"]);
                lblClientName.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Client_Name"]);
                lblLocationOfClient.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Location_Of_Client"]);
                lblDirectBrokerAjent.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Direct_Broker_Agent"]);
                lblDirectBrokerAjentName.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["DirectBrokerAgent_Name"]);
                lblPolicyInceptionDate.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Policy_Inception_Date"]);
                lblExpiryDate.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Expiry_Date"]);
                lblExpiryTPA.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Expiry_TPA"]);
                lblInsuranceCompany.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Insurance_Company"]);
                lblExpiringBroker.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Expiring_Broker"]);
                lblEERelationship.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["EE_Relationship"]);
            }
            else
            {
                trGMC_GPA.Visible = false;
            }
            // [CR Anjali by JD] End

            //[CR-23] Reopen time increase Start
            if (strUserName == "C074" || strUserName == "C077" || strUserName == "C041" || strUserName == "70017693" || strUserName == "rgiadmin")
            {
                if (ReopenTime <= 720)
                {
                    ReopenTime = 70;
                }
            }
            //[CR-23] Reopen time increase End
            if (ReopenTime <= 168)
            //if ((ReopenTime <= 168) || (ApplicationReopenAllow(Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApplicationName"]), Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSupportCloseDate"]))))
            {
                if (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UserConfirmation"]) != "True")
                {
                    lblUserconfirmation.Text = "User confirmation pending";
                    if ((lblApproverStatus.Text.Equals("Approved") && lblAppSupportStatus.Text.Equals("Resolved")) || (lblApproverStatus.Text.Equals("") && lblAppSupportStatus.Text.Equals("Resolved")) || (lblApproverStatus.Text.Equals("Send Back")) && ((dsCallDetails.Tables[0].Rows[0]["CallCreatedBy"].ToString() == strUserName) || IsAdmin) || (lblAppSupportStatus.Text.Equals("Rejected") || strTeamName != "IT"))
                    {
                        trReopen.Visible = true;
                        btnReopen.Visible = true;
                        //VO changes start
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
                                }
                            }
                            else
                            {
                                lblMessage.Text = "As branch not available in Call desk please contact Rgicl.Applnsupport@relianceada.com";
                                // btnRegisterCall.Enabled = false;
                                btnReopen.Enabled = false;
                            }
                        }


                        //vo end
                        //if ((LargeMarketHideShow(dsCallDetails.Tables[0].Rows[0]["AIRSMID_FK"])) && (!string.IsNullOrEmpty(dsCallDetails.Tables[0].Rows[0]["SM_ID"].ToString())) && (IsLargeMarket(Session["OfficeType"].ToString().ToUpper()) || Session["OfficeType"].ToString().ToUpper() == "SERVICE CENTER"))
                        if (LargeMarketHideShowNew(dsCallDetails.Tables[0].Rows[0]["AIRSMID_FK"], lblCallLoggedUser.Text))
                        {
                            trlargemarket.Visible = true;
                            tbllargemarket.Visible = true;
                            BindChannel();

                            //[CR-07] Large Market Start
                            if(!string.IsNullOrEmpty(strChannel))
                            {
                                ddlchannel.SelectedItem.Text = strChannel;
                            }
                            //[CR-07] Large Market End

                            /* //if (IsLargeMarket(Session["OfficeType"].ToString().ToUpper()))
                            if (IsLargeMarket(strOfficeType))
                            {
                                //BindBranch("VERTICAL OFFICE");
                                BindBranch(Session["OfficeType"].ToString().ToUpper(), Session["BranchCode"].ToString());
                                //BindBranch(strOfficeType,strBranchCode);
                            }
                            else
                                BindBranch("SERVICE CENTER", Session["BranchCode"].ToString());
                            //BindBranch("SERVICE CENTER", strBranchCode);

                           ListItem oListBranchItem = ddlbranchname.Items.FindByValue(dsCallDetails.Tables[0].Rows[0]["Branch_ID"].ToString());
                            if (oListBranchItem != null)
                                ddlbranchname.SelectedValue = oListBranchItem.Value.ToString();
                            if (ddlbranchname.SelectedIndex > 0)
                                ViewState["BranchID"] = ddlbranchname.SelectedItem.Value.ToString();
                            ListItem oListChannelItem = ddlchannel.Items.FindByValue(dsCallDetails.Tables[0].Rows[0]["Channel_ID"].ToString());
                            if (oListChannelItem != null)
                                ddlchannel.SelectedValue = oListChannelItem.Value.ToString();
                            if ((ddlbranchname.SelectedIndex > 0) && (ddlchannel.SelectedIndex > 0))
                            {
                                BindSM(Convert.ToInt32(ddlbranchname.SelectedItem.Value), Convert.ToInt32(ddlchannel.SelectedItem.Value));
                                ListItem oListSMItem = ddlsm.Items.FindByValue(dsCallDetails.Tables[0].Rows[0]["SM_ID"].ToString());
                                if (oListSMItem != null)
                                    ddlsm.SelectedValue = oListSMItem.Value.ToString();
                                if (ddlsm.SelectedIndex > 0)

                                    ViewState["SMID"] = ddlsm.SelectedItem.Value.ToString();
                            }
                            ddlbranchname.Enabled = false;
                            ddlchannel.Enabled = false;
                            ddlsm.Enabled = false;

                            */
                        }
                        else
                        {
                            trlargemarket.Visible = false;
                            ViewState["BranchID"] = 0;
                            ViewState["SMID"] = 0;
                        }
                    }
                    else
                    {
                        lblUserconfirmation.Text = "-";
                        trReopen.Visible = false;
                        btnReopen.Visible = false;
                    }

                }
                else
                {
                    lblUserconfirmation.Text = "Confirmed by user";
                    trReopen.Visible = false;
                    btnReopen.Visible = false;
                }
            }
            else
            {
                if (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UserConfirmation"]) == "True")
                {
                    lblUserconfirmation.Text = "Confirmed by user";
                }
                else
                {
                    lblUserconfirmation.Text = "Auto confirmed";
                }
                trReopen.Visible = false;
                btnReopen.Visible = false;
            }

            //[CR-12] RCA Start
            DataSet dsRCADetails = new DataSet();
            dsRCADetails = GetRCADetails(lblTicketNumber.Text);
            if (dsRCADetails != null && dsRCADetails.Tables[0] != null)
            {
                if (dsRCADetails.Tables[0].Rows.Count != 0)
                {
                    trRCA.Visible = true;

                    lblRCADateOccured.Text = Convert.ToString(dsRCADetails.Tables[0].Rows[0]["RCA_DateOccured"]);
                    lblRCAPublishDate.Text = Convert.ToString(dsRCADetails.Tables[0].Rows[0]["RCA_PublishDate"]);
                    lblRCAType.Text = Convert.ToString(dsRCADetails.Tables[0].Rows[0]["RCA_Type"]);
                    lblRCADeploymentDate.Text = Convert.ToString(dsRCADetails.Tables[0].Rows[0]["RCA_DeploymentDate"]);
                    lblTeamMemberInvolved.Text = Convert.ToString(dsRCADetails.Tables[0].Rows[0]["RCA_TeamMembers"]);
                    lblRCADetails.Text = Convert.ToString(dsRCADetails.Tables[0].Rows[0]["RCA_Details"]);
                }

                else
                {
                    trRCA.Visible = false;
                }

            }
            else
            {
                trRCA.Visible = false;
            }

            //added by ##shilpa on 14jan2020
            //purpose to hide the reopen module when call is already Opened or inprogress

           //if   ((Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]) == "Open") || (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]) == "In Progress") || (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]) == "Resend to L1") || (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]) == "Forward to L2") || (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallStatus"]) == "Forward to L3"))
           //{
                //trReopen.Visible = true;
                //btnReopen.Visible = true;
            //}
            //else
            //{
                //trReopen.Visible = false;
                //btnReopen.Visible = false;
            //}


            //[CR-12] RCA End
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            FileStream fs1 = new FileStream(Server.MapPath("..\\Files") + "\\" + "CustomError.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw1 = new StreamWriter(fs1);
            sw1.Write("\r\n =======================================================================================");
            sw1.Write("\r\n Log Entry On : " + System.DateTime.Now);
            sw1.Write("\n " + ex.Message);
            //sw1.Write("\n " + ex.InnerException);
            sw1.Write("\n " + strTicketNumber);
            sw1.Write("\n " + ex.StackTrace.ToString());
            sw1.Close();
            fs1.Close();
            //if (lblMessage.Text == "Object reference not set to an instance of an object.")
            // {
            //  PopupCloseFunction();

            //Response.Redirect("~/TrackCall.aspx");
            // }
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
                    sUploadPath = null, sUserName, sCallType, sTypeofIR, sTicketNumber, sChannel, sIRSubType,
                    sGroups, sPriority, sCallTAT, sOldRemark, sAppSupportPerformer, sTicketValue, sServiceCenterName, sGroupType, scRegionID,
                    strDefaultApprover = null, strDefaultApproverName = null,
                    strDefaultApproverEmail = null, strDefaultApproverDesignation = null, sUserDesignation, sApproverTAT, sAppSupportTAT;

            //[CR-34] - Proposal No field add -Start
            string strProposalNo = string.Empty;
            //[CR-34] - Proposal No field add -End

            string scZoneID;
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
            sGroupType = dsCallDetails.Tables[0].Rows[0]["GroupType"] == null ? "direct" : Convert.ToString(dsCallDetails.Tables[0].Rows[0]["GroupType"]);
            scRegionID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["scRegionID"] == null ? "0" : Convert.ToString(dsCallDetails.Tables[0].Rows[0]["scRegionID"]));
            sApproverTAT = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverTAT"]);
            sAppSupportTAT = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSupportTAT"]);
            scZoneID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["scZoneID"] == null ? "0" : Convert.ToString(dsCallDetails.Tables[0].Rows[0]["scZoneID"]));
            if (sGroups == "SCU" && sServiceCenterName != string.Empty)
            {
                //sGroups = sGroups + "$" + sServiceCenterName;
                sGroups = sServiceCenterName;
            }
            //			if (sGroups == "SCUD" && sServiceCenterName != string.Empty)
            //			{
            //				sServiceCenterName = sServiceCenterName.Replace("SC", "SCD");
            //				sGroups = sServiceCenterName;
            //			}

            if (sGroups == "SCUD")
            {
                sGroups = sGroups + scRegionID;
            }
            if (sGroups == "LegalClaim")
            {
                sGroups = sGroups + scRegionID;
            }
            //if (sGroups == "HUB")
            //{
            //    sGroups = sGroups + scZoneID;
            //}

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

            if (sGroups == "HUB")
            {
                sGroups = sGroups + scZoneID;
            }

            //[CR-09] Group Mapping Start

            string strLoggedBranch = Convert.ToString(Session["LoggedBranch"]);

            DataSet dsGroupAssign = new DataSet();
            dsGroupAssign = GetGroupAssign(Convert.ToInt32(dsCallDetails.Tables[0].Rows[0]["AIRSMID_FK"]), strLoggedBranch, sUserName);
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
            dsGroupMapped = GetGroupMapped(Convert.ToInt32(dsCallDetails.Tables[0].Rows[0]["AIRSMID_FK"]), strLoggedBranch, sUserName);
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

            //[CR-34] Proposal No field add Start
            strProposalNo = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ProposalNo"]);
            //[CR-34] Proposal No field add End

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

                string strSelectedChannel = null;
                if (ddlchannel.Visible == false)
                {
                    strSelectedChannel = null;
                }
                else
                {
                    strSelectedChannel = ddlchannel.SelectedItem.Text;
                }

                dsApproverID = objBAL.GetApproverForReopenTicket(sTicketNumber, Convert.ToInt32(ViewState["SMID"]), Convert.ToInt32(ViewState["BranchID"]), /*[CR-07]*/ strSelectedChannel /*[CR-07]*/ );
                if (dsApproverID.Tables[0].Rows[0]["AppoverAvailibility"].ToString().ToUpper() != "AVAILABLE")
                {
                    lblMessage.Text = dsApproverID.Tables[0].Rows[0]["AppoverAvailibility"].ToString();
                    return;
                }
                sApproverID = Convert.ToString(dsApproverID.Tables[0].Rows[0]["FApproverID"]);
                sSApproverID = Convert.ToString(dsApproverID.Tables[0].Rows[0]["SApproverID"]);

                //Check if Approver is blocked Code Starts

                EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
                DataTable dtLED = new DataTable();
                dtLED = objEMBAL.GetLoggedEmployeeDetails(sApproverID);

                if (dtLED.Rows.Count == 0)
                {
                    sApproverID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverID"]);
                    sApproverName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverName"]);
                    sApproverEmail = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverMail"]);
                    sApproverDesignation = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverDesignation"]);

                    //sApproverID = strDefaultApprover = CommonUtility.GetValueByKey("DefaultApproverID");
                    //sApproverName = strDefaultApproverName = CommonUtility.GetValueByKey("DefaultApproverName");
                    //sApproverEmail = strDefaultApproverEmail = CommonUtility.GetValueByKey("DefaultApproverEmail");
                    //sApproverDesignation = strDefaultApproverDesignation = CommonUtility.GetValueByKey("DefaultApproverDesignation");
                    isReopen = true;
                }

                //Check if Approver is blocked Code Ends
            }
            sReopenID = objTCBAL.UpdateCallStatusforReopen(sTicketNumber, sApproverID, sSApproverID, sUserRemark, sUploadPath, strReopenTicketNo, Convert.ToInt32(ViewState["SMID"]));
            ViewState["sReopenID"] = sReopenID;

            intCheckException = 1;

            sUserRemark = "[" + DateTime.Now.ToString() + "]: " + sUserRemark + " $ " + sOldRemark;

            string strReturnValue = objCDS.STARTCALLDESK(sUserRemark, sApproverEmail, sApproverDesignation, sApproverName,
                sUserMail, sContactNumber, sApplication, sApproverID, sReopenID, sBranchName, sCallDate, sUploadPath,
                sUserName, sCallType, sTypeofIR, sTicketNumber, sUserDesignation, sGroups, sIRSubType, sCallTAT,
                sSApproverID, sAppSupportPerformer, sSApproverDesignation, sSApproverEmail, sSApproverName, sTicketValue,
                 "rgicl", "rgicl", "", sPriority
                //[CR-34] Proposal No field add Start
                , strProposalNo
                //[CR-34] Proposal No field add End
                //[CR-1]  CQR CR Add Fields 06052019 Start
               , "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""
                //[CR-1]  CQR CR Add Fields 06052019 EN=nd
                 );
            //sApproverTAT, sAppSupportTAT, "rgicl", "rgicl", "", sPriority);
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

    #region Application Reopen Allow
    public bool ApplicationReopenAllow(string AppicationName, string AppSupportCloseDate)
    {
        bool result = false;
        DateTime callclosedate = Convert.ToDateTime(AppSupportCloseDate).Date;
        FABHolidayService.GetFabHolidays FAS = new FABHolidayService.GetFabHolidays();
        try
        {
            int reopendays = GetMaxReopenDays(AppicationName);
            string[] MaxReopenDate = (FAS.GetWorkingdays(Session["BranchCode"].ToString(), callclosedate, reopendays));
            //string MaxReopenDate = Convert.ToString(callclosedate.AddDays(reopendays));
            if (Convert.ToDateTime(MaxReopenDate[0]) >= DateTime.Today.AddDays(0))
                //if (Convert.ToDateTime(MaxReopenDate) >= DateTime.Today.AddDays(0))
                result = true;
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return result;
    }
    #endregion

    #region Get Max Reopen Days

    public int GetMaxReopenDays(params object[] param)
    {
        DataSet oReopenDays = new DataSet();
        int result = 3;
        try
        {
            oReopenDays = DataUtils.ExecuteDataset("usp_ApplicationCallReopenAllow", param);
            if (oReopenDays.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(oReopenDays.Tables[0].Rows[0]["ReopenDays"]);

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
        //try
        //{
        //    if ((ddlbranchname.SelectedIndex > 0) && (ddlchannel.SelectedIndex > 0))
        //    {
        //        BindSM(Convert.ToInt32(ddlbranchname.SelectedItem.Value), Convert.ToInt32(ddlchannel.SelectedItem.Value));
        //        ListItem oListSMItem = ddlsm.Items.FindByText(Session["EmployeeName"].ToString());
        //        if (oListSMItem != null)
        //            ddlsm.SelectedValue = oListSMItem.Value.ToString();
        //        if (ddlsm.SelectedIndex > 0)
        //            //SMID = Convert.ToInt32(ddlsm.SelectedItem.Value);
        //            ViewState["SMID"] = ddlsm.SelectedItem.Value.ToString();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    lblMessage.Text = ex.Message;
        //}
    }

    //protected void ddlbranchnameselectedindexchanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ddlchannel.SelectedIndex = 0;
    //        if (ddlbranchname.SelectedIndex > 0)
    //            // BranchID = Convert.ToInt32(ddlbranchname.SelectedItem.Value);
    //            ViewState["BranchID"] = ddlbranchname.SelectedItem.Value.ToString();
    //        // ddlsm.SelectedIndex = 0;
    //        BindSM(0, 0);

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //    }
    //}

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

    //----------------------Added for resigning issue calls on 07-07-2016----------------------//

    protected void btnReassign_Click(object sender, EventArgs e)
    {
        ////Add code to save or reassign the call to different department


        int IsUpdated = 0;
        int IsException = 0;

        string sApplication = null, sTypeofIR = null, sIRSubType = null, sGroups = null, sServiceCenterName = null, sGroupType = null, scRegionID = null, scZoneID = null;

        try
        {

            string strTicketno = Convert.ToString(Session["TicketNumber"]);
            string strAction = Convert.ToString("GETDETAILS");
            int iAIRSTM = 0;

            iAIRSTM = Convert.ToInt32(ddlIssueRequestSubType.SelectedValue);

            DataSet oDSApp = GetUserDetailsReassign(strAction, iAIRSTM, strTicketno);
            DataTable oDTApp = oDSApp.Tables[0];           
           
            if (oDTApp.Rows.Count > 0 && oDTApp != null)
            {
                sApplication = Convert.ToString(oDSApp.Tables[0].Rows[0]["ApplicationName"]);

                sTypeofIR = Convert.ToString(oDSApp.Tables[0].Rows[0]["IssueRequestType"]);
                sIRSubType = Convert.ToString(oDSApp.Tables[0].Rows[0]["IssueRequestSubType"]);

                sGroups = Convert.ToString(oDSApp.Tables[0].Rows[0]["Groups"]);
                sServiceCenterName = oDSApp.Tables[0].Rows[0]["ServiceCenterName"] == null ? "" : Convert.ToString(oDSApp.Tables[0].Rows[0]["ServiceCenterName"]);
                sGroupType = oDSApp.Tables[0].Rows[0]["GroupType"] == null ? "direct" : Convert.ToString(oDSApp.Tables[0].Rows[0]["GroupType"]);
                scRegionID = Convert.ToString(oDSApp.Tables[0].Rows[0]["scRegionID"] == null ? "0" : Convert.ToString(oDSApp.Tables[0].Rows[0]["scRegionID"]));
                scZoneID = Convert.ToString(oDSApp.Tables[0].Rows[0]["scZoneID"] == null ? "0" : Convert.ToString(oDSApp.Tables[0].Rows[0]["scZoneID"]));


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

            }
            //string strTicketno = Convert.ToString(Session["TicketNumber"]);
            int intIssueRequestSubTypeCode = Convert.ToInt32(ddlIssueRequestSubType.SelectedValue);
            string strEmployee = Convert.ToString(Membership.GetUser().UserName);
            string strActionType = Convert.ToString("APPLICATION CATEGORY CHANGE");
            string strIPADD = Request.UserHostAddress;
            string strActionReassign = "REASSIGN";

            TechDeskService.TechDeskService objTDS = new TechDeskService.TechDeskService();
            TechDeskObject obj = new TechDeskObject();

            obj.applicationType = sApplication;
            obj.issueReqSubTypeID = sTypeofIR;
            obj.issueRequestId = sIRSubType;
            obj.teamName = sGroups;
            obj.ticketNo = strTicketno;          


            bool strResponseVal = objTDS.UpdateTicket(obj);           

            // bool strResponseVal=true;// = objTDS.UpdateTicket(obj);

            if (strResponseVal == true)
            {
                IsException = 1;

                DataSet oDSAssignApp = UpdateCallTypes(strTicketno, intIssueRequestSubTypeCode, strEmployee, strActionType, strIPADD, strActionReassign);
                DataTable oDTAssignApp = oDSAssignApp.Tables[0];


                if (oDTAssignApp.Rows.Count > 0 && oDTAssignApp != null)
                {
                    IsUpdated = Convert.ToInt32(oDTAssignApp.Rows[0]["EXISTS"].ToString());
                    IsException = 1;
                }

                if (IsUpdated == 1)
                {
                    lblMessage.Text = "Ticket Updated Successfully";
                    trReassign.Visible = false;
                    BindCallDetailsGridByTicketNumber();
                    //CALL Savvion Service to Update Data
                }
                else
                {
                    IsException = 2;
                }
            }

            ////CALL SAVVION SERVICE TO UPDATE DATA
            //IF Success
            //{ 
            // IsException = 1; 
            //CALL Calldesk update Method }
            //ELSE
            //{ }


        }
        catch (Exception ex)
        {
            FileStream fs2 = new FileStream(Server.MapPath("..\\Files") + "\\" + "CustomError.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw2 = new StreamWriter(fs2);
            sw2.Write("\r\n =======================================================================================");
            sw2.Write("\r\n Log Entry On : " + System.DateTime.Now);           
            sw2.Write("\n " + ex.StackTrace);
            sw2.Write("\n " + ex.Message);
            sw2.Close();
            fs2.Close();
            
            if (IsException == 1)
            {
                lblMessage.Text = "Problem occured while updating data to server, please contact your administrator.";
            }
            if (IsException == 0)
            {
                lblMessage.Text = "Problem occured while updating data to server.";
            }
            if (IsException == 2)
            {
                lblMessage.Text = "Problem occured while updating data in Call Desk.";
            }
        }

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (trReassign.Visible == true)
        {
            trReassign.Visible = false;
        }
        else
        {
            trReassign.Visible = true;
        }
    }


    #region Check User
    public int CheckUser()
    {
        int Isuser = 0;

        string strEmployee = Convert.ToString(Membership.GetUser().UserName);
        string strAction = Convert.ToString("USER");
        DataSet oDSApp = GetUserToEdit(strAction, strEmployee);
        DataTable oDTApp = oDSApp.Tables[0];


        if (oDTApp.Rows.Count > 0 && oDTApp != null)
        {
            Isuser = Convert.ToInt32(oDTApp.Rows[0]["ISEXISTS"].ToString());
        }

        return Isuser;
        //ddlApplication.Items.Clear();
        //ddlApplication.DataSource = oDTApp;
        //ddlApplication.DataTextField = "ApplicationName";
        //ddlApplication.DataValueField = "ApplicationID_PK";
        //ddlApplication.DataBind();
        //ddlApplication.Items.Insert(0, new ListItem("All Applications", "0"));
    }
    #endregion

    #region Get Authorised User to Edit Call
    public DataSet GetUserToEdit(params object[] param)
    {
        DataSet oDSApplicationTypes = new DataSet();
        try
        {
            oDSApplicationTypes = DataUtils.ExecuteDataset("USP_GET_AUTHORIZED_USER_FOR_EDIT", param);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return oDSApplicationTypes;
    }
    #endregion



    #region Reassign call details
    public DataSet UpdateCallTypes(params object[] param)
    {
        DataSet oDSApplicationTypes = new DataSet();
        try
        {
            oDSApplicationTypes = DataUtils.ExecuteDataset("USP_INSERT_CALL_DETAILS_LOG", param);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return oDSApplicationTypes;
    }
    #endregion



    #region Get Call Details For Reassign
    public DataSet GetUserDetailsReassign(params object[] param)
    {
        DataSet oDSApplicationTypes = new DataSet();
        try
        {
            oDSApplicationTypes = DataUtils.ExecuteDataset("USP_GETDETIALS_REASSIGN_TICKET", param);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return oDSApplicationTypes;
    }
    #endregion

    //----------------------Added for resigning issue calls on 07-07-2016----------------------//

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

    //[CR-12] RCA Start
    public DataSet GetRCADetails(string TicketNumber)
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
            cmd.CommandText = "usp_GetRCADetails";
            cmd.Parameters.AddWithValue("@TicketNumber", TicketNumber);

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
    //[CR-12] RCA End

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

    //[CR-07] Large Market start

    //[CR-07] Large Market Start
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
}
