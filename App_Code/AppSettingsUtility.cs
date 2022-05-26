/****************************************************************************************************************
Class Name   : AppSettingsUtility.cs 
Purpose      : Used to define utility functions for appSettings keys of web.config
Created By   : 
Created Date : 
Version      : 1.0
History      :
Modified By        | CR <CR NO  : NAME>/BUG ID/Interaction No  | Date(dd/MMM/yyyy) | Comments
<EMP Name(EMP ID)> | <CR <CR NO : NAME>/BUG ID/Interaction No> |  dd/MMM/yyyy      | <Reason For Modifications>
****************************************************************************************************************/

#region "Using Directives"
using System;
using System.Configuration;
#endregion


    public static class AppSettingsUtility
    {
        #region Public Methods
        /// <summary>
        /// GetAppSettingsKeyValue function will return value of appSettings Key defined in web.config
        /// </summary>
        /// <param name="strKey">appSettings Key</param>
        /// <returns></returns>
        public static string GetAppSettingsKeyValue(string strKey)
        {
            return GetAppSettingsKeyValue(strKey, string.Empty);
        }
        public static string GetAppSettingsKeyValue(string strKey, string strDefaultValue)
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings[strKey]))
                return ConfigurationManager.AppSettings[strKey];

            return strDefaultValue;
        }
        public static bool IsEnabledAppSettingsKeyValue(string strKey, bool bDefaultValue = false)
        {
            if (GetAppSettingsKeyValue(strKey).ToUpper() == "TRUE")
                return true;
            return bDefaultValue;
        }
        #endregion
    }

