<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    Theme="SkinFile" EnableEventValidation="false" CodeFile="CreateUser.aspx.cs"
    Inherits="Admin_CreateUser" Title=":: Call Desk - Create User ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue" style="height: 22px">
                            Create User
                        </td>
                    </tr>
                </table>
                <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
                <asp:UpdatePanel id="UpdatePanel2" runat="server">
                    <contenttemplate>
                        <asp:Label ID="lblMessage" runat="server" SkinID="SkinLabel"></asp:Label>
                    </contenttemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Zone
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlZone" runat="server" SkinID="dropdownSkin" Width="156px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvZone" runat="server" ControlToValidate="ddlZone"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Select Zone" />
                            <cc1:ValidatorCalloutExtender ID="vceZone" TargetControlID="rfvZone" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:CascadingDropDown ID="cddZone" Category="category" TargetControlID="ddlZone"
                                PromptText="Select Zone" LoadingText="Loading Text..." ServicePath="../WebServices/AdminServices.asmx"
                                ServiceMethod="GetZone" runat="server">
                            </cc1:CascadingDropDown>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Region
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlRegion" runat="server" SkinID="dropdownSkin" Width="156px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ControlToValidate="ddlRegion"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Select Region" />
                            <cc1:ValidatorCalloutExtender ID="vceRegion" TargetControlID="rfvRegion" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:CascadingDropDown ID="ccdRegion" Category="Region" TargetControlID="ddlRegion"
                                PromptText="Select Region" ParentControlID="ddlZone" LoadingText="Loading Text..."
                                ServicePath="../WebServices/AdminServices.asmx" ServiceMethod="GetRegion" runat="server">
                            </cc1:CascadingDropDown>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle">
                            Branch
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlBranch" runat="server" Width="156px" SkinID="dropdownSkin"
                                DataTextField="BranchName" DataValueField="BranchCode">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Select Branch" />
                            <cc1:ValidatorCalloutExtender ID="vceBranch" TargetControlID="rfvBranch" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:CascadingDropDown ID="ccdBranch" Category="Branch" TargetControlID="ddlBranch"
                                PromptText="Select Branch" ParentControlID="ddlRegion" LoadingText="Loading Text..."
                                ServicePath="../WebServices/AdminServices.asmx" ServiceMethod="GetBranch" runat="server">
                            </cc1:CascadingDropDown>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            User Name
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtUserName" runat="server" SkinID="textboxSkin"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter the User Name">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceUserName" TargetControlID="rfvUserName" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtUserName"
                                WatermarkText="Type User Name Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Password
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" SkinID="textboxSkin"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter the Password">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vcePassword" TargetControlID="rfvPassword" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                             <asp:RegularExpressionValidator ID="revtxtPassword" runat="server" ControlToValidate="txtPassword"
                                ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                                ErrorMessage="Password should be Minimum 8 characters at least 1 Alphabet, 1 Number and 1 Special Character"
                                Display="None" ValidationGroup="CheckData"> 
                            </asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="vcrevtxtPassword" runat="server" TargetControlID="revtxtPassword"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Confirm Password
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtConfirmPwd" runat="server" TextMode="Password" SkinID="textboxSkin"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvConfirmPwd" runat="server" ControlToValidate="txtConfirmPwd"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please re-enter the Password ">
                            </asp:RequiredFieldValidator>
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
                            Employee Name</td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtEmployeeName" runat="server" SkinID="textboxSkin"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmployeeName" runat="server" ControlToValidate="txtEmployeeName"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter employee name">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceEmployeeName" TargetControlID="rfvEmployeeName"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:TextBoxWatermarkExtender ID="TBWE4" runat="server" TargetControlID="txtEmployeeName"
                                WatermarkText="Type Employee Name Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Employee E-mail
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtEmail" runat="server" SkinID="textboxSkin"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter the Email">
                            </asp:RequiredFieldValidator>
                            <%--<asp:RegularExpressionValidator ID="reEmail" runat="server" ControlToValidate="txtemail" 
                             ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                              ErrorMessage="Invalid email address" SetFocusOnError="true" Display="None" ValidationGroup="CheckData">
                            </asp:RegularExpressionValidator>--%>
                            <cc1:ValidatorCalloutExtender ID="vceEmail" TargetControlID="rfvEmail" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <%--<cc1:ValidatorCalloutExtender ID="vcereEmail" TargetControlID="reEmail" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>--%>
                            <cc1:TextBoxWatermarkExtender ID="TBWE5" runat="server" TargetControlID="txtEmail"
                                WatermarkText="Type Email Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Emoplyee Role
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlRole" runat="server" Width="156px" SkinID="dropdownSkin"
                                DataTextField="RoleName" DataValueField="RoleName">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvRole" runat="server" InitialValue="0" ControlToValidate="ddlRole"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please select the Role">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceRole" TargetControlID="rfvRole" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Employee Designation</td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlDesignation" runat="server" Width="156px" SkinID="dropdownSkin">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" InitialValue="0" ControlToValidate="ddlDesignation"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please select the designation">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vcwDesignation" TargetControlID="rfvDesignation"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Employee Channel</td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlChannel" runat="server" Width="156px" SkinID="dropdownSkin"
                                DataTextField="RoleName" DataValueField="RoleName">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvChannel" runat="server" InitialValue="0" ControlToValidate="ddlChannel"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please select the channel">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="rfvChannel"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                      <td class="rcd-FieldTitle" style="width: 40%">
                       User Expiration Date
                      </td>
                       <td class="rcd-tableCell" style="width: 60%">
                          <asp:TextBox ID="txtUserExpirationDate" runat="server"></asp:TextBox>
                           <img alt="Icon" style="cursor: hand" src="../Images/Calander_New.jpg" id="imgExpirationDate" />
                           <cc1:CalendarExtender ID="ceUserExpirationDate" Format="MM/dd/yyyy" TargetControlID="txtUserExpirationDate"
                           PopupButtonID="imgExpirationDate" runat="server">
                           </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvUserExpirationDate" runat="server" ControlToValidate="txtUserExpirationDate"
                             ErrorMessage="Select user expiration date" Display="None"  ValidationGroup="CheckData" /> 
                             <cc1:ValidatorCalloutExtender ID="vceUserExpirationDate" TargetControlID="rfvUserExpirationDate"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>                       
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Location Type
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:DropDownList ID="ddlLocationType" runat="server" SkinID="dropdownSkin">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLocationType" runat="server" ControlToValidate="ddlLocationType"
                                Display="None" InitialValue="0" SetFocusOnError="true" ValidationGroup="CheckData"
                                ErrorMessage="Please Select Location Type">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceLocationType" runat="server" TargetControlID="rfvLocationType"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" SkinID="buttonSkin" OnClick="btnSubmit_Click"
                                ValidationGroup="CheckData" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="buttonSkin" OnClick="btnReset_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
