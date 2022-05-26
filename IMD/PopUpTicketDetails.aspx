<%@ Page Language="C#" AutoEventWireup="true" Theme="SkinFile" CodeFile="PopUpTicketDetails.aspx.cs"
    Inherits="User_PopUpTicketDetails" Title=":: Call Desk - Ticket Details ::" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../App_Themes/rcd.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" border="0" cellspacing="1" cellpadding="1">
            <tr style="height:15px">
             <td>
             </td>
            </tr>
            <tr id="trRequest" runat="server">
                <td>
                    <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                        <tr class="rcd-TableHeader">
                            <td colspan="2">
                                Ticket Details
                            </td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">
                                Ticket Number
                            </td>
                            <td class="rcd-tableCell" style="width: 25%">
                                <asp:Label ID="lblTicketNumber" runat="server" CssClass="rcd-label" Text="lblTicketNumber"></asp:Label></td>
                           
                        </tr>
                       
                        <tr>
                            <td class="rcd-FieldTitle" style="width: 25%;">
                                First Approver Name
                            </td>
                            <td class="rcd-tableCell" style="width: 25%;">
                                <asp:Label ID="lblApproverName" runat="server" CssClass="rcd-label" Text="lblApproverName"></asp:Label></td>
                           
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle" style="width: 25%">
                                First Approver E-Mail</td>
                            <td class="rcd-tableCell" >
                                 <asp:Label ID="lblApproverEMail" runat="server" CssClass="rcd-label" Text="lblApproverEMail"></asp:Label> </td>
                        </tr>                        
                        <tr id="trSApproverName" runat="server">
                        
                             <td class="rcd-FieldTitle" style="width: 25%">
                                Second Approver Name</td>
                            <td class="rcd-tableCell" style="width: 25%">
                                <asp:Label ID="lblSecondApproverName" runat="server" CssClass="rcd-label"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trSApproverEmail" runat="server" >
                             <td class="rcd-FieldTitle" style="width: 25%">
                                Second Approver E-Mail</td>
                            <td class="rcd-tableCell">
                                <asp:Label ID="lblSecondApproverEMail" runat="server" CssClass="rcd-label"></asp:Label>
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            <tr id="trIssue" runat="server">
               <td>
                     <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                        <tr class="rcd-TableHeader">
                            <td colspan="2">
                                Ticket Details
                            </td>
                        </tr>
                         <tr>
                            <td class="rcd-FieldTitle" style="width: 50%">
                                Ticket Number
                            </td>
                            <td class="rcd-tableCell" >
                                <asp:Label ID="lblTicketNumberIssue" runat="server" CssClass="rcd-label" Text="lblTicketNumberIssue"></asp:Label></td>
                          </tr>
                         <tr>
                           <td colspan="2" class="rcd-tableCell" style="width: 25%">                          
                             <asp:Label ID="lblMsgIssue" runat="server" CssClass="rcd-label" Text="lblMsgIssue"></asp:Label>
                           </td>
                         </tr>
                     </table>
               
               </td>
            </tr>
             <tr style="height:15px">
             <td>
             </td>
            </tr>
             <tr>
              <td align="center" valign="middle">
              <asp:Button ID="btnClose" runat="server" CssClass="rcd-FlatButton" Text="Close" OnClick="btnClose_Click" />
              </td>
            </tr>    
            
        </table>
    </form>
</body>
</html>
