using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CallDeskBAL;
using CallDeskBO;
using AjaxControlToolkit;

public partial class Admin_ApplicationIssueRequestMapping : System.Web.UI.Page
{
    private int intCnt;

    #region Page Level Varaibles
    ArrayList lasset = new ArrayList();
    ArrayList lsubordinate = new ArrayList();
    static ArrayList UpdateList = new ArrayList();

    #endregion

    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);        
        if (!IsPostBack)
        {
            GetApplications();
            // GetIssueRequestType();
            GetMappedApplicationIssueRequestType();
        }
    }

    #endregion

    #region Get Mapped Application Issue Request Type

    public void GetMappedApplicationIssueRequestType()
    {
        try
        {
            RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
            gvMappedApplicationIssueRequest.DataSource = objBAL.GetMappedApplicationIssueRequestType();
            gvMappedApplicationIssueRequest.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Get Application

    public void GetApplications()
    {
        try
        {
            RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
            ddlApplications.DataSource = objBAL.GetApplicationType();
            ddlApplications.DataTextField = "ApplicationName";
            ddlApplications.DataValueField = "ApplicationID_PK";
            ddlApplications.DataBind();
            CommonUtility.AddSelectToDropDown(ddlApplications);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Get Issue Request Type

    public void GetIssueRequestType()
    {
        try
        {
            AdminBAL objBAL = new AdminBAL();
            lstbxIssueRequest.DataSource = objBAL.GetIssueRequestType();
            lstbxIssueRequest.DataTextField = "IssueRequestType";
            lstbxIssueRequest.DataValueField = "IssueRequestType_PK";
            lstbxIssueRequest.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Add From Issue Request ListBox

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstbxIssueRequest.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstbxIssueRequest.Items.Count; i++)
                {
                    if (lstbxIssueRequest.Items[i].Selected)
                    {
                        if (!lasset.Contains(lstbxIssueRequest.Items[i]))
                        {
                            lasset.Add(lstbxIssueRequest.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < lasset.Count; i++)
                {
                    if (!lstbxSelectedIssueRequest.Items.Contains(((ListItem)lasset[i])))
                    {
                        lstbxSelectedIssueRequest.Items.Add(((ListItem)lasset[i]));
                    }
                    lstbxIssueRequest.Items.Remove(((ListItem)lasset[i]));
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

    #region Remove From Issue Request ListBox

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstbxSelectedIssueRequest.SelectedItem != null)
            {
                for (int i = 0; i < lstbxSelectedIssueRequest.Items.Count; i++)
                {
                    if (lstbxSelectedIssueRequest.Items[i].Selected)
                    {
                        if (!lsubordinate.Contains(lstbxSelectedIssueRequest.Items[i]))
                        {
                            lsubordinate.Add(lstbxSelectedIssueRequest.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < lsubordinate.Count; i++)
                {
                    if (!lstbxIssueRequest.Items.Contains(((ListItem)lsubordinate[i])))
                    {
                        lstbxIssueRequest.Items.Add(((ListItem)lsubordinate[i]));
                    }
                    lstbxSelectedIssueRequest.Items.Remove(((ListItem)lsubordinate[i]));
                    UpdateList.Add(lsubordinate[i]);
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

    #region Create Application Issue Requets Mapping Code

    protected void btnCreateMapping_Click(object sender, EventArgs e)
    {
        try
        {
            AdminBAL objAdminBAL = new AdminBAL();
            List<AdminBOList> lstAdminBO = new List<AdminBOList>();
            AdminBO objAdminBO = new AdminBO();
            AdminBOList objAdminBOLst;

            objAdminBO.ApplicationID = int.Parse(ddlApplications.SelectedValue);
            objAdminBO.CreatedBy = Membership.GetUser().UserName;
            
            foreach (ListItem lstItem in lstbxSelectedIssueRequest.Items)
            {
                objAdminBOLst = new AdminBOList();
                objAdminBOLst.IssueRequestID = int.Parse(lstItem.Value);
                lstAdminBO.Add(objAdminBOLst);
            }
            objAdminBO.AdminBOList = lstAdminBO;

            int intReturnVal = objAdminBAL.AddApplicationIssueRequestMapping(objAdminBO);
            
            if (intReturnVal > 0)
            {
                lblMessage.Text = "Application Issue Request Mapping Created.";
                GetMappedApplicationIssueRequestType();
                ClearContros();
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }        

    #endregion

    #region Clear Controls Code

    private void ClearContros()
    {
        ddlApplications.SelectedValue = "0";
        lstbxIssueRequest.Items.Clear();
        lstbxSelectedIssueRequest.Items.Clear();
    }

    #endregion
    
    #region Application Issue Request Mapping Grid Page Index Change Event

    protected void gvMappedApplicationIssueRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMappedApplicationIssueRequest.PageIndex = e.NewPageIndex;
            if (Convert.ToString(ViewState["SortPaging"]) == "Yes")
            {
                GetMappedApplicationIssueRequestApplicationID();
            }
            else
            {
                GetMappedApplicationIssueRequestType();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion
    
    #region Application Issue Request Type Mapping Row Command Event

    protected void gvMappedApplicationIssueRequest_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int intRowID, intReturnVal;
            lblMessage.Text = "";

            if (e.CommandName == "lnkEdit")
            {
                //Session["RowId"] = intRowID;
                //Response.Redirect("EditAIRSubtypeMapping.aspx");
            }
            else if (e.CommandName == "lnkDelete")
            {
                AdminBO objBO = new AdminBO();
                AdminBAL objBAL = new AdminBAL();

                intRowID = Convert.ToInt32(e.CommandArgument);

                objBO.DeletedBy = Membership.GetUser().UserName;

                intReturnVal = objBAL.DeleteApplicationIssueRequestTypeMapping(intRowID, objBO.DeletedBy);

                if (intReturnVal == 1)
                {
                    lblMessage.Text = "Mapping deleted successfully.";
                    GetMappedApplicationIssueRequestType();
                }
                else
                {
                    lblMessage.Text = "Application Issue Request Mapping in Sub Type Table Exists.";
                }
            }
            else if (e.CommandName == "btnSortApplications")
            {
                ViewState["SortPaging"] = "Yes";
                GetMappedApplicationIssueRequestApplicationID();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Get Issue Request Sub Type By Application ID

    public void GetMappedApplicationIssueRequestApplicationID()
    {
        try
        {            
            RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
            DropDownList ddlApplicationName = (DropDownList)gvMappedApplicationIssueRequest.FooterRow.FindControl("ddlApplicationName");

            if (Convert.ToString(ddlApplicationName.SelectedValue) != "")
            {
                ViewState["ApplicationID"] = ddlApplicationName.SelectedValue;
            }
            int intApplicationID = Convert.ToInt32(ViewState["ApplicationID"]);
            gvMappedApplicationIssueRequest.DataSource = objBAL.GetMappedApplicationIssueRequestbyApplicationID(intApplicationID);
            gvMappedApplicationIssueRequest.DataBind();
            lblMessage.Text = "";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Row Data Bound Code

    protected void gvMappedApplicationIssueRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
                intCnt = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                intCnt++;
                ModalPopupExtender objModalPopupExtender = (ModalPopupExtender)e.Row.FindControl("md1");
                ImageButton objLinkButton = (ImageButton)e.Row.FindControl("lnkbtnDelete");
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
    
    #region Cancel Button Code

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ApplicationIssueRequestMapping.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Application Dropdown Selected Index Change Event

    protected void ddlApplications_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AdminBAL objBAL = new AdminBAL();

            int intApplicationID = Convert.ToInt32(ddlApplications.SelectedValue);
            lstbxSelectedIssueRequest.Items.Clear();
            lstbxIssueRequest.DataSource = objBAL.GetIssueRequestTypeNotMappedToApplication(intApplicationID);
            lstbxIssueRequest.DataTextField = "IssueRequestType";
            lstbxIssueRequest.DataValueField = "IssueRequestType_PK";
            lstbxIssueRequest.DataBind();            
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion
    
}
