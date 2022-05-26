<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="UpdateIssueRequestType.aspx.cs" Theme="SkinFile" Inherits="UpdateIssueRequestMaster"
    Title=":: Call Desk - Update Issue Request ::" %>
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
                            Update Issue Request Type
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
                            Issue Request Type
                        </td>
                        <td class="rcd-tableCell" width="60%">
                            <asp:TextBox ID="txtIssueRequestType" runat="server" SkinID="multilinetextboxSkin" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUpdateApplication" runat="server" ControlToValidate="txtIssueRequestType"
                             ValidationGroup="CheckData" SetFocusOnError="true" ErrorMessage="Please Enter Request Type" Display="None">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceApplicationName" TargetControlID="rfvUpdateApplication"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnUpdateIssueRequest" SkinID="buttonSkin" ValidationGroup="CheckData"
                                runat="server" Text="Update" OnClick="btnUpdateIssueRequest_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                SkinID="buttonSkin" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-tableCellCenterAlign" colspan="2">
                            <asp:HiddenField ID="hfIssueRequestTypeID" runat="server" />
                            <asp:HiddenField ID="hfIssueRequestType" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
