using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CallDeskBAL;
using CallDeskBO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

public partial class User_ChangePassword : System.Web.UI.Page
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        string strUserName = Membership.GetUser().UserName;
        txtUserName.Text = strUserName;
    }
    #endregion

    #region Save Button Event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            UserRoleBO objBO = new UserRoleBO();
            UserBAL objBAL = new UserBAL();
            string strOldParrword, strNewPassword, strUserName;

            strUserName = Membership.GetUser().UserName;

            //strOldParrword = CallDeskEncrypt(txtOldPassword.Text);
            //strNewPassword = CallDeskEncrypt(txtConfirmPassword.Text);

            // [CR-18] Password Vulnaribility Start
            if (!String.IsNullOrEmpty(Convert.ToString(txtConfirmPassword.Text)))
            {

                string expression = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";

                Match match = Regex.Match(txtConfirmPassword.Text, expression, RegexOptions.IgnoreCase);
                if (match.Success)
                {

                }
                else
                {
                    lblMessage.Text = "Password should be Minimum 8 characters at least 1 Alphabet, 1 Number and 1 Special Character";
                    return;
                }

            }
            // [CR-18] Password Vulnaribility End


            strOldParrword = SHAEncription(txtOldPassword.Text);
            strNewPassword = SHAEncription(txtConfirmPassword.Text);
            

            if (objBAL.CheckExistingPassword(strUserName, strOldParrword, strNewPassword) == "0")
            {
                lblMessage.Text = "Old Password enterd is not correct.";
            }
            else
            {
                lblMessage.Text = "Password changed successfully, please logout and loging again using new password.";
                // int intReturnVal = objBAL.ChangePassword(strOldParrword, strNewPassword);
            }


        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }
    #endregion

    #region Cancel Button Event
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Default.aspx");
    }
    #endregion

    #region Method to Encrypt Password

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

    #region Check All Validations
    public bool CheckValidations(string strNewPassword, string userName)
    {
        if (strNewPassword.Length < 8)
        {
            lblMessage.Text = "Password should be minimum length of 8 characters.";
            return false;
        }
        //if (IsValidAlphaNumeric(strNewPassword) != true)
        //{
        //    lblMessage.Text = "Password should be alphanumeric.";
        //    return false;
        //}
        if (CheckPasswordForUserName(strNewPassword, userName) != true)
        {
            lblMessage.Text = "Password should not be same as username or a part of username.";
            return false;
        }
        return true;
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

    #region Check for Letter

    public bool CheckLetter(string inputStr)
    {
        bool isLetter = false;
        for (int j = 0; j < inputStr.Length; j++)
        {
            if (char.IsLetter(inputStr[j]))
            {
                isLetter = true;
                break;
            }

        }
        return isLetter;
    }

    #endregion

    #region Check for Number

    public bool CheckNumber(string inputStr)
    {
        bool isNumber = false;
        for (int k = 0; k < inputStr.Length; k++)
        {
            if (char.IsNumber(inputStr[k]))
            {
                isNumber = true;
                break;
            }
        }
        return isNumber;
    }

    #endregion
}
