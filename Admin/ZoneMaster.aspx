<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="ZoneMaster.aspx.cs" Inherits="Admin_ZoneMaster" Theme="SkinFile" Title=":: Call Desk - Add Zone ::" %>

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
                            Add Zone
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
                                    <td class="rcd-FieldTitle" width="40%">
                                        Zone Name
                                    </td>
                                    <td class="rcd-tableCell" width="60%">
                                        <asp:TextBox ID="txtZone" runat="server" SkinID="textboxSkin"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvZone" runat="server" ControlToValidate="txtZone"
                                            ValidationGroup="CheckData" Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Zone">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceZone" TargetControlID="rfvZone" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" width="40%">
                                        Zone Code
                                    </td>
                                    <td class="rcd-tableCell" width="60%">
                                        <asp:TextBox ID="txtZoneCode" runat="server" SkinID="textboxSkin"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvZoneCode" runat="server" ControlToValidate="txtZoneCode"
                                            ValidationGroup="CheckData" Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Zone Code">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceZoneCode" TargetControlID="rfvZoneCode" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:Button ID="btnSubmit" runat="server" SkinID="buttonSkin" Text="Submit" ValidationGroup="CheckData"
                                            OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" runat="server" SkinID="buttonSkin" Text="Cancel" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                                <tr class="rcd-TableHeader">
                                    <td colspan="4">
                                        Zone
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:GridView ID="gvZone" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="false"
                                            AllowPaging="true" PageSize="10" OnRowCommand="gvZone_RowCommand" OnRowDataBound="gvZone_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ZoneID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblZoneID" runat="server" Text='<%# Bind("ZoneID_PK") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Zone Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblZoneCode" runat="server" Text='<%# Bind("ZoneCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Zone Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblZoneName" runat="server" Text='<%# Bind("ZoneName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("ZoneID_PK") %>'
                                                            CausesValidation="False" CommandName="EditZone" ImageUrl="~/Images/edit3.jpg" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("ZoneID_PK") %>'
                                                            OnClientClick="showConfirm(this); return false;" CausesValidation="false" CommandName="DeleteZone"
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
