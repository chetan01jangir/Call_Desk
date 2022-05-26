<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddAIRMComment.aspx.cs" Theme="SkinFile" Inherits="Admin_AddAIRMComment"
    Title=":: Call Desk - Add Application Issue / Request Comments ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                       
                        <td class="rcd-TopHeaderBlue" style="height: 22px">
                            Add Comment for Application Issue / Request Mapping
                        </td>
                        
                    </tr>
                </table>
                <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Applications</td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlApplications" runat="server" SkinID="dropdownSkin" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlApplications_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvApplications" InitialValue="0" runat="server"
                                ControlToValidate="ddlApplications" ValidationGroup="CheckData" SetFocusOnError="true"
                                Display="None" ErrorMessage="Select application type" />
                            <cc1:ValidatorCalloutExtender ID="vceApplications" TargetControlID="rfvApplications"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Mapped Issue / Request</td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlIssueRequestTypes" runat="server" SkinID="dropdownSkin">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvIssueRequestTypes" InitialValue="0" runat="server"
                                ControlToValidate="ddlIssueRequestTypes" ValidationGroup="CheckData" SetFocusOnError="true"
                                Display="None" ErrorMessage="Select issue / request type" />
                            <cc1:ValidatorCalloutExtender ID="vceIssueRequestTypes" TargetControlID="rfvIssueRequestTypes"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Add Comments</td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvComment" runat="server"
                                ControlToValidate="txtComment" ValidationGroup="CheckData" SetFocusOnError="true"
                                Display="None" ErrorMessage="Select issue / request type" />
                            <cc1:ValidatorCalloutExtender ID="vceComment" TargetControlID="rfvComment"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnAddComment" ValidationGroup="CheckData" runat="server" Text="Add Comments"
                                SkinID="buttonSkin" OnClick="btnAddComment_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" SkinID="buttonSkin" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
