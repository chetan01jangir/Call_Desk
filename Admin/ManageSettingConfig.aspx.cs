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
using System.Xml;
using System.Text;
using System.IO;

public partial class Admin_ManageSettingConfig : System.Web.UI.Page
{
    #region Page Load Event

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            txtKey.Text = Session["Key"].ToString();
            txtValue.Text = Session["Value"].ToString();
        }
    }

    #endregion

    #region Get all the Keys and binding to dropdown

    //public void GetAllKeys()
    //{
    //    try
    //    {
    //        string baseDir = System.Web.HttpRuntime.AppDomainAppPath;
    //        string configPath = Path.Combine(baseDir, "Setting.config");

    //        DataSet dsConfigSettings = new DataSet();
    //        dsConfigSettings.ReadXml(configPath, XmlReadMode.Auto);

    //        ddlKey.DataSource = dsConfigSettings;
    //        ddlKey.DataTextField = "key";
    //        ddlKey.DataValueField = "value";
    //        ddlKey.DataBind();
    //        CommonUtility.AddSelectToDropDown(ddlKey);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //    }
    //}

    #endregion

    #region Save Button Code

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Session["Key"] = txtKey.Text;
            Session["Value"] = txtValue.Text;
            CommonUtility.SetValueByKey(txtKey.Text, txtValue.Text);
            lblMessage.Text = "Value changed successfully.";
            Response.Redirect("AddConfigSettings.aspx");
            // ClearControls();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Dropdown Selected Index Change Event

    //protected void ddlKey_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlKey.SelectedValue != "0")
    //        {
    //            txtKey.Text = ddlKey.SelectedItem.Text;
    //            txtValue.Text = ddlKey.SelectedValue;
    //        }
    //        else
    //        {
    //            txtKey.Text = "";
    //            txtValue.Text = "";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //    }
    //}

    #endregion

    #region Clear Controls

    public void ClearControls()
    {
        txtKey.Text = "";
        txtValue.Text = "";
    }

    #endregion

    #region Cancel Button Code

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("AddConfigSettings.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion   
}
