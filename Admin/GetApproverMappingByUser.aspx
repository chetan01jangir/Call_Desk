<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    Theme="SkinFile" CodeFile="GetApproverMappingByUser.aspx.cs" Inherits="Admin_GetApproverMappingByUser"
    Title=":: Call Desk - Manage Approver By User Name ::" %>

<%@ Register Src="../UserControls/wucSearchmoreUserMapping.ascx" TagName="wucSearchmoreUserMapping"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue" style="height: 22px">
                            Application Issue Request SubType Approver Mapping By User
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
                                        Approver
                                    </td>
                                    <td class="rcd-tableCell" style="width: 60%">
                                        <asp:TextBox ID="txtUserId" runat="server" SkinID="textboxSkin">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUserId" runat="server" ControlToValidate="txtUserId"
                                            ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Please enter Approver" />
                                        <cc1:ValidatorCalloutExtender ID="vceUserId" TargetControlID="rfvUserId" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg" CssClass="CustomValidator">
                                        </cc1:ValidatorCalloutExtender>
                                        <uc1:wucSearchmoreUserMapping ID="WucSearchmoreUserMapping1" runat="server" />
                                    </td>
                                    <td>
                                        <cc1:AutoCompleteExtender ID="ace" runat="server" MinimumPrefixLength="1" ServiceMethod="GetEmpLoyeeNames"
                                            ServicePath="../WebServices/WebService.asmx" TargetControlID="txtUserId">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:Button ID="btnGetApprover" runat="server" Text="Get Approver Mapping" SkinID="buttonSkin"
                                            ValidationGroup="CheckData" OnClick="btnGetApprover_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:GridView ID="gvMappedApprover" AutoGenerateColumns="false" runat="server" SkinID="gridviewSkin"
                                            OnRowCommand="gvMappedApprover_RowCommand" OnRowCreated="gvMappedApprover_RowCreated"
                                            ShowFooter="true">
                                            <Columns>
                                                <asp:BoundField DataField="ApplicationName" HeaderText="Application" />
                                                <asp:BoundField DataField="IssueRequestType" HeaderText="Issue Request" />
                                                <asp:BoundField DataField="IssueRequestSubType" HeaderText="Issue Request SubType" />
                                                <asp:TemplateField HeaderText="First Approver">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFirstApprover" Font-Underline="true" runat="server" Text='<%# Bind("FApproverID") %>'></asp:Label>
                                                        <cc1:PopupControlExtender ID="popCtrlExtAddPopUp1" runat="server" DynamicServiceMethod="GetApproverDetails"
                                                            DynamicContextKey='<%# Eval("FApproverID") %>' DynamicControlID="pnlAddPopUP"
                                                            TargetControlID="lblFirstApprover" PopupControlID="pnlAddPopUP" Position="Bottom">
                                                        </cc1:PopupControlExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Second Approver">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSecondApprover" Font-Underline="true" runat="server" Text='<%# Bind("SApproverID") %>'></asp:Label>
                                                        <cc1:PopupControlExtender ID="popCtrlExtAddPopUp2" runat="server" DynamicServiceMethod="GetApproverDetails"
                                                            DynamicContextKey='<%# Eval("SApproverID") %>' DynamicControlID="pnlAddPopUP"
                                                            TargetControlID="lblSecondApprover" PopupControlID="pnlAddPopUP" Position="Bottom">
                                                        </cc1:PopupControlExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                                                <asp:TemplateField HeaderText="First Approver">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtFirstApprover" SkinID="textboxSkin" Text='<%# Bind("FApproverID") %>'
                                                                        runat="server" />
                                                                </td>
                                                                <td>
                                                                    <%--<uc1:wucSearchmoreUserMapping ID="WucSearchmoreUserMapping1" runat="server" />--%>
                                                                    <asp:RequiredFieldValidator ID="rfvFirstApprover" runat="server" ControlToValidate="txtFirstApprover"
                                                                        ValidationGroup="CheckData1" SetFocusOnError="true" Display="None" ErrorMessage="Enter Approver" />
                                                                    <cc1:ValidatorCalloutExtender ID="vceFirstApprover" TargetControlID="rfvFirstApprover"
                                                                        runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                                                    </cc1:ValidatorCalloutExtender>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblReplaceApprover" runat="server" SkinID="SkinLabel" Text="Replace Approver with :"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Second Approver">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtSecondApprover" SkinID="textboxSkin" Text='<%# Bind("SApproverID") %>'
                                                                        runat="server" />
                                                                </td>
                                                                <td>
                                                                    <%--<uc1:wucSearchmoreUserMapping ID="WucSearchmoreUserMapping2" runat="server" />--%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtReplaceApprover" runat="server" SkinID="textboxSkin"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="rfvReplaceApprover" runat="server" ControlToValidate="txtReplaceApprover"
                                                                        Display="None" SetFocusOnError="true" ValidationGroup="CheckData2" ErrorMessage="Enter Approver">
                                                                    </asp:RequiredFieldValidator>
                                                                    <cc1:ValidatorCalloutExtender ID="vceReplaceApprover" runat="server" TargetControlID="rfvReplaceApprover"
                                                                        WarningIconImageUrl="../Images/Warning1.jpg">
                                                                    </cc1:ValidatorCalloutExtender>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Replace">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnReplace" runat="server" ValidationGroup="CheckData1" CommandName="btnReplace"
                                                            CommandArgument='<%# Bind("ApproverRowID") %>' Text="Replace" SkinID="buttonSkin" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnReplaceAll" runat="server" SkinID="buttonSkin" Text="Replace All"
                                                            CommandName="btnReplaceAll" ValidationGroup="CheckData2" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField>
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnReplaceAll" runat="server" SkinID="buttonSkin" Text="Replace All" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Panel ID="pnlAddPopUP" runat="server">
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>