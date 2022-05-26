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
using System.IO;
using CallDeskBAL;
using System.Text;
using CallDeskDAL;

public partial class User_Confirmation : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            try
            {
                BindGrid();

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }


    

    #region Track Call Button Code

    protected void BtnGetdetail_Click(object sender, EventArgs e)
    {
        //txtTicketNumber.Text = "";

        try
        {
            gvCallDetails.DataSource = null;
            gvCallDetails.DataBind();
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Track Call Button Code

    protected void BtnTrackCall_Click(object sender, EventArgs e)
    {
		
		
        txtTicketNumber.Text = "";

        try
        {
            gvCallDetails.DataSource = null;
            gvCallDetails.DataBind();
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    //#region Bind Grid

    //public void BindGrid()
    //{
    //    string strCallCreatedBy;
       

    //    DataSet dsCDData = new DataSet();
    //    TrackCallBAL objBAL = new TrackCallBAL();
    //    strCallCreatedBy = Membership.GetUser().UserName;
    //    dsCDData = objBAL.GetCallDetailsForUsersOpenCall(strCallCreatedBy, "CustomerApproval", "");

    //    ViewState["GridViewData"] = dsCDData;
    //    gvCallDetails.DataSource = dsCDData;
    //    gvCallDetails.DataBind();



    //}

    //#endregion Bind Grid

    #region Bind Grid

     public void BindGrid()
    {
        string strCallCreatedBy;
        DataSet dsCDData = new DataSet();
        TrackCallBAL objBAL = new TrackCallBAL();
        strCallCreatedBy = Membership.GetUser().UserName;

		object[] parmsTicket = new object[3]{strCallCreatedBy,"CustomerApproval", txtTicketNumber.Text};
		object[] parms = new object[3]{strCallCreatedBy,"CustomerApproval",""};
		
		
		
        if (txtTicketNumber.Text != "")
        {
			
            dsCDData = GetCallDetailsForUsersOpenCall_Userconfirmation(parmsTicket);
            
        }
        else
        {
			
            dsCDData = GetCallDetailsForUsersOpenCall_Userconfirmation(parms);
        }

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
		if(Dtable.Rows.Count > 0)
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
        TrackCallBAL objBAL = new TrackCallBAL();
        strCallCreatedBy = Membership.GetUser().UserName;
		
		object[] parms = new object[3]{strCallCreatedBy, "Close", Ticketno};
        dsCDData = GetCallDetailsForUsersOpenCall_Userconfirmation(parms);



        //ViewState["GridViewData"] = dsCDData;
        //gvCallDetails.DataSource = dsCDData;
        //gvCallDetails.DataBind();
    }

    #endregion CLose Ticket





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
                
                StringBuilder stPopupScript = new StringBuilder();
                stPopupScript.Append("<script language='javascript'>");
                stPopupScript.Append("var w = window.open('CallDetails.aspx?','PopUpWindowName','width=700,left=150,top=100,height=600,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up
                stPopupScript.Append("w.focus()");
                stPopupScript.Append("</script>");

                Page.RegisterClientScriptBlock("PopUpwindowOpen", stPopupScript.ToString());
               // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + "User/CallDetails.aspx" + "','_blank')", true);
                //StringBuilder stPopupScript = new StringBuilder();
                //stPopupScript.Append("<script language='javascript'>");
                //stPopupScript.Append("var w = window.open('CallDetails.aspx','PopUpWindowName','width=700,left=150,top=100,height=600,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up
                //stPopupScript.Append("w.focus()");

                //stPopupScript.Append("</script>");

                //Page.RegisterClientScriptBlock("PopUpwindowOpen", stPopupScript.ToString());



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



        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

}
