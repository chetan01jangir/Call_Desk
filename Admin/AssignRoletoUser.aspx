<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="AssignRoletoUser.aspx.cs" Inherits="Admin_AssignRoletoUser" Theme="SkinFile"
    Title=":: Call Desk - Assign Role to User ::" %>

<%@ Register Src="../UserControls/wucSearchmoreUserMapping.ascx" TagName="wucSearchmoreUserMapping"
    TagPrefix="uc1" %>
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
    </script>

    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue" style="height: 22px">
                            Assigned Role
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 20px">
                            <asp:Label ID="lblMessage" runat="server" SkinID="SkinLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                                <tr>
                                    <td style="width: 30%" class=" rcd-FieldTitle">
                                        User ID<font color='red'>*</font>
                                    </td>
                                    <td class=" rcd-tableCell" style="width: 70%">
                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtUserCode" runat="server" SkinID="textboxSkin" Wrap="False"></asp:TextBox>
                                                    <uc1:wucSearchmoreUserMapping ID="WucSearchmoreUserMapping1" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="rfvtxtUserCode" runat="server" ControlToValidate="txtUserCode"
                                                        ErrorMessage="Enter User Code" SetFocusOnError="true" ValidationGroup="CheckData"
                                                        Display="None" />
                                                    <cc1:ValidatorCalloutExtender ID="vcetxtUserCode" WarningIconImageUrl="../Images/Warning1.jpg"
                                                        TargetControlID="rfvtxtUserCode" runat="server">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <asp:RegularExpressionValidator ID="revtxtUserCode" runat="server" ControlToValidate="txtUserCode"
                                                        Display="None" ErrorMessage="Invalid Input" ValidationExpression="[a-z,A-Z,0-9,\s,-,/]*"
                                                        ValidationGroup="CheckData"></asp:RegularExpressionValidator>
                                                    <cc1:ValidatorCalloutExtender ID="vcetxtUserCodeRE" WarningIconImageUrl="../Images/Warning1.jpg"
                                                        TargetControlID="revtxtUserCode" runat="server">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <asp:RequiredFieldValidator ID="rfvUserInRole" runat="server" ControlToValidate="txtUserCode"
                                                        ErrorMessage="Enter User Code" SetFocusOnError="true" ValidationGroup="CheckData1"
                                                        Display="None" />
                                                    <cc1:ValidatorCalloutExtender ID="vceUserInRole" WarningIconImageUrl="../Images/Warning1.jpg"
                                                        TargetControlID="rfvUserInRole" runat="server">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Get Mapped Roles" SkinID="buttonSkin"
                                                        OnClick="btnSearch_Click" ValidationGroup="CheckData1" />
                                                </td>
                                                <td>
                                                    <cc1:AutoCompleteExtender ID="ace" runat="server" MinimumPrefixLength="1" ServiceMethod="GetEmpLoyeeNames"
                                                        ServicePath="../WebServices/WebService.asmx" TargetControlID="txtUserCode">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%" class=" rcd-FieldTitle">
                                        Role
                                    </td>
                                    <td class=" rcd-tableCell" style="width: 60%">
                                        <asp:DropDownList ID="ddlRole" runat="server" SkinID="dropdownSkin">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="ddlRole"
                                            ErrorMessage="Select role" InitialValue="0" SetFocusOnError="true" ValidationGroup="CheckData"
                                            Display="None" />
                                        <cc1:ValidatorCalloutExtender ID="vceRole" WarningIconImageUrl="../Images/Warning1.jpg"
                                            TargetControlID="rfvRole" runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign" style="height: 18px">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" SkinID="buttonSkin" ValidationGroup="CheckData"
                                            OnClick="btnSubmit_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr class="rcd-TableHeader">
                                    <td colspan="2">
                                        User in Roles
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="false" SkinID="gridviewSkin"
                                            OnRowCommand="gvRoles_RowCommand" AllowPaging="True" OnPageIndexChanging="gvRoles_PageIndexChanging"
                                            OnRowDataBound="gvRoles_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="RoleID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRoleID" runat="server" Text='<%# Bind("RoleName") %>' SkinID="SkinLabel"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Role">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRole" runat="server" Text='<%# Bind("RoleName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="IsDefault" HeaderText="Is Default Role">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="lnkDelete"
                                                            OnClientClick="if(confirm('Are You Sure Want To Delete This Record ?')){return true;}else {return false;};"
                                                            Text="Delete" CommandArgument='<%# Bind("RoleName") %>'>
                                                        </asp:LinkButton>--%>
                                                        <asp:ImageButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("RoleName") %>'
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
                                                <asp:TemplateField HeaderText="Set as Default">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkDefault" runat="server" CommandArgument='<%# Bind("RoleName") %>'
                                                            OnClientClick="showConfirm(this); return false;" CausesValidation="false" CommandName="lnkDefault"
                                                            ImageUrl="~/Images/default.jpg" ToolTip="Default" />
                                                        <cc1:ModalPopupExtender ID="md2" runat="server" BackgroundCssClass="modalBackground"
                                                            CancelControlID="btnNo1" OkControlID="btnOk1" OnCancelScript="cancelClick();"
                                                            OnOkScript="okClick();" PopupControlID="div1" TargetControlID="div1">
                                                        </cc1:ModalPopupExtender>
                                                        <div id="div1" runat="server" align="center" class="confirm" style="display: none">
                                                            Are you sure you want to set the role as default?
                                                            <asp:ImageButton ID="btnOk1" runat="server" ImageUrl="~/Images/smallsuccess.gif"
                                                                ToolTip="Yes" Width="22px" Height="22px" />
                                                            <asp:ImageButton ID="btnNo1" runat="server" ImageUrl="~/Images/Delete.gif" Width="22px"
                                                                Height="22px" ToolTip="No" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
