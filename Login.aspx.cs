using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Mail;
using System.Collections.Generic;
using System.Net;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CallDeskBAL;
using PasswordGeneratorCode;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using CallDeskDAL;
using CallDeskBO;
using System.Security.Principal;
using System.DirectoryServices;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    #region Page Load Event

    int intCheck = 0;
    string strAppName = "CallDeskOnline";
    string strPwdSalt = "ril@123";
    string strPasswordAnswer = null;
    int intMaxInvalidPasswordAttempts = 5;
    int intPasswordAttemptWindow = 0;
    int intPasswordFormat = 0;
    int i = 0;
    int captch = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((!IsPostBack) && (Request.QueryString["ReturnURL"] != null))
        {
            //Session.Abandon();
            //Response.Redirect(FormsAuthentication.LoginUrl, true);
        }
        if (!IsPostBack)
        {
            GenerateImage();
        }
        //[CR-13] Call Desk Message Display Start

        //DataSet dsDisplayMsg = new DataSet();

        //dsDisplayMsg = GetCallDeskMessage("");

        //lblDisplayMsg.Text = Convert.ToString(dsDisplayMsg.Tables[0].Rows[0]["DisplayMsg"]);

        //if(string.IsNullOrEmpty(lblDisplayMsg.Text))
        //{
        //    divDispayMsg.Visible=false;
        //}

        // [CR-13] Call Desk Message Display End


        //        if (GetWindosUser() == "70008042")
        //        {
        //
        //            LinkButton lnkSignUp = (LinkButton)lgLogin.FindControl("lnkSignUp");
        //            lnkSignUp.Visible = true;
        //        }
        //        else
        //        {
        //            LinkButton lnkSignUp = (LinkButton)lgLogin.FindControl("lnkSignUp");
        //            lnkSignUp.Visible = false;
        //        }

        //		if (Request.QueryString["a"] != null)
        //		{
        //
        //			string user = Convert.ToString(Request.QueryString["a"]);
        //
        //			if (user == "s")
        //			{
        //				LinkButton lnkSignUp = (LinkButton)lgLogin.FindControl("lnkSignUp");
        //				lnkSignUp.Visible = true;
        //				trMarquee.Visible = true;
        //			}
        //			else
        //			{
        //				LinkButton lnkSignUp = (LinkButton)lgLogin.FindControl("lnkSignUp");
        //				lnkSignUp.Visible = false;
        //				trMarquee.Visible = false;
        //			}
        //		}
        //		else
        //		{
        //			LinkButton lnkSignUp = (LinkButton)lgLogin.FindControl("lnkSignUp");
        //			lnkSignUp.Visible = false;
        //			trMarquee.Visible = false;
        //		}

        LinkButton lnkSignUp = (LinkButton)lgLogin.FindControl("lnkSignUp");
        lnkSignUp.Visible = false;
        trMarquee.Visible = false;


        if (Request.QueryString["AppType"] != null && Request.QueryString["UserID"] != null)
        {
            Session["AgentUserID"] = null;
            Session["AppType"] = null;

            //string strAppType = Convert.ToString(Request.QueryString["AppType"]);
            //string strUserID = Convert.ToString(Request.QueryString["UserID"]);            
            string strAppTypeencrypt = Request.QueryString["AppType"];
            string strUserIDn = Convert.ToString(Request.QueryString["UserID"]);
            string strUserIDencrypt = Convert.ToString(Request.QueryString["UserID"]);
            //added by shilpa for csc requirement
            string cscchannel = Convert.ToString(Request.QueryString["IsCSCChannel"]);
            string CSCCompanyName = Convert.ToString(Request.QueryString["CSCCompanyName"]);
            string RAPId = Convert.ToString(Request.QueryString["RAPId"]);

            Session["AppType"] = strAppTypeencrypt;

            UserRoleBAL objBAL = new UserRoleBAL();

            try
            {
                if (strAppTypeencrypt.ToLower() != "pos" && strAppTypeencrypt.ToLower() != "imd" && strAppTypeencrypt.ToLower() != "portal" && strAppTypeencrypt.ToLower() != "smartzone" && strAppTypeencrypt.ToLower() != "" && strAppTypeencrypt.ToLower() != "Smartzone - CSC" && strAppTypeencrypt.ToLower() != "Smartzone - Non CSC" && strAppTypeencrypt.ToLower() != "Smartzone - Tele Sales")
                //if (strAppTypeencrypt != "POS" && strAppTypeencrypt != "IMD" && strAppTypeencrypt != "Portal"  )
                {
                    strUserIDn = Decrypt12(strUserIDencrypt);
                    // strAppTypeencrypt = Decrypt12(strAppTypeencrypt);
                }
            }
            catch (Exception ex)
            {
                //lblMessage.Text=ex.Message;
                WriteToEvent(ex.Message, ex.Message, ex.Message);
            }
            string strReturnValue = objBAL.CheckExistingMember(strUserIDn);
            ///// for simple smarzone
            //if (strReturnValue == "1" && cscchannel=="0")
            //{
            //    Session["AgentUserID"] = strUserIDn;
            //    EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
            //    objEMBAL.InsertLastLoginLogOut(strUserIDn, DateTime.Now, DateTime.Now);
            //    Session["UserJustLoginDate"] = DateTime.Now;
            //    Response.Redirect("IMD/Default.aspx");
            //}

            ////smartzone csc
            //else if (strReturnValue == "1" && cscchannel == "1" && CSCCompanyName=="CSC")
            //{
            //    Session["AgentUserID"] = strUserIDn;
            //    EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
            //    objEMBAL.InsertLastLoginLogOut(strUserIDn, DateTime.Now, DateTime.Now);
            //    Session["UserJustLoginDate"] = DateTime.Now;
            //    Response.Redirect("IMD/Default.aspx");
            //}
            //// smartzone vakrangee
            //else if (strReturnValue == "1" && cscchannel == "1" && CSCCompanyName == "Vakregnee")
            //{
            //    Session["AgentUserID"] = strUserIDn;
            //    EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
            //    objEMBAL.InsertLastLoginLogOut(strUserIDn, DateTime.Now, DateTime.Now);
            //    Session["UserJustLoginDate"] = DateTime.Now;
            //    Response.Redirect("IMD/Default.aspx");
            //}
            /// for simple smarzones


            if (strReturnValue == "1" && strAppTypeencrypt == "yLjJXcAxyt8Gc0q1SGj6Ig==")
            {
                Session["AgentUserID"] = strUserIDn;
                EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
                objEMBAL.InsertLastLoginLogOut(strUserIDn, DateTime.Now, DateTime.Now);
                Session["UserJustLoginDate"] = DateTime.Now;
                Response.Redirect("IMD/Default.aspx");
            }

            //smartzone csc
            else if (strReturnValue == "1" && strAppTypeencrypt == "tZ2Ql/KkCzv96pEuECEJnQ==")
            {
                Session["AgentUserID"] = strUserIDn;
                EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
                objEMBAL.InsertLastLoginLogOut(strUserIDn, DateTime.Now, DateTime.Now);
                Session["UserJustLoginDate"] = DateTime.Now;
                Response.Redirect("IMD/Default.aspx");
            }
            // smartzone vakrangee
            else if (strReturnValue == "1" && strAppTypeencrypt == "AywZs7j7i4q0j28NTTqjMSYQL JgCErDD/QOA9TXMuA=")
            {
                Session["AgentUserID"] = strUserIDn;
                EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
                objEMBAL.InsertLastLoginLogOut(strUserIDn, DateTime.Now, DateTime.Now);
                Session["UserJustLoginDate"] = DateTime.Now;
                Response.Redirect("IMD/Default.aspx");
            }
            else if (strReturnValue == "1" && strAppTypeencrypt == "AywZs7j7i4q0j28NTTqjMSYQL+JgCErDD/QOA9TXMuA=")
            {
                Session["AgentUserID"] = strUserIDn;
                EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
                objEMBAL.InsertLastLoginLogOut(strUserIDn, DateTime.Now, DateTime.Now);
                Session["UserJustLoginDate"] = DateTime.Now;
                Response.Redirect("IMD/Default.aspx");
            }
            // Smartzone-Tele Sales
            else if (strReturnValue == "1" && strAppTypeencrypt == "RQFzwBcM9v01JvVikOfoSsRG0k7aHzUQF6sEKIKzgsw=")
            {
                Session["AgentUserID"] = strUserIDn;
                EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
                objEMBAL.InsertLastLoginLogOut(strUserIDn, DateTime.Now, DateTime.Now);
                Session["UserJustLoginDate"] = DateTime.Now;
                Response.Redirect("IMD/Default.aspx");
            }

            else
            {
                Response.Redirect("IMD/UpdateProfile.aspx?AppType=" + strAppTypeencrypt + "&UserID=" + strUserIDencrypt, false);
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {

        //        if (DateTime.Now > System.Convert.ToDateTime("01/09/2013 09:00:00 PM") && DateTime.Now < System.Convert.ToDateTime("01/09/2013 10:00:00 PM"))
        //        {
        //            Button oBtn = (Button)lgLogin.FindControl("LoginButton");
        //            oBtn.Enabled = false;
        //        }
        //        else
        //        {
        //            Button oBtn = (Button)lgLogin.FindControl("LoginButton");
        //            oBtn.Enabled = true;
        //        }

        //		if (DateTime.Now > System.Convert.ToDateTime("03/01/2013 08:00:00 PM"))
        //		{
        //			tdMsg.Visible = false;
        //		}
        //		else
        //		{
        //			tdMsg.Visible = true;
        //		}
        tdMsg.Visible = false;

    }


    #endregion

    #region Forgot Password Code

    protected void lnkForgotPwd_Click(object sender, EventArgs e)
    {

        UserBAL objBAL = new UserBAL();
        UserRoleBAL objRBAL = new UserRoleBAL();
        //string strUserName = lgLogin.UserName;
        string strUserName = HttpUtility.HtmlEncode(lgLogin.UserName);
        //StringBuilder sb = new StringBuilder(
        //                   HttpUtility.HtmlEncode(lgLogin.UserName));
        //// Selectively allow  <b> and <i>
        // sb.Replace("&lt;b&gt;", "<b>");
        // sb.Replace("&lt;/b&gt;", "");
        //  sb.Replace("&lt;i&gt;", "<i>");
        // sb.Replace("&lt;/i&gt;", "");

        // string strUserName = sb.ToString();


        try
        {
            lblMessage.Text = "";
            string strReturnVal = objRBAL.CheckExistingMember(strUserName);
            if (strReturnVal != "1")
            {
                lblMessage.Text = "User Name " + '"' + strUserName + '"' + " does not exists in the system.Please enter correct User Name";
            }
            else
            {
                int NoofAttempt = 0;
                EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
                DataTable dtLED = new DataTable();
                string strOldPassword = null;
                dtLED = objEMBAL.GetLoggedEmployeeDetailsForASLC(strUserName, DateTime.Now.Date);
                strOldPassword = Convert.ToString(dtLED.Rows[0]["Password"]);
                string strEmail = Convert.ToString(dtLED.Rows[0]["LoweredEmail"]);

                ViewState["dtLED"] = dtLED;

                //[CR-18] vulnaribility account lock Start

                //if (dtLED.Rows.Count > 0)
                //{
                //    if (dtLED.Rows[0]["Attempt"] == null || dtLED.Rows[0]["Attempt"].ToString() == string.Empty)
                //    {
                //        NoofAttempt = 0;
                //    }
                //    else
                //    {
                //        NoofAttempt = Convert.ToInt32(dtLED.Rows[0]["Attempt"]);
                //    }
                //}

                //int intFailedPasswordAttemptCount = Convert.ToInt32(dtLED.Rows[0]["FailedPasswordAttemptCount"]);

                //if (intFailedPasswordAttemptCount < 5)
                //{
                //    if (NoofAttempt >= 3)
                //    {
                //        //lblMessage.Text = "Your account has been locked due to three incorrect login attempts. Please Contact administrator";
                //        lblMessage.Text = "<nobr>User locked due to worng Attempt please contact <br>Rgicl.Applnsupport@Relianceada.com / contact no - 022-30380406</nobr>";

                //    }
                //    else
                //    {

                //        if (strEmail == "")
                //        {
                //            lblMessage.Text = "Your email id is not available in calldesk";
                //            return;
                //        }
                //        else
                //        {
                //            lblEmailMessage.Text = "Password will be sent to email :";
                //            lblUserEmail.Text = strEmail;
                //            //divConfirm.Visible = true; 
                //            divConfirm.Style.Add("display", "block");
                //            btnOk.Style.Add("display", "block");
                //        }
                //    }
                //}
                //else
                //{
                //    lblMessage.Text = "The Account has been locked, ask the administrator to unlock.";
                //}

                if (strEmail == "")
                {
                    lblMessage.Text = "Your email id is not available in calldesk";
                    return;
                }
                else
                {
                    lblEmailMessage.Text = "Please Check UserID and Password";
                    //lblUserEmail.Text = strEmail;
                    //divConfirm.Visible = true; 
                    divConfirm.Style.Add("display", "block");
                    btnOk.Style.Add("display", "block");
                }

                //[CR-18] vulnaribility account lock End
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region OK Button Code

    protected void btnOk_Click(object sender, EventArgs e)
    {
        //btnOk.Attributes.Add("onClientClick", " return VisibleFalse();");
        //Page.RegisterStartupScript("txt", "<script>alert('Hello')</script>");

        UserBAL objBAL = new UserBAL();
        int intReturnVal;
        string strOldPassword = null;
        string strCurrentTimeUtc = System.DateTime.Now.ToString();
        //string strUserName =sb.ToString();//lgLogin.UserName;
        string strUserName = HttpUtility.HtmlEncode(lgLogin.UserName);
        // StringBuilder sb = new StringBuilder(
        //                  HttpUtility.HtmlEncode(lgLogin.UserName));
        //// Selectively allow  <b> and <i>
        //sb.Replace("&lt;b&gt;", "<b>");
        //sb.Replace("&lt;/b&gt;", "");
        //sb.Replace("&lt;i&gt;", "<i>");
        //sb.Replace("&lt;/i&gt;", "");

        //string strUserName = sb.ToString();

        try
        {
            i = i + 1;
            AlphaNumericPasswordGenerator objPGC = new AlphaNumericPasswordGenerator();
            string strNewPassword, strEncryptedPassword;
            strNewPassword = objPGC.Generate(8);
            //strEncryptedPassword = CallDeskEncrypt(strNewPassword);
            strEncryptedPassword = SHAEncription(strNewPassword);
            DataTable dtLED = new DataTable();



            dtLED = (DataTable)ViewState["dtLED"];
            strOldPassword = Convert.ToString(dtLED.Rows[0]["Password"]);
            string strEmail = Convert.ToString(dtLED.Rows[0]["LoweredEmail"]);

            intReturnVal = objBAL.ResetPassword(strAppName, strUserName, strEncryptedPassword, intMaxInvalidPasswordAttempts, intPasswordAttemptWindow, strPwdSalt, strCurrentTimeUtc, intPasswordFormat, strPasswordAnswer);
            intCheck = 1;

            if (intReturnVal == 1)
            {

                //[CR-18] vulnaribility Failed login Start
                //EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
                //objEMBAL.DeleteLoginAttemptDetails(lgLogin.UserName);
                //[CR-18] vulnaribility Failed login End

                string FromAddress = "calldesk@reliancegeneral.co.in";
                //string ToAddress = "abc.Xyz@relianceada.com";

                string msgBody = "Password : " + strNewPassword + "\nThe request has been issued from machine with IP :" + Request.UserHostAddress + ". \nThis is an autogenerated mail please do not Reply.";
                System.Net.Mail.MailMessage objMailMessage = new System.Net.Mail.MailMessage();
                objMailMessage.From = new System.Net.Mail.MailAddress(FromAddress);
                objMailMessage.To.Add(strEmail);
                objMailMessage.Body = msgBody;
                objMailMessage.Subject = "Call Desk New Password";
                objMailMessage.IsBodyHtml = true;
                System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient("10.65.8.45");
                //System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient("10.185.6.109");
                mailClient.Send(objMailMessage);



                lblMessage.Text = "Password sent to your email : " + strEmail;
            }
            else
            {
                intReturnVal = objBAL.ResetPassword(strAppName, strUserName, strOldPassword, intMaxInvalidPasswordAttempts, intPasswordAttemptWindow, strPwdSalt, strCurrentTimeUtc, intPasswordFormat, strPasswordAnswer);
                lblMessage.Text = "Password could not be sent to your email.";
            }
            //divConfirm.Visible = false;
            divConfirm.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            System.IO.FileStream fs1 = new System.IO.FileStream(Server.MapPath("..\\Files") + "\\" + "CustomError.txt", System.IO.FileMode.Append, System.IO.FileAccess.Write);
            System.IO.StreamWriter sw1 = new System.IO.StreamWriter(fs1);
            sw1.Write("\r\n =======================================================================================");
            sw1.Write("\r\n Login Mail Error");
            sw1.Write("\r\n Log Entry On : " + System.DateTime.Now);
            sw1.Write("\n " + ex.Message);
            sw1.Close();
            fs1.Close();

            if (intCheck == 1)
            {
                intReturnVal = objBAL.ResetPassword(strAppName, strUserName, strOldPassword, intMaxInvalidPasswordAttempts, intPasswordAttemptWindow, strPwdSalt, strCurrentTimeUtc, intPasswordFormat, strPasswordAnswer);
            }
            divConfirm.Style.Add("display", "none");
            lblMessage.Text = ex.Message + " Number of Click on ok button  " + i.ToString();
        }
    }

    #endregion

    #region Cancel Button Code

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //divConfirm.Visible = false;
            divConfirm.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region On Auhenticate Event Handler

    protected void OnAuthenticate(object sender, AuthenticateEventArgs e)
    {
        try
        {
            if (CaptcahVerify() == false)
            {
                lgLogin.FailureText = "Image code is not valid.";
                TextBox tb = (TextBox)lgLogin.FindControl("Password");
                tb.Focus();
            }
            else
            {
                lblMessage.Text = string.Empty;
                EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
                DataTable dtLED = new DataTable();
                string strUserName, strPassword, strEnteredPassword;
                int NoofAttempt = 0;
                strUserName = lgLogin.UserName;
                //strUserName =  HttpUtility.HtmlEncode(lgLogin.UserName);
                //StringBuilder sb = new StringBuilder(
                //                   HttpUtility.HtmlEncode(lgLogin.UserName));
                //// Selectively allow  <b> and <i>
                //sb.Replace("&lt;b&gt;", "<b>");
                //sb.Replace("&lt;/b&gt;", "");
                // sb.Replace("&lt;i&gt;", "<i>");
                //sb.Replace("&lt;/i&gt;", "");

                //strUserName = sb.ToString();

                //DateTime dt = DateTime.Now;
                //DateTime dt1 = DateTime.Parse(abc.Value);
                //var seconds = System.Math.Abs((dt - dt1).TotalSeconds);

                //if (seconds > 1007)
                //{
                //    return;
                //}

                e.Authenticated = false;
                lgLogin.FailureText = null;

                dtLED = objEMBAL.GetLoggedEmployeeDetailsForASLC(strUserName, DateTime.Now.Date);
                //Nullable<DateTime> dtCalculatedDate;
                Nullable<DateTime> dtExpirationDate;

                if (dtLED.Rows.Count > 0)
                {
                    //************************** 45 days logic *************************************
                    //if (dtLED.Rows[0]["UserLastLoginDateTime"] == null || dtLED.Rows[0]["UserLastLoginDateTime"].ToString() == string.Empty)
                    //{
                    //    dtCalculatedDate = null;
                    //}
                    //else
                    //{
                    //    DateTime userLastLogoutDate = Convert.ToDateTime(dtLED.Rows[0]["UserLastLoginDateTime"]);
                    //    double noOfDaysForInactiveUser = Convert.ToDouble(CommonUtility.GetValueByKey("DefaultDayForInactiveUser"));
                    //    dtCalculatedDate = (Nullable<DateTime>)userLastLogoutDate.AddDays(noOfDaysForInactiveUser);
                    //}
                    //************************************************************************************


                    if (dtLED.Rows[0]["UserExpirationDate"] == null || dtLED.Rows[0]["UserExpirationDate"].ToString() == string.Empty)
                    {
                        dtExpirationDate = null;
                    }
                    else
                    {
                        dtExpirationDate = (Nullable<DateTime>)Convert.ToDateTime(dtLED.Rows[0]["UserExpirationDate"]);
                    }

                    strPassword = Convert.ToString(dtLED.Rows[0]["Password"]);
                    //** Commnet Account lock
                    //[CR-18] Vulnaribility Failure Attempt Start
                    if (dtLED.Rows[0]["Attempt"] == null || dtLED.Rows[0]["Attempt"].ToString() == string.Empty)
                    {
                        NoofAttempt = 0;
                    }
                    else
                    {
                        NoofAttempt = Convert.ToInt32(dtLED.Rows[0]["Attempt"]);
                    }

                    if (NoofAttempt >= 5)
                    {
                        lgLogin.FailureText = "Accountlocked";
                        return;
                    }
                    //[CR-18] Vulnaribility Failure Attempt End
                    //else 
                    //***********************************************************************

                    //*********************** 45 days *************************************************
                    //if ((dtCalculatedDate != null) && (dtCalculatedDate < (Nullable<DateTime>)DateTime.Now))
                    //{
                    //    e.Authenticated = false;
                    //}
                    //else
                    //*****************************************************************************

                    if ((dtExpirationDate != null) && (dtExpirationDate < (Nullable<DateTime>)DateTime.Now))
                    {
                        e.Authenticated = false;
                    }
                    else
                    {
                        strEnteredPassword = SHAEncription(lgLogin.Password);

                        if (strEnteredPassword == strPassword)
                        {
                            e.Authenticated = true;
                            //objEMBAL.InsertLastLoginLogOut(lgLogin.UserName, "Login");
                            objEMBAL.InsertLastLoginLogOut(lgLogin.UserName, DateTime.Now, DateTime.Now);
                            Session["UserJustLoginDate"] = DateTime.Now;
                            objEMBAL.DeleteLoginAttemptDetails(lgLogin.UserName);
                        }

                        /* For AD authentication start */
                        else
                        {
                            string strAdPassword = lgLogin.Password;
                            bool bUserCheck = false;
                            //bUserCheck = Authenticate(strUserName, strAdPassword);
                            bUserCheck = API_Bridge.AuthenticateInActiveDirectory(strUserName, strAdPassword);
                            if (bUserCheck) //Commented for deactivating AD
                            // if (true)  //Added for deactivating AD
                            {
                                e.Authenticated = true;
                                objEMBAL.InsertLastLoginLogOut(lgLogin.UserName, DateTime.Now, DateTime.Now);
                                Session["UserJustLoginDate"] = DateTime.Now;
                                objEMBAL.DeleteLoginAttemptDetails(lgLogin.UserName);
                                return;
                            }
                        }
                        /* For AD Authentication end */
                    }
                }
                else
                {
                    //[CR-05]  New User Login Start
                    DataSet dsEmployeeExist = new DataSet();

                    dsEmployeeExist = GetEmployeeExist(strUserName);

                    if (dsEmployeeExist.Tables[0].Rows.Count > 0)
                    {
                        string strAdPassword = lgLogin.Password;
                        bool bUserCheck = false;
                        //bUserCheck = Authenticate(strUserName, strAdPassword);
                        bUserCheck = API_Bridge.AuthenticateInActiveDirectory(strUserName, strAdPassword);
                        if (bUserCheck)
                        {
                            InsertNewUserDetails(strUserName, SHAEncription(lgLogin.Password));
                            e.Authenticated = true;
                        }
                        else
                        {
                            e.Authenticated = false;
                        }
                    }
                    else
                    {
                        e.Authenticated = false;
                    }

                    //e.Authenticated = false;

                    //[CR-05]  New User Login End

                }
            }
        }
        catch (Exception ex)
        {

            lgLogin.FailureText = ex.ToString();
        }
    }

    #endregion

    #region Verify Encryption

    public string CallDeskEncrypt(String strValue)
    {
        string EDPassword = "TDES2006YWR";
        string Original = null;
        string Password = null;
        string Retencrypt = null;
        byte[] PwdHash = null;
        byte[] Buff = null;

        TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider HashMD5 = new MD5CryptoServiceProvider();
        Original = strValue;
        Password = EDPassword;
        PwdHash = HashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Password));
        HashMD5 = null;
        des.Key = PwdHash;

        des.Mode = CipherMode.ECB;
        Buff = ASCIIEncoding.ASCII.GetBytes(Original);
        Retencrypt = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(Buff, 0, Buff.Length));
        Session["EncPwd"] = Retencrypt;
        return Retencrypt;
    }

    #endregion

    #region Login Error Code

    protected void lgLogin_LoginError(object sender, EventArgs e)
    {
        MembershipUser userInfo = Membership.GetUser(lgLogin.UserName);
        EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
        DataTable dtLED = new DataTable();
        if (userInfo == null)
        {
            lgLogin.FailureText = "Cannot Login Due to Invalid Login Details";
        }
        else
        {
            //int NoofAttempt = 0;
            // Nullable<DateTime> dtCalculatedDate;
            Nullable<DateTime> dtExpirationDate;
            dtLED = objEMBAL.GetLoggedEmployeeDetailsForASLC(lgLogin.UserName, DateTime.Now.Date);
            if (dtLED.Rows.Count == 0)
            {
                lgLogin.FailureText = "User is not available.";
                return;
            }

            //********************************* 45 days ******************************
            //double noOfDaysForInactiveUser = Convert.ToDouble(CommonUtility.GetValueByKey("DefaultDayForInactiveUser"));
            //if (dtLED.Rows[0]["UserLastLoginDateTime"] == null || dtLED.Rows[0]["UserLastLoginDateTime"].ToString() == string.Empty)
            //{
            //    dtCalculatedDate = null;
            //}
            //else
            //{
            //    DateTime userLastLogoutDate = Convert.ToDateTime(dtLED.Rows[0]["UserLastLoginDateTime"]);
            //    dtCalculatedDate = (Nullable<DateTime>)userLastLogoutDate.AddDays(noOfDaysForInactiveUser);
            //}
            //***************************************************************************

            if (dtLED.Rows[0]["UserExpirationDate"] == null || dtLED.Rows[0]["UserExpirationDate"].ToString() == string.Empty)
            {
                dtExpirationDate = null;
            }
            else
            {
                dtExpirationDate = (Nullable<DateTime>)Convert.ToDateTime(dtLED.Rows[0]["UserExpirationDate"]);
            }

            //[CR-18] Vulnaribility Failure Attempt Start
            if (lgLogin.FailureText == "Accountlocked")
            {
                lgLogin.FailureText = "Account locked due to 5 failure login attempts. Please contact administrator.";
                return;
            }
            //[CR-18] Vulnaribility Failure Attempt End

            //******************** Lock Account ****************
            //if (dtLED.Rows.Count > 0)
            //{
            //    if (dtLED.Rows[0]["Attempt"] == null || dtLED.Rows[0]["Attempt"].ToString() == string.Empty)
            //    {
            //        NoofAttempt = 0;
            //    }
            //    else
            //    {
            //        NoofAttempt = Convert.ToInt32(dtLED.Rows[0]["Attempt"]);
            //    }
            //}
            //if (NoofAttempt >= 3)
            //{
            //    //lgLogin.FailureText = "Your account has been locked due to three incorrect login attempts.";
            //    lgLogin.FailureText = "<nobr>User locked due to worng Attempt please contact <br>Rgicl.Applnsupport@Relianceada.com / contact no - 022-30380406</nobr>";
            //}
            //else 
            //*************************************************************

            //************************ 45 day logic********************************************
            //if ((dtCalculatedDate != null) && (dtCalculatedDate < (Nullable<DateTime>)DateTime.Now))
            //{               
            //     lgLogin.FailureText = "<nobr>User locked due to inoperable from last " + noOfDaysForInactiveUser + " days, please contact <br>Rgicl.Applnsupport@Relianceada.com / contact no - 022-30380406<nobr>";
            //} 
            //else 
            //*************************************************************************

            if ((dtExpirationDate != null) && (dtExpirationDate < (Nullable<DateTime>)DateTime.Now))
            {
                lgLogin.FailureText = "<nobr>User has been Expired  on " + dtExpirationDate.ToString() + ", please contact <br>Rgicl.Applnsupport@Relianceada.com / contact no - 022-30380406<nobr>";
            }
            else
            {
                objEMBAL.AddFaultLoginAttemptDetails(lgLogin.UserName, DateTime.Now);
                lgLogin.FailureText = "Your login attempt was not successful. Please try again.";
            }

        }
    }

    #endregion
    protected void LoginButton_Click(object sender, EventArgs e)
    {

    }

    #region Disable the Buttons for Double Click

    public void DisableButtons()
    {
        try
        {
            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
            sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ");
            sbValid.Append("if (Page_ClientValidate() == false) { return false; }} ");
            sbValid.Append("document.getElementById('" + btnOk.ClientID.ToString().Replace('$', '_') + "').disabled = true;");
            sbValid.Append("document.getElementById('" + btnCancel.ClientID.ToString().Replace('$', '_') + "').disabled = true;");
            sbValid.Append(this.Page.GetPostBackEventReference(this.btnOk));
            sbValid.Append(";");
            this.btnOk.Attributes.Add("onclick", sbValid.ToString());
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region SHA256 Encryption

    public string SHAEncription(string strval)
    {
        System.Security.Cryptography.SHA256 sha256 = new System.Security.Cryptography.SHA256Managed();
        byte[] sha256Bytes = System.Text.Encoding.Default.GetBytes(strval);
        byte[] cryString = sha256.ComputeHash(sha256Bytes);
        string sha256Str = string.Empty;
        for (int i = 0; i < cryString.Length; i++)
        {
            sha256Str += cryString[i].ToString("X");
        }
        return sha256Str;
    }

    #endregion

    #region lnkSignUp Click
    protected void lnkSignUp_Click(object sender, EventArgs e)
    {
        StringBuilder stPopupScript = new StringBuilder();
        stPopupScript.Append("<script language='javascript'>");
        stPopupScript.Append("var w = window.open('SignUp/CreateIMDPOSUser.aspx?','PopUpWindowName','width=700,left=150,top=100,height=600,titlebar=no, menubar=no, resizable=yes, scrollbars = yes');");//opens the pop up
        stPopupScript.Append("w.focus()");
        stPopupScript.Append("</script>");
        Page.RegisterClientScriptBlock("PopUpwindowOpen", stPopupScript.ToString());
    }
    #endregion

    #region GetWindosUser

    public string GetWindosUser()
    {
        string strUserName = WindowsIdentity.GetCurrent().Name;
        if (strUserName.Contains(@"\"))
        {
            strUserName = strUserName.Substring(strUserName.LastIndexOf(@"\") + 1);
        }
        return strUserName;

    }
    #endregion

    #region Manual DownLoad Click
    protected void lnkManualDownLoad_Click(object sender, EventArgs e)
    {

        try
        {
            string strFile = "IMDUser_manual.doc";
            DownloadFile(strFile);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    #endregion

    #region Download File Code

    public void DownloadFile(string strFileName)
    {
        try
        {
            FileInfo file;
            //string filename = Server.MapPath("..\\ManualFiles\\") + strFileName;
            string filename = Server.MapPath("ManualFiles\\") + strFileName;
            file = new FileInfo(filename);
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(HttpUtility.HtmlEncode(filename));
            //Response.WriteFile(filename);
            Response.End();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region decryption Logic
    public string Decrypt(string cipherText)
    {

        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Rijndael encryptor = Rijndael.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
    #endregion

    public void WriteToEvent(string e, string action, string strPagedata)
    {
        string strPath = @"D:\Logs";
        StringBuilder sb = new StringBuilder();
        if (!Directory.Exists(strPath))
        {
            Directory.CreateDirectory(strPath);
        }
        FileStream Fs = new FileStream(strPath + @"\" + "GoalSet.txt", FileMode.Append);
        FileInfo file = new FileInfo(strPath + @"\" + "GoalSet.txt");
        BinaryWriter BWriter = new BinaryWriter(Fs, Encoding.GetEncoding("UTF-8"));
        sb.AppendLine();
        sb.AppendLine(">>");
        sb.Append(" Error Occurred:--");
        sb.Append(DateTime.Now.ToString());
        sb.AppendLine(">>");
        sb.Append(action);
        sb.AppendLine("::");
        sb.Append(e);
        sb.AppendLine("::");
        sb.Append(strPagedata);
        sb.AppendFormat("\\");
        BWriter.Write(sb.ToString());
        Fs.Close();
    }

    #region AD Authentication
    public bool Authenticate(string UserID, string Password)
    {
        try
        {
            System.DirectoryServices.DirectoryEntry deADS = new System.DirectoryServices.DirectoryEntry("LDAP://" + ConfigurationManager.AppSettings["ADSDomain"].ToString(), UserID, Password);
            DirectorySearcher dsADS = new DirectorySearcher(deADS);
            SearchResult searchResult;
            searchResult = dsADS.FindOne();
            return true;
        }
        catch (Exception Ex)
        {
            string str;
            str = Ex.ToString();
            return false;
        }
    }
    #endregion

    //[CR-05] New User Login Start
    public void InsertNewUserDetails(string strEmployeeCode, string strPassword)
    {
        DataSet dtUserDetail = new DataSet();
        try
        {
            SqlParameter[] Params = 
			{   
				new SqlParameter("@Employee_code", strEmployeeCode),	
                new SqlParameter("@Password", strPassword)
			};
            dtUserDetail = DataUtils.ExecuteDataset("usp_CreateUser_firstLogin", Params);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    public DataSet GetEmployeeExist(string strUserId)
    {
        DataSet dsEmployee = new DataSet();

        try
        {
            SqlParameter[] Params = 
			{   
				new SqlParameter("@UserId",strUserId),
            };

            dsEmployee = DataUtils.ExecuteDataset("usp_CheckEmployeeExist", Params);

        }
        catch (Exception EX)
        {
        }

        return dsEmployee;
    }

    //[CR-05] New User Login End

    //[CR-13] Calldesk Msg display --start
    #region Get ApplicationType By Branch
    public DataSet GetCallDeskMessage(params object[] param)
    {
        DataSet oDSDisplayMsg = new DataSet();
        try
        {
            oDSDisplayMsg = DataUtils.ExecuteDataset("usp_GetCalldeskMessage", param);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

        return oDSDisplayMsg;
    }
    #endregion
    //[CR-13] Calldesk Msg display --End

    public static string Encrypt12(string textValue, string encryptKey = null)
    {
        if (string.IsNullOrEmpty(encryptKey))
        {
            encryptKey = ConfigurationManager.AppSettings["EncryptDecryptKey"];
        }
        if (textValue != null)
        {

            string passPhrase = "Pas5pr@se";
            int passwordIterations = 2;
            string hashAlgorithm = "SHA1";
            string initVector = "@1B2c3D4e5F6g7H8";
            int keySize = 128;

            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(encryptKey);

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(textValue);

            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            passPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations);

            byte[] keyBytes = password.GetBytes(keySize / 8);


            RijndaelManaged symmetricKey = new RijndaelManaged();



            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                             keyBytes,
                                                             initVectorBytes);


            MemoryStream memoryStream = new MemoryStream();


            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         encryptor,
                                                         CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);


            cryptoStream.FlushFinalBlock();


            byte[] cipherTextBytes = memoryStream.ToArray();


            memoryStream.Close();
            cryptoStream.Close();


            string cipherText = Convert.ToBase64String(cipherTextBytes);


            return cipherText;
        } return textValue;
    }

    public static string Decrypt12(string encodedText, string decryptKey = null)
    {

        if (string.IsNullOrEmpty(decryptKey))
        {
            decryptKey = ConfigurationManager.AppSettings["EncryptDecryptKey"];
        }
        if (encodedText != null)
        {
            string passPhrase = "Pas5pr@se";
            int passwordIterations = 2;
            string hashAlgorithm = "SHA1";
            string initVector = "@1B2c3D4e5F6g7H8";
            int keySize = 128;

            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(decryptKey);


            string stringToDecrypt = encodedText.Replace(" ", "+");
            byte[] cipherTextBytes = Convert.FromBase64String(stringToDecrypt);

            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            passPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations);

            byte[] keyBytes = password.GetBytes(keySize / 8);


            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                             keyBytes,
                                                             initVectorBytes);


            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);


            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                          decryptor,
                                                          CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];


            int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                       0,
                                                       plainTextBytes.Length);


            memoryStream.Close();
            cryptoStream.Close();

            string plainText = Encoding.UTF8.GetString(plainTextBytes,
                                                       0,
                                                       decryptedByteCount);



            return plainText;
        } return decryptKey;
    }

    private bool CaptcahVerify()
    {
        //TextBox txtimgcode = (TextBox)lgLogin.FindControl("txtimgcode");
        //string textimage = txtimgcode.Text.ToString();
        //string strcap = this.Session["CaptchaImageText"].ToString();
        //if (txtimgcode.Text == this.Session["CaptchaImageText"].ToString())
        //{
        //    return true;
        //}
        //else
        //{
        //    GenerateImage();
        //    txtimgcode.Text = "";
        //    lgLogin.FailureText = "";
        //    return false;
        //}
        return true; // for local
    }

    protected void ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        TextBox txtimgcode = (TextBox)lgLogin.FindControl("txtimgcode");
        txtimgcode.Text = "";
        lgLogin.FailureText = "";
        GenerateImage();
    }

    private void GenerateImage()
    {
        // Captcha image code
        //// Create a random code and store it in the Session object.
        this.Session["CaptchaImageText"] = "";
        this.Session["CaptchaImageText"] = GenerateRandomCode();
        // Create a CAPTCHA image using the text stored in the Session object.
        RandomImage ci = new RandomImage(this.Session
            ["CaptchaImageText"].ToString(), 150, 40);

        byte[] data = default(byte[]);
        using (System.IO.MemoryStream sampleStream = new System.IO.MemoryStream())
        {
            //save to stream.
            ci.Image.Save(sampleStream, System.Drawing.Imaging.ImageFormat.Bmp);
            //the byte array
            data = sampleStream.ToArray();
        }
        ci.Dispose();
        string PROFILE_PIC = Convert.ToBase64String(data);
        Image Image1 = (Image)lgLogin.FindControl("Image1");
        Image1.ImageUrl = String.Format("data:image/jpg;base64,{0}", PROFILE_PIC);
        lgLogin.FailureText = "";
        // Captcha image code end
    }

    private string GenerateRandomCode()
    {
        Random r = new Random();
        string s = "";

        for (int j = 0; j <= 4; j++)
        {
            int ch;
            int i = r.Next(3);
            switch (i)
            {
                case 1:
                    ch = r.Next(0, 9);
                    s = s + ch.ToString();
                    break;
                case 2:
                    ch = r.Next(65, 90);
                    s = s + Convert.ToChar(ch).ToString();
                    break;
                case 3:
                    ch = r.Next(97, 122);
                    s = s + Convert.ToChar(ch).ToString();
                    break;
                default:
                    ch = r.Next(97, 122);
                    s = s + Convert.ToChar(ch).ToString();
                    break;
            }
            r.NextDouble();
        }
        return s;
    }
}


