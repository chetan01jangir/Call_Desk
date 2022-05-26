using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Net;
using System.ComponentModel;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection DBConnection;
    string url;
    string zipname;
    protected void Page_Load(object sender, EventArgs e)
    {
      
    theDiv.Visible = false;

    }

         protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                GridViewRow gr = (GridViewRow)btn.NamingContainer;
                string filePath = (gr.FindControl("hidFilePath") as HiddenField).Value;
                HttpContext.Current.Response.Redirect("DownLoadFile.ashx?file=" + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            DataSet ds = new DataSet();
                    DBConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CallDeskDB"].ConnectionString);
                    SqlCommand DBcommand = new SqlCommand("usp_copyfiles", DBConnection);
                    DBcommand.Parameters.AddWithValue("@TicketNumberPK", TextBox1.Text.Trim());
                    DBcommand.CommandType = CommandType.StoredProcedure;

                    DBConnection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(DBcommand);
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            url = ds.Tables[0].Rows[i]["UploadedScreen"].ToString();
                      
       
			if (url.Contains("\\"))
                        {
                            zipname = url.Substring(url.LastIndexOf("\\"));
                            zipname = zipname.Replace("\\", "");
				
			
                            if (File.Exists(url))
                            {


                                HttpContext.Current.Response.Redirect("DownLoadFile.ashx?file=" + url);


                            }

                            else
                            {
 
                               Response.Write("<script language=javascript>alert('No file Found for this Ticket Number1')</script>");


                            }
                        }
                        else
                        {
                            Response.Write("<script language=javascript>alert('No file Found for this Ticket Number2')</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('No file Found for this Ticket Number3')</script>");
                    }
                          
            //SqlConnection DBConnection;
            //DBConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CallDeskDB"].ConnectionString);
            //SqlCommand DBcommand = new SqlCommand("Insert_SMS_LOG", DBConnection);
            //DBcommand.CommandType = CommandType.StoredProcedure;
            //DBConnection.Open();



            
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fuUpLoadFile.HasFile)
            {

                string FileName = Path.GetFileName(fuUpLoadFile.PostedFile.FileName);

                string Extension = Path.GetExtension(fuUpLoadFile.PostedFile.FileName);

                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];



                string FilePath = Server.MapPath(FileName);

                fuUpLoadFile.SaveAs(FilePath);

                Import_To_Grid(FilePath, Extension);

            }
            else
            {
                Response.Write("<script language=javascript>alert('Please Select the File to Upload.')</script>");
            }
        }


        private void Import_To_Grid(string FilePath, string Extension)
        {
                DataTable dtr = new DataTable();
                dtr.Columns.Add("wrong_ticketno");
                string conStr = "";

            switch (Extension)
            {

                        case ".xls": //Excel 97-03

                                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                                .ConnectionString;
                        break;

                        case ".xlsx": //Excel 07

                                  conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                                  .ConnectionString;

                        break;

            }

                         conStr = String.Format(conStr, FilePath);
                         OleDbConnection connExcel = new OleDbConnection(conStr);
                         OleDbCommand cmdExcel = new OleDbCommand();
                         OleDbDataAdapter oda = new OleDbDataAdapter();
                         DataTable dt = new DataTable();
                         cmdExcel.Connection = connExcel;

                         connExcel.Open();

                         DataTable dtExcelSchema;

                         dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                         string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                         connExcel.Close();



                         //Read Data from First Sheet

                         connExcel.Open();

                         cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";

                         oda.SelectCommand = cmdExcel;

                         oda.Fill(dt);

                         connExcel.Close();


            foreach (DataRow st in dt.Rows)
            {

                var ticketno = st.ItemArray[0].ToString().Trim();




                if (ticketno.Count() == 13)
                {
                    DataSet ds = new DataSet();
                    DBConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CallDeskDB"].ConnectionString);
                    SqlCommand DBcommand = new SqlCommand("usp_copyfiles", DBConnection);
                    DBcommand.Parameters.AddWithValue("@TicketNumberPK", ticketno);
                    DBcommand.CommandType = CommandType.StoredProcedure;

                    DBConnection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(DBcommand);
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            url = ds.Tables[0].Rows[i]["UploadedScreen"].ToString();
                        string Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
                        if (!Directory.Exists(@"D:\Files\" + Todaysdate))
                            Directory.CreateDirectory(@"D:\Files\" + Todaysdate);
                        //using (
                        //    FileStream file =
                        //        new FileStream("C:\Files\" + Todaysdate + "/" + "Reminder" + (gvGeneratedLetters.Rows.Count + 1) + ".pdf",
                        //            FileMode.Create, FileAccess.Write))
                        //{
                        //    file.Write(bytes, 0, bytes.Length);
                        //} 

                        if (!Directory.Exists("C:\\Files\\" + Todaysdate))
                        {
                            Directory.CreateDirectory("C:\\Files\\" + Todaysdate);



                        }

                        string zipname = url.Substring(url.LastIndexOf("\\"));



                        if (File.Exists(@"C:\\Files\\" + Todaysdate + zipname))
                        {
                            zipname = zipname + 1;
                        }
                        else
                        {
                            System.IO.File.Copy(url, "C:\\Files\\" + Todaysdate + zipname);
                        }



                    }
                    else
                    {
                        //dtr.Rows.Add(st);
                        DataRow dr3 = dtr.NewRow();
                        dtr.ImportRow(st);
 
                    }


                }

                else
                {


                    DataRow dr3 = dtr.NewRow();
                    dtr.ImportRow(st);


                }



            }







            Response.Write("<script language=javascript>alert('Files Uploaded Sucessfully.')</script>");
        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            var coverFolderPath1 = Server.MapPath(@"~/Archieves");
            var directory = new DirectoryInfo(coverFolderPath1);

            // or...
            FileInfo myFile = directory.GetFiles()
                            .OrderByDescending(f => f.LastWriteTime)
                            .First();

            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + myFile.FullName + "\"");
            Response.TransmitFile(myFile.FullName);
            Response.End();
        }
}
