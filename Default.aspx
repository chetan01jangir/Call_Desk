<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title=":: Call Desk - Home" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
	<script language="javascript" type="text/javascript">
     function checkTextAreaMaxLength(textBox, e, length) {

    var mLen = textBox["MaxLength"];
    if (null == mLen)
        mLen = length;

    var maxLength = parseInt(mLen);
    if (!checkSpecialKeys(e)) {
        if (textBox.value.length > maxLength - 1) {
            if (window.event)//IE
            {
                e.returnValue = false;
                return false;
            }
            else
                e.preventDefault();
        }
    }
}

function checkSpecialKeys(e) {
    if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 35 && e.keyCode != 36 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
        return false;
    else
        return true;
}     


function Savedata() {
    var x;
    //if (confirm("Do you really want to close the call!") == true) {
    if (confirm("Please click on ok to confirm that the given issue stands resolved.") == true) {
        return true;
    } else {
        return false;
    }

}

    </script>
    </head>
    <body>
    <%--<marquee scrollamount="3"><h3 style="color:Maroon">*Do's and don'ts Document now available in Downloads option for user understanding. </h3></marquee>--%>
	<%--<marquee scrollamount="3"><h3 style="color:Maroon">*New IT Escalation Matrix are uploaded, please check and follow as per the process.</h3></marquee>--%>	

	<h3 style="color:Maroon">*For any payment detail related issues, If the policy is Motor+PA (Bundle), Then no backend changes can be done from IT Support, Please select the correct payment while replenishing. </h3>
	
	<%--<h3 style="color:Maroon">*Pravasi Bharti Product is blocked across all systems, kindly contact Amish Mhatre  for further clarification. </h3>--%>
	
	<h3 style="color:Blue">*Call desk support team available Between 9.30AM - 6.30PM.</h3>
	<h3 style="color:Blue">*Lunch hour at 1.30PM - 2.15 PM.</h3>
    
	<%--<marquee scrollamount="3"><h3 style="color:Maroon">*Dear User, please note that some of our land line numbers of support team members are not working. 
	Please follow escalation matrix for any issue.</h3></marquee>--%>	
	
    <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
				
                 <asp:UpdatePanel id="UpdatePanel1" runat="server">
                    <contenttemplate>
                <asp:Label ID="lblcontrol" runat="server"></asp:Label>
                <asp:Label ID="lblcontrol2" runat="server"></asp:Label>
				<asp:Label ID="lblcontrol3" runat="server"></asp:Label>
				
				
                <table width="100%" border="0" cellspacing="1" cellpadding="1">
        <tr>
        <td>
        <asp:Panel ID="pnlMyPanel" runat="server" Style="display: none" Height="284px" Width="730px" CssClass="modalPopup2" >
        <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label>
            <table width="100%" border="0" cellspacing="1" cellpadding="1" >
            <tr>
             <td>
             <img style="float: left; width: 30%" src="Images/reliancelogo.jpg" title="" />             
               </td>
               </tr>
               <tr>
                <td class="rcd-FieldText">
                <div style="text-align:left">
                     Thank you for using Call desk. please provide your valuable Feedback on the service provided by IT support Desk which will help us to serve you better.
                  </div> 
                   <hr style="color:Red" />
                   <div style="text-align:left">
                    On behalf of <b>Reliance General Insurance</b> taking this as an opportunity to know your feedback for the Raised Call.
					</div>
                </td>
               </tr>
               <tr>
               <td>
                 <table width="100%" border="0" cellspacing="1" cellpadding="1">
                  <tr>
                   <td class="rcd-FieldTitle">Name<span style="color:Red">*</span></td>
                   <td colspan="2" class="rcd-tableCell"><asp:TextBox ID="txtusernm" runat="server" SkinID="textboxSkin" Width="50%" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvname" runat="server" 
                        ControlToValidate="txtusernm" Display="Dynamic" 
                        ErrorMessage="Please enter your name." SetFocusOnError="True" 
                        ValidationGroup="Add"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="revname" runat="server" 
                        ControlToValidate="txtusernm" Display="Dynamic" 
                        ErrorMessage="Please enter proper name." SetFocusOnError="True" ValidationExpression="^[a-zA-Z\s]+$"></asp:RegularExpressionValidator>
                   </td>
                  </tr>
                  <tr>
                   <td class="rcd-FieldTitle">Email<span style="color:Red">*</span></td>
                   <td class="rcd-tableCell"><asp:TextBox ID="txtemail" runat="server" SkinID="textboxSkin" Width="50%" MaxLength="50"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rfvemail" runat="server" 
                        ControlToValidate="txtemail" Display="Dynamic" 
                        ErrorMessage="Please enter your email id." SetFocusOnError="True" 
                        ValidationGroup="Add"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="revemail" runat="server" 
                        ControlToValidate="txtemail" Display="Dynamic" 
                        ErrorMessage="Please enter valid email id." SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                   </td>
                   </tr>
                   <tr>
                   <td class="rcd-FieldTitle">Mobile No<span style="color:Red">*</span></td>
                   <td class="rcd-tableCell"><asp:TextBox ID="txtmobileno" runat="server" SkinID="textboxSkin" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvmobile" runat="server" 
                        ControlToValidate="txtmobileno" Display="Dynamic" 
                        ErrorMessage="Please enter your mobile no." SetFocusOnError="True" 
                        ValidationGroup="Add"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="revmobile" runat="server" 
                        ControlToValidate="txtmobileno" Display="Dynamic" 
                        ErrorMessage="Please enter correct mobile no." SetFocusOnError="True" 
                        ValidationExpression="^[7-9]{1}[0-9]{9}"></asp:RegularExpressionValidator>
                   </td>
                   </tr>
                 </table>
               </td>
               </tr>
               <tr>
               <td>
               <hr style="color:Red" /></td>
               </tr>
			      <tr runat="server" id="tryesno">
                <td style="text-align:left; color:#00549D; font-weight:bold">
                  Have you ever tried call logging ?
                </td>
               </tr>
               <tr runat="server" id="trrdoyesno">
               <td class="rcd-FieldTitleSmall">
                 <asp:RadioButtonList ID="rdbyesno" CellSpacing="1" CellPadding="1" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbyesnochange" AutoPostBack="true">
                 <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                 <asp:ListItem Text="No" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                </td>
               </tr>
               <tr runat="server" id="trqst" visible="false">
               <td>
                <table width="100%" border="0" cellspacing="1" cellpadding="1">
                <tr> <td style="text-align:left; color:#00549D; font-weight:bold">
    1. How satisfied were you with the service you received from IT support? 
    </td>
    
    <td class="rcd-FieldTitleSmall">
    <asp:RadioButtonList ID="rdblistq1" CellSpacing="1" CellPadding="1" runat="server" RepeatDirection="Horizontal">
     <asp:ListItem Text="Excellent" Selected="True" Value="1"></asp:ListItem>
     <asp:ListItem Text="Good" Value="2"></asp:ListItem>
     <asp:ListItem Text="Average" Value="3"></asp:ListItem>
     <asp:ListItem Text="Poor" Value="4"></asp:ListItem>
    </asp:RadioButtonList>
    
    </td> </tr>
    <tr> <td style="text-align:left; color:#00549D; font-weight:bold">
    2. Rate us on Resolution Time. 
    </td>
    
    <td class="rcd-FieldTitleSmall">
    <asp:RadioButtonList ID="rdblistq2" CellSpacing="1" CellPadding="1" runat="server" RepeatDirection="Horizontal">
     <asp:ListItem Text="Excellent" Selected="True" Value="1"></asp:ListItem>
     <asp:ListItem Text="Good" Value="2"></asp:ListItem>
     <asp:ListItem Text="Average" Value="3"></asp:ListItem>
     <asp:ListItem Text="Poor" Value="4"></asp:ListItem>
    </asp:RadioButtonList>
    
    </td> </tr>
    <tr> <td style="text-align:left; color:#00549D; font-weight:bold">
    3. Rate us on Quality of Resolution. 
    </td>
    
    <td class="rcd-FieldTitleSmall">
    <asp:RadioButtonList ID="rdblistq3" CellSpacing="1" CellPadding="1" runat="server" RepeatDirection="Horizontal">
     <asp:ListItem Text="Excellent" Selected="True" Value="1"></asp:ListItem>
     <asp:ListItem Text="Good" Value="2"></asp:ListItem>
     <asp:ListItem Text="Average" Value="3"></asp:ListItem>
     <asp:ListItem Text="Poor" Value="4"></asp:ListItem>
    </asp:RadioButtonList>
    
    </td> </tr>
                </table>
               </td>
               </tr>
   <tr runat="server" id="trqst2" visible="false">
    <td style="text-align:left; color:#00549D; padding-left:3px; font-weight:bold">
    4. In the Space below please Write your Valuable Comments.
    </td>
    </tr>
    <tr runat="server" id="trtxtremark" visible="false">
     <td class="rcd-FieldTitleSmall">
      <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Height="100px" Width="465px" MaxLength="500" onkeyDown="return checkTextAreaMaxLength(this,event,'500')"></asp:TextBox>
     </td>
    </tr>
    <tr runat="server" id="trbtns">
     <td class="rcd-TopHeaderBlue"><center>
      <asp:Button ID="btnsubmit" Visible="false" ValidationGroup="Add" runat="server" Text="Submit" CssClass="rcd-FlatButton" OnClick="btnsubmit_Click" />
      <asp:Button ID="btnclose" runat="server" Text="Skip for now" CssClass="rcd-FlatButton" /></center>
     </td>
    </tr>
    </table>                             
        </asp:Panel>
        <cc1:ModalPopupExtender ID="popupconfirm" runat="server" TargetControlID="lblcontrol"
            PopupControlID="pnlMyPanel" CancelControlID="btnclose" Enabled="true" DropShadow="false"
             BackgroundCssClass="modalBackground1" DynamicServicePath="">
        </cc1:ModalPopupExtender>
        </td>
        </tr>
       
        </table>
		
		<table width="100%" border="0" cellspacing="1" cellpadding="1">
      
                  <tr>
                  <td>
                 
                  <asp:Panel ID="Panel1" runat="server" Style="display: none" ScrollBars="Vertical" Height="250px" Width="880px" CssClass="modalPopup2" >
               <div id="Divcustomer" runat="server" style="overflow:auto">
               User Confirmation for recently resolved calls.(Calls within reopen TAT only.)

                <asp:GridView ID="gvCallDetails" runat="server"  AutoGenerateColumns="False"
                                        SkinID="gridviewCustomer" AllowPaging="True" OnPageIndexChanging="gvCallDetails_PageIndexChanging"
                                        OnRowCommand="gvCallDetails_RowCommand" 
                                        >
                                        <Columns>
                                            <asp:TemplateField HeaderText="View Details" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgLetter" runat="server" ImageUrl="~/images/icon-details.gif" />
            
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ticket Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTicketNo"  runat="server" Text='<%# Bind("TicketNumberPK") %>'></asp:Label>
  
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
                                                <HeaderStyle ForeColor="White" />
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
 
                                              <%--<asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkclose" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="User Confirmation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSelect" runat="server" onclientclick="return Savedata()" CausesValidation="False" 
                                                        CommandArgument='<%# Eval("TicketNumberPK") %>'
                                                        CommandName="lnkDetails" Text="Close"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reopen/Viewdetails">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkreopen" runat="server"  CausesValidation="False" CommandArgument='<%# Eval("TicketNumberPK") %>'
                                                        CommandName="lnkreopen" Text="Reopen/Details" ToolTip="Click to reopen call or view details."></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                  
                                   
                                        </Columns>
                                    </asp:GridView>

                             <asp:Button ID="btncloseCustomer" runat="server" OnClick="btncloseCustomer_Click" Text="Skip for now" CssClass="rcd-FlatButton" /></center>
                              </div>
                              </asp:Panel>

                             

        <cc1:ModalPopupExtender ID="popupcustomer" runat="server" TargetControlID="lblcontrol3"
            PopupControlID="Panel1"  Enabled="true" DropShadow="false"
             BackgroundCssClass="modalBackground1" DynamicServicePath="">
        </cc1:ModalPopupExtender>

                            </td>

                            </tr>
                            <tr runat="server" id="tr1">
     <td class="rcd-TopHeaderBlue"><center>
      
     
     </td>
    </tr>
                            </table>
		
        <table width="100%" border="0" cellspacing="1" cellpadding="1">
        
        <tr>
        <td>
        <asp:Panel ID="pnlmailer" runat="server" Style="display: none" Height="400px" Width="550px"  ScrollBars="Vertical" CssClass="pnlControl" >
          <table width="90%" border="0" cellspacing="1" cellpadding="1"  >
           <tr>
         <td align="right">
          <asp:ImageButton ID="btnmailerclose" runat="server" ToolTip="Close" ImageUrl="~/Images/Closebtn.png" />
         </td>
         </tr>
          <tr>
          <td>
          <%--<img src="Images/ITAlert.jpg" style="width:780px; height:700px"  title="" alt="Mailer" />--%> 
			   
          <img src="Images/CalldeskDIrectContact.png" style="width:525px; height:350px"  title="" alt="Mailer" /> 
          </td>
         </tr>
        
         </table>
        </asp:Panel>
        
        <cc1:ModalPopupExtender ID="mdlmailer" runat="server" TargetControlID="lblcontrol2"
            PopupControlID="pnlmailer" CancelControlID="btnmailerclose" Enabled="true" DropShadow="false"
             BackgroundCssClass="modalBackground1" DynamicServicePath="">
        </cc1:ModalPopupExtender>
        </td>
        </tr>
        </table> 
</contenttemplate>
                          </asp:UpdatePanel>		
    </body>
    </html>
</asp:Content>

