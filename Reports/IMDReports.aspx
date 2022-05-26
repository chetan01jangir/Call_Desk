<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IMDReports.aspx.cs" Inherits="Reports_IMDReports"  MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile" Title=":: Call Desk - IMD Reports ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<asp:Panel ID="pnlNoAutho" runat="server">
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue" style="height: 22px">
                           IMD Reports
                        </td>
                    </tr>
                </table>                
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
              <asp:Label ID="Label1" runat="server" Text="You are not authorised to view this page." SkinID="SkinLabel"></asp:Label>        
            </td>        
        </tr>    
    </table>   
   </asp:Panel>


<asp:Panel ID="pnlAutho" runat="server">
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue" style="height: 22px">
                           IMD Reports
                        </td>
                    </tr>
                </table>
                <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
                <asp:UpdatePanel id="UpdatePanel1" runat="server">
                    <contenttemplate>
                        <asp:Label ID="lblMessage" runat="server" SkinID="SkinLabel"></asp:Label>
                    </contenttemplate>
                </asp:UpdatePanel>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                   <%-- <tr id="trRGON" runat="server">
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Reports Generate On
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlReportsOn" runat="server" SkinID="dropdownSkin" Width="156px" AutoPostBack="True">
                                <asp:ListItem>Region</asp:ListItem>
                                <asp:ListItem>Branch</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvReportsOn" runat="server" ControlToValidate="ddlReportsOn"
                                ValidationGroup="CheckData" Display="None" SetFocusOnError="true" ErrorMessage="Please Select the Reports Type">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceReportsOn" TargetControlID="rfvReportsOn" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>--%>
                  <%--  <tr id="trRegion" runat="server">
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Region
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlRegion" runat="server" SkinID="dropdownSkin" Width="156px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ControlToValidate="ddlRegion"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please Select Region">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceRegion" TargetControlID="rfvRegion" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>--%>
                  <%--  <tr id="trBranch" runat="server">
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Branch
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlBranch" runat="server" SkinID="dropdownSkin" Width="156px"
                                DataTextField="BranchName" DataValueField="BranchCode">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please Select The Branch">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceBranch" TargetControlID="rfvBranch" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>--%>
                     <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Application
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                         <asp:DropDownList ID="ddlApplication" runat="server" SkinID="dropdownSkin" Width="156px"
                                DataTextField="ApplicationName" DataValueField="ApplicationID_PK" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlApplication_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvApplication" runat="server" ControlToValidate="ddlApplication"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please Select Application">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceApplication" TargetControlID="rfvApplication"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            
                        </td>
                    </tr>
                     <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Issue / Request Type
                        </td>
                        
                        <td class="rcd-tableCell" style="width: 60%">
                         <asp:UpdatePanel ID="uptpnlissue" runat="server" UpdateMode="Conditional">
                                 <contenttemplate>
                            <asp:DropDownList ID="ddlIssueRequestType" runat="server" SkinID="dropdownSkin"
                               OnSelectedIndexChanged="ddlIssueRequestType_SelectedIndexChanged" AutoPostBack="true"> 
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvIssueRequestType" runat="server" ControlToValidate="ddlIssueRequestType"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please Select Request Type">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceIssueRequestType" TargetControlID="rfvIssueRequestType"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            </contenttemplate>
                             <triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlApplication" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>                                                                                                                          
                                                                                   
                                </triggers>
                            </asp:UpdatePanel>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Issue / Request Sub Type
                        </td>
                        
                        <td class="rcd-tableCell" style="width: 60%">
                         <asp:UpdatePanel ID="uptpnlissuesubtype" runat="server" UpdateMode="Conditional">
                                 <contenttemplate>
                            <asp:DropDownList ID="ddlIssuesubRequestType" runat="server" SkinID="dropdownSkin" 
                                DataTextField="ApplicationName" DataValueField="ApplicationID_PK">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvIssueRequestsubType" runat="server" ControlToValidate="ddlIssuesubRequestType"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please Select Request Sub Type">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceIssueRequestsubType" TargetControlID="rfvIssueRequestsubType"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            </contenttemplate>
                             <triggers>
                             <asp:AsyncPostBackTrigger ControlID="ddlApplication" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>                                                  
                             <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestType" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>                                                                                                                          
                                 
                                </triggers>
                            </asp:UpdatePanel>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            From Date
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img alt="Icon" style="cursor: hand" src="../Images/Calander_New.jpg" id="imgFromDate" />
                                    </td>
                                    <td>
                                        <cc1:CalendarExtender ID="ceFromDate" Format="MM/dd/yyyy" TargetControlID="txtFromDate"
                                            PopupButtonID="imgFromDate" runat="server">
                                        </cc1:CalendarExtender>
                                        <!--OnClientDateSelectionChanged="checkDate"-->
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                            ErrorMessage="Select from date" ValidationGroup="CheckData" Display="None" />
                                        <cc1:ValidatorCalloutExtender ID="vceFromDate" TargetControlID="rfvFromDate" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            To Date
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img alt="Icon" style="cursor: hand" src="../Images/Calander_New.jpg" id="imgToDate" />
                                    </td>
                                    <td>
                                        <cc1:CalendarExtender ID="ceToDate" Format="MM/dd/yyyy" TargetControlID="txtToDate"
                                            PopupButtonID="imgToDate" runat="server">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvtxtToDate" runat="server" ControlToValidate="txtToDate"
                                            ErrorMessage="Select to date" Font-Bold="True" Font-Size="Medium" ValidationGroup="CheckData"
                                            Display="None" />
                                        <cc1:ValidatorCalloutExtender ID="vceToDate" TargetControlID="rfvtxtToDate" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <asp:CompareValidator ID="cmpvToDate" runat="server" ControlToValidate="txtToDate"
                                            ControlToCompare="txtFromDate" ErrorMessage="To date should be greater than from date"
                                            Operator="GreaterThanEqual" Type="Date" Display="None" ValidationGroup="CheckData">
                                        </asp:CompareValidator>
                                        <cc1:ValidatorCalloutExtender ID="vcecmpvToDate" TargetControlID="cmpvToDate" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="CheckData"
                                SkinID="buttonSkin" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="buttonSkin" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </asp:Panel>
    
</asp:Content>
