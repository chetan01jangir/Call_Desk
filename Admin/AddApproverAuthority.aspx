<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddApproverAuthority.aspx.cs" Theme="SkinFile" Inherits="ApproverMaster"
    Title=":: Call Desk - New Approver ::" %>

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
                        <td class="rcd-TopHeaderBlue">
                            Add New Approver Authority
                        </td>
                    </tr>
                </table>
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
                            Add Approver Authority</td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtApproverAuthority" runat="server" SkinID="textboxSkin"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvApproverAuthority" runat="server" ControlToValidate="txtApproverAuthority"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Enter Approver Authority" />
                            <cc1:ValidatorCalloutExtender ID="vceApproverAuthority" TargetControlID="rfvApproverAuthority"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="CheckData" Text="Submit"
                                OnClick="btnSubmit_Click" SkinID="buttonSkin" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                SkinID="buttonSkin" />
                        </td>
                    </tr>
                    <tr class="rcd-TableHeader">
                        <td colspan="4">
                            Approver Authorities</td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:GridView ID="gvApproverAuthority" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="False"
                                ShowFooter="True" AllowPaging="True" OnPageIndexChanging="gvApproverAuthority_PageIndexChanging"
                                OnRowCommand="gvApproverAuthority_RowCommand" OnRowDataBound="gvApproverAuthority_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Approver Authority ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproverID" runat="server" Text='<%# Bind("ApproverAuthorityID_PK") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approver Authority">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproverAuthority" runat="server" Text='<%# Bind("ApproverAuthority") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("ApproverAuthorityID_PK") %>'
                                                CausesValidation="False" CommandName="EditApprover" Text="Edit">
                                            </asp:LinkButton>--%>
                                            <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("ApproverAuthorityID_PK") %>'
                                                CausesValidation="false" CommandName="EditApprover" ImageUrl="~/Images/edit3.jpg"
                                                ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("ApproverAuthorityID_PK") %>'
                                                CausesValidation="False" CommandName="DeleteApprover" Text="Delete" OnClientClick=" if(confirm('Are You Sure Want To Delete This Record ?')){return true;}else {return false;};javascript:__doPostBack('ctl00$cphMain$gvApproverAuthority','Delete$9')">
                                            </asp:LinkButton>--%>
                                            <asp:ImageButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("ApproverAuthorityID_PK") %>'
                                                OnClientClick="showConfirm(this); return false;" CausesValidation="false" CommandName="DeleteApprover"
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
</asp:Content>
