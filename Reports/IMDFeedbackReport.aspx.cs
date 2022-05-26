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
using CallDeskBO;
using CallDeskBAL;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using CallDeskDAL;

public partial class Reports_IMDFeedbackReport : System.Web.UI.Page
{

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        string strUserName = string.Empty;
        strUserName = Convert.ToString(Session["UserName"]);
        if (!IsPostBack)
        {
            //GetApplication();
            if (strUserName == "70008042" || strUserName == "rgiadmin" || strUserName=="70251180")
            {
                pnlNoAutho.Visible = false;
                pnlAutho.Visible = true;
            }
            else
            {
                pnlNoAutho.Visible = true;
                pnlAutho.Visible = false;
            }
        }

    }

    #endregion 

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ReportsBAL objBAL = new ReportsBAL();
            DataSet dsFile = new DataSet();
            string stempfile;
            string strfromdate, strtodate;
            //strfromdate = Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy");
            //strtodate = Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy");
            strfromdate = txtFromDate.Text;
            strtodate = txtToDate.Text;
            dsFile = GetFeedback(strfromdate,strtodate, txtimduserid.Text);
            stempfile = Session.SessionID + ".xml";
            dsFile.WriteXml(Server.MapPath(stempfile));
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";
            XmlDataDocument xdd = new XmlDataDocument(dsFile);
            XslCompiledTransform xt = new XslCompiledTransform();
            xt.Load(Server.MapPath("IMDFeedback.xsl"));
            xt.Transform(xdd, null, Response.OutputStream);
            File.Delete(Server.MapPath(stempfile));
            Response.End();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Reports/IMDFeedbackReport.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    #region Get Report
    public DataSet GetFeedback(params object[] param)
    {
        DataSet oDSDescription = new DataSet();
        try
        {
            oDSDescription = DataUtils.ExecuteDataset("sp_IMDFeedbackReport", param);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        return oDSDescription;
    }
    #endregion
}
