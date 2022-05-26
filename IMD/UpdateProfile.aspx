<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateProfile.aspx.cs" Inherits="UpdateProfile_UpdateProfile"
 Theme="SkinFile"
 %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <link href="rcd.css" rel="stylesheet" type="text/css" />
   <style>
        a.tooltip {outline:none; } a.tooltip strong {line-height:30px;} 
        a.tooltip:hover {text-decoration:none;} 
        a.tooltip span { z-index:10;display:none; padding:8px 5px; margin-top:-30px; margin-left:5px; width:160px; line-height:16px; } 
        a.tooltip:hover span{ display:inline; position:absolute; color:#111; border:1px solid #DCA; background:#fffAF0;}
         .callout {z-index:20;position:absolute;top:30px;border:0;left:-12px;} /*CSS3 extras*/
          a.tooltip span { border-radius:4px; -moz-border-radius: 4px; -webkit-border-radius: 4px; -moz-box-shadow: 5px 5px 8px #CCC; -webkit-box-shadow: 5px 5px 8px #CCC; box-shadow: 5px 5px 8px #CCC; }
    </style>
 </head>
 
<body style="background-color: White">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="width:100%">
      
           <table width="98%" border="0" cellspacing="1" cellpadding="1">
             <tr>
                <td colspan="3">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <%--<td class="rcd-TopLeftBanner">
                                &nbsp;</td>
                            <td class="rcd-TopRightBanner">
                                &nbsp;</td>--%>
                            <asp:Image runat="server" ID="imgMain" ImageUrl="~/Images/CallDeskMain.jpg" Width="100%"
                                ImageAlign="Middle" /></tr>
                    </table>
                </td>
            </tr>
           
             <tr>
               
               <td style="width:20%">
               
               </td>               
             
               <td>
                    <div style="width: 100%">    
                      <center>                 
                      <asp:Label ID="lblMessage" runat="server" SkinID="SkinLabel" ForeColor="Red" Font-Size="14px"></asp:Label>                            
                      </center>   
                     </div> 
                     
                <div id="divAgentCreate" runat="server" >
                                 
                            <table style="width: 100%" >
                                <tr>
                                    <td colspan="3" style="height: 43px">
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="rcd-TopHeaderBlue" style="height: 15px">
                                                   Update Profile 
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>                             
                                
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 40%">
                                        Login ID                                       
                                    </td>
                                    <td class="rcd-tableCell" style="width: 60%">
                                        <asp:TextBox ID="txtUserName" runat="server" SkinID="textboxSkin" MaxLength="20" Width="200px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" InitialValue=""
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter the User Name">
                                        </asp:RequiredFieldValidator>
                                  
                                        <cc1:ValidatorCalloutExtender ID="vceUserName" TargetControlID="rfvUserName" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>                                    
                                        
                                        <cc1:TextBoxWatermarkExtender ID="twUserName" runat="server" TargetControlID="txtUserName"
                                            WatermarkText="Enter User Name Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                    </td>
                                </tr>  
                                <tr>
                                  <td class="rcd-FieldTitle" style ="width:40%">
                                    Application System 
                                  </td>
                                     <td class="rcd-tableCell" style="width: 60%">
                                      <asp:TextBox ID="txtAppType" runat="server" SkinID="textboxSkin" MaxLength="20" Width="200px"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="rfvAppType" runat="server" ControlToValidate="txtAppType" InitialValue=""
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter Application Type. (eg. Poral/POS)">
                                        </asp:RequiredFieldValidator>
                                  
                                        <cc1:ValidatorCalloutExtender ID="vceAppType" TargetControlID="rfvAppType" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender> 
                                        <cc1:TextBoxWatermarkExtender ID="twAppType" runat="server" TargetControlID="txtAppType"
                                            WatermarkText="Enter User Application Type." WatermarkCssClass="rcd-txtboxvaluewatermark" />    
                                      
                                  </td>
                                
                                
                                </tr>                    
                                  <tr>
                                            <td class="rcd-FieldTitle" style="width: 40%">
                                                Agent Name<span style="color:Red">*</span>
                                            </td>
                                            <td class="rcd-tableCell" style="width: 60%">
                                                <asp:TextBox ID="txtAgentName" runat="server" SkinID="textboxSkin" MaxLength="100" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAgentName" runat="server" ControlToValidate="txtAgentName"
                                                    Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter your name"></asp:RequiredFieldValidator>
                                                 <asp:RegularExpressionValidator ID="revAgentName" runat="server" ValidationGroup="CheckData"
                                                   ControlToValidate="txtAgentName" Display="None" SetFocusOnError="true"
                                                     ValidationExpression="^[a-zA-Z ]+$" 
                                                  ErrorMessage="Please eneter characters only." ></asp:RegularExpressionValidator>
                                                    
                                                <cc1:ValidatorCalloutExtender ID="vceAgentName" runat="server" TargetControlID="rfvAgentName"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender>
                                                <cc1:ValidatorCalloutExtender ID="vceRevAgentName" runat="server" TargetControlID="revAgentName"
                                                  WarningIconImageUrl="../Images/Warning1.jpg"></cc1:ValidatorCalloutExtender>
                                                
                                                <cc1:TextBoxWatermarkExtender ID="twAgentName" runat="server" TargetControlID="txtAgentName"
                                                    WatermarkText="Enter Agent Name Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                            </td>
                                  </tr>   
                                  <tr>
                                            <td class="rcd-FieldTitle" style="width: 40%">
                                                Email<span style="color:Red">*</span>
                                                 <asp:LinkButton ID="lnkEmalTooltip" runat="server" CssClass="tooltip"> 
                                                <img id="imgEmailTooltip" alt="" src="../Images/nchat.png" border="none" /><span> Kindly Note that Mail Id should be Correct to receive all intimation sent by Reliance call desk</span>
                                                 </asp:LinkButton>                                                 
                                                 
                                            </td>
                                            <td class="rcd-tableCell" style="width: 60%">
                                                <asp:TextBox ID="txtEmail" runat="server" SkinID="textboxSkin" MaxLength="100" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                                    Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter valid Email"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="vceEmail" runat="server" TargetControlID="rfvEmail"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                                    Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                 <cc1:ValidatorCalloutExtender ID="vceEmailrev" runat="server" TargetControlID="revEmail" 
                                                    WarningIconImageUrl="../Images/Warning1.jpg"></cc1:ValidatorCalloutExtender>                                            
                                                <cc1:TextBoxWatermarkExtender ID="twEmail" runat="server" TargetControlID="txtEmail"
                                                    WatermarkText="Enter Email Address Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />    
                                            </td>
                                   </tr>
                                   <tr>
                                            <td class="rcd-FieldTitle" style="width: 40%">
                                                Mobile No<span style="color:Red">*</span>
                                                   <asp:LinkButton ID="lnkMbTooltip" runat="server" CssClass="tooltip"> 
                                                <img id="img1" alt="" src="../Images/nchat.png" border="none" /><span> Kindly Note that Mobile no should be Correct to receive all intimation sent by Reliance call desk</span>
                                                 </asp:LinkButton>   
                                                
                                            </td>
                                            <td class="rcd-tableCell" style="width: 60%">
                                                <asp:TextBox ID="txtMobileNo" runat="server" SkinID="textboxSkin" MaxLength="10" Width="200px"></asp:TextBox>
                                               <asp:RequiredFieldValidator ID="rfvMobileNo" runat="server" ControlToValidate="txtMobileNo"
                                                    Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Enter Valid mobile no of 10 digits starting with 7,8,9."></asp:RequiredFieldValidator>
                                                                                               
                                                   <asp:RegularExpressionValidator ID="revtxtMobileNo" runat="server" ControlToValidate="txtMobileNo" 
                                                ErrorMessage="Enter Valid mobile no of 10 digits starting with 7,8,9 ." ValidationExpression="^[7-9]{1}[0-9]{9}" SetFocusOnError="true" 
                                                Display="None" ValidationGroup="CheckData" Font-Size="9px" ></asp:RegularExpressionValidator>                                                     
                                                    
                                                <cc1:ValidatorCalloutExtender ID="vceMobileNo" runat="server" TargetControlID="rfvMobileNo"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender>
                                                  <cc1:ValidatorCalloutExtender ID="vceMobileNo1" runat="server" TargetControlID="revtxtMobileNo"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender>
                                                &nbsp;
                                                <cc1:TextBoxWatermarkExtender ID="twMobileNo" runat="server" TargetControlID="txtMobileNo"
                                                    WatermarkText="Enter Mobile No Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                                                                       
                                            </td>
                                   </tr>   
                                   <tr>
                                     <td colspan="2">                                     
                                     </td>
                                   </tr>                   
                                   
                                    <tr>
                                            <td colspan="2" class="rcd-tableCellCenterAlign">
                                                <asp:Button ID="btnSave" runat="server" SkinID="buttonSkin" Text="Save" ValidationGroup="CheckData" OnClick="btnSave_Click"/>
                                            </td>
                                  </tr>                                
                           
                            </table>
                        </div>                       
                             
               </td>
               
                <td style="width:20%">
               
               </td>  
               
             </tr>
            </table>
        
            
       </div>    
    
 </form>
</body>
</html>
