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
using CallDeskBAL;
using AjaxControlToolkit;

public partial class Admin_AddTemplateDocuments : System.Web.UI.Page
{

    private int intCnt;

    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);        
        if (!IsPostBack)
        {
            GetApplicationFileTemplateMapping();
        }
    }

    #endregion

    #region Submit Button Code

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ApplicationFileTemplateBAL objBAL = new ApplicationFileTemplateBAL();
            int intReturnVal, intRowID;
            string strCreatedBy, strFileTemplateName, strCheckFileName;

            intRowID = Convert.ToInt32(ddlIssueRequestSubType.SelectedValue);
            strCreatedBy = Membership.GetUser().UserName;
            strFileTemplateName = fuUpLoadFile.FileName;

            // [CR-18] Vulnaribility file extension check Start           
            string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "txt", "jpeg" };
            string ext = System.IO.Path.GetExtension(fuUpLoadFile.PostedFile.FileName);
            bool isValidFile = false;

            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break;
                }
            }
            if (!isValidFile)
            {

                lblMessage.Text = "Invalid File. Please upload a File with extension " +
                               string.Join(",", validFileTypes);
                return;
            }

            if (strFileTemplateName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) != -1)
            {
                lblMessage.Text = "The filename is invalid";
                return;
            }

            string strFileNameWithoutExtension = strFileTemplateName.Remove(strFileTemplateName.LastIndexOf("."));
            if (strFileNameWithoutExtension.Contains(".") == true)
            {
                lblMessage.Text = "In the filename dot is not allowed";
                return;
            }

            // [CR-18] Vulnaribility File extension check End



            strCheckFileName = objBAL.CheckFileTemplateNameExists(strFileTemplateName);

            if (strCheckFileName == strFileTemplateName)
            {
                lblMessage.Text = "File template name already exists";
                return;
            }
            else
            {
                if (fuUpLoadFile.HasFile == true && fuUpLoadFile.PostedFile != null)
                {
                    string savePath;

                    savePath = Server.MapPath("..\\TemplateFiles") + "\\" + strFileTemplateName;
                    fuUpLoadFile.PostedFile.SaveAs(savePath);
                }
                intReturnVal = objBAL.AddApplicationFileTemplateMapping(intRowID, strFileTemplateName, strCreatedBy);
                if (intReturnVal == 1)
                {
                    lblMessage.Text = "Application and file template mapping saved successfully";
                    GetApplicationFileTemplateMapping();
                }
                else
                {
                    lblMessage.Text = "Mapping already exists";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Get Application File Template Mapping

    private void GetApplicationFileTemplateMapping()
    {
        try
        {
            ApplicationFileTemplateBAL objBAL = new ApplicationFileTemplateBAL();
            DataSet dsApplicationFileMapping = new DataSet();
            dsApplicationFileMapping = objBAL.GetApplicationFileTemplateMapping();
            gvApplicationFileMapping.DataSource = dsApplicationFileMapping;
            gvApplicationFileMapping.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Grid Row Command Event

    protected void gvApplicationFileMapping_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ApplicationFileTemplateBAL objBAL = new ApplicationFileTemplateBAL();
            int intRowID, intReturnVal;
            string strUserName;

            strUserName = Membership.GetUser().UserName;
            intRowID = int.Parse(Convert.ToString(e.CommandArgument));

            if (e.CommandName == "EditRow")
            {

            }
            else if (e.CommandName == "DeleteRow")
            {
                intReturnVal = objBAL.DeleteApplicationFileTemplateMapping(intRowID, strUserName);
                if (intReturnVal == 1)
                {
                    GetApplicationFileTemplateMapping();
                    lblMessage.Text = "Application and file template mapping deleted successfully.";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Grid Row Data Bound Event

    protected void gvApplicationFileMapping_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
                intCnt = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                intCnt++;
                ModalPopupExtender objModalPopupExtender = (ModalPopupExtender)e.Row.FindControl("md1");
                ImageButton objLinkButton = (ImageButton)e.Row.FindControl("lnkDelete");
                objModalPopupExtender.BehaviorID = "mdlPopup" + intCnt.ToString();
                objLinkButton.OnClientClick = "showConfirm(this, 'mdlPopup" + intCnt.ToString() + "'); return false;";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Page Index Change Code

    protected void gvApplicationFileMapping_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvApplicationFileMapping.PageIndex = e.NewPageIndex;
            GetApplicationFileTemplateMapping();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion
    
}

