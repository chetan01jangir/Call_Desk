<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddNewApplication.aspx.cs" Inherits="AddApplication" Title=":: Call Desk - New Application ::"
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
            document.getElementById('<%=txtApplicationName.ClientID%>').value = "";            
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
                            Add New Application
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
                            Search Application Name (Like)</td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtSearchApplicationName" SkinID="textboxSkin" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSearchApplicationName" runat="server" ControlToValidate="txtSearchApplicationName"
                                ValidationGroup="CheckSearchData" SetFocusOnError="true" Display="None" ErrorMessage="Enter Application Name" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rfvSearchApplicationName"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <asp:Button ID="btnApplicationName" runat="server" ValidationGroup="CheckSearchData" Text="Search" SkinID="buttonSkin" OnClick="btnApplicationName_Click" /></td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Add Application Name
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtApplicationName" EnableTheming="true" SkinID="textboxSkin" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvApplicationName" runat="server" ControlToValidate="txtApplicationName"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Enter Application Name" />
                            <cc1:ValidatorCalloutExtender ID="vceApplicationName" TargetControlID="rfvApplicationName"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtApplicationName"
                                WatermarkText="Type Application Name" WatermarkCssClass="rcd-txtboxvaluewatermark" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="CheckData" SkinID="buttonSkin"
                                Text="Submit" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" runat="server" SkinID="buttonSkin" Text="Cancel"/>
                        </td>
                    </tr>
                    <tr class="rcd-TableHeader">
                        <td colspan="4">
                            Applications</td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:GridView ID="gvApplication" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="False"
                                ShowFooter="True" AllowPaging="True" OnPageIndexChanging="gvApplication_PageIndexChanging"
                                OnRowCommand="gvApplication_RowCommand" OnRowDataBound="gvApplication_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Application ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApplicationID" runat="server" Text='<%# Bind("ApplicationID_PK") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Application">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApplicationName" runat="server" Text='<%# Bind("ApplicationName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("ApplicationID_PK") %>'
                                                CausesValidation="False" CommandName="EditApplication" Text="Edit">
                                            </asp:LinkButton>--%>
                                            <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("ApplicationID_PK") %>'
                                                CausesValidation="False" CommandName="EditApplication" ImageUrl="~/Images/edit3.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("ApplicationID_PK") %>'
                                                CausesValidation="False" CommandName="DeleteApplication" Text="Delete" OnClientClick=" if(confirm('Are You Sure Want To Delete This Record ?')){return true;}else {return false;};javascript:__doPostBack('ctl00$cphMain$gvApplication','Delete$9')">
                                            </asp:LinkButton>--%>
                                            <asp:ImageButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("ApplicationID_PK") %>'
                                                OnClientClick="showConfirm(this); return false;" CausesValidation="false" CommandName="DeleteApplication"
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
