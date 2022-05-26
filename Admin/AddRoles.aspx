<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    Theme="SkinFile" CodeFile="AddRoles.aspx.cs" Inherits="Admin_AddRoles" Title=":: Call Desk - Add / Manage Roles ::" %>

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
         function ClearControl()
         {            
            document.getElementById('<%=txtRole.ClientID%>').value = "";            
            return false;
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
                            Add Role
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 20px">
                            <asp:Label ID="lblMessage" runat="server" SkinID="SkinLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 40%">
                                        Search Role (Like)</td>
                                    <td class="rcd-tableCell" style="width: 60%">
                                        <asp:TextBox ID="txtRoleType" SkinID="textboxSkin" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRoleType" runat="server" ControlToValidate="txtRoleType"
                                            ValidationGroup="CheckSearchData" SetFocusOnError="true" Display="None" ErrorMessage="Enter role" />
                                        <cc1:ValidatorCalloutExtender ID="vceRoleType" TargetControlID="rfvRoleType"
                                            runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                        <asp:Button ID="btnSearchRole" runat="server" ValidationGroup="CheckSearchData"
                                            Text="Search" SkinID="buttonSkin" OnClick="btnSearchRole_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" style="width: 40%">
                                        Add Role
                                    </td>
                                    <td class="rcd-tableCell" style="width: 60%">
                                        <asp:TextBox ID="txtRole" runat="server" SkinID="textboxSkin"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="txtRole"
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please Enter the Role">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceRole" TargetControlID="rfvRole" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                        <cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtRole"
                                            WatermarkText="Type Role Name Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="CheckData"
                                            SkinID="buttonSkin" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" SkinID="buttonSkin" />
                                    </td>
                                </tr>
                                <tr class="rcd-TableHeader">
                                    <td colspan="4">
                                        Role
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:GridView ID="gvRole" SkinID="gridviewSkin" runat="server" AutoGenerateColumns="false"
                                            OnRowCommand="gvRole_RowCommand" AllowPaging="True" OnPageIndexChanging="gvRole_PageIndexChanging"
                                            OnRowDataBound="gvRole_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Role Name" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRoleID" runat="server" Text='<%# Bind("RoleId") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Role">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRole" runat="server" Text='<%# Bind("RoleName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%# Bind("RoleId") %>'
                                                            CausesValidation="false" CommandName="EditRole">
                                                        </asp:LinkButton>--%>
                                                        <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("RoleId") %>'
                                                            CausesValidation="false" CommandName="EditRole" ImageUrl="~/Images/edit3.jpg" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandArgument='<%# Bind("RoleId") %>'
                                                            CausesValidation="false" CommandName="DeleteRole" OnClientClick="    if(confirm('Are You Sure Want To Delete This Record ?')){return true;}else {return false;};javascript:__doPostBack('ctl00$cphMain$gvRole','Delete$9')">
                                                        </asp:LinkButton>--%>
                                                        <asp:ImageButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("RoleId") %>'
                                                            OnClientClick="showConfirm(this); return false;" CausesValidation="false" CommandName="DeleteRole"
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
