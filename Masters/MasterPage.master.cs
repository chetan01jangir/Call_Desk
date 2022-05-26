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

public partial class MasterPage : System.Web.UI.MasterPage
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
            dtLED = objEMBAL.GetLoggedEmployeeDetailsForASLC(UserName,DateTime.Now.Date);
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
			//LM
			Session["OfficeType"] = dtLED.Rows[0]["OfficeType"].ToString();
			Session["EmployeeFunction"] = dtLED.Rows[0]["Employee_Function"].ToString();
			Session["SMChannel"] = dtLED.Rows[0]["SM_Channel"].ToString();
			//
            int intAD = Convert.ToInt32(CommonUtility.GetValueByKey("PasswordChangeDaysAllowed"));
            if (string.IsNullOrEmpty(dtLED.Rows[0]["UserLastLogin"].ToString()))
            {

                lblLastLogin.Visible = false;
                lblLoginVal.Visible = false;
            }
            else
            {
                Session["UserLastLogin"] = dtLED.Rows[0]["UserLastLogin"].ToString();
                lblLoginVal.Text =Session["UserLastLogin"].ToString(); 

            }

            if (string.IsNullOrEmpty(dtLED.Rows[0]["UserLastLogOut"].ToString()))
            {
                lblLastLogOut.Visible = false;
                lblLogOutVal.Visible = false;
            }
            else
            {
                Session["UserLastLogOut"] = dtLED.Rows[0]["UserLastLogOut"].ToString();
                lblLogOutVal.Text = Session["UserLastLogOut"].ToString();
            }


            
            if (Session["BranchName"].ToString() == "Agent Branch")
            {
                lblLoggedBranchName.Text = "Logged Branch :";
                lblBranchName.Text = Session["SMBranchName"].ToString();
            }
            else
            {
                lblLoggedBranchName.Text = "Logged Branch :";
                lblBranchName.Text = Session["BranchName"].ToString();
            }


            lblUserName.Text = Session["EmployeeName"].ToString().ToUpper();
        
        }

        if (Request.Url.AbsolutePath.Contains("User/ChangePassword.aspx") == false)
        {
            // commented as AD authencation done so password expiry prompt disabled
            //int intAD = Convert.ToInt32(CommonUtility.GetValueByKey("PasswordChangeDaysAllowed"));
            ////int intAD = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordChangeDaysAllowed"].ToString());
            //if (Convert.ToInt32(Session["PasswordChangeDaysAllowed"]) > intAD)
            //{
            //    Response.Redirect("~/User/ChangePassword.aspx", false);
            //}
        }

        if (Session["BranchName"].ToString() == "Agent Branch")
        {
            lblLoggedBranchName.Text = "Logged Branch :";
            lblBranchName.Text = Session["SMBranchName"].ToString();
        }
        else
        {
            lblLoggedBranchName.Text = "Logged Branch :";
            lblBranchName.Text = Session["BranchName"].ToString();
        }


        lblUserName.Text = Session["EmployeeName"].ToString().ToUpper();

        dtLED = objEMBAL.GetLoggedEmployeeDetailsForASLC(UserName, DateTime.Now.Date);
        if (string.IsNullOrEmpty(dtLED.Rows[0]["UserLastLogin"].ToString()))
        {

            lblLastLogin.Visible = false;
            lblLoginVal.Visible = false;
        }
        else
        {
            Session["UserLastLogin"] = dtLED.Rows[0]["UserLastLogin"].ToString();
            lblLoginVal.Text = Session["UserLastLogin"].ToString();

        }

        if (string.IsNullOrEmpty(dtLED.Rows[0]["UserLastLogOut"].ToString()))
        {
            lblLastLogOut.Visible = false;
            lblLogOutVal.Visible = false;
        }
        else
        {
            Session["UserLastLogOut"] = dtLED.Rows[0]["UserLastLogOut"].ToString();
            lblLogOutVal.Text = Session["UserLastLogOut"].ToString();
        }

    }

    #endregion

    //protected void lsLoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
    //{
    //    EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
    //    objEMBAL.InsertLastLoginLogOut(Membership.GetUser().UserName, "LogOut");
    //    Session.Abandon();      
    //}
    protected void lsLoginStatus_LoggingOut(object sender, EventArgs e)
    {
        EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
        //objEMBAL.InsertLastLoginLogOut(Membership.GetUser().UserName, "LogOut");       
		if (Session["EmployeeName"] != null)
		{
			objEMBAL.InsertLastLoginLogOut(Membership.GetUser().UserName, Convert.ToDateTime(Session["UserJustLoginDate"]), DateTime.Now);
		}

        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        FormsAuthentication.SignOut();        
        Response.Redirect("~/Login.aspx", true);
    }

    private void ExpireAllCookies()
    {
        if (HttpContext.Current != null)
        {
            int cookieCount = HttpContext.Current.Request.Cookies.Count;
            for (var i = 0; i < cookieCount; i++)
            {
                var cookie = HttpContext.Current.Request.Cookies[i];
                if (cookie != null)
                {
                    var cookieName = cookie.Name;
                    var expiredCookie = new HttpCookie(cookieName) { Expires = DateTime.Now.AddDays(-1) };
                    HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                }
            }

            // clear cookies server side
            HttpContext.Current.Request.Cookies.Clear();
            HttpContext.Current.Response.Cookies.Clear();
        }
    } 

   protected void trv_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (trv.SelectedNode.Text == "Feedback")
        {
            Session["Type"] = "Feedback";
            Response.Redirect("~/Default.aspx");
        }
    }
   

   

}
