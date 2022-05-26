using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CallDeskDAL;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
	private  string connectionString = ConfigurationManager.ConnectionStrings["CallDeskDB"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
       // AntiforgeryChecker.Check(this, antiforgery);
    }
	protected void Page_PreRender(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            try
            {
                BindGrid();
                if (gvCallDetails.Rows.Count > 0)
                {
                    popupcustomer.Show();
                }
                else
                {
                    showpopup();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string UniqueID = null;
            DataSet dsID = new DataSet();
            //dsID = GetID(3, 0, 0, 0, 0);
            dsID = GetID();
            if (dsID.Tables[0].Rows.Count > 0)
            {
                UniqueID =dsID.Tables[0].Rows[0]["ID"].ToString();
            }
            int[] ratings = new int[4];
            ratings[0] = Convert.ToInt32(rdblistq1.SelectedValue);
            ratings[1] = Convert.ToInt32(rdblistq2.SelectedValue);
            ratings[2] = Convert.ToInt32(rdblistq3.SelectedValue);
            ratings[3] = 5;
            string username = Session["UserName"].ToString();
            string remarks = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add(new System.Data.DataColumn("ID", typeof(int)));
            dt.Columns.Add(new System.Data.DataColumn("EmployeeCode", typeof(string)));
            dt.Columns.Add(new System.Data.DataColumn("Questionid", typeof(int)));
            dt.Columns.Add(new System.Data.DataColumn("Ratingid", typeof(int)));
            dt.Columns.Add(new System.Data.DataColumn("CreatedDate", typeof(DateTime)));
            dt.Columns.Add(new System.Data.DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new System.Data.DataColumn("UniqueID", typeof(string)));
            for (int i = 0; i < 4; i++)
            {
                if (i == 3)
                {
                    remarks = txtremarks.Text;
                }
                dt.Rows.Add(1, username, i + 1, ratings[i], DateTime.Now, remarks, UniqueID);
            }
            if (UniqueID!=string.Empty ||UniqueID!="")
			{
                SaveFeedback(dt, UniqueID, txtusernm.Text, txtemail.Text, txtmobileno.Text);
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('On behalf of Reliance General Insurance we Thank you for your valuable feedback. Your feedback is saved successfully.')", true);
			}
            else
            {
                Exception ex = new Exception();
                throw ex;
            }  
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error Occured";
            pnlMyPanel.Height = new Unit(481);
            popupconfirm.Show();
        }
      
    }

    protected void rdbyesnochange(object sender, EventArgs e)
    {
        try
        {
            if (rdbyesno.SelectedItem.Value == "1")
            {
                tryesno.Visible = false;
                trrdoyesno.Visible = false;
                trqst.Visible = true;
                trqst2.Visible = true;
                trtxtremark.Visible = true;
                trbtns.Visible = true;
                pnlMyPanel.Height = new Unit(453);
                btnsubmit.Visible = true;
                popupconfirm.Show();
               
            }
            else
            {
                //tryesno.Visible = true;
                //trqst.Visible = false;
                //trqst2.Visible = false;
                //trtxtremark.Visible = false;
                //trbtns.Visible = false;
                popupconfirm.Hide();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

	 #region Get Agent Profile
    public DataSet GetProfile(params object[] param)
    {
        DataSet oDSData = new DataSet();
        try
        {
            oDSData = DataUtils.ExecuteDataset("sp_Getagentprofile", param);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return oDSData;
    }
    #endregion

    #region Fill Profile Info
    public void FillProfileInfo()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = GetProfile(Session["UserName"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtusernm.Text = ds.Tables[0].Rows[0]["EmployeeName"].ToString();
                txtmobileno.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                txtemail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Already saved records
    public DataSet GetData(params object[] param)
    {
        DataSet oDSData = new DataSet();
        try
        {
            oDSData = DataUtils.ExecuteDataset("sp_employeefeedback", param);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return oDSData;
    }
    #endregion

    #region UniqueID
    public DataSet GetID(params object[] param)
    {
        DataSet oDSData = new DataSet();
        try
        {
            oDSData = DataUtils.ExecuteDataset("usp_GetUniqueID", param);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return oDSData;
    }
    #endregion

    #region save feedback
    public void SaveFeedback(DataTable fdt,string uniqueid,string username,string emailid,string mobileno)
    {
        SqlConnection oCon = new SqlConnection();
        SqlCommand oComm = new SqlCommand();
        try
        {
            oCon.ConnectionString = connectionString;
            oCon.Close();
            oCon.Open();
            oComm.Connection = oCon;
            oComm.CommandType = CommandType.StoredProcedure;
            oComm.CommandText = "sp_insertemployeefeedback";
            oComm.Parameters.Add(new SqlParameter("@empfeedback", fdt));
            oComm.Parameters.Add(new SqlParameter("@UniqueID", uniqueid));
            oComm.Parameters.Add(new SqlParameter("@employeename", username));
            oComm.Parameters.Add(new SqlParameter("@EmailID", emailid));
            oComm.Parameters.Add(new SqlParameter("@MobileNo", mobileno));
            oComm.ExecuteNonQuery();
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            oComm.Dispose();
            oCon.Close();
        }
    }
    #endregion

	/////////////////////////////////// Added for User Confirmation on 01-SEP-2015

    #region Show Popup

    public void showpopup()
    {
        string username = Session["UserName"].ToString();
        DataSet ds = new DataSet();
        if (Session["Type"] != null)
        {
            popupconfirm.Show();
            FillProfileInfo();
            Session["Type"] = null;
        }
        else
        {
            ds = GetData(2, username, 0, 0, 0);
            if ((ds.Tables[0].Rows.Count < 1) && (Convert.ToInt32(DateTime.Today.Day) <= 5))
            {
                popupconfirm.Show();
                FillProfileInfo();
            }
            //if (Convert.ToInt32(DateTime.Today.Day) >= 6)
            //    mdlmailer.Show();
        }
    }

    #endregion

    #region Bind Grid

    public void BindGrid()
    {
        string strCallCreatedBy;
        DataSet dsCDData = new DataSet();

        strCallCreatedBy = Membership.GetUser().UserName;

        //object[] parmsTicket = new object[3] { strCallCreatedBy, "CustomerApproval", txtTicketNumber.Text };
        object[] parms = new object[3] { strCallCreatedBy, "CustomerApproval", "" };

        dsCDData = GetCallDetailsForUsersOpenCall_Userconfirmation(parms);
        
       // dsCDData = GetCallDetailsForUsersOpenCall(strCallCreatedBy, "CustomerApproval", "");

        DataTable Dtable = new DataTable();

        if (dsCDData != null)
        {
            if (dsCDData.Tables[0].Rows.Count > 0)
            {
                Dtable = dsCDData.Tables[0].Clone();
                foreach (DataRow drow in dsCDData.Tables[0].Rows)
                {
                    int ReopenTime = Convert.ToInt32(drow["DD"]);
                    if ((ReopenTime <= 72) || (ApplicationReopenAllow(Convert.ToString(drow["ApplicationName"]), Convert.ToString(drow["AppSupportCloseDate"]))))
                    {
                        DataRow drNew = Dtable.NewRow();
                        drNew.ItemArray = drow.ItemArray;
                        Dtable.Rows.Add(drNew);
                    }
                }
            }
        }

        //ViewState["GridViewData"] = dsCDData;
        //gvCallDetails.DataSource = dsCDData;
        //gvCallDetails.DataBind();

        if (Dtable.Rows.Count > 0)
        {
            ViewState["GridViewData"] = Dtable;
            gvCallDetails.DataSource = Dtable;
            gvCallDetails.DataBind();
        }
        else
        {
            gvCallDetails.DataSource = null;
            gvCallDetails.DataBind();
        }
    }

    #endregion Bind Grid

    #region GetCallDetailsForUsersOpenCall_Userconfirmation

    public DataSet GetCallDetailsForUsersOpenCall_Userconfirmation(params object[] param)
    {
        try
        {
            return DataUtils.ExecuteDataset("usp_GetCallDetailsForUsersOpenCall_Userconfirmation", param);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    #endregion
    
    #region Get Max Reopen Days

    public int GetMaxReopenDays(params object[] param)
    {
        DataSet oReopenDays = new DataSet();
        int result = 0;
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


            if (Convert.ToDateTime(MaxReopenDate[0]) >= DateTime.Today.AddDays(0))
                result = true;

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return result;
    }
    #endregion

    #region Close Ticket

    public void CLoseTicket(string Ticketno)
    {
        string strCallCreatedBy;
        DataSet dsCDData = new DataSet();
        
        strCallCreatedBy = Membership.GetUser().UserName;

        object[] parms = new object[3] { strCallCreatedBy, "Close", Ticketno };
        dsCDData = GetCallDetailsForUsersOpenCall_Userconfirmation(parms);

    }

    #endregion CLose Ticket   

    //#region Bind Call Details on Page Load For User(Open Call Only)

    //public DataSet GetCallDetailsForUsersOpenCall(params object[] param)
    //{
    //    try
    //    {
    //        TrackCallDAL objTCDAL = new TrackCallDAL();
    //        return objTCDAL.GetCallDetailsForUsersOpenCall(param);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new ApplicationException(ex.Message);
    //    }
    //}

    //#endregion

    #region Call Details Grid Row Command Event

    protected void gvCallDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "lnkreopen")
            {
                string strTicketNumber = Convert.ToString(e.CommandArgument);
                Session["TicketNumber"] = strTicketNumber;
                //Response.Redirect("CallDetails.aspx",true);
                Session["PreviousPage"] = null;
                Session["PreviousPage"] = "Main";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "User/CallDetails.aspx" + "','_blank')", true);
                //StringBuilder stPopupScript = new StringBuilder();
                //stPopupScript.Append("<script language='javascript'>");
                //stPopupScript.Append("var w = window.open('CallDetails.aspx','PopUpWindowName','width=700,left=150,top=100,height=600,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up
                //stPopupScript.Append("w.focus()");

                //stPopupScript.Append("</script>");

                //Page.RegisterClientScriptBlock("PopUpwindowOpen", stPopupScript.ToString());


                if (gvCallDetails.Rows.Count > 0)
                {
                    popupcustomer.Show();
                }
                else
                {
                    showpopup();
                }
            }

            if (e.CommandName == "lnkDetails")
            {
                string strTicketNumber = Convert.ToString(e.CommandArgument);
                Session["TicketNumber"] = strTicketNumber;
                // Response.Redirect("CallDetails.aspx");

                //GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                //int iIndex = gvRow.RowIndex;

                //CheckBox chkclose = (CheckBox)gvCallDetails.Rows[iIndex].FindControl("chkclose");

                //if (chkclose.Checked == true)
                //{
                CLoseTicket(strTicketNumber);
                //}

                BindGrid();
                if (gvCallDetails.Rows.Count > 0)
                {
                    popupcustomer.Show();
                }
                else
                {
                    showpopup();
                }
                //StringBuilder stPopupScript = new StringBuilder();
                //stPopupScript.Append("<script language='javascript'>");
                //stPopupScript.Append("var w = window.open('CallDetails.aspx?','PopUpWindowName','width=700,left=150,top=100,height=600,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up
                //stPopupScript.Append("w.focus()");
                //stPopupScript.Append("</script>");

                //Page.RegisterClientScriptBlock("PopUpwindowOpen", stPopupScript.ToString());
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Call Details Grid View Page Index Changing Code

    protected void gvCallDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCallDetails.PageIndex = e.NewPageIndex;

            DataTable dsCDData = new DataTable();
            dsCDData = (DataTable)ViewState["GridViewData"];
            gvCallDetails.DataSource = dsCDData;
            gvCallDetails.DataBind();


            if (gvCallDetails.Rows.Count > 0)
            {
                popupcustomer.Show();
            }
            else
            {
                showpopup();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Close Customer Panel

    protected void btncloseCustomer_Click(object sender, EventArgs e)
    {
        showpopup();
    }

    #endregion

    /////////////////////////////////// Added for User Confirmation on 01-SEP-2015
	
	}
