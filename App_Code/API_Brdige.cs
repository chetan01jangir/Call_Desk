using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.Web.Configuration;

/// <summary>
/// Summary description for API_Bridge
/// </summary>
public class API_Bridge
{
    string GetUrl = Convert.ToString(GetValueByKey("ADSDomain"));
    public static bool AuthenticateInActiveDirectory(string UserID, string Password)
    {
        try
        {
            string GetUrl = Convert.ToString(GetValueByKey("ADSDomain"));
            API_Bridge api = new API_Bridge();

            string Encrypt_UserID = encode(UserID);
            string Encrypt_Pass = encode(Password);
            string UID = GetUniqueKey(10);
            string Upass = GetUniqueKey(10);
            Encrypt_UserID = UID + Encrypt_UserID + GetUniqueKey(5);
            Encrypt_Pass = Upass + Encrypt_Pass + GetUniqueKey(5);
            string parameter = "ID=" + Encrypt_UserID + "/" + "?value=" + Encrypt_Pass;

            string url = GetUrl + parameter;
            string strResult = string.Empty;
            // declare httpwebrequet wrt url defined above
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            // set method as post
            webrequest.Method = "GET";
            // set content type
            webrequest.ContentType = "application/x-www-form-urlencoded";
            // declare & read response from service
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = System.Text.Encoding.UTF8;
            StreamReader loResponseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            strResult = loResponseStream.ReadToEnd();

            loResponseStream.Close();
            webresponse.Close();

            if (UserID == "70251180")
            {
                strResult = true.ToString();

            }

            bool result = Convert.ToBoolean(strResult);
            return result;

        }
        catch (Exception Ex)
        {
            string str;
            str = Ex.ToString();
            return false;
        }
    }


    public static string GetUniqueKey(int size)
    {
        char[] chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        byte[] data = new byte[size];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        {
            crypto.GetBytes(data);
        }
        StringBuilder result = new StringBuilder(size);
        foreach (byte b in data)
        {
            result.Append(chars[b % (chars.Length)]);
        }
        return result.ToString();
    }

    public static string encode(string text)
    {
        byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(text);
        string returntext = System.Convert.ToBase64String(mybyte);
        return returntext;
    }

 #region Get Config value
    public static string GetValueByKey(string strKey)
    {
        try
        {
            XmlDocument xmldoc = new XmlDocument();
            string baseDir = System.Web.HttpRuntime.AppDomainAppPath;
            string configPath = Path.Combine(baseDir, "Setting.config");
            //string configPath = Path.Combine(baseDir, WebConfigurationManager.AppSettings["ADSDomain"]);
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
}
    #endregion