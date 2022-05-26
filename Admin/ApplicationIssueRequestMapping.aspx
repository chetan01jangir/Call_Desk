<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile"
    AutoEventWireup="true" CodeFile="ApplicationIssueRequestMapping.aspx.cs" Inherits="Admin_ApplicationIssueRequestMapping"
    Title=":: Call Desk - Application Issue / Request Mapping::" EnableEventValidation="false" %>

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
    
    function checkDate(sender,args)
    {        
        if (sender._selectedDate < new Date()) 
        {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date(); 
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        }
    }
            
    </script>

    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue">
                            Application Issue / Request Mapping
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
                <asp:Label ID="lblMessage" SkinID="SkinLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                    <tr>
                        <td class="rcd-FieldTitle" width="40%">
                            Applications</td>
                        <td class="rcd-tableCell" width="60%">
                            <asp:DropDownList ID="ddlApplications" runat="server" AutoPostBack="true" SkinID="dropdownSkin"
                                OnSelectedIndexChanged="ddlApplications_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvApplications" InitialValue="0" runat="server"
                                ControlToValidate="ddlApplications" ValidationGroup="CheckData" SetFocusOnError="true"
                                Display="None" ErrorMessage="Select application type" />
                            <cc1:ValidatorCalloutExtender ID="vceApplications" TargetControlID="rfvApplications"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Issue / Request Types</td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table width="100%">
                                <tr>
                                    <td style="width: 45%" align="right">
                                        <asp:ListBox ID="lstbxIssueRequest" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                    </td>
                                    <td style="width: 10%">
                                        <table width="100%">
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnAdd" runat="server" SkinID="buttonSkin" Text=">" OnClick="btnAdd_Click"
                                                        Width="25px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnRemove" runat="server" SkinID="buttonSkin" Text="<" OnClick="btnRemove_Click"
                                                        Width="25px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:ListBox ID="lstbxSelectedIssueRequest" runat="server" SelectionMode="Multiple">
                                        </asp:ListBox>
                                        <asp:RequiredFieldValidator ID="rfvSelectedIR" runat="server" ControlToValidate="lstbxSelectedIssueRequest"
                                            ErrorMessage="Select atleast one issue request" InitialValue="" Font-Bold="True"
                                            Font-Size="Medium" ValidationGroup="CheckData" Display="None" />
                                        <cc1:ValidatorCalloutExtender ID="vceSelectedIR" TargetControlID="rfvSelectedIR"
                                            runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Valid From
                        </td>
                        <td class="rcd-tableCell" width="60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtValidFrom" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 26px; height: 42px">
                                        <img alt="Icon" style="cursor: hand" src="../Images/Calander_New.jpg" id="imgFromDate" />
                                    </td>
                                    <td>
                                        <cc1:CalendarExtender ID="ceFromDate" OnClientDateSelectionChanged="checkDate" Format="dd/MM/yyyy"
                                            TargetControlID="txtValidFrom" PopupButtonID="imgFromDate" runat="server">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtValidFrom"
                                            ErrorMessage="Select valid from date" Font-Bold="True" Font-Size="Medium" ValidationGroup="CheckData"
                                            Display="None" />
                                    </td>
                                    <td>
                                        <cc1:ValidatorCalloutExtender ID="vceFromDate" TargetControlID="rfvFromDate" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Valid To
                        </td>
                        <td class="rcd-tableCell" width="60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtValidTo" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img alt="Icon" style="cursor: hand" src="../Images/Calander_New.jpg" id="imgToDate" />
                                    </td>
                                    <td>
                                        <cc1:CalendarExtender ID="ceToDate" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate"
                                            TargetControlID="txtValidTo" PopupButtonID="imgToDate" runat="server">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ControlToValidate="txtValidTo"
                                            ErrorMessage="Select valid till date" Font-Bold="True" Font-Size="Medium" ValidationGroup="CheckData"
                                            Display="None" />
                                    </td>
                                    <td>
                                        <cc1:ValidatorCalloutExtender ID="vceToDate" TargetControlID="rfvValidTo" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnCreateMapping" ValidationGroup="CheckData" SkinID="buttonSkin"
                                runat="server" Text="Create Mapping" OnClick="btnCreateMapping_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Clear" SkinID="buttonSkin" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-tableCellCenterAlign" colspan="2">
                            <asp:GridView ID="gvMappedApplicationIssueRequest" runat="server" AutoGenerateColumns="false"
                                SkinID="gridviewSkin" AllowPaging="True" OnPageIndexChanging="gvMappedApplicationIssueRequest_PageIndexChanging"
                                OnRowCommand="gvMappedApplicationIssueRequest_RowCommand" OnRowDataBound="gvMappedApplicationIssueRequest_RowDataBound"
                                ShowFooter="true">
                                <Columns>
                                    <%--<asp:BoundField DataField="ApplicationName" HeaderText="Application Name" ItemStyle-HorizontalAlign="Left" />--%>
                                                                        
                                    <asp:TemplateField HeaderText="Application Name" FooterStyle-ForeColor="Black" FooterStyle-Font-Bold="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApplicationName" runat="server" ToolTip='<%# Bind("ApplicationName") %>'
                                                Text='<%# Bind("ApplicationName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlApplicationName" runat="server" SkinID="dropdownSkin" Style="z-index: 999;">
                                            </asp:DropDownList>
                                            <cc1:CascadingDropDown ID="ccdApplication" Category="category" TargetControlID="ddlApplicationName"
                                                PromptText="Select Application" LoadingText="Loading Text..." ServicePath="../WebServices/WebService.asmx"
                                                ServiceMethod="GetApplicationTypes" runat="server">
                                            </cc1:CascadingDropDown>
                                            <asp:RequiredFieldValidator ID="rfvApplicationName1" runat="server" ControlToValidate="ddlApplicationName"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please select application"
                                                ValidationGroup="CheckData1">
                                            </asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceApplicationName1" TargetControlID="rfvApplicationName1"
                                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg" CssClass="CustomValidator">
                                            </cc1:ValidatorCalloutExtender>
                                            <asp:Button runat="server" ID="btnSort" CommandArgument='<%# Bind("RowID") %>' ValidationGroup="CheckData1" Text="Filter"
                                                CommandName="btnSortApplications" SkinID="buttonSkin" />
                                        </FooterTemplate>
                                        <HeaderStyle Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IssueRequestType" HeaderText="Issue Request Type" ItemStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField HeaderText="Edit" Visible="false">
                                        <ItemTemplate>                                            
                                            <asp:ImageButton ID="lnkbtnEdit" runat="server" CommandArgument='<%# Bind("RowID") %>'
                                                CausesValidation="false" CommandName="lnkEdit" ImageUrl="~/Images/edit3.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>                                            
                                            <asp:ImageButton ID="lnkbtnDelete" runat="server" CommandArgument='<%# Bind("RowID") %>'
                                                OnClientClick="showConfirm(this); return false;" CausesValidation="false" CommandName="lnkDelete"
                                                ImageUrl="~/Images/delete.jpg" ToolTip="Delete" />
                                            <cc1:ModalPopupExtender ID="md1" runat="server" BackgroundCssClass="modalBackground"
                                                BehaviorID="mdlPopup" CancelControlID="btnNo" OkControlID="btnOk" OnCancelScript="cancelClick();"
                                                OnOkScript="okClick();" PopupControlID="div" TargetControlID="div">
                                            </cc1:ModalPopupExtender>
                                            <div id="div" runat="server" class="confirm" style="display: none; text-align: center">
                                                Are you sure you want to delete ?
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
