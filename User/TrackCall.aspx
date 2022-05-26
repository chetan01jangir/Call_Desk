<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    Theme="SkinFile" CodeFile="TrackCall.aspx.cs" Inherits="_Default" Title=":: Call Desk - Track Call ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <script type="text/javascript">
    function checkDate(sender,args)
    {
        if (sender._selectedDate < new Date()) 
        {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date(); 
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        }
    }
    </script>

    <script language="javascript" type="text/javascript">
                
        function SearchCriteria()
        {
            var ddlSearch = document.getElementById('ctl00_ContentPlaceHolder1_ddlSearch');
                        
            if(ddlSearch.value == '1')
            {
                document.getElementById('ctl00_ContentPlaceHolder1_trFromDate').style.display= 'inline';
                document.getElementById('ctl00_ContentPlaceHolder1_trToDate').style.display= 'inline';
                document.getElementById('ctl00_ContentPlaceHolder1_trApplication').style.display= 'inline';  
                //var _ddlDept = document.getElementById('ctl00_ContentPlaceHolder1_ddlApplication')
                //_ddlDept.selectedIndex=0
                              
                document.getElementById('ctl00_ContentPlaceHolder1_trTicketNo').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trCallStatus').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trPolicyNo').style.display= 'none';
            
                document.getElementById('ctl00_ContentPlaceHolder1_rfvFromDate').enabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvtxtToDate').enabled = true;              
                document.getElementById('ctl00_ContentPlaceHolder1_rfvTicketNo').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvCallStatus').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvPolicyNo').enabled = false;

            }
            else if(ddlSearch.value == '2')
            {
                document.getElementById('ctl00_ContentPlaceHolder1_trFromDate').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trToDate').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trApplication').style.display= 'none';                 
                document.getElementById('ctl00_ContentPlaceHolder1_trTicketNo').style.display= 'inline';
                document.getElementById('ctl00_ContentPlaceHolder1_trCallStatus').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trPolicyNo').style.display= 'none';
            
                document.getElementById('ctl00_ContentPlaceHolder1_rfvFromDate').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvtxtToDate').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvTicketNo').enabled = true; 
                document.getElementById('ctl00_ContentPlaceHolder1_rfvCallStatus').enabled = false;  
                document.getElementById('ctl00_ContentPlaceHolder1_rfvPolicyNo').enabled = false;             

            }            
            else if(ddlSearch.value == '3')
            {
                document.getElementById('ctl00_ContentPlaceHolder1_trFromDate').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trToDate').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trApplication').style.display= 'inline'; 
                //var _ddlDept = document.getElementById('ctl00_ContentPlaceHolder1_ddlApplication')
                //_ddlDept.selectedIndex=0   
                document.getElementById('ctl00_ContentPlaceHolder1_trTicketNo').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trCallStatus').style.display= 'inline';
                document.getElementById('ctl00_ContentPlaceHolder1_trPolicyNo').style.display= 'none';
            
                document.getElementById('ctl00_ContentPlaceHolder1_rfvFromDate').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvtxtToDate').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvTicketNo').enabled = false; 
                document.getElementById('ctl00_ContentPlaceHolder1_rfvCallStatus').enabled = true; 
                document.getElementById('ctl00_ContentPlaceHolder1_rfvPolicyNo').enabled = false;                

            }
            else if(ddlSearch.value == '4')
            {
                document.getElementById('ctl00_ContentPlaceHolder1_trFromDate').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trToDate').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trApplication').style.display= 'none';  
                document.getElementById('ctl00_ContentPlaceHolder1_trTicketNo').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trCallStatus').style.display= 'none';   
                document.getElementById('ctl00_ContentPlaceHolder1_trPolicyNo').style.display= 'inline';
                
                document.getElementById('ctl00_ContentPlaceHolder1_rfvFromDate').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvtxtToDate').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvTicketNo').enabled = false; 
                document.getElementById('ctl00_ContentPlaceHolder1_rfvCallStatus').enabled = false; 
                document.getElementById('ctl00_ContentPlaceHolder1_rfvPolicyNo').enabled = true;   
                
                
            } 
            else
            {
                document.getElementById('ctl00_ContentPlaceHolder1_trFromDate').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trToDate').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trApplication').style.display= 'none';  
                document.getElementById('ctl00_ContentPlaceHolder1_trTicketNo').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trCallStatus').style.display= 'none';
                document.getElementById('ctl00_ContentPlaceHolder1_trPolicyNo').style.display= 'none';
            
                document.getElementById('ctl00_ContentPlaceHolder1_rfvFromDate').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvtxtToDate').enabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_rfvTicketNo').enabled = false; 
                document.getElementById('ctl00_ContentPlaceHolder1_rfvCallStatus').enabled = false;  
                document.getElementById('ctl00_ContentPlaceHolder1_rfvPolicyNo').enabled = false;    
          
            }
           
        }
    </script>

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
                <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
                
                
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px;">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Search Criteria
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlSearch" onchange="SearchCriteria();" runat="server" SkinID="dropdownSkin">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Call Date</asp:ListItem>
                                <asp:ListItem Value="2">Ticket Number</asp:ListItem>
                                <asp:ListItem Value="3">Call Status</asp:ListItem>
                                <asp:ListItem Value="4">Policy No/Keyword</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="ddlSearch"
                                ErrorMessage="Select search criteria" InitialValue="0" ValidationGroup="CheckData"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="vceSearch" TargetControlID="rfvSearch" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr id="trFromDate" runat="server">
                        <td class="rcd-FieldTitle" style="width: 40%; height: 18px;">
                            From Date
                        </td>
                        <td class="rcd-tableCell" style="width: 60%; height: 18px;">
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
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                            ErrorMessage="Select from date" ValidationGroup="CheckData" Display="None" />
                                        <cc1:ValidatorCalloutExtender ID="vceFromDate" TargetControlID="rfvFromDate" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trToDate" runat="server">
                        <td class="rcd-FieldTitle" style="width: 40%; height: 18px;">
                            To Date
                        </td>
                        <td class="rcd-tableCell" style="width: 60%; height: 18px;">
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
                    <tr id="trTicketNo" runat="server">
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Ticket Number
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtTicketNumber" runat="server" SkinID="textboxSkin"></asp:TextBox>
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
                    <tr id="trCallStatus" runat="server">
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Call Status
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlCallStatus" runat="server" SkinID="dropdownSkin">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Open</asp:ListItem>
                                <asp:ListItem Value="9">Resolved</asp:ListItem>
                                <asp:ListItem Value="12">Rejected</asp:ListItem>
                                <asp:ListItem Value="11">Auto Closed</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvCallStatus" runat="server" ControlToValidate="ddlCallStatus"
                                Display="None" SetFocusOnError="true" ErrorMessage="Please select Call status"
                                ValidationGroup="CheckData" InitialValue="0">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceCallStatus" TargetControlID="rfvCallStatus"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    
                     <tr id="trPolicyNo" runat="server" >
                      <td class="rcd-FieldTitle" style="width: 40%">
                         Policy No/Keyword
                      </td>
                       <td class="rcd-tableCell" style="width: 60%">
                          <asp:TextBox ID="txtPolicyNo" runat="server"></asp:TextBox>   
                          <asp:RequiredFieldValidator ID="rfvPolicyNo" runat="server" ControlToValidate="txtPolicyNo"
                           Display="None" SetFocusOnError="true" ErrorMessage="Please enter policy no" ValidationGroup="CheckData" InitialValue=""></asp:RequiredFieldValidator>
                           <cc1:ValidatorCalloutExtender ID="vcePolicyNo" TargetControlID="rfvPolicyNo" runat="server"   
                              WarningIconImageUrl="../Images/Warning1.jpg">
                              </cc1:ValidatorCalloutExtender>                                            
                       </td>                    
                    </tr>                       
                    <tr id="trApplication" runat="server">
                         <td class="rcd-FieldTitle" style="width: 40%">
                                Application
                         </td>
                         <td class="rcd-tableCell" style="width: 60%">
                           <asp:DropDownList ID="ddlApplication" runat="server" SkinID="dropdownSkin">
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
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="gvCallDetails" runat="server" AllowSorting="true" AutoGenerateColumns="False"
                                SkinID="gridviewSkin" AllowPaging="True" OnPageIndexChanging="gvCallDetails_PageIndexChanging"
                                OnRowCommand="gvCallDetails_RowCommand" OnRowCreated="gvCallDetails_RowCreated"
                                OnSorting="gvCallDetails_Sorting">
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
                                     <asp:TemplateField HeaderText="Track details">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="False" CommandArgument='<%# Eval("TicketNumberPK") %>'
                                                CommandName="lnkDetails" Text="Details"><img src="../Images/detailBlink.gif" / style="border:0"></asp:LinkButton>
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
                                    <asp:TemplateField HeaderText="Approver Status">
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
                                    </asp:TemplateField>
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
                                   
                                </Columns>
                            </asp:GridView>
                            <asp:Panel ID="pnlAddPopUP" runat="server">
                            </asp:Panel>
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
