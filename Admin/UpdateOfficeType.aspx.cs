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

public partial class Admin_UpdateOfficeType : System.Web.UI.Page
{

    #region Page Load Code

    protected void Page_Load(object sender, EventArgs e)
    {
        AntiforgeryChecker.Check(this, antiforgery);
        if (!IsPostBack)
        {
            hfOfficeTypeID.Value = Session["OfficeTypeID"].ToString();
            hfOfficeType.Value = Session["OfficeType"].ToString();
            txtOfficeType.Text = hfOfficeType.Value;
        }
    }

    #endregion

    #region Update Button Code

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            OfficeTypeBAL objBAL = new OfficeTypeBAL();
            int intOfficeTypeID = Convert.ToInt32(hfOfficeTypeID.Value);
            string strUpdatedBy, strOfficeType;
            strUpdatedBy = Membership.GetUser().UserName;
            strOfficeType = txtOfficeType.Text.Trim();

            if (strOfficeType == hfOfficeType.Value)
            {
                Response.Redirect("AddOfficeType.aspx");                
            }
            else
            {
                int intReturnVal = objBAL.UpdateOfficeType(intOfficeTypeID, strOfficeType, strUpdatedBy);

                if (intReturnVal == 1)
                {
                    Response.Redirect("AddOfficeType.aspx");
                }
                else 
                {
                    lblMessage.Text = "Office type alrady exists.";
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

    #region Cancel Button Code

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("AddOfficeType.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
			lblMessage.Text = "Error has occurred please contact the administrator.";
        }
    }

    #endregion
}
