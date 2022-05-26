<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfileUpdation.aspx.cs" Inherits="IMD_ProfileUpdation" 
 MasterPageFile="~/IMD/AgentMasterPage.master" Theme="SkinFile" Title=":: Call Desk - Profile Updation ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<table width="98%" border="0" cellspacing="1" cellpadding="1">
      <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        
                        <td class="rcd-TopHeaderBlue">
                           Profile Updation
                        </td>
                         <asp:ScriptManager ID="ScriptManager1" runat="server">
                         </asp:ScriptManager>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
          <td>    
                   
           <table style="width: 100%">
                               <tr>
                                <td class="rcd-FieldTitle" style="width: 40%">
                                   Login ID 
                                </td>
                                <td class="rcd-tableCell" style="width: 60%">
                                  <asp:TextBox ID="txtUserNameLock" runat="server" SkinID="textboxSkin" ValidationGroup="CheckEditData" Width="200px" MaxLength="20" ></asp:TextBox>
                                </td> 
                               </tr>
                            
                            
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 40%">
                                        Email<span style="color:Red">*</span>
                                    </td>
                                    <td class="rcd-tableCell" style="width: 60%">
                                        <asp:TextBox ID="txtEditEmail" runat="server" SkinID="textboxSkin" ValidationGroup="CheckEditData" Width="200px" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEditEmail" runat="server" ControlToValidate="txtEditEmail"
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckEditData" ErrorMessage="Please enter valid Email"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceEditEmail" runat="server" TargetControlID="rfvEditEmail"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                          <asp:RegularExpressionValidator ID="revEditEmail" runat="server" ControlToValidate="txtEditEmail"
                                            Display="None" ValidationGroup="CheckEditData" ErrorMessage="Please enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>   
                                            <cc1:ValidatorCalloutExtender ID="vceRevEditMail" runat="server" TargetControlID="revEditEmail" WarningIconImageUrl="../Images/Warning1.jpg" >
                                            </cc1:ValidatorCalloutExtender>
                                                                                 
                                          <cc1:TextBoxWatermarkExtender ID="twEditEmail" runat="server" TargetControlID="txtEditEmail"
                                            WatermarkText="Enter New Email Address Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 40%">
                                        Mobile No<span style="color:Red">*</span>
                                    </td>
                                    <td class="rcd-tableCell" style="width: 60%">
                                        <asp:TextBox ID="txtEditMobileNo" runat="server" SkinID="textboxSkin" ValidationGroup="CheckEditData" Width="200px" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEditMobileNo" runat="server" ControlToValidate="txtEditMobileNo"
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckEditData" ErrorMessage="Enter Valid mobile no of 10 digits starting with 7,8,9 ."></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceEditMobileNo" runat="server" TargetControlID="rfvEditMobileNo"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                          <asp:RegularExpressionValidator ID="revEditMobileNo" runat="server" ControlToValidate="txtEditMobileNo" 
                                        ErrorMessage="Enter Valid mobile no of 10 digits starting with 7,8,9 ." ValidationExpression="^[7-9]{1}[0-9]{9}" SetFocusOnError="true" 
                                        Display="None" ValidationGroup="CheckEditData" Font-Size="9px" ></asp:RegularExpressionValidator>
                                         <cc1:ValidatorCalloutExtender ID="vceRevEditMobileNo" runat="server" TargetControlID="revEditMobileNo" 
                                         WarningIconImageUrl="../Images/Warning1.jpg">
                                         </cc1:ValidatorCalloutExtender>
                                          <cc1:TextBoxWatermarkExtender ID="twEditMobileNo" runat="server" TargetControlID="txtEditMobileNo"
                                            WatermarkText="Enter New Mobile No" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                    </td>
                                </tr>
                                <tr>
                                   <td class="rcd-FieldTitle" style="width: 40%">
                                     Sales Manager Name<span style="color:Red">*</span>
                                   </td>
                                   <td class="rcd-tableCell" style="width: 60%">
                                      <asp:TextBox ID="txtEditSMName" runat="server" SkinID="textboxSkin" ValidationGroup="CheckEditData" Width="200px" MaxLength="100"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="rfvEditSMName" runat="server" ControlToValidate="txtEditSMName"
                                          ValidationGroup="CheckEditData" Display="None" SetFocusOnError="true" ErrorMessage="Please enter Sales Manager Name"></asp:RequiredFieldValidator>
                                      <asp:RegularExpressionValidator ID="revEditSMName" runat="server" ValidationGroup="CheckEditData"
                                       ControlToValidate="txtEditSMName" Display="None" SetFocusOnError="true" ValidationExpression="^[a-zA-Z ]+$" 
                                       ErrorMessage="Please eneter characters only." ></asp:RegularExpressionValidator>                                                    
                                       <cc1:ValidatorCalloutExtender ID="vceEditSMName" runat="server" TargetControlID="rfvEditSMName"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>                                                
                                        <cc1:ValidatorCalloutExtender ID="vceEditSMName1" runat="server" TargetControlID="revEditSMName"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>                                                
                                         <cc1:TextBoxWatermarkExtender ID="twEditSMName" runat="server" TargetControlID="txtEditSMName"
                                            WatermarkText="Enter Sales Manager Name Here" WatermarkCssClass="rcd-txtboxvaluewatermark" /> 
                                   
                                   
                                   </td>
                                
                                </tr>
                                
                                 <tr>
                                           <td class="rcd-FieldTitle" style="width: 40%" >
                                                Sales Manager Code<span style="color:Red">*</span>
                                           </td>
                                           <td class="rcd-tableCell" style="width: 60%">
                                              <asp:TextBox ID="txtEditSMCode" runat="server" SkinID="textboxSkin" ValidationGroup="CheckEditData" MaxLength="10" Width="200px"></asp:TextBox>
                                               <asp:RequiredFieldValidator ID="rfvEditSMCode" runat="server" ControlToValidate="txtEditSMCode"
                                                    ValidationGroup="CheckEditData" Display="None" SetFocusOnError="true" ErrorMessage="Please enter Sales Manager Code"></asp:RequiredFieldValidator>
                                              
                                                 <asp:RegularExpressionValidator ID="revEditSMCode" runat="server" ControlToValidate="txtEditSMCode" 
                                                ErrorMessage="Enter 8 digit number only" ValidationExpression="^[0-9]{8}" SetFocusOnError="true" 
                                                Display="None" ValidationGroup="CheckEditData" Font-Size="9px" ></asp:RegularExpressionValidator>
                                              
                                               <cc1:ValidatorCalloutExtender ID="vceEditSMCode" runat="server" TargetControlID="rfvEditSMCode"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender>
                                                  <cc1:ValidatorCalloutExtender ID="vceEditSMCode1" runat="server" TargetControlID="revEditSMCode"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender>
                                              
                                                <cc1:TextBoxWatermarkExtender ID="twEditSMCode" runat="server" TargetControlID="txtEditSMCode"
                                                    WatermarkText="Enter Sales Manager Code Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />                                                 
                                           </td>                                        
                                  </tr>
                                
                                
                                
                                
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:Button ID="btnSaveUserFields" runat="server" Text="Update" SkinID="buttonSkin"
                                            ValidationGroup="CheckEditData" OnClick="btnSaveUserFields_Click" />
                                    </td>
                                 </tr>
                            </table>
          
                  
          </td>        
        </tr>

</table>
</asp:Content>