<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="UpdateRegion.aspx.cs" Inherits="Admin_UpdateRegion" Theme="SkinFile"
    Title=":: Call Desk - Update Region ::" %>

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
                            Update Region
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
                            Zone Name
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlZone" runat="server" SkinID="dropdownSkin">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvZone" runat="server" ControlToValidate="ddlZone"
                                Display="None" SetFocusOnError="true" ValidationGroup="" ErrorMessage="Please select Zone">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceZone" TargetControlID="rfvZone" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Region Name
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtRegion" runat="server" SkinID="textboxSkin"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ControlToValidate="txtRegion"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter Region Name">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceRegion" TargetControlID="rfvRegion" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnSubmit" runat="server" SkinID="buttonSkin" Text="Update" ValidationGroup="CheckData"
                                CausesValidation="False" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" runat="server" SkinID="buttonSkin" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-tableCellCenterAlign" colspan="2">
                            <asp:HiddenField ID="hfZoneID" runat="server" />
                            <asp:HiddenField ID="hfRegionID" runat="server" />
                            <asp:HiddenField ID="hfRegionCode" runat="server" />
                            <asp:HiddenField ID="hfRegionName" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
