<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="UpdateBranch.aspx.cs" Inherits="Admin_UpdateBranch" Theme="SkinFile"
    Title=":: Call Desk - Update Branch ::" %>

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
                            Update Branch
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
                            Branch Name
                        </td>
                        <td class="rcd-tableCell" width="60%">
                            <asp:TextBox ID="txtBranchName" runat="server" SkinID="textboxSkin"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="txtBranchName"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter Branch Name">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceBranch" TargetControlID="rfvBranch" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" width="40%">
                            Office Type
                        </td>
                        <td class="rcd-tableCell" width="60%">
                            <asp:DropDownList ID="ddlOfficeType" runat="server" SkinID="dropdownSkin">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvOfficeType" runat="server" InitialValue="0" ControlToValidate="ddlOfficeType"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please select Office Type">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceOfficeType" TargetControlID="rfvOfficeType"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" SkinID="buttonSkin" ValidationGroup="CheckData"
                                OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" SkinID="buttonSkin" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-tableCellCenterAlign" colspan="2">
                            <asp:HiddenField ID="hfBranchID" runat="server" />
                            <asp:HiddenField ID="hfBranchCode" runat="server" />
                            <asp:HiddenField ID="hfBranchName" runat="server" />
                            <asp:HiddenField ID="hfOfficeType" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
