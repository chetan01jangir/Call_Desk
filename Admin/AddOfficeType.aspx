<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddOfficeType.aspx.cs" Inherits="Admin_AddOfficeType" Title=":: Call Desk - New Office Type ::"
    Theme="SkinFile" %>

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
            document.getElementById('<%=txtOfficeType.ClientID%>').value = "";            
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
                        <td class="rcd-TopHeaderBlue">
                            Add New Office Type
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
                            Search Office Type (Like)</td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtSearchOfficeType" SkinID="textboxSkin" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSearchOfficeType" runat="server" ControlToValidate="txtSearchOfficeType"
                                ValidationGroup="CheckSearchData" SetFocusOnError="true" Display="None" ErrorMessage="Enter Office Type" />
                            <cc1:ValidatorCalloutExtender ID="vceSearchOfficeType" TargetControlID="rfvSearchOfficeType"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <asp:Button ID="btnSearchOfficeType" runat="server" ValidationGroup="CheckSearchData"
                                Text="Search" SkinID="buttonSkin" OnClick="btnSearchOfficeType_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Add Office Type
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtOfficeType" SkinID="textboxSkin" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvOfficeType" runat="server" ControlToValidate="txtOfficeType"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Enter Office Type" />
                            <cc1:ValidatorCalloutExtender ID="vceApplicationName" TargetControlID="rfvOfficeType"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtOfficeType"
                                WatermarkText="Type Application Name" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="CheckData" SkinID="buttonSkin"
                                Text="Submit" OnClick="btnSubmit_Click"/>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" SkinID="buttonSkin"/> 
                        </td>
                    </tr>
                    <tr class="rcd-TableHeader">
                        <td colspan="4">
                            Office Types</td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:GridView ID="gvOfficeTypes" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="False"
                                ShowFooter="True" AllowPaging="True" OnRowCommand="gvOfficeTypes_RowCommand" OnRowDataBound="gvOfficeTypes_RowDataBound" OnPageIndexChanging="gvOfficeTypes_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Office Type ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOfficeTypeID" runat="server" Text='<%# Bind("TypeId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Office Types">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOfficeType" runat="server" Text='<%# Bind("Location_Type") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("TypeId") %>'
                                                CausesValidation="False" CommandName="EditOfficeType" ImageUrl="~/Images/edit3.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("TypeId") %>'
                                                OnClientClick="showConfirm(this); return false;" CausesValidation="false" CommandName="DeleteOfficeType"
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
