<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    Theme="SkinFile" CodeFile="AddDesignation.aspx.cs" Inherits="Admin_AddDesignation"
    Title=":: Call Desk - Add New Designation ::" %>

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
                            Add Designation
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
                                    <td class="rcd-FieldTitle" width="40%">
                                        Add Designation
                                    </td>
                                    <td class="rcd-tableCell" width="60%">
                                        <asp:TextBox ID="txtDesignation" runat="server" SkinID="textboxSkin"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="txtDesignation"
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please Enter the Designation">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceDesignation" TargetControlID="rfvDesignation"
                                            runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:Button ID="btnSubmit" runat="server" SkinID="buttonSkin" ValidationGroup="CheckData"
                                            Text="Submit" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" runat="server" SkinID="buttonSkin" Text="Cancel" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                                <tr class="rcd-TableHeader">
                                    <td colspan="4">
                                        Designation
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:GridView ID="gvDesignation" runat="server" AutoGenerateColumns="false" SkinID="gridviewSkin" OnRowCommand="gvDesignation_RowCommand" AllowPaging="True" OnPageIndexChanging="gvDesignation_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="DesignationID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesignationID" runat="server" Text='<%# Bind("DesignationID_PK") %>'
                                                            SkinID="SkinLabel"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandName="EditDesignation"
                                                            CommandArgument='<%# Bind("DesignationID_PK") %>' Text="Edit">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField  HeaderText="Delete">
                                                <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="DeleteDesignation" OnClientClick = "if(confirm('Are You Sure Want To Delete This Record ?')){return true;}else {return false;};javascript:__doPostBack('ctl00$cphMain$gvDesignation','Delete$9')"
                                                 Text="Delete" CommandArgument='<%# Bind("DesignationID_PK") %>'>
                                                </asp:LinkButton>
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
            </td>
        </tr>
    </table>
</asp:Content>
