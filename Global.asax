<%@ Application Language="C#" %>

<script RunAt="server">    
    
    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_EndRequest(Object sender, EventArgs e)
    {
        //if (Response.Cookies["ASP.NET_SessionId"] != null)
        //{
        //    Response.Cookies["ASP.NET_SessionId"].Value = Request.Cookies["ASP.NET_SessionId"].Value + GenerateHashKey();
        //}
    }
    
    void Application_End(object sender, EventArgs e)
    {
        //if (Response.Cookies["ASP.NET_SessionId"] != null)
        //{
        //    Response.Cookies["ASP.NET_SessionId"].Value = Request.Cookies["ASP.NET_SessionId"].Value + GenerateHashKey();
        //}
    }

    void Application_Error(object sender, EventArgs e)
    {
        //Exception objErr = Server.GetLastError().GetBaseException();

        //System.IO.FileStream fs1 = new System.IO.FileStream(Server.MapPath("..\\Files") + "\\" + "CustomError.txt", System.IO.FileMode.Append, System.IO.FileAccess.Write);
        //System.IO.StreamWriter sw1 = new System.IO.StreamWriter(fs1);
        //sw1.Write("\r\n =======================================================================================");
        //sw1.Write("\r\n Log Entry On : " + System.DateTime.Now);
        //sw1.Write("\n " + objErr.Message);        
        //sw1.Close();
        //fs1.Close();

        //Response.Redirect("~/ErrorPages/ErrorPage.aspx");
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        string CookieHeaders = HttpContext.Current.Request.Headers["Cookie"];

        if ((null != CookieHeaders) && (CookieHeaders.IndexOf("ASP.NET_SessionId") >= 0))
        {
            // It is existing visitor, but ASP.NET session is expired
            Response.Redirect("~/Login.aspx", true);
        }
        else
        {
            // It is a new visitor, session was not created before
        }
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }
     
	protected void Application_BeginRequest(object sender, EventArgs e)
    {
        //HttpApplication application = sender as HttpApplication;
        //if (application != null && application.Context != null)
        //{
        //    application.Context.Response.Headers.Remove("Server");
        //}

        //if (Request.Cookies["ASP.NET_SessionId"] != null && Request.Cookies["ASP.NET_SessionId"].Value != null)
        //{
        //    //Response.Cookies["ASP.NET_SessionId"].HttpOnly = true;
        //    //Response.Cookies["ASP.NET_SessionId"].Secure = true;        
        //    string newSessionID = Request.Cookies["ASP.NET_SessionID"].Value;
        //    //Check the valid length of your Generated Session ID
        //    if (newSessionID.Length <= 24)
        //    {
        //        //Log the attack details here
        //        Response.Cookies["TriedTohack"].Value = "True";
        //        //throw new HttpException("Invalid Request1");
        //    }

        //    //Genrate Hash key for this User,Browser and machine and match with the Entered NewSessionID
        //    if (GenerateHashKey() != newSessionID.Substring(24))
        //    {
        //        //Log the attack details here
        //        Response.Cookies["TriedTohack"].Value = "True";
        //        //throw new HttpException("Invalid Request2");
        //    }

        //    //Use the default one so application will work as usual//ASP.NET_SessionId
        //    Request.Cookies["ASP.NET_SessionId"].Value = Request.Cookies["ASP.NET_SessionId"].Value.Substring(0, 24);
        //}
    }

    private string GenerateHashKey()
    {
        StringBuilder myStr = new StringBuilder();
        myStr.Append(Request.Browser.Browser);
        myStr.Append(Request.Browser.Platform);
        myStr.Append(Request.Browser.MajorVersion);
        myStr.Append(Request.Browser.MinorVersion);
        //myStr.Append(Request.LogonUserIdentity.User.Value);
        System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
        byte[] hashdata = sha.ComputeHash(Encoding.UTF8.GetBytes(myStr.ToString()));
        return Convert.ToBase64String(hashdata);
    }
    
    	 
</script>

