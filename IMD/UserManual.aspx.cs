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
            string strFile = "IMD_User_Manual.doc";
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





}
