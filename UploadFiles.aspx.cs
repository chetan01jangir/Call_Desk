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

public partial class UploadFiles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fuUpLoadFile.HasFile == true && fuUpLoadFile.PostedFile != null)
        {
            string fileName;
            string savePath;
            fileName = System.IO.Path.GetFileName(fuUpLoadFile.PostedFile.FileName);
            savePath = Server.MapPath("Uploads\\") + fileName;
            fuUpLoadFile.PostedFile.SaveAs(savePath);
            lblMsg.Text = "File Uploaded sucessfully.";
        }
    }


}
