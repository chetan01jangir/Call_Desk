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
using CallDeskBAL;
using CallDeskBO;
using System.Collections.Generic;

public partial class Admin_AIRSTApproverMapping : System.Web.UI.Page
{

    #region Page Level Varaibles

    ArrayList lasset = new ArrayList();
    ArrayList lsubordinate = new ArrayList();
    static ArrayList UpdateList = new ArrayList();

    #endregion

    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        
        ddlType.Attributes.Add("onchange", "SearchCriteria();");

        if (!IsPostBack)
        {
            BindZone();
            WucSearchmoreUserMapping1.TextboxName = txtUserCode.ClientID;
            WucSearchmoreUserMapping2.TextboxName = txtSecondLevelApprover.ClientID;
        }
    }

    #endregion

    #region Bind Zone Code

    private void BindZone()
    {
        try
        {
            UserRoleBAL objBAL = new UserRoleBAL();
            DataSet dsZone = new DataSet();

            dsZone = objBAL.GetZone();
            lstbxZone.DataSource = dsZone;
            lstbxZone.DataValueField = "ZoneID_PK";
            lstbxZone.DataTextField = "ZoneName";
            lstbxZone.DataBind();
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
            if (lstbxBranch.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstbxBranch.Items.Count; i++)
                {
                    if (lstbxBranch.Items[i].Selected)
                    {
                        if (!lasset.Contains(lstbxBranch.Items[i]))
                        {
                            lasset.Add(lstbxBranch.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < lasset.Count; i++)
                {
                    if (!lstbxSelectedBranches.Items.Contains(((ListItem)lasset[i])))
                    {
                        lstbxSelectedBranches.Items.Add(((ListItem)lasset[i]));
                    }
                    lstbxBranch.Items.Remove(((ListItem)lasset[i]));
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
            if (lstbxSelectedBranches.SelectedItem != null)
            {
                for (int i = 0; i < lstbxSelectedBranches.Items.Count; i++)
                {
                    if (lstbxSelectedBranches.Items[i].Selected)
                    {
                        if (!lsubordinate.Contains(lstbxSelectedBranches.Items[i]))
                        {
                            lsubordinate.Add(lstbxSelectedBranches.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < lsubordinate.Count; i++)
                {
                    if (!lstbxBranch.Items.Contains(((ListItem)lsubordinate[i])))
                    {
                        lstbxBranch.Items.Add(((ListItem)lsubordinate[i]));
                    }
                    lstbxSelectedBranches.Items.Remove(((ListItem)lsubordinate[i]));
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

    #region Region Selected Index Change

    //protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string strRegionID = ddlRegion.SelectedValue;
    //        AdminBAL objAdminBAL = new AdminBAL();
    //        lstbxBranch.DataSource = objAdminBAL.GetBranchByRegion(strRegionID);
    //        lstbxBranch.DataTextField = "BranchName";
    //        lstbxBranch.DataValueField = "BranchCode";
    //        lstbxBranch.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //    }
    //}

    #endregion

    #region Add Approver Code

    protected void btnAddApprover_Click(object sender, EventArgs e)
    {
        try
        {
            // lstbxSelectedApprover.Items.Add(new ListItem(txtUserCode.Text, ddlApproverLever.SelectedValue));
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Create Mapping

    protected void btnCreateMapping_Click(object sender, EventArgs e)
    {
        try
        {
            BranchBOList objBBOList;
            ApproverAuthorityBO objAABO = new ApproverAuthorityBO();
            List<BranchBOList> lstBBO = new List<BranchBOList>();
            UserRoleBAL objRoleBAL = new UserRoleBAL();

            objAABO.AIRSTRowID = Convert.ToInt32(ddlIssueRequestSubType.SelectedValue);
            objAABO.CreatedBy = Membership.GetUser().UserName;
            objAABO.FApprover = txtUserCode.Text.Trim();
            objAABO.SApprover = txtSecondLevelApprover.Text.Trim();

            string strChkFirstApprover = objRoleBAL.CheckExistingMember(objAABO.FApprover);
            if (strChkFirstApprover != "1")
            {
                lblMessage.Text = "First " + objAABO.FApprover + " approver does not exists ";
                return;
            }

            if (txtSecondLevelApprover.Text.Trim() == "")
            {
                string strChkSecondApprover = objRoleBAL.CheckExistingMember(objAABO.SApprover);
                if (strChkFirstApprover != "1")
                {                    
                    lblMessage.Text = "First " + objAABO.SApprover + " approver does not exists ";
                    return;
                }
            }

            int intBranchCount = lstbxSelectedBranches.Items.Count;

            if (intBranchCount == 0)
            {
                objBBOList = new BranchBOList();
                objBBOList.BranchCode = "0";
                lstBBO.Add(objBBOList);
            }
            else
            {
                for (int j = 0; j < intBranchCount; j++)
                {
                    objBBOList = new BranchBOList();
                    objBBOList.BranchCode = lstbxSelectedBranches.Items[j].Value;
                    lstBBO.Add(objBBOList);
                }
            }
            objAABO.BranchList = lstBBO;

            ApproverAuthorityBAL objBAL = new ApproverAuthorityBAL();
            int intReturnVal = objBAL.CreateAIRSTApproverMapping(objAABO);
            if (intReturnVal > 0)
            {
                lblMessage.Text = "Approver Mapped Successfully.";
                ClearControls();
            }
            else
            {
                lblMessage.Text = "Mapping already exists branchwise.";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Zone List Box Change Event

    protected void lstbxZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lstbxBranch.Items.Clear();
            lstbxSelectedBranches.Items.Clear();

            AdminBAL objBAL = new AdminBAL();

            StringBuilder strbldZone = new StringBuilder();

            for (int i = 0; i < lstbxZone.Items.Count; i++)
            {
                if (lstbxZone.Items[i].Selected == true)
                {
                    strbldZone.Append(lstbxZone.Items[i].Value + ",");
                }
            }

            strbldZone.Remove(strbldZone.Length - 1, 1);

            lstbxRegions.DataSource = objBAL.GetRegionByMultipleZone(strbldZone.ToString());
            lstbxRegions.DataTextField = "RegionName";
            lstbxRegions.DataValueField = "RegionID_PK";
            lstbxRegions.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Region change event code

    protected void lstbxRegions_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AdminBAL objBAL = new AdminBAL();
            StringBuilder strbldRegion = new StringBuilder();
            string strAIRSTMID_FK;

            for (int i = 0; i < lstbxRegions.Items.Count; i++)
            {
                if (lstbxRegions.Items[i].Selected == true)
                {
                    strbldRegion.Append(lstbxRegions.Items[i].Value + ",");
                }
            }

            strbldRegion.Remove(strbldRegion.Length - 1, 1);
            strAIRSTMID_FK = ddlIssueRequestSubType.SelectedValue;
            lstbxBranch.DataSource = objBAL.GetNotMappedBranchByMultipleRegion(strbldRegion.ToString(), strAIRSTMID_FK);
            lstbxBranch.DataTextField = "BranchName";
            lstbxBranch.DataValueField = "BranchCode";
            lstbxBranch.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion

    #region Clear Controls Code

    private void ClearControls()
    {
        ccdApplication.SelectedValue = "Select Application";
        txtUserCode.Text = "";
        txtSecondLevelApprover.Text = "";
        ddlType.SelectedValue = "0";
        lstbxRegions.Items.Clear();
        lstbxBranch.Items.Clear();
        lstbxSelectedBranches.Items.Clear();
        lstbxZone.Items.Clear();
        BindZone();
    }

    #endregion
}
