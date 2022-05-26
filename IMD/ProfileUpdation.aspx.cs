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
using CallDeskBO;
using CallDeskBAL;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Mail;
using SMSSendService;

public partial class IMD_ProfileUpdation : System.Web.UI.Page
{
	SqlConnection DBConnection;
    #region Objects
    UserRoleBAL objBAL = new UserRoleBAL();
    EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(Session["AgentUserID"]!=null)
            {
                string strUserName = Session["AgentUserID"].ToString();
                txtUserNameLock.Text = strUserName;
                txtUserNameLock.Enabled = false;

                DataTable oDTEmp = new DataTable();
                oDTEmp = objEMBAL.GetLoggedEmployeeDetailsForASLC(strUserName, DateTime.Now.Date);

                txtEditEmail.Text = Convert.ToString(oDTEmp.Rows[0]["Email"]);
                txtEditMobileNo.Text = Convert.ToString(oDTEmp.Rows[0]["MobileNo"]);
                txtEditSMName.Text = Convert.ToString(oDTEmp.Rows[0]["SMName"]);
                txtEditSMCode.Text = Convert.ToString(oDTEmp.Rows[0]["SMCode"]);

            }
        }
    }
    #endregion

    #region btnSaveUserFields Click
    protected void btnSaveUserFields_Click(object sender, EventArgs e)
     {
         try
         {
             string uName = txtUserNameLock.Text.Trim();
             string uEmail = txtEditEmail.Text.Trim();
             string uMobileNo = txtEditMobileNo.Text.Trim();
             string uSMName = txtEditSMName.Text.Trim();
             string uSMCode = txtEditSMCode.Text.Trim();
             UpdateUserProfile(uName, uEmail, uMobileNo, uSMName, uSMCode);

             string strEmailText = string.Empty;
             string strSMSText = string.Empty;
             string strEmpName = string.Empty;
             DataTable oDtUser = GetUserDetailsByUserName(uName);
             strEmpName = Convert.ToString(oDtUser.Rows[0]["EmployeeName"]);

             if (uEmail != string.Empty)
             {
                 strEmailText = GetEmailTextUpdateProfile(strEmpName, uName, uEmail, uMobileNo, uSMName, uSMCode);
                 string strSubject = "Reliance CallDesk Profile Updated Details";
                 SendEmailToUser(uEmail, strSubject, strEmailText);
             }

			 
			// Added to call new SMS service 06-05-2016

             string App_Process = "User Management";
             string SMS_Event = "User Profile";
             string Ref_Value = Convert.ToString(Session["AgentUserID"]);
             string Ref_Name = "User Id";
             string Department = "1";

             // Added to call new SMS service 06-05-2016
			 
             if (uMobileNo != string.Empty)
             {
                 strSMSText = GetSMStextUpdation(strEmpName);
                 //SendSMSToUser(uMobileNo, strSMSText);
				 
				  Set_Message_InServer(uMobileNo, strSMSText, "", App_Process, SMS_Event, Ref_Name, Ref_Value, Department);
             }
             lblMessage.Text = "Profile Updated Successfully.";

         }
         catch (Exception ex)
         {
             lblMessage.Text = ex.Message;
         }
     }
     #endregion

    #region Update User Profile
    public void UpdateUserProfile(params object[] param)
    {
        try
        {
            DataUtils.ExecuteNonQuery("usp_UpdateUserProfile", param);

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    #endregion

    #region GetEmailTextUpdateProfile
    public string GetEmailTextUpdateProfile(string empName, string strUserName, string strEmailID, string strMobileNo, string strSMName, string strSMCode)
    {
        string strEmail = string.Empty;
        strEmail = @"Dear " + empName.ToUpper() + ",<br><br><br>";
        strEmail = strEmail + "Your profile updated successfully. Following are the profile details.<br><br>";
        strEmail = strEmail + "<b>Login Id</b>  : " + strUserName + "<br>";
        strEmail = strEmail + "<b>Agent Name</b>: " + empName + "<br>";
        strEmail = strEmail + "<b>User Email Id</b>: " + strEmailID + "<br>";
        strEmail = strEmail + "<b>User Mobile No</b>: " + strMobileNo + "<br>";
        strEmail = strEmail + "<b>Sales Manager Name</b>: " + strSMName + "<br>";
        strEmail = strEmail + "<b>Sales Manager Code</b>: " + strSMCode + "<br><br><br><br><br>";
        strEmail = strEmail + "Thank you<br>";
        strEmail = strEmail + "Reliance Application Support Team";
		strEmail = strEmail + "<br><br><br>";
		strEmail = strEmail + "Note - : All IMD Portal and POS user's please logging to your respective <br>";
		strEmail = strEmail + "POS and IM portal system and click on the call desk option made <br>";
		strEmail = strEmail + "available, to access your call desk application.";
        return strEmail;
    }
    #endregion         

    #region GetSMStextUpdation
    public string GetSMStextUpdation(string strEmpName)
    {
        string strSMS = string.Empty;
        strSMS = @"Dear <" + strEmpName.ToUpper() + ">, your profile is updated successfully with the required changes.Please login to http://calldesk.reliancegeneral.co.in, check details in update profile link.";
        return strSMS;
    }
    #endregion

    #region Get User Details By UserName
    public DataTable GetUserDetailsByUserName(string strUserName)
    {
        string strConnection = ConfigurationManager.ConnectionStrings["CallDeskDB"].ConnectionString;
        DataTable oDT = new DataTable();
        SqlConnection oCon = new SqlConnection();
        SqlCommand oComm = new SqlCommand();
        IDataReader reader = null;
        try
        {
            oCon.ConnectionString = strConnection;
            oCon.Open();
            oComm.Connection = oCon;
            oComm.CommandType = CommandType.StoredProcedure;
            oComm.CommandText = "usp_GetUserDetailsByUserName";
            oComm.Parameters.Add(new SqlParameter("@UserName", strUserName));
            reader = oComm.ExecuteReader();
            oDT.Load(reader);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }
            oCon.Close();
            oComm.Dispose();
        }
        return oDT;
    }
    #endregion

    #region Send Email To User
    
	 public void Set_Message_InServer(string PhoneNumber, string MessageText, string SubmittedTime
        , string App_Process, string SMS_Event, string Ref_Name, string Ref_Value, string Department
        )
    {

        try
        {

            string strUnique = Ref_Value + "-" + Guid.NewGuid().ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-"));

            if (strUnique.Length >= 20)
            {
                strUnique = strUnique.Substring(0, 20);
            }
            //SMS Web Service is upgraded to wcf effective 9th July 2009
            //SMSService.SingleMessage msg = new SMSService.SingleMessage();
            //SMSService.SendMessageClient SMS = new SMSService.SendMessageClient();

            //SMSSendService.SingleMessage msgData = new SMSSendService.SingleMessage();
            //SMSSendService.SendMessageClient sendMsg = new SMSSendService.SendMessageClient();


            SMSSendService.SingleMessage msgData = new SMSSendService.SingleMessage();
            SMSSendService.SendMessage sendMsg = new SMSSendService.SendMessage();

            //SingleMessage msg = new SingleMessage();
            msgData.Message = MessageText;            //Message to Send
            msgData.MobileNumber = PhoneNumber;                //Mobile Number - Should not be in DND list
            msgData.UserName = "intranet";
            msgData.Password = "rgiclintra07#";

            //[CR-253]-SMS Implementation mail - Provission for Event and Department Id-Start
            //msg.Department = "IT";
            msgData.App_Process = App_Process;
            msgData.SMS_Event = SMS_Event;
            msgData.Ref_Name = Ref_Name;
            msgData.Ref_Value = Ref_Value;
            msgData.Department = Department;
            msgData.Source_RequestID = strUnique;// Ref_Value;

            //[CR-253]-SMS Implementation mail - Provission for Event and Department Id-End




            string retVal = sendMsg.Send(msgData);

            DBConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CallDeskDB"].ConnectionString);
            SqlCommand DBcommand = new SqlCommand("Insert_SMS_LOG", DBConnection);
            DBcommand.CommandType = CommandType.StoredProcedure;
            DBConnection.Open();
            DBcommand.Parameters.AddWithValue("@System_RequestID", strUnique);
            DBcommand.Parameters.AddWithValue("@App_Process", App_Process);
            DBcommand.Parameters.AddWithValue("@SMS_Event", SMS_Event);
            DBcommand.Parameters.AddWithValue("@Ref_Name", Ref_Name);
            DBcommand.Parameters.AddWithValue("@Ref_Value", Ref_Value);
            DBcommand.Parameters.AddWithValue("@Department", Department);
            DBcommand.Parameters.AddWithValue("@Message", MessageText);
            DBcommand.Parameters.AddWithValue("@MobileNo", PhoneNumber);
            DBcommand.Parameters.AddWithValue("@SMS_TokenID", retVal);

            DBcommand.ExecuteNonQuery();


            //Code changes end
        }
        catch (Exception Ex)
        {
            string str;
            str = Ex.ToString();
        }
        finally
        {
            DBConnection.Close();
        }
    }
	
	public void SendEmailToUser(string strEmail, string strSubject, string strMailMsg)
    {
        try
        {
            MailMessage msg = new MailMessage();
            msg.BodyFormat = MailFormat.Html;
            msg.To = strEmail;
            msg.From = "calldesk@reliancegeneral.co.in";
            msg.Subject = strSubject;
            msg.Body = strMailMsg;
            SmtpMail.SmtpServer = "10.65.8.45";
            SmtpMail.Send(msg);
            //lblCreateUserMSG.Text = "Password sent to your email : " + strEmail;
        }
        catch
        {

        }
    }
    #endregion

    #region Send SMS To USer
    public void SendSMSToUser(string strMobileNo, string smsMsg)
    {
        try
        {
            SMSSendService.SingleMessage msgData = new SMSSendService.SingleMessage();
            SMSSendService.SendMessage sendMsg = new SMSSendService.SendMessage();
            msgData.Department = "IT";
            msgData.UserName = "intranet";
            msgData.Password = "rgiclintra07#";
            msgData.Message = smsMsg;
            msgData.MobileNumber = strMobileNo;
            sendMsg.Send(msgData);
        }
        catch
        {

        }
    }
    #endregion

}
