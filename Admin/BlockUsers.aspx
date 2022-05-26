<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="BlockUsers.aspx.cs" Inherits="Admin_BlockUser" Theme="SkinFile" Title=":: Call Desk - Block User ::" %>

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
                            Block User
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
                                        Block User
                                    </td>
                                    <td class="rcd-tableCell" style="width: 60%">
                                        <asp:TextBox ID="txtUserName" runat="server" SkinID="textboxSkin"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" ValidationGroup="CheckData" SkinID="buttonSkin"
                                            Text="Search" OnClick="btnSearch_Click" />
                                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter User Name">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceUserName" TargetControlID="rfvUserName" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                        <cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtUserName"
                                            WatermarkText="Type User Name Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:GridView ID="gvBlockUser" runat="server" SkinID="gridviewSkin" AllowPaging="false"
                                            PageSize="10" AutoGenerateColumns="false" OnRowCommand="gvBlockUser_RowCommand"
                                            OnRowDataBound="gvBlockUser_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="User Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserCode" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserStatus" runat="server" Text='<%# Bind("IsApproved") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Block">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBlock" runat="server" Text="Block" CommandName="BlockUser">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UnBlock">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkUnBlock" runat="server" Text="UnBlock" CommandName="UnBlockUser">
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
