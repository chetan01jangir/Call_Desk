<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateIMDPOSUser.aspx.cs"
    Inherits="CreateIMDPOSUser" Theme="SkinFile" Title=":: Call Desk - Create IMD/POS User ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <link href="../SignUp/rcd.css" rel="stylesheet" type="text/css" />
    
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
        <div>
            <table width="98%" border="0" cellspacing="1" cellpadding="1">
                <tr>
                    <td>
                     <div style="width: 100%">    
                      <center>                 
                      <asp:Label ID="lblMessage" runat="server" SkinID="SkinLabel" ForeColor="Red" Font-Size="14px"></asp:Label>                            
                      </center>   
                     </div>
                        
                        
                        <div id="divMenu" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2" class="rcd-FieldTitle">
                                       <table>
                                         <tr>
                                         <td>
                                         Note :
                                         </td>
                                         <td>
                                              User creation and updation only for IMD-Portal / POS users. 
                                         </td>
                                         </tr>
                                         <tr>
                                          <td>
                                          </td>
                                          <td>
                                             User ID being entered should be login ID used for IMD Portal / POS. 

                                          </td>
                                         </tr>
                                       </table>                                               
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle">
                                        <asp:LinkButton ID="lnkCreateAgent" runat="server" Text="Create User" SkinID="lnkSkin"
                                            OnClick="lnkCreateAgent_Click"></asp:LinkButton>
                                    </td>
                                    <td class="rcd-FieldTitle">
                                        <asp:LinkButton ID="lnkEditProfile" runat="server" Text="Edit Profile" SkinID="lnkSkin"
                                            OnClick="lnkEditProfile_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divAgentCreate" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2" style="height: 43px">
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="rcd-TopHeaderBlue" style="height: 15px">
                                                    Create IMD / POS User Only
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                 <td colspan="2" style="height: 20px" align="center">                                    
                                     <asp:Label ID="lblCreateUserMSG" runat="server" SkinID="SkinLabel" ForeColor="Red" Font-Size="14px"></asp:Label>
                                 </td>
                                </tr>
                                
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 40%">
                                        User ID<span style="color:Red">*</span> 
                                        <span style="color:Red;font-size:9px">(Enter User ID of IMD Portal / POS)</span>                                       
                                    </td>
                                    <td class="rcd-tableCell" style="width: 60%">
                                        <asp:TextBox ID="txtUserName" runat="server" SkinID="textboxSkin" MaxLength="20" Width="200px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" InitialValue=""
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter the User Name">
                                        </asp:RequiredFieldValidator>
                                     <%--   <asp:RegularExpressionValidator ID="revUserName" runat="server"  ValidationGroup="CheckData" ValidationExpression="^[a-zA-Z0-9\s]{8}$"
                                        ErrorMessage="UserName should be aphanumeric and 8 characters."  ControlToValidate="txtUserName" Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>--%>
                                        <cc1:ValidatorCalloutExtender ID="vceUserName" TargetControlID="rfvUserName" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                      <%--    <cc1:ValidatorCalloutExtender ID="vceUserNameRev" TargetControlID="revUserName" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>--%>
                                        
                                        <cc1:TextBoxWatermarkExtender ID="twUserName" runat="server" TargetControlID="txtUserName"
                                            WatermarkText="Enter User Name Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                    </td>
                                </tr>
                               
                               <tr>
                                    <td class="rcd-FieldTitle" style="width: 40%">
                                        Create call desk user<span style="color:Red">*</span>
                                        <asp:LinkButton ID="lnkAppTypeTooltip" runat="server" CssClass="tooltip"> 
                                                <img id="imgAppType" alt="" src="../Images/nchat.png" border="none" /><span> Kindly note that please select the respective Application from dropdown for which call desk login needs to be created</span>
                                                 </asp:LinkButton>  
                                        
                                    </td>
                                    <td class="rcd-tableCell" style="width: 60%">
                                        <asp:DropDownList ID="ddlApplicationType" runat="server" AutoPostBack="true" SkinID="dropdownSkin" ValidationGroup="CheckData" Width="160px" OnSelectedIndexChanged="ddlApplicationType_SelectedIndexChanged">
                                            <asp:ListItem Text="-- Application Type--" Value="0"></asp:ListItem>
                                            <%--<asp:ListItem Text="IMD-Portal" Value="IMD-Portal"></asp:ListItem>--%>
                                            <asp:ListItem Text="POS" Value="POS"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvApplicationType" runat="server" ControlToValidate="ddlApplicationType"
                                            Display="None" ValidationGroup="CheckData" SetFocusOnError="true" ErrorMessage="Please select Application Type" InitialValue="-- Application Type--"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceApplicationType" runat="server" TargetControlID="rfvApplicationType"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender> 
                                    </td>
                                </tr>
                               
                           
                                 
                                       <tr>
                                        <td class="rcd-FieldTitle" style="width: 40%">
                                            Password<span style="color:Red">*</span>
                                            <span style="color:Red;font-size:9px;">(8 - 15 Characters only)</span>
                                        </td>
                                        <td class="rcd-tableCell" style="width: 60%">
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" SkinID="textboxSkin" CssClass="rcd-txtboxvaluewatermark" MaxLength="15" Width="200px"></asp:TextBox>
                                            
                                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter the Password">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword" 
                                             Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ValidationExpression="^.{8,15}$"  ErrorMessage="Password sholud be minimum 8 and Maximum 15 characters.">
                                             </asp:RegularExpressionValidator>
                                            
                                            <cc1:ValidatorCalloutExtender ID="vcePassword" TargetControlID="rfvPassword" runat="server"
                                                WarningIconImageUrl="../Images/Warning1.jpg">
                                            </cc1:ValidatorCalloutExtender>                                      
                                            <cc1:ValidatorCalloutExtender ID="vcePasswordRev" TargetControlID="revPassword" runat="server"
                                             WarningIconImageUrl="../Images/Warning1.jpg"></cc1:ValidatorCalloutExtender>
                                            
                                            &nbsp;
                                        </td>
                                        </tr>
                                        <tr>
                                            <td class="rcd-FieldTitle" style="width: 40%">
                                                Confirm Password<span style="color:Red">*</span>
                                            </td>
                                            <td class="rcd-tableCell" style="width: 60%">
                                                <asp:TextBox ID="txtConfirmPwd" runat="server" TextMode="Password" SkinID="textboxSkin" MaxLength="15" CssClass="rcd-txtboxvaluewatermark" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvConfirmPwd" runat="server" ControlToValidate="txtConfirmPwd"
                                                    Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please re-enter the Password">
                                                </asp:RequiredFieldValidator> 
                                                <cc1:ValidatorCalloutExtender ID="vceConfirmPwdrfv" runat="server" TargetControlID="rfvConfirmPwd"
                                                WarningIconImageUrl="../Images/Warning1.jpg"></cc1:ValidatorCalloutExtender>                                                                               
                                                <asp:CompareValidator ID="cmpPassword" runat="server" ErrorMessage="Password entered does not match"
                                                    ControlToCompare="txtPassword" SetFocusOnError="true" ValidationGroup="CheckData"
                                                    Display="None" ControlToValidate="txtConfirmPwd"></asp:CompareValidator>
                                                <cc1:ValidatorCalloutExtender ID="vceConfirmPwd" TargetControlID="cmpPassword" runat="server"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender> 
                                                
                                                &nbsp;
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
                                             Gender<span style="color:Red">*</span>
                                         </td>
                                         <td class="rcd-tableCell" style="width: 60%">
                                            <asp:DropDownList ID="ddlGender" runat="server" SkinID="dropdownSkin" ValidationGroup="CheckData" Width="160px">
                                               <asp:ListItem Value="0" Selected="True">--Select Gender--</asp:ListItem>
                                               <asp:ListItem Value="M">Male</asp:ListItem>
                                               <asp:ListItem Value="F">Female</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="ddlGender"
                                             Display="None" SetFocusOnError="true" ValidationGroup="CheckData" InitialValue="0" ErrorMessage="Please select gender">
                                             </asp:RequiredFieldValidator>
                                             <cc1:ValidatorCalloutExtender ID="vceGender" runat="server" TargetControlID="rfvGender"
                                              WarningIconImageUrl="../Images/Warning1.jpg"></cc1:ValidatorCalloutExtender>
                                            
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
                                                <img id="img1" alt="" src="../Images/nchat.png" border="none" /><span> Kindly Note that Mobile no should be Correct to receive all intimation sent by Reliance call </span>
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
                                                <cc1:TextBoxWatermarkExtender ID="twMobileNo" runat="server" TargetControlID="txtMobileNo"
                                                    WatermarkText="Enter Mobile No Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                                                                       
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="rcd-FieldTitle" style="width: 40%">
                                                Birth date<span style="color:Red">*</span>
                                            </td>
                                            <td class="rcd-tableCell" style="width: 60%">
                                                <asp:TextBox ID="txtBirthDate" runat="server" SkinID="textboxSkin" ></asp:TextBox>
                                                <img alt="Icon" style="cursor: hand" src="../Images/Calander_New.jpg" id="imgBirthDate" />
                                                <cc1:CalendarExtender ID="ceUserBirthDate" Format="dd/MM/yyyy" TargetControlID="txtBirthDate"
                                                    PopupButtonID="imgBirthDate" runat="server">
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvBirthDate" runat="server" ControlToValidate="txtBirthDate"
                                                    ErrorMessage="Select Birth date" Display="None" SetFocusOnError="true" ValidationGroup="CheckData" />
                                                <cc1:ValidatorCalloutExtender ID="vceBirthDate" TargetControlID="rfvBirthDate" runat="server"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender>
                                                <cc1:TextBoxWatermarkExtender ID="twBirthdate" runat="server" TargetControlID="txtBirthDate"
                                                    WatermarkText="Select your Birth Date" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                            </td>
                                        </tr>
                                        <%--<tr>
                                           <td class="rcd-FieldTitle" style="width: 40%">
                                           Region<span style="color:Red">*</span>
                                           </td>
                                           <td class="rcd-tableCell" style="width: 60%">
                                             <asp:DropDownList ID="ddlRegion" runat="server" SkinID="dropdownSkin" ValidationGroup="CheckData" Width="160px">    
                                             <asp:ListItem Text="--Select Region--" Value="0" Selected="True"></asp:ListItem>                                                                     
                                             </asp:DropDownList>   
                                             <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ErrorMessage="Please select Region"
                                              Display="None" ValidationGroup="CheckData" ControlToValidate="ddlRegion" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>             
                                              <cc1:ValidatorCalloutExtender ID="vceRegion" runat="server"  TargetControlID="rfvRegion" WarningIconImageUrl="../Images/Warning1.jpg">
                                              </cc1:ValidatorCalloutExtender>
                                              
                                           </td>                                
                                        </tr>--%>
                                        
                       
                                      <%--  <tr>
                                            <td class="rcd-FieldTitle" style="width: 40%">
                                                Agent Code<span style="color:Red">*</span>
                                            </td>
                                            <td class="rcd-tableCell" style="width: 60%">
                                                <asp:TextBox ID="txtAgentCode" runat="server" SkinID="textboxSkin" ValidationGroup="CheckData" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAgentCode" runat="server" ControlToValidate="txtAgentCode"
                                                    ValidationGroup="CheckData" Display="None" SetFocusOnError="true" ErrorMessage="Please enter Agent Code"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="vceAgentCode" runat="server" TargetControlID="rfvAgentCode"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender>
                                                 <cc1:TextBoxWatermarkExtender ID="twAgentCode" runat="server" TargetControlID="txtAgentCode"
                                                    WatermarkText="Enter Agent Code Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                                
                                            </td>
                                        </tr>--%>
                                        <tr>
                                          <td class="rcd-FieldTitle" style="width: 40%" >
                                             Sales Manager Name<span style="color:Red">*</span>
                                          </td>
                                          <td class="rcd-tableCell" style="width: 60%">
                                             <asp:TextBox ID="txtSMName" runat="server" SkinID="textboxSkin" ValidationGroup="CheckData" MaxLength="100" Width="200px"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="rfvSMName" runat="server" ControlToValidate="txtSMName"
                                                    ValidationGroup="CheckData" Display="None" SetFocusOnError="true" ErrorMessage="Please enter Sales Manager"></asp:RequiredFieldValidator>
                                               <asp:RegularExpressionValidator ID="revSMName" runat="server" ValidationGroup="CheckData"
                                                   ControlToValidate="txtSMName" Display="None" SetFocusOnError="true"
                                                     ValidationExpression="^[a-zA-Z ]+$" 
                                                  ErrorMessage="Please eneter characters only." ></asp:RegularExpressionValidator>
                                                    
                                               <cc1:ValidatorCalloutExtender ID="vceSMName" runat="server" TargetControlID="rfvSMName"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender>
                                                
                                                <cc1:ValidatorCalloutExtender ID="vceSMName1" runat="server" TargetControlID="revSMName"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender>
                                                
                                                 <cc1:TextBoxWatermarkExtender ID="twSMName" runat="server" TargetControlID="txtSMName"
                                                    WatermarkText="Enter Sales Manager Name Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />                                                    
                                             
                                          </td>                                       
                                        
                                        </tr>
                                        
                                        <tr>
                                           <td class="rcd-FieldTitle" style="width: 40%" >
                                                Sales Manager Code<span style="color:Red">*</span>
                                           </td>
                                           <td class="rcd-tableCell" style="width: 60%">
                                              <asp:TextBox ID="txtSMCode" runat="server" SkinID="textboxSkin" ValidationGroup="CheckData" MaxLength="100" Width="200px"></asp:TextBox>
                                               <asp:RequiredFieldValidator ID="rfvSmCode" runat="server" ControlToValidate="txtSMCode"
                                                    ValidationGroup="CheckData" Display="None" SetFocusOnError="true" ErrorMessage="Please enter Sales Manager Code"></asp:RequiredFieldValidator>
                                              
                                                 <asp:RegularExpressionValidator ID="revSMCode" runat="server" ControlToValidate="txtSMCode" 
                                                ErrorMessage="Enter 8 digit number only" ValidationExpression="^[0-9]{8}" SetFocusOnError="true" 
                                                Display="None" ValidationGroup="CheckData" Font-Size="9px" ></asp:RegularExpressionValidator>
                                              
                                               <cc1:ValidatorCalloutExtender ID="vceSMCode" runat="server" TargetControlID="rfvSmCode"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender>
                                                  <cc1:ValidatorCalloutExtender ID="vceSMCode1" runat="server" TargetControlID="revSMCode"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender>
                                              
                                                <cc1:TextBoxWatermarkExtender ID="twSMCode" runat="server" TargetControlID="txtSMCode"
                                                    WatermarkText="Enter Sales Manager Code Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />                                                 
                                           </td>                                        
                                        </tr>
                                        
                                        <tr>
                                            <td class="rcd-FieldTitle" style="width: 40%">
                                                Enter your security Question?<span style="color:Red">*</span>
                                            </td>
                                            <td class="rcd-tableCell" style="width: 60%">
                                                <asp:TextBox ID="txtQuestion" runat="server" SkinID="textboxSkin" MaxLength="100" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" ControlToValidate="txtQuestion"
                                                    ValidationGroup="CheckData" Display="None" SetFocusOnError="true" ErrorMessage="Please enter security question"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="vceQuestion" TargetControlID="rfvQuestion" runat="server"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender>
                                                 <cc1:TextBoxWatermarkExtender ID="twQuestion" runat="server" TargetControlID="txtQuestion"
                                                    WatermarkText="Enter Security Question Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                                      
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="rcd-FieldTitle" style="width: 40%">
                                                Answer<span style="color:Red">*</span>
                                            </td>
                                            <td class="rcd-tableCell" style="width: 60%">
                                                <asp:TextBox ID="txtAnswer" runat="server" SkinID="textboxSkin" MaxLength="100" Width="200px" AutoCompleteType="disabled"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAnswer" runat="server" ControlToValidate="txtAnswer"
                                                    ValidationGroup="CheckData" Display="None" SetFocusOnError="true" ErrorMessage="Please enter answer"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="vceAnswer" runat="server" TargetControlID="rfvAnswer"
                                                    WarningIconImageUrl="../Images/Warning1.jpg">
                                                </cc1:ValidatorCalloutExtender>
                                                <cc1:TextBoxWatermarkExtender ID="twAnswer" runat="server" TargetControlID="txtAnswer"
                                                    WatermarkText="Enter Security Answer Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                           </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="rcd-tableCellCenterAlign">
                                                <asp:Button ID="btnSave" runat="server" SkinID="buttonSkin" Text="Save" ValidationGroup="CheckData"
                                                    OnClick="btnSave_Click" />
                                                <asp:Button ID="btnCancel" runat="server" SkinID="buttonSkin" Text="Cancel" OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>                                
                           
                            </table>
                        </div>
                        <div id="divEditProfile" runat="server">
                            <table style="width: 100%">
                            
                                <tr>
                                    <td colspan="2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="rcd-TopHeaderBlue" style="height: 15px">
                                                    Edit Profile
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                  <td colspan="2" style="height: 20px" align="center">
                                    <center>
                                     <asp:Label ID="lblEditProfile" runat="server" SkinID="SkinLabel"  ForeColor="Red" Font-Size="14px"></asp:Label>
                                     </center>
                                  </td>
                                  
                                </tr>
                                
                                <tr>
                                 <td colspan="2">  
                                <div id="divUserValidate" runat="server">
                                <table>   
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 30%; height: 18px;">
                                        User ID<span style="color:Red">*</span>
                                    </td>
                                    <td class="rcd-tableCell" style="width: 40%; height: 18px;">
                                        <asp:TextBox ID="txtEditUserName" runat="server" ValidationGroup="CheckValidate" MaxLength="20" Width="200px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEditUserName" runat="server" ControlToValidate="txtEditUserName"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please enter username" ValidationGroup="CheckValidate">
                                        </asp:RequiredFieldValidator>
                                         <cc1:ValidatorCalloutExtender ID="vceEditUserName" runat="server" TargetControlID="rfvEditUserName"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                         <cc1:TextBoxWatermarkExtender ID="twEditUserName" runat="server" TargetControlID="txtEditUserName"
                                            WatermarkText="Enter User Name Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                    </td>
                                    <td class="rcd-tableCell" style="width: 30%; height: 18px;">
                                      <asp:Button ID="btnValidate" runat="server" Text="Validate" SkinID="buttonSkin" ValidationGroup="CheckValidate" OnClick="btnValidate_Click" />
                                    </td>
                                 </tr> 
                               </table>
                               </div>
                               
                             <div id="divEditProfileFields" runat="server">
                               <table> 
                                 
                                <tr>
                                    <td class="rcd-FieldTitle" colspan="2">
                                        Note:- Select Password / Security question for validating credentials.
                                    </td>
                                </tr>
                                                               
                                <tr>
                                    <td colspan="2" class="rcd-FieldTitle">
                                        <asp:RadioButton ID="chkEditPassword" runat="server" Text="Password" Checked="true" GroupName="EditOption"
                                            AutoPostBack="true" OnCheckedChanged="chkEditPassword_CheckedChanged" />
                                        <asp:RadioButton ID="chkEditSecurityQuestion" runat="server" Text="Security Question"
                                            GroupName="EditOption" AutoPostBack="true" OnCheckedChanged="chkEditSecurityQuestion_CheckedChanged" />
                                    </td>
                                </tr>  
                                                             
                                <tr id="trEditPassword" runat="server">
                                    <td class="rcd-FieldTitle" style="width: 40%">
                                        Password<span style="color:Red">*</span>
                                    </td>
                                    <td class="rcd-tableCell" style="width: 60%">
                                        <asp:TextBox ID="txtEditPassword" runat="server" ValidationGroup="CheckEdit" MaxLength="15"  TextMode="Password" SkinID="textboxSkin" CssClass="rcd-txtboxvaluewatermark" Width="200px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEditPassword" runat="server" ControlToValidate="txtEditPassword" SetFocusOnError="true"
                                            Display="None" ValidationGroup="CheckEdit" InitialValue="" ErrorMessage="Please enter password"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceEditPassword" runat="server" TargetControlID="rfvEditPassword"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                       
                                        
                                    </td>
                                </tr>
                                
                                <tr id="trEditSecurityQues" runat="server">
                                    <td class="rcd-FieldTitle">
                                        Security Question :-
                                        
                                     </td>
                                     <td class="rcd-FieldTitle">
                                      <asp:Label ID="lblEditSecurityQues" runat="server"></asp:Label>                                       
                                     </td>
                                     
                                </tr>
                                
                                <tr id="trEditSecurityAnswer" runat="server">
                                    <td class="rcd-FieldTitle" style="width: 40%">
                                        Answer<span style="color:Red">*</span>
                                    </td>
                                    <td class="rcd-tableCell" style="width: 60%">
                                        <asp:TextBox ID="txtEditAnswer" runat="server" ValidationGroup="CheckEdit" SkinID="textboxSkin" AutoCompleteType="disabled" MaxLength="100" Width="200px" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEditAnswer" runat="server" ControlToValidate="txtEditAnswer"
                                            SetFocusOnError="true" InitialValue="" ErrorMessage="Please enter answer" ValidationGroup="CheckEdit" Display="None"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceEditAnswer" runat="server" TargetControlID="rfvEditAnswer"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                         <cc1:TextBoxWatermarkExtender ID="twEditAnswer" runat="server" TargetControlID="txtEditAnswer"
                                            WatermarkText="Enter Answer Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                        
                                        
                                        
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:Button ID="btnEditProfile" runat="server" SkinID="buttonSkin" Text="Edit Profile"
                                            ValidationGroup="CheckEdit" OnClick="btnEditProfile_Click" />
                                    </td>
                                </tr>
                                
                                </table>
                                </div> 
                                
                                
                                </td>
                           </tr>
                                
                         </table>
                        </div>
                        <div id="divEditUserFields" runat="server">
                            <table style="width: 100%">
                               <tr>
                                <td class="rcd-FieldTitle" style="width: 40%">
                                  User Name
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
                        </div>
                        
                        <div id="divMessage" runat="server">
                          <table style="width: 100%;height:200px">
                            <tr>
                              <td align="center" class="rcd-tableCellCenterAlign">
                                 <asp:Label ID="lblMsgDiv" runat="server" SkinID="SkinLabel"></asp:Label>                                     
                              </td>                            
                            </tr>
                          </table>                        
                        </div>                                                
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
