<%@ Page Language="C#" AutoEventWireup="true"
     CodeFile="MTrackCall.aspx.cs" Inherits="_Default" Title=":: Call Desk - Track Call ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="CP" TagName="TitleBar" Src="~/UserControls/MenuUserControl.ascx" %>
 <link href="../App_Themes/mrcd.css" rel="stylesheet" type="text/css" />
<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
--%>
    <script type="text/javascript">
        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>

    <%--<script language="javascript" type="text/javascript">

        function SearchCriteria() {
            var ddlSearch = document.getElementById('<%=ddlSearch.ClientID%>');

            if (ddlSearch.value == '1') {
                document.getElementById('<%=trFromDate.ClientID%>').style.display = 'inline';
                document.getElementById('<%=trToDate.ClientID%>').style.display = 'inline';
                document.getElementById('<%=trApplication.ClientID%>').style.display = 'inline';
                //var _ddlDept = document.getElementById('ctl00_ContentPlaceHolder1_ddlApplication')
                //_ddlDept.selectedIndex=0

                document.getElementById('<%=trTicketNo.ClientID%>').style.display = 'none';
                document.getElementById('<%=trCallStatus.ClientID%>').style.display = 'none';
                document.getElementById('<%=trPolicyNo.ClientID%>').style.display = 'none';

                document.getElementById('<%=rfvFromDate.ClientID%>').enabled = true;
                document.getElementById('<%=rfvtxtToDate.ClientID%>').enabled = true;
                document.getElementById('<%=rfvTicketNo.ClientID%>').enabled = false;
                document.getElementById('<%=rfvCallStatus.ClientID%>').enabled = false;
                document.getElementById('<%=rfvPolicyNo.ClientID%>').enabled = false;

            }
            else if (ddlSearch.value == '2') {
                document.getElementById('<%=trFromDate.ClientID%>').style.display = 'none';
                document.getElementById('<%=trToDate.ClientID%>').style.display = 'none';
                document.getElementById('<%=trApplication.ClientID%>').style.display = 'none';
                document.getElementById('<%=trTicketNo.ClientID%>').style.display = 'inline';
                document.getElementById('<%=trCallStatus.ClientID%>').style.display = 'none';
                document.getElementById('<%=trPolicyNo.ClientID%>').style.display = 'none';

                document.getElementById('<%=rfvFromDate.ClientID%>').enabled = false;
                document.getElementById('<%=rfvtxtToDate.ClientID%>').enabled = false;
                document.getElementById('<%=rfvTicketNo.ClientID%>').enabled = true;
                document.getElementById('<%=rfvCallStatus.ClientID%>').enabled = false;
                document.getElementById('<%=rfvPolicyNo.ClientID%>').enabled = false;

            }
            else if (ddlSearch.value == '3') {
                document.getElementById('<%=trFromDate.ClientID%>').style.display = 'none';
                document.getElementById('<%=trToDate.ClientID%>').style.display = 'none';
                document.getElementById('<%=trApplication.ClientID%>').style.display = 'inline';
                //var _ddlDept = document.getElementById('ctl00_ContentPlaceHolder1_ddlApplication')
                //_ddlDept.selectedIndex=0   
                document.getElementById('<%=trTicketNo.ClientID%>').style.display = 'none';
                document.getElementById('<%=trCallStatus.ClientID%>').style.display = 'inline';
                document.getElementById('<%=trPolicyNo.ClientID%>').style.display = 'none';

                document.getElementById('<%=rfvFromDate.ClientID%>').enabled = false;
                document.getElementById('<%=rfvtxtToDate.ClientID%>').enabled = false;
                document.getElementById('<%=rfvTicketNo.ClientID%>').enabled = false;
                document.getElementById('<%=rfvCallStatus.ClientID%>').enabled = true;
                document.getElementById('<%=rfvPolicyNo.ClientID%>').enabled = false;

            }
            else if (ddlSearch.value == '4') {
                document.getElementById('<%=trFromDate.ClientID%>').style.display = 'none';
                document.getElementById('<%=trToDate.ClientID%>').style.display = 'none';
                document.getElementById('<%=trApplication.ClientID%>').style.display = 'none';
                document.getElementById('<%=trTicketNo.ClientID%>').style.display = 'none';
                document.getElementById('<%=trCallStatus.ClientID%>').style.display = 'none';
                document.getElementById('<%=trPolicyNo.ClientID%>').style.display = 'inline';

                document.getElementById('<%=rfvFromDate.ClientID%>').enabled = false;
                document.getElementById('<%=rfvtxtToDate.ClientID%>').enabled = false;
                document.getElementById('<%=rfvTicketNo.ClientID%>').enabled = false;
                document.getElementById('<%=rfvCallStatus.ClientID%>').enabled = false;
                document.getElementById('<%=rfvPolicyNo.ClientID%>').enabled = true;


            }
            else {
                document.getElementById('<%=trFromDate.ClientID%>').style.display = 'none';
                document.getElementById('<%=trToDate.ClientID%>').style.display = 'none';
                document.getElementById('<%=trApplication.ClientID%>').style.display = 'none';
                document.getElementById('<%=trTicketNo.ClientID%>').style.display = 'none';
                document.getElementById('<%=trCallStatus.ClientID%>').style.display = 'none';
                document.getElementById('<%=trPolicyNo.ClientID%>').style.display = 'none';

                document.getElementById('<%=rfvFromDate.ClientID%>').enabled = false;
                document.getElementById('<%=rfvtxtToDate.ClientID%>').enabled = false;
                document.getElementById('<%=rfvTicketNo.ClientID%>').enabled = false;
                document.getElementById('<%=rfvCallStatus.ClientID%>').enabled = false;
                document.getElementById('<%=rfvPolicyNo.ClientID%>').enabled = false;

            }

        }
    </script>--%>


    <html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>


<body>
 <form id="form1" runat="server">
      <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
				
<div class="MobileMenu">
	<CP:TitleBar Title="User Control Test" TextColor="red"  Padding=10 runat="server" />
	</div>
	<div class="clear"></div>
        <div>

                <table width="100%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue">
                            Track Call Details
                        </td>
                    </tr>
                </table>
               
                
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20dp;">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
    </table>


                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 250px">
                            Search Criteria
                        </td>
                        <td class="rcd-tableCell">
                        <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                        <td>
                        <asp:DropDownList ID="ddlSearch"  runat="server" CssClass="DropDownList" width= "250px" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchselectedindexchanged" >
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Call Date</asp:ListItem>
                                <asp:ListItem Value="2">Ticket Number</asp:ListItem>
                                <asp:ListItem Value="3">Call Status</asp:ListItem>
                                <asp:ListItem Value="4">Policy No</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                         <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="ddlSearch"
                                ErrorMessage="Select search criteria" InitialValue="0" ValidationGroup="CheckData"
                                Display="None" /></td>
                        <td colspan="3">
                        <cc1:ValidatorCalloutExtender ID="vceSearch" TargetControlID="rfvSearch" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender></td>
                        <td></td>
                        </tr>
                        </table>
                            
                           
                            
                        </td>
                    </tr>
                    <tr id="trFromDate" runat="server" visible="false">
                        <td class="rcd-FieldTitle" style="width: 250px;">
                            From Date
                        </td>
                        <td class="rcd-tableCell" >
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img alt="Icon" style="cursor: hand" src="../Images/Calander_New.jpg" id="imgFromDate" />
                                    </td>
                                    <td>
                                        <cc1:CalendarExtender ID="ceFromDate" Format="MM/dd/yyyy" TargetControlID="txtFromDate"
                                            PopupButtonID="imgFromDate" runat="server">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td colspan="2">
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
                    <tr id="trToDate" runat="server" visible="false">
                        <td class="rcd-FieldTitle" style="width: 250px;">
                            To Date
                        </td>
                        <td class="rcd-tableCell" colspan="2" >
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox"></asp:TextBox>
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
                    <tr id="trTicketNo" runat="server" visible="false">
                        <td class="rcd-FieldTitle" style="width: 250px;">
                            Ticket Number
                        </td>
                        <td class="rcd-tableCell" >
                            <asp:TextBox ID="txtTicketNumber" runat="server" CssClass="textbox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTicketNo" runat="server" ControlToValidate="txtTicketNumber"
                                ErrorMessage="Enter ticket number" Font-Bold="True" Font-Size="Medium" ValidationGroup="CheckData"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="vceTicketNo" TargetControlID="rfvTicketNo" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <%--<cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtTicketNumber"
                                WatermarkText="Type Ticket Number Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />--%>
                        </td>
                    </tr>
                    <tr id="trCallStatus" runat="server" visible="false">
                        <td class="rcd-FieldTitle" style="width: 250px;">
                            Call Status
                        </td>
                        <td class="rcd-tableCell" >
                            <asp:DropDownList ID="ddlCallStatus" runat="server" CssClass="DropDownList">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Open</asp:ListItem>
                                <asp:ListItem Value="9">Resolved</asp:ListItem>
                                <asp:ListItem Value="12">Rejected</asp:ListItem>
                                <asp:ListItem Value="11">Auto Closed</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCallStatus" runat="server" ControlToValidate="ddlCallStatus"
                                Display="None" SetFocusOnError="true" ErrorMessage="Please select Call status"
                                ValidationGroup="CheckData" InitialValue="0">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceCallStatus" TargetControlID="rfvCallStatus"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                      
                    </tr>
                    
                     <tr id="trPolicyNo" runat="server" visible="false" >
                      <td class="rcd-FieldTitle" style="width: 250px;">
                         Policy No
                      </td>
                       <td class="rcd-tableCell" >
                          <asp:TextBox ID="txtPolicyNo" runat="server" CssClass="textbox"></asp:TextBox>   
                          <asp:RequiredFieldValidator ID="rfvPolicyNo" runat="server" ControlToValidate="txtPolicyNo"
                           Display="None" SetFocusOnError="true" ErrorMessage="Please enter policy no" ValidationGroup="CheckData" InitialValue=""></asp:RequiredFieldValidator>
                           <cc1:ValidatorCalloutExtender ID="vcePolicyNo" TargetControlID="rfvPolicyNo" runat="server"   
                              WarningIconImageUrl="../Images/Warning1.jpg">
                              </cc1:ValidatorCalloutExtender>                                            
                       </td>                    
                    </tr>                       
                    <tr id="trApplication" runat="server" visible="false">
                         <td class="rcd-FieldTitle" style="width: 250px">
                                Application
                         </td>
                         <td class="rcd-tableCell" >
                           <asp:DropDownList ID="ddlApplication" runat="server" CssClass="DropDownList">
                            <asp:ListItem Text="All Applications" Value="0"></asp:ListItem>
                           </asp:DropDownList>
                         </td>                    
                    </tr>
                    
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="BtnTrackCall" ValidationGroup="CheckData" runat="server" Text="Track the Call"
                                 SkinID="buttonSkin" OnClick="BtnTrackCall_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" SkinID="buttonSkin" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
				

                <div style="height:1200px; background-color:White; width:100%">

               <br/>
                            <asp:GridView ID="gvCallDetails" width="100%" runat="server" AllowSorting="true" AutoGenerateColumns="False"
                                CssClass="RGIL-Grid" AllowPaging="True" OnPageIndexChanging="gvCallDetails_PageIndexChanging"
                                OnRowCommand="gvCallDetails_RowCommand" OnRowCreated="gvCallDetails_RowCreated"
                                OnSorting="gvCallDetails_Sorting">
											<RowStyle CssClass="RGIL-Grid" HorizontalAlign="Left" />
                                            <HeaderStyle CssClass="RGIL-GridHeader" />
                                            <AlternatingRowStyle CssClass="RGIL-GridAlt" />
                                <Columns>
                                    <asp:TemplateField HeaderText="View Details" Visible="false">
                                        <ItemTemplate>
                                            <asp:Image ID="imgLetter" runat="server" ImageUrl="~/images/icon-details.gif" />
                                            <cc1:PopupControlExtender ID="popCtrlExtAddPopUp" runat="server" DynamicServiceMethod="GetDynamicContent"
                                                DynamicContextKey='<%# Eval("TicketNumberPK") %>' DynamicControlID="pnlAddPopUP"
                                                TargetControlID="imgLetter" PopupControlID="pnlAddPopUP" Position="Bottom">
                                            </cc1:PopupControlExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ticket Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTicketNo" Font-Underline="true" runat="server" Text='<%# Bind("TicketNumberPK") %>'></asp:Label>
                                            <cc1:PopupControlExtender ID="popCtrlExtAddPopUp1" runat="server" DynamicServiceMethod="GetDynamicContent"
                                                DynamicContextKey='<%# Eval("TicketNumberPK") %>' DynamicControlID="pnlAddPopUP"
                                                TargetControlID="lblTicketNo" PopupControlID="pnlAddPopUP" Position="Bottom">
                                            </cc1:PopupControlExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Application">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApplication" runat="server" Text='<%# Bind("ApplicationName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssueRequestType" runat="server" Text='<%# Bind("IssueRequestType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssueRequestSubType" runat="server" Text='<%# Bind("IssueRequestSubType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Call Date" HeaderStyle-ForeColor="White" SortExpression="CallDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCallDate" runat="server" Text='<%# Eval("CallDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Approver Status">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproverStatus" runat="server" Text='<%# Bind("ApproverStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approver Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproverRemark" runat="server" Text='<%# Bind("ApproverRemark") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AppSupport Status">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppSupportStatus" runat="server" Text='<%# Bind("AppSupportStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AppSupport Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppSupportRemark" runat="server" Text='<%# Bind("AppSupportRemark") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                   <asp:TemplateField HeaderText="Expected Close Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpectedClosureDate" runat="server" Text='<%# Eval("ExpectedCloseDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Call Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCallStatus" runat="server" Text='<%# Bind("CallStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Escalation Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEscalationRemark" runat="server" Text='<%# Bind("EscalationRemark") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="False" CommandArgument='<%# Eval("TicketNumberPK") %>'
                                                CommandName="lnkDetails" Text="Details"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Panel ID="pnlAddPopUP" runat="server">
                            </asp:Panel>
                      
                
                <script language="javascript" type="text/javascript">
                    SearchCriteria();
                </script>
                </div>
        </div>

    </form>
    </body>
    </html>
<%--</asp:Content>--%>
