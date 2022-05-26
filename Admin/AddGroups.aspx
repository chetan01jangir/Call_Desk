<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    Theme="SkinFile" CodeFile="AddGroups.aspx.cs" Inherits="Admin_AddGroups" Title=":: Call Desk - Add Groups ::" %>

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
            document.getElementById('<%=txtGroups.ClientID%>').value = "";            
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
                            Add Groups
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
                                        Add Groups
                                    </td>
                                    <td class="rcd-tableCell" style="width: 60%">
                                        <asp:TextBox ID="txtGroups" runat="server" SkinID="textboxSkin"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvGroups" runat="server" ControlToValidate="txtGroups"
                                            ValidationGroup="CheckData" Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Groups">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceGroups" TargetControlID="rfvGroups" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                        <cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtGroups"
                                            WatermarkText="Type Group Name Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:Button ID="btnSubmit" runat="server" SkinID="buttonSkin" Text="Submit" ValidationGroup="CheckData"
                                            OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" runat="server" SkinID="buttonSkin" Text="Cancel" />
                                    </td>
                                </tr>
                                <tr class="rcd-TableHeader">
                                    <td colspan="4">
                                        Groups
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:GridView ID="gvGroups" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="false"
                                            AllowPaging="true" OnRowCommand="gvGroups_RowCommand" OnRowDataBound="gvGroups_RowDataBound" OnPageIndexChanging="gvGroups_OnPageIndexChanging" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="GroupID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGroupId" runat="server" Text='<%# Bind("GroupID_pk") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Groups">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGroups" runat="server" Text='<%# Bind("Groups") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("GroupID_pk") %>'
                                                            CausesValidation="false" CommandName="EditGroups" ImageUrl="~/Images/edit3.jpg" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkDelete" runat="server" OnClientClick="showConfirm(this); return false;"
                                                            CausesValidation="false" CommandArgument='<%# Bind("GroupID_pk") %>' CommandName="DeleteGroups"
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
