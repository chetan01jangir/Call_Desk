<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManageSettingConfig.aspx.cs" Theme="SkinFile" Inherits="Admin_ManageSettingConfig"
    Title=":: Call Desk - Manage Setting Config file ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue" style="height: 22px">
                            Manage Setting Config File
                        </td>
                    </tr>
                </table>
                <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
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
                    <%--<tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Key
                        </td>
                        <td class="rcd-tableCell" width="60%">
                            <asp:DropDownList ID="ddlKey" SkinID="dropdownSkin" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlKey_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            key (Alphanumeric value without space)
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtKey" ReadOnly="true" runat="server" SkinID="multilinetextboxSkin" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtKey" runat="server" ControlToValidate="txtKey"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Please enter key" />
                            <cc1:ValidatorCalloutExtender ID="vcetxtKey" TargetControlID="rfvtxtKey" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revtxtKey" runat="server" ControlToValidate="txtKey"
                                Display="None" ErrorMessage="Invalid Input" ValidationExpression="^[0-9a-zA-Z]+$"
                                ValidationGroup="CheckData"></asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="vcetxtKey1" TargetControlID="revtxtKey"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Value
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtValue" runat="server" SkinID="multilinetextboxSkin" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtValue" runat="server" ControlToValidate="txtValue"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Please enter key" />
                            <cc1:ValidatorCalloutExtender ID="vcetxtValue" TargetControlID="rfvtxtValue" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revtxtValue" runat="server" ControlToValidate="txtValue"
                                Display="None" ErrorMessage="Invalid Input" ValidationExpression="^[0-9a-zA-Z]+$"
                                ValidationGroup="CheckData"></asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="vcetxtValue2" TargetControlID="revtxtValue"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnSave" runat="server" ValidationGroup="CheckData" Text="Save" SkinID="buttonSkin" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" SkinID="buttonSkin" Text="Cancel" OnClick="btnCancel_Click"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
