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
using CallDeskBO;
using CallDeskBAL;

public partial class Admin_EditAIRSubtypeMapping : System.Web.UI.Page
{

    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        chkSelectAll.Attributes.Add("Onclick", "ChkUnChk('ctl00_ContentPlaceHolder1_chkSelectAll','ctl00_ContentPlaceHolder1_chkLocationType');");
        ddlIssueRequest.Attributes.Add("OnChange", "HideTableRow();");
        txtValidFrom.Attributes.Add("readonly", "true");
        txtValidTo.Attributes.Add("readonly", "true");
        chkLocationType.Attributes.Add("Onclick", "CheckUnCheckAll('ctl00_ContentPlaceHolder1_chkSelectAll','ctl00_ContentPlaceHolder1_chkLocationType');");

        if (!IsPostBack)
        {
            try
            {
                // GetApproverAuthority();
                GetLocationType();
                // GetRoles();
                GetGroups();
                int intRowID = Convert.ToInt32(Session["AIRSTRowId"]);
                RegisterNewCallBAL objBAL = new RegisterNewCallBAL();
                DataSet dsMappedData = new DataSet();
                DataSet dsMappedLocationTypes = new DataSet();

                dsMappedData = objBAL.GetMappedApplicationIssueRequestSubType(intRowID);
                dsMappedLocationTypes = objBAL.GetMappedLocationTypesByIssueRequestSubTypeID(intRowID);

                lblApplication.Text = dsMappedData.Tables[0].Rows[0]["ApplicationName"].ToString();
                lblIssueRequestType.Text = dsMappedData.Tables[0].Rows[0]["IssueRequestType"].ToString();
                lblIssueRequestSubType.Text = dsMappedData.Tables[0].Rows[0]["IssueRequestSubType"].ToString();
                ddlIssueRequest.SelectedValue = dsMappedData.Tables[0].Rows[0]["IssueRequest"].ToString();
                // ddlApproverAuthority.SelectedValue = dsMappedData.Tables[0].Rows[0]["ApproverAuthorityID"].ToString();

                ddlMail.SelectedValue = (dsMappedData.Tables[0].Rows[0]["SendEmail"].ToString() == "Yes") ? "0" : "1";
                ddlSMS.SelectedValue = (dsMappedData.Tables[0].Rows[0]["SendSMS"].ToString() == "Yes") ? "0" : "1";
                // ddlRole.SelectedValue = dsMappedData.Tables[0].Rows[0]["ApproverRole"].ToString();
                ddlPriority.SelectedValue = dsMappedData.Tables[0].Rows[0]["Priority"].ToString();
                ddlGroups.SelectedValue = dsMappedData.Tables[0].Rows[0]["Groups"].ToString();
                txtCallTAT.Text = dsMappedData.Tables[0].Rows[0]["CallTAT"].ToString();
                txtComment.Text = dsMappedData.Tables[0].Rows[0]["Description"].ToString();
                txtValidFrom.Text = Convert.ToDateTime(dsMappedData.Tables[0].Rows[0]["ValidFrom"]).ToString("dd/MM/yyyy");
                txtValidTo.Text = Convert.ToDateTime(dsMappedData.Tables[0].Rows[0]["ValidTo"]).ToString("dd/MM/yyyy");

                if (dsMappedLocationTypes.Tables[0].Rows.Count > 1)
                {
                    for (int i = 0; i < chkLocationType.Items.Count; i++)
                    {
                        for (int j = 0; j < dsMappedLocationTypes.Tables[0].Rows.Count; j++)
                        {
                            if (chkLocationType.Items[i].Value == Convert.ToString(dsMappedLocationTypes.Tables[0].Rows[j]["TypeId"]))
                            {
                                chkLocationType.Items[i].Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    int intCheckVal = Convert.ToInt32(dsMappedLocationTypes.Tables[0].Rows[0]["LocationTypeID_FK"]);
                    if (intCheckVal == 0)
                    {
                        chkSelectAll.Checked = true;
                        for (int i = 0; i < chkLocationType.Items.Count; i++)
                        {
                            chkLocationType.Items[i].Selected = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < chkLocationType.Items.Count; i++)
                        {
                            if (Convert.ToString(dsMappedLocationTypes.Tables[0].Rows[0]["TypeId"]) == chkLocationType.Items[i].Value)
                            {
                                chkLocationType.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
				lblMessage.Text = "Error has occurred please contact the administrator.";
            }
        }
    }

    #endregion

    #region Get ApproverAuthority

    public void GetApproverAuthority()
    {
        try
        {
            ApproverAuthorityBAL objBAL = new ApproverAuthorityBAL();
            ddlApproverAuthority.DataSource = objBAL.GetApproverAuthority();
            ddlApproverAuthority.DataTextField = "ApproverAuthority";
            ddlApproverAuthority.DataValueField = "ApproverAuthorityID_PK";
            ddlApproverAuthority.DataBind();
            CommonUtility.AddSelectToDropDown(ddlApproverAuthority);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Get Roles

    public void GetRoles()
    {
        try
        {
            UserRoleBAL objBAL = new UserRoleBAL();
            ddlRole.DataSource = objBAL.GetRole();
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "RoleName";
            ddlRole.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Update Button Code

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            AdminBAL objAdminBAL = new AdminBAL();
            AdminBO objAdminBO = new AdminBO();

            objAdminBO.RowId = Convert.ToInt32(Session["AIRSTRowId"]);
            objAdminBO.UpdatedBy = Membership.GetUser().UserName;
            objAdminBO.IssueRequestType = ddlIssueRequest.SelectedValue;
            objAdminBO.Priority = ddlPriority.SelectedValue;

            if (ddlIssueRequest.SelectedValue != "Request")
            {
                objAdminBO.Email = 1;
                objAdminBO.SMS = 1;
                objAdminBO.ApproverAuthorityID = 0;
            }
            else
            {
                objAdminBO.Email = Convert.ToInt32(ddlMail.SelectedValue);
                objAdminBO.SMS = Convert.ToInt32(ddlSMS.SelectedValue);
                // objAdminBO.ApproverAuthorityID = Convert.ToInt32(ddlApproverAuthority.SelectedValue);
                // objAdminBO.Role = ddlRole.SelectedValue;                
            }

            objAdminBO.ValidFrom = Convert.ToDateTime(CommonUtility.ConvertDateToMMddyyyy(txtValidFrom.Text));
            objAdminBO.ValidTo = Convert.ToDateTime(CommonUtility.ConvertDateToMMddyyyy(txtValidTo.Text));
            objAdminBO.Groups = ddlGroups.SelectedValue;
            objAdminBO.Comment = txtComment.Text;
            objAdminBO.CallTAT = Convert.ToInt32(txtCallTAT.Text);

            List<LocationBOList> lstLocationBO = new List<LocationBOList>();
            LocationBOList lstBOLocation;
            if (chkSelectAll.Checked == true)
            {
                lstBOLocation = new LocationBOList();
                lstBOLocation.LocationTypeID = 0;
                lstLocationBO.Add(lstBOLocation);
            }
            else
            {
                for (int i = 0; i < chkLocationType.Items.Count; i++)
                {
                    if (chkLocationType.Items[i].Selected)
                    {
                        lstBOLocation = new LocationBOList();
                        lstBOLocation.LocationTypeID = Convert.ToInt32(chkLocationType.Items[i].Value);
                        lstLocationBO.Add(lstBOLocation);
                    }
                }
            }

            objAdminBO.LocationBOType = lstLocationBO;

            int intReturnVal = objAdminBAL.UpdateApplicationIssueRequestSubTypeDetails(objAdminBO);
            if (intReturnVal == 1)
            {
                // lblMessage.Text = "Record updated successfully.";
                Response.Redirect("AddAIRSubtypeMapping.aspx");
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
            Response.Redirect("AddAIRSubtypeMapping.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Get Location Type

    public void GetLocationType()
    {
        try
        {
            AdminBAL objBAL = new AdminBAL();
            chkLocationType.DataSource = objBAL.GetLocationType();
            chkLocationType.DataTextField = "Location_Type";
            chkLocationType.DataValueField = "TypeId";
            chkLocationType.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Method to Bind Grid

    public void GetGroups()
    {
        try
        {
            GroupsBAL objBAL = new GroupsBAL();
            ddlGroups.DataSource = objBAL.GetGroups();
            ddlGroups.DataTextField = "Groups";
            ddlGroups.DataValueField = "Groups";
            ddlGroups.DataBind();
            CommonUtility.AddSelectToDropDown(ddlGroups);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion
}
