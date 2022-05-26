<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile"
    AutoEventWireup="true" CodeFile="AddConfigSettings.aspx.cs" Inherits="Admin_AddConfigSettings"
    Title=":: Call Desk - Add Setting Config ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue" style="height: 22px">
                            Add Values to Setting Config File
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
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Key (Alphanumeric value without space)
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtKey" runat="server" SkinID="multilinetextboxSkin" TextMode="MultiLine"></asp:TextBox>
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
                            <cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtKey"
                                WatermarkText="Type Key Here" WatermarkCssClass="rcd-multilinetxtboxvaluewatermark" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Value (Alphanumeric value without space)
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtValue" runat="server" SkinID="multilinetextboxSkin" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtValue" runat="server" ControlToValidate="txtValue"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Please enter value" />
                            <cc1:ValidatorCalloutExtender ID="vcetxtValue" TargetControlID="rfvtxtValue" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revtxtValue" runat="server" ControlToValidate="txtValue"
                                Display="None" ErrorMessage="Invalid Input" ValidationExpression="^[0-9a-zA-Z]+$"
                                ValidationGroup="CheckData"></asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="vcetxtValue2" TargetControlID="revtxtValue"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtValue"
                                WatermarkText="Type Value Here" WatermarkCssClass="rcd-multilinetxtboxvaluewatermark" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="CheckData" SkinID="buttonSkin"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvConfigValues" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="false"
                    OnRowCommand="gvConfigValues_RowCommand" AllowPaging="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Key">
                            <ItemTemplate>
                                <asp:Label ID="lblKey" runat="server" Text='<%# Bind("key") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="True" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value">
                            <ItemTemplate>
                                <asp:Label ID="lblValue" runat="server" Text='<%# Bind("value") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="True" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="lnkEdit" runat="server" CommandName="EditValues" CommandArgument='<%# Bind("key") %>'
                                    ImageUrl="~/Images/edit3.jpg" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" Visible="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="lnkDelete" runat="server" CommandName="DeleteValues" CommandArgument='<%# Bind("key") %>'
                                    ImageUrl="~/Images/delete.jpg" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
