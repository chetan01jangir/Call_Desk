<%@ WebHandler Language="C#" CodeBehind="IHttpHandler.ashx.cs" Class="Handler" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        Stream stream = null;
        try
        {
            string FileLocation = HttpContext.Current.Request.QueryString["file"];
            stream = new FileStream(FileLocation, FileMode.Open, FileAccess.Read, FileShare.Read);
            long bytesToRead = stream.Length;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(FileLocation));
            while (bytesToRead > 0)
            {
                if (HttpContext.Current.Response.IsClientConnected)
                {
                    byte[] buffer = new Byte[10000];
                    int length = stream.Read(buffer, 0, 10000);
                    HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                    HttpContext.Current.Response.Flush();
                    //string Todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
                    //if (!Directory.Exists("D:\\Files\\" + Todaysdate))
                    //{
                    //    Directory.CreateDirectory("D:\\Files\\" + Todaysdate);



                    //}

                    //HttpContext.Current.Response.AppendHeader( "content-disposition", "attachment; filename=" + "" );
                    //HttpContext.Current.Response.ContentType = "application/zip";
                    //HttpContext.Current.Response.WriteFile("");
                    //HttpContext.Current.Response.TransmitFile("D:\\Files\\");
                    bytesToRead = bytesToRead - length;
                }
                else
                {
                    bytesToRead = -1;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}