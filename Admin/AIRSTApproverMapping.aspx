<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile"
    AutoEventWireup="true" CodeFile="AIRSTApproverMapping.aspx.cs" Inherits="Admin_AIRSTApproverMapping"
    EnableEventValidation="false" Title=":: Call Desk - Approver Mapping ::" %>

<%@ Register Src="../UserControls/wucSearchmoreUserMapping.ascx" TagName="wucSearchmoreUserMapping"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <script language="javascript" type="text/javascript">
                
        function SearchCriteria()
        {
            var ddlSearch = document.getElementById('<%=ddlType.ClientID%>');            
            if(ddlSearch.value == '1')
            {                
                document.getElementById('ctl00_ContentPlaceHolder1_trBranch').style.display= 'inline';
                document.getElementById('ctl00_ContentPlaceHolder1_trRegion').style.display= 'inline';
                document.getElementById('ctl00_ContentPlaceHolder1_trZone').style.display= 'inline';
            
                document.getElementById('ctl00_ContentPlaceHolder1_rfvBranch').enabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvRegion').enabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvZone').enabled = true;
            }
            else if(ddlSearch.value == '2')
            {
                document.getElementById('ctl00_ContentPlaceHolder1_trBranch').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trRegion').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trZone').style.display= 'none';
            
                document.getElementById('ctl00_ContentPlaceHolder1_rfvBranch').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvRegion').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvZone').enabled = false;
            }
            else
            {                
                document.getElementById('ctl00_ContentPlaceHolder1_trBranch').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trRegion').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trZone').style.display= 'none';
            
                document.getElementById('ctl00_ContentPlaceHolder1_rfvBranch').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvRegion').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvZone').enabled = false;
            }            
        }
    </script>

    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue" style="height: 22px">
                            Application Issue Request SubType Approver Mapping
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
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="rfvApplicationType" runat="server" ControlToValidate="ddlApplicationType"
                                                        ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Select application type" />
                                                    <cc1:ValidatorCalloutExtender ID="vceApplicationType" TargetControlID="rfvApplicationType"
                                                        runat="server" WarningIconImageUrl="../Images/Warning1.jpg" CssClass="CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                    <cc1:CascadingDropDown ID="ccdApplication" Category="category" TargetControlID="ddlApplicationType"
                                                        PromptText="Select Application" LoadingText="Loading Text..." ServicePath="../WebServices/WebService.asmx"
                                                        ServiceMethod="GetApplicationTypes" runat="server">
                                                    </cc1:CascadingDropDown>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 40%; height: 18px;">
                                        Issue / Request Type
                                    </td>
                                    <td class="rcd-tableCell" style="width: 60%; height: 18px;">
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
                                                        runat="server" TargetControlID="rfvIssueRequestType" CssClass="CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                    <cc1:CascadingDropDown ID="ccdIssueRequestType" Category="IssueRequestType" TargetControlID="ddlIssueRequestType"
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
                                    <td class="rcd-FieldTitle" style="height: 18px; width: 40%">
                                        Issue / Request Sub Type
                                    </td>
                                    <td class="rcd-tableCell" style="height: 18px; width: 60%">
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
                                                        runat="server" TargetControlID="rfvIssueRequestSubType" CssClass="CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                    <cc1:CascadingDropDown ID="ccdIssueRequestSubType" Category="IssueRequestSubType"
                                                        TargetControlID="ddlIssueRequestSubType" PromptText="Select Issue Request Sub Type"
                                                        ParentControlID="ddlIssueRequestType" LoadingText="Loading Text..." ServicePath="../WebServices/WebService.asmx"
                                                        ServiceMethod="GetTypeofIssueRequestSubTypeForApproverMapping" runat="server">
                                                    </cc1:CascadingDropDown>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 40%; height: 18px">
                                        Branchwise or Direct</td>
                                    <td class="rcd-tableCell" style="width: 60%; height: 18px">
                                        <asp:DropDownList runat="server" ID="ddlType" SkinID="dropdownSkin">
                                            <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Branchwise" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Direct" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <asp:RequiredFieldValidator ID="rfvddlType" runat="server" ControlToValidate="ddlType"
                                        Display="None" SetFocusOnError="true" ErrorMessage="Select type" InitialValue="0"
                                        ValidationGroup="CheckData"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                                            ID="vceddlType" TargetControlID="rfvddlType" runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                </tr>
                                <tr runat="server" id="trZone">
                                    <td class="rcd-FieldTitle" style="width: 40%; height: 18px;">
                                        Zone
                                    </td>
                                    <td class="rcd-tableCell" style="width: 60%; height: 18px;">
                                        <asp:UpdatePanel id="upZones" runat="server">
                                            <contenttemplate>
                                            
                                            <asp:ListBox runat="server" ID="lstbxZone" OnSelectedIndexChanged="lstbxZone_SelectedIndexChanged"
                                            AutoPostBack="true" Width="167px" SelectionMode="Multiple"></asp:ListBox>
                                        </contenttemplate>
                                        </asp:UpdatePanel>
                                        <%--<asp:DropDownList ID="ddlZone" runat="server" SkinID="dropdownSkin" Width="156px">
                                        </asp:DropDownList>--%>
                                        <asp:RequiredFieldValidator ID="rfvZone" runat="server" ControlToValidate="lstbxZone"
                                            ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Select Zone" />
                                        <cc1:ValidatorCalloutExtender ID="vceZone" TargetControlID="rfvZone" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                        <%--<cc1:CascadingDropDown ID="cddZone" Category="category" TargetControlID="ddlZone"
                                            PromptText="Select Zone" LoadingText="Loading Text..." ServicePath="../WebServices/AdminServices.asmx"
                                            ServiceMethod="GetZone" runat="server">
                                        </cc1:CascadingDropDown>--%>
                                    </td>
                                </tr>
                                <tr runat="server" id="trRegion">
                                    <td class="rcd-FieldTitle" style="width: 40%">
                                        Region
                                    </td>
                                    <td class="rcd-tableCell" style="width: 60%">
                                        <%--<asp:DropDownList ID="ddlRegion" runat="server" SkinID="dropdownSkin" AutoPostBack="true"
                                            Width="156px" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>--%>
                                        <asp:UpdatePanel id="upRegions" runat="server">
                                            <contenttemplate>
                                                        
                                        <asp:ListBox ID="lstbxRegions" runat="server" SelectionMode="Multiple" AutoPostBack="true"
                                            OnSelectedIndexChanged="lstbxRegions_SelectedIndexChanged"></asp:ListBox>
                                            </contenttemplate>
                                        </asp:UpdatePanel>
                                        <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ControlToValidate="lstbxRegions"
                                            ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Select Region" />
                                        <cc1:ValidatorCalloutExtender ID="vceRegion" TargetControlID="rfvRegion" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                        <%--<cc1:CascadingDropDown ID="ccdRegion" Category="Region" TargetControlID="ddlRegion"
                                            PromptText="Select Region" ParentControlID="ddlZone" LoadingText="Loading Text..."
                                            ServicePath="../WebServices/AdminServices.asmx" ServiceMethod="GetRegion" runat="server">
                                        </cc1:CascadingDropDown>--%>
                                    </td>
                                </tr>
                                <tr runat="server" id="trBranch">
                                    <td class="rcd-FieldTitle" style="width: 40%; height: 18px;">
                                        Locatiion</td>
                                    <td class="rcd-tableCell" style="width: 60%; height: 18px;">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 45%" align="right">
                                                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                                                        <contenttemplate>
                                            <asp:ListBox ID="lstbxBranch" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                        </contenttemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td style="width: 10%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:UpdatePanel id="UpdatePanel2" runat="server">
                                                                    <contenttemplate>
                                                            <asp:Button id="btnAdd" onclick="btnAdd_Click" runat="server" SkinID="buttonSkin" Text=">" Width="25px"></asp:Button>
                                                        </contenttemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:UpdatePanel id="UpdatePanel3" runat="server">
                                                                    <contenttemplate>
                                                            <asp:Button id="btnRemove" onclick="btnRemove_Click" runat="server" SkinID="buttonSkin" Text="<" Width="25px"></asp:Button>
                                                        </contenttemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 45%">
                                                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                                                        <contenttemplate>
                                                <asp:ListBox ID="lstbxSelectedBranches" runat="server" SelectionMode="Multiple">
                                                </asp:ListBox>
                                            </contenttemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="lstbxSelectedBranches"
                                                        ErrorMessage="Select atleast one branch" InitialValue="" Font-Bold="True" Font-Size="Medium"
                                                        ValidationGroup="CheckData" Display="None" />
                                                    <cc1:ValidatorCalloutExtender ID="vceSelectedBranch" TargetControlID="rfvBranch"
                                                        runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 40%;">
                                        First Level Approver</td>
                                    <td class="rcd-tableCell">
                                        <asp:TextBox ID="txtUserCode" runat="server" SkinID="textboxSkin" Wrap="False"></asp:TextBox>
                                        <uc1:wucSearchmoreUserMapping ID="WucSearchmoreUserMapping1" runat="server" />
                                        
                                        <asp:RequiredFieldValidator ID="rfvtxtUserCode" runat="server" ControlToValidate="txtUserCode"
                                            ErrorMessage="Enter User Code" SetFocusOnError="true" ValidationGroup="CheckData"
                                            Display="None" />
                                        <cc1:ValidatorCalloutExtender ID="vcetxtUserCode" WarningIconImageUrl="../Images/Warning1.jpg"
                                            TargetControlID="rfvtxtUserCode" runat="server">
                                        </cc1:ValidatorCalloutExtender>                                        
                                        <cc1:AutoCompleteExtender ID="ace" runat="server" MinimumPrefixLength="1" ServiceMethod="GetEmpLoyeeNames"
                                            ServicePath="../WebServices/WebService.asmx" TargetControlID="txtUserCode">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 40%">
                                        Second Level Approver</td>
                                    <td class="rcd-tableCell">
                                        <asp:TextBox ID="txtSecondLevelApprover" runat="server" SkinID="textboxSkin"></asp:TextBox>
                                        <uc1:wucSearchmoreUserMapping ID="WucSearchmoreUserMapping2" runat="server" />
                                        <cc1:AutoCompleteExtender ID="aceSecondLevelApprover" runat="server" MinimumPrefixLength="1" ServiceMethod="GetEmpLoyeeNames"
                                            ServicePath="../WebServices/WebService.asmx" TargetControlID="txtSecondLevelApprover">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:Button ID="btnCreateMapping" runat="server" Text="Create Mapping" SkinID="buttonSkin"
                                            ValidationGroup="CheckData" OnClick="btnCreateMapping_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

                <script language="javascript" type="text/javascript">
                    SearchCriteria();
                </script>

            </td>
        </tr>
    </table>
</asp:Content>
