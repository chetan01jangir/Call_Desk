<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile"
    AutoEventWireup="true" CodeFile="EditAIRSubtypeMapping.aspx.cs" Inherits="Admin_EditAIRSubtypeMapping"
    Title=":: Call Desk - Edit Application Issue Request Subtype Mapping ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <script type="text/javascript">
    function checkDate(sender,args)
    {
        document.getElementById('ctl00_ContentPlaceHolder1_txtValidTo').value = "";
        
        var dateSelected = sender._selectedDate;
        
        var currentDate = new Date();
        var currentDay = currentDate.getDate();
        var currentMonth = currentDate.getMonth();
        var currentYear = currentDate.getFullYear();
        
        var newDate = new Date(currentYear, currentMonth, currentDay);                      
                
        if (dateSelected < newDate)
        {            
            alert("You cannot select a day earlier than today!");
            sender._selectedDate = new Date(); 
            // set the date back to the current date
            sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        }
    }

    function checkToDate(sender,args)
    {        
        var FromDate = document.getElementById('ctl00_ContentPlaceHolder1_txtValidFrom');
        var x = FromDate.value;
        var arr1 = x.split('/');
        
        var date1 = new Date(arr1[2], arr1[1] - 1, arr1[0]);
                                                       
        if(FromDate.value != "")
        {                                                                                            
            if (sender._selectedDate <= date1)
            {                   
                alert("Select a date greater than valid from date!");
                sender._textbox.set_Value('');
            }
        }
        else
        {            
            alert("Select valid from date first.");
            sender._textbox.set_Value('')
        }
    }

    function NumericCharacter(varTextBox)
        {
         if (event.keyCode < 48 ||event.keyCode > 57)
            {
                 event.returnValue = false;
            }
        }

    function HideTableRow()
    {
         var ddlIssueRequest = document.getElementById('ctl00_ContentPlaceHolder1_ddlIssueRequest');

            if(ddlIssueRequest.value == '0')
            {
                document.getElementById('ctl00_ContentPlaceHolder1_trMail').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trSMS').style.display= 'none';
                //document.getElementById('ctl00_ContentPlaceHolder1_trRole').style.display= 'none';
                //document.getElementById('ctl00_ContentPlaceHolder1_rfvRole').enabled = false;
            }
            else if(ddlIssueRequest.value == 'Issue')
            {
                document.getElementById('ctl00_ContentPlaceHolder1_trMail').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trSMS').style.display= 'none';
                //document.getElementById('ctl00_ContentPlaceHolder1_trRole').style.display= 'none';
                //document.getElementById('ctl00_ContentPlaceHolder1_rfvRole').enabled = false;
            }
            else if(ddlIssueRequest.value == 'Request')
            {
                document.getElementById('ctl00_ContentPlaceHolder1_trMail').style.display= 'inline';
                document.getElementById('ctl00_ContentPlaceHolder1_trSMS').style.display= 'inline';
                //document.getElementById('ctl00_ContentPlaceHolder1_trRole').style.display= 'inline';
                //document.getElementById('ctl00_ContentPlaceHolder1_rfvRole').enabled = true;
            }
    }
    
     function CheckIRType()
     {
        var ddlIssueRequest = document.getElementById('ctl00_ContentPlaceHolder1_ddlIssueRequest');
        var ddlSMS = document.getElementById('ctl00_ContentPlaceHolder1_ddlSMS');
        var ddlMail = document.getElementById('ctl00_ContentPlaceHolder1_ddlMail');

        if(ddlIssueRequest.value == 'Issue')
        {
            alert('For Issue SMS and Email can not be sent!');

            if(ddlSMS.value = '0')
            {
                document.getElementById('ctl00_ContentPlaceHolder1_ddlSMS').value = '1';
            }
            if(ddlMail.value = '0')
            {
                document.getElementById('ctl00_ContentPlaceHolder1_ddlMail').value = '1';
            }
        }
        else if(ddlIssueRequest.value == '0')
        {
            alert('Please first select issue request type!');
            if(ddlSMS.value = '0')
            {
                document.getElementById('ctl00_ContentPlaceHolder1_ddlSMS').value = '1';
            }
            if(ddlMail.value = '0')
            {
                document.getElementById('ctl00_ContentPlaceHolder1_ddlMail').value = '1';
            }
        }
     }
     
     function ChkUnChk(chk,lst)//('chkListFromCountry','ListFromCountry')   
    {
        var Count;
                
        var name=document.getElementById(lst)

        var RowCount= name.getElementsByTagName('TR');

        for (Count = 0; Count < RowCount.length ; Count++)
        { 
            document.getElementById(lst+"_"+ Count).checked=document.getElementById(chk).checked                        
        }
    }

    function CheckUnCheckAll(chk,lst)
    {
        var chkAll = document.getElementById(chk);
        
        if(chkAll.checked)
        {
            chkAll.checked = false;
        }
        else
        {
            var name = document.getElementById(lst)
            var RowCount = name.getElementsByTagName('TR');
            var IsValid = true;
            for (Count = 0; Count < RowCount.length ; Count++)
            { 
                if(document.getElementById(lst+"_"+ Count).checked == false)
                {
                    IsValid = false;              
                }
            }
            if(IsValid)
            {
                document.getElementById(chk).checked = true;
            }
            else
            {
                document.getElementById(chk).checked = false;
            }
        }
    }

    function chqLocationTypeSelected(source, args)
    {
       var chkListaTipoModificaciones= document.getElementById ('<%= chkLocationType.ClientID %>');
       var chkLista= chkListaTipoModificaciones.getElementsByTagName("input");
       for(var i=0;i<chkLista.length;i++)
        {  
            if(chkLista[i].checked)
            {
                args.IsValid = true;
                return;
            }
        }
      args.IsValid = false;
    }

    </script>

    <table width="100%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue">
                            Edit Application Issue Request Subtype Mapping
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
                            Applications
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:Label ID="lblApplication" SkinID="SkinLabel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%; height: 18px;">
                            Mapped Issue / Request
                        </td>
                        <td class="rcd-tableCell" style="width: 60%; height: 18px;">
                            <asp:Label ID="lblIssueRequestType" SkinID="SkinLabel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%; height: 18px;">
                            Mapped Issue / Request SubType
                        </td>
                        <td class="rcd-tableCell" style="width: 60%; height: 18px;">
                            <asp:Label ID="lblIssueRequestSubType" SkinID="SkinLabel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Issue / Request
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlIssueRequest" runat="server" SkinID="dropdownSkin">
                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="Issue">Issue</asp:ListItem>
                                <asp:ListItem Value="Request">Request</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvIssueRequest" InitialValue="0" runat="server"
                                ControlToValidate="ddlIssueRequest" Display="None" SetFocusOnError="true" ValidationGroup="CheckData"
                                ErrorMessage="Please Select Issue/Request">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rfvIssueRequest"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr id="trApproverAuthority" runat="server" visible="false">
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Approver Authority
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlApproverAuthority" SkinID="dropdownSkin" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvApproverAuthority" InitialValue="0" runat="server"
                                ControlToValidate="ddlApproverAuthority" Display="None" SetFocusOnError="true"
                                ErrorMessage="Please Select Approver Authority" ValidationGroup="CheckData">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="rfvApproverAuthority"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr id="trRole" runat="server" visible="false">
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Approver Role
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlRole" SkinID="dropdownSkin" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvRole" InitialValue="0" runat="server" ControlToValidate="ddlRole"
                                Display="None" SetFocusOnError="true" ErrorMessage="Please select role" ValidationGroup="CheckData">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceRole" TargetControlID="rfvRole" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr id="trMail" runat="server">
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Send Mail
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlMail" runat="server" SkinID="dropdownSkin" onchange="CheckIRType();">
                                            <%--<asp:ListItem Selected="True" Text="--Select--" Value="--Select--" />--%>
                                            <asp:ListItem Text="Yes" Value="0" />
                                            <asp:ListItem Text="No" Value="1" />
                                        </asp:DropDownList>
                                    </td>
                                    <%--<td>
                                        <asp:RequiredFieldValidator ID="rfvMail" runat="server" ControlToValidate="ddlMail"
                                            Display="None" SetFocusOnError="true" InitialValue="--Select--" ValidationGroup="CheckData" ErrorMessage="Please select the Value">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <cc1:ValidatorCalloutExtender ID="vceMail" TargetControlID="rfvMail" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>--%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trSMS" runat="server">
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Send SMS
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlSMS" runat="server" SkinID="dropdownSkin" onchange="CheckIRType();">
                                            <%--<asp:ListItem Selected="True" Value="--Select--" Text="--Select--" />--%>
                                            <asp:ListItem Text="Yes" Value="0" />
                                            <asp:ListItem Text="No" Value="1" />
                                        </asp:DropDownList>
                                    </td>
                                    <%--<td>
                                        <asp:RequiredFieldValidator ID="rfvSMS" runat="server" ControlToValidate="ddlSMS"
                                            Display="None" SetFocusOnError="true" InitialValue="--Select--" ValidationGroup="CheckData" ErrorMessage="Please select the value">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <cc1:ValidatorCalloutExtender ID="vceSMS" TargetControlID="rfvSMS" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>--%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Valid From
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtValidFrom" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 26px; height: 42px">
                                        <img alt="Icon" style="cursor: hand" src="../Images/Calander_New.jpg" id="imgFromDate" />
                                    </td>
                                    <td>
                                        <cc1:CalendarExtender ID="ceFromDate" OnClientDateSelectionChanged="checkDate" Format="dd/MM/yyyy"
                                            TargetControlID="txtValidFrom" PopupButtonID="imgFromDate" runat="server">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtValidFrom"
                                            ErrorMessage="Select valid from date" Font-Bold="True" Font-Size="Medium" ValidationGroup="CheckData"
                                            Display="None" />
                                    </td>
                                    <td>
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
                            Valid To
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtValidTo" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img alt="Icon" style="cursor: hand" src="../Images/Calander_New.jpg" id="imgToDate" />
                                    </td>
                                    <td>
                                        <cc1:CalendarExtender ID="ceToDate" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkToDate"
                                            TargetControlID="txtValidTo" PopupButtonID="imgToDate" runat="server">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ControlToValidate="txtValidTo"
                                            ErrorMessage="Select valid till date" Font-Bold="True" Font-Size="Medium" ValidationGroup="CheckData"
                                            Display="None" />
                                    </td>
                                    <td>
                                        <cc1:ValidatorCalloutExtender ID="vceToDate" TargetControlID="rfvValidTo" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Comments
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Add comment" />
                            <cc1:ValidatorCalloutExtender ID="vceComment" TargetControlID="rfvComment" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtComment"
                                WatermarkText="Type Comment Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Priority
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlPriority" runat="server" SkinID="dropdownSkin">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                                <asp:ListItem>Critical</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPriority" runat="server" InitialValue="--Select--"
                                ControlToValidate="ddlPriority" ValidationGroup="CheckData" SetFocusOnError="true"
                                Display="None" ErrorMessage="Please Select Priority" />
                            <cc1:ValidatorCalloutExtender ID="vcePriority" TargetControlID="rfvPriority" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Processing Groups
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlGroups" runat="server" SkinID="dropdownSkin">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem>IT</asp:ListItem>
                                <asp:ListItem>Hp</asp:ListItem>
                                <asp:ListItem>Seles</asp:ListItem>
                                <asp:ListItem>Operations</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvProcessingGroups" runat="server" InitialValue="--Select--"
                                ControlToValidate="ddlGroups" ValidationGroup="CheckData" SetFocusOnError="true"
                                Display="None" ErrorMessage="Please Select Priority" />
                            <cc1:ValidatorCalloutExtender ID="vceProcessingGroups" TargetControlID="rfvProcessingGroups"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Call TAT
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtCallTAT" runat="server" SkinID="textboxSkin" onkeypress="NumericCharacter(this)"
                                MaxLength="3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCallTAT" runat="server" ControlToValidate="txtCallTAT"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Please Enter Call TAT" />
                            <cc1:ValidatorCalloutExtender ID="vceCallTAT" TargetControlID="rfvCallTAT" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <%--<cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtCallTAT"
                                WatermarkText="Type Call TAT Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Location Type
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:CheckBox ID="chkSelectAll" Text="All" runat="server" />
                            <asp:CheckBoxList ID="chkLocationType" runat="server">
                            </asp:CheckBoxList>
                            <asp:CustomValidator ID="cvSelectLocationType" Enabled="true" EnableClientScript="true"
                                ValidationGroup="CheckData" runat="server" ErrorMessage="Select atleast one location type"
                                ClientValidationFunction="chqLocationTypeSelected"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnUpdate" ValidationGroup="CheckData" runat="server" Text="Update"
                                SkinID="buttonSkin" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" SkinID="buttonSkin" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>

                <script language="javascript" type="text/javascript">
                    HideTableRow();
                </script>

            </td>
        </tr>
    </table>
</asp:Content>
