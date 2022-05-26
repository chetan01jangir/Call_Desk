using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using CallDeskBAL;
using System.IO;
//using TechDeskService;

public partial class User_CallDetails : System.Web.UI.Page
{
    #region Objects
    public static string strCalldekDBConn = "Data source=RGIRMSCDdb.reliancegeneral.com,7359;initial catalog=CallDeskManagement;user id=calldesk ;password=calldesk; integrated security=false;Max Pool Size=500;Min Pool Size=10;Pooling=true;";
    #endregion

    #region Page Load Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["TicketNum"] != null)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["TicketNum"])))
                    {
                        trDetails.Visible = true;
                        BindCallDetailsGridByTicketNumber();
                        trSearch.Visible = false;
                    }
                    else
                    {
                        trSearch.Visible = true;
                        trDetails.Visible = false;
                    }

                }
                else
                {
                    trSearch.Visible = true;
                    trDetails.Visible = false;
                }

                
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;				
            }
        }
        //DisableButtons();
    }

    #endregion
 

    #region Bind Call Details Grid By Ticket Number

    public void BindCallDetailsGridByTicketNumber()
    {
        try
        {
            string strTicketNumber = string.Empty;

            if (Request.QueryString["TicketNum"] != null)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["TicketNum"])))
                {
                    strTicketNumber = Convert.ToString(Request.QueryString["TicketNum"]);

                }
                else
                {
                    strTicketNumber = txtTicketNo.Text.Trim();
                }

            }
            else
            {
                strTicketNumber = txtTicketNo.Text.Trim();
            }

			if (strTicketNumber != null)
			{						
			
				DataTable dtUserFiles = new DataTable();           

                DataSet dsCallDetails = new DataSet();
                dsCallDetails = GetCallDetailsByTicketNo(strTicketNumber);

				if(dsCallDetails!=null && dsCallDetails.Tables[0].Rows.Count > 0)
				{

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

					//lblApproverRemark.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverRemark"]);

					lblApproverRemark.Text = "";

					string approverRemark = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverRemark"]);

					if (approverRemark != null && approverRemark.Contains("$")) 
					{
						string []arr = approverRemark.Split('$');
						for (int i = 0; i < arr.Length; i++) 
						{
							string rem = "Remark " + (i+1) + " : " + arr[i] + "<br/>";
							lblApproverRemark.Text =  lblApproverRemark.Text + rem;
						}
					}

					if (approverRemark != null && !approverRemark.Contains("$")) 
					{						
						lblApproverRemark.Text =  approverRemark;						
					}

					lblApproverClosedDate.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverClosedDate"]);

					lblAppSupportStatus.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSupportStatus"]);

					if (lblAppSupportStatus.Text == "In Progress")
					{
						tdCloseDate.InnerText = "Forwarded Date";
					}

					//lblRemarkforUser.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSupportRemark"]);

					lblAppSupportCloseDate.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSupportCloseDate"]);
					
					//lblAppSupportRemark.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSupportRemark"]);

					lblAppSupportRemark.Text = "";

					string appsupportRemark = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSupportRemark"]);

					if (appsupportRemark != null && appsupportRemark.Contains("$")) 
					{
						string []arr1 = appsupportRemark.Split('$');
						for (int i = 0; i < arr1.Length; i++) 
						{
							string rem1 = "Remark " + (i+1) + " : " + arr1[i] + "</br>";
							lblAppSupportRemark.Text = lblAppSupportRemark.Text + rem1;
						}
					}

					if (appsupportRemark != null && !appsupportRemark.Contains("$"))
					{
                        lblAppSupportRemark.Text = appsupportRemark; 
					}

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

					//Get Approver files Code Starts Here               

                    DataSet dsApproverFiles = new DataSet();
                    dsApproverFiles = GetFilesByApprover(strTicketNumber);
				
					if (dsApproverFiles!=null && dsApproverFiles.Tables[0].Rows.Count > 0)
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
                        dsAppSupportFile = GetAppSupportFileByRowID(strTicketNumber);
						gvFilesUploadedforUser.DataSource = dsAppSupportFile;
						gvFilesUploadedforUser.DataBind();
					}
					//Get AppSupport files Code Ends Here

					int ReopenTime = Convert.ToInt32(dsCallDetails.Tables[0].Rows[0]["DD"]);

					if (strCallType == "Request")
					{
						trRequest.Visible = true;
                       
                        DataSet dsApprover = new DataSet();
                        dsApprover = GetReopenCallApprover(strTicketNumber); 
					
						if (dsApprover!=null && dsApprover.Tables.Count > 0)
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

					if (ReopenTime <= 24)
					{
						if ((lblApproverStatus.Text.Equals("Approved") && lblAppSupportStatus.Text.Equals("Resolved")) || (lblApproverStatus.Text.Equals("") && lblAppSupportStatus.Text.Equals("Resolved")))
						{
							//trReopen.Visible = true;
							//btnReopen.Visible = true;
						}

						else
						{
							//trReopen.Visible = false;
							//btnReopen.Visible = false;
						}
					}
					else
					{						
						//trReopen.Visible = false;
						//btnReopen.Visible = false;
					}
				}
			}
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.StackTrace;
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
            //sbValid.Append("document.getElementById('" + btnReopen.ClientID.ToString().Replace('$', '_') + "').disabled = true;");
            //sbValid.Append("document.getElementById('" + btnOK.ClientID.ToString().Replace('$', '_') + "').disabled = true;");
            //sbValid.Append(this.Page.GetPostBackEventReference(this.btnReopen));
            sbValid.Append(";");
            //this.btnReopen.Attributes.Add("onclick", sbValid.ToString());
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Get CallDetails By TicketNo
    public DataSet GetCallDetailsByTicketNo(string strTicketNumber)
    {
        SqlConnection sqlCON = new SqlConnection();
        SqlCommand sqlCMD = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();  
        DataSet oDS = new DataSet();
        try
        {        
            sqlCON.ConnectionString = strCalldekDBConn;
            sqlCON.Open();
            sqlCMD.Connection = sqlCON;
            sqlCMD.CommandType = CommandType.StoredProcedure;
            sqlCMD.CommandText = "usp_GetCallDetailsByTicketNumberForAdmin";
            SqlParameter par = new SqlParameter("@TicketNumberPK", SqlDbType.NVarChar, 25);
            par.Value = strTicketNumber;
            sqlCMD.Parameters.Add(par);                           
            sda.SelectCommand = sqlCMD;
            sda.Fill(oDS);
            //sqlCMD.ExecuteNonQuery();           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            sqlCON.Close();
            sqlCMD.Dispose();
            sda.Dispose();
        }
        return oDS;
    }
    #endregion

    #region Get Files ByApprover
    public DataSet GetFilesByApprover(string strTicketNumber)
    {
        SqlConnection sqlCON = new SqlConnection();
        SqlCommand sqlCMD = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter(); 
        DataSet oDS = new DataSet();
        try
        {
            sqlCON.ConnectionString = strCalldekDBConn;
            sqlCON.Open();
            sqlCMD.Connection = sqlCON;
            sqlCMD.CommandType = CommandType.StoredProcedure;
            sqlCMD.CommandText = "usp_GetFilesByApprover";
            SqlParameter par = new SqlParameter("@TicketNumberFK", SqlDbType.NVarChar, 25);
            par.Value = strTicketNumber;
            sqlCMD.Parameters.Add(par);          
            sda.SelectCommand = sqlCMD;
            sda.Fill(oDS);
            //sqlCMD.ExecuteNonQuery();           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            sqlCON.Close();
            sqlCMD.Dispose();
            sda.Dispose();
        }
        return oDS;

    }
    #endregion

    #region Get AppSupport File By RowID
    public DataSet GetAppSupportFileByRowID(string strTicketNumber)
    {
        SqlConnection sqlCON = new SqlConnection();
        SqlCommand sqlCMD = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataSet oDS = new DataSet();
        try
        {
            sqlCON.ConnectionString = strCalldekDBConn;
            sqlCON.Open();
            sqlCMD.Connection = sqlCON;
            sqlCMD.CommandType = CommandType.StoredProcedure;
            sqlCMD.CommandText = "usp_GetAppSupportFileByRowID";
            SqlParameter par = new SqlParameter("@TicketNumberFK", SqlDbType.NVarChar, 25);
            par.Value = strTicketNumber;
            sqlCMD.Parameters.Add(par);
            sda.SelectCommand = sqlCMD;
            sda.Fill(oDS);
            //sqlCMD.ExecuteNonQuery();           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            sqlCON.Close();
            sqlCMD.Dispose();
            sda.Dispose();
        }
        return oDS;

    }
    #endregion

    #region Get Reopen Call Approver
    public DataSet GetReopenCallApprover(string strTicketNumber)
    {
         SqlConnection sqlCON = new SqlConnection();
        SqlCommand sqlCMD = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataSet oDS = new DataSet();
        try
        {
            sqlCON.ConnectionString = strCalldekDBConn;
            sqlCON.Open();
            sqlCMD.Connection = sqlCON;
            sqlCMD.CommandType = CommandType.StoredProcedure;
            sqlCMD.CommandText = "usp_GetReopenCallApprover";
            SqlParameter par = new SqlParameter("@TicketNumberFK", SqlDbType.NVarChar, 25);
            par.Value = strTicketNumber;
            sqlCMD.Parameters.Add(par);
            sda.SelectCommand = sqlCMD;
            sda.Fill(oDS);
            //sqlCMD.ExecuteNonQuery();           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            sqlCON.Close();
            sqlCMD.Dispose();
            sda.Dispose();
        }
        return oDS;
    }
    #endregion

    #region btnSearch Click
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        trDetails.Visible = true;
        BindCallDetailsGridByTicketNumber();
    }
    #endregion
}
