<%@ Page Language="C#" MasterPageFile="~/IMD/AgentMasterPage.master" Theme="SkinFile"
    AutoEventWireup="true" CodeFile="RegisterCall.aspx.cs" Inherits="_Default" EnableEventValidation="false"
    Title=":: Call Desk - Register New Call ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function NumericCharacter(varTextBox) {
            if (event.keyCode < 48 || event.keyCode > 57) {
                event.returnValue = false;
            }
        }

        function CheckFileExists() {
            document.getElementById('<%=rfvUpload.ClientID%>').enabled = false;
            var fileName = document.getElementById('<%=lnkFileTemplate.ClientID%>').innerText;
            if (fileName != '') {
                document.getElementById('<%=rfvUpload.ClientID%>').enabled = true;
            }
            else {
                document.getElementById('<%=rfvUpload.ClientID%>').enabled = false;
            }
        }

        function EndRequestHandler(sender, args) {

            CheckFileExists();
        }



        function ShowMyDiv() {

            var lblRemark = document.getElementById("ctl00_ContentPlaceHolder1_lblRemark");
            var hfRemark = document.getElementById("ctl00_ContentPlaceHolder1_hfRemark");
            lblRemark.innerHTML = hfRemark.value;

        }

        function howMany() {

            var lblRemark1 = document.getElementById("ctl00_ContentPlaceHolder1_lblRemark");
            var file = document.getElementById("ctl00_ContentPlaceHolder1_lnkFileTemplate");
            file.style.visibility = 'hidden';
            lblRemark1.innerHTML = '';
            lblRemark1.value = '';
        }

        function load() {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        }            
    
    </script>
    <body onload="load();" />
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue">
                            Register New Call
                        </td>
                    </tr>
                </table>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
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
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Application Type
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlApplicationType" onchange="howMany();" runat="server" SkinID="dropdownSkin">
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
                                            ServiceMethod="GetApplicationTypesByBracnch" runat="server">
                                        </cc1:CascadingDropDown>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Issue / Request Type
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
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
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Issue / Request Sub Type
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlIssueRequestSubType" AutoPostBack="true" runat="server"
                                            SkinID="dropdownSkin" onchange="ShowMyDiv();" OnSelectedIndexChanged="ddlIssueRequestSubType_SelectedIndexChanged">
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
                                        <cc1:CascadingDropDown ID="ccdIssueRequestSubType" Category="IssueRequestSubType"
                                            TargetControlID="ddlIssueRequestSubType" PromptText="Select Issue Request Sub Type"
                                            ParentControlID="ddlIssueRequestType" LoadingText="Loading Text..." ServicePath="../WebServices/WebService.asmx"
                                            ServiceMethod="GetTypeofIssueRequestSubTypeRC" runat="server">
                                        </cc1:CascadingDropDown>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!-- [CR-10] IMD Group Creation Start -->
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Region
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <asp:DropDownList ID="ddlRegion" runat="server" SkinID="dropdownSkin" Width="200px"
                                OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlRegion" runat="server" ControlToValidate="ddlRegion"
                                Display="None" SetFocusOnError="true" ErrorMessage="Select Region" ValidationGroup="CheckData"
                                InitialValue="0">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vcerfvddlRegion" WarningIconImageUrl="../Images/Warning1.jpg"
                                runat="server" TargetControlID="rfvddlRegion" CssClass="CustomValidator">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Branch
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <asp:DropDownList ID="ddlBranch" runat="server" SkinID="dropdownSkin" Width="200px">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                Display="None" SetFocusOnError="true" ErrorMessage="Select Branch" ValidationGroup="CheckData"
                                InitialValue="0">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vcerfvddlBranch" WarningIconImageUrl="../Images/Warning1.jpg"
                                runat="server" TargetControlID="rfvddlBranch" CssClass="CustomValidator">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <!-- [CR-10] IMD Group Creation End -->
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 30%">
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lblRemark" runat="server" SkinID="SkinDivLabel"></asp:Label>
                                    <asp:HiddenField ID="hfRemark" runat="server"></asp:HiddenField>
                                    <asp:LinkButton ID="lnkFileTemplate" ToolTip="Click to download the sample file"
                                        runat="server" SkinID="lnkSkin" OnClick="lnkFileTemplate_Click"></asp:LinkButton>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestSubType" EventName="SelectedIndexChanged">
                                    </asp:AsyncPostBackTrigger>
                                    <asp:PostBackTrigger ControlID="lnkFileTemplate" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Remark
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtRemark" runat="server" MaxLength="1000" SkinID="multilinetextboxSkin"
                                            TextMode="MultiLine">
                                        </asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtRemark"
                                            WatermarkText="Type Remark Here" WatermarkCssClass="rcd-multilinetxtboxvaluewatermark" />
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvtxtRemark" runat="server" ControlToValidate="txtRemark"
                                            ErrorMessage="Enter remark" SetFocusOnError="true" ValidationGroup="CheckData"
                                            Display="None" />
                                        <cc1:ValidatorCalloutExtender ID="vcetxtRemark" WarningIconImageUrl="../Images/Warning1.jpg"
                                            TargetControlID="rfvtxtRemark" runat="server" CssClass="CustomValidator">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <%--<td>
                                        <asp:Panel ID="pnlMyPanel" runat="server">
                                        </asp:Panel>
                                        <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="ddlIssueRequestSubType"
                                            PopupControlID="pnlMyPanel" Position="Bottom">
                                        </cc1:PopupControlExtender>
                                    </td>--%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Contact Number
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <asp:TextBox ID="txtContactNumber" runat="server" MaxLength="11" onkeypress="NumericCharacter(this)"
                                SkinID="textboxSkin"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvContactNumber" runat="server" ControlToValidate="txtContactNumber"
                                ErrorMessage="Enter contact number" SetFocusOnError="true" ValidationGroup="CheckData"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="vceContactNumber" CssClass="CustomValidator" WarningIconImageUrl="../Images/Warning1.jpg"
                                TargetControlID="rfvContactNumber" runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtContactNumber"
                                WatermarkText="Type Contact Number" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                        </td>
                    </tr>
                    <tr id="trTicketValue" runat="server">
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Ticket value (Premium amount)
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <nobr>
                          <asp:TextBox ID="txtTicketValue" runat="server" MaxLength="15" 
                          SkinID="textboxSkin"></asp:TextBox>                            
                          <asp:RegularExpressionValidator ID="revTicketValue"  runat="server" ControlToValidate="txtTicketValue"
                           ErrorMessage="Enter number with two decimal places only"  SetFocusOnError="true" Display="None" ValidationGroup="CheckData" ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,18}(\.\d{1,2})?$" >
                          </asp:RegularExpressionValidator>   
                          <cc1:ValidatorCalloutExtender ID="vceTicketValue" runat="server" CssClass="CustomValidator"  WarningIconImageUrl="../Images/Warning1.jpg" TargetControlID="revTicketValue">
                          </cc1:ValidatorCalloutExtender>                                                                  
                          <cc1:TextBoxWatermarkExtender ID="TBWMTicketValue" runat="server" TargetControlID="txtTicketValue"
                           WatermarkText="Type Premium Amount" WatermarkCssClass="rcd-txtboxvaluewatermark"></cc1:TextBoxWatermarkExtender>
                          <font style="font-style: italic; color: Maroon; font-size: smaller;">Note: For ICM Call Categories Only.</font></nobr>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Upload Screenshot
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <asp:FileUpload ID="fuUpLoadFile" runat="server" SkinID="FileUploaderSkin" />
                            <asp:RegularExpressionValidator ID="revFileUpload" runat="server" ControlToValidate="fuUpLoadFile"
                                Display="None" SetFocusOnError="true" ErrorMessage="File Format not Supported"
                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.doc|.DOC|.docx|.DOCX|.xls|.XLS|.xlsx|.XLSX|.txt|.TXT|.jpeg|.JPEG|.zip|.ZIP|.Zip)$">
                            </asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="vceFileUpload" WarningIconImageUrl="../Images/Warning1.jpg"
                                TargetControlID="revFileUpload" runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="rfvUpload" runat="server" ControlToValidate="fuUpLoadFile"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please upload the file">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceUpload" WarningIconImageUrl="../Images/Warning1.jpg"
                                TargetControlID="rfvUpload" runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <br />
                            <font style="font-style: italic; color: Maroon; font-size: smaller;">Note : Please zip
                                files and attach if you want to attach more than one file.</font>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCell">
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table id="tblPortalIDLocked" runat="server" visible="false" width="100%" cellspacing="0"
                                        cellpadding="0">
                                        <tr>
                                            <td class="rcd-FieldTitle" style="width: 30%">
                                                Please enter Portal ID
                                            </td>
                                            <td class="rcd-tableCell" style="width: 70%">
                                                <asp:TextBox ID="txtPortalID" runat="server" MaxLength="15" SkinID="textboxSkin"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender ID="TWEPortalID" runat="server" TargetControlID="txtPortalID"
                                                    WatermarkText="Enter Portal ID Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                                <asp:RequiredFieldValidator ID="rfvPortalID" runat="server" ControlToValidate="txtPortalID"
                                                    ErrorMessage="Enter Portal ID" SetFocusOnError="true" ValidationGroup="CheckData"
                                                    Display="None" />
                                                <cc1:ValidatorCalloutExtender ID="vcePortalID" WarningIconImageUrl="../Images/Warning1.jpg"
                                                    TargetControlID="rfvPortalID" runat="server" CssClass="CustomValidator">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestSubType" EventName="SelectedIndexChanged">
                                    </asp:AsyncPostBackTrigger>
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnRegisterCall" ValidationGroup="CheckData" runat="server" Text="Register Your Call"
                                SkinID="buttonSkin" OnClick="btnRegisterCall_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" SkinID="buttonSkin" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="paneldesc" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlMyPanel" runat="server" Style="display: none" Height="170px" Width="400px"
                                        CssClass="modalPopup2">
                                        <div style="color: White; font-size: 14" class="rcd-TopHeaderBlue">
                                            For Your Information.
                                        </div>
                                        <%--<hr class="hrLine" />--%>
                                        <table cellpadding="1" cellspacing="1" width="100%" class="rcd-Instruct" style="font-size: 11">
                                            <tr>
                                                <td>
                                                    <p>
                                                        <asp:Label ID="lbldesc" runat="server"></asp:Label></p>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Do you still want to log a call ?"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblcontrol" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <center>
                                            <asp:Button ID="btnyes" runat="server" Text="Yes" SkinID="buttonSkin" />
                                            <asp:Button ID="btnno" OnClick="btnno_Click" runat="server" Text="No" SkinID="buttonSkin" />
                                        </center>
                                    </asp:Panel>
                                    <cc1:ModalPopupExtender ID="popupconfirm" runat="server" TargetControlID="lblcontrol"
                                        PopupControlID="pnlMyPanel" CancelControlID="btnyes" Enabled="true" DropShadow="false"
                                        BackgroundCssClass="modalBackground1" DynamicServicePath="">
                                    </cc1:ModalPopupExtender>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestSubType" EventName="SelectedIndexChanged">
                                    </asp:AsyncPostBackTrigger>
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>       
       <%-- <tr>
            <td width="100%">
                <center>
                    <b>Due to Documentum Server down, Slowness noticed in IMD Portal.<br />
                        Request to co operate until the issue resolved.</b>
                </center>
            </td>
        </tr>--%>
    </table>
</asp:Content>
