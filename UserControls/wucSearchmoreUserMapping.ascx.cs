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

public partial class UserControls_wucSearchmoreUserMapping : System.Web.UI.UserControl
{
    [Flags]
    public enum enSearchType
    {
        Employee = 0,
        DEO = 1
    }

    private enSearchType _enSearchFor;
    
    public enSearchType SearchFor
    {
        get { return _enSearchFor; }
        set { _enSearchFor = value; }
    }

    private string _TextboxName = null;
    public string TextboxName
    {
        get { return _TextboxName; }
        set { _TextboxName = value; }
    }

    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ImageButton1.Attributes.Add("onclick", "javascript:return funOpenSearch('" + _TextboxName + "','" + _enSearchFor.ToString() + "')");
        }
    }
    #endregion    

}
