<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="UpdateChannel.aspx.cs" Inherits="Admin_UpdateChannel" Theme="SkinFile"
    Title=":: Call Desk - Update Designation ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <table style="width: 100%;" border="0" cellpadding="1" cellspacing="1" class="ricd-TableBorder">
        <tr>
            <td colspan="2">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue">
                            Update Channel
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
                        <td class="rcd-FieldTitle" width="40%">
                            Update Channel
                        </td>
                        <td class="rcd-tableCell" width="60%">
                            <asp:TextBox ID="txtChannel" runat="server" SkinID="textboxSkin"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvChannel" runat="server" ControlToValidate="txtChannel"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter the Channel">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceChannel" TargetControlID="rfvChannel" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="CheckData" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-tableCellCenterAlign" colspan="2">
                            <asp:HiddenField ID="hfChannel" runat="server" />
                            <asp:HiddenField ID="hfChannelID" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
