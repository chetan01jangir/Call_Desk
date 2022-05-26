<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile" AutoEventWireup="true" CodeFile="UpdateIssueRequestSubType.aspx.cs" Inherits="Admin_UpdateIssueRequestSubType" 
Title=":: Call Desk - Update Issue Request SubType ::"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:HiddenField ID="antiforgery" runat="server"/>    
<table style="width: 100%;" border="0" cellpadding="1" cellspacing="1" class="ricd-TableBorder">
        <tr>
            <td colspan="2">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue">
                            Update Issue Request SubType
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
                            Issue Request SubType
                        </td>
                        <td class="rcd-tableCell" width="60%">
                            <asp:TextBox ID="txtIssueRequestSubType" SkinID="multilinetextboxSkin" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRequestSubType" runat="server" ControlToValidate="txtIssueRequestSubType"
                             Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Sub Request Type" ValidationGroup="CheckData">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceApplicationName" TargetControlID="rfvRequestSubType"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnUpdateIssueRequestSubType" SkinID="buttonSkin" ValidationGroup="CheckData"
                                runat="server" Text="Update" OnClick="btnUpdateIssueRequestSubType_Click"/>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                SkinID="buttonSkin" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-tableCellCenterAlign" colspan="2">
                            <asp:HiddenField ID="hfIssueRequestSubTypeID" runat="server" />
                            <asp:HiddenField ID="hfIssueRequestSubType" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>

