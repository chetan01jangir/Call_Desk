using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using CallDeskBAL;
using System.Collections.Generic;
using System.Data;
using System.Collections.Specialized;


/// <summary>
/// Summary description for AdminServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class AdminServices : System.Web.Services.WebService
{
    #region Admin Services

    public AdminServices()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    #endregion

    #region WebMethod Hello World

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    #endregion

    #region WebMethod Get Zone

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetZone()
    {
        try
        {
            UserRoleBAL objBAL = new UserRoleBAL();
            DataTable dtZone = new DataTable();
            DataSet dsZone = new DataSet();

            dsZone = objBAL.GetZone();
            dtZone = dsZone.Tables[0];

            List<AjaxControlToolkit.CascadingDropDownNameValue> ZoneValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtZone.Rows)
            {
                string ZoneID_PK = dRow["ZoneID_PK"].ToString();
                string ZoneName = dRow["ZoneName"].ToString();

                ZoneValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(ZoneName, ZoneID_PK));
            }
            return ZoneValues.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region WebMethod Get Region

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetRegion(string knownCategoryValues, string category)
    {
        try
        {
            int categoryID;
            StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            categoryID = Convert.ToInt32(categoryValues["category"]);

            UserRoleBAL objBAL = new UserRoleBAL();
            DataSet dsRegion = new DataSet();

            DataTable dtIssueRequest = new DataTable();
            dsRegion = objBAL.GetRegion(categoryID);
            dtIssueRequest = dsRegion.Tables[0];
            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtIssueRequest.Rows)
            {
                string categoryID11 = dRow["RegionID_PK"].ToString();
                string categoryName11 = dRow["RegionName"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName11, categoryID11));
            }
            return cascadingValues.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region WebMethod Get Branch

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetBranch(string knownCategoryValues, string category)
    {
        try
        {
            int categoryID;
            StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            categoryID = Convert.ToInt32(categoryValues["Region"]);

            UserRoleBAL objBAL = new UserRoleBAL();
            DataSet dsBranch = new DataSet();

            DataTable dtBranch = new DataTable();
            dsBranch = objBAL.GetBranch(categoryID);
            dtBranch = dsBranch.Tables[0];
            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtBranch.Rows)
            {
                string categoryID11 = dRow["BranchCode"].ToString();
                string categoryName11 = dRow["BranchName"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName11, categoryID11));
            }
            return cascadingValues.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}

