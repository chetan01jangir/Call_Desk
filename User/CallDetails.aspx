<%@ Page Language="C#" AutoEventWireup="true" Theme="SkinFile" CodeFile="CallDetails.aspx.cs"
    Inherits="User_CallDetails" Title=":: Call Desk - Call Details ::" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../App_Themes/rcd.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var _source;
        var _popup;
        function showConfirm(source) {
            this._source = source;
            this._popup = $find('mdlPopup');
            this._popup.show();
        }
        function okClick() {
            this._popup.hide();
            __doPostBack(this._source.name, '');
        }
        function cancelClick() {
            this._popup.hide();
            this._source = null;
            this._popup = null;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <%--<tr>
                <td colspan="2">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="rcd-TopHeaderBlue">
                                Call Details
                            </td>
                        </tr>
                    </table>
                    
                    
                </td>
            </tr>--%>
        <tr>
            <td colspan="2" style="height: 20px">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                    <tr class="rcd-TableHeader">
                        <td colspan="4">
                            Call Details
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle">
                            Ticket Number
                        </td>
                        <td class="rcd-tableCell" style="width: 25%">
                            <asp:Label ID="lblTicketNumber" runat="server" CssClass="rcd-label" Text="lblTicketNumber"></asp:Label>
                        </td>
                        <td class="rcd-FieldTitle" style="width: 25%">
                            Application Type
                        </td>
                        <td class="rcd-tableCell" style="width: 25%">
                            <asp:Label ID="lblApplicationType" runat="server" CssClass="rcd-label" Text="lblApplicationType"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle">
                            Issue / Requset Type
                        </td>
                        <td class="rcd-tableCell" style="width: 25%">
                            <asp:Label ID="lblIssueRequsetType" runat="server" CssClass="rcd-label" Text="lblIssueRequsetType"></asp:Label>
                        </td>
                        <td class="rcd-FieldTitle">
                            Issue / Requset Sub Type
                        </td>
                        <td class="rcd-tableCell" style="width: 25%">
                            <asp:Label ID="lblIssueRequsetSubType" runat="server" CssClass="rcd-label" Text="lblIssueRequsetSubType"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle">
                            Call Date
                        </td>
                        <td class="rcd-tableCell" style="width: 25%">
                            <asp:Label ID="lblCallDate" runat="server" CssClass="rcd-label" Text="lblCallDate"></asp:Label>
                        </td>
                        <td class="rcd-FieldTitle">
                            Call Status
                        </td>
                        <td class="rcd-tableCell" style="width: 25%">
                            <asp:Label ID="lblCallStatus" runat="server" CssClass="rcd-label" Text="lblCallStatus"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle">
                            Ticket value (Premium amount)
                        </td>
                        <td class="rcd-tableCell" style="width: 25%">
                            <asp:Label ID="lblTicketValue" runat="server" CssClass="rcd-label"></asp:Label>
                        </td>
                        <!-- [CR-34] Proposal No field add Start -->
                        <td class="rcd-FieldTitle">
                            Policy/Claim/Proposal/CoverNote No.
                        </td>
                        <td class="rcd-tableCell" style="width: 25%">
                            <asp:Label ID="lblProposalNo" runat="server" CssClass="rcd-label"></asp:Label>
                        </td>
                        <!-- [CR-34] Proposal No field add End -->
                    </tr>
                    <tr>
                        <td colspan="4" class="rcd-tableCell">
                        </td>
                    </tr>
                    <tr class="rcd-TableHeader">
                        <td colspan="4">
                            User Details
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle">
                            Call Logged User
                        </td>
                        <td class="rcd-tableCell">
                            <asp:Label ID="lblCallLoggedUser" CssClass="rcd-label" runat="server" Text="lblCallLoggedUser"></asp:Label>
                        </td>
                        <td class="rcd-FieldTitle">
                            Call Logged Branch
                        </td>
                        <td class="rcd-tableCell" style="width: 25%">
                            <asp:Label ID="lblCallLoggedLocation" CssClass="rcd-label" runat="server" Text="lblCallLoggedLocation"></asp:Label>
                        </td>
                    </tr>
                    <!--[CR-01] Service desk application change Start -->
                    <tr>
                        <td class="rcd-FieldTitle">
                            Call Logged By
                        </td>
                        <td class="rcd-tableCell">
                            <asp:Label ID="lblCallLoggedBy" CssClass="rcd-label" runat="server" Text="lblCallLoggedBy"></asp:Label>
                        </td>
                        <td class="rcd-FieldTitle">
                            User Contact No
                        </td>
                        <td class="rcd-tableCell" style="width: 25%">
                            <asp:Label ID="lblUserContactNo" CssClass="rcd-label" runat="server" Text="lblUserContactNo"></asp:Label>
                        </td>
                    </tr>
                    <!--[CR-01] Service desk application change End -->
                    <tr>
                        <td class="rcd-FieldTitle">
                            User Remark
                        </td>
                        <td class="rcd-tableCell" colspan="3">
                            <asp:Label ID="lblUserRemark" runat="server" CssClass="rcd-label" Text="lblUserRemark"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle">
                            Files Uploaded by User
                        </td>
                        <td class="rcd-tableCell" colspan="3">
                            <asp:GridView ID="gvUploadedFiles" SkinID="gridviewSkin" AutoGenerateColumns="false"
                                runat="server" OnRowCommand="gvUploadedFiles_RowCommand" OnRowDataBound="gvUploadedFiles_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Files Uploaded by User">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkFileName" CommandName="btnUploadedFile" Text='<%# Bind("UploadedFile") %>'
                                                CommandArgument='<%# Bind("UploadedFile") %>' runat="server"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <!--new cr for anjali by jd start-->
                    <tr>
                        <td colspan="4" class="rcd-tableCell">
                        </td>
                    </tr>
                    <tr id="trGMC_GPA" runat="server" visible="true">
                        <td colspan="4" style="height: 156px">
                            <table border="0" cellpadding="1" cellspacing="1" width="100%" class="rcd-TableBorder">
                                <tr class="rcd-TableHeader">
                                    <td colspan="4">
                                        GMC/GPA Details
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        Slip Received Date
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%;">
                                        <asp:Label ID="lblslipReceivedDate" runat="server" CssClass="rcd-label" Text="lblslipReceivedDate"></asp:Label>
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        Client Name
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%;">
                                        <asp:Label ID="lblClientName" runat="server" CssClass="rcd-label" Text="lblClientName"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Location Of Client
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblLocationOfClient" runat="server" CssClass="rcd-label" Text="lblLocationOfClient"></asp:Label>
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Direct/Broker/Agent
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblDirectBrokerAjent" runat="server" CssClass="rcd-label" Text="lblDirectBrokerAjent"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Direct/Broker/Agent Name
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblDirectBrokerAjentName" runat="server" CssClass="rcd-label" Text="lblDirectBrokerAjentName"></asp:Label>
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Expiring Policy Inception Date
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblPolicyInceptionDate" runat="server" CssClass="rcd-label" Text="lblPolicyInceptionDate"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Expiry Date
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblExpiryDate" runat="server" CssClass="rcd-label" Text="lblExpiryDate"></asp:Label>
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Expiry TPA
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblExpiryTPA" runat="server" CssClass="rcd-label" Text="lblExpiryTPA"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        Insurance Company
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        <asp:Label ID="lblInsuranceCompany" runat="server" CssClass="rcd-label" Text="lblInsuranceCompany"></asp:Label>
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        Expiring Broker
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%;">
                                        <asp:Label ID="lblExpiringBroker" runat="server" CssClass="rcd-label" Text="lblExpiringBroker"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                       Employer Employee Relationship
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        <asp:Label ID="lblEERelationship" runat="server" CssClass="rcd-label" Text="lblEERelationship"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="rcd-tableCell">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!--new cr for anjali by jd end-->
                    <tr id="trRequest" runat="server">
                        <td colspan="4" style="height: 156px">
                            <table border="0" cellpadding="1" cellspacing="1" width="100%" class="rcd-TableBorder">
                                <tr class="rcd-TableHeader">
                                    <td colspan="4">
                                        Approver Details
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        Approver Status
                                    </td>
                                    <td class="rcd-tableCell" style="height: 18px;" colspan="3">
                                        <asp:Label ID="lblApproverStatus" runat="server" CssClass="rcd-label" Text="lblApproverStatus"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        First Approver Name
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%;">
                                        <asp:Label ID="lblApproverName" runat="server" CssClass="rcd-label" Text="lblApproverName"></asp:Label>
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        First Approver Designation
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%;">
                                        <asp:Label ID="lblApproverDesignation" runat="server" CssClass="rcd-label" Text="lblApproverDesignation"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        First Approver E-Mail
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblApproverEMail" runat="server" CssClass="rcd-label" Text="lblApproverEMail"></asp:Label>
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        First Approver Contact No
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblFirstcontact" runat="server" CssClass="rcd-label" Text="lblFirstcontact"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Second Approver Name
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblSecondApproverName" runat="server" CssClass="rcd-label"></asp:Label>
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Second Approver Designation
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblSecondApproverDesignation" runat="server" CssClass="rcd-label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Second Approver E-Mail
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblSecondApproverEMail" runat="server" CssClass="rcd-label"></asp:Label>
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Second Approver Contact No
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblSecondcontact" runat="server" CssClass="rcd-label" Text="lblSecondcontact"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Approver Remark
                                    </td>
                                    <td class="rcd-tableCell" colspan="3">
                                        <asp:Label ID="lblApproverRemark" runat="server" CssClass="rcd-label" Text="lblApproverRemark"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        Expected Close Date
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        <asp:Label ID="lblApproverexpectedCloseDate" runat="server" CssClass="rcd-label"
                                            Text="lblApproverExpectedClosedDate"></asp:Label>
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        Actual Close Date
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%;">
                                        <asp:Label ID="lblApproverClosedDate" runat="server" CssClass="rcd-label" Text="lblApproverClosedDate"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Files Uploaded by Approver
                                    </td>
                                    <td class="rcd-tableCell" colspan="3">
                                        <asp:GridView ID="gvFilesUploadedbyApprover" SkinID="gridviewSkin" AutoGenerateColumns="false"
                                            runat="server" OnRowCommand="gvFilesUploadedbyApprover_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Files Uploaded by Approver">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkFileName" CommandName="btnUploadedFile" Text='<%# Bind("FilebyApprover") %>'
                                                            CommandArgument='<%# Bind("FilebyApprover") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="rcd-tableCell">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trIssue" runat="server">
                        <td colspan="4">
                            <table border="0" cellpadding="1" cellspacing="1" width="100%" class="rcd-TableBorder">
                                <tr class="rcd-TableHeader">
                                    <td colspan="4">
                                        AppSupport Details
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        AppSupport Status
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblAppSupportStatus" runat="server" CssClass="rcd-label" Text="lblAppSupportStatus"></asp:Label>
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Ticket Processing Group
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblTicketProGroup" runat="server" CssClass="rcd-label" Text="lblTicketProGroup"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td class="rcd-FieldTitle" style="width: 25%">
                                            Performer
                                        </td>
                                        <td class="rcd-tableCell" style="width: 25%">
                                            <asp:Label ID="lblperformer" runat="server" CssClass="rcd-label" Text="lblPerformer"></asp:Label></td>
                                         <td class="rcd-FieldTitle" style="width: 25%">
                                            Performer Contact
                                        </td>
                                        <td class="rcd-tableCell" style="width: 25%">
                                            <asp:Label ID="lblPerformerContact" runat="server" CssClass="rcd-label" Text="lblPerformerContact"></asp:Label></td>--%>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Performer
                                    </td>
                                    <td class="rcd-tableCell" colspan="3">
                                        <asp:Label ID="lblperformer" runat="server" CssClass="rcd-label" Text="lblPerformer"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        AppSupport Remark
                                    </td>
                                    <td class="rcd-tableCell" colspan="3">
                                        <asp:Label ID="lblAppSupportRemark" runat="server" CssClass="rcd-label" Text="lblAppSupportCloseDate"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Closure Category
                                    </td>
                                    <td class="rcd-tableCell" colspan="3">
                                        <asp:Label ID="lblclosurecategory" runat="server" CssClass="rcd-label" Text="lblclosurecategory"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        AppSupport Expected Close Date
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        <asp:Label ID="lblAppSupportExpectedCloseDate" runat="server" CssClass="rcd-label"
                                            Text="lblAppSupportCloseDate"></asp:Label>
                                    </td>
                                    <td id="tdCloseDate" class="rcd-FieldTitle" style="width: 25%;" runat="server">
                                        Actual Close Date
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%;">
                                        <asp:Label ID="lblAppSupportCloseDate" runat="server" CssClass="rcd-label" Text="lblAppSupportCloseDate"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Files Uploaded by AppSupport
                                    </td>
                                    <td class="rcd-tableCell" colspan="3">
                                        <asp:GridView ID="gvFilesUploadedforUser" SkinID="gridviewSkin" AutoGenerateColumns="false"
                                            runat="server" OnRowCommand="gvFilesUploadedforUser_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Files Uploaded by AppSupport">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkFileName" CommandName="btnUploadedFile" Text='<%# Bind("FileforUser") %>'
                                                            CommandArgument='<%# Bind("FileforUser") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        User Confirmation
                                    </td>
                                    <td class="rcd-tableCell" colspan="3">
                                        <asp:Label ID="lblUserconfirmation" runat="server" CssClass="rcd-label" Text="lblUserconfirmation"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="rcd-tableCell">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trReopen" runat="server" visible="false">
                        <td colspan="4">
                            <table border="0" cellpadding="1" cellspacing="1" width="100%" class="rcd-TableBorder">
                                <tr class="rcd-TableHeader">
                                    <td colspan="4">
                                        Reopen Call
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Upload File
                                    </td>
                                    <td class="rcd-tableCell" style="width: 75%">
                                        <asp:FileUpload ID="fuUpLoadFile" runat="server" SkinID="FileUploaderSkin" />
                                        <asp:RegularExpressionValidator ID="revFileUpload" runat="server" ControlToValidate="fuUpLoadFile"
                                            Display="None" ValidationGroup="CheckData" ErrorMessage="File Format not Supported"
                                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.doc|.DOC|.docx|.DOCX|.xls|.XLS|.xlsx|.XLSX|.txt|.TXT|.jpeg|.JPEG|.zip|.ZIP|.Zip)$">
                                        </asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceFileUpload" WarningIconImageUrl="../Images/Warning1.jpg"
                                            TargetControlID="revFileUpload" runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                        <br />
                                        <font style="font-style: italic; color: Maroon; font-size: smaller;">Note : Please zip
                                            files and attach if you want to attach more than one file.</font>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Reopen remark
                                    </td>
                                    <td class="rcd-tableCell" style="width: 75%">
                                        <asp:TextBox ID="txtReopenRemark" SkinID="multilinetextboxSkin" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvReopenRemark" runat="server" ControlToValidate="txtReopenRemark"
                                            ValidationGroup="CheckData1" SetFocusOnError="true" Display="None" ErrorMessage="Enter remark" />
                                        <cc1:ValidatorCalloutExtender ID="vceReopenRemark" TargetControlID="rfvReopenRemark"
                                            runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr runat="server" id="trlargemarket">
                                    <td class="rcd-tableCell" style="width: 70%" colspan="2">
                                        <!-- [CR-07] Large Market Start -->
                                        <asp:UpdatePanel ID="uplargemarket" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table width="100%" border="0" cellpadding="1" cellspacing="1" runat="server" id="tbllargemarket"
                                                    visible="false">
                                                    <%--<tr>
                                            <td class="rcd-FieldTitle" style="width: 30%">
                                                Branch Name
                                            </td>
                                            <td class="rcd-tableCell" style="width: 70%">
                                                <asp:DropDownList ID="ddlbranchname" runat="server" SkinID="dropdownSkin" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlbranchnameselectedindexchanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="rfvbranchname" runat="server" ControlToValidate="ddlbranchname"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Select Branch Name" ValidationGroup="CheckData"
                                                    InitialValue="--Select--"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="vcebranchname" WarningIconImageUrl="../Images/Warning1.jpg"
                                                    runat="server" TargetControlID="rfvbranchname" CssClass="CustomValidator">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>--%>
                                                    <tr>
                                                        <td class="rcd-FieldTitle" style="width: 30%">
                                                            Channel
                                                        </td>
                                                        <td class="rcd-tableCell" style="width: 70%">
                                                            <asp:DropDownList ID="ddlchannel" runat="server" SkinID="dropdownSkin" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlchannelselectedindexchanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvchannel" runat="server" ControlToValidate="ddlchannel"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Select Channel" ValidationGroup="CheckData"
                                                                InitialValue="--Select--"></asp:RequiredFieldValidator>
                                                            <cc1:ValidatorCalloutExtender ID="vcechannel" WarningIconImageUrl="../Images/Warning1.jpg"
                                                                runat="server" TargetControlID="rfvchannel" CssClass="CustomValidator">
                                                            </cc1:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                            <td class="rcd-FieldTitle" style="width: 30%">
                                                SM
                                            </td>
                                            <td class="rcd-tableCell" style="width: 70%">
                                                <asp:DropDownList ID="ddlsm" runat="server" SkinID="dropdownSkin" OnSelectedIndexChanged="ddlsmselectedindexchanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="--Select--" Value="--Select--" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="rfvsm" runat="server" ControlToValidate="ddlsm" Display="None"
                                                    SetFocusOnError="true" ErrorMessage="Select SM" ValidationGroup="CheckData" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="vcesm" WarningIconImageUrl="../Images/Warning1.jpg"
                                                    runat="server" TargetControlID="rfvsm" CssClass="CustomValidator">
                                                </cc1:ValidatorCalloutExtender>
                                            </td>
                                        </tr>--%>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestSubType" EventName="SelectedIndexChanged">
                                                </asp:AsyncPostBackTrigger>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <!-- [CR-07] Large Market End -->
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!-- [CR-12] RCA start -->
                    <tr id="trRCA" runat="server" visible="false">
                        <td colspan="4">
                            <table border="0" cellpadding="1" cellspacing="1" width="100%" class="rcd-TableBorder">
                                <tr class="rcd-TableHeader">
                                    <td colspan="4">
                                        Root Cause Analysis (RCA)
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        RCA Date Occurred
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblRCADateOccured" runat="server" Text="lblRCADateOccured" CssClass="rcd-label"></asp:Label>
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        RCA Publish Date
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblRCAPublishDate" runat="server" Text="lblRCAPublishDate" CssClass="rcd-label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        RCA Type
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblRCAType" runat="server" Text="lblRCAType" CssClass="rcd-label"></asp:Label>
                                    </td>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        RCA Deployment Date
                                    </td>
                                    <td class="rcd-tableCell" style="width: 25%">
                                        <asp:Label ID="lblRCADeploymentDate" runat="server" Text="lblRCADeploymentDate" CssClass="rcd-label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        Team Member Involved
                                    </td>
                                    <td class="rcd-tableCell" colspan="3" style="width: 75%;">
                                        <asp:Label ID="lblTeamMemberInvolved" runat="server" Text="lblTeamMemberInvolved"
                                            CssClass="rcd-label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 25%">
                                        RCA Details and Action taken
                                    </td>
                                    <td class="rcd-tableCell" colspan="3" style="width: 75%;">
                                        <asp:Label ID="lblRCADetails" runat="server" Text="lblRCADetails" CssClass="rcd-label"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!-- [CR-12] RCA END -->
                    <tr id="trReassign" runat="server">
                        <td colspan="4">
                            <table border="0" cellpadding="1" cellspacing="1" width="100%" class="rcd-TableBorder">
                                <tr class="rcd-TableHeader">
                                    <td colspan="4">
                                        Reassign Call
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:UpdatePanel ID="panelMsg" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Label ID="lblReassignLabel" runat="server" SkinID="SkinLabel"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlApplicationType" EventName="SelectedIndexChanged">
                                                </asp:AsyncPostBackTrigger>
                                                <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestType" EventName="SelectedIndexChanged">
                                                </asp:AsyncPostBackTrigger>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 30%">
                                        Application Type
                                    </td>
                                    <td class="rcd-tableCell" style="width: 70%">
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
                                                        ServiceMethod="GetApplicationTypesByBracnchGroup" runat="server">
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
                                                    <cc1:CascadingDropDown ID="ccdIssueRequestSubType" Category="IssueRequestSubType"
                                                        TargetControlID="ddlIssueRequestSubType" PromptText="Select Issue Request Sub Type"
                                                        ParentControlID="ddlIssueRequestType" LoadingText="Loading Text..." ServicePath="../WebServices/WebService.asmx"
                                                        ServiceMethod="GetTypeofIssueRequestSubTypeRCIssueOnly" runat="server">
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
                                <tr>
                                    <td class="rcd-tableCell" colspan="2">
                                        <asp:Button ID="btnReassign" runat="server" Text="Reassign" OnClick="btnReassign_Click"
                                            SkinID="buttonSkin" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-tableCellCenterAlign" colspan="4">
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" SkinID="buttonSkin" OnClick="btnEdit_Click"
                                Visible="false" />
                            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="CheckData" Text="Submit"
                                SkinID="buttonSkin" OnClick="btnSubmit_Click" Visible="false" />
                            <asp:Button ID="btnReopen" runat="server" Text="Reopen" ValidationGroup="CheckData1"
                                SkinID="buttonSkin" OnClick="btnReopen_Click" />
                            <%--OnClientClick="showConfirm(this); return false;"--%>
                            <asp:Button ID="btnOK" runat="server" Text="Cancel" SkinID="buttonSkin" OnClick="btnOK_Click"
                                Width="60px" />
                            <cc1:ModalPopupExtender ID="md1" runat="server" BackgroundCssClass="modalBackground"
                                BehaviorID="mdlPopup" CancelControlID="btnNo" OkControlID="btnOk2" OnCancelScript="cancelClick();"
                                OnOkScript="okClick();" PopupControlID="div" TargetControlID="div">
                            </cc1:ModalPopupExtender>
                            <div id="div" runat="server" align="center" class="confirm" style="display: none">
                                Are you sure you want to Reopen ?
                                <asp:ImageButton ID="btnOk2" runat="server" ImageUrl="~/Images/smallsuccess.gif"
                                    ToolTip="Yes" Width="22px" Height="22px" CommandName="btnReopen_Click" />
                                <asp:ImageButton ID="btnNo" runat="server" ImageUrl="~/Images/Delete.gif" Width="22px"
                                    Height="22px" ToolTip="No" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%--</asp:Content>--%>
    </form>
</body>
</html>
