using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;


    public class InputsInfo
    {
        public static string GetTicketObjInputs(Ticket objTicket)
        {
            try
            {

                StringBuilder strTicketInputs = new StringBuilder();

                strTicketInputs.Append("AuthKey: " + objTicket.AuthKey);
                strTicketInputs.Append(" ,TicketNo: " + objTicket.TicketNO);
                strTicketInputs.Append(" ,ApplicationId: " + Convert.ToString(objTicket.ApplicationId));
                strTicketInputs.Append(" ,ApplicationName: " + objTicket.ApplicationName);
                strTicketInputs.Append(" ,IssueTypeId: " + Convert.ToString(objTicket.IssueTypeId));
                strTicketInputs.Append(" ,IssueType: " + objTicket.IssueType);
                strTicketInputs.Append(" ,IssueSubTypeId: " + Convert.ToString(objTicket.IssueSubTypeId));
                strTicketInputs.Append(" ,IssueSubType: " + objTicket.IssueSubType);
                strTicketInputs.Append(" ,TicketDate: " + objTicket.TicketDate);
                strTicketInputs.Append(" ,UserRemark: " + objTicket.UserRemark);
                strTicketInputs.Append(" ,ContactNumber: " + objTicket.ContactNumber);
                strTicketInputs.Append(" ,Ticketvalue: " + Convert.ToString(objTicket.Ticketvalue));
                strTicketInputs.Append(" ,UploadScreenPath: " + objTicket.UploadScreenPath);
                strTicketInputs.Append(" ,CreatedBy: " + objTicket.CreatedBy);
                strTicketInputs.Append(" ,ApproverId: " + objTicket.ApproverId);
                strTicketInputs.Append(" ,TicketStatus: " + objTicket.TicketStatus);
                strTicketInputs.Append(" ,UserConfirmation: " + objTicket.UserConfirmation);
                strTicketInputs.Append(" ,ExpectedApprovedDate: " + objTicket.ExpectedApprovedDate);
                strTicketInputs.Append(" ,ExpectedCloseDate: " + objTicket.ExpectedCloseDate);
                strTicketInputs.Append(" ,EscalationRemark: " + objTicket.EscalationRemark);
                strTicketInputs.Append(" ,ReturnValue: " + objTicket.ReturnValue);
                strTicketInputs.Append(" ,DueDate: " + objTicket.DueDate);
                strTicketInputs.Append(" ,CallStartDate: " + objTicket.CallStartDate);
                strTicketInputs.Append(" ,CallEndDate: " + objTicket.CallEndDate);
                strTicketInputs.Append(" ,ErrorStatus: " + Convert.ToString(objTicket.ErrorStatus));
                strTicketInputs.Append(" ,ErrorMessage: " + objTicket.ErrorMessage);
                strTicketInputs.Append(" ,FileName: " + objTicket.FileName);
                strTicketInputs.Append(" ,AssignToUserId: " + objTicket.AssignToUserId);
                strTicketInputs.Append(" ,StatusOther: " + objTicket.StatusOther);
                strTicketInputs.Append(" ,StatusSub: " + objTicket.StatusSub);
                strTicketInputs.Append(" ,FileBytesAsString: " + objTicket.FileBytesAsString);

                //if (objTicket.FileBytes != null)
                //{
                //    strTicketInputs.Append(" ,FileBytes: " + Encoding.Unicode.GetString(objTicket.FileBytes));
                //}


                return strTicketInputs.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string GetUserObjectInputs(UserDetail objUser)
        {
            try
            {
                StringBuilder strUserInputs = new StringBuilder();

                strUserInputs.Append("UserId: " + objUser.UserId);
                strUserInputs.Append(" ,AuthKey: " + objUser.AuthKey);
                strUserInputs.Append(" ,Password: " + objUser.Password);
                strUserInputs.Append(" ,ApplicationId: " + Convert.ToString(objUser.ApplicationId));
                strUserInputs.Append(" ,ApplicationName: " + objUser.ApplicationName);
                strUserInputs.Append(" ,IssueTypeId: " + Convert.ToString(objUser.IssueTypeId));
                strUserInputs.Append(" ,IssueType: " + objUser.IssueType);
                strUserInputs.Append(" ,IssueSubTypeId: " + Convert.ToString(objUser.IssueSubTypeId));
                strUserInputs.Append(" ,IssueSubType: " + objUser.IssueSubType);
                strUserInputs.Append(" ,UserType: " + objUser.UserType);
                strUserInputs.Append(" ,Name: " + objUser.Name);
                strUserInputs.Append(" ,UserLastLogin: " + objUser.UserLastLogin);
                strUserInputs.Append(" ,UserLastLogOut: " + objUser.UserLastLogOut);

                return strUserInputs.ToString();
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public static string GetCalldeskReportObjectInputs(CalldeskReport objCalldeskReport)
        {
            try
            {
                StringBuilder strUserInputs = new StringBuilder();
                strUserInputs.Append("AuthKey: " + objCalldeskReport.AuthKey);
                strUserInputs.Append(" ,StartDate: " + Convert.ToString(objCalldeskReport.StartDate));
                strUserInputs.Append(" ,EndDate: " + Convert.ToString(objCalldeskReport.EndDate));

                return strUserInputs.ToString();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }

   
