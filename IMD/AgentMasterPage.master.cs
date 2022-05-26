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
using System.IO;

public partial class UpdateProfile_AgentMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetLoggedAgentDetails();
        }
    }

    #region Get Logged in User Details

    public void GetLoggedAgentDetails()
    {
        EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
        DataTable dtLED = new DataTable();
        string UserName = Convert.ToString(Session["AgentUserID"]);
        //UserName = Membership.GetUser().UserName;
        try
        {
            if (Session["AgentUserID"] == null)
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
        catch (Exception ex)
        {
            FileStream fs1 = new FileStream(Server.MapPath("..\\Files") + "\\" + "RegisterCall.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw1 = new StreamWriter(fs1);
            sw1.Write("\r\n =======================================================================================");
            sw1.Write("\r\n Log Entry On : " + System.DateTime.Now);
            sw1.Write("\n " + ex.Message);
            sw1.Write("\n " + ex.StackTrace);
            sw1.Write("\n UserName " + UserName);
            //sw1.Write("\n " + strReturnVal);
            sw1.Close();
            fs1.Close();
        }

    }

    #endregion

    protected void lsLoginStatus_LoggingOut(object sender, EventArgs e)
    {
        try
        {
            EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
            if (Session["EmployeeName"] != null)
            {
                objEMBAL.InsertLastLoginLogOut(Session["AgentUserID"].ToString(), Convert.ToDateTime(Session["UserJustLoginDate"]), DateTime.Now);
            }
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
        catch (Exception ex)
        {
            //lblMessage.Text = ex.Message;
            FileStream fs1 = new FileStream(Server.MapPath("..\\Files") + "\\" + "RegisterCall.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw1 = new StreamWriter(fs1);
            sw1.Write("\r\n =======================================================================================");
            sw1.Write("\r\n Log Entry On : " + System.DateTime.Now);
            sw1.Write("\n " + ex.Message);
            sw1.Write("\n " + ex.StackTrace);
            //sw1.Write("\n " + ex.InnerException);
            //sw1.Write("\n " + strReturnVal);
            sw1.Close();
            fs1.Close();
        }
    }


}
