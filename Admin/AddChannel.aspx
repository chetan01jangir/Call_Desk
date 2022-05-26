<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddChannel.aspx.cs" Inherits="Admin_AddChannel" Theme="SkinFile" Title=":: Call Desk -Add New Channel ::" %>

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
                            Add Channel
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
                                    <td class="rcd-FieldTitle" style="width:40%">
                                        Add Channel
                                    </td>
                                    <td class="rcd-tableCell" style="width:60%">
                                        <asp:TextBox ID="txtChannel" runat="server" SkinID="textboxSkin"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvChannel" runat="server" ControlToValidate="txtChannel"
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please Enter the Channel">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceChannel" TargetControlID="rfvChannel" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:Button ID="btnSubmit" runat="server" SkinID="buttonSkin" Text="Submit" ValidationGroup="CheckData" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" runat="server" SkinID="buttonSkin" Text="Cancel" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                                <tr class="rcd-TableHeader">
                                    <td colspan="4">
                                        Channel
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:GridView ID="gvChannel" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="false" OnRowCommand="gvChannel_RowCommand" AllowPaging="True" OnPageIndexChanging="gvChannel_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ChannelID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChannelID" runat="server"  Text='<%# Bind("ChannelID_PK") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Channel">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChannel" runat="server" Text='<%# Bind("ChannelName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandName="EditChannel"
                                                            CommandArgument='<%# Bind("ChannelID_PK") %>' Text="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" OnClientClick="if(confirm('Are You Sure Want To Delete This Record ?')){return true;}else {return false;};javascript:__doPostBack('ctl00$cphMain$gvDesignation','Delete$9')"
                                                            CommandName="DeleteChannel" CommandArgument='<%# Bind("ChannelID_PK") %>' Text="Delete"></asp:LinkButton>
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
