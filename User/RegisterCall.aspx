<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile"
    AutoEventWireup="true" CodeFile="RegisterCall.aspx.cs" Inherits="_Default" EnableEventValidation="false"
    Title=":: Call Desk - Register New Call ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../Include/jquery-1.7.1.min.js" type="text/javascript"></script>--%>
    <script src="../Include/jquery-3.6.0.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function fnRedirectSZ() {

            location.reload();
            window.open('https://smartzone.reliancegeneral.co.in/Login/userexist');
        }

        function NumericCharacter(varTextBox) {
            if (event.keyCode < 48 || event.keyCode > 57) {
                event.returnValue = false;
            }
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                //  return false;
            }
            return true;
        }
        function CheckFileExists() {


            var occupancy = $('#<%= ddloccupany.ClientID %> option:selected').text();
            $('#<%= hdnoccupancy.ClientID %>').val(occupancy);
            document.getElementById('<%=rfvUpload.ClientID%>').enabled = false;
            var fileName = document.getElementById('<%=lnkFileTemplate.ClientID%>').innerText;
            if (fileName != '') {
                document.getElementById('<%=rfvUpload.ClientID%>').enabled = true;
            }
            else {
                document.getElementById('<%=rfvUpload.ClientID%>').enabled = false;
            }

            //var IssueRequestType = document.getElementById('<%= ddlIssueRequestType.ClientID %>');
            var ApplicationType = document.getElementById('<%=ddlApplicationType.ClientID %>');

            var IssueRequestTypevalue = ApplicationType.value;

            var IssueText = $('#<%= ddlIssueRequestType.ClientID %> option:selected').text();
            var strSubstring_4 = IssueText.substring(0, 4);
            var strSubstring_3 = IssueText.substring(0, 3);

            // VALUE FOR UAT / PRODUCTION ---- 109 / 118 ---- Comment accordingly for UAT and Production
            //if (IssueRequestTypevalue == "109") {

            if (IssueRequestTypevalue == "118") {
                // VALUE FOR UAT / PRODUCTION ---- 109 / 118 ---- Comment accordingly for UAT and Production

                //If Value in given 4 category add occupancy list mandatory

                if (strSubstring_3.toLowerCase() == "gmc") {
                    document.getElementById('<%=trtotalsum.ClientID%>').style.display = "none";
                    document.getElementById('<%=trinsurename.ClientID%>').style.display = "none";
                    document.getElementById('<%=trimdcode.ClientID%>').style.display = "none";
                    document.getElementById('<%=tragentcode.ClientID%>').style.display = "none";
                    document.getElementById('<%=trddloccupany.ClientID%>').style.display = "none";


                    document.getElementById('<%=rfctottalsuminsured.ClientID %>').enabled = false;
                    document.getElementById('<%=rfvInsurerName.ClientID %>').enabled = false;
                    document.getElementById('<%=rfvImdCode.ClientID %>').enabled = false;
                    document.getElementById('<%=rfvAgentCode.ClientID %>').enabled = false;
                    document.getElementById('<%=rfvddloccupancy.ClientID %>').enabled = false;
                }
                else {


                    document.getElementById('<%=rfctottalsuminsured.ClientID %>').enabled = true;
                    document.getElementById('<%=rfvInsurerName.ClientID %>').enabled = true;
                    document.getElementById('<%=rfvImdCode.ClientID %>').enabled = true;
                    document.getElementById('<%=rfvAgentCode.ClientID %>').enabled = true;
                    if (strSubstring_4.toLowerCase() == "fire" || strSubstring_3.toLowerCase() == "ccp" || strSubstring_3.toLowerCase() == "icp" || strSubstring_3.toLowerCase() == "iar") {
                        document.getElementById('<%=rfvddloccupancy.ClientID %>').enabled = true;
                    }
                    else {
                        document.getElementById('<%=rfvddloccupancy.ClientID %>').enabled = false;
                    }
                }

            }
            else {
                document.getElementById('<%=rfctottalsuminsured.ClientID %>').enabled = false;
                document.getElementById('<%=rfvInsurerName.ClientID %>').enabled = false;
                document.getElementById('<%=rfvImdCode.ClientID %>').enabled = false;
                document.getElementById('<%=rfvAgentCode.ClientID %>').enabled = false;
                document.getElementById('<%=rfvddloccupancy.ClientID %>').enabled = false;
            }

            //            for (i = 0; i < Page_Validators.length; i++) {
            //                if (Page_Validators[i].controltovalidate == "ctl00_ContentPlaceHolder1_txttotalsuminsured") {
            //                    //debugger;

            //                    // VALUE FOR UAT / PRODUCTION ---- 109 / 118 ---- Comment accordingly for UAT and Production
            //                    if (IssueRequestTypevalue == "109") {
            //                    // if (IssueRequestTypevalue == "118") {
            //                    // VALUE FOR UAT / PRODUCTION ---- 109 / 118 ---- Comment accordingly for UAT and Production
            //                        ValidatorEnable(Page_Validators[i], true);
            //                    }
            //                    else {
            //                        ValidatorEnable(Page_Validators[i], false);

            //                    }
            //                }
            //            }

        }


        function CallWebService() {
            // debugger;
            var IMDCODE = $('#<%= txtimdcode.ClientID %>').val();
            //  alert(IMDCODE);
            $.ajax({
                type: "post",
                data: "{'IMDcode':'" + IMDCODE + "'}",
                url: "../WebServices/WebService.asmx/GetIMDCODE",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    // debugger;
                    OnSuccess(result.d);
                },
                error: function (xhr, status, error) {
                    OnFailure(error);
                }
            });
        }
        function OnSuccess(result) {
            // debugger;
            if (result) {
                if (result == "true") {
                    // debugger;
                    $('#<%= hdnimdcode.ClientID %>').val('Yes');
                    // alert($('#ctl00_ContentPlaceHolder1_hdnimdcode').val());
                    return true;
                } else {
                    // alert('IMD code wrong');
                    $('#<%= hdnimdcode.ClientID %>').val('No');
                    return false;
                }
                // alert(result);
                // document.getElementById("result").innerHTML = result;
            }
        }
        function OnFailure(error) {
            $('#<%= hdnimdcode.ClientID %>').val('No');
            //  alert(error);
            return false;
        }

        function Fnshowhide_field() {

            //var IssueRequestType = document.getElementById('<%=ddlIssueRequestType.ClientID%>');
            var ApplicationType = document.getElementById('<%=ddlApplicationType.ClientID %>');

            var IssueRequestTypevalue = ApplicationType.value;
            //var IssueRequestTypevalue = IssueRequestType.value;

            // VALUE FOR UAT / PRODUCTION ---- 109 / 118 ---- Comment accordingly for UAT and Production
            //if (IssueRequestTypevalue == "109") {
            if (IssueRequestTypevalue == "118") {
                // VALUE FOR UAT / PRODUCTION ---- 109 / 118 ---- Comment accordingly for UAT and Production
                var IssueText = $('#<%= ddlIssueRequestType.ClientID %> option:selected').text();
                var strSubstring_4 = IssueText.substring(0, 4);
                var strSubstring_3 = IssueText.substring(0, 3);


                if (strSubstring_3.toLowerCase() == "gmc") {
                    document.getElementById('<%=trtotalsum.ClientID%>').style.display = "none";
                    document.getElementById('<%=trinsurename.ClientID%>').style.display = "none";
                    document.getElementById('<%=trimdcode.ClientID%>').style.display = "none";
                    document.getElementById('<%=tragentcode.ClientID%>').style.display = "none";
                    document.getElementById('<%=trddloccupany.ClientID%>').style.display = "none";


                    document.getElementById('<%=rfctottalsuminsured.ClientID %>').enabled = false;
                    document.getElementById('<%=rfvInsurerName.ClientID %>').enabled = false;
                    document.getElementById('<%=rfvImdCode.ClientID %>').enabled = false;
                    document.getElementById('<%=rfvAgentCode.ClientID %>').enabled = false;
                    document.getElementById('<%=rfvddloccupancy.ClientID %>').enabled = false;
                }
                else {

                    document.getElementById('<%=trtotalsum.ClientID%>').style.display = "";
                    document.getElementById('<%=trinsurename.ClientID%>').style.display = "";
                    document.getElementById('<%=trimdcode.ClientID%>').style.display = "";
                    document.getElementById('<%=tragentcode.ClientID%>').style.display = "";
                    document.getElementById('<%=trddloccupany.ClientID%>').style.display = "";
                }
            }
            else {


                document.getElementById('<%=trtotalsum.ClientID%>').style.display = "none";
                document.getElementById('<%=trinsurename.ClientID%>').style.display = "none";
                document.getElementById('<%=trimdcode.ClientID%>').style.display = "none";
                document.getElementById('<%=tragentcode.ClientID%>').style.display = "none";
                document.getElementById('<%=trddloccupany.ClientID%>').style.display = "none";

            }
        }


        function EndRequestHandler(sender, args) {
            //var hfRemark = document.getElementById("ctl00_ContentPlaceHolder1_hfRemark");
            //creatediv('newdiv', hfRemark.value, 100, 100, 720, 237);
            CheckFileExists();
        }

        /*
        function creatediv(id, html, width, height, left, top)
        {
        var newdiv;
        var divid = document.getElementById("newdiv");
        if(divid != null)
        {
        newdiv = divid.id;
        if (html)
        {
        document.getElementById("newdiv").style.display = 'inline';
        document.getElementById("newdiv").innerHTML = html;
        }
        else
        {
        document.getElementById("newdiv").style.display = 'none';
        }
        }
        else
        {
        newdiv = document.createElement('div');
        newdiv.setAttribute('id', id);      
                
        if (width) 
        {
        newdiv.style.width = "auto";
        }

        if (height)
        {
        newdiv.style.height = "auto";
        }

        if ((left || top) || (left && top))
        {
        //newdiv.style.position = "absolute";
        newdiv.style.position = "fixed";

        if (left)
        {
        newdiv.style.left = left;
        }

        if (top) 
        {
        newdiv.style.top = top;
        }
        }
                          
        newdiv.style.background = "#F8FBFF";
        newdiv.style.border = "1px solid #000";
        newdiv.style.fontFamily = "Arial, Tahoma,Verdana, Helvetica, sans-serif";
        newdiv.style.fontSize = "11px";
        newdiv.style.zIndex = "1";
            
        if (html)
        {
        newdiv.innerHTML = html;
        }
        else
        {
        newdiv.innerHTML = "";
        }
                
        //                var newTR = document.createElement('TR');
        //                newdiv.appendChild(newTR);    
        //                  
        //                var lnk = document.createElement('a');                
        //                if(lnk.addEventListener)
        //                {
        //                    lnk.addEventListener('click', (function(i){return function(){document.body.removeChild(i)}})(newdiv), false);
        //                }
        //                else if(lnk.attachEvent)
        //                {
        //                    lnk.attachEvent('onclick', (function(i){return function(){document.body.removeChild(i)}})(newdiv));
        //                }
        //                else
        //                {
        //                    lnk.onclick = (function(i){return function(){document.body.removeChild(i)}})(newdiv);
        //                }
        //                lnk.href = '#';
        //                lnk.appendChild(document.createTextNode("Close"));
        //                newdiv.appendChild(lnk);            
                                
        //                var objectToAppend;
        //                objectToAppend = document.createElement("img");
        //                objectToAppend.setAttribute("src","../images/close.gif");                
        //                 newdiv.appendChild(objectToAppend);                                   
               
        document.body.appendChild(newdiv);                
        }
        } */

        function ShowMyDiv() {
            //var divid = document.getElementById("newdiv");
            //if(divid != null)
            //{
            //    document.getElementById("newdiv").style.visibility = 'visible';
            //}

            var lblRemark = document.getElementById("ctl00_ContentPlaceHolder1_lblRemark");
            var hfRemark = document.getElementById("ctl00_ContentPlaceHolder1_hfRemark");
            lblRemark.innerHTML = hfRemark.value;



        }

        function howMany() {
            //  var divs = document.getElementsByTagName("div");
            //            var divid = document.getElementById("newdiv");
            //            
            //            //var file = document.getElementById('<%=lnkFileTemplate.ClientID%>');
            //            var file = document.getElementById("ctl00_ContentPlaceHolder1_lnkFileTemplate");
            //            
            //            if(divid != null)
            //            {                      
            //                document.getElementById("newdiv").style.visibility = 'hidden';
            //                file.style.visibility = 'hidden';
            //            }


            var lblRemark1 = document.getElementById("ctl00_ContentPlaceHolder1_lblRemark");
            var file = document.getElementById("ctl00_ContentPlaceHolder1_lnkFileTemplate");
            file.style.visibility = 'hidden';
            lblRemark1.innerHTML = '';
            lblRemark1.value = '';

            document.getElementById('<%=trtotalsum.ClientID%>').style.display = "none";
            document.getElementById('<%=trinsurename.ClientID%>').style.display = "none";
            document.getElementById('<%=trimdcode.ClientID%>').style.display = "none";
            document.getElementById('<%=tragentcode.ClientID%>').style.display = "none";
            document.getElementById('<%=trddloccupany.ClientID%>').style.display = "none";


            //added by shilpa
            var ApplicationType = document.getElementById('<%=ddlApplicationType.ClientID %>');

            var IssueRequestTypevalue = ApplicationType.value;
            if (IssueRequestTypevalue == "183") {
                document.getElementById('<%=policytable.ClientID%>').style.display = "none";
                document.getElementById('<%=policytable.ClientID %>').disabled = true;
                document.getElementById('<%=Tr1.ClientID%>').style.display = "";
                document.getElementById('<%=Tr1.ClientID %>').enabled = true;


                //document.getElementById('policytable').removeAttribute("disabled");
            }
            else {
                //document.getElementById('<%=rfvddloccupancy.ClientID %>').enabled = true;
                document.getElementById('<%=policytable.ClientID%>').style.display = "";
                document.getElementById('<%=policytable.ClientID %>').enabled = true;
                document.getElementById('<%=Tr1.ClientID%>').style.display = "none";
                document.getElementById('<%=Tr1.ClientID %>').disabled = true;
            }


            //added by shilpa for HR Purposes

            if (IssueRequestTypevalue == "184") {
                //document.getElementById('ctl00_ContentPlaceHolder1_policytable').style.display = "none";
                // document.getElementById('ctl00_ContentPlaceHolder1_policytable').disabled = true;


                //document.getElementById('ctl00_ContentPlaceHolder1_trTicketValue').style.display = "none";
                //document.getElementById('ctl00_ContentPlaceHolder1_trTicketValue').disabled = true;


                document.getElementById('ctl00_ContentPlaceHolder1_policytable').style.visibility = 'hidden';
                document.getElementById('ctl00_ContentPlaceHolder1_trTicketValue').style.visibility = 'hidden';




                //document.getElementById('policytable').removeAttribute("disabled");
            }
            else {
                //document.getElementById('ctl00_ContentPlaceHolder1_rfvddloccupancy').enabled = true;
                //document.getElementById('ctl00_ContentPlaceHolder1_policytable').style.display = "";
                //document.getElementById('ctl00_ContentPlaceHolder1_policytable').enabled = true;


                //document.getElementById('ctl00_ContentPlaceHolder1_trTicketValue').style.display = "";
                //document.getElementById('ctl00_ContentPlaceHolder1_trTicketValue').enabled = true;
                //document.getElementById('ctl00_ContentPlaceHolder1_trTicketValue').style.visibility='visible';
                document.getElementById('ctl00_ContentPlaceHolder1_policytable').style.visibility = 'visible';
                document.getElementById('ctl00_ContentPlaceHolder1_trTicketValue').style.visibility = 'visible';

            }


        }

        function load() {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        }            
    
    </script>
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
                <asp:UpdatePanel ID="panelMsg" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblMessage" runat="server" SkinID="SkinLabel"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlApplicationType" EventName="SelectedIndexChanged">
                        </asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestType" EventName="SelectedIndexChanged">
                        </asp:AsyncPostBackTrigger>
                        <%-- <asp:AsyncPostBackTrigger ControlID="rdoProcessing" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>                                                                                                                            --%>
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                    <tr id="trDepartment" runat="server" visible="false">
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Department
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="rcd-FieldTitle">
                                        <asp:RadioButtonList ID="rdoProcessing" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rdoProcessing_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1">IT</asp:ListItem>
                                            <asp:ListItem Value="2">NON-IT</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlApplicationType"
                                            ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Select application type" />
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="rfvApplicationType"
                                            runat="server" WarningIconImageUrl="../Images/Warning1.jpg" CssClass="CustomValidator">
                                        </cc1:ValidatorCalloutExtender>--%>
                                    </td>
                                    <td>
                                        <%-- <cc1:CascadingDropDown ID="CascadingDropDown1" Category="category" TargetControlID="ddlApplicationType"
                                            PromptText="Select Application" LoadingText="Loading Text..." ServicePath="../WebServices/WebService.asmx"
                                            ServiceMethod="GetApplicationTypesByBracnchGroup" runat="server">
                                        </cc1:CascadingDropDown>--%>
                                    </td>
                                </tr>
                            </table>
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
                                        <asp:DropDownList ID="ddlApplicationType" onchange="howMany();" runat="server" SkinID="dropdownSkin"
                                            OnSelectedIndexChanged="ddlApplicationType_SelectedIndexChanged1" AutoPostBack="true">
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
                                        <asp:DropDownList ID="ddlIssueRequestType" onchange="Fnshowhide_field();" runat="server"
                                            SkinID="dropdownSkin">
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
                    <!-- [CR-03]-Additional dropdown for admin start-->
                    <tr>
                        <td colspan="2" class="rcd-tableCell">
                            <asp:UpdatePanel ID="upAdminZone" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
                                        <table style="width: 100%" border="0" cellspacing="1" cellpadding="1">
                                            <tr>
                                                <td class="rcd-FieldTitle" style="width: 30%">
                                                    Zone
                                                </td>
                                                <td class="rcd-tableCell" style="width: 70%">
                                                    <asp:DropDownList ID="ddlzone" runat="server" SkinID="dropdownSkin" Width="200px"
                                                        OnSelectedIndexChanged="ddlzone_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlzone" runat="server" ControlToValidate="ddlzone"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Select Zone" ValidationGroup="CheckData"
                                                        InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vcerfvddlzone" WarningIconImageUrl="../Images/Warning1.jpg"
                                                        runat="server" TargetControlID="rfvddlzone" CssClass="CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
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
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestSubType" EventName="SelectedIndexChanged">
                                    </asp:AsyncPostBackTrigger>
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <!-- [CR-03]-Additional dropdown for admin end-->
                    <!-- [CR-004]-Additional dropdown for Commercial/Liability quote request start-->
                    <tr>
                        <td colspan="2">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="upCommercial_Liability" runat="server" Visible="false">
                                        <table style="width: 100%" border="0" cellspacing="0" cellpadding="0" class="rcd-TableBorder">
                                            <tr>
                                                <td class="rcd-FieldTitle" style="width: 30%">
                                                    Vertical Name
                                                </td>
                                                <td class="rcd-tableCell" style="width: 70%">
                                                    <asp:DropDownList ID="ddlVertical_Name" runat="server" SkinID="dropdownSkin" Width="200px">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvVertical_Name" runat="server" ControlToValidate="ddlVertical_Name"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Select Vertical Name" ValidationGroup="CheckData"
                                                        InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" WarningIconImageUrl="../Images/Warning1.jpg"
                                                        runat="server" TargetControlID="rfvVertical_Name" CssClass="CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <!-- [CR-004]-Additional dropdown for Commercial/Liability quote request end-->
                    <!-- [CR-002]-Additional dropdown for GMC/GPA start-->
                    <tr>
                        <td colspan="4">
                            <asp:UpdatePanel ID="upGMCGPA" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlGMCGPA" runat="server" Visible="false">
                                        <table style="width: 100%" border="0" cellspacing="1" cellpadding="1" class="rcd-TableBorder">
                                            <tr>
                                                <td class="rcd-FieldTitle" style="width: 25%;">
                                                    Slip Received Date
                                                </td>
                                                <td class="rcd-tableCell" style="width: 25%;">
                                                    <asp:TextBox ID="txt_slip_rdate" runat="server" ReadOnly="true"></asp:TextBox>
                                                    <img alt="Icon" style="cursor: hand" src="../Images/Calander_New.jpg" id="imgFromDate" />
                                                    <cc1:CalendarExtender ID="ceFromDate" Format="MM/dd/yyyy" TargetControlID="txt_slip_rdate"
                                                        PopupButtonID="imgFromDate" runat="server">
                                                    </cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfv_slip_rdate" runat="server" ControlToValidate="txt_slip_rdate"
                                                        ErrorMessage="Select from date" ValidationGroup="CheckData" Display="None" />
                                                    <cc1:ValidatorCalloutExtender ID="vceFromDate" TargetControlID="rfv_slip_rdate" runat="server"
                                                        WarningIconImageUrl="../Images/Warning1.jpg">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                                <td class="rcd-FieldTitle" style="width: 25%;">
                                                    Client Name
                                                </td>
                                                <td class="rcd-tableCell" style="width: 25%;">
                                                    <asp:TextBox ID="txt_Clientname" runat="server" MaxLength="1000">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_Clientname" runat="server" ControlToValidate="txt_Clientname"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Enter Client Name" ValidationGroup="CheckData">
                                                    </asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_Clientname" WarningIconImageUrl="../Images/Warning1.jpg"
                                                        runat="server" TargetControlID="rfv_Clientname" CssClass="CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rcd-FieldTitle" style="width: 25%">
                                                    Location Of Client
                                                </td>
                                                <td class="rcd-tableCell" style="width: 25%">
                                                    <asp:TextBox ID="txt_location_client" runat="server" MaxLength="1000">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_location_client" runat="server" ControlToValidate="txt_location_client"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Enter Location Of Client"
                                                        ValidationGroup="CheckData">
                                                    </asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="cve_location_client" WarningIconImageUrl="../Images/Warning1.jpg"
                                                        runat="server" TargetControlID="rfv_location_client" CssClass="CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                                <td class="rcd-FieldTitle" style="width: 25%">
                                                    Direct/Broker/Agent
                                                </td>
                                                <td class="rcd-tableCell" style="width: 25%">
                                                    <asp:TextBox ID="txt_DirectBrokerAgent" runat="server" MaxLength="1000">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_DirectBrokerAgent" runat="server" ControlToValidate="txt_DirectBrokerAgent"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Enter Direct/Broker/Agent"
                                                        ValidationGroup="CheckData">
                                                    </asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_DirectBrokerAgent" WarningIconImageUrl="../Images/Warning1.jpg"
                                                        runat="server" TargetControlID="rfv_DirectBrokerAgent" CssClass="CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rcd-FieldTitle" style="width: 25%">
                                                    Direct/Broker/Agent Name
                                                </td>
                                                <td class="rcd-tableCell" style="width: 25%">
                                                    <asp:TextBox ID="txt_DirectBrokerAgent_name" runat="server" MaxLength="1000">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_DirectBrokerAgent_name" runat="server" ControlToValidate="txt_DirectBrokerAgent_name"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Enter Direct/Broker/Agent Name"
                                                        ValidationGroup="CheckData">
                                                    </asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_DirectBrokerAgent_name" WarningIconImageUrl="../Images/Warning1.jpg"
                                                        runat="server" TargetControlID="rfv_DirectBrokerAgent_name" CssClass="CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                                <td class="rcd-FieldTitle" style="width: 25%">
                                                    Expiring Policy Inception Date
                                                </td>
                                                <td class="rcd-tableCell" style="width: 25%">
                                                    <asp:TextBox ID="txt_policy_inception_date" runat="server" ReadOnly="true"></asp:TextBox>
                                                    <img alt="Icon" style="cursor: hand" src="../Images/Calander_New.jpg" id="img_policy_inception_date" />
                                                    <cc1:CalendarExtender ID="CalendarExtender1" Format="MM/dd/yyyy" TargetControlID="txt_policy_inception_date"
                                                        PopupButtonID="img_policy_inception_date" runat="server">
                                                    </cc1:CalendarExtender>
                                                    <%--<asp:RequiredFieldValidator ID="rfv_policy_inception_date" runat="server" ControlToValidate="txt_policy_inception_date"
                                                        ErrorMessage="Select from date" ValidationGroup="CheckData" Display="None" />
                                                    <cc1:ValidatorCalloutExtender ID="vce_policy_inception_date" TargetControlID="rfv_policy_inception_date"
                                                        runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rcd-FieldTitle" style="width: 25%">
                                                    Expiry Date
                                                </td>
                                                <td class="rcd-tableCell" style="width: 25%">
                                                    <asp:TextBox ID="txt_expiry_date" runat="server" ReadOnly="true"></asp:TextBox>
                                                    <img alt="Icon" style="cursor: hand" src="../Images/Calander_New.jpg" id="img_expiry_date" />
                                                    <cc1:CalendarExtender ID="CalendarExtender2" Format="MM/dd/yyyy" TargetControlID="txt_expiry_date"
                                                        PopupButtonID="img_expiry_date" runat="server">
                                                    </cc1:CalendarExtender>
                                                    <%-- <asp:RequiredFieldValidator ID="rfv_expiry_date" runat="server" ControlToValidate="txt_expiry_date"
                                                        ErrorMessage="Select from date" ValidationGroup="CheckData" Display="None" />
                                                    <cc1:ValidatorCalloutExtender ID="vce_expiry_date" TargetControlID="rfv_expiry_date"
                                                        runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                </td>
                                                <td class="rcd-FieldTitle" style="width: 25%">
                                                    Expiry TPA
                                                </td>
                                                <td class="rcd-tableCell" style="width: 25%">
                                                    <asp:TextBox ID="txt_expiry_tpa" runat="server" MaxLength="1000">
                                                    </asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="rfv_expiry_tpa" runat="server" ControlToValidate="txt_expiry_tpa"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Enter Expiry TPA" ValidationGroup="CheckData">
                                                    </asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_expiry_tpa" WarningIconImageUrl="../Images/Warning1.jpg"
                                                        runat="server" TargetControlID="rfv_expiry_tpa" CssClass="CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rcd-FieldTitle" style="width: 25%">
                                                    Insurance Company
                                                </td>
                                                <td class="rcd-tableCell" style="width: 25%">
                                                    <asp:TextBox ID="txt_insurance_company" runat="server" MaxLength="1000">
                                                    </asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="rfv_insurance_company" runat="server" ControlToValidate="txt_insurance_company"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Enter Insurance Company"
                                                        ValidationGroup="CheckData">
                                                    </asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_insurance_company" WarningIconImageUrl="../Images/Warning1.jpg"
                                                        runat="server" TargetControlID="rfv_insurance_company" CssClass="CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                </td>
                                                <td class="rcd-FieldTitle" style="width: 25%">
                                                    Expiring Broker
                                                </td>
                                                <td class="rcd-tableCell" style="width: 25%">
                                                    <asp:TextBox ID="txt_expiring_broker" runat="server" MaxLength="1000">
                                                    </asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="rfv_expiring_broker" runat="server" ControlToValidate="txt_expiring_broker"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Enter Expiring Broker" ValidationGroup="CheckData">
                                                    </asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vce_expiring_broker" WarningIconImageUrl="../Images/Warning1.jpg"
                                                        runat="server" TargetControlID="rfv_expiring_broker" CssClass="CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rcd-FieldTitle" style="width: 25%">
                                                    Employer Employee Relationship
                                                </td>
                                                <td class="rcd-tableCell" style="width: 25%">
                                                    <asp:DropDownList ID="ddlEmpRelation" runat="server" SkinID="dropdownSkin" Width="180px">
                                                        <asp:ListItem Text="--Select Relationship--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Employer-Employee" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Non Employer-Employee" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Student" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEmpRelation"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Select Relationship" ValidationGroup="CheckData"
                                                        InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <!-- [CR-002]-Additional dropdown for GMC/GPA end-->
                    <!-- [CR-25]-Additional dropdown for New CR JD start-->
                    <tr>
                        <td colspan="2" class="rcd-tableCell">
                            <asp:UpdatePanel ID="UpChannelMarket" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="PnlChMr" runat="server" Visible="false">
                                        <table style="width: 100%" border="0" cellspacing="1" cellpadding="1">
                                            <tr>
                                                <td class="rcd-FieldTitle" style="width: 30%">
                                                    Select Your Channel
                                                </td>
                                                <td class="rcd-tableCell" style="width: 70%">
                                                    <asp:DropDownList ID="ddlchannelnew" runat="server" SkinID="dropdownSkin" Width="200px">
                                                        <%-- <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlzone"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Select Zone" ValidationGroup="CheckData"
                                                        InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" WarningIconImageUrl="../Images/Warning1.jpg"
                                                        runat="server" TargetControlID="rfvddlzone" CssClass="CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rcd-FieldTitle" style="width: 30%">
                                                    Select Your Market
                                                </td>
                                                <td class="rcd-tableCell" style="width: 70%">
                                                    <asp:DropDownList ID="ddlmarket" runat="server" SkinID="dropdownSkin" Width="200px">
                                                        <%-- <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlRegion"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Select Region" ValidationGroup="CheckData"
                                                        InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" WarningIconImageUrl="../Images/Warning1.jpg"
                                                        runat="server" TargetControlID="rfvddlRegion" CssClass="CustomValidator">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestSubType" EventName="SelectedIndexChanged">
                                    </asp:AsyncPostBackTrigger>
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <!-- [CR-25]-Additional dropdown for New CR JD end-->
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
                        </td>
                    </tr>
                    <!-- [CR-1.0]  fields add - Start -->
                    <tr id="trtotalsum" runat="server" style="display: none;">
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Total Sum Insured
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <asp:TextBox ID="txttotalsuminsured" runat="server" MaxLength="12" onkeypress="return isNumber(event)"
                                SkinID="textboxSkin"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfctottalsuminsured" runat="server" ControlToValidate="txttotalsuminsured"
                                ErrorMessage="Enter total sum insured amount" SetFocusOnError="true" ValidationGroup="CheckData"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="vcetotalsuminsured" CssClass="CustomValidator"
                                WarningIconImageUrl="../Images/Warning1.jpg" TargetControlID="rfctottalsuminsured"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:TextBoxWatermarkExtender ID="TBWSuminsured" runat="server" TargetControlID="txttotalsuminsured"
                                WatermarkText="Type total sum insured " WatermarkCssClass="rcd-txtboxvaluewatermark" />
                        </td>
                    </tr>
                    <tr id="trinsurename" runat="server" style="display: none;">
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Insured Name
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <%-- <nobr>--%>
                            <asp:TextBox ID="txtinsurename" runat="server" MaxLength="250" SkinID="textboxSkin"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInsurerName" runat="server" ControlToValidate="txtinsurename"
                                ErrorMessage="Enter insurer name" SetFocusOnError="true" ValidationGroup="CheckData"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="vceInsurername" CssClass="CustomValidator" WarningIconImageUrl="../Images/Warning1.jpg"
                                TargetControlID="rfvInsurerName" runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:TextBoxWatermarkExtender ID="TBWInsurename" runat="server" TargetControlID="txtinsurename"
                                WatermarkText="Type Insure name" WatermarkCssClass="rcd-txtboxvaluewatermark">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr id="trimdcode" runat="server" style="display: none;">
                        <td class="rcd-FieldTitle" style="width: 30%">
                            IMDCODE
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <asp:TextBox ID="txtimdcode" runat="server" MaxLength="30" onkeypress="return isNumber(event)"
                                onblur="CallWebService()" SkinID="textboxSkin"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TBWIMDcode" runat="server" TargetControlID="txtimdcode"
                                WatermarkText="Type IMD code " WatermarkCssClass="rcd-txtboxvaluewatermark" />
                            <asp:RequiredFieldValidator ID="rfvImdCode" runat="server" ControlToValidate="txtimdcode"
                                ErrorMessage="Enter IMD Code" SetFocusOnError="true" ValidationGroup="CheckData"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="vceIMDCODe" CssClass="CustomValidator" WarningIconImageUrl="../Images/Warning1.jpg"
                                TargetControlID="rfvImdCode" runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr id="tragentcode" runat="server" style="display: none;">
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Agent Code
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <nobr>
                          <asp:TextBox ID="txtagentcode" runat="server" MaxLength="250" 
                          SkinID="textboxSkin"></asp:TextBox>                            
                                                                              
                          <cc1:TextBoxWatermarkExtender ID="TBWagentcode" runat="server" TargetControlID="txtagentcode"
                           WatermarkText="Type Agent code" WatermarkCssClass="rcd-txtboxvaluewatermark"></cc1:TextBoxWatermarkExtender>

                            <asp:RequiredFieldValidator ID="rfvAgentCode" runat="server" ControlToValidate="txtagentcode"
                                ErrorMessage="Enter Agent Code" SetFocusOnError="true" ValidationGroup="CheckData"
                                Display="None" />                        
                          <cc1:ValidatorCalloutExtender ID="vceAgentCOde" CssClass="CustomValidator"
                                WarningIconImageUrl="../Images/Warning1.jpg" TargetControlID="rfvAgentCode"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr id="trddloccupany" runat="server" style="display: none;">
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Occupancy (Under Fire)
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddloccupany" runat="server" Style="width: 200px;" SkinID="dropdownSkin">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvddloccupancy" runat="server" ControlToValidate="ddloccupany"
                                            ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Select Occupancy" />
                                        <cc1:ValidatorCalloutExtender ID="vceOccupancy" TargetControlID="rfvddloccupancy"
                                            runat="server" WarningIconImageUrl="../Images/Warning1.jpg" CssClass="CustomValidator">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <cc1:CascadingDropDown ID="ccdddloccupany" TargetControlID="ddloccupany" PromptText="Select Occupany"
                                            Category="Occupany" ServicePath="../WebServices/WebService.asmx" ServiceMethod="GETOccupancy_Under_Fire"
                                            runat="server">
                                        </cc1:CascadingDropDown>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!-- [CR-1.0]  fields add - End -->
                    <!-- [CR-34] Proposal No field add - Start -->
                    <tr id="policytable" runat="server">
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Policy/Claim/Proposal/CoverNote Number
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <asp:UpdatePanel ID="uplProposalNo" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtProposalNo" runat="server" MaxLength="18" SkinID="textboxSkin"
                                        Width="250px"></asp:TextBox>
                                    <cc1:TextBoxWatermarkExtender ID="twetxtProposalNo" runat="server" TargetControlID="txtProposalNo"
                                        WatermarkText="Type Policy/Claim/Proposal/CoverNote Number" WatermarkCssClass="rcd-txtboxvaluewatermark">
                                    </cc1:TextBoxWatermarkExtender>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestSubType" EventName="SelectedIndexChanged">
                                    </asp:AsyncPostBackTrigger>
                                    <asp:PostBackTrigger ControlID="txtProposalNo" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <!--added by shilpa-->
                    <tr id="Tr1" runat="server" style="display: none;">
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Policy No
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtProposalNo12" runat="server" MaxLength="18" SkinID="textboxSkin"
                                        Width="250px"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProposalNo12"
                                ErrorMessage="Enter Policy No" SetFocusOnError="true" ValidationGroup="CheckData"
                                Display="None" />
                            
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" CssClass="CustomValidator" WarningIconImageUrl="../Images/Warning1.jpg"
                                TargetControlID="RequiredFieldValidator1" runat="server">
                            </cc1:ValidatorCalloutExtender>--%>
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtProposalNo12"
                                        WatermarkText="Type Policy Number" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestSubType" EventName="SelectedIndexChanged">
                                    </asp:AsyncPostBackTrigger>
                                    <asp:PostBackTrigger ControlID="txtProposalNo12" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <!-- [CR-34] Proposal No field add - End -->
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
                        <td colspan="2" class="rcd-tableCell">
                            <asp:UpdatePanel ID="UpdatePanelRN" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table id="tblPolicyNoRN" runat="server" visible="false" width="100%" cellspacing="0"
                                        cellpadding="0">
                                        <tr>
                                            <td class="rcd-FieldTitle" style="width: 30%">
                                                Please enter Policy No (Renewal Notice)
                                            </td>
                                            <td class="rcd-tableCell" style="width: 70%">
                                                <asp:TextBox ID="txtPolicyNoRN" runat="server" MaxLength="16" SkinID="textboxSkin"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender ID="TWPolicyNoRN" runat="server" TargetControlID="txtPolicyNoRN"
                                                    WatermarkText="Enter Policy No Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                                <asp:RequiredFieldValidator ID="rfvPolicyNoRN" runat="server" ControlToValidate="txtPolicyNoRN"
                                                    ErrorMessage="Enter Policy No Here" SetFocusOnError="true" ValidationGroup="CheckData"
                                                    Display="None" />
                                                <cc1:ValidatorCalloutExtender ID="vcePolicyNoRN" WarningIconImageUrl="../Images/Warning1.jpg"
                                                    TargetControlID="rfvPolicyNoRN" runat="server" CssClass="CustomValidator">
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
                                SkinID="buttonSkin" OnClick="btnRegisterCall_Click" OnClientClick="CheckFileExists()" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" SkinID="buttonSkin" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="paneldesc" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlMyPanel" runat="server" Style="display: none" Height="250px" Width="500px"
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
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server" Style="display: none" Height="250px" Width="500px"
                                        CssClass="modalPopup2">
                                        <div style="color: White; font-size: 14" class="rcd-TopHeaderBlue">
                                            For Your Information.
                                        </div>
                                        <%--<hr class="hrLine" />--%>
                                        <table cellpadding="1" cellspacing="1" width="100%" class="rcd-Instruct" style="font-size: 11">
                                            <tr>
                                                <td>
                                                    <p>
                                                        <asp:Label ID="lblVerifyDesc" runat="server"></asp:Label></p>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%--<asp:Label ID="Label3" runat="server" Text="Do you still want to log a call ?"></asp:Label>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblControlValidate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <center>
                                            <asp:Button ID="btnnoVerify" OnClick="btnnoVerify_Click" runat="server" Text="Verify ID"
                                                SkinID="buttonSkin" />
                                            <asp:Button ID="btnyesVerify" runat="server" Text="Raise Ticket" SkinID="buttonSkin" />
                                        </center>
                                    </asp:Panel>
                                    <cc1:ModalPopupExtender ID="popup_Verify" runat="server" TargetControlID="lblControlValidate"
                                        PopupControlID="Panel1" CancelControlID="btnyesVerify" Enabled="true" DropShadow="false"
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
    </table>
    <asp:HiddenField ID="hdnimdcode" runat="server" />
    <asp:HiddenField ID="hdnoccupancy" runat="server" />
</asp:Content>
