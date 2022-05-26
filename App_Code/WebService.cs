using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using CallDeskBAL;
using System.Collections.Generic;
using System.Data;
using System.Collections.Specialized;
using CallDeskDAL;
using System.Data.SqlClient;
using System.Configuration;
using CallDeskBO;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for WebService
/// </summary>
/// 

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class WebService : System.Web.Services.WebService
{
    
    #region Web Service

    public WebService()
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


    #region WebMethod Get Application Types by LoggedBranch

    [WebMethod(EnableSession = true)]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetApplicationTypesByBracnch()
    {
        try
        {
            RegisterNewCallBAL objRCBAL = new RegisterNewCallBAL();
            string strLoggedBranch = Convert.ToString(Session["LoggedBranch"]);
            string strAppType = Convert.ToString(Session["AppType"]);
            DataTable dtApplication = new DataTable();
            dtApplication = GetApplicationTypeByBranch(strLoggedBranch, strAppType);

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtApplication.Rows)
            {
                string categoryID = dRow["ApplicationID_PK"].ToString();
                string categoryName = dRow["ApplicationName"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName, categoryID));
            }
            return cascadingValues.ToArray();


        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    //[CR-06] IT-NonIT start
    #region WebMethod Get Application Types by LoggedBranch Group

    [WebMethod(EnableSession = true)]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetApplicationTypesByBracnchGroup()
    {
        try
        {
            RegisterNewCallBAL objRCBAL = new RegisterNewCallBAL();
            string strLoggedBranch = Convert.ToString(Session["LoggedBranch"]);
            string strAppType = Convert.ToString(Session["AppType"]);
            string strprocess = Convert.ToString(Session["ProcessGroup"]);
            DataTable dtApplication = new DataTable();
            dtApplication = GetApplicationTypeByBranchProcessing(strLoggedBranch, strAppType, strprocess);

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtApplication.Rows)
            {
                string categoryID = dRow["ApplicationID_PK"].ToString();
                string categoryName = dRow["ApplicationName"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName, categoryID));
            }
            return cascadingValues.ToArray();


        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region Get All Applications By Branch And Group

    public DataTable GetApplicationTypeByBranchProcessing(params object[] param)
    {
        DataSet dsApplicationType = new DataSet();
        DataTable dtApplicationType = new DataTable();

        try
        {
            dsApplicationType = DataUtils.ExecuteDataset("usp_GetApplicationTypeByBranch_Processing", param);
            dtApplicationType = dsApplicationType.Tables[0];

        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
        return dtApplicationType;
    }

    #endregion

    //[CR-06] IT-NonIT end

    #region WebMethod Get Application Types

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetApplicationTypes()
    {
        try
        {
            RegisterNewCallBAL objRCBAL = new RegisterNewCallBAL();
            DataTable dtApplication = new DataTable();

            dtApplication = objRCBAL.GetApplicationType();

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtApplication.Rows)
            {
                string categoryID = dRow["ApplicationID_PK"].ToString();
                string categoryName = dRow["ApplicationName"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName, categoryID));
            }
            return cascadingValues.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region WebMethod Get Type of Issue Request

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetTypeofIssueRequest(string knownCategoryValues, string category)
    {
        try
        {
            int categoryID;
            StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            categoryID = Convert.ToInt32(categoryValues["category"]);

            RegisterNewCallBAL objRCBAL = new RegisterNewCallBAL();
            DataTable dtIssueRequest = new DataTable();

            dtIssueRequest = objRCBAL.GetIssueRequestTypeByApplicationType(categoryID);

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtIssueRequest.Rows)
            {
                string categoryID11 = dRow["IssueRequestType_PK"].ToString();
                string categoryName11 = dRow["IssueRequestType"].ToString();
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

    #region WebMethod Get Type of Issue Request Sub Type

    [WebMethod(EnableSession = true)]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetTypeofIssueRequestSubType(string knownCategoryValues, string category)
    {
        try
        {
            int applicationID;
            int categoryID;
            int LocationTypeID;
            StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);

            applicationID = Convert.ToInt32(categoryValues["category"]);
            categoryID = Convert.ToInt32(categoryValues["IssueRequestType"]);
            //LocationTypeID = Convert.ToInt32(Session["LocationTypeID"].ToString());

            RegisterNewCallBAL objRCBAL = new RegisterNewCallBAL();

            DataTable dtIssueRequest = new DataTable();

            dtIssueRequest = objRCBAL.GetIssueRequestSubTypeByIssueRequestType(applicationID, categoryID);

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtIssueRequest.Rows)
            {
                string categoryID11 = dRow["RowID"].ToString();
                string categoryName11 = dRow["IssueRequestSubType"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName11, categoryID11));
            }
            return cascadingValues.ToArray();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region WebMethod Get Type of Issue Request Sub TypeRC

    [WebMethod(EnableSession = true)]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetTypeofIssueRequestSubTypeRC(string knownCategoryValues, string category)
    {
        try
        {
            int applicationID;
            int categoryID;
            int LocationTypeID;

            StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);

            applicationID = Convert.ToInt32(categoryValues["category"]);
            categoryID = Convert.ToInt32(categoryValues["IssueRequestType"]);
            LocationTypeID = Convert.ToInt32(Session["LocationTypeID"]);
            //LocationTypeID = Convert.ToInt32(Session["LocationTypeID"].ToString());

            RegisterNewCallBAL objRCBAL = new RegisterNewCallBAL();

            DataTable dtIssueRequest = new DataTable();

            dtIssueRequest = objRCBAL.GetIssueRequestSubTypeByIssueRequestTypeRC(applicationID, categoryID, LocationTypeID);

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtIssueRequest.Rows)
            {
                string categoryID11 = dRow["RowID"].ToString();
                string categoryName11 = dRow["IssueRequestSubType"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName11, categoryID11));
            }
            return cascadingValues.ToArray();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region WebMethod Get Type of Issue Request Sub Type For File Template

    [WebMethod(EnableSession = true)]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetTypeofIssueRequestSubTypeForFileTemplate(string knownCategoryValues, string category)
    {
        try
        {
            int applicationID;
            int categoryID;

            StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);

            applicationID = Convert.ToInt32(categoryValues["category"]);
            categoryID = Convert.ToInt32(categoryValues["IssueRequestType"]);

            RegisterNewCallBAL objRCBAL = new RegisterNewCallBAL();

            DataTable dtIssueRequest = new DataTable();

            dtIssueRequest = objRCBAL.GetTypeofIssueRequestSubTypeForFileTemplate(applicationID, categoryID);

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtIssueRequest.Rows)
            {
                string categoryID11 = dRow["RowID"].ToString();
                string categoryName11 = dRow["IssueRequestSubType"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName11, categoryID11));
            }
            return cascadingValues.ToArray();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region WebMethod Get Type of Issue Request Sub Type for Approver Mapping

    [WebMethod(EnableSession = true)]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetTypeofIssueRequestSubTypeForApproverMapping(string knownCategoryValues, string category)
    {
        try
        {
            int applicationID;
            int categoryID;

            StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);

            applicationID = Convert.ToInt32(categoryValues["category"]);
            categoryID = Convert.ToInt32(categoryValues["IssueRequestType"]);

            RegisterNewCallBAL objRCBAL = new RegisterNewCallBAL();

            DataTable dtIssueRequest = new DataTable();

            dtIssueRequest = objRCBAL.GetTypeofIssueRequestSubTypeForApproverMapping(applicationID, categoryID);

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtIssueRequest.Rows)
            {
                string categoryID11 = dRow["RowID"].ToString();
                string categoryName11 = dRow["IssueRequestSubType"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName11, categoryID11));
            }
            return cascadingValues.ToArray();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region WebMethod Get Type of Issue Request Sub Type for Approver Mapping for Manage

    [WebMethod(EnableSession = true)]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetTypeofIssueRequestSubTypeForApproverMappingForManage(string knownCategoryValues, string category)
    {
        try
        {
            int applicationID;
            int categoryID;

            StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);

            applicationID = Convert.ToInt32(categoryValues["category"]);
            categoryID = Convert.ToInt32(categoryValues["IssueRequestType"]);

            RegisterNewCallBAL objRCBAL = new RegisterNewCallBAL();

            DataTable dtIssueRequest = new DataTable();

            dtIssueRequest = objRCBAL.GetTypeofIssueRequestSubTypeForApproverMappingForManage(applicationID, categoryID);

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtIssueRequest.Rows)
            {
                string categoryID11 = dRow["RowID"].ToString();
                string categoryName11 = dRow["IssueRequestSubType"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName11, categoryID11));
            }
            return cascadingValues.ToArray();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region WebMethod Get Type of Issue Request Sub Type Where Description is Null

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetTypeofIssueRequestSubTypeNullDescription(string knownCategoryValues, string category)
    {
        try
        {
            int applicationID;
            int categoryID;
            StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);

            applicationID = Convert.ToInt32(categoryValues["category"]);
            categoryID = Convert.ToInt32(categoryValues["IssueRequestType"]);

            RegisterNewCallBAL objRCBAL = new RegisterNewCallBAL();

            DataTable dtIssueRequest = new DataTable();

            dtIssueRequest = objRCBAL.GetIssueRequestSubTypeByNullDescription(applicationID, categoryID);

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtIssueRequest.Rows)
            {
                string categoryID11 = dRow["RowID"].ToString();
                string categoryName11 = dRow["IssueRequestSubType"].ToString();
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

    #region WebMethod Get Zones

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetZones()
    {
        try
        {
            AdminBAL objAdminBAL = new AdminBAL();
            DataTable dtZones = new DataTable();

            dtZones = objAdminBAL.GetZones();

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtZones.Rows)
            {
                string categoryID = dRow["ZoneID_PK"].ToString();
                string categoryName = dRow["ZoneName"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName, categoryID));
            }
            return cascadingValues.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region WebMethod Get Regions By Zone

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetRegionsByZone(string knownCategoryValues, string category)
    {
        try
        {
            AdminBAL objAdminBAL = new AdminBAL();
            DataTable dtRegion = new DataTable();

            int categoryID;
            StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            categoryID = Convert.ToInt32(categoryValues["category"]);

            dtRegion = objAdminBAL.GetRegionByZone(categoryID);

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtRegion.Rows)
            {
                string categoryID1 = dRow["RegionID_PK"].ToString();
                string categoryName1 = dRow["RegionName"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName1, categoryID1));
            }
            return cascadingValues.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region WebMethod Get Branch By Region

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetBranchByRegion(string knownCategoryValues, string category)
    {
        try
        {
            AdminBAL objAdminBAL = new AdminBAL();
            DataTable dtRegion = new DataTable();

            int categoryID;
            StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            categoryID = Convert.ToInt32(categoryValues["category"]);

            dtRegion = objAdminBAL.GetBranchByRegion(categoryID);

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtRegion.Rows)
            {
                string categoryID1 = dRow["BranchCode"].ToString();
                string categoryName1 = dRow["BranchName"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName1, categoryID1));
            }
            return cascadingValues.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region WebMethod Get Employee Names

    [WebMethod]
    public string[] GetEmployeeNames(string prefixText)
    {
        try
        {
            EmployeeSearchBAL objESBAL = new EmployeeSearchBAL();
            DataTable dtEC = new DataTable();

            dtEC = objESBAL.GetEmployeeCodes(prefixText);

            string[] items = new string[dtEC.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dtEC.Rows)
            {
                items.SetValue(dr["UserName"].ToString(), i);
                i++;
            }
            return items;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    #endregion

    #region Get All Applications By Branch

    public DataTable GetApplicationTypeByBranch(params object[] param)
    {
        DataSet dsApplicationType = new DataSet();
        DataTable dtApplicationType = new DataTable();

        try
        {
            dsApplicationType = DataUtils.ExecuteDataset("usp_GetApplicationTypeByBranch", param);
            dtApplicationType = dsApplicationType.Tables[0];

        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
        return dtApplicationType;
    }

    #endregion

    //----------------------Added for resigning issue calls on 07-07-2016----------------------//

    #region WebMethod Get Type of Issue Request Sub TypeRC For Issue Only

    [WebMethod(EnableSession = true)]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GetTypeofIssueRequestSubTypeRCIssueOnly(string knownCategoryValues, string category)
    {
        try
        {
            int applicationID;
            int categoryID;
            int LocationTypeID;

            StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);

            applicationID = Convert.ToInt32(categoryValues["category"]);
            categoryID = Convert.ToInt32(categoryValues["IssueRequestType"]);
            LocationTypeID = Convert.ToInt32(Session["LocationTypeID"]);
            //LocationTypeID = Convert.ToInt32(Session["LocationTypeID"].ToString());

            RegisterNewCallBAL objRCBAL = new RegisterNewCallBAL();

            DataSet dsApplicationType = new DataSet();

            DataTable dtIssueRequest = new DataTable();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ApplicationID_FK", applicationID);
            param[1] = new SqlParameter("@IssueRequestID_FK", categoryID);
            param[2] = new SqlParameter("@LocationTypeID", LocationTypeID);

            //dtIssueRequest = objRCBAL.GetIssueRequestSubTypeByIssueRequestTypeRC(applicationID, categoryID, LocationTypeID);

            dsApplicationType = DataUtils.ExecuteDataset("usp_GetIssueRequestSubTypeByIssueRequestTypeRCIssueOnly", param);
            dtIssueRequest = dsApplicationType.Tables[0];

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            foreach (DataRow dRow in dtIssueRequest.Rows)
            {
                string categoryID11 = dRow["RowID"].ToString();
                string categoryName11 = dRow["IssueRequestSubType"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName11, categoryID11));
            }
            return cascadingValues.ToArray();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion


    //----------------------Added for resigning issue calls on 07-07-2016----------------------//


    //----------------------Added for Register call occupancy under fire on 07-07-2016----------------------//
    #region WebMethod Get Type of Issue Request

    [WebMethod]
    public AjaxControlToolkit.CascadingDropDownNameValue[] GETOccupancy_Under_Fire()
    {
        try
        {
            //  int categoryID;
            //  StringDictionary categoryValues = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            // categoryID = Convert.ToInt32(categoryValues["category"]);

            RegisterNewCallBAL objRCBAL = new RegisterNewCallBAL();
            DataTable dtIssueoccupency = new DataTable();

            dtIssueoccupency = objRCBAL.GETOccupencyunderfire();

            List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            //foreach (DataRow dRow in dtIssueRequest.Rows)
            //{
            //    string categoryID11 = dRow["IssueRequestType_PK"].ToString();
            //    string categoryName11 = dRow["IssueRequestType"].ToString();
            //    cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName11, categoryID11));
            //}


            //dtIssueoccupency.Columns.Add("IssueRequestType_PK", typeof(string));
            //dtIssueoccupency.Columns.Add("IssueRequestType", typeof(string));


            //DataRow dr = dtIssueoccupency.NewRow();
            //dr["IssueRequestType_PK"] = "22";
            //dr["IssueRequestType"] = "fire";
            //dtIssueoccupency.Rows.Add(dr);


            //dr = dtIssueoccupency.NewRow();
            //dr["IssueRequestType_PK"] = "23";
            //dr["IssueRequestType"] = "occupancy";
            //dtIssueoccupency.Rows.Add(dr);


            foreach (DataRow dRow in dtIssueoccupency.Rows)
            {
                string categoryID11 = dRow["ID"].ToString();
                string categoryName11 = dRow["Occupency_data"].ToString();
                cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName11, categoryID11));
            }


            // string categoryID11 = "11";
            // string categoryName11 = "Occupancy";
            // cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue(categoryName11, categoryID11));
            return cascadingValues.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    //----------------------Added for Register call occupancy under fire on 07-07-2016----------------------//

    #region WebMethod Get GetIMDCODE

    [WebMethod]
    public string GetIMDCODE(string IMDcode)
    {
        try
        {
            RegisterNewCallBAL objESBAL = new RegisterNewCallBAL();
            DataTable dtEC = new DataTable();
            string FLAG = "";

            dtEC = objESBAL.GETIMDCODE(IMDcode);

            if (dtEC.Rows.Count > 0)
            {
                FLAG = dtEC.Rows[0]["FLAG"].ToString();
            }

            return FLAG;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    #endregion

    public class Returnname
    {
        public string Reason { get; set; }
        public string Errorcode { get; set; }

    }
    public class Return
    {
        public string Reason { get; set; }
        public string Errorcode { get; set; }


        public List<Return> Currentvalue { get; set; }
    }

    //[CR-35] Calldesk Ticket details service -Start
    #region Get User Details By TicketNumber
    [WebMethod]
    public DataTable GetTicketDetailsByTicketNumber(string strTicketNo)
    {
        DataTable oDT = new DataTable("TicketDetails");
        SqlConnection oCon = new SqlConnection();
        SqlCommand oComm = new SqlCommand();
        IDataReader reader = null;
        try
        {
            string strConnection = ConfigurationManager.ConnectionStrings["CallDeskDB"].ConnectionString;
            oCon.ConnectionString = strConnection;
            oCon.Open();
            oComm.Connection = oCon;
            oComm.CommandType = CommandType.StoredProcedure;
            oComm.CommandText = "usp_GetTicketDetailsByTicketNo";
            oComm.Parameters.Add(new SqlParameter("@TicketNo", strTicketNo));
            reader = oComm.ExecuteReader();
            oDT.Load(reader);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }
            oCon.Close();
            oComm.Dispose();
        }
        return oDT;
    }
    #endregion
    //created by shilpa requirement of ICM
    #region Get User Details By TicketNumber
    [WebMethod]
    public DataTable GetTicketDetailsByTicketNumberICM(string strTicketNo, string category)
    {
        DataTable oDT = new DataTable("TicketDetails");
        SqlConnection oCon = new SqlConnection();
        SqlCommand oComm = new SqlCommand();
        IDataReader reader = null;
        try
        {
            string strConnection = ConfigurationManager.ConnectionStrings["CallDeskDB"].ConnectionString;
            oCon.ConnectionString = strConnection;
            oCon.Open();
            oComm.Connection = oCon;
            oComm.CommandType = CommandType.StoredProcedure;
            oComm.CommandText = "usp_GetTicketDetailsByTicketNoICM";
            oComm.Parameters.Add(new SqlParameter("@TicketNo", strTicketNo));
            oComm.Parameters.Add(new SqlParameter("@category", category));


            reader = oComm.ExecuteReader();
            oDT.Load(reader);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }
            oCon.Close();
            oComm.Dispose();
        }
        return oDT;
    }
    #endregion

    //created by shilpa on 20-01-2020
    //[CR-35] Calldesk Managerdetails service -End

    [WebMethod]
    //added by shilpa on 22-02-2021
    public Return RMS_generateMoMMappingReqeust(string SM_Code, string Agent_Code, string HNIN_Code, string IRC_Code, string Branch_Code, string Channel_Name, string requester, string RequesterContactNo)
    {



        RegisterNewCallBAL objRCBAL = new RegisterNewCallBAL();

        DataSet dsApplicationType = new DataSet();

        DataTable dtIssueRequest = new DataTable();



        string strReturnVal = "";
        int intTAT = 0;
        string sUserRemark, sApproverEmail, sApproverDesignation, sApproverName, sUserMail, sContactNumber, sApplication
               , sApproverID, sSApproverID, sSApproverEmail, sSApproverDesignation, sSApproverName, sReopenID, sBranchName, sCallDate, sUploadPath,
               sUserName, sCallType, sTypeofIR, sTicketNumber, sChannel, sIRSubType, sPriority, scRegionID, scZoneID, strChannelName = "",
               sGroups, sCallTAT, sAppSupportPerformer, sUserDesignation, sTicketValue, sServiceCenterName, sApproverTAT, hdnimdcode = "", sAppSupportTAT, strAgentCode = "", strIMDCode = "", hdnoccupancy = "", strInsurerName = "", strProposalNo = "", strTotalInsured = "";







        string strPortalIDLocked = string.Empty;
        string strPolicyNoRN = string.Empty;
        strPolicyNoRN = "NA";

        string ContactNumber = RequesterContactNo.ToString();
        string Issuerequestsubtype = "9725";


        RegisterNewCallBO objRNCBO = new RegisterNewCallBO();
        RegisterNewCallBAL objRNCBAL = new RegisterNewCallBAL();
        string strN = objRNCBO.TicketNumber = GetTicketNumberByBranchCode(Branch_Code.ToString());

        string remark = "SM : " + SM_Code + "$" + "Agent : " + Agent_Code + "$" + "HNIN : " + HNIN_Code + "$" + "IRC : " + IRC_Code + "$" + "Branch : " + Branch_Code + "$" + "Vertical : " + Channel_Name + "$" + "Employee : " + requester;
        strReturnVal = objRNCBAL.AddCallDetails(Issuerequestsubtype, requester, ContactNumber, remark, "", Branch_Code, objRNCBO.TicketNumber, objRNCBO.TicketValue, strPortalIDLocked, strPolicyNoRN, "0", Branch_Code/*[CR-06] start*/, 1/*[CR-06] end*/ /*[CR-07] start*/, Channel_Name/*[CR-07] end*//*[CR-34] start*/, ""/*[CR-34] end*//*[CR-1] start*/, "", "", hdnimdcode, strIMDCode, Agent_Code, hdnimdcode/*[CR-1] end*/);



        //TechDeskService objService = new TechDeskService();

        TechDeskService.TechDeskService objService = new TechDeskService.TechDeskService();
        TrackCallBAL objTCBAL = new TrackCallBAL();
        DataSet dsCallDetails = new DataSet();

        //[CR-10] IMD Group Creation Start
        //string strLoggedBranch = Convert.ToString(Session["LoggedBranch"]);
        string strLoggedBranch = Branch_Code;

        //[CR-10] IMD Group Creation End

        dsCallDetails = objTCBAL.GetCallDetailsByTicketNumber(strReturnVal, strLoggedBranch, requester);
        List<AjaxControlToolkit.CascadingDropDownNameValue> cascadingValues = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
        Return Baxter = new Return();


        if (dsCallDetails.Tables[0].Rows.Count == 0)
        {

            // cascadingValues.Add(new AjaxControlToolkit.CascadingDropDownNameValue( "Requester are not available in call desk","<Errorcode>1</Errorcode>"));
            ////.Add(new AjaxControlToolkit.CascadingDropDownNameValue( "<Errorcode>1</Errorcode>",""));
            // return cascadingValues.ToArray();

            //Baxter.Currentvalue = new List<Return>();
            Baxter.Reason = "Requester are not available in call desk";
            Baxter.Errorcode = "1";

        }
        else
        {

            //[CR-10] IMD Group Creation
            int AIRSMID_FK = Convert.ToInt32(dsCallDetails.Tables[0].Rows[0]["AIRSMID_FK"]);
            //[CR-10] IMD Group Creation

            string strCallCreatedBy = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallCreatedBy"]);
            sUserName = strCallCreatedBy;
            if (Convert.ToString(dsCallDetails.Tables[0].Rows[0]["BranchName"]) == "Agent Branch")
            {
                //string strAgentCode = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AgentCode"]);                
                sUserName = strCallCreatedBy;

            }

            sChannel = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SMChannel"]);
            sUserDesignation = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UserDesignation"]);
            sContactNumber = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ContactNumber"]);
            sUserMail = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UserMail"]);
            sUserRemark = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UserRemark"]);

            sBranchName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["BranchName"]);
            if (sBranchName == "Agent Branch")
            {
                sBranchName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SMBranchName"]);
            }
            //sCallDate = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallDate"]);
            DateTime dtCallDate = Convert.ToDateTime(dsCallDetails.Tables[0].Rows[0]["CallDate"]);
            sCallDate = dtCallDate.ToString("yyyy-MM-dd hh:mm:ss");

            sUploadPath = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["UploadedScreen"]);

            sTicketNumber = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["TicketNumberPK"]);

            sTicketValue = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["TicketValue"]);
            sReopenID = null;

            sCallType = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["CallType"]);
            sApplication = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApplicationName"]);
            sTypeofIR = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["IssueRequestType"]);
            sIRSubType = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["IssueRequestSubType"]);
            sPriority = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Priority"]);
            sGroups = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["Groups"]);
            sServiceCenterName = dsCallDetails.Tables[0].Rows[0]["ServiceCenterName"] == null ? "" : Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ServiceCenterName"]);
            scRegionID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["scRegionID"] == null ? "0" : Convert.ToString(dsCallDetails.Tables[0].Rows[0]["scRegionID"]));
            scZoneID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["scZoneID"] == null ? "0" : Convert.ToString(dsCallDetails.Tables[0].Rows[0]["scZoneID"]));
            sApproverTAT = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverTAT"]);
            sAppSupportTAT = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["AppSupportTAT"]);
            if (sGroups == "SCU" && sServiceCenterName != string.Empty)
            {
                //sGroups = sGroups + "$" + sServiceCenterName;
                sGroups = sServiceCenterName;
            }
            if (sGroups == "SCD" && sServiceCenterName != string.Empty)
            {
                //sGroups = sGroups + "$" + sServiceCenterName;
                sGroups = sGroups + "-" + sServiceCenterName;
            }

            if (sGroups == "SCUD")
            {
                sGroups = sGroups + scRegionID;
            }
            if (sGroups == "LegalClaim")
            {
                sGroups = sGroups + scRegionID;
            }
            if (sGroups == "ZAM")
            {
                sGroups = sGroups + scRegionID;
            }
            if (sGroups == "ROC")
            {
                sGroups = sGroups + scRegionID;
            }
            if (sGroups == "IT_infra")
            {
                sGroups = sGroups + scRegionID;
            }
            if (sGroups == "HUB")
            {
                sGroups = sGroups + scZoneID;
            }
            if (sGroups == "COP")
            {
                if (sServiceCenterName == "SC-East")
                {
                    sGroups = "COPE";
                }
                else if (sServiceCenterName == "SC-West")
                {
                    sGroups = "COPW";
                }
                else if (sServiceCenterName == "SC-North")
                {
                    sGroups = "COPN";
                }
                else
                {
                    sGroups = "COPS";
                }
            }
            if (sGroups == "GMC")
            {
                if (sServiceCenterName == "SC-East")
                {
                    sGroups = "GMC_East";
                }
                else if (sServiceCenterName == "SC-West")
                {
                    sGroups = "GMC_West";
                }
                else if (sServiceCenterName == "SC-North")
                {
                    sGroups = "GMC_North";
                }
                else if (sServiceCenterName == "SC-South")
                {
                    sGroups = "GMC_South";
                }
                else if (sServiceCenterName == "Corporate Zone")
                {
                    sGroups = "GMC_Corporate";
                }
            }

            //[CR-10] IMD Group Creation

            DataSet dsGroupAssign = new DataSet();
            dsGroupAssign = GetGroupAssign(AIRSMID_FK, strLoggedBranch, strCallCreatedBy);
            if (dsGroupAssign != null && dsGroupAssign.Tables[0] != null)
            {
                if (dsGroupAssign.Tables[0].Rows.Count >= 1)
                {
                    string GroupAssign = dsGroupAssign.Tables[0].Rows[0]["GroupAssign"].ToString();

                    if (!string.IsNullOrEmpty(GroupAssign))
                    {
                        sGroups = GroupAssign;
                    }
                }
            }

            //[CR-10] MD Group Creation

            intTAT = Convert.ToInt32(dsCallDetails.Tables[0].Rows[0]["CallTAT"]);
            sCallTAT = Convert.ToString(intTAT * 60 * 60);
            sAppSupportPerformer = null;

            sApproverID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverID"]);
            sApproverName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverName"]);
            sApproverEmail = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverMail"]);
            sApproverDesignation = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ApproverDesignation"]);

            sSApproverID = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverID"]);
            sSApproverName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverName"]);
            sSApproverEmail = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverMail"]);
            sSApproverDesignation = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["SApproverDesignation"]);


            string strResponseVal = objService.STARTCALLDESK(sUserRemark, "", "", "", sUserMail, sContactNumber, sApplication, "", "", sBranchName, sCallDate, "http://calldeskuat.reliancegeneral.co.in/CallDeskDocBase/T022321100012_som.zip", "RGI ADMIN (rgiadmin)", sCallType, sTypeofIR, sTicketNumber, sUserDesignation, sGroups, Issuerequestsubtype, sIRSubType, sCallTAT, "", "", "", "", "100", "rgicl", "rgicl", "", sPriority
                , strProposalNo

             , strTotalInsured, strInsurerName, HNIN_Code, strIMDCode, Agent_Code, Channel_Name
             , "", "", "", "", "", "", "", "", "", "", "", ""

            );

            objRNCBAL.UpdateSavvionResponse(strReturnVal, strResponseVal);
            // return objRNCBO.TicketNumber + "\r\n <Errorcode>0</Errorcode> ";
            //return Baxter;
            //Baxter.Currentvalue = new List<Return>();
            //Baxter.Currentvalue.Add(new Return() { Reason = objRNCBO.TicketNumber });
            //Baxter.Currentvalue.Add(new Return() { Errorcode = "1" });

            string strSMSText = string.Empty;
            string strContactNo = string.Empty;
            string strFirstName = string.Empty;
            strContactNo = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["ContactNumber"]);
            strFirstName = Convert.ToString(dsCallDetails.Tables[0].Rows[0]["First_Name"]);
            //strSMSText = GetSMStextCallLog(sTicketNumber, Session["EmployeeName"].ToString());
            strSMSText = GetSMStextCallLog(sTicketNumber, strFirstName, sApplication);


            // Added to call new SMS service 06-05-2016

            string App_Process = "Call Register";
            string SMS_Event = "Call Register";
            string Ref_Value = strReturnVal;
            string Ref_Name = "Ticket no";
            string Department = "1";

            // Added to call new SMS service 06-05-2016

            //[CR-21] Mobile No validation start
            if (!string.IsNullOrEmpty(strContactNo))
            {
                string expression = @"^[789]\d{9}$";

                Match match = Regex.Match(strContactNo, expression, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    Set_Message_InServer(strContactNo, strSMSText, "", App_Process, SMS_Event, Ref_Name, Ref_Value, Department);
                }
                else
                {

                }
            }
            //[CR-21] Mobile No validation End

            Baxter.Reason = objRNCBO.TicketNumber;
            Baxter.Errorcode = "0";

        }
        return Baxter;

    }

    public DataSet GetGroupAssign(int AIRSMID, string BranchCode, string UserId)
    {
        DataSet ds = null;
        SqlConnection con = null;
        string strCon = ConfigurationManager.ConnectionStrings["CallDeskDB"].ConnectionString;

        try
        {
            ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            con = new SqlConnection(strCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetGroupAssign";
            cmd.Parameters.AddWithValue("@AIRSMID", AIRSMID);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            con.Close();
        }
        catch (Exception)
        {

        }
        finally
        {
            try
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {




            }
        }
        return ds;
    }
    #region Get TicketNumber By BranchCode
    public string GetTicketNumberByBranchCode(params object[] param)
    {
        string ticketNumber = string.Empty;
        try
        {
            ticketNumber = DataUtils.ExecuteScalar("usp_GetTicketNumberByBranchCode", param).ToString();
        }
        catch (Exception ex)
        {
            //lblMessage.Text = ex.Message;
        }
        return ticketNumber;
    }
    #endregion


    public void Set_Message_InServer(string PhoneNumber, string MessageText, string SubmittedTime
     , string App_Process, string SMS_Event, string Ref_Name, string Ref_Value, string Department
     )
    {

        try
        {

            string strUnique = Ref_Value + "-" + Guid.NewGuid().ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-"));

            if (strUnique.Length >= 20)
            {
                strUnique = strUnique.Substring(0, 20);
            }



            SMSSendService.SingleMessage msgData = new SMSSendService.SingleMessage();
            SMSSendService.SendMessage sendMsg = new SMSSendService.SendMessage();

            //SingleMessage msg = new SingleMessage();
            msgData.Message = MessageText;            //Message to Send
            msgData.MobileNumber = PhoneNumber;                //Mobile Number - Should not be in DND list
            msgData.UserName = "intranet";
            msgData.Password = "rgiclintra07#";

            //[CR-253]-SMS Implementation mail - Provission for Event and Department Id-Start
            //msg.Department = "IT";
            msgData.App_Process = App_Process;
            msgData.SMS_Event = SMS_Event;
            msgData.Ref_Name = Ref_Name;
            msgData.Ref_Value = Ref_Value;
            msgData.Department = Department;
            msgData.Source_RequestID = strUnique;// Ref_Value;

            //[CR-253]-SMS Implementation mail - Provission for Event and Department Id-End


            SqlConnection DBConnection;

            string retVal = sendMsg.Send(msgData);

            DBConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CallDeskDB"].ConnectionString);
            SqlCommand DBcommand = new SqlCommand("Insert_SMS_LOG", DBConnection);
            DBcommand.CommandType = CommandType.StoredProcedure;
            DBConnection.Open();
            DBcommand.Parameters.AddWithValue("@System_RequestID", strUnique);
            DBcommand.Parameters.AddWithValue("@App_Process", App_Process);
            DBcommand.Parameters.AddWithValue("@SMS_Event", SMS_Event);
            DBcommand.Parameters.AddWithValue("@Ref_Name", Ref_Name);
            DBcommand.Parameters.AddWithValue("@Ref_Value", Ref_Value);
            DBcommand.Parameters.AddWithValue("@Department", Department);
            DBcommand.Parameters.AddWithValue("@Message", MessageText);
            DBcommand.Parameters.AddWithValue("@MobileNo", PhoneNumber);
            DBcommand.Parameters.AddWithValue("@SMS_TokenID", retVal);

            DBcommand.ExecuteNonQuery();


            //Code changes end
        }
        catch (Exception Ex)
        {
            string str;
            str = Ex.ToString();
            //lblMessage.Text = str;
        }
        finally
        {
            // DBConnection.Close();
        }
    }



    #region Get SMS text CallLog
    public string GetSMStextCallLog(string strTicketNo, string strEmpName, string application)
    {
        string strSMS = string.Empty;
        //strSMS = @"Dear <"+strEmpName.ToUpper()+">, you have successfully registered a call in Reliance Call desk. Your Ticket no is  <"+strTicketNo+"> will be taken care shortly."; 
        // strSMS = @"Dear <" + strEmpName.ToUpper() + ">, Ticket no<" + strTicketNo + "> for<" + application + "> is registered successfully with RGICL and will be taken care shortly."; 
        DataSet ds = new DataSet();
        ds = GetSMSTEXT(strTicketNo, strEmpName);
        if (ds.Tables[0].Rows.Count > 0)
        {

            strSMS = ds.Tables[0].Rows[0]["SMSTEXT"].ToString();

        }

        return strSMS;
    }
    #endregion
    #region Get SMSTEXT

    public DataSet GetSMSTEXT(params object[] param)
    {
        DataSet oDSParentBranch = new DataSet();
        try
        {
            oDSParentBranch = DataUtils.ExecuteDataset("USP_GetSMStextCallLog", param);
        }
        catch (Exception ex)
        {
            //lblMessage.Text = ex.Message;
        }
        return oDSParentBranch;
    }

    #endregion


    public void SendSMSToUser(string strMobileNo, string smsMsg)
    {
        try
        {
            SMSSendService.SingleMessage msgData = new SMSSendService.SingleMessage();
            SMSSendService.SendMessage sendMsg = new SMSSendService.SendMessage();
            msgData.Department = "IT";
            msgData.UserName = "intranet";
            msgData.Password = "rgiclintra07#";
            msgData.Message = smsMsg;
            msgData.MobileNumber = strMobileNo;
            sendMsg.Send(msgData);
        }
        catch
        {

        }
    }
    // #endregion

}

