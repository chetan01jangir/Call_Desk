using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CallDeskDAL;
using CallDeskBO;
using CallDeskBAL;
using System.Data.SqlClient;
using IMDPortalUserValidate;
using POSUserValidate;
//using MPLUSUserValidate;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class UpdateProfile_UpdateProfile : System.Web.UI.Page
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
            if (Request.QueryString["AppType"] != null && Request.QueryString["UserID"] != null)
            {
                string strAppType = Request.QueryString["AppType"];
                string strUserID = Convert.ToString(Request.QueryString["UserID"]);
                //added by shilpa on 23-09-2020

                if (strAppType != "POS" && strAppType != "IMD" && strAppType != "Portal" && strAppType.ToLower() != "smartzone" && strAppType.ToLower() != "Smartzone - CSC" && strAppType.ToLower() != "Smartzone - Non CSC" && strAppType.ToLower() != "Smartzone - Tele Sales")

                // if (strAppType != "POS" && strAppType != "IMD" && strAppType != "Portal" )
                {


                    strUserID = Decrypt12(strUserID);
                    strAppType = Decrypt12(strAppType);
                }

                //txtAppType.Text = SHAEncription(strAppType);
                //txtUserName.Text = SHAEncription(strUserID);
                //txtAppType.Text = Decrypt(strAppType);
                //txtUserName.Text = Decrypt(strUserID);
                txtAppType.Text = strAppType;
                txtUserName.Text = strUserID;

                if (!string.IsNullOrEmpty(strAppType))
                {
                    txtAppType.Enabled = false;
                }
                else
                {
                    txtAppType.Enabled = true;
                }
                if (!string.IsNullOrEmpty(strUserID))
                {
                    txtUserName.Enabled = false;
                }
                else
                {
                    txtUserName.Enabled = true;
                }
            }

        }
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
            throw new Exception(ex.Message);
        }
        return intReturnValue;
    }
    #endregion

    #region btnSave_Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Session["AgentUserID"] = null;

            string sUserName = txtUserName.Text.Trim();
            string sAppName = txtAppType.Text.Trim();
            string strReturnValue = objBAL.CheckExistingMember(sUserName);
            if (strReturnValue == "1")
            {
                lblMessage.Text = "Username " + sUserName + " already exists!";
            }
            else
            {
                string strBranchCode, strEmail, strPassword, strUserName, strRole, strCreatedBy,
                strAgentName, strDesignation, strChannel, strMobileNo, strApplicationType, strAgentCode,
                strQuestion, strAnswer, strGender, SMCode, SMBranchCode, SMBranchName, SMRegionName, SMZoneName, SMName;

                DateTime dtExpiryDate = new DateTime();
                DateTime dtBirthDate = new DateTime();

                if (CheckValidations(sUserName) == true)
                {

                    dtExpiryDate = DateTime.Now.Date;
                    dtBirthDate = DateTime.Now.Date;

                    strBranchCode = "A000";
                    strEmail = txtEmail.Text.ToString().Trim();
                    strPassword = SHAEncription("calldesk123");
                    strUserName = txtUserName.Text.ToString().Trim();
                    strRole = "Agent";
                    strCreatedBy = "rgiadmin";
                    strAgentName = txtAgentName.Text.ToString().Trim();
                    strDesignation = "AGENT IMDsPOS";
                    strChannel = "Channel IMDsPOS";
                    strMobileNo = txtMobileNo.Text.Trim();
                    strApplicationType = txtAppType.Text.Trim();
                    strAgentCode = strUserName;
                    strQuestion = "Quest";
                    strAnswer = "Ans";
                    strGender = "";

                    SMName = string.Empty;
                    SMCode = string.Empty;

                    SMBranchCode = string.Empty;
                    SMBranchName = string.Empty;
                    SMRegionName = string.Empty;
                    SMZoneName = string.Empty;

                    if (strApplicationType.ToLower() == "portal" || strApplicationType.ToLower() == "smartzone")
                    //if (strApplicationType.ToLower() == "portal" )
                    {
                        UserDetails oIMDUser = new UserDetails();
                        DataTable ODTUser = oIMDUser.GetUserDetailsCallDesk(txtUserName.Text.ToString());
                        if (ODTUser != null)
                        {
                            if (ODTUser.Rows.Count > 0)
                            {
                                //if (!string.IsNullOrEmpty(ODTUser.Rows[0]["SM_Code"].ToString()))
                                //{
                                //    SMCode = ODTUser.Rows[0]["SM_Code"].ToString();
                                //}
                                //else
                                //{
                                //    SMCode = "";
                                //}


                                if (!string.IsNullOrEmpty(ODTUser.Rows[0]["UserID"].ToString()))
                                {
                                    string sAgentCode = ODTUser.Rows[0]["UserID"].ToString().Trim().ToLower();
                                    SMCode = ODTUser.Rows[0]["SM_Code"].ToString().Trim().ToLower();

                                    if (sAgentCode != string.Empty)
                                    {
                                        DataTable ODtUserPortalDet = new DataTable();
                                        ODtUserPortalDet = GetUserDetailsByUserName(SMCode);
                                        if (ODtUserPortalDet != null)
                                        {
                                            if (ODtUserPortalDet.Rows.Count > 0)
                                            {
                                                SMName = Convert.ToString(ODtUserPortalDet.Rows[0]["EmployeeName"]);

                                            }
                                        }

                                    }
                                }

                                if (string.IsNullOrEmpty(SMName))
                                {
                                    if (!string.IsNullOrEmpty(ODTUser.Rows[0]["SM_Name"].ToString()))
                                    {
                                        SMName = ODTUser.Rows[0]["SM_Name"].ToString();
                                    }
                                    else
                                    {
                                        SMName = "";
                                    }
                                }


                                if (!string.IsNullOrEmpty(ODTUser.Rows[0]["Rel_Equavalent"].ToString()))
                                {
                                    SMBranchCode = ODTUser.Rows[0]["Rel_Equavalent"].ToString();
                                }
                                else
                                {
                                    SMBranchCode = "";
                                }
                                if (!string.IsNullOrEmpty(ODTUser.Rows[0]["SM_Branch"].ToString()))
                                {
                                    SMBranchName = ODTUser.Rows[0]["SM_Branch"].ToString();
                                }
                                else
                                {
                                    SMBranchName = "";
                                }

                                if (!string.IsNullOrEmpty(ODTUser.Rows[0]["SM_Region"].ToString()))
                                {
                                    SMRegionName = ODTUser.Rows[0]["SM_Region"].ToString();
                                }
                                else
                                {
                                    SMRegionName = "";
                                }

                                if (!string.IsNullOrEmpty(ODTUser.Rows[0]["SM_Zone"].ToString()))
                                {
                                    SMZoneName = ODTUser.Rows[0]["SM_Zone"].ToString();
                                }
                                else
                                {
                                    SMZoneName = "";
                                }
                            }
                        }

                    }
                    else if (strApplicationType.ToLower() == "pos")
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


                                        if (!string.IsNullOrEmpty(ODTPOS.Rows[0]["UserID"].ToString()))
                                        {
                                            string sAgentCode = ODTPOS.Rows[0]["UserID"].ToString().Trim().ToLower();
                                            SMCode = ODTPOS.Rows[0]["SMCode"].ToString().Trim().ToLower();

                                            if (sAgentCode != string.Empty)
                                            {
                                                DataTable ODtUserPOSDet = new DataTable();
                                                ODtUserPOSDet = GetUserDetailsByUserName(SMCode);
                                                if (ODtUserPOSDet != null)
                                                {
                                                    if (ODtUserPOSDet.Rows.Count > 0)
                                                    {
                                                        SMName = Convert.ToString(ODtUserPOSDet.Rows[0]["EmployeeName"]);

                                                    }
                                                }

                                            }
                                        }

                                        if (string.IsNullOrEmpty(SMName))
                                        {
                                            if (!string.IsNullOrEmpty(ODTPOS.Rows[0]["SMName"].ToString()))
                                            {
                                                SMName = ODTPOS.Rows[0]["SMName"].ToString();
                                            }
                                            else
                                            {
                                                SMName = "";
                                            }
                                        }


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
                    else if (strApplicationType.ToLower() == "mplus")
                    {
                        strDesignation = "AGENT MPLUS";
                        strChannel = "Channel MPLUS";
                    }
                    CreateAgentUser(strUserName, strPassword, strEmail, strRole, strBranchCode, strCreatedBy,
                    strAgentName, strDesignation, strChannel, dtExpiryDate, strQuestion, strAnswer, strMobileNo,
                    dtBirthDate, strApplicationType, strAgentCode, strGender, SMCode, SMBranchCode, SMBranchName, SMRegionName, SMZoneName, SMName);


                    Session["AgentUserID"] = txtUserName.Text.Trim();
                    objEMBAL.InsertLastLoginLogOut(Convert.ToString(Session["AgentUserID"]), DateTime.Now, DateTime.Now);
                    Session["UserJustLoginDate"] = DateTime.Now;
                    Response.Redirect("Default.aspx");
                }
            }


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

    #region CheckValidations
    public bool CheckValidations(string userName)
    {
        string sUserName = txtUserName.Text.Trim();
        string sAppName = txtAppType.Text.Trim();
        bool Isvaliduser = true;

        string strAgentCodeUser = string.Empty;
        //added by shilpa on 09/09/2020

        if (txtAppType.Text.Trim().ToLower() == "portal" || txtAppType.Text.Trim().ToLower() == "imd" || txtAppType.Text.Trim().ToLower() == "smartzone" || txtAppType.Text.Trim().ToLower() == "Smartzone - CSC" || txtAppType.Text.Trim().ToLower() == "Smartzone - Non CSC" || txtAppType.Text.Trim().ToLower() == "Smartzone - Tele Sales")
        //if (txtAppType.Text.Trim().ToLower() == "portal" || txtAppType.Text.Trim().ToLower() == "imd"  )
        {
            UserDetails oIMDUser = new UserDetails();
            DataTable ODTUser = oIMDUser.GetUserDetailsCallDesk(txtUserName.Text.ToString());
            //DataTable ODTUser = GetIMDPortalAgentDetails(txtUserName.Text.ToString().Trim());
            if (ODTUser != null)
            {
                if (ODTUser.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ODTUser.Rows[0]["UserID"].ToString()))
                    {
                        strAgentCodeUser = ODTUser.Rows[0]["UserID"].ToString().Trim().ToLower();
                    }
                }
            }
            if (strAgentCodeUser != userName.Trim().ToLower())
            {
                lblMessage.Text = "User ID is not exists in IMD-Portal System.";
                //return false;
                Isvaliduser = false;

            }
        }
        else if (txtAppType.Text.Trim().ToLower() == "pos")
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
                lblMessage.Text = "User ID is not exists in POS System.";
                //return false;
                Isvaliduser = false;
            }

        }
        else if (sAppName.ToLower() == "mplus" || sAppName == "Smartzone" || sAppName == "SmartZone-CSC" || sAppName == "Smartzone - Tele Sales" || sAppName == "Smartzone - Non CSC")
        {

            //bool result;
            //bool resultspecified;
            //CallHelpDesk oMPLUSUser = new CallHelpDesk();
            //oMPLUSUser.CheckValidUser(txtUserName.Text.Trim(), out result, out resultspecified);

            //if (result == true)
            //{
            //    strAgentCodeUser = txtUserName.Text.Trim();

            //}

            //else
            //{
            //    lblMessage.Text = "User ID is not exists in MPLUS System.";
            //    //return false;
            //    Isvaliduser = false;
            //}
        }



        else
        {
            Isvaliduser = false;
            lblMessage.Text = "Application does not exists.";
        }

        return Isvaliduser;
        //return true;


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
}
