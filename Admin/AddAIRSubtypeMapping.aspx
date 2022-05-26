<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="AddAIRSubtypeMapping.aspx.cs"
    Inherits="Admin_AddAIRSubtypeMapping" Title=":: Call Desk - Add Application Issue Request Subtype Mapping ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <script type="text/javascript">
    
        var _source;
         var _popup;
         function showConfirm(source, mdlPopup)
         {
             this._source = source;
             this._popup = $find(mdlPopup);
             this._popup.show();
         }
         function okClick()
         {
             this._popup.hide();
             __doPostBack(this._source.name, '');
         }
         function cancelClick()
         {  
             this._popup.hide();
             this._source = null;
             this._popup = null;
         }
         
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
    
    function NumericCharacter(varTextBox)
        {        
         if (event.keyCode < 48 ||event.keyCode > 57)
            {              
                 event.returnValue = false;         
            }
        }
            
     function CheckIRType()
     {
        var ddlIssueRequest = document.getElementById('<%=ddlIssueRequest.ClientID%>')
        var ddlSMS = document.getElementById('<%=ddlSMS.ClientID%>')
        var ddlMail = document.getElementById('<%=ddlMail.ClientID%>')        

        
        if(ddlIssueRequest.value == 'Issue')
        {        
            alert('For Issue SMS and Email can not be sent!');
            
            if(ddlSMS.value = '0')
            {                
                ddlSMS.value = '1';
            }
            if(ddlMail.value = '0')
            {                
                ddlMail.value = '1';
            }
        }
        else if(ddlIssueRequest.value == '0')
        {
            alert('Please first select issue request type!');
            if(ddlSMS.value = '0')
            {
                ddlSMS.value = '1';
            }
            if(ddlMail.value = '0')
            {
                ddlMail.value = '1';
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
            document.getElementById(lst+"_"+ Count).checked = document.getElementById(chk).checked
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
//        var Count;                
//        var name = document.getElementById(lst);
//        var RowCount= name.getElementsByTagName('TR');
//        for (Count = 0; Count < RowCount.length ; Count++)
//        { 
//            if(document.getElementById(lst+"_"+ Count).checked == false)
//            {
//                document.getElementById(chk).checked = false
//            }            
//        }
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

    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue">
                            Add Application Issue / Request Subtype Mapping
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
                <asp:Literal ID="LitMessage" runat="server"></asp:Literal>
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
                            <asp:UpdatePanel id="UpdatePanel6" runat="server">
                                <contenttemplate>
                                    <asp:DropDownList ID="ddlApplications" runat="server" SkinID="dropdownSkin" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlApplications_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </contenttemplate>
                            </asp:UpdatePanel>
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
                            Mapped Issue / Request
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:UpdatePanel id="UpdatePanel5" runat="server">
                                <contenttemplate>
                                    <asp:DropDownList id="ddlIssueRequestTypes" runat="server" SkinID="dropdownSkin" 
                                    OnSelectedIndexChanged="ddlIssueRequestTypes_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList> 
                                </contenttemplate>
                            </asp:UpdatePanel>
                            <asp:RequiredFieldValidator ID="rfvIssueRequestTypes" InitialValue="0" runat="server"
                                ControlToValidate="ddlIssueRequestTypes" ValidationGroup="CheckData" SetFocusOnError="true"
                                Display="None" ErrorMessage="Select issue / request type" />
                            <cc1:ValidatorCalloutExtender ID="vceIssueRequestTypes" TargetControlID="rfvIssueRequestTypes"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%; height: 18px;">
                            Issue / Request Sub Types
                        </td>
                        <td class="rcd-tableCell" style="width: 60%; height: 18px;">
                            <table width="100%">
                                <tr>
                                    <td style="width: 45%" align="right">
                                        <asp:UpdatePanel id="UpdatePanel1" runat="server">
                                            <contenttemplate>
                                            <asp:ListBox ID="lstbxIssueRequestSubType" runat="server" SelectionMode="Multiple"></asp:ListBox>
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
                                                            <asp:Button id="btnRemove" onclick="btnRemove_Click" runat="server" SkinID="buttonSkin" Text="<" Width="25px" __designer:wfdid="w2"></asp:Button>
                                                        </contenttemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:UpdatePanel id="UpdatePanel4" runat="server">
                                            <contenttemplate>
                                                <asp:ListBox ID="lstbxSelectedIssueRequestSubType" runat="server" SelectionMode="Multiple">
                                                </asp:ListBox>
                                            </contenttemplate>
                                        </asp:UpdatePanel>
                                        <asp:RequiredFieldValidator ID="rfvSelectedIR" runat="server" ControlToValidate="lstbxSelectedIssueRequestSubType"
                                            ErrorMessage="Select atleast one issue request subtype" InitialValue="" Font-Bold="True"
                                            Font-Size="Medium" ValidationGroup="CheckData" Display="None" />
                                        <cc1:ValidatorCalloutExtender ID="vceSelectedIR" TargetControlID="rfvSelectedIR"
                                            runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%; height: 18px;">
                            Issue / Request
                        </td>
                        <td class="rcd-tableCell" style="width: 60%; height: 18px;">
                            <asp:DropDownList ID="ddlIssueRequest" SkinID="dropdownSkin" runat="server">
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
                                            <asp:ListItem Text="Yes" Value="0" />
                                            <asp:ListItem Text="No" Value="1" Selected="True" />
                                        </asp:DropDownList>
                                    </td>
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
                                            <asp:ListItem Text="Yes" Value="0" />
                                            <asp:ListItem Text="No" Value="1" Selected="True" />
                                        </asp:DropDownList>
                                    </td>
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
                                    <td>
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
                                            Display="None">
                                        </asp:RequiredFieldValidator>
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
                                            Display="None">
                                        </asp:RequiredFieldValidator>
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
                            Call TAT (In Hours)</td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtCallTAT" runat="server" SkinID="textboxSkin" MaxLength="3" onkeypress="NumericCharacter(this)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCallTAT" runat="server" ControlToValidate="txtCallTAT"
                                Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Call TAT" ValidationGroup="CheckData">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceCallTAT" TargetControlID="rfvCallTAT" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtCallTAT"
                                WatermarkText="Type Call TAT Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Priority
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlPriority" runat="server" SkinID="dropdownSkin">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                                <asp:ListItem>Critical</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPriority" runat="server" ControlToValidate="ddlPriority"
                                InitialValue="0" Display="None" SetFocusOnError="true" ErrorMessage="Please Select Priority"
                                ValidationGroup="CheckData">
                            </asp:RequiredFieldValidator>
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
                            <asp:DropDownList ID="ddlGroups" runat="server" SkinID="dropdownSkin" />
                            <asp:RequiredFieldValidator ID="rfvProcessingGroups" runat="server" ControlToValidate="ddlGroups"
                                InitialValue="0" Display="None" SetFocusOnError="true" ErrorMessage="Please Select Processing Group"
                                ValidationGroup="CheckData">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceProcessingGroups" TargetControlID="rfvProcessingGroups"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Location Type
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <%--<asp:DropDownList ID="ddlLocationType" runat="server" SkinID="dropdownSkin">
                            </asp:DropDownList>--%>
                            <asp:CheckBox ID="chkSelectAll" Text="All" runat="server" />
                            <asp:CheckBoxList ID="chkLocationType" runat="server">
                            </asp:CheckBoxList>
                            <asp:CustomValidator ID="cvSelectLocationType" Enabled="true" EnableClientScript="true"
                                ValidationGroup="CheckData" runat="server" ErrorMessage="Select atleast one location type"
                                ClientValidationFunction="chqLocationTypeSelected"></asp:CustomValidator>
                            <%--<cc1:ValidatorCalloutExtender ID="vceLocationType" runat="server" TargetControlID="cvSelectLocationType"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnCreateMapping" ValidationGroup="CheckData" runat="server" Text="Create Mapping"
                                SkinID="buttonSkin" OnClick="btnCreateMapping_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Clear" SkinID="buttonSkin" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-tableCellCenterAlign" colspan="2">
                            <asp:GridView ID="gvAIRSTMapping" AutoGenerateColumns="False" SkinID="gridviewSkin"
                                AllowPaging="True" runat="server" OnPageIndexChanging="gvAIRSTMapping_PageIndexChanging"
                                OnRowCommand="gvAIRSTMapping_RowCommand" ShowFooter="True" OnRowDataBound="gvAIRSTMapping_RowDataBound"
                                OnRowCreated="gvAIRSTMapping_RowCreated">
                                <Columns>
                                    <asp:TemplateField HeaderText="View Details">
                                        <ItemTemplate>
                                            <asp:Image ID="imgLetter" runat="server" ImageUrl="~/images/icon-details.gif" />
                                            <cc1:PopupControlExtender ID="popCtrlExtAddPopUp" runat="server" DynamicServiceMethod="GetDynamicContent"
                                                DynamicContextKey='<%# Eval("RowID") %>' DynamicControlID="pnlAddPopUP" TargetControlID="imgLetter"
                                                PopupControlID="pnlAddPopUP" Position="Bottom">
                                            </cc1:PopupControlExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Application Name" FooterStyle-ForeColor="Black" FooterStyle-Font-Bold="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApplicationName" runat="server" ToolTip='<%# Bind("ApplicationName") %>'
                                                Text='<%# Bind("ApplicationName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlApplicationName" runat="server" SkinID="dropdownSkin" Style="z-index: 999;">
                                            </asp:DropDownList>
                                            <cc1:CascadingDropDown ID="ccdApplication" Category="category" TargetControlID="ddlApplicationName"
                                                PromptText="Select Application" LoadingText="Loading Text..." ServicePath="../WebServices/WebService.asmx"
                                                ServiceMethod="GetApplicationTypes" runat="server">
                                            </cc1:CascadingDropDown>
                                            <asp:RequiredFieldValidator ID="rfvApplicationName1" runat="server" ControlToValidate="ddlApplicationName"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please select application"
                                                ValidationGroup="CheckData1">
                                            </asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceApplicationName1" TargetControlID="rfvApplicationName1"
                                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg" CssClass="CustomValidator">
                                            </cc1:ValidatorCalloutExtender>
                                            <asp:Button runat="server" ID="btnSort" ValidationGroup="CheckData1" Text="Filter"
                                                CommandName="btnSortApplications" SkinID="buttonSkin" />
                                        </FooterTemplate>
                                        <HeaderStyle Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IssueRequestType" HeaderText="Issue Request Type">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IssueRequestSubType" HeaderText="Issue Request Sub Type">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IssueRequest" HeaderText="Issue / Request">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ApproverRole" HeaderText="Approver" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Priority" HeaderText="Priority" Visible="false" />
                                    <asp:TemplateField HeaderText="Purpose" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" ToolTip='<%# Bind("Description") %>'
                                                Text='<%# Eval("Description")==DBNull.Value ? "" :Eval("Description").ToString().Substring(0,Math.Min(20,Eval("Description").ToString().Length))+"...." %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkbtnEdit" runat="server" CommandArgument='<%# Bind("RowID") %>'
                                                CausesValidation="false" CommandName="lnkEdit" ToolTip="Edit" ImageUrl="~/Images/edit3.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkbtnDelete" runat="server" CommandArgument='<%# Bind("RowID") %>'
                                                OnClientClick="showConfirm(this); return false;" CausesValidation="false" CommandName="lnkDelete"
                                                ImageUrl="~/Images/delete.jpg" ToolTip="Delete" />
                                            <cc1:ModalPopupExtender ID="md1" runat="server" BackgroundCssClass="modalBackground"
                                                BehaviorID="mdlPopup" CancelControlID="btnNo" OkControlID="btnOk" OnCancelScript="cancelClick();"
                                                OnOkScript="okClick();" PopupControlID="div" TargetControlID="div">
                                            </cc1:ModalPopupExtender>
                                            <div id="div" runat="server" class="confirm" style="display: none; text-align: center">
                                                Are you sure you want to delete ?
                                                <asp:ImageButton ID="btnOk" runat="server" ImageUrl="~/Images/smallsuccess.gif" ToolTip="Yes"
                                                    Width="22px" Height="22px" />
                                                <asp:ImageButton ID="btnNo" runat="server" ImageUrl="~/Images/Delete.gif" Width="22px"
                                                    Height="22px" ToolTip="No" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="False" CommandName="lnkDetails"
                                                Text="Details" CommandArgument='<%# Eval("RowID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                            <asp:Panel ID="pnlAddPopUP" runat="server" Style="z-index: 1000; text-transform: uppercase;">
                            </asp:Panel>
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
