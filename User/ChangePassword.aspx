<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile"
    AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="User_ChangePassword"
    Title=":: Call Desk - Change Password ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue">
                            Change Password
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
                <asp:Label ID="lblMessage" SkinID="SkinLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Old Password
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtOldPassword" runat="server" SkinID="textboxSkin" TextMode="Password"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvOldPassword1" runat="server" ControlToValidate="txtOldPassword"
                                ErrorMessage="<img height=15 width=15 src='../Images/smallfail.gif' title='Enter password' >"
                                Font-Bold="True" Font-Size="Medium" ValidationGroup="CheckData" Display="Dynamic" />--%>
                            <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txtOldPassword"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Enter password" />
                            <cc1:ValidatorCalloutExtender ID="vceOldPassword" TargetControlID="rfvOldPassword"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            <strong>New Password</strong>
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtNewPassword" runat="server" SkinID="textboxSkin" TextMode="Password"></asp:TextBox>
                            <asp:TextBox ID="txtUserName" runat="server" Style="visibility: hidden"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvNewPassword1" runat="server" ControlToValidate="txtNewPassword"
                                ErrorMessage="<img height=15 width=15 src='../Images/smallfail.gif' title='Enter new password' >"
                                Font-Bold="True" Font-Size="Medium" ValidationGroup="CheckData" Display="Dynamic" />--%>
                            <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Enter new password" />
                            <cc1:ValidatorCalloutExtender ID="vceNewPassword" TargetControlID="rfvNewPassword"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="cvNewPassword" Operator="NotEqual" runat="server" SetFocusOnError="true"
                                Display="None" ControlToCompare="txtUserName" ControlToValidate="txtNewPassword"
                                ValidationGroup="CheckData" ErrorMessage="New Password can not be same as UserName">
                            </asp:CompareValidator>
                            <cc1:ValidatorCalloutExtender ID="vceNewPwd" WarningIconImageUrl="../Images/Warning1.jpg"
                                TargetControlID="cvNewPassword" runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:PasswordStrength ID="PasswordStrength1" TargetControlID="txtNewPassword" StrengthIndicatorType="Text"
                                TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent" runat="server"
                                RequiresUpperAndLowerCaseCharacters="true" PreferredPasswordLength="8" MinimumNumericCharacters="1"
                                PrefixText="Strength:" DisplayPosition="RightSide" MinimumSymbolCharacters="1"
                                MinimumUpperCaseCharacters="1" BarBorderCssClass="barIndicatorBorder" StrengthStyles="barIndicator_poor; barIndicator_weak; barIndicator_good; barIndicator_strong; barIndicator_excellent">
                            </cc1:PasswordStrength>
                             <!-- [CR-18] Password Vulnaribility Start -->
                              <asp:RegularExpressionValidator ID="revtxtNewPassword" runat="server" ControlToValidate="txtNewPassword"
                                ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                                ErrorMessage="Password should be Minimum 8 characters at least 1 Alphabet, 1 Number and 1 Special Character"
                                Display="None" ValidationGroup="CheckData"> 
                            </asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="vcrevtxtNewPassword" runat="server" TargetControlID="revtxtNewPassword"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                             <!-- [CR-18] Password Vulnaribility End -->
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            <strong>Confirm Password</strong>
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtConfirmPassword" runat="server" SkinID="textboxSkin" TextMode="Password"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvConfirmPassword1" runat="server" ControlToValidate="txtConfirmPassword"
                                ErrorMessage="<img height=15 width=15 src='../Images/smallfail.gif' title='Enter confirm password' >"
                                Font-Bold="True" Font-Size="Medium" ValidationGroup="CheckData" Display="Dynamic" />--%>
                            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Enter confirm password" />
                            <asp:CompareValidator ID="cmpPassword" runat="server" ErrorMessage="Password entered does not match"
                                ControlToCompare="txtNewPassword" SetFocusOnError="true" ValidationGroup="CheckData"
                                Display="None" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
                            <cc1:ValidatorCalloutExtender ID="vceConfirmPassword" TargetControlID="rfvConfirmPassword"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:ValidatorCalloutExtender ID="vcePassword" TargetControlID="cmpPassword" runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnSubmit" ValidationGroup="CheckData" runat="server" OnClick="btnSubmit_Click"
                                Text="Submit" SkinID="buttonSkin" />
                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                SkinID="buttonSkin" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
