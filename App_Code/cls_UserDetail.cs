using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.Configuration;


public class cls_UserDetail
{
    public bool AdAuthenticate(string UserID, string Password)
    {
        try
        {
            System.DirectoryServices.DirectoryEntry deADS = new System.DirectoryServices.DirectoryEntry("LDAP://" + ConfigurationManager.AppSettings["ADSDomain"].ToString(), UserID, Password);
            DirectorySearcher dsADS = new DirectorySearcher(deADS);

            SearchResult searchResult;

            searchResult = dsADS.FindOne();

            return true;

        }
        catch (Exception Ex)
        {
            string str;
            str = Ex.ToString();
            return false;
        }
    }

}


