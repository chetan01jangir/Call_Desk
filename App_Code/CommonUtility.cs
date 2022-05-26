using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Xml;
using System.IO;

/// <summary>
/// Summary description for CommonUtility
/// </summary>
public class CommonUtility
{
    public CommonUtility()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Add Select to Dropdown

    public static void AddSelectToDropDown(DropDownList dropDownList)
    {
        dropDownList.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    #endregion

    #region Add All to Dropdown

    public static void AddAllToDropDown(DropDownList dl)
    {
        dl.Items.Insert(0, new ListItem("All", "0"));
    }

    #endregion

    #region Convert Selected Date to MMDDYYYY format

    public static String ConvertDateToMMddyyyy(String strDateTime)
    {
        DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
        dtfi.ShortDatePattern = "dd/MM/yyyy";
        dtfi.DateSeparator = "/";
        if (strDateTime == "")
            strDateTime = "1/1/1900";

        DateTime objDate = Convert.ToDateTime(strDateTime, dtfi);
        return String.Format("{0:MM/dd/yyyy}", objDate).ToString();        
    }

    #endregion

    #region Get Value by Key from Setting.config file

    public static string GetValueByKey(string strKey)
    {
        try
        {
            XmlDocument xmldoc = new XmlDocument();
            string baseDir = System.Web.HttpRuntime.AppDomainAppPath;
            string configPath = Path.Combine(baseDir, "Setting.config");
            xmldoc.Load(configPath);
            XmlNode node = xmldoc.SelectSingleNode("appSettings/add[@key = '" + strKey + "']");
            XmlNode valueAttribute = node.Attributes.GetNamedItem("value");
            if (valueAttribute == null)
                return "";
            else
                return valueAttribute.InnerXml;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex); ;
        }
    }

    #endregion    

    #region Set Value by Key in Setting.config file
    
    public static void SetValueByKey(string strKey, string strValue)
    {
        try
        {
            XmlDocument xmldoc = new XmlDocument();
            string baseDir = System.Web.HttpRuntime.AppDomainAppPath;
            string configPath = Path.Combine(baseDir, "Setting.config");
            xmldoc.Load(configPath);
            XmlNode node = xmldoc.SelectSingleNode("appSettings/add[@key = '" + strKey + "']");
            XmlNode valueAttribute = node.Attributes.GetNamedItem("value");
            if (valueAttribute != null)
            {
                valueAttribute.InnerXml = strValue;
                xmldoc.Save(configPath);
            }

            System.Configuration.ConfigurationManager.AppSettings.Set(strKey, strValue);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex); ;
        }
    }
    #endregion
}
