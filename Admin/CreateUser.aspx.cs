using System;
using System.Text;
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
using System.Security.Cryptography;

public partial class Admin_CreateUser : System.Web.UI.Page
{

    #region private Variables

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);        
        if (!IsPostBack)
        {
            try
            {
                GetRole();
                GetDesignation();
                GetChannel();                
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }

    #endregion

    #region BindZone

    //public void BindZone()
    //{
    //    UserRoleBAL objBAL = new UserRoleBAL();
    //    ddlZone.DataTextField = "ZoneName";
    //    ddlZone.DataValueField = "ZoneName";
    //    ddlZone.DataSource = objBAL.GetZone();
    //    ddlZone.DataBind();
    //    CommonUtility.AddSelectToDropDown(ddlZone);
    //    CommonUtility.AddSelectToDropDown(ddlRegion);
    //    CommonUtility.AddSelectToDropDown(ddlBranch);


    //}

    #endregion

    #region BindRegion

    //public void BindRegion()
    //{
    //    UserRoleBO objBO = new UserRoleBO();
    //    UserRoleBAL objBAL = new UserRoleBAL();
    //    ddlRegion.DataTextField = "RegionName";
    //    ddlRegion.DataValueField = "RegionName";
    //    objBO.Zone = ddlZone.SelectedValue.ToString();
    //    ddlRegion.DataSource = objBAL.GetRegion(objBO.Zone);
    //    ddlRegion.DataBind();
    //}
    #endregion

    #region BindBranch

    //public void BindBranch()
    //{
    //    UserRoleBO objBO = new UserRoleBO();
    //    UserRoleBAL objBAL = new UserRoleBAL();
    //    ddlBranch.DataTextField = "BranchName";
    //    ddlBranch.DataValueField = "BranchCode";
    //    objBO.Branch = ddlRegion.SelectedValue.ToString();
    //    ddlBranch.DataSource = objBAL.GetBranch(objBO.Branch);
    //    ddlBranch.DataBind();

    //}
    #endregion

    #region Get Role

    public void GetRole()
    {
        UserRoleBO objBO = new UserRoleBO();
        UserRoleBAL objBAL = new UserRoleBAL();
        ddlRole.DataTextField = "RoleName";
        ddlRole.DataValueField = "RoleName";
        ddlRole.DataSource = objBAL.GetRole();
        ddlRole.DataBind();
        CommonUtility.AddSelectToDropDown(ddlRole);
    }

    #endregion
    
    #region Bind Designation Code

    public void GetDesignation()
    {
        try
        {
            DesignationBAL objBAL = new DesignationBAL();
            ddlDesignation.DataSource = objBAL.GetDesignation();
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataValueField = "Designation";
            ddlDesignation.DataBind();
            CommonUtility.AddSelectToDropDown(ddlDesignation);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

    }

    #endregion

    #region Bind Channel Code

    public void GetChannel()
    {
        ChannelBAL objBAL = new ChannelBAL();
        ddlChannel.DataSource = objBAL.GetChannel();
        ddlChannel.DataTextField = "ChannelName";
        ddlChannel.DataValueField = "ChannelName";
        ddlChannel.DataBind();
        CommonUtility.AddSelectToDropDown(ddlChannel);
    }

    #endregion

    #region btnSubmit_Click

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            UserRoleBAL objBAL = new UserRoleBAL();
            UserRoleBO objBO = new UserRoleBO();
            objBO.UserName = txtUserName.Text.ToString();
            string strReturnValue = objBAL.CheckExistingMember(objBO.UserName);
            if (strReturnValue == "1")
            {
                lblMessage.Text = "Username " + objBO.UserName + " already exists!";
            }
            else
            {                
                string strBranchCode, strEmail, strPassword, strUserName, strRole, strCreatedBy, strEmployeeName, strDesignation, strChannel;
                UserBAL objuBAL = new UserBAL();
                DateTime dtExpiryDate=new DateTime();

                if (CheckValidations(txtPassword.Text.Trim(), txtUserName.Text.ToString()) == true)
                {
                    dtExpiryDate = Convert.ToDateTime(txtUserExpirationDate.Text.Trim());
                    strBranchCode = ddlBranch.SelectedValue;
                    strEmail = txtEmail.Text;
                    //strPassword = Encrypt(txtPassword.Text.Trim());
                    strPassword = SHAEncription(txtPassword.Text.Trim());
                    strUserName = txtUserName.Text.Trim();
                    strRole = ddlRole.SelectedValue;
                    strCreatedBy = Membership.GetUser().UserName;
                    strEmployeeName = txtEmployeeName.Text.Trim();
                    strDesignation = ddlDesignation.SelectedValue;
                    strChannel = ddlChannel.SelectedValue;
                    objuBAL.CreateUser(strUserName, strPassword, strEmail, strRole, strBranchCode, strCreatedBy, strEmployeeName, strDesignation, strChannel, dtExpiryDate);
                    lblMessage.Text = "Username " + strUserName + " created successfully.";
                }
              
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

    #region Reset Button Code

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateUser.aspx");
        //txtUserName.Text = "";
        //txtPassword.Text = "";
        //txtConfirmPwd.Text = "";
        //txtemail.Text = "";
        //ddlZone.SelectedIndex = 0;
        //ddlRegion.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        //ddlRole.SelectedIndex = 0;
    }

    #endregion

    #region Method to Encrypt the Password

    public string Encrypt(String strValue)
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
        //if (strNewPassword.Length < 8)
        //{
        //    lblMessage.Text = "Password should be minimum length of 8 characters.";
        //    return false;
        //}
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

    #region Check AlphaNumeric
    public bool IsValidAlphaNumeric(string inputStr)
    {

        if (string.IsNullOrEmpty(inputStr))
            return false;

        for (int i = 0; i < inputStr.Length; i++)
        {
            if (!(char.IsLetter(inputStr[i])) && (!(char.IsNumber(inputStr[i]))))
                return false;
        }
        if (CheckLetter(inputStr) != true)
        {
            return false;
        }
        if (CheckNumber(inputStr) != true)
        {
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
