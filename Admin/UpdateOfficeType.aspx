<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile" 
Title=":: Call Desk - Update Office Type ::" AutoEventWireup="true" CodeFile="UpdateOfficeType.aspx.cs" Inherits="Admin_UpdateOfficeType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
 <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        
                        <td class="rcd-TopHeaderBlue">
                            Update Office Type
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
                            Update Office Type</td>
                        <td class="rcd-tableCell" width="60%">
                            <asp:TextBox ID="txtOfficeType" SkinID="textboxSkin" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvOfficeType" runat="server" ControlToValidate="txtOfficeType"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Enter Application Name" />
                            <cc1:ValidatorCalloutExtender ID="vceApplicationName" TargetControlID="rfvOfficeType"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnUpdate" runat="server" ValidationGroup="CheckData" SkinID="buttonSkin"
                                Text="Update" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnCancel" runat="server" SkinID="buttonSkin" Text="Cancel" OnClick="btnCancel_Click"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-tableCellCenterAlign" colspan="2">
                            <asp:HiddenField ID="hfOfficeTypeID" runat="server" />
                            <asp:HiddenField ID="hfOfficeType" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

