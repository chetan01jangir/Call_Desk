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

public partial class User_PopUpTicketDetails : System.Web.UI.Page
{
    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["TicketNo"] != null)
        {
            string strTicketNo = Convert.ToString(Request.QueryString["TicketNo"]);
            BindDetails(strTicketNo);
        }
    }

    #endregion

    #region Bind Ticket Details

    public void BindDetails(string strTicketNo)
    {
        try
        {

            TrackCallBAL objTCBAL = new TrackCallBAL();
            DataSet dsCallDetails = new DataSet();
            //string strUserName = Membership.GetUser().UserName;
            string strUserName = Convert.ToString(Session["AgentUserID"]);
            string strLoggedBranch = Convert.ToString(Session["LoggedBranch"]);
            dsCallDetails = objTCBAL.GetCallDetailsByTicketNumber(strTicketNo, strLoggedBranch, strUserName);
            if (dsCallDetails != null)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallType"])))
                {
                    if (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallType"]).ToLower() == "request")
                    {
                        trRequest.Visible = true;
                        trIssue.Visible = false;

                        if (string.IsNullOrEmpty(Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverID"])))
                        {
                            lblTicketNumber.Text = strTicketNo;
                            lblApproverName.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverName"]);
                            lblApproverEMail.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverMail"]);
                            trSApproverName.Visible = false;
                            trSApproverEmail.Visible = false;
                        }
                        else
                        {
                            trSApproverName.Visible = true;
                            trSApproverEmail.Visible = true;
                            lblTicketNumber.Text = strTicketNo;
                            lblApproverName.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverName"]);
                            lblApproverEMail.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverMail"]);
                            lblSecondApproverName.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverName"]);
                            lblSecondApproverEMail.Text = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverMail"]);
                        }
                    }
                    else
                    {
                        trRequest.Visible = false;
                        trIssue.Visible = true;
                        lblTicketNumberIssue.Text = strTicketNo;
                        string sApplication = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApplicationName"]);
                        lblMsgIssue.Text = "The call is allocated to " + sApplication + " Application Support Team for resolution.";

                    }
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;

        }

    }

    #endregion

    #region Close window
    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Write("<script language=javascript> window.close();</script>");
            Response.End();

        }
        catch (Exception ex)
        {
            throw ex;
        }
      
    }
    #endregion
}
