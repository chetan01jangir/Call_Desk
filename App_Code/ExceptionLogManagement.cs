using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CalldeskServices;

using ExceptionHandling;



    public class ExceptionLogManagement
    {
        public static ExceptionInfo objExceptionInfo = new ExceptionInfo();

        public static bool rethrow = true;


        public ExceptionLogManagement()
        {
            objExceptionInfo = new ExceptionInfo();
        }

        public static int LogException(Exception ex, string ModuleName, string FunctionName, string FunctionInputs, string UserId)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["CallDeskDB123"].ToString();

            SqlParameter[] objParam = new SqlParameter[12];

            objParam[0] = new SqlParameter("@ExceptionMessage", SqlDbType.VarChar);
            objParam[0].Value = ex.Message.ToString();
            objParam[0].Direction = ParameterDirection.Input;

            objParam[1] = new SqlParameter("@ExceptionSource", SqlDbType.VarChar);
            objParam[1].Value = ex.Source.ToString();
            objParam[1].Direction = ParameterDirection.Input;

            objParam[2] = new SqlParameter("@StackTrace", SqlDbType.VarChar);
            objParam[2].Value = ex.StackTrace.ToString();
            objParam[2].Direction = ParameterDirection.Input;

            objParam[3] = new SqlParameter("@TargetSiteName", SqlDbType.VarChar);
            objParam[3].Value = ex.TargetSite.ToString();
            objParam[3].Direction = ParameterDirection.Input;

            objParam[4] = new SqlParameter("@TargetSiteModule", SqlDbType.VarChar);
            objParam[4].Value = ex.TargetSite.Module.ToString();
            objParam[4].Direction = ParameterDirection.Input;

            objParam[5] = new SqlParameter("@HelpLink", SqlDbType.VarChar);
            objParam[5].Value = "";
            objParam[5].Direction = ParameterDirection.Input;

            objParam[6] = new SqlParameter("@ExceptionType", SqlDbType.VarChar);
            objParam[6].Value = ex.GetType().ToString();
            objParam[6].Direction = ParameterDirection.Input;

            objParam[7] = new SqlParameter("@ModuleName", SqlDbType.VarChar);
            objParam[7].Value = ModuleName.ToString();
            objParam[7].Direction = ParameterDirection.Input;

            objParam[8] = new SqlParameter("@FunctionName", SqlDbType.VarChar);
            objParam[8].Value = FunctionName.ToString();
            objParam[8].Direction = ParameterDirection.Input;

            objParam[9] = new SqlParameter("@TokenNo", SqlDbType.VarChar, 10);
            objParam[9].Value = "";
            objParam[9].Direction = ParameterDirection.InputOutput;

            objParam[10] = new SqlParameter("@FunctionInputs", SqlDbType.VarChar);
            objParam[10].Value = FunctionInputs.ToString();
            objParam[10].Direction = ParameterDirection.Input;

            objParam[11] = new SqlParameter("@UserId", SqlDbType.VarChar);
            objParam[11].Value = UserId;
            objParam[11].Direction = ParameterDirection.Input;

            DataSet ds = SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, AppConstants.Usp_MobileService_LogException, objParam);

            int TokenNo = Convert.ToInt32(objParam[9].Value);

            return TokenNo;

        }

        public void Log_Ticket(Ticket objTicket, string FunctionName, string UserId, string Output)
        {
            string FunctionInputs = InputsInfo.GetTicketObjInputs(objTicket);

            string connectionString = ConfigurationManager.ConnectionStrings["CallDeskDB123"].ToString();

            SqlParameter[] objParam = new SqlParameter[13];

            objParam[0] = new SqlParameter("@ExceptionMessage", SqlDbType.VarChar);
            objParam[0].Value = "I/O";
            objParam[0].Direction = ParameterDirection.Input;

            objParam[1] = new SqlParameter("@ExceptionSource", SqlDbType.VarChar);
            objParam[1].Value = "I/O";
            objParam[1].Direction = ParameterDirection.Input;

            objParam[2] = new SqlParameter("@StackTrace", SqlDbType.VarChar);
            objParam[2].Value = "I/O";
            objParam[2].Direction = ParameterDirection.Input;

            objParam[3] = new SqlParameter("@TargetSiteName", SqlDbType.VarChar);
            objParam[3].Value = "I/O";
            objParam[3].Direction = ParameterDirection.Input;

            objParam[4] = new SqlParameter("@TargetSiteModule", SqlDbType.VarChar);
            objParam[4].Value = "I/O";
            objParam[4].Direction = ParameterDirection.Input;

            objParam[5] = new SqlParameter("@HelpLink", SqlDbType.VarChar);
            objParam[5].Value = "";
            objParam[5].Direction = ParameterDirection.Input;

            objParam[6] = new SqlParameter("@ExceptionType", SqlDbType.VarChar);
            objParam[6].Value = "I/O";
            objParam[6].Direction = ParameterDirection.Input;

            objParam[7] = new SqlParameter("@ModuleName", SqlDbType.VarChar);
            objParam[7].Value = "CallDesk Rest Service";
            objParam[7].Direction = ParameterDirection.Input;

            objParam[8] = new SqlParameter("@FunctionName", SqlDbType.VarChar);
            objParam[8].Value = FunctionName.ToString();
            objParam[8].Direction = ParameterDirection.Input;

            objParam[9] = new SqlParameter("@TokenNo", SqlDbType.VarChar, 10);
            objParam[9].Value = "";
            objParam[9].Direction = ParameterDirection.InputOutput;

            objParam[10] = new SqlParameter("@FunctionInputs", SqlDbType.VarChar);
            objParam[10].Value = FunctionInputs.ToString();
            objParam[10].Direction = ParameterDirection.Input;

            objParam[11] = new SqlParameter("@UserId", SqlDbType.VarChar);
            objParam[11].Value = UserId;
            objParam[11].Direction = ParameterDirection.Input;

            objParam[12] = new SqlParameter("@Output", SqlDbType.VarChar);
            objParam[12].Value = Output;
            objParam[12].Direction = ParameterDirection.Input;

            DataSet ds = SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, AppConstants.Usp_MobileService_LogException, objParam);

            //int TokenNo = Convert.ToInt32(objParam[9].Value);

            //return TokenNo;

        }

        public void Log_UserDetail(UserDetail objUserDetail, string FunctionName, string UserId, string Output)
        {
            string FunctionInputs = InputsInfo.GetUserObjectInputs(objUserDetail);

            string connectionString = ConfigurationManager.ConnectionStrings["CallDeskDB123"].ToString();

            SqlParameter[] objParam = new SqlParameter[13];

            objParam[0] = new SqlParameter("@ExceptionMessage", SqlDbType.VarChar);
            objParam[0].Value = "I/O";
            objParam[0].Direction = ParameterDirection.Input;

            objParam[1] = new SqlParameter("@ExceptionSource", SqlDbType.VarChar);
            objParam[1].Value = "I/O";
            objParam[1].Direction = ParameterDirection.Input;

            objParam[2] = new SqlParameter("@StackTrace", SqlDbType.VarChar);
            objParam[2].Value = "I/O";
            objParam[2].Direction = ParameterDirection.Input;

            objParam[3] = new SqlParameter("@TargetSiteName", SqlDbType.VarChar);
            objParam[3].Value = "I/O";
            objParam[3].Direction = ParameterDirection.Input;

            objParam[4] = new SqlParameter("@TargetSiteModule", SqlDbType.VarChar);
            objParam[4].Value = "I/O";
            objParam[4].Direction = ParameterDirection.Input;

            objParam[5] = new SqlParameter("@HelpLink", SqlDbType.VarChar);
            objParam[5].Value = "";
            objParam[5].Direction = ParameterDirection.Input;

            objParam[6] = new SqlParameter("@ExceptionType", SqlDbType.VarChar);
            objParam[6].Value = "I/O";
            objParam[6].Direction = ParameterDirection.Input;

            objParam[7] = new SqlParameter("@ModuleName", SqlDbType.VarChar);
            objParam[7].Value = "CallDesk Rest Service";
            objParam[7].Direction = ParameterDirection.Input;

            objParam[8] = new SqlParameter("@FunctionName", SqlDbType.VarChar);
            objParam[8].Value = FunctionName.ToString();
            objParam[8].Direction = ParameterDirection.Input;

            objParam[9] = new SqlParameter("@TokenNo", SqlDbType.VarChar, 10);
            objParam[9].Value = "";
            objParam[9].Direction = ParameterDirection.InputOutput;

            objParam[10] = new SqlParameter("@FunctionInputs", SqlDbType.VarChar);
            objParam[10].Value = FunctionInputs.ToString();
            objParam[10].Direction = ParameterDirection.Input;

            objParam[11] = new SqlParameter("@UserId", SqlDbType.VarChar);
            objParam[11].Value = UserId;
            objParam[11].Direction = ParameterDirection.Input;

            objParam[12] = new SqlParameter("@Output", SqlDbType.VarChar);
            objParam[12].Value = Output;
            objParam[12].Direction = ParameterDirection.Input;

            DataSet ds = SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, AppConstants.Usp_MobileService_LogException, objParam);

            //int TokenNo = Convert.ToInt32(objParam[9].Value);

            //return TokenNo;

        }

        public void Log_CallDeskReport(CalldeskReport objCalldeskReport, string FunctionName, string UserId, string Output)
        {
            string FunctionInputs = InputsInfo.GetCalldeskReportObjectInputs(objCalldeskReport);

            string connectionString = ConfigurationManager.ConnectionStrings["CallDeskDB123"].ToString();

            SqlParameter[] objParam = new SqlParameter[13];

            objParam[0] = new SqlParameter("@ExceptionMessage", SqlDbType.VarChar);
            objParam[0].Value = "I/O";
            objParam[0].Direction = ParameterDirection.Input;

            objParam[1] = new SqlParameter("@ExceptionSource", SqlDbType.VarChar);
            objParam[1].Value = "I/O";
            objParam[1].Direction = ParameterDirection.Input;

            objParam[2] = new SqlParameter("@StackTrace", SqlDbType.VarChar);
            objParam[2].Value = "I/O";
            objParam[2].Direction = ParameterDirection.Input;

            objParam[3] = new SqlParameter("@TargetSiteName", SqlDbType.VarChar);
            objParam[3].Value = "I/O";
            objParam[3].Direction = ParameterDirection.Input;

            objParam[4] = new SqlParameter("@TargetSiteModule", SqlDbType.VarChar);
            objParam[4].Value = "I/O";
            objParam[4].Direction = ParameterDirection.Input;

            objParam[5] = new SqlParameter("@HelpLink", SqlDbType.VarChar);
            objParam[5].Value = "";
            objParam[5].Direction = ParameterDirection.Input;

            objParam[6] = new SqlParameter("@ExceptionType", SqlDbType.VarChar);
            objParam[6].Value = "I/O";
            objParam[6].Direction = ParameterDirection.Input;

            objParam[7] = new SqlParameter("@ModuleName", SqlDbType.VarChar);
            objParam[7].Value = "CallDesk Rest Service";
            objParam[7].Direction = ParameterDirection.Input;

            objParam[8] = new SqlParameter("@FunctionName", SqlDbType.VarChar);
            objParam[8].Value = FunctionName.ToString();
            objParam[8].Direction = ParameterDirection.Input;

            objParam[9] = new SqlParameter("@TokenNo", SqlDbType.VarChar, 10);
            objParam[9].Value = "";
            objParam[9].Direction = ParameterDirection.InputOutput;

            objParam[10] = new SqlParameter("@FunctionInputs", SqlDbType.VarChar);
            objParam[10].Value = FunctionInputs.ToString();
            objParam[10].Direction = ParameterDirection.Input;

            objParam[11] = new SqlParameter("@UserId", SqlDbType.VarChar);
            objParam[11].Value = UserId;
            objParam[11].Direction = ParameterDirection.Input;

            objParam[12] = new SqlParameter("@Output", SqlDbType.VarChar);
            objParam[12].Value = Output;
            objParam[12].Direction = ParameterDirection.Input;

            DataSet ds = SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, AppConstants.Usp_MobileService_LogException, objParam);

            //int TokenNo = Convert.ToInt32(objParam[9].Value);

            //return TokenNo;

        }

    }
