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

public partial class Admin_AddConfigSettings : System.Web.UI.Page
{
    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            GetAllKeys();
        }
    }

    #endregion    

    #region Get all the Keys and binding to dropdown

    public void GetAllKeys()
    {
        try
        {
            string baseDir = System.Web.HttpRuntime.AppDomainAppPath;
            string configPath = Path.Combine(baseDir, "Setting.config");

            DataSet dsConfigSettings = new DataSet();
            dsConfigSettings.ReadXml(configPath, XmlReadMode.Auto);

            gvConfigValues.DataSource = dsConfigSettings;
            gvConfigValues.DataBind();            
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Save Button Code

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string baseDir = System.Web.HttpRuntime.AppDomainAppPath;
            string configPath = Path.Combine(baseDir, "Setting.config");

            DataSet dsConfigSettings = new DataSet();
            dsConfigSettings.ReadXml(configPath, XmlReadMode.Auto);

            int i = dsConfigSettings.Tables[0].Rows.Count;
            
            DataRow dr = dsConfigSettings.Tables[0].NewRow();
            dr["key"] = txtKey.Text;
            dr["value"] = txtValue.Text;
            dsConfigSettings.Tables[0].Rows.Add(dr);
            dsConfigSettings.AcceptChanges();

            dsConfigSettings.WriteXml(configPath, XmlWriteMode.IgnoreSchema);

            lblMessage.Text = "Config values added successfully.";
            ClearControl();
            GetAllKeys();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion    

    #region Clear Controls

    public void ClearControl()
    {
        txtKey.Text = "";
        txtValue.Text = "";       
    }

    #endregion

    protected void gvConfigValues_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
            int index = gr.RowIndex;
            Label lblKey = (Label)gvConfigValues.Rows[index].FindControl("lblKey");
            Label lblValue = (Label)gvConfigValues.Rows[index].FindControl("lblValue");
            if (e.CommandName == "EditValues")
            {
                //txtKey.Text = lblKey.Text;
                //txtValue.Text = lblValue.Text;
                Session["Key"] = lblKey.Text;
                Session["Value"] = lblValue.Text;
                Response.Redirect("ManageSettingConfig.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
        
    }
}
