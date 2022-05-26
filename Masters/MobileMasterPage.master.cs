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
using System.Text;
using System.Collections.Generic;
using CallDeskBAL;

public partial class Masters_MobileMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetLoggedUserDetails();
        }


    }

    #region Get Logged in User Details

    public void GetLoggedUserDetails()
    {
        EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
        DataTable dtLED = new DataTable();
        string UserName;
        UserName = Membership.GetUser().UserName;
        if (Session["EmployeeName"] == null)
        {
            dtLED = objEMBAL.GetLoggedEmployeeDetailsForASLC(UserName, DateTime.Now.Date);
            Session["EmployeeName"] = dtLED.Rows[0]["EmployeeName"].ToString();
            Session["EmployeeDesignation"] = dtLED.Rows[0]["EmployeeDesignation"].ToString();
            Session["BranchCode"] = dtLED.Rows[0]["BranchCode"].ToString();
            Session["SMChannel"] = dtLED.Rows[0]["SMChannel"].ToString();
            Session["LoggedBranch"] = dtLED.Rows[0]["BranchCode"].ToString(); // Same as Default branch
            Session["LoweredEmail"] = dtLED.Rows[0]["LoweredEmail"].ToString();
            Session["BranchName"] = dtLED.Rows[0]["BranchName"].ToString();
            Session["RegionCode"] = dtLED.Rows[0]["RegionCode"].ToString();
            Session["RegionName"] = dtLED.Rows[0]["RegionName"].ToString();
            Session["LastPasswordChangedDate"] = dtLED.Rows[0]["LastPasswordChangedDate"].ToString();
            Session["PasswordChangeDaysAllowed"] = dtLED.Rows[0]["PasswordChangeDaysAllowed"].ToString();
            Session["LocationTypeID"] = dtLED.Rows[0]["LocationTypeID"].ToString();
            Session["SMBranchName"] = dtLED.Rows[0]["SMBranchName"].ToString();
            Session["UserName"] = dtLED.Rows[0]["UserName"].ToString();
            Session["OfficeType"] = dtLED.Rows[0]["OfficeType"].ToString();
            Session["EmployeeFunction"] = dtLED.Rows[0]["Employee_Function"].ToString();
            Session["SMChannel"] = dtLED.Rows[0]["SM_Channel"].ToString();



        }
    }
    #endregion
}
