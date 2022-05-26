using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Security;

/// <summary>
/// Summary description for AntiforgeryChecker
/// </summary>
public static class AntiforgeryChecker
{
    public static void Check(Page page, HiddenField antiforgery)
    {
        if (!page.IsPostBack)
        {
            Guid antiforgeryToken = Guid.NewGuid();
            page.Session["AntiforgeryToken"] = antiforgeryToken;
            antiforgery.Value = antiforgeryToken.ToString();
        }
        else
        {
            try
            {
                Guid stored = (Guid)page.Session["AntiforgeryToken"];            
                Guid sent = new Guid(antiforgery.Value);
                if (sent != stored)
                {
                    //throw new SecurityException("XSRF Attack Detected!");                    
                }
            }
            catch (Exception ex) 
            {
                //throw new SecurityException("XSRF Attack Detected!");
            }
        }
    }
}