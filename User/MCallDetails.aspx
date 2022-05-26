<%@ Page Language="C#" AutoEventWireup="true" Theme="SkinFile" CodeFile="MCallDetails.aspx.cs"
    Inherits="User_CallDetails" Title=":: Call Desk - Call Details ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../App_Themes/mrcd.css" rel="stylesheet" type="text/css" />

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
           
            <tr>
                <td colspan="2" style="height: 20px">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                <div style="min-height:1200px; background-color:White; width:100%">
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
                                            Performer
                                        </td>
                                        <td class="rcd-tableCell" colspan="3">
                                            <asp:Label ID="lblperformer" runat="server" CssClass="rcd-label" Text="lblPerformer"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%">
                                            AppSupport Remark
                                        </td>
                                        <td class="rcd-tableCell" colspan="3">
                                            <asp:Label ID="lblAppSupportRemark" runat="server" CssClass="rcd-label" Text="lblAppSupportCloseDate"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%">
                                            Closure Category
                                        </td>
                                        <td class="rcd-tableCell" colspan="3">
                                            <asp:Label ID="lblclosurecategory" runat="server" CssClass="rcd-label" Text="lblclosurecategory"></asp:Label></td>
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
                        <tr id="trReopen" runat="server">
                            <td colspan="4">
                                <table border="0" cellpadding="1" cellspacing="1" width="100%" class="rcd-TableBorder">
                                    <tr class="rcd-TableHeader">
                                        <td colspan="4">
                                            Reopen Call
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%">
                                            Upload File</td>
                                        <td class="rcd-tableCell" style="width: 75%">
                                            <asp:FileUpload ID="fuUpLoadFile" runat="server" SkinID="FileUploaderSkin" />
                                            <asp:RegularExpressionValidator ID="revFileUpload" runat="server" ControlToValidate="fuUpLoadFile"
                                                Display="None" ValidationGroup="CheckData" ErrorMessage="File Format not Supported"
                                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.doc|.DOC|.docx|.DOCX|.xls|.XLS|.xlsx|.XLSX|.txt|.TXT|.jpeg|.JPEG|.zip|.ZIP|.Zip)$">
                                            </asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceFileUpload" WarningIconImageUrl="../Images/Warning1.jpg"
                                                TargetControlID="revFileUpload" runat="server">
                                            </cc1:ValidatorCalloutExtender>
                                            <br />
                                            <font style="font-style: italic; color: Maroon; font-size: smaller;">Note : Please zip
                                                files and attach if you want to attach more than one file.</font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="rcd-FieldTitle" style="width: 25%">
                                            Reopen remark
                                        </td>
                                        <td class="rcd-tableCell" style="width: 75%">
                                            <asp:TextBox ID="txtReopenRemark" CssClass="textbox" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvReopenRemark" runat="server" ControlToValidate="txtReopenRemark"
                                                ValidationGroup="CheckData1" SetFocusOnError="true" Display="None" ErrorMessage="Enter remark" />
                                            <cc1:ValidatorCalloutExtender ID="vceReopenRemark" TargetControlID="rfvReopenRemark"
                                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                              <tr runat="server" id="trlargemarket">
                             <td class="rcd-tableCell" style="width: 70%" colspan="2">
                              <asp:UpdatePanel ID="uplargemarket" runat="server" UpdateMode="Conditional">
                                    <contenttemplate>
                              <table width="100%" border="0" cellpadding="1" cellspacing="1" >
                                    <tr>
                                     <td class="rcd-FieldTitle" style="width: 30%">
                                        Branch Name
                                     </td>
                                       <td class="rcd-tableCell" style="width: 70%">
                                                <asp:DropDownList ID="ddlbranchname" runat="server" CssClass="DropDownList" AutoPostBack="true" OnSelectedIndexChanged="ddlbranchnameselectedindexchanged"  >
                                                </asp:DropDownList>
                                       </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="rfvbranchname" runat="server" ControlToValidate="ddlbranchname"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Select Branch Name"
                                                    ValidationGroup="CheckData" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="vcebranchname" WarningIconImageUrl="../Images/Warning1.jpg"
                                                    runat="server" TargetControlID="rfvbranchname" CssClass="CustomValidator">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                    </tr>
                                    <tr>
                                     <td class="rcd-FieldTitle" style="width: 30%">
                                        Channel
                                     </td>
                                       <td class="rcd-tableCell" style="width: 70%">
                                                <asp:DropDownList ID="ddlchannel" runat="server" CssClass="DropDownList" AutoPostBack="true" OnSelectedIndexChanged="ddlchannelselectedindexchanged"  >
                                                </asp:DropDownList>
                                       </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="rfvchannel" runat="server" ControlToValidate="ddlchannel"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Select Channel"
                                                    ValidationGroup="CheckData" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="vcechannel" WarningIconImageUrl="../Images/Warning1.jpg"
                                                    runat="server" TargetControlID="rfvchannel" CssClass="CustomValidator">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                    </tr>
                                     <tr>
                                     <td class="rcd-FieldTitle" style="width: 30%">
                                        SM
                                     </td>
                                       <td class="rcd-tableCell" style="width: 70%">
                                                <asp:DropDownList ID="ddlsm" runat="server" CssClass="DropDownList" OnSelectedIndexChanged="ddlsmselectedindexchanged" AutoPostBack="true" >
                                                <asp:ListItem Text="--Select--" Value="--Select--" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                       </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="rfvsm" runat="server" ControlToValidate="ddlsm"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Select SM"
                                                    ValidationGroup="CheckData" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="vcesm" WarningIconImageUrl="../Images/Warning1.jpg"
                                                    runat="server" TargetControlID="rfvsm" CssClass="CustomValidator">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                    </tr>
                      </table>
                      </contenttemplate>
                      </asp:UpdatePanel>
                     </td>
                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="rcd-tableCellCenterAlign" colspan="4">
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="CheckData" Text="Submit"
                                    SkinID="buttonSkin" OnClick="btnSubmit_Click" Visible="false" />
                                <asp:Button ID="btnReopen" runat="server" Text="Reopen" ValidationGroup="CheckData1"
                                    SkinID="buttonSkin" OnClick="btnReopen_Click" />
                                <%--OnClientClick="showConfirm(this); return false;"--%>
                                <asp:Button ID="btnOK" runat="server" Text="Cancel" SkinID="buttonSkin" OnClick="btnOK_Click"
                                    Width="60px" /> 
                                <cc1:ModalPopupExtender ID="md1" runat="server" BackgroundCssClass="modalBackground"
                                    BehaviorID="mdlPopup" CancelControlID="btnNo" OkControlID="btnOk2" OnCancelScript="cancelClick();"
                                    OnOkScript="okClick();" PopupControlID="div" TargetControlID="div">
                                </cc1:ModalPopupExtender>
                                <div id="div" runat="server" align="center" class="confirm" style="display: none">
                                    Are you sure you want to Reopen ?
                                    <asp:ImageButton ID="btnOk2" runat="server" ImageUrl="~/Images/smallsuccess.gif"
                                        ToolTip="Yes" Width="22px" Height="22px" CommandName="btnReopen_Click" />
                                    <asp:ImageButton ID="btnNo" runat="server" ImageUrl="~/Images/Delete.gif" Width="22px"
                                        Height="22px" ToolTip="No" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    </div>
                </td>
            </tr>
        </table>
        <%--</asp:Content>--%>
    </form>
</body>
</html>
