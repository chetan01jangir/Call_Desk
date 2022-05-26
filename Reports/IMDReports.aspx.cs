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
using CallDeskDAL;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

public partial class Reports_IMDReports : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //string UserRole = System.Web.Security.Roles.GetRolesForUser()[0].ToString();
		string strUserName = string.Empty;
		strUserName = Convert.ToString(Session["UserName"]);
		if (!IsPostBack)
		{
			//GetApplication();
			if (strUserName == "70008042" || strUserName== "rgiadmin" || strUserName== "70035081")
			{
				GetApplication();
				CommonUtility.AddAllToDropDown(ddlIssueRequestType);
				CommonUtility.AddAllToDropDown(ddlIssuesubRequestType);
				pnlNoAutho.Visible = false;
				pnlAutho.Visible = true;
			}
			else
			{
				pnlNoAutho.Visible = true;
				pnlAutho.Visible = false;
			}
		}
	}
    #endregion

    #region Method To Get Region

    //public void GetRegion()
    //{
    //    try
    //    {
    //        ddlRegion.Items.Clear();
    //        ddlRegion.DataTextField = "RegionName";
    //        ddlRegion.DataValueField = "RegionID_PK";
    //        ddlRegion.DataSource = GetAgentRegions();
    //        ddlRegion.DataBind();
    //        CommonUtility.AddAllToDropDown(ddlRegion);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //    }
    //}

    #endregion

    #region Method To Get Application Name

    public void GetApplication()
    {
        try
        {           
            ddlApplication.DataTextField = "ApplicationName";
            ddlApplication.DataValueField = "ApplicationID_PK";
            ddlApplication.DataSource = GetAgentApplications();
            ddlApplication.DataBind();
            CommonUtility.AddAllToDropDown(ddlApplication);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    #endregion

    #region btnSubmit Click
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //string UserRole = System.Web.Security.Roles.GetRolesForUser()[0].ToString();
            ReportsBAL objBAL = new ReportsBAL();
            DataSet dsFile = new DataSet();
            //string strRegionID, strApplication, stempfile;
            string strApplication, stempfile, strRequestType, strRequestSubType;
            DateTime strFromDate;
            DateTime strToDate;
            //strRegionID = ddlRegion.SelectedValue;
            strApplication = ddlApplication.SelectedValue;        
			strRequestType = ddlIssueRequestType.SelectedValue;
			strRequestSubType = ddlIssuesubRequestType.SelectedValue;
            strFromDate = Convert.ToDateTime(txtFromDate.Text);
            strToDate = Convert.ToDateTime(txtToDate.Text);
          

            strToDate = strToDate.AddDays(1);

            //dsFile = GetReportsForIMDs(Convert.ToInt32(strRegionID), Convert.ToInt32(strApplication), strFromDate, strToDate);
            dsFile = GetReportsForIMDs(Convert.ToInt32(strApplication),strRequestType,strRequestSubType, strFromDate, strToDate);


            stempfile = Session.SessionID + ".xml";
            dsFile.WriteXml(Server.MapPath(stempfile));
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";
            XmlDataDocument xdd = new XmlDataDocument(dsFile);
            XslCompiledTransform xt = new XslCompiledTransform();
            xt.Load(Server.MapPath("CallDeskIMDs.xsl"));
            xt.Transform(xdd, null, Response.OutputStream);
            File.Delete(Server.MapPath(stempfile));
            Response.End();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }


    }
    #endregion

    #region Get Agent Regions
    public DataTable GetAgentRegions()
    {
        DataTable oDTRegions = new DataTable();
        DataSet oDSRegions = new DataSet();
        try
        {
            oDSRegions = DataUtils.ExecuteDataset("usp_GetAgentRegions");
            oDTRegions = oDSRegions.Tables[0];
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return oDTRegions;
    }
    #endregion

    #region Get Agent Application
    public DataTable GetAgentApplications()
    {
        DataTable oDtApplication= new DataTable();
        DataSet oDsApplication = new DataSet();
        try
        {
            oDsApplication = DataUtils.ExecuteDataset("usp_GetAgentApplications");
            oDtApplication = oDsApplication.Tables[0];
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return oDtApplication;

    }
    #endregion

    #region GetReportsForIMDs
    public DataSet GetReportsForIMDs(params object[] param)
    {
        DataSet oDSReports = new DataSet();
        try
        {
            oDSReports = DataUtils.ExecuteDataset("usp_GetIMDReports", param);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return oDSReports;
    }
    #endregion

	#region Method To Get Request Type

	public void GetRequestType()
	{
		try
		{
			RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
			ddlIssueRequestType.DataTextField = "IssueRequestType";
			ddlIssueRequestType.DataValueField = "IssueRequestType_PK";
			ddlIssueRequestType.DataSource = objBAL.GetIssueRequestTypeByApplicationType(Convert.ToInt32(ddlApplication.SelectedItem.Value));
			ddlIssueRequestType.DataBind();
			CommonUtility.AddAllToDropDown(ddlIssueRequestType);
		}
		catch (Exception ex)
		{
			lblMessage.Text = ex.Message;
		}
	}

	#endregion

	#region Method To Get Request Sub Type

	public void GetRequestSubType()
	{
		try
		{
			RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
			ddlIssuesubRequestType.DataTextField = "IssueRequestSubType";
			ddlIssuesubRequestType.DataValueField = "IssueRequestSubType_PK";
			ddlIssuesubRequestType.DataSource = objBAL.GetIssueRequestSubTypeByIssueRequestType(Convert.ToInt32(ddlApplication.SelectedItem.Value), Convert.ToInt32(ddlIssueRequestType.SelectedItem.Value));
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
		ddlIssuesubRequestType.Items.Clear();
		GetRequestType();
		CommonUtility.AddAllToDropDown(ddlIssuesubRequestType);
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


}
