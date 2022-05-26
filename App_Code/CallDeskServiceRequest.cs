using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Web;

using System.ServiceModel.Channels;
using System.Net;
using System.Text;


    public class CallDeskServiceRequest
    {
        private int UserId { get; set; }
        private string LoginId { get; set; }
        private string Password { get; set; }
        private string DeviceNumber { get; set; }
        private string ServiceName { get; set; }
        public string StatusMessage { get; set; }
        public bool IsValid { get; set; }
        public string RequestStartTime { get; set; }


        public CallDeskServiceRequest(string loginId, string password, string deviceNo, string svcName)
        {
            LoginId = loginId;
            Password = password;
            DeviceNumber = deviceNo;
            ServiceName = svcName;
            RequestStartTime = Convert.ToString(DateTime.Now);

        }

        public CallDeskServiceRequest()
        {
            // TODO: Complete member initialization
        }

        public static CallDeskServiceRequest IsRequestAuthorized(string AuthKey)
        {
            if (AuthKey == null)
            {
                return new CallDeskServiceRequest { IsValid=false,StatusMessage = MessageConstants.AuthKeyBlank };
                WriteLogFile.WriteLog(" AuthKey is blank", String.Format("{0} @ {1}", "Log is Created at", DateTime.Now));
            }

            AuthKey objAuthKey = new AuthKey();
            objAuthKey = DecryptAuthKey(AuthKey);
            WriteLogFile.WriteLog(" DecryptAuthKey", String.Format("{0} @ {1}", "Log is Created at", DateTime.Now));

            // DateTime RequestDate = Convert.ToDateTime(objAuthKey.TimeSpan);
             //DateTime RequestDate = DateTime.ParseExact(objAuthKey.TimeSpan, "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

             //DateTime CurrentTimeStamp = DateTime.Now;

             //TimeSpan ts = new TimeSpan();
             //ts = CurrentTimeStamp.Subtract(RequestDate);

             //int TimespanMinutes =Convert.ToInt32(ts.TotalMinutes);

             //string TimeDuration = AppSettingsUtility.GetAppSettingsKeyValue(AppSettingsConstants.TimeDuration);

             //if (TimespanMinutes < Convert.ToInt32(TimeDuration))
             //{


            WriteLogFile.WriteLog(" IsCalldeskAuthenticate", String.Format("{0} @ {1}", "Log is Created at", DateTime.Now)); 
                 var MobileSecureKey = AppSettingsUtility.GetAppSettingsKeyValue(AppSettingsConstants.MobileSecureKey);

                 Cls_validation objValidation = new Cls_validation();
                 bool IsCalldeskAuthenticate = objValidation.CallDeskAuthentication(objAuthKey.UserId);
                 bool IsSavvionAuthenticate = objValidation.SavvionAuthentication(objAuthKey.UserId);
                 WriteLogFile.WriteLog(" IsSavvionAuthenticate", String.Format("{0} @ {1}", "Log is Created at", DateTime.Now)); 
                 if (MobileSecureKey == objAuthKey.Password)
                 {

                     if (IsCalldeskAuthenticate == true || IsSavvionAuthenticate == true)
                     {
                         return new CallDeskServiceRequest { IsValid = true };
                     }
                     else
                     {
                         return new CallDeskServiceRequest { IsValid = false, StatusMessage = MessageConstants.InvalidLoginId };
                     }
                 }
                 else
                 {
                     return new CallDeskServiceRequest { IsValid = false, StatusMessage = MessageConstants.AuthKeyInvalid };
                 }
             //}
             //else
             //{
             //    return new CallDeskServiceRequest { IsValid = false, RequestError = MessageConstants.AuthKeyInvalid };
             //} 
        }

        public static CallDeskServiceRequest IsRequestAuthorized1()
        {
            WriteLogFile.WriteLog(" IsRequestAuthorized1", String.Format("{0} @ {1}", "Log is Created at", DateTime.Now)); 
            if (WebOperationContext.Current == null && WebOperationContext.Current.IncomingRequest == null) return null;

            string authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];

            //HttpContext httpContext = HttpContext.Current;
            //string authHeader = this.httpContext.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                string[] Array = usernamePassword.Split('~');

                string UserId = null, password = null, TimeStamp = null;
                for (int i = 0; i < Array.Length; i++)
                {
                    UserId = Array[0].ToString();
                    password = Array[1].ToString();
                    TimeStamp = Array[2].ToString();
                }

                //int seperatorIndex = usernamePassword.IndexOf('~');
                //var UserId = usernamePassword.Substring(0, seperatorIndex);
                //var password = usernamePassword.Substring(seperatorIndex + 1);
                //var TimeStamp = usernamePassword.Substring(seperatorIndex + 2);

                Cls_validation objValidation = new Cls_validation();
                bool IsCalldeskAuthenticate = objValidation.CallDeskAuthentication(UserId.ToString());

                return new CallDeskServiceRequest { IsValid = true };
            }
            else
            {
                return null;
            }

            var configValue = AppSettingsUtility.GetAppSettingsKeyValue(AppSettingsConstants.MobileServiceAuthorization);
            var skipValue = AppSettingsUtility.GetAppSettingsKeyValue(AppSettingsConstants.MobileServiceSkipAuthorization);
            if (Convert.ToBoolean(configValue))
            {
                if (WebOperationContext.Current != null &&
                    WebOperationContext.Current.IncomingRequest.Headers["Status"] != null)
                {
                    var errorJson = WebOperationContext.Current.IncomingRequest.Headers["Status"];
                    var objRequest = JsonHelper.JsonDeserialize<CallDeskServiceRequest>(errorJson);
                    return objRequest;
                }
                if (Convert.ToBoolean(skipValue))
                {
                    return new CallDeskServiceRequest { IsValid = true };
                }
            }
            else
            {
                return new CallDeskServiceRequest { IsValid = true };
            }
            return null;
        }

        public static AuthKey DecryptAuthKey(string AuthKey)
        {
            try
            {
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(AuthKey));

                string[] Array = usernamePassword.Split('~');

                string strUserId = null, strpassword = null, strTimeStamp = null, strIMEI = null;

                for (int i = 0; i < Array.Length; i++)
                {
                    strUserId = Array[0].ToString();
                    strpassword = Array[1].ToString();
                    strIMEI = Array[2].ToString();
                    strTimeStamp = Array[3].ToString();
                }

                AuthKey objAuthKey = new AuthKey();
                objAuthKey.UserId = strUserId;
                objAuthKey.Password = strpassword;
                objAuthKey.IMEI = strIMEI;
                objAuthKey.TimeSpan = strTimeStamp;              


                return objAuthKey;
            }
            catch (Exception)
            {

                AuthKey objAuthKey = new AuthKey();
                objAuthKey.UserId = "";
                objAuthKey.Password = "";
                objAuthKey.IMEI = "";
                objAuthKey.TimeSpan = "";
                return objAuthKey;
            }

        }
    }


