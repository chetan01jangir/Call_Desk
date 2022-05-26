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
using CallDeskBAL;

public partial class Swap_SwapBranch : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);

        lblerror.Text = "";

        if (!IsPostBack)
        {
            GetBranchForSwapping();
        }
    }
    #endregion  Page Load

    #region Get Branch for Swapping

    public void GetBranchForSwapping()
    {
        try
        {
            EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
            string strUserName = Membership.GetUser().UserName;

            ddlBranch.DataSource = objEMBAL.GetBranchForSwapping(strUserName);
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();

            CommonUtility.AddSelectToDropDown(ddlBranch);
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
    }

    #endregion  Get Branch for Swapping

    #region Button Save

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
            LocationBAL objLoc = new LocationBAL();
            DataSet dsRegionDetails = new DataSet();
            string strUserName = Membership.GetUser().UserName;
            string strBranchName = ddlBranch.SelectedItem.Text;
            string strBranchCode = ddlBranch.SelectedValue;

            int intReturnValue = objEMBAL.UpdateUserSwapBranch(strUserName, strBranchCode);
            dsRegionDetails = objLoc.GetRegionByBranchCode(strBranchCode);
            //if (intReturnValue == 3)
            //{
            //Response.Redirect("SwapBranch.aspx");
            ((Label)this.Master.FindControl("lblBranchName")).Text = strBranchName;
            Session["BranchCode"] = strBranchCode;
            Session["BranchName"] = strBranchName;
            Session["LoggedBranch"] = strBranchCode;
            Session["RegionCode"] = dsRegionDetails.Tables[0].Rows[0]["RegionCode"].ToString();
            Session["RegionName"] = dsRegionDetails.Tables[0].Rows[0]["RegionName"].ToString();
            Session["LocationTypeID"] = dsRegionDetails.Tables[0].Rows[0]["TypeId"].ToString();
            //}
            //else
            //{
            //    lblerror.Text = intReturnValue.ToString();
            //}
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
    }

    #endregion  Button Save

}
