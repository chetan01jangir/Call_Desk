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
using System.Collections.Generic;
using CallDeskBO;
using CallDeskBAL;

public partial class UserControls_SearchmoreUserMapping : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    #endregion

    #region Search Button Event
    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        BindToGrid();
    }
    #endregion

    #region Grid View Page Index Changing Event
    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvData.PageIndex = e.NewPageIndex;
            BindToGrid();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
    #endregion

    #region Bing Grid Code

    public void BindToGrid()
    {
        EmployeeSearchBO objESBO = new EmployeeSearchBO();
        EmployeeSearchBAL objESBAL = new EmployeeSearchBAL();

        try
        {
            objESBO.SearchValue = txtValue.Text;

            if (ddlCriteria.SelectedValue == "1")
                objESBO.SearchBy = "0";
            else
                objESBO.SearchBy = "1";

            gvData.DataSource = objESBAL.GetSearchedEmployeeDetails(objESBO.SearchBy, objESBO.SearchValue);
            gvData.DataBind();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    #endregion

    #region Grid View Row Data Bount Event

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblCode = new Label();
            LinkButton lnkCode = new LinkButton();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lblCode = (Label)e.Row.FindControl("lblCode");
                lnkCode = (LinkButton)e.Row.FindControl("lnkCode");
                string strTextBox = "";
                if (Request.QueryString["TextboxName"] != null)
                    strTextBox = Request.QueryString["TextboxName"].ToString();

                lnkCode.Attributes.Add("onclick", "javascript:funSetCode('" + strTextBox + "','" + lblCode.Text + "')");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    #endregion
    
}
