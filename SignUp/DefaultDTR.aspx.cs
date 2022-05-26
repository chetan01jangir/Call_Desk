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
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using IIBService;
public partial class _Default : System.Web.UI.Page
{    
    
    protected void Page_Load(object sender, EventArgs e)
    {        
        //TextBox1.Attributes.Add("onfocus", "WaterMark(this, event);");
        //TextBox1.Attributes.Add("onblur", "WaterMark(this, event);"); 
        TextBox1.ForeColor = Color.Gray;
    }

    public DataSet getData(string RegNo, string ChassisNo, string EngineNo, string policyNo, string insurerName)
    {
        XmlNode[] ds = null;
        DataSet d = null;
        try
        {
            IIBService.Service svc = new IIBService.Service();
            ds = svc.getResults(RegNo, ChassisNo, EngineNo, policyNo, insurerName);
            string abc = "";
            for (int i = 0; i < ds.Length; i++)
            {
                abc = abc + ds[i].OuterXml;
            }
            d = new DataSet();
            StringReader xr = new StringReader(abc);
            d.ReadXml(xr);

        }
        catch (Exception ex) { }
        return d;
    }

    protected Boolean checkNull(string str)
    {
        if (str != null && str.Length > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private DataTable GenerateTransposedTable(DataTable dt)
    {

        DataTable dtnew = new DataTable();
        dtnew.TableName = dt.TableName;
        //Convert all the rows to columns
        if (dt != null)
        {
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    //dtnew.Columns.Add(Convert.ToString(i));
                    dtnew.Columns.Add("Details");
                }
                else
                {
                    if(dt.TableName.ToString().ToUpper().Contains("CLAIMDETAILS"))
                    {
                        dtnew.Columns.Add("Claim Details " + Convert.ToString(i));                        
                    }
                    if (dt.TableName.ToString().ToUpper().Contains("POLICYDETAILS")) 
                    {
                        dtnew.Columns.Add("Policy Details ");
                    }
                }

            }
            DataRow dr;
            // Convert All the Columns to Rows
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                dr = dtnew.NewRow();
                dr[0] = dt.Columns[j].ToString();
                for (int k = 1; k <= dt.Rows.Count; k++)
                {
                    dr[k] = dt.Rows[k - 1][j];
                }
                dtnew.Rows.Add(dr);
            }
        }
        return dtnew;


    }

    protected void Button1_Click(object sender, ImageClickEventArgs e)
    {        
       
        string RegNo = TextBox1.Text;
        string ChassisNo = TextBox2.Text;
        string EngineNo = TextBox3.Text;
        string policyNo = TextBox4.Text;
        string insurerName = TextBox5.Text;
        string covernoteNo = TextBox6.Text;
        Label lblHeading = new Label();
        lblHeading.Font.Bold = true;
        lblHeading.Font.Size = 11;
        lblHeading.Font.Name = "Arial";
        lblHeading.BackColor = Color.LightGray;
        lblHeading.ForeColor = Color.Black;
        lblHeading.Width = Unit.Percentage(100);

        //lblHeading.Text = "CLAIM / POLICY DETAILS";
        //PHldr_ParamGrid.Controls.Add(lblHeading);        
        //Boolean bln = true;
        //if (checkNull(TextBox1.Text) || checkNull(TextBox1.Text))
        //{
        //    if (!(checkNull(ChassisNo) && checkNull(EngineNo)))
        //    {
        //        Label1.Text = "Engine and Chassis No should be used in combination";
        //        return;
        //    }
        //}
        
         //foreach (DataRow dr in dt1.Rows)
            //{                
        try
        {
            DataSet ds = getData(RegNo, ChassisNo, EngineNo, policyNo, insurerName);

            if (ds != null)
            {
                GridView gd = new GridView();
                gd.ID = "gdParams";
                gd.Width = Unit.Percentage(100);
                gd.CssClass = "rcd-Grid";
                gd.HeaderStyle.CssClass = "rcd-GridHead";
                gd.AlternatingRowStyle.CssClass = "rcd-GridAlt";

                DataTable dtCliam = null;
                DataSet dsCl = new DataSet();
                foreach (DataTable dt in ds.Tables)
                {

                    if (dt.TableName.ToString().ToUpper().Contains("CLAIMDETAILS") || dt.TableName.ToString().ToUpper().Contains("POLICYDETAILS1"))
                    {

                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (DataColumn column in dt.Columns)
                            {
                                string ColumnName = column.ColumnName;
                                if (dt.TableName.ToString().ToUpper().Contains("CLAIMDETAILS"))
                                {                                   

                                    if (ColumnName.ToUpper().Contains("REGNO"))
                                    {
                                        column.ColumnName = "Vechile RegNo";
                                    }
                                    if (ColumnName.ToUpper().Contains("CHASISNO"))
                                    {
                                        column.ColumnName = "CHASIS NO";
                                    }
                                    if (ColumnName.ToUpper().Contains("ENGINENO"))
                                    {
                                        column.ColumnName = "ENGINE NO";
                                    }
                                    if (ColumnName.ToUpper().Contains("VEHICLEMAKE"))
                                    {
                                        column.ColumnName = "VEHICLE MAKE";
                                    }
                                    if (ColumnName.ToUpper().Contains("VEHICLEMODEL"))
                                    {
                                        column.ColumnName = "VEHICLE MODEL";
                                    }
                                    if (ColumnName.ToUpper().Contains("INSURERNAME"))
                                    {
                                        column.ColumnName = "INSURER NAME";
                                    }
                                    if (ColumnName.ToUpper().Contains("TYPEOFCLAIM"))
                                    {
                                        column.ColumnName = "TYPE OF CLAIM";
                                    }
                                    if (ColumnName.ToUpper().Contains("DATEOFLOSS"))
                                    {
                                        column.ColumnName = "DATE OF LOSS";
                                    }
                                    if (ColumnName.ToUpper().Contains("CLAIMINTIMATIONDATE"))
                                    {
                                        column.ColumnName = "CLAIM INTIMATION DATE";
                                    }
                                    if (ColumnName.ToUpper().Contains("TOTALODCLAIMSPAID"))
                                    {
                                        column.ColumnName = "TOTAL OD CLAIMS PAID";
                                    }
                                    if (ColumnName.ToUpper().Contains("ODOPENCLAIMPROVISON"))
                                    {
                                        column.ColumnName = "OD OPEN CLAIM PROVISON";
                                    }
                                    if (ColumnName.ToUpper().Contains("ODCLOSECLAIMPROVISON"))
                                    {
                                        column.ColumnName = "OD CLOSE CLAIM PROVISON";
                                    }
                                    if (ColumnName.ToUpper().Contains("ODCLOSECLAIMPROVISON"))
                                    {
                                        column.ColumnName = "OD CLOSE CLAIM PROVISON";
                                    }
                                    if (ColumnName.ToUpper().Contains("WHETHERTOTALLOSSCLAIM"))
                                    {
                                        column.ColumnName = "WHETHER TOTAL LOSS CLAIM";
                                    }
                                    if (ColumnName.ToUpper().Contains("WHETHERTHEFTCLAIM"))
                                    {
                                        column.ColumnName = "WHETHER THEFT CLAIM";
                                    }
                                    if (ColumnName.ToUpper().Contains("TOTALTPCLAIMSPAID"))
                                    {
                                        column.ColumnName = "TOTAL TP CLAIMS PAID";
                                    }
                                    if (ColumnName.ToUpper().Contains("TPOPENCLAIMPROVISON"))
                                    {
                                        column.ColumnName = "TP OPEN CLAIM PROVISON";
                                    }
                                    if (ColumnName.ToUpper().Contains("TPCLOSECLAIMPROVISON"))
                                    {
                                        column.ColumnName = "TP CLOSE CLAIM PROVISON";
                                    }
                                }
                                else
                                {
                                    if (ColumnName.ToUpper().Contains("POLICYNO"))
                                    {
                                        column.ColumnName = "POLICY NO";
                                    }
                                    if (ColumnName.ToUpper().Contains("POLICYSTATUS"))
                                    {
                                        column.ColumnName = "POLICY STATUS";
                                    }
                                    if (ColumnName.ToUpper().Contains("ISTHISANANNUALPOLICY"))
                                    {
                                        column.ColumnName = "ANNUAL POLICY";
                                    }
                                    if (ColumnName.ToUpper().Contains("HAS90DAYSCROSSEDAFTEREXPIRYDATE"))
                                    {
                                        column.ColumnName = "90 DAYS CROSSED AFTER POLICY EXP DATE";
                                    }
                                }

                            }
                            if (dt.TableName.ToString().ToUpper().Contains("CLAIMDETAILS")){
								string dtloss = row["DATE OF LOSS"].ToString();
								DateTime enteredDate;
								if (dtloss != null && dtloss != "NA" && dtloss != "")
								{
									enteredDate = DateTime.Parse(dtloss);
									row["DATE OF LOSS"] = enteredDate.ToString("dd-MMM-yyyy");
								}

								string dtcl = row["CLAIM INTIMATION DATE"].ToString();
								if (dtcl != null && dtcl != "NA" && dtcl != "")
								{
									enteredDate = DateTime.Parse(dtcl);
									row["CLAIM INTIMATION DATE"] = enteredDate.ToString("dd-MMM-yyyy");
								}
                            }

                        }
                        dsCl.Tables.Add(dt.Copy());

                    }
                    else
                    {
                        if (dt.TableName.ToString().ToUpper().Contains("OUTPUT"))
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn column in dt.Columns)
                                {
                                    string ColumnName = column.ColumnName;
                                    if (ColumnName.ToUpper().Contains("MESSAGE"))
                                    {
                                        //Response.Write("<br/><br/>" + row["MESSAGE"].ToString());
                                        Label lblHeading1 = new Label();
                                        lblHeading1.Font.Bold = true;
                                        lblHeading1.Font.Size = 11;
                                        lblHeading1.Font.Name = "Arial";
                                        lblHeading1.BackColor = Color.AntiqueWhite;
                                        lblHeading1.ForeColor = Color.Black;
                                        lblHeading1.Width = Unit.Percentage(100);
                                        lblHeading1.Text = "CLAIM DETAILS";
                                        PHldr_ParamGrid.Controls.Add(lblHeading1);

                                        Label lblHeading11 = new Label();
                                        lblHeading11.Font.Bold = true;
                                        lblHeading11.Font.Size = 11;
                                        lblHeading11.Font.Name = "Arial";
                                        lblHeading11.BackColor = Color.AntiqueWhite;
                                        lblHeading11.ForeColor = Color.Red;
                                        lblHeading11.Width = Unit.Percentage(100);
                                        lblHeading11.BackColor = Color.White;
                                        lblHeading11.ForeColor = Color.Red;
                                        lblHeading11.Text = row["MESSAGE"].ToString();
                                        PHldr_ParamGrid.Controls.Add(lblHeading11);
                                    }
                                    if (ColumnName.ToUpper().Contains("POLICYDETAILS"))
                                    {
                                        //Response.Write("<br/><br/>" + row["MESSAGE"].ToString());
                                        Label lblHeading2 = new Label();
                                        lblHeading2.Font.Bold = true;
                                        lblHeading2.Font.Size = 11;
                                        lblHeading2.Font.Name = "Arial";
                                        lblHeading2.BackColor = Color.AntiqueWhite;
                                        lblHeading2.ForeColor = Color.Black;
                                        lblHeading2.Width = Unit.Percentage(100);
                                        lblHeading2.Text = "POLICY DETAILS";
                                        PHldr_ParamGrid.Controls.Add(lblHeading2);

                                        Label lblHeading22 = new Label();
                                        lblHeading22.Font.Bold = true;
                                        lblHeading22.Font.Size = 11;
                                        lblHeading22.Font.Name = "Arial";
                                        lblHeading22.BackColor = Color.AntiqueWhite;
                                        lblHeading22.ForeColor = Color.Red;
                                        lblHeading22.Width = Unit.Percentage(100);
                                        lblHeading22.BackColor = Color.White;
                                        lblHeading22.ForeColor = Color.Red;
                                        lblHeading22.Text = row["POLICYDETAILS"].ToString();
                                        PHldr_ParamGrid.Controls.Add(lblHeading22);
                                    }
                                }
                            }
                        }
                    }

                }
                int i = 0;
                foreach (DataTable dt in dsCl.Tables)
                {
                    if (i == 0)
                    {
                        dtCliam = dt.Copy();
                        i = i + 1;
                    }
                    else
                    {
                        dtCliam.Merge(dt, true);
                    }

                }

                if (dtCliam != null)
                {
                    dtCliam = GenerateTransposedTable(dtCliam);
                    gd.DataSource = dtCliam;
                    gd.DataBind();
                    if (dtCliam != null && dtCliam.TableName.ToString().ToUpper().Contains("CLAIMDETAILS"))
                    {
                        Label lblHeading3 = new Label();
                        lblHeading3.Font.Bold = true;
                        lblHeading3.Font.Size = 11;
                        lblHeading3.Font.Name = "Arial";
                        lblHeading3.BackColor = Color.AntiqueWhite;
                        lblHeading3.ForeColor = Color.Black;
                        lblHeading3.Width = Unit.Percentage(100);
                        lblHeading3.Text = "CLAIM DETAILS";
                        PHldr_ParamGrid.Controls.Add(lblHeading3);
                        PHldr_ParamGrid.Controls.Add(gd);
                    }
                    if (dtCliam != null && dtCliam.TableName.ToString().ToUpper().Contains("POLICYDETAILS1"))
                    {
                        Label lblHeading4 = new Label();
                        lblHeading4.Font.Bold = true;
                        lblHeading4.Font.Size = 11;
                        lblHeading4.Font.Name = "Arial";
                        lblHeading4.BackColor = Color.AntiqueWhite;
                        lblHeading4.ForeColor = Color.Black;
                        lblHeading4.Width = Unit.Percentage(100);
                        lblHeading4.Text = "POLICY DETAILS";
                        PHldr_ParamGrid.Controls.Add(lblHeading4);
                        PHldr_ParamGrid.Controls.Add(gd);
                    }
                }
            }
            else
            {
                Label lblIIB = new Label();
                lblIIB.Font.Bold = true;
                lblIIB.Font.Size = 11;
                lblIIB.Font.Name = "Arial";
                lblIIB.BackColor = Color.White;
                lblIIB.ForeColor = Color.Red;
                lblIIB.Width = Unit.Percentage(100);
                lblIIB.Text = "OOPS !!! Looks like IIB Service is Down.  Please try later...";
                PHldr_ParamGrid.Controls.Add(lblIIB);
            }
        }
        catch (Exception ex) 
        {
            Label lblErr = new Label();
            lblErr.Font.Bold = true;
            lblErr.Font.Size = 11;
            lblErr.Font.Name = "Arial";
            lblErr.BackColor = Color.White;
            lblErr.ForeColor = Color.Red;
            lblErr.Width = Unit.Percentage(100);
            lblErr.Text = "Error occured. Please contact Administrator (parshuram.juwekar@rcap.co.in)" + ex.Message;
            PHldr_ParamGrid.Controls.Add(lblErr);
        }
            //}        
    }
    
}
