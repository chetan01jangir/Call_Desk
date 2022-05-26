<%@ Page Language="C#" Theme="SkinFile" AutoEventWireup="true" CodeFile="CallDetails.aspx.cs"
    Inherits="User_CallDetails" Title=":: Call Desk - Call Details ::" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../SignUp/rcd.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
         var _source;
         var _popup;
         function showConfirm(source)
         {
             this._source = source;
             this._popup = $find('mdlPopup');
             this._popup.show();
         }
         function okClick()
         {
             this._popup.hide();
             __doPostBack(this._source.name, '');
         }
         function cancelClick()
         {
             this._popup.hide();
             this._source = null;
             this._popup = null;
         }
    </script>

</head>
<body>
    <form id="form1" runat="server">    
        <table width="98%" border="0" cellspacing="1" cellpadding="1">
            <%--<tr>
                <td colspan="2">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="rcd-TopHeaderBlue">
                                Call Details
                            </td>
                        </tr>
                    </table>
                    
                    
                </td>
            </tr>--%>
            <tr>
                <td colspan="4" style="height: 20px">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </td>
            </tr>
            <tr id="trSearch" runat="server">
              <td colspan="4">
                      <table width="50%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                      <tr class="rcd-TableHeader">
                        <td colspan="2">
                          Search TicketNumber
                        </td>
                      </tr>
                       <tr>
                         <td class="rcd-FieldTitle" style="width: 15%">
                           Ticket Number
                         </td>
                         <td class="rcd-tableCell" style="width: 20%">
                          <asp:TextBox ID="txtTicketNo" runat="server" SkinID="textboxSkin" MaxLength="50" Width="200px"></asp:TextBox>
                          <asp:Button ID="btnSearch" runat="server" SkinID="buttonSkin" Text = "Search" OnClick="btnSearch_Click" />
                         </td>                         
                       </tr>
                      
                      </table>
              </td>
            </tr>            
            <tr id="trDetails" runat="server" >
                <td colspan="4">
                    <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                        <tr class="rcd-TableHeader">
                            <td colspan="4"  >
                                Call Details
                            </td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">
                                Ticket Number
                            </td>
                            <td class="rcd-tableCell" style="width: 25%">
                                <asp:Label ID="lblTicketNumber" runat="server" CssClass="rcd-label" Text="lblTicketNumber"></asp:Label></td>
                            <td class="rcd-FieldTitle" style="width: 25%">
                                Application Type
                            </td>
                            <td class="rcd-tableCell" style="width: 25%">
                                <asp:Label ID="lblApplicationType" runat="server" CssClass="rcd-label" Text="lblApplicationType"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">
                                Issue / Requset Type
                            </td>
                            <td class="rcd-tableCell" style="width: 25%">
                                <asp:Label ID="lblIssueRequsetType" runat="server" CssClass="rcd-label" Text="lblIssueRequsetType"></asp:Label></td>
                            <td class="rcd-FieldTitle">
                                Issue / Requset Sub Type
                            </td>
                            <td class="rcd-tableCell" style="width: 25%">
                                <asp:Label ID="lblIssueRequsetSubType" runat="server" CssClass="rcd-label" Text="lblIssueRequsetSubType"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">
                                Call Date
                            </td>
                            <td class="rcd-tableCell" style="width: 25%">
                                <asp:Label ID="lblCallDate" runat="server" CssClass="rcd-label" Text="lblCallDate"></asp:Label></td>
                            <td class="rcd-FieldTitle">
                                Call Status
                            </td>
                            <td class="rcd-tableCell" style="width: 25%">
                                <asp:Label ID="lblCallStatus" runat="server" CssClass="rcd-label" Text="lblCallStatus"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle"  >
                            Ticket value (Premium amount) 
                            </td>
                            <td class="rcd-tableCell" style="width: 25%">
                                <asp:Label ID="lblTicketValue" runat="server" CssClass="rcd-label"></asp:Label>
                            </td>
                            <td class="rcd-FieldTitle">
                            </td>
                            <td class="rcd-tableCell" style="width: 25%">
                            </td>
                                                    
                        </tr>                        
                        <tr>
                            <td colspan="4" class="rcd-tableCell">
                            </td>
                        </tr>
                        <tr class="rcd-TableHeader">
                            <td colspan="4">
                                User Details
                            </td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">
                                Call Logged User
                            </td>
                            <td class="rcd-tableCell">
                                <asp:Label ID="lblCallLoggedUser" CssClass="rcd-label" runat="server" Text="lblCallLoggedUser"></asp:Label></td>
                            <td class="rcd-FieldTitle">
                                Call Logged Branch                                
                            </td>
                            <td class="rcd-tableCell" style="width: 25%">
                                <asp:Label ID="lblCallLoggedLocation" CssClass="rcd-label" runat="server" Text="lblCallLoggedLocation"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">
                                User Remark
                            </td>
                            <td class="rcd-tableCell" colspan="3">
                                <asp:Label ID="lblUserRemark" runat="server" CssClass="rcd-label" Text="lblUserRemark"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">
                                Files Uploaded by User
                            </td>
                            <td class="rcd-tableCell" colspan="3">
                                <asp:GridView ID="gvUploadedFiles" SkinID="gridviewSkin" AutoGenerateColumns="false"
                                    runat="server" OnRowCommand="gvUploadedFiles_RowCommand" OnRowDataBound="gvUploadedFiles_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Files Uploaded by User">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkFileName" CommandName="btnUploadedFile" Text='<%# Bind("UploadedFile") %>'
                                                    CommandArgument='<%# Bind("UploadedFile") %>' runat="server"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="rcd-tableCell">
                            </td>
                        </tr>
                        <tr id="trRequest" runat="server">
                            <td colspan="4" style="height: 156px">
                                <table border="0" cellpadding="1" cellspacing="1" width="100%" class="rcd-TableBorder">
                                    <tr class="rcd-TableHeader">
                                        <td colspan="4">
                                            Approver Details
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%; ;">
                                            Approver Status
                                        </td>
                                        <td class="rcd-tableCell" style="height: 18px;" colspan="3">
                                            <asp:Label ID="lblApproverStatus" runat="server" CssClass="rcd-label" Text="lblApproverStatus"></asp:Label>
                                             </td>
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%; ">
                                            First Approver Name
                                        </td>
                                        <td class="rcd-tableCell" style="width: 25%; ">
                                            <asp:Label ID="lblApproverName" runat="server" CssClass="rcd-label" Text="lblApproverName"></asp:Label></td>
                                        <td class="rcd-FieldTitle" style="width: 25%; ">
                                            First Approver Designation</td>
                                        <td class="rcd-tableCell" style="width: 25%; ">
                                            <asp:Label ID="lblApproverDesignation" runat="server" CssClass="rcd-label" Text="lblApproverDesignation"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%">
                                            First Approver E-Mail</td>
                                        <td class="rcd-tableCell" colspan="3">
                                             <asp:Label ID="lblApproverEMail" runat="server" CssClass="rcd-label" Text="lblApproverEMail"></asp:Label> </td>
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%">
                                            Second Approver Name</td>
                                        <td class="rcd-tableCell" style="width: 25%">
                                            <asp:Label ID="lblSecondApproverName" runat="server" CssClass="rcd-label"></asp:Label>
                                        </td>
                                        <td class="rcd-FieldTitle" style="width: 25%">
                                            Second Approver Designation</td>
                                        <td class="rcd-tableCell" style="width: 25%">
                                            <asp:Label ID="lblSecondApproverDesignation" runat="server" CssClass="rcd-label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%">
                                            Second Approver E-Mail</td>
                                        <td class="rcd-tableCell" colspan="3">
                                            <asp:Label ID="lblSecondApproverEMail" runat="server" CssClass="rcd-label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%">
                                            Approver Remark</td>
                                        <td class="rcd-tableCell" colspan="3">
                                            <asp:Label ID="lblApproverRemark" runat="server" CssClass="rcd-label" Text="lblApproverRemark"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%; ;">
                                            Expected Close Date</td>
                                        <td class="rcd-FieldTitle" style="width: 25%; ;">
                                            <asp:Label ID="lblApproverexpectedCloseDate" runat="server" CssClass="rcd-label"
                                                Text="lblApproverExpectedClosedDate"></asp:Label></td>
                                        <td class="rcd-FieldTitle" style="width: 25%; ;">
                                            Actual Close Date</td>
                                        <td class="rcd-tableCell" style="width: 25%; ;">
                                             <asp:Label ID="lblApproverClosedDate" runat="server" CssClass="rcd-label" Text="lblApproverClosedDate"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%">
                                            Files Uploaded by Approver
                                        </td>
                                        <td class="rcd-tableCell" colspan="3">
                                            <asp:GridView ID="gvFilesUploadedbyApprover" SkinID="gridviewSkin" AutoGenerateColumns="false"
                                                runat="server" OnRowCommand="gvFilesUploadedbyApprover_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Files Uploaded by Approver">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkFileName" CommandName="btnUploadedFile" Text='<%# Bind("FilebyApprover") %>'
                                                                CommandArgument='<%# Bind("FilebyApprover") %>' runat="server"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="rcd-tableCell">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trIssue" runat="server">
                            <td colspan="4">
                                <table border="0" cellpadding="1" cellspacing="1" width="100%" class="rcd-TableBorder">
                                    <tr class="rcd-TableHeader">
                                        <td colspan="4"  >
                                            AppSupport Details
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%">
                                            AppSupport Status
                                        </td>
                                        <td class="rcd-tableCell" style="width: 25%">
                                            <asp:Label ID="lblAppSupportStatus" runat="server" CssClass="rcd-label" Text="lblAppSupportStatus"></asp:Label>
                                            </td>
                                            <td class="rcd-FieldTitle" style="width: 25%">
                                              Ticket Processing  Group
                                            </td>
                                            <td class="rcd-tableCell" style="width: 25%">
                                               <asp:Label ID="lblTicketProGroup" runat="server" CssClass="rcd-label" Text="lblTicketProGroup"></asp:Label>
                                            
                                            </td>
                                            
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%">
                                            AppSupport Remark
                                        </td>
                                        <td class="rcd-tableCell" colspan="3">
                                            <asp:Label ID="lblAppSupportRemark" runat="server" CssClass="rcd-label" Text="lblAppSupportCloseDate"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%; ">
                                            AppSupport Expected Close Date</td>
                                        <td class="rcd-FieldTitle" style="width: 25%; ">
                                            <asp:Label ID="lblAppSupportExpectedCloseDate" runat="server" CssClass="rcd-label"
                                                Text="lblAppSupportCloseDate"></asp:Label></td>
                                        <td id="tdCloseDate" class="rcd-FieldTitle" style="width: 25%;" runat="server">
                                            Actual Close Date</td>
                                        <td class="rcd-FieldTitle" style="width: 25%; ">
                                            <asp:Label ID="lblAppSupportCloseDate" runat="server" CssClass="rcd-label" Text="lblAppSupportCloseDate"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%">
                                            Files Uploaded by AppSupport
                                        </td>
                                        <td class="rcd-tableCell" colspan="3">
                                            <asp:GridView ID="gvFilesUploadedforUser" SkinID="gridviewSkin" AutoGenerateColumns="false"
                                                runat="server" OnRowCommand="gvFilesUploadedforUser_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Files Uploaded by AppSupport">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkFileName" CommandName="btnUploadedFile" Text='<%# Bind("FileforUser") %>'
                                                                CommandArgument='<%# Bind("FileforUser") %>' runat="server"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="rcd-tableCell">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        </table>  
                     </td>
            </tr>   
          </table>                               
    </form>
</body>
</html>
