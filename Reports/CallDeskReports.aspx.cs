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
using CallDeskBO;
using CallDeskBAL;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Data.SqlClient;

public partial class Reports_CallDeskReports : System.Web.UI.Page
{

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        string UserRole = System.Web.Security.Roles.GetRolesForUser()[0].ToString();
        

        if (!IsPostBack)
        {
            if (UserRole.ToLower() == "admin")
            {
                GetBranch();
            }
            else if(UserRole =="ReportFullAccess")
            {
                GetBranch();
            }
            else
            {
                GetBranchByRegion();
            }
            GetRegion();
            GetApplication();
			CommonUtility.AddAllToDropDown(ddlIssueRequestType);
			CommonUtility.AddAllToDropDown(ddlIssuesubRequestType);
        }
        
        if (UserRole.ToLower() == "admin")
        {
            trBranch.Visible = false;
            trRGON.Visible = false;
        }
        else if (UserRole == "ReportFullAccess")
        {
            trBranch.Visible = false;
            trRGON.Visible = false;
        }
        else if (ddlReportsOn.SelectedValue == "Region")
        {
            trBranch.Visible = false;
            trRegion.Visible = false;
        }
        else
        {
            trBranch.Visible = true;
            trRGON.Visible = true;
        }
    }

    #endregion

    #region Method To Get Branch

    public void GetBranch()
    {
        try
        {
            LocationBAL objBAL = new LocationBAL();
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataSource = objBAL.GetBranchName();
            ddlBranch.DataBind();
            CommonUtility.AddAllToDropDown(ddlBranch);

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Method To Get Branch By Region

    public void GetBranchByRegion()
    {
        try
        {
            AdminBAL objBAL = new AdminBAL();
            string strRegionCode = Session["RegionCode"].ToString();
            ddlBranch.DataSource = objBAL.GetBranchByRegion(strRegionCode);
            ddlBranch.DataBind();
            CommonUtility.AddAllToDropDown(ddlBranch);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }


    #endregion

    #region Method To Get Region

    public void GetRegion()
    {
        try
        {
            LocationBAL objBAL = new LocationBAL();
            ddlRegion.DataTextField = "RegionName";
            ddlRegion.DataValueField = "RegionID_PK";
            ddlRegion.DataSource = objBAL.GetRegionName();
            ddlRegion.DataBind();
            CommonUtility.AddAllToDropDown(ddlRegion);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

	#region CheckBox Change
	
	 protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name = "";
        ddlIssueRequestType.Items.Clear();
        ddlIssuesubRequestType.Items.Clear();
         int Appcount = 0;

        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            if (CheckBoxList1.Items[i].Selected)
            {
                name += CheckBoxList1.Items[i].Text + ",";

                Appcount = Appcount + 1;
                if (Appcount > 1)
                {
                    ddlIssueRequestType.Enabled = false;
                    ddlIssuesubRequestType.Enabled = false;
                    CommonUtility.AddAllToDropDown(ddlIssueRequestType);
                    CommonUtility.AddAllToDropDown(ddlIssuesubRequestType);
                }
                else
                {
                    ddlIssueRequestType.Enabled = true;
                    ddlIssuesubRequestType.Enabled = true;
                    ddlIssuesubRequestType.Items.Clear();
                    //GetRequestType();
                    GetRequestTypeNew(Convert.ToInt32(CheckBoxList1.Items[i].Value));
                    CommonUtility.AddAllToDropDown(ddlIssueRequestType);
                    CommonUtility.AddAllToDropDown(ddlIssuesubRequestType);
                }
                //GetRequestTypeNew(Convert.ToInt32(CheckBoxList1.Items[i].Value));
            }
        }
        txtApplication_name.Text = name;

       
        //GetRequestType();


    }
	
	#endregion
	
	
    #region Method To Get Application Name

    public void GetApplication()
    {
        try
        {
           RegisterNewCallBAL objBAL = new RegisterNewCallBAL();

            //---------------------------Commented On 03-07-2015---------------------------------//

            //ddlApplication.DataTextField = "ApplicationName";
            //ddlApplication.DataValueField = "ApplicationID_PK";
            //ddlApplication.DataSource = objBAL.GetApplicationType();
            //ddlApplication.DataBind();
            //CommonUtility.AddAllToDropDown(ddlApplication);

            //---------------------------Commented On 03-07-2015---------------------------------//

            CheckBoxList1.DataTextField = "ApplicationName";
            CheckBoxList1.DataValueField = "ApplicationID_PK";
            CheckBoxList1.DataSource = objBAL.GetApplicationType();
            CheckBoxList1.DataBind();
            //CommonUtility.AddAllToDropDown(CheckBoxList1);
            CheckBoxList1.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

	#region Method To Get Request Type

    public void GetRequestTypeNew(int Appid)
    {
        try
        {
            RegisterNewCallBAL objBAL = new RegisterNewCallBAL();



            ddlIssueRequestType.DataTextField = "IssueRequestType";
            ddlIssueRequestType.DataValueField = "IssueRequestType_PK";
            ddlIssueRequestType.DataSource = objBAL.GetIssueRequestTypeByApplicationType(Appid);
            ddlIssueRequestType.DataBind();

            //CommonUtility.AddAllToDropDown(ddlIssueRequestType);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion
	
	#region Method To Get Request Type

	//public void GetRequestType()
    //{
    //    try
    //    {
    //        RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
    //        //---------------------------Commented On 03-07-2015---------------------------------//

    //        ddlIssueRequestType.DataTextField = "IssueRequestType";
    //        ddlIssueRequestType.DataValueField = "IssueRequestType_PK";
    //        ddlIssueRequestType.DataSource = objBAL.GetIssueRequestTypeByApplicationType(Convert.ToInt32(ddlApplication.SelectedItem.Value));
    //        ddlIssueRequestType.DataBind();

    //        //---------------------------Commented On 03-07-2015---------------------------------//

    //        //CheckBoxList2.DataTextField = "IssueRequestType";
    //        //CheckBoxList2.DataValueField = "IssueRequestType_PK";
    //        //CheckBoxList2.DataSource = objBAL.GetIssueRequestTypeByApplicationType(Convert.ToInt32(ddlApplication.SelectedItem.Value));
    //        //CheckBoxList2.DataBind();

    //        CommonUtility.AddAllToDropDown(ddlIssueRequestType);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //    }
    //}

	#endregion

	#region Method To Get Request Sub Type

	public void GetRequestSubType()
	{
	string  Value = null ;
		try
		{
		for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    Value = CheckBoxList1.Items[i].Value;
                }
            }
			
			RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
			ddlIssuesubRequestType.DataTextField = "IssueRequestSubType";
			ddlIssuesubRequestType.DataValueField = "IssueRequestSubType_PK";
			ddlIssuesubRequestType.DataSource = objBAL.GetIssueRequestSubTypeByIssueRequestType(Convert.ToInt32(Value), Convert.ToInt32(ddlIssueRequestType.SelectedItem.Value));
			ddlIssuesubRequestType.DataBind();
			CommonUtility.AddAllToDropDown(ddlIssuesubRequestType);
		}
		catch (Exception ex)
		{
			lblMessage.Text = ex.Message;
		}
	}

	#endregion

	#region ddlApplicationType SelectedIndexChanged
	protected void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
	{
		//UpdatePanel1.Update();
		//lblMessage.Text = string.Empty;
		//lblRemark.Text = string.Empty;
		//btnRegisterCall.Enabled = true;
		//ddlIssuesubRequestType.Items.Clear();
		//GetRequestType();
		//CommonUtility.AddAllToDropDown(ddlIssuesubRequestType);
	}
	#endregion

	#region ddlIssueRequestType SelectedIndexChanged
	protected void ddlIssueRequestType_SelectedIndexChanged(object sender, EventArgs e)
	{
		//UpdatePanel1.Update();
		//lblMessage.Text = string.Empty;
		//lblRemark.Text = string.Empty;
		//btnRegisterCall.Enabled = true;
		GetRequestSubType();
	}
	#endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
	 string Value=null;
        try
        {
            string UserRole = System.Web.Security.Roles.GetRolesForUser()[0].ToString();
            ReportsBAL objBAL = new ReportsBAL();
            DataSet dsFile = new DataSet();
            string strRegionID, strApplication, stempfile,strRequestType,strRequestSubType;
            DateTime strFromDate;
            DateTime strToDate;
            strRegionID = ddlRegion.SelectedValue;
            if (ddlIssueRequestType.Enabled == false)
            {
                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    if (CheckBoxList1.Items[i].Selected)
                    {
                        if (CheckBoxList1.Items[i].Value == "0")
                        {
                            Value = CheckBoxList1.Items[i].Value;
                            break;
                        }

                        if (Value == null)
                        {
                            Value = CheckBoxList1.Items[i].Value;
                        }
                        else
                        {
                            Value += "," + CheckBoxList1.Items[i].Value;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    if (CheckBoxList1.Items[i].Selected)
                    {
                        Value = CheckBoxList1.Items[i].Value;
                    }
                }
            }
            strApplication = Value;//ddlApplication.SelectedValue;
			strRequestType = ddlIssueRequestType.SelectedValue;
			strRequestSubType = ddlIssuesubRequestType.SelectedValue;
            //strFromDate = Convert.ToDateTime(CommonUtility.ConvertDateToMMddyyyy(txtFromDate.Text));
            //strToDate = Convert.ToDateTime(CommonUtility.ConvertDateToMMddyyyy(txtToDate.Text));
            strFromDate = Convert.ToDateTime(txtFromDate.Text);
            strToDate = Convert.ToDateTime(txtToDate.Text);
            string ReportUserWithRegionAllSelected = string.Empty;

            strToDate = strToDate.AddDays(1);

            if (UserRole.ToLower() == "admin")
            {
                ReportUserWithRegionAllSelected = "N";
                //dsFile = objBAL.GetReports(strRegionID,strApplication,strRequestType,strRequestSubType,strFromDate, strToDate, ReportUserWithRegionAllSelected);
                dsFile = GetReport(strRegionID, strApplication, strRequestType, strRequestSubType, strFromDate, strToDate, ReportUserWithRegionAllSelected);
            }
            else if (UserRole == "ReportFullAccess")
            {
                ReportUserWithRegionAllSelected = "N";
                //dsFile = objBAL.GetReports(strRegionID, strApplication, strRequestType, strRequestSubType, strFromDate, strToDate, ReportUserWithRegionAllSelected);
                dsFile = GetReport(strRegionID, strApplication, strRequestType, strRequestSubType, strFromDate, strToDate, ReportUserWithRegionAllSelected);
            }
            else if (ddlReportsOn.SelectedValue == "Region")
            {
                ReportUserWithRegionAllSelected = "Y";
                string strRegionCode = Session["RegionCode"].ToString();
                //dsFile = objBAL.GetReports(strRegionCode, strApplication, strRequestType, strRequestSubType, strFromDate, strToDate, ReportUserWithRegionAllSelected);
                dsFile = GetReport(strRegionCode, strApplication, strRequestType, strRequestSubType, strFromDate, strToDate, ReportUserWithRegionAllSelected);
            }
            else
            {
                string strRegionCode, strBranchCode;
                strBranchCode = ddlBranch.SelectedValue;
                strRegionCode = Session["RegionCode"].ToString();

                dsFile = objBAL.GetReportsByBranch(strRegionCode, strBranchCode, strApplication, strRequestType, strRequestSubType, strFromDate, strToDate);
            }

            stempfile = Session.SessionID + ".xml";
            dsFile.WriteXml(Server.MapPath(stempfile));
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";
            XmlDataDocument xdd = new XmlDataDocument(dsFile);
            XslCompiledTransform xt = new XslCompiledTransform();
            xt.Load(Server.MapPath("CallDeskAdmin.xsl"));
            xt.Transform(xdd, null, Response.OutputStream);
            File.Delete(Server.MapPath(stempfile));
            Response.End();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    public DataSet GetReport(string strRegionID, string strApplication, string strRequestType, string strRequestSubType, DateTime strFromDate, DateTime strToDate, string ReportUserWithRegionAllSelected)
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
            cmd.CommandText = "usp_GetCallDeskReports";
            cmd.CommandTimeout = 500;
            cmd.Parameters.AddWithValue("@RegionId", strRegionID);
            cmd.Parameters.AddWithValue("@ApplicationId", strApplication);
            cmd.Parameters.AddWithValue("@IssueRequestTypeId", strRequestType);
            cmd.Parameters.AddWithValue("@IssueRequestSubTypeId", strRequestSubType);
            cmd.Parameters.AddWithValue("@StartDate", strFromDate);
            cmd.Parameters.AddWithValue("@EndDate", strToDate);
            cmd.Parameters.AddWithValue("@Flag", ReportUserWithRegionAllSelected);
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
}
