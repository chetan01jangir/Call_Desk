using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Configuration;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.IO;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using HttpUtils;
using System.Web;
using CalldeskServices;
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CallDesk" in code, svc and config file together.
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
public class CallDesk : ICallDesk
{
    DataTable dt;
    DataSet ds;
    Cls_validation objValidation = new Cls_validation();

    #region Connection String
    private string CallDeskConnectionString = ConfigurationManager.ConnectionStrings["CallDeskDB123"].ToString();

    #endregion


    public string GetData(string value)
    {
        return string.Format("You entered: {0}", value);
    }

    //[WebInvoke(ResponseFormat = WebMessageFormat.Json)]
    public bool LoginAuthentication(UserDetail objUser)
    {
        try
        {
            bool IsUserADAuthenticate = false;

            cls_UserDetail objUserDetail = new cls_UserDetail();
            IsUserADAuthenticate = objUserDetail.AdAuthenticate(objUser.UserId, objUser.Password);


            if (IsUserADAuthenticate == true) //Comment for De-Activating Ad
            //if (true) //Uncomment for activating Ad
            {

                dt = new DataTable();

                SqlParameter[] objParam = new SqlParameter[1];

                objParam[0] = new SqlParameter("@UserId", SqlDbType.VarChar, 20);
                objParam[0].Value = objUser.UserId;
                objParam[0].Direction = ParameterDirection.Input;

                dt = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.usp_MobileServiceGetAuthenticateUser, objParam);

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        catch (Exception Ex)
        {
            string str;
            str = Ex.ToString();
            return false;
        }
    }

    [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetLastLoginUserDetail/{UserId}")]
    public List<UserDetail> GetLastLoginUserDetail(string UserId)
    {
        try
        {
            dt = new DataTable();

            List<UserDetail> lstLastLoginUserDetail = new List<UserDetail>();

            SqlParameter[] objParam = new SqlParameter[1];

            objParam[0] = new SqlParameter("@UserId", SqlDbType.VarChar, 20);
            objParam[0].Value = UserId;
            objParam[0].Direction = ParameterDirection.Input;


            dt = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.usp_MobileServiceGetLastLoginUserDetail, objParam);

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lstLastLoginUserDetail.Add(new UserDetail
                    {
                        UserId = dt.Rows[i]["UserName"].ToString(),
                        UserLastLogin = dt.Rows[i]["UserLastLogin"].ToString()

                    });
                }
            }
            else
            {
                lstLastLoginUserDetail = null;
            }

            return lstLastLoginUserDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet InsertTicketDetail(Ticket objTicket)
    {
        try
        {

            DataSet dsResult = new DataSet();

            DataTable dtValidation = new DataTable();

            dtValidation = objValidation.TicketCreation_CheckMandatoryFields(objTicket);
            dsResult.Tables.Add(dtValidation);
            dsResult.Tables[0].TableName = "Validation";


            DataTable dtTicket = new DataTable();

            if (dtValidation != null && dtValidation.Rows.Count > 0 && dtValidation.Rows[0]["ValidationText"].ToString() == "Data Validated")
            //if(true) //uncomment for insert without validation
            {
                //Generate Ticket No Start
                string strTicketNo = string.Empty;
                strTicketNo = objValidation.GenerateTicketNo();
                //Generate Ticke No End

                SqlParameter[] objParam = new SqlParameter[9];

                objParam[0] = new SqlParameter("@TicketNumber", SqlDbType.VarChar, 20);
                objParam[0].Value = strTicketNo;
                objParam[0].Direction = ParameterDirection.Input;


                objParam[1] = new SqlParameter("@ApplicationName", SqlDbType.VarChar, 50);
                objParam[1].Value = objTicket.ApplicationName;
                objParam[1].Direction = ParameterDirection.Input;

                objParam[2] = new SqlParameter("@IssueType", SqlDbType.VarChar, 500);
                objParam[2].Value = objTicket.IssueType;
                objParam[2].Direction = ParameterDirection.Input;

                objParam[3] = new SqlParameter("@IssueSubType", SqlDbType.VarChar, 500);
                objParam[3].Value = objTicket.IssueSubType;
                objParam[3].Direction = ParameterDirection.Input;

                objParam[4] = new SqlParameter("@CallCreatedBy", SqlDbType.VarChar, 50);
                objParam[4].Value = objTicket.CreatedBy;
                objParam[4].Direction = ParameterDirection.Input;

                objParam[5] = new SqlParameter("@ContactNumber", SqlDbType.VarChar, 20);
                objParam[5].Value = objTicket.ContactNumber;
                objParam[5].Direction = ParameterDirection.Input;

                objParam[6] = new SqlParameter("@UserRemark", SqlDbType.VarChar, 1000);
                objParam[6].Value = objTicket.UserRemark;
                objParam[6].Direction = ParameterDirection.Input;

                objParam[7] = new SqlParameter("@UploadedScreen", SqlDbType.VarChar, 1000);
                objParam[7].Value = objTicket.UploadScreenPath;
                objParam[7].Direction = ParameterDirection.Input;

                objParam[8] = new SqlParameter("@TicketValue", SqlDbType.VarChar, 1000);
                objParam[8].Value = objTicket.Ticketvalue;
                objParam[8].Direction = ParameterDirection.Input;

                dtTicket = SqlHelper.ExecuteDatatable(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.usp_MobileService_AddCallDetails, objParam);

                string strReturnValue = dtTicket.Rows[0]["ReturnValue"].ToString();
                //Savvion Service Start
                if (strReturnValue.Length == 13)
                {
                    objValidation.SavvionService(strTicketNo);
                }
                //Savvion Service End

                dsResult.Tables.Add(dtTicket);
                dsResult.Tables[1].TableName = "ResponseResult";

            }
            return dsResult;

        }
        catch (Exception)
        {
            return null;
        }
    }

    [WebGet(ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMyRaiseTicketByTicketNo/{strTicketNo}")]
    public DataSet GetMyRaiseTicketByTicketNo(string strTicketNo)
    {
        DataSet dsTicketDetails = new DataSet();

        try
        {
            SqlParameter[] objParam = new SqlParameter[1];

            objParam[0] = new SqlParameter("@TicketNumberPK", SqlDbType.VarChar, 20);
            objParam[0].Value = strTicketNo;
            objParam[0].Direction = ParameterDirection.Input;


            dsTicketDetails = SqlHelper.ExecuteDataset(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.usp_MobileService_GetMyRaiseTicketByTicketNo, objParam);

            return dsTicketDetails;
        }
        catch (Exception)
        {

            return null;
        }
    }

    [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetInbox/{UserId}")]
    public DataSet GetInbox(string UserId)
    {
        DataSet dsTicketDetails = new DataSet();

        try
        {
            SqlParameter[] objParam = new SqlParameter[1];

            objParam[0] = new SqlParameter("@UserId", SqlDbType.VarChar, 20);
            objParam[0].Value = UserId;
            objParam[0].Direction = ParameterDirection.Input;


            dsTicketDetails = SqlHelper.ExecuteDataset(CallDeskConnectionString, CommandType.StoredProcedure, AppConstants.Usp_MobileService_GetInbox, objParam);

            return dsTicketDetails;
        }
        catch (Exception)
        {

            return null;
        }
    }

    public Stream RetrieveFile(string path)
    {
        if (WebOperationContext.Current == null) throw new Exception("WebOperationContext not set");

        // As the current service is being used by a windows client, there is no browser interactivity.
        // In case you are using the code Web, please use the appropriate content type.
        var fileName = Path.GetFileName(path);
        WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
        WebOperationContext.Current.OutgoingResponse.Headers.Add("content-disposition", "inline; filename=" + fileName);

        return File.OpenRead(path);
    }

    public void UploadFile(string path, Stream stream)
    {
        CreateDirectoryIfNotExists(path);
        using (var file = File.Create(path))
        {
            stream.CopyTo(file);
        }
    }

    private void CreateDirectoryIfNotExists(string filePath)
    {
        var directory = new FileInfo(filePath).Directory;
        if (directory == null) throw new Exception("Directory could not be determined for the filePath");

        Directory.CreateDirectory(directory.FullName);
    }

    public Stream GetInboxTask(string UserId)
    {
        var currentMethodName = MethodBase.GetCurrentMethod().Name;
        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized("xyz");
        try
        {
            var result = GetLastLoginUserDetail(UserId);

            return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest }, currentMethodName);

        }
        catch (Exception)
        {

            throw;
        }

    }

    public Stream GetApplicationList(string ApplicationName, string IssueType, string UserId, string UserType)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized("XYZ");

        try
        {
            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
                return CallDeskServiceResponse.ServiceJsonResponse(JsonHelper.JsonDeserialize<CallDeskServiceResponse>(oCallDeskRequest.StatusMessage), currentMethodName);

            UserDetail objApplicationDetail = new UserDetail
            {
                ApplicationName = ApplicationName,
                IssueType = IssueType,
                UserId = UserId,
                UserType = UserType

            };

            var oTicket = new cls_Ticket();

            var result = oTicket.GetApplication(objApplicationDetail);
            return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest }, currentMethodName);

        }
        catch (Exception)
        {

            throw;
        }

    }

    public string CreatePerson(Person createPerson)
    {
        //var objReq = CalldeskServices.IsRequestAuthorized();
        //createPerson.ID = (++personCount).ToString();
        //persons.Add(createPerson);
        return "Created";
    }

    public Stream GetLastLogin(string UserId)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized("XYZ");
        oCallDeskRequest.RequestStartTime = DateTime.Now.ToString();

        try
        {
            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {
                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }

            if (string.IsNullOrEmpty(Convert.ToString(UserId)))
            {

                oCallDeskRequest.StatusMessage = MessageConstants.UserIdBlank;
                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }


            var oTicket = new cls_Ticket();
            var result = oTicket.GetLastLoginUserDetail(UserId);
            return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest }, currentMethodName);

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    #region Call Desk Restful Services

    public Stream CreateTicket(Ticket objTicket)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, "");
        //Log End

        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objTicket.AuthKey);


        try
        {
            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);

                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }

            var oTicket = new cls_Ticket();
            var result = oTicket.InsertTicketDetail(objTicket);

            if (result[0].ErrorStatus == true)
            {
                oCallDeskRequest.StatusMessage = result[0].ErrorMessage;

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }
            else
            {
                oCallDeskRequest.StatusMessage = "Success";

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);

            }

        }
        catch (Exception ex)
        {

            string strUserId = objTicket.CreatedBy;
            string strInputs = InputsInfo.GetTicketObjInputs(objTicket);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            oCallDeskRequest.StatusMessage = ex.Message;
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
        }
    }

    public Stream GetApplication(UserDetail objUserDetail)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, "");
        //Log End

        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objUserDetail.AuthKey);
        oCallDeskRequest.RequestStartTime = DateTime.Now.ToString();

        try
        {
            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {
                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

            }

            else
            {

                var oTicket = new cls_Ticket();

                var result = oTicket.GetApplication(objUserDetail);
                oCallDeskRequest.StatusMessage = "Success";

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
            }
        }
        catch (Exception ex)
        {

            string strUserId = objUserDetail.UserId;
            string strInputs = InputsInfo.GetUserObjectInputs(objUserDetail);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            oCallDeskRequest.StatusMessage = ex.Message;
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
        }
    }

    public Stream GetApprovalInbox(UserDetail objUserDetail)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, "");
        //Log End

        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objUserDetail.AuthKey);
        oCallDeskRequest.RequestStartTime = DateTime.Now.ToString();

        try
        {
            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

            }

            if (string.IsNullOrEmpty(Convert.ToString(objUserDetail.UserId)))
            {

                oCallDeskRequest.StatusMessage = MessageConstants.UserIdBlank;

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }

            var oTicket = new cls_Ticket();
            var result = oTicket.GetApprovalInbox(objUserDetail);

            if (result[0].ErrorStatus == true)
            {
                oCallDeskRequest.StatusMessage = result[0].ErrorMessage;

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

            }
            else
            {
                oCallDeskRequest.StatusMessage = "Success";

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
            }
        }
        catch (Exception ex)
        {

            string strUserId = objUserDetail.UserId;
            string strInputs = InputsInfo.GetUserObjectInputs(objUserDetail);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            oCallDeskRequest.StatusMessage = ex.Message;
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
        }
    }

    //public Stream UpdateTicket(Ticket objTicket)
    //{

    //    var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

    //    //Log Start
    //    ExceptionLogManagement objException = new ExceptionLogManagement();
    //    objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, "");
    //    //Log End

    //    var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objTicket.AuthKey);
    //    oCallDeskRequest.RequestStartTime = DateTime.Now.ToString();
    //    try
    //    {

    //        if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
    //        {

    //            //Output Log Start
    //            var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

    //            string FinalResult = null;
    //            using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
    //            {
    //                FinalResult = reader.ReadToEnd();
    //            }

    //            objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
    //            //Output Log End

    //            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
    //        }

    //        var oTicket = new cls_Ticket();
    //        var result = oTicket.UpdateTicket(objTicket);

    //        if (result[0].ErrorStatus == true)
    //        {
    //            oCallDeskRequest.StatusMessage = result[0].ErrorMessage;

    //            //Output Log Start
    //            var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

    //            string FinalResult = null;
    //            using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
    //            {
    //                FinalResult = reader.ReadToEnd();
    //            }

    //            objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
    //            //Output Log End

    //            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

    //        }
    //        else
    //        {
    //            //Output Log Start
    //            var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
    //            string FinalResult = null;
    //            using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
    //            {
    //                FinalResult = reader.ReadToEnd();
    //            }

    //            objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
    //            //Output Log End

    //            return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //        string strUserId = objTicket.CreatedBy;
    //        string strInputs = InputsInfo.GetTicketObjInputs(objTicket);
    //        int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
    //        oCallDeskRequest.StatusMessage = ex.Message;
    //        return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
    //    }
    //}



    public Stream UpdateTicket(Ticket objTicket)
    {

        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, "");
        //Log End

        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objTicket.AuthKey);
        oCallDeskRequest.RequestStartTime = DateTime.Now.ToString();
        try
        {

            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Not Valid ", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Not Valid", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }

            var oTicket = new cls_Ticket();
            var result = oTicket.UpdateTicket(objTicket);

            if (result[0].ErrorStatus == true)
            {
                oCallDeskRequest.StatusMessage = result[0].ErrorMessage;
                string st = result[0].ErrorMessage;
                string result1 = result[0].TicketMessage;
                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = st, StatusCode = 9999, Result = result1, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Timeout error", StatusCode = 9999, Result = result1, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

            }
            else
            {
                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
            }

        }
        catch (Exception ex)
        {

            string strUserId = objTicket.CreatedBy;
            string strInputs = InputsInfo.GetTicketObjInputs(objTicket);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            oCallDeskRequest.StatusMessage = ex.Message;
            string st = ex.Message;
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = st, StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
        }
    }


    public Stream GetTicketDetails(Ticket objTicket)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, "");
        //Log End

        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objTicket.AuthKey);
        oCallDeskRequest.RequestStartTime = DateTime.Now.ToString();
        try
        {

            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }

            if (!string.IsNullOrEmpty(Convert.ToString(objTicket.TicketNO)))
            {

                var oTicket = new cls_Ticket();
                var result = oTicket.GetTicketDetails(Convert.ToString(objTicket.TicketNO));

                oCallDeskRequest.StatusMessage = "Success";

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
            }
            else
            {
                oCallDeskRequest.StatusMessage = MessageConstants.TicketNoBlank;

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }

        }
        catch (Exception ex)
        {
            string strUserId = objTicket.CreatedBy;
            string strInputs = InputsInfo.GetTicketObjInputs(objTicket);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            oCallDeskRequest.StatusMessage = ex.Message;
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
        }
    }

    public Stream GetMyTickets(UserDetail objUserDetail)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, "");
        //Log End


        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objUserDetail.AuthKey);
        oCallDeskRequest.RequestStartTime = DateTime.Now.ToString();

        try
        {

            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

            }

            if (!string.IsNullOrEmpty(Convert.ToString(objUserDetail.UserId)))
            {
                var oTicket = new cls_Ticket();
                var result = oTicket.GetMyTickets(Convert.ToString(objUserDetail.UserId));

                oCallDeskRequest.StatusMessage = "Success";

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
            }
            else
            {
                oCallDeskRequest.StatusMessage = MessageConstants.UserIdBlank;

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }
        }
        catch (Exception ex)
        {
            string strUserId = objUserDetail.UserId;
            string strInputs = InputsInfo.GetUserObjectInputs(objUserDetail);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
        }
    }

    #endregion

    public Stream DownloadFile(Ticket objTicket)
    {

        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, "");
        //Log End


        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objTicket.AuthKey);
        oCallDeskRequest.RequestStartTime = DateTime.Now.ToString();


        try
        {

            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {
                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }

            if (!string.IsNullOrEmpty(Convert.ToString(objTicket.UploadScreenPath)))
            {
                var oTicket = new cls_Ticket();
                var result = oTicket.DownloadFile(objTicket);

                if (result[0].ErrorStatus == false)
                {
                    oCallDeskRequest.StatusMessage = "Success";

                    //Output Log Start
                    var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);

                    string FinalResult = null;
                    using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                    {
                        FinalResult = reader.ReadToEnd();
                    }

                    objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                    //Output Log End

                    return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
                }

                else
                {
                    oCallDeskRequest.StatusMessage = MessageConstants.DownloadFileNotAvailable;

                    //Output Log Start
                    var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
                    string FinalResult = null;
                    using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                    {
                        FinalResult = reader.ReadToEnd();
                    }

                    objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                    //Output Log End

                    return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
                }

            }
            else
            {
                oCallDeskRequest.StatusMessage = MessageConstants.FilePathBlank;

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }
        }
        catch (Exception ex)
        {
            string strUserId = objTicket.CreatedBy;
            string strInputs = InputsInfo.GetTicketObjInputs(objTicket);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            oCallDeskRequest.StatusMessage = ex.Message;
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
        }
    }

    public Stream GetFileTemplate(Ticket objTicket)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, "");
        //Log End


        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objTicket.AuthKey);
        oCallDeskRequest.RequestStartTime = DateTime.Now.ToString();


        try
        {

            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {
                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }

            if (!string.IsNullOrEmpty(Convert.ToString(objTicket.ApplicationId)) || !string.IsNullOrEmpty(Convert.ToString(objTicket.IssueTypeId)) || !string.IsNullOrEmpty(Convert.ToString(objTicket.IssueSubTypeId)))
            {
                var oTicket = new cls_Ticket();
                var result = oTicket.GetFileTemplate(objTicket);

                oCallDeskRequest.StatusMessage = "Success";

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
            }
            else
            {
                oCallDeskRequest.StatusMessage = MessageConstants.ApplicationDetailBlank;

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

            }
        }
        catch (Exception ex)
        {
            string strUserId = objTicket.CreatedBy;
            string strInputs = InputsInfo.GetTicketObjInputs(objTicket);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            oCallDeskRequest.StatusMessage = ex.Message;
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
        }
    }

    public Stream GetApplicationMaster(UserDetail objUserDetail)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, "");
        //Log End


        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objUserDetail.AuthKey);
        oCallDeskRequest.RequestStartTime = DateTime.Now.ToString();

        try
        {

            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }

            else
            {

                var oTicket = new cls_Ticket();
                var result = oTicket.GetApplicationMaster();


                oCallDeskRequest.StatusMessage = "Success";

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
            }
        }
        catch (Exception ex)
        {
            string strUserId = objUserDetail.UserId;
            string strInputs = InputsInfo.GetUserObjectInputs(objUserDetail);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            oCallDeskRequest.StatusMessage = ex.Message;
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
        }
    }


    public Stream GetIssueTypeMaster(UserDetail objUserDetail)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, "");
        //Log End


        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objUserDetail.AuthKey);
        oCallDeskRequest.RequestStartTime = DateTime.Now.ToString();

        try
        {

            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

            }

            else
            {

                var oTicket = new cls_Ticket();
                var result = oTicket.GetIssueTypeMaster();

                oCallDeskRequest.StatusMessage = "Success";

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
            }
        }
        catch (Exception ex)
        {
            string strUserId = objUserDetail.UserId;
            string strInputs = InputsInfo.GetUserObjectInputs(objUserDetail);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            oCallDeskRequest.StatusMessage = ex.Message;
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
        }
    }


    public Stream GetIssueSubTypeMaster(UserDetail objUserDetail)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, "");
        //Log End


        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objUserDetail.AuthKey);
        oCallDeskRequest.RequestStartTime = DateTime.Now.ToString();

        try
        {

            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {
                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }

            else
            {

                var oTicket = new cls_Ticket();
                var result = oTicket.GetIssueSubTypeMaster();

                oCallDeskRequest.StatusMessage = "Success";

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
            }
        }
        catch (Exception ex)
        {
            string strUserId = objUserDetail.UserId;
            string strInputs = InputsInfo.GetUserObjectInputs(objUserDetail);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            oCallDeskRequest.StatusMessage = ex.Message;
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
        }
    }


    public Stream GetAppSuportCategory(UserDetail objUserDetail)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, "");
        //Log End


        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objUserDetail.AuthKey);
        oCallDeskRequest.RequestStartTime = DateTime.Now.ToString();

        try
        {

            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

            }

            else
            {

                var oTicket = new cls_Ticket();
                var result = oTicket.GetAppSupportCategory();


                oCallDeskRequest.StatusMessage = "Success";

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_UserDetail(objUserDetail, currentMethodName, objUserDetail.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
            }
        }
        catch (Exception ex)
        {
            string strUserId = objUserDetail.UserId;
            string strInputs = InputsInfo.GetUserObjectInputs(objUserDetail);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            oCallDeskRequest.StatusMessage = ex.Message;
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
        }
    }


    public Stream CallDeskReport(CalldeskReport objCalldeskReport)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_CallDeskReport(objCalldeskReport, currentMethodName, objCalldeskReport.UserId, "");
        //Log End

        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objCalldeskReport.AuthKey);
        oCallDeskRequest.RequestStartTime = DateTime.Now.ToString();
        try
        {

            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_CallDeskReport(objCalldeskReport, currentMethodName, objCalldeskReport.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

            }

            if (!string.IsNullOrEmpty(Convert.ToString(objCalldeskReport.StartDate)) && !string.IsNullOrEmpty(Convert.ToString(objCalldeskReport.EndDate)))
            {

                var oTicket = new cls_Ticket();
                var result = oTicket.GetCalldeskReport(objCalldeskReport);

                oCallDeskRequest.StatusMessage = "Success";

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_CallDeskReport(objCalldeskReport, currentMethodName, objCalldeskReport.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);

            }
            else
            {
                oCallDeskRequest.StatusMessage = MessageConstants.DateBlank;

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_CallDeskReport(objCalldeskReport, currentMethodName, objCalldeskReport.UserId, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }

        }
        catch (Exception ex)
        {
            string strUserId = objCalldeskReport.UserId;
            string strInputs = InputsInfo.GetCalldeskReportObjectInputs(objCalldeskReport);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            oCallDeskRequest.StatusMessage = ex.Message;
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

        }
    }

    public Stream CreateTicketNew(Stream stream)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        HttpMultipartParser parser = new HttpMultipartParser(stream, "FileContents");

        string objTicket1 = null;
        string param1 = null;
        Ticket objTicket = new Ticket();

        if (parser.Success)
        {
            objTicket1 = parser.Parameters["ticket"];
            objTicket1 = objTicket1.Substring(objTicket1.IndexOf("\r\n\r\n") + 4);

            if (parser.Parameters.Count > 1)
            {

                param1 = HttpUtility.UrlDecode(parser.Parameters["filecontents"]);
                param1 = param1.Substring(param1.IndexOf("\r\n\r\n") + 4);
            }
            //sfile = parser.Parameters["filebytesasstring"];
        }

        JavaScriptSerializer j = new JavaScriptSerializer();
        objTicket = j.Deserialize<Ticket>(objTicket1);

        if (param1 != null)
        {
            objTicket.FileBytes = Convert.FromBase64String(param1.Replace(' ', '+'));
        }

        //objTicket.FileBytes = Convert.FromBase64String(param1);
        //sfile = sfile.Replace('-', '+');
        //sfile = sfile.Replace('_', '/');
        //objTicket.FileBytes = Convert.FromBase64String(sfile.Replace(' ', '+'));
        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, "");
        //Log End

        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objTicket.AuthKey);

        try
        {
            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {
                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }

            var oTicket = new cls_Ticket();
            var result = oTicket.InsertTicketDetail(objTicket);

            if (result[0].ErrorStatus == true)
            {
                oCallDeskRequest.StatusMessage = result[0].ErrorMessage;

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }
            else
            {
                oCallDeskRequest.StatusMessage = "Success";

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
            }

        }
        catch (Exception ex)
        {
            //FileStream fs11 = new FileStream(System.Web.HttpContext.Current.Server.MapPath("..\\Files") + "\\" + "RegisterCall.txt", FileMode.Append, FileAccess.Write);
            //StreamWriter sw11 = new StreamWriter(fs11);
            //sw11.Write(" =======================================================================================");
            //sw11.Write("\r\n Log Entry On : " + System.DateTime.Now.ToString());
            //sw11.Write(ex.Message.ToString());
            //sw11.Write(ex.StackTrace.ToString());
            //sw11.Write("\r\n=======================================================================================");
            //sw11.Close();
            //fs11.Close();
            string strUserId = objTicket.CreatedBy;
            string strInputs = InputsInfo.GetTicketObjInputs(objTicket);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            oCallDeskRequest.StatusMessage = ex.Message;
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
        }
    }

    public Stream CreateTicketIOS(Stream stream)
    {
        var currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        HttpMultipartParser parser = new HttpMultipartParser(stream, "FileContents");

        string objTicket1 = null;
        string param1 = null;
        Ticket objTicket = new Ticket();

        if (parser.Success)
        {
            objTicket1 = parser.Parameters["ticket"];
            objTicket1 = objTicket1.Substring(objTicket1.IndexOf("\r\n\r\n") + 2);

            if (parser.Parameters.Count > 1)
            {

                param1 = HttpUtility.UrlDecode(parser.Parameters["filecontents"]);
                //param1 = param1.Substring(param1.IndexOf("\r\n\r\n") + 2);
            }
            //sfile = parser.Parameters["filebytesasstring"];
        }

        try
        {
            JavaScriptSerializer j = new JavaScriptSerializer();
            objTicket = j.Deserialize<Ticket>("{" + objTicket1);

            if (param1 != null)
            {
                param1 = param1.Replace(' ', '+');
                objTicket.FileBytes = Convert.FromBase64String(param1);
            }
        }
        catch (Exception e)
        {
            string strUserId = objTicket.CreatedBy;
        }
        //objTicket.FileBytes = Convert.FromBase64String(param1);
        //sfile = sfile.Replace('-', '+');
        //sfile = sfile.Replace('_', '/');
        //objTicket.FileBytes = Convert.FromBase64String(sfile.Replace(' ', '+'));
        //Log Start
        ExceptionLogManagement objException = new ExceptionLogManagement();
        objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, "");
        //Log End

        var oCallDeskRequest = CallDeskServiceRequest.IsRequestAuthorized(objTicket.AuthKey);

        try
        {
            if (oCallDeskRequest != null && !oCallDeskRequest.IsValid)
            {
                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }

            var oTicket = new cls_Ticket();
            var result = oTicket.InsertTicketDetail(objTicket);

            if (result[0].ErrorStatus == true)
            {
                oCallDeskRequest.StatusMessage = result[0].ErrorMessage;

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Validation Error", StatusCode = 9999, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
            }
            else
            {
                oCallDeskRequest.StatusMessage = "Success";

                //Output Log Start
                var OutPut = CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);

                string FinalResult = null;
                using (StreamReader reader = new StreamReader(OutPut, Encoding.UTF8))
                {
                    FinalResult = reader.ReadToEnd();
                }

                objException.Log_Ticket(objTicket, currentMethodName, objTicket.CreatedBy, FinalResult);
                //Output Log End

                return CallDeskServiceResponse.ServiceJsonResponse(result != null ? new CallDeskServiceResponse { Status = "Success", StatusCode = 2000, Result = result, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() } : new CallDeskServiceResponse { Status = "Success", StatusCode = 1009, Result = "Data Not Found", ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() }, currentMethodName);
            }

        }
        catch (Exception ex)
        {

            string strUserId = objTicket.CreatedBy;
            string strInputs = InputsInfo.GetTicketObjInputs(objTicket);
            int strStatusCode = ExceptionLogManagement.LogException(ex, "Calldesk Mobile Service", currentMethodName, strInputs, strUserId);
            oCallDeskRequest.StatusMessage = ex.Message;
            return CallDeskServiceResponse.ServiceJsonResponse(new CallDeskServiceResponse { Status = "Error", StatusCode = strStatusCode, Result = null, ServiceRequest = oCallDeskRequest, RequestEndTime = DateTime.Now.ToString() });
        }
    }
}

