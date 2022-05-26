/****************************************************************************************************************
Class Name   : AppSettingsConstants.cs 
Purpose      : Used to define appSettings KEYS of web.config
Created By   : 
Created Date : 
Version      : 1.0
History      :
Modified By        | CR <CR NO  : NAME>/BUG ID/Interaction No  | Date(dd/MMM/yyyy) | Comments
<EMP Name(EMP ID)> | <CR <CR NO : NAME>/BUG ID/Interaction No> |  dd/MMM/yyyy      | <Reason For Modifications>
****************************************************************************************************************/

#region "Using Directives"



#endregion


    public static class AppSettingsConstants
    {
        #region Public Variables

        #region Supress Example
        //[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Staffware", Justification = "FxCop doesn't know that Staffware is a software name")]
        //public const string StaffwareQueryClosureProcedureName = "SW_QC_ProcName";
        #endregion

        #region Connection String
        //public static string RGICL_MC_ConnectionString = "RGICL_MC_ConnectionString";
        #endregion

        #region Audit Vulnerabilities
        public const string SqlCharacters = "SqlCharacters",
                            SqlInjectionEnabled = "SqlInjectionEnabled",
                            SkipParameters = "SkipParameters",
                            SkipPagesSqlInjection = "SkipPagesSqlInjection",

                            UrlEncryptionEnabled = "URLEncryptionEnabled",
                            SkipPagesUrlEncryption = "SkipPagesURLEncryption",
                            AnchorHRefEncryptionEnabled = "AnchorHRefEncryptionEnabled",
                            SkipPagesAnchorHRefEncryption = "SkipPagesAnchorHRefEncryption",

                            EnableCsrfProtection = "EnableCSRFProtection",
                            SkipCsrfProtection = "SkipCSRFProtection",
                            CsrfAttackRedirectPage = "CSRFAttackRedirectPage",

                            FileTypeGenericHandlersEnabled = "FileTypeGenericHandlersEnabled",
                            SkipExtensionsFileTypeGenericHandlers = "SkipExtensionsFileTypeGenericHandlers",

                            LfiDownloadInvalidExtensions = "LFIDownloadInvalidExtensions",
                            DorInvalidExtensions = "DORInvalidExtensions",



                            SavvionUserID = "SavvionUserID",
                            SavvionUserPass = "SavvionUserPass",


                            AuthorizationEnabled = "AuthorizationEnabled",
                            SkipPagesAuthorization = "SkipPagesAuthorization",
                            MobileServiceAuthorization = "AuthCustomHeader",
                            MobileServiceSkipAuthorization = "SkipCustomHeader",
                            CustomAuthorizationEnabled = "CustomAuthorizationEnabled",
                            EnableMultipleLoginCheck = "EnableMultipleLogin",
                            EnableSitFakeUserLogin = "EnableSitFakeLogin",
                            SitFakeUserLoginId = "SITFakeUserLoginId",
                            FakeUserDetails = "FakeLoginIdPwd",

                            EfinderUserId = "EfinderUserId",
                            EfinderUserPwd = "EfinderUserPwd",

        EnableSavvionUserCreate = "CreateSavvionUser",
        PreInspectionPdfFolder = "PreInspectionPdfFolderPath",
        PreInspectionPdfUrl = "PreInspectionPdfURL",
        SurveyFileUrl = "FileUrl",
        LatestApkFileUrl = "LatestApkUrl",
        LossAndAsstReportHtmlFileUrl = "LossAndAsstReportHtmlFilePath",
        LossAndAsstReportPdfRelativePath = "LossAndAsstReportPdfRelativePath",
        LossAndAsstReportFolderPath = "LossAndAsstReportPdfFilePath",
        DOPdfGenerationPath = "DOPdfGenerationPath",
        OldPolicyCallExpireIntervalInMinutes="OldPolicyCallExpireIntervalInMinutes";
        #endregion

        #region "LogMessageFlag"

        public const string LogStartMessage = "ConfigLogStartMessage",
            LogEndMessage = "ConfigLogEndMessage";
        #endregion

        #region Error/StackTrace
        public const string StackTraceLogEnabled = "StackTraceLogEnabled",
                            ErrorLogType = "ErrorLogType",
                            ErrorLogEnabled = "ErrorLogEnabled",
                            spNameForLog="spNameForLog",
                            CheckSPNameForLog = "CheckSPNameForLog";
        #endregion

        #region EmailSMS
        public const string SMTP = "SMTP",
                            SMTPAuthenticate = "SMTPAuthenticate",
                            EnableSsl = "EnableSsl",
                            MailFromEmailID = "MailFromEmailID",
                            MailUserName = "MailUserName",
                            MailPassword = "MailPassword",
            //IncomingMailServerType = "IncomingMailServerType",
            //IncomingMailServer = "IncomingMailServer",
            //IncomingMailServerPort = "IncomingMailServerPort",
                            OutgoingMailServerPort = "OutgoingMailServerPort",
                            MailAttachmentMaxSize = "MailAttachmentMaxSize",
                            MailAttachmentMaxSizeMessage = "MailAttachmentMaxSizeMessage",
                            EmailSchedulerEnabled = "EmailSchedulerEnabled",
                            SMSSchedulerEnabled = "SMSSchedulerEnabled",
                            MailAttachemntPath = @"D:\CMSAPP\EmailAttachment";


        public const string SMSUserName = "SMSUserName",
                            SMSPassword = "SMSPassword";
        #endregion
        public const string ApplicationURL = "ApplicationURL";
        public const string ADSDomain = "ADSDomain";
        public const string RightClickEnabled = "RightClickEnabled";
        public const string CAPTCHAVALUE = "CaptchaValue";
        public const string CachetimeForRegister = "CachetimeForRegister";
        public const string MaxRepeatedChatMsgCount = "MaxRepeatedChatMsgCount";
        public const string MaxChatParticipants = "MaxChatParticipantsCount";
         
        

        #region Path KEYS
        public const string UploadFolderPath = "UploadFolderPath",
                            LogsFolderPath = "LogsFolderPath",
                            XMLFolderPath = "XMLFolderPath",
                            XMLConfigFileName = "XMLConfigFileName",
                            EmailAttachmentFolderPath = "EmailAttachmentFolderPath",
                            UploadChequeFolderPath = "UploadChequeFolderPath",
                            UserPicVdPath = "UserProfilePic",
                            TabControl = "TabControl",
                            HomePage = "HomePage",
                            SkipPagesForSpotReinsTabs = "SkipPagesForSpotReinsTabs";

        #endregion
        public const string IscompressionEnable = "IscompressionEnable";
        public const string ECSUrl = "ECSURL";
        public const string UserPicsFolderPath = "UserPicsFolderPath";//"../UserPics/";
        public const string UserPicRelativePath = "UserPicRelativePath";
        public const string DefaultPic = "DefaultUserPic";
        public const string MobileDocUploadPath = "DocumentUploadPath";//"../UserPics/";
        public const string MobileDocUploadRelativePath = "DocumentUploadRelativePath";//"../UserPics/";
        public const string IsBypassSavvion = "IsBypassSavvion";
        public const string RolesAboveZM = "RolesAboveZM";
        public const string PdfToImageGhost = "PdfToImageGhost";
        #endregion


        #region Calldesk
        public const string MobileSecureKey = "MobileSecureKey";
        public const string TimeDuration = "TimeDuration";
        public const string UploadSavePath = "UploadSavePath1";
        public const string UploadsavePathWebsite = "UploadsavePathWebsite";
        public const string FileTemplatePath = "FileTemplatePath";
        #endregion
    }

