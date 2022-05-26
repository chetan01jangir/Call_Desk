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
using System.Web.Mail;
using SMSSendService;
using System.Globalization;
//using IMDPortalUserValidate;
using POSUserValidate;
using System.Data.SqlClient;

public partial class CreateIMDPOSUser : System.Web.UI.Page
{
    #region Objects
    UserRoleBAL objBAL = new UserRoleBAL();
    EmployeeManagerBAL objEMBAL = new EmployeeManagerBAL();
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {          
            //divAgentCreate.Visible = false;
            //FillRegions();
            divEditProfile.Visible = false;
            divEditUserFields.Visible = false;           
        }
        lblMessage.Text = string.Empty;
        lblEditProfile.Text = string.Empty;
        lblCreateUserMSG.Text = string.Empty;
        ///DisablePageCaching();
    }
    #endregion

    #region btnSave Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try       
        {                     
            string sUserName= txtUserName.Text.Trim();
            string strReturnValue = objBAL.CheckExistingMember(sUserName);           
            if (strReturnValue == "1")
            {
                lblCreateUserMSG.Text = "Username " + sUserName + " already exists!";
            }
            else 
            {
                string strBranchCode, strEmail, strPassword, strUserName, strRole, strCreatedBy,
                strAgentName, strDesignation, strChannel, strMobileNo, strApplicationType, strAgentCode,
                strQuestion, strAnswer, strGender, SMCode, SMBranchCode, SMBranchName, SMRegionName, SMZoneName,SMName;
 
                DateTime dtExpiryDate=new DateTime();
                DateTime dtBirthDate = new DateTime();
              
                if (CheckValidations(txtPassword.Text.Trim(), txtUserName.Text.Trim()) == true)
                {
                    dtExpiryDate = DateTime.Now.Date;
                    dtBirthDate = Convert.ToDateTime(GetDateMMDDYYYY(txtBirthDate.Text.ToString().Trim()));

                    strBranchCode = "A000";
                    strEmail = txtEmail.Text.ToString().Trim();
                    strPassword = SHAEncription(txtConfirmPwd.Text.ToString().Trim());
                    strUserName = txtUserName.Text.ToString().Trim();
                    strRole = "Agent";
                    strCreatedBy = "rgiadmin";
                    strAgentName = txtAgentName.Text.ToString().Trim();
                    strDesignation = "AGENT IMDsPOS";
                    strChannel = "Channel IMDsPOS";
                    strMobileNo = txtMobileNo.Text.Trim();
                   // strRegionID = ddlRegion.SelectedValue.ToString();                    
                    strApplicationType = ddlApplicationType.SelectedValue.ToString();
                    strAgentCode = strUserName;
                    strQuestion = txtQuestion.Text.Trim();
                    strAnswer = txtAnswer.Text.Trim();
                    //strApplicationType = ddlApplicationType.SelectedValue.ToString();
                    strGender = ddlGender.SelectedValue.ToString();

                    SMName = txtSMName.Text.Trim();
                    SMCode = txtSMCode.Text.Trim();

                    SMBranchCode = string.Empty;
                    SMBranchName = string.Empty;
                    SMRegionName = string.Empty;
                    SMZoneName = string.Empty;
                   

                    //if (ddlApplicationType.SelectedValue == "IMD-Portal")
                    //{
                    //     UserDetails oIMDUser = new UserDetails();
                    //     DataTable ODTUser = oIMDUser.GetUserDetailsCallDesk(txtUserName.Text.ToString());
                    //     if (ODTUser != null)
                    //     {
                    //         if (ODTUser.Rows.Count > 0)
                    //         {
                    //              if (!string.IsNullOrEmpty(ODTUser.Rows[0]["SM_Code"].ToString()))
                    //             {
                    //                 SMCode = ODTUser.Rows[0]["SM_Code"].ToString();
                    //             }
                    //             else
                    //             {
                    //                 SMCode = "";
                    //             }
                  
                    //             if (!string.IsNullOrEmpty(ODTUser.Rows[0]["Rel_Equavalent"].ToString()))
                    //             {
                    //                 SMBranchCode = ODTUser.Rows[0]["Rel_Equavalent"].ToString();
                    //             }
                    //             else
                    //             {
                    //                 SMBranchCode = "";
                    //             }
                    //             if (!string.IsNullOrEmpty(ODTUser.Rows[0]["SM_Branch"].ToString()))
                    //             {
                    //                 SMBranchName = ODTUser.Rows[0]["SM_Branch"].ToString();
                    //             }
                    //             else
                    //             {
                    //                 SMBranchName = "";
                    //             }

                    //             if (!string.IsNullOrEmpty(ODTUser.Rows[0]["SM_Region"].ToString()))
                    //             {
                    //                 SMRegionName = ODTUser.Rows[0]["SM_Region"].ToString();
                    //             }
                    //             else
                    //             {
                    //                 SMRegionName = "";
                    //             }

                    //             if (!string.IsNullOrEmpty(ODTUser.Rows[0]["SM_Zone"].ToString()))
                    //             {
                    //                 SMZoneName = ODTUser.Rows[0]["SM_Zone"].ToString();
                    //             }
                    //             else
                    //             {
                    //                 SMZoneName = "";
                    //             }
                    //         }
                    //     }

                    //}
                    //else 
                        
                    if (ddlApplicationType.SelectedValue == "POS")
                    {
                        UserDetailService oPOSUser = new UserDetailService();

                        DataSet ODsPOS = oPOSUser.SMInfo(txtUserName.Text.Trim());
                        if (ODsPOS != null)
                        {
                            if (ODsPOS.Tables.Count > 0)
                            {

                                DataTable ODTPOS = (DataTable)ODsPOS.Tables[0];
                                if (ODTPOS != null)
                                {
                                    if (ODTPOS.Rows.Count > 0)
                                    {
                                        //if (!string.IsNullOrEmpty(ODTPOS.Rows[0]["SMCode"].ToString()))
                                        //{
                                        //    SMCode = ODTPOS.Rows[0]["SMCode"].ToString();
                                        //}
                                        //else
                                        //{
                                        //    SMCode = "";
                                        //}
                                        if (!string.IsNullOrEmpty(ODTPOS.Rows[0]["SMBranchCode"].ToString()))
                                        {
                                            SMBranchCode = ODTPOS.Rows[0]["SMBranchCode"].ToString();
                                        }
                                        else
                                        {
                                            SMBranchCode = "";
                                        }
                                        if (!string.IsNullOrEmpty(ODTPOS.Rows[0]["SMBranchName"].ToString()))
                                        {
                                            SMBranchName = ODTPOS.Rows[0]["SMBranchName"].ToString();
                                        }
                                        else
                                        {
                                            SMBranchName = "";
                                        }
                                        if (!string.IsNullOrEmpty(ODTPOS.Rows[0]["RegionName"].ToString()))
                                        {
                                            SMRegionName = ODTPOS.Rows[0]["RegionName"].ToString();
                                        }
                                        else
                                        {
                                            SMRegionName = "";
                                        }
                                        if (!string.IsNullOrEmpty(ODTPOS.Rows[0]["ZoneName"].ToString()))
                                        {
                                            SMZoneName = ODTPOS.Rows[0]["ZoneName"].ToString();
                                        }
                                        else
                                        {
                                            SMZoneName = "";
                                        }


                                    }

                                }
                            }
                        }

                    }

                    CreateAgentUser(strUserName, strPassword, strEmail, strRole, strBranchCode, strCreatedBy,
                    strAgentName, strDesignation, strChannel, dtExpiryDate, strQuestion, strAnswer, strMobileNo,
                    dtBirthDate, strApplicationType, strAgentCode, strGender, SMCode, SMBranchCode, SMBranchName, SMRegionName, SMZoneName, SMName);                    
                    
                    divAgentCreate.Visible = false;
                    lblMsgDiv.Text = "Congratulation! User ID " + strUserName + " created successfully. <br>Password is sent on Register Mobile no –" + strMobileNo + " and mail id  - " + strEmail;
                    divMessage.Visible = true;


                    string strSMSMsg = "";
                    string strMailMsg = "";    
                    string strPasswordSend = txtConfirmPwd.Text.ToString().Trim();
                    string strEmailSubject = "Registration in Reliance CallDesk System.";


                    strSMSMsg = GetSMStextRegistration(strUserName,strPasswordSend );
                    strMailMsg = GetEmailtextRegistration(strAgentName, strUserName, strPasswordSend, strEmail, strApplicationType, SMName, SMCode);

                    SendEmailToUser(strEmail, strEmailSubject, strMailMsg);
                    SendSMSToUser(strMobileNo, strSMSMsg);
                }
            }

        }
        catch (Exception ex)
        {
            lblCreateUserMSG.Text = ex.Message;
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

    #region Check Validations
    public bool CheckValidations(string strNewPassword, string userName)
    {
        string strAgentCodeUser = string.Empty;
        if (strNewPassword.Length < 8)
        {
            lblCreateUserMSG.Text = "Password should be minimum length of 8 characters.";
            return false;
        }     
        if (CheckPasswordForUserName(strNewPassword, userName) != true)
        {
            lblCreateUserMSG.Text = "Password should not be same as username or a part of username.";
            return false;
        }

        //if (ddlApplicationType.SelectedValue == "IMD-Portal")
        //{
        //    UserDetails oIMDUser = new UserDetails();
        //    DataTable ODTUser = oIMDUser.GetUserDetailsCallDesk(txtUserName.Text.ToString());
        //    //DataTable ODTUser = GetIMDPortalAgentDetails(txtUserName.Text.ToString().Trim());
        //    if (ODTUser != null)
        //    {
        //        if (ODTUser.Rows.Count > 0)
        //        {
        //            if (!string.IsNullOrEmpty(ODTUser.Rows[0]["Agent_Code"].ToString()))
        //            {
        //                strAgentCodeUser = ODTUser.Rows[0]["Agent_Code"].ToString().Trim().ToLower();
        //            }
        //        }
        //    }
        //    if (strAgentCodeUser != userName.Trim().ToLower())
        //    {
        //        lblCreateUserMSG.Text = "User ID is not exists in IMD-Portal System.";
        //        return false;

        //    }
        //}
        //else
        if (ddlApplicationType.SelectedValue == "POS")
        {

            UserDetailService oPOSUser = new UserDetailService();

            DataSet ODsPOS = oPOSUser.SMInfo(txtUserName.Text.Trim());
            if (ODsPOS != null)
            {
                if (ODsPOS.Tables.Count > 0)
                {
                    DataTable ODTPOS = (DataTable)ODsPOS.Tables[0];
                    if (ODTPOS != null)
                    {
                        if (ODTPOS.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(ODTPOS.Rows[0]["UserID"].ToString()))
                            {
                                strAgentCodeUser = ODTPOS.Rows[0]["UserID"].ToString().Trim().ToLower();
                            }
                        }
                    }
                }
            }
            if (strAgentCodeUser != userName.Trim().ToLower())
            {
                lblCreateUserMSG.Text = "User ID is not exists in POS System.";
                return false;
            }
           
        }        
        
     
        DateTime odtOut = new DateTime();
        try
        {
            odtOut = Convert.ToDateTime(GetDateMMDDYYYY(txtBirthDate.Text.ToString()));
            if (odtOut.AddDays(0).Date >= DateTime.Now.Date)
            {
                lblCreateUserMSG.Text = "Birth date should not be today date or future date.";
                return false;
            }
            else
            {
                return true;
            }
          
        }
        catch
        {
            lblCreateUserMSG.Text = "Birth date is not valid.";
            return false;
        }
       

    }
    #endregion

    #region Check Password for UserName
    public bool CheckPasswordForUserName(string inputStr, string checkUserName)
    {
        if (inputStr.Contains(checkUserName) == true || inputStr.Contains(checkUserName.ToLower()) == true)
        {
            return false;
        }
        return true;
    }
    #endregion

    #region Create Agent User
    public int CreateAgentUser(params object[] param)
    {
        int intReturnValue = 0;
        try
        {
            intReturnValue = DataUtils.ExecuteNonQuery("usp_AgentCreateUser", param);
           
        }
        catch (Exception ex)
        {
            lblCreateUserMSG.Text = ex.Message;
        }
        return intReturnValue;
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
        catch(Exception ex)
        {
            lblCreateUserMSG.Text = ex.Message;
        }
        return oDTRegions;
    }
    #endregion

    #region Link Create Agent Click
    protected void lnkCreateAgent_Click(object sender, EventArgs e)
    {
        divAgentCreate.Visible = true;
        divEditProfile.Visible = false;
        divEditUserFields.Visible = false;
        divMessage.Visible = false;
        ClearAgentCreation();
    }
    #endregion

    #region Link Edit Profile Click
    protected void lnkEditProfile_Click(object sender, EventArgs e)
    {
        divAgentCreate.Visible = false;
        divEditUserFields.Visible = false;
        divEditProfile.Visible = true;
        divUserValidate.Visible = true;
        txtEditUserName.Enabled = true;
        btnValidate.Enabled = true;
        divEditProfileFields.Visible = false;
        //trEditSecurityQues.Visible = false;
        //trEditSecurityAnswer.Visible = false;
        //trEditPassword.Visible = true;       
        //chkEditSecurityQuestion.Checked = false;
        //chkEditPassword.Checked = true;       
        //lblEditSecurityQues.Text = string.Empty;
        divMessage.Visible = false;
        ClearEditProfileFields();        
        
    }
    #endregion

    #region Button Edit Profile
    protected void btnEditProfile_Click(object sender, EventArgs e)
    {
        if (txtEditUserName.Text.Trim() == string.Empty)
        {
            divEditUserFields.Visible = false;
            lblEditProfile.Text = "Please enter UserName";
        }
        else
        {
            string strUserName = txtEditUserName.Text.ToString().Trim();
            string strReturnValue = objBAL.CheckExistingMember(strUserName);
            if (strReturnValue != "1")
            {
                lblEditProfile.Text = "User Name " + '"' + strUserName + '"' + " does not exists in the system. Please enter correct User Name";
            }
            else
            {

                DataTable oDTEmp = new DataTable();
                oDTEmp = objEMBAL.GetLoggedEmployeeDetailsForASLC(strUserName, DateTime.Now.Date);

                if (chkEditPassword.Checked == true)
                {
                    string strEnterPassword = SHAEncription(txtEditPassword.Text.Trim());  
                    string strActualPassward= Convert.ToString(oDTEmp.Rows[0]["Password"]);
                    if (strEnterPassword == strActualPassward)
                    {
                        divEditUserFields.Visible = true;
                        divEditProfileFields.Visible = false;
                        divUserValidate.Visible = false;
                        txtUserNameLock.Text = strUserName;
                        txtUserNameLock.Enabled = false;
                        txtEditEmail.Text = Convert.ToString(oDTEmp.Rows[0]["Email"]);
                        txtEditMobileNo.Text = Convert.ToString(oDTEmp.Rows[0]["MobileNo"]);
                        txtEditSMName.Text = Convert.ToString(oDTEmp.Rows[0]["SMName"]);
                        txtEditSMCode.Text = Convert.ToString(oDTEmp.Rows[0]["SMCode"]);
                    }
                    else
                    {
                        lblEditProfile.Text = "Password is incorrect";
                    }
                }
                if(chkEditSecurityQuestion.Checked==true)
                {
                    string strEnterAnswer = txtEditAnswer.Text.Trim();
                    string strActualAnswer = Convert.ToString(oDTEmp.Rows[0]["PasswordAnswer"]);
                    if (strEnterAnswer.ToLower().Trim() == strActualAnswer.ToLower().Trim())
                    {
                        divEditUserFields.Visible = true;
                        divEditProfileFields.Visible = false;
                        divUserValidate.Visible = false;
                        txtUserNameLock.Text = strUserName;
                        txtUserNameLock.Enabled = false;
                        txtEditEmail.Text = Convert.ToString(oDTEmp.Rows[0]["Email"]);
                        txtEditMobileNo.Text = Convert.ToString(oDTEmp.Rows[0]["MobileNo"]);
                        txtEditSMName.Text = Convert.ToString(oDTEmp.Rows[0]["SMName"]);
                        txtEditSMCode.Text = Convert.ToString(oDTEmp.Rows[0]["SMCode"]);
                    }
                    else
                    {
                        lblEditProfile.Text = "Answer is incorrect";
                    }
                }
            }
        }
    }
    #endregion

    #region chkEditSecurityQuestion CheckedChanged
    protected void chkEditSecurityQuestion_CheckedChanged(object sender, EventArgs e)
    {
        try
        {           
                trEditPassword.Visible = false;               
                trEditSecurityQues.Visible = true;
                trEditSecurityAnswer.Visible = true;
               
                string userName = txtEditUserName.Text.Trim();
                DataTable oDTEmpDetails = new DataTable();
                oDTEmpDetails = objEMBAL.GetLoggedEmployeeDetailsForASLC(userName, DateTime.Now.Date);
                lblEditSecurityQues.Text = Convert.ToString(oDTEmpDetails.Rows[0]["PasswordQuestion"]) + " ?";
        }
        catch (Exception ex)
        {
            lblEditProfile.Text = ex.Message;
        }
    }
    #endregion

    #region chkEditPassword CheckedChanged
    protected void chkEditPassword_CheckedChanged(object sender, EventArgs e)
    {
        try
       {
            trEditSecurityQues.Visible = false;
            trEditSecurityAnswer.Visible = false;
            trEditPassword.Visible = true;
            lblEditSecurityQues.Text = string.Empty; 
        }
        catch (Exception ex)
        {
            lblEditProfile.Text = ex.Message;
        }
    }
    #endregion

    #region Clear Edit Profile Fields
    public void ClearEditProfileFields()
    {
        txtEditUserName.Text = string.Empty;
        txtEditPassword.Text = string.Empty;
        txtEditAnswer.Text = string.Empty;
    }
    #endregion

    #region Clear AgentCreation Fields
    public void ClearAgentCreation()
    {
        txtUserName.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtConfirmPwd.Text = string.Empty;
        txtAgentName.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtBirthDate.Text = string.Empty;
        ddlApplicationType.SelectedIndex = 0;
        //ddlRegion.SelectedIndex = 0;
        ddlGender.SelectedIndex = 0;
       // txtAgentCode.Text = string.Empty;
        txtQuestion.Text = string.Empty;
        txtAnswer.Text = string.Empty;
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
            lblEditProfile.Text = ex.Message;
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
            UpdateUserProfile(uName, uEmail, uMobileNo,uSMName,uSMCode);
            divEditProfile.Visible = false;
            divEditUserFields.Visible = false;
            divMessage.Visible = true;

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

            if (uMobileNo != string.Empty)
            {
                strSMSText = GetSMStextUpdation(strEmpName);
                SendSMSToUser(uMobileNo, strSMSText);
            }


            lblMsgDiv.Text = " Profile Updated Successfully with Mobile no:" + uMobileNo + " and Email Id:" + uEmail;            

        }
        catch (Exception ex)
        {
            lblEditProfile.Text = ex.Message;
        }
    }
    #endregion

    #region Send Email To User
    public void SendEmailToUser(string strEmail, string strSubject,string strMailMsg)
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

    //#region Fill Regions
    //public void FillRegions()
    //{
    //    try
    //    {
    //        ddlRegion.Items.Clear();
    //        ddlRegion.DataSource = GetAgentRegions();
    //        ddlRegion.DataTextField = "RegionName";
    //        ddlRegion.DataValueField = "RegionID_PK";
    //        ddlRegion.DataBind();
    //        ddlRegion.Items.Insert(0, new ListItem("--Select Region--", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        lblCreateUserMSG.Text = ex.Message;
    //    }
    //}
    //#endregion

    #region Reset 
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAgentCreation();
    }
    #endregion

    #region GetDateMMDDYYYY
    public string GetDateMMDDYYYY(string strDate)
    {
        string[] splitDate = strDate.Split('/');
        return splitDate[1] + "/" + splitDate[0] + "/" + splitDate[2];
    }
    #endregion

    #region btnValidate Click
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (txtEditUserName.Text.Trim() == string.Empty)       
        {
          
            lblEditProfile.Text = "Please enter UserName";
            
        }
        else
        {
            string strUserName = txtEditUserName.Text.ToString().Trim();
            string strReturnValue = objBAL.CheckExistingMember(strUserName);
            if (strReturnValue != "1")
            {
                lblEditProfile.Text = "User Name " + '"' + strUserName + '"' + " does not exists in the system. Please enter correct User Name";
            }
            else
            {              
                divEditProfileFields.Visible = true;
                trEditSecurityQues.Visible = false;
                trEditSecurityAnswer.Visible = false;
                trEditPassword.Visible = true;
                chkEditSecurityQuestion.Checked = false;
                chkEditPassword.Checked = true;
                lblEditSecurityQues.Text = string.Empty;
                divMessage.Visible = false;
                txtEditUserName.Enabled = false;
                btnValidate.Enabled = false;
            }
        }
    }
    #endregion

    #region ddlApplicationType SelectedIndexChanged
    protected void ddlApplicationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strAgentCode = string.Empty;
        string strEmpName = string.Empty;
        string strSMCode = string.Empty;
        if (txtUserName.Text.Trim() == string.Empty)
        {
            lblCreateUserMSG.Text = "Please enter current User ID that exists in " + ddlApplicationType.SelectedValue;          
        }
        else
        {
            //if (ddlApplicationType.SelectedValue == "IMD-Portal")
            //{
            //   UserDetails oIMDUser = new UserDetails();
            //    DataTable ODTUser = oIMDUser.GetUserDetailsCallDesk(txtUserName.Text.ToString());
            //    //DataTable ODTUser=GetIMDPortalAgentDetails(txtUserName.Text.ToString().Trim());
            //    if (ODTUser != null)
            //    {
            //        if (ODTUser.Rows.Count > 0)
            //        {
            //            if (!string.IsNullOrEmpty(ODTUser.Rows[0]["Agent_Code"].ToString()))
            //            {
            //                strAgentCode = ODTUser.Rows[0]["Agent_Code"].ToString().Trim().ToLower();
            //                strSMCode = ODTUser.Rows[0]["SM_Code"].ToString().Trim().ToLower();
            //               if (strAgentCode != string.Empty)
            //              {
            //                  DataTable ODtUserPortalDet = new DataTable();
            //                  ODtUserPortalDet = GetUserDetailsByUserName(strSMCode);
            //                  if (ODtUserPortalDet != null)
            //                  {
            //                      if (ODtUserPortalDet.Rows.Count > 0)
            //                       {
            //                          strEmpName = Convert.ToString(ODtUserPortalDet.Rows[0]["EmployeeName"]);

            //                       }
            //                   }

            //              }

            //            }
            //        }
            //    }
            //}
            //else 
                
            if (ddlApplicationType.SelectedValue == "POS")
            {
                UserDetailService oPOSUser = new UserDetailService();
                DataSet ODsPOS = oPOSUser.SMInfo(txtUserName.Text.Trim());
                if (ODsPOS != null)
                {
                    if (ODsPOS.Tables.Count > 0)
                    {
                        DataTable ODTPOS = (DataTable)ODsPOS.Tables[0];
                        if (ODTPOS != null)
                        {
                            if (ODTPOS.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(ODTPOS.Rows[0]["UserID"].ToString()))
                                {
                                    strAgentCode = ODTPOS.Rows[0]["UserID"].ToString().Trim().ToLower();
                                    strSMCode = ODTPOS.Rows[0]["SMCode"].ToString().Trim().ToLower();

                                    if (strAgentCode != string.Empty)
                                    {
                                        DataTable ODtUserPOSDet = new DataTable();
                                        ODtUserPOSDet = GetUserDetailsByUserName(strSMCode);
                                        if (ODtUserPOSDet != null)
                                        {
                                            if (ODtUserPOSDet.Rows.Count > 0)
                                            {
                                                strEmpName = Convert.ToString(ODtUserPOSDet.Rows[0]["EmployeeName"]);

                                            }
                                        }

                                    }


                                }
                            }
                        }
                    }
                }
               
            } 
            if (ddlApplicationType.SelectedValue != "0")
            {
                if (strAgentCode != txtUserName.Text.ToString().Trim().ToLower())
                {
                    lblCreateUserMSG.Text = "User ID does not exists in " + ddlApplicationType.SelectedValue + "  System. Enter current Login ID that exists in " + ddlApplicationType.SelectedValue + ".";
                }
                else
                {
                    txtSMCode.Text = strSMCode;
                    txtSMName.Text = strEmpName;
                }
            }

        }   
    }
    #endregion
    
    #region GetSMStextRegistration
    public string GetSMStextRegistration(string strUserName,string strPwd)
    {
        string strSMS = string.Empty;
        strSMS = @"Congratulation! You have successfully registered on Reliance Call desk. Your login details are User Id <" + strUserName + "> & Password  <" + strPwd + "> Please login to http://calldesk.reliancegeneral.co.in ";
        return strSMS;
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


    #region GetEmailtextRegistration
    public string GetEmailtextRegistration(string empName, string strUserName, string strPwd,string strEmailID,string strApp,string strSMName,string strSMCode)
    {
        string strEmail = string.Empty;
        strEmail = @"Dear "+empName.ToUpper()+",<br><br><br>";
        strEmail = strEmail + "Thank you for providing us an opportunity and registering with Reliance Call desk Application to serve you better. Following are the registration details.<br><br>";
        strEmail =  strEmail + "<b>User Id</b>  : "+strUserName+"<br>";
        strEmail = strEmail + "<b>Password</b>: " + strPwd + "<br>";
        strEmail = strEmail + "<b>User Name</b>: " + empName + "<br>";
        strEmail = strEmail + "<b>User Email Id</b>: " + strEmailID + "<br>";
        strEmail = strEmail + "<b>Application</b>: " + strApp + "<br>";
        strEmail = strEmail + "<b>Sales Manager Name</b>: " +strSMName+"<br>";
        strEmail = strEmail + "<b>Sales Manager Code</b>: " + strSMCode + "<br><br><br>";
        strEmail = strEmail + "Click <a href='http://calldesk.reliancegeneral.co.in/'>here</a> on this link to login into the system <br><br><br><br><br>";            
        strEmail =  strEmail + "Thank you<br>";
        strEmail =  strEmail + "Reliance Application Support Team";    
        return strEmail;
    }
    #endregion


    #region GetEmailTextUpdateProfile
    public string GetEmailTextUpdateProfile(string empName, string strUserName, string strEmailID, string strMobileNo, string strSMName, string strSMCode)
    {
        string strEmail = string.Empty;
        strEmail = @"Dear " + empName.ToUpper() + ",<br><br><br>";
        strEmail = strEmail + "Your profile updated successfully. Following are the profile details.<br><br>";
        strEmail = strEmail + "<b>User Id</b>  : " + strUserName + "<br>";
        strEmail = strEmail + "<b>Agent Name</b>: " + empName + "<br>";
        strEmail = strEmail + "<b>User Email Id</b>: " + strEmailID + "<br>";
        strEmail = strEmail + "<b>User Mobile No</b>: " + strMobileNo + "<br>";
        strEmail = strEmail + "<b>Sales Manager Name</b>: " + strSMName + "<br>";
        strEmail = strEmail + "<b>Sales Manager Code</b>: " + strSMCode + "<br><br><br><br><br>";
        strEmail = strEmail + "Thank you<br>";
        strEmail = strEmail + "Reliance Application Support Team";
        return strEmail;
    }
    #endregion     

    #region GetIMDPortalAgentDetails
    public DataTable GetIMDPortalAgentDetails(string userName)
    {
        DataTable dtAgent = new DataTable();
        IDataReader reader = null;
        SqlCommand oSqlComm = new SqlCommand();
        SqlConnection oConn = new SqlConnection();
        string strConnStr = @"Data Source=10.65.15.82,7359;Initial Catalog=RGICLPORTAL_TND_ICM3;User ID=icm ;Password=icm";
        try
        {
            oConn.ConnectionString = strConnStr;
            oConn.Open();
            oSqlComm.Connection = oConn;
            oSqlComm.CommandType = CommandType.StoredProcedure;
            oSqlComm.CommandText = "usp_CheckForAgent";
            SqlParameter oPUserName = new SqlParameter("@UserID", userName);
            oSqlComm.Parameters.Add(oPUserName);
            reader = oSqlComm.ExecuteReader();
            dtAgent.Load(reader);
        }
        catch (Exception ex)
        {
            lblCreateUserMSG.Text = ex.Message;
        }
        finally
        {
            oConn.Close();
            oConn.Dispose();
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }           
        }
        return dtAgent;
    }
    #endregion

    #region Disable Page Caching
    public static void DisablePageCaching()
    {
        //Used for disabling page caching
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
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


}
 
 


