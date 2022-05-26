<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="SwapBranch.aspx.cs" Theme="SkinFile" Inherits="Swap_SwapBranch" Title=":: Call Desk - Switch Branch ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <table border="0" cellpadding="1" cellspacing="1" width="100%">
        <tr>
            <td colspan="4">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class=" rcd-TopHeaderBlue">
                            Switch Branch
                        </td>
                    </tr>
                </table>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblerror" runat="server" SkinID="SkinLabel"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" cellpadding="1" cellspacing="1" class=" rcd-TableBorder">
                    <tr>
                        <td width="40%" class=" rcd-FieldTitle">
                            Branch Name
                        </td>
                        <td width="60%" class=" rcd-tableCell">
                            <asp:DropDownList ID="ddlBranch" SkinID="dropdownSkin" ValidationGroup="CheckData" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" InitialValue="0"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Select Branch" />
                            <cc1:ValidatorCalloutExtender ID="vceBranch" TargetControlID="rfvBranch"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnSave" runat="server" Text="Save Branch" OnClick="btnSave_Click" ValidationGroup="CheckData"
                                SkinID="buttonSkin" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
