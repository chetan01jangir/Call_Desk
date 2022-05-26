<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddTemplateDocuments.aspx.cs" Inherits="Admin_AddTemplateDocuments"
    Title=":: Call Desk - Add Application Template ::" Theme="SkinFile" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <script type="text/javascript">
         var _source;
         var _popup;
         function showConfirm(source, mdlPopup)
         {
             this._source = source;
             this._popup = $find(mdlPopup);
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

    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue">
                            Add Application Template
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
                <asp:Label ID="lblMessage" runat="server" SkinID="SkinLabel"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                                        <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Application Type
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlApplicationType" runat="server" SkinID="dropdownSkin">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvApplicationType" runat="server" ControlToValidate="ddlApplicationType"
                                            ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Select application type" />
                                        <cc1:ValidatorCalloutExtender ID="vceApplicationType" TargetControlID="rfvApplicationType"
                                            runat="server" WarningIconImageUrl="../Images/Warning1.jpg" CssClass="CustomValidator">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <cc1:CascadingDropDown ID="ccdApplication" Category="category" TargetControlID="ddlApplicationType"
                                            PromptText="Select Application" LoadingText="Loading Text..." ServicePath="../WebServices/WebService.asmx"
                                            ServiceMethod="GetApplicationTypes" runat="server">
                                        </cc1:CascadingDropDown>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Issue / Request Type
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlIssueRequestType" runat="server" SkinID="dropdownSkin">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvIssueRequestType" runat="server" ControlToValidate="ddlIssueRequestType"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Select issue request type"
                                            ValidationGroup="CheckData"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceIssueRequestType" WarningIconImageUrl="../Images/Warning1.jpg"
                                            runat="server" TargetControlID="rfvIssueRequestType" CssClass="CustomValidator">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <cc1:CascadingDropDown ID="ccdIssueRequestType" Category="IssueRequestType" TargetControlID="ddlIssueRequestType"
                                            PromptText="Select Issue Request" ParentControlID="ddlApplicationType" LoadingText="Loading Text..."
                                            ServicePath="../WebServices/WebService.asmx" ServiceMethod="GetTypeofIssueRequest"
                                            runat="server">
                                        </cc1:CascadingDropDown>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="height: 18px; width: 40%">
                            Issue / Request Sub Type
                        </td>
                        <td class="rcd-tableCell" style="height: 18px; width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlIssueRequestSubType" runat="server"
                                            SkinID="dropdownSkin">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvIssueRequestSubType" runat="server" ControlToValidate="ddlIssueRequestSubType"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Select issue request sub type"
                                            ValidationGroup="CheckData">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceIssueRequestSubType" WarningIconImageUrl="../Images/Warning1.jpg"
                                            runat="server" TargetControlID="rfvIssueRequestSubType" CssClass="CustomValidator">
                                        </cc1:ValidatorCalloutExtender>
                                        <cc1:CascadingDropDown ID="ccdIssueRequestSubType" Category="IssueRequestSubType"
                                            TargetControlID="ddlIssueRequestSubType" PromptText="Select Issue Request Sub Type"
                                            ParentControlID="ddlIssueRequestType" LoadingText="Loading Text..." ServicePath="../WebServices/WebService.asmx"
                                            ServiceMethod="GetTypeofIssueRequestSubTypeForFileTemplate" runat="server">
                                        </cc1:CascadingDropDown>
                                    </td>                                                                       
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Select Document
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:FileUpload ID="fuUpLoadFile" runat="server" SkinID="FileUploaderSkin" />
                            <asp:RequiredFieldValidator ID="rfvUpLoadFile" runat="server" ControlToValidate="fuUpLoadFile"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Please select File." />
                            <cc1:ValidatorCalloutExtender ID="vceUpLoadFile" TargetControlID="rfvUpLoadFile"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revFileUpload" runat="server" ControlToValidate="fuUpLoadFile"
                                Display="None" SetFocusOnError="true" ErrorMessage="File Format not Supported"
                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.doc|.DOC|.docx|.DOCX|.xls|.xlsx|.XLSX|.XLS|.txt|.TXT|.jpeg|.JPEG)$">
                            </asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="vceFileUpload" WarningIconImageUrl="../Images/Warning1.jpg"
                                TargetControlID="revFileUpload" runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="CheckData" SkinID="buttonSkin"
                                Text="Submit" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" runat="server" SkinID="buttonSkin" Text="Cancel" />
                        </td>
                    </tr>
                    <tr class="rcd-TableHeader">
                        <td colspan="4">
                            Applications</td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:GridView ID="gvApplicationFileMapping" SkinID="gridviewSkin" runat="server"
                                AutoGenerateColumns="false" AllowPaging="True" OnRowCommand="gvApplicationFileMapping_RowCommand" OnRowDataBound="gvApplicationFileMapping_RowDataBound" OnPageIndexChanging="gvApplicationFileMapping_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="RowID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowID" runat="server" Text='<%# Bind("RowId") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Application Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApplicationName" runat="server" Text='<%# Bind("ApplicationName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Request Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssueRequestType" runat="server" Text='<%# Bind("IssueRequestType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Request Sub Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssueRequestSubType" runat="server" Text='<%# Bind("IssueRequestSubType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Template">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileTemplateName" runat="server" Text='<%# Bind("FileTemplateName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("RowId") %>'
                                                CausesValidation="false" CommandName="EditRow" ImageUrl="~/Images/edit3.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("RowId") %>'
                                                OnClientClick="showConfirm(this); return false;" CausesValidation="false" CommandName="DeleteRow"
                                                ImageUrl="~/Images/delete.jpg" ToolTip="Delete" />
                                            <cc1:ModalPopupExtender ID="md1" runat="server" BackgroundCssClass="modalBackground"
                                                BehaviorID="mdlPopup" CancelControlID="btnNo" OkControlID="btnOk" OnCancelScript="cancelClick();"
                                                OnOkScript="okClick();" PopupControlID="div" TargetControlID="div">
                                            </cc1:ModalPopupExtender>
                                            <div id="div" runat="server" class="confirm" style="display: none; text-align: center">
                                                Are you sure you want to delete mapping?
                                                <asp:ImageButton ID="btnOk" runat="server" ImageUrl="~/Images/smallsuccess.gif" ToolTip="Yes"
                                                    Width="22px" Height="22px" />
                                                <asp:ImageButton ID="btnNo" runat="server" ImageUrl="~/Images/Delete.gif" Width="22px"
                                                    Height="22px" ToolTip="No" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
