<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    Theme="SkinFile" CodeFile="UpdateApplication.aspx.cs" Inherits="UpdateIssueRequestMaster"
    Title=":: Call Desk - Update Application ::" %>

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
                        
                        <td class="rcd-TopHeaderBlue">
                            Update Application Details
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
                            Update Application Name</td>
                        <td class="rcd-tableCell" width="60%">
                            <asp:TextBox ID="txtApplicationName" EnableTheming="true" SkinID="textboxSkin" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvApplicationName" runat="server" ControlToValidate="txtApplicationName"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Enter Application Name" />
                            <cc1:ValidatorCalloutExtender ID="vceApplicationName" TargetControlID="rfvApplicationName"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnUpdate" runat="server" ValidationGroup="CheckData" SkinID="buttonSkin"
                                Text="Update" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnCancel" runat="server" SkinID="buttonSkin" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-tableCellCenterAlign" colspan="2">
                            <asp:HiddenField ID="hfApplicationID" runat="server" />
                            <asp:HiddenField ID="hfApplicationName" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
