<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    Theme="SkinFile" CodeFile="AddAIRSTMComment.aspx.cs" Inherits="Admin_AddAIRSTMComment"
    EnableEventValidation="false" Title=":: Call Desk - Application Issue Request Sub Type Comment ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue">
                            Application Issue Request Sub Type Comment
                        </td>
                    </tr>
                </table>
                <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
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
                            Application Type
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlApplicationType" runat="server" SkinID="dropdownSkin">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvApplicationType" runat="server" ControlToValidate="ddlApplicationType"
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please select the Application type">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceApplicationType" TargetControlID="rfvApplicationType"
                                            runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <cc1:CascadingDropDown ID="ccdApplications" Category="category" TargetControlID="ddlApplicationType"
                                            PromptText="Select Application" LoadingText="Loading Text..." ServicePath="../WebServices/WebService.asmx"
                                            ServiceMethod="GetApplicationTypes" runat="server">
                                        </cc1:CascadingDropDown>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Issue / Request Type
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlIssueRequestType" runat="server" SkinID="dropdownSkin">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvIssueRequestType" runat="server" ControlToValidate="ddlIssueRequestType"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Select issue request type"
                                            ValidationGroup="CheckData"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceIssueRequestType" WarningIconImageUrl="../Images/Warning1.jpg"
                                            runat="server" TargetControlID="rfvIssueRequestType">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <cc1:CascadingDropDown ID="cdIssueRequestType" Category="IssueRequestType" TargetControlID="ddlIssueRequestType"
                                            PromptText="Select Issue Request" ParentControlID="ddlApplicationType" LoadingText="Loading Text..."
                                            ServicePath="../WebServices/WebService.asmx" ServiceMethod="GetTypeofIssueRequest"
                                            runat="server">
                                        </cc1:CascadingDropDown>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Issue / Request Sub Type
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlIssueRequestSubType" runat="server" SkinID="dropdownSkin">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvIssueRequestSubType" runat="server" ControlToValidate="ddlIssueRequestSubType"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Select issue request sub type"
                                            ValidationGroup="CheckData">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceIssueRequestSubType" WarningIconImageUrl="../Images/Warning1.jpg"
                                            runat="server" TargetControlID="rfvIssueRequestSubType">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <cc1:CascadingDropDown ID="ccdIssueRequestSubType" Category="IssueRequestSubType" TargetControlID="ddlIssueRequestSubType"
                                            PromptText="Select Issue Request Sub Type" ParentControlID="ddlIssueRequestType"
                                            LoadingText="Loading Text..." ServicePath="../WebServices/WebService.asmx" ServiceMethod="GetTypeofIssueRequestSubTypeNullDescription"
                                            runat="server">
                                        </cc1:CascadingDropDown>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Purpose</td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" SkinID="multilinetextboxSkin">
                                        </asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtComment"
                                            WatermarkText="Type Purpose Here" WatermarkCssClass="rcd-multilinetxtboxvaluewatermark" />
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment"
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter the Comment">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" WarningIconImageUrl="../Images/Warning1.jpg"
                                            runat="server" TargetControlID="rfvComment">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnSubmit" runat="server" SkinID="buttonSkin" ValidationGroup="CheckData"
                                Text="Submit" OnClick="btnSubmit_Click" />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
