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
using System.IO;

public partial class User_UserManual : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    #region Manual DownLoad Click
    protected void lnkManualDownLoad_Click(object sender, EventArgs e)
    {

        try
        {
            string strFile = "Calldesk_User_manual.docx";
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
            string filename = Server.MapPath("..\\ManualFiles\\") + strFileName;
            file = new FileInfo(filename);
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(filename);
            Response.End();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #endregion

	#region Do's and Don'ts DownLoad Click
	protected void lnkdos_Click(object sender, EventArgs e)
	{

		try
		{
			string strFile = "DOs_and_Donts.docx";
			DownloadFile(strFile);
		}
		catch (Exception ex)
		{
			lblMessage.Text = ex.Message;
		}
	}
#endregion

    protected void lnkSmartPassword_Click(object sender, EventArgs e)
    {

        try
        {
            string strFile = "Smart Zone -Traning module Reset or Forgot Password.xls";
            DownloadFile(strFile);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void lnkSmartAgentHNIN_Click(object sender, EventArgs e)
    {

        try
        {
            string strFile = "Smart zone - Traning module -Agent & HNIN Active & Inactive status.xls";
            DownloadFile(strFile);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void lnkSmartUserIdCreation_Click(object sender, EventArgs e)
    {

        try
        {
            string strFile = "Smart Zone - Traning Module for Sub User ID Creation.xlsx";
            DownloadFile(strFile);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void lnkSmartProfileUpdation_Click(object sender, EventArgs e)
    {

        try
        {
            string strFile = "Smart Zone - Traning module for Profile Updation for Email & Mobile number.xlsx";
            DownloadFile(strFile);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void lnkSmartBranchMapping_Click(object sender, EventArgs e)
    {

        try
        {
            string strFile = "Smart Zone & MOM  -Traning module Branch Mapping for SM-Vendor in MOM & Smart Zone.xlsx";
            DownloadFile(strFile);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void lnkSmartIDLocked_Click(object sender, EventArgs e)
    {

        try
        {
            string strFile = "Smart Zone -Traning module for Smart Zone ID showing locked.xls";
            DownloadFile(strFile);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }


}
