<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddNewIssueRequestType.aspx.cs" Theme="SkinFile" Inherits="AddIssueRequestType"
    Title=":: Call Desk - New Issue Request Type ::" %>

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
            document.getElementById('<%=txtIssueRequestType.ClientID%>').value = "";            
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
                            Add New Issue / Request
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
                            Search Issue / Request Type</td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtSearchIRT" runat="server" SkinID="textboxSkin"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSearchIRT" runat="server" ControlToValidate="txtSearchIRT"
                                ValidationGroup="CheckSearchData" SetFocusOnError="true" Display="None" ErrorMessage="Enter Issue Request Type" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rfvSearchIRT"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>                            
                            <asp:Button ID="btnSearch" runat="server" ValidationGroup="CheckSearchData" Text="Search"
                                SkinID="buttonSkin" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            Issue / Request Type</td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtIssueRequestType" runat="server" SkinID="multilinetextboxSkin"
                                TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvIssueRequest" runat="server" ControlToValidate="txtIssueRequestType"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Enter Issue Request Type" />
                            <cc1:ValidatorCalloutExtender ID="vceIssueRequest" TargetControlID="rfvIssueRequest"
                                runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtIssueRequestType"
                                WatermarkText="Type Issue Request Here" WatermarkCssClass="rcd-multilinetxtboxvaluewatermark" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="Submit" runat="server" ValidationGroup="CheckData" Text="Submit"
                                OnClick="Submit_Click" SkinID="buttonSkin" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                SkinID="buttonSkin" />
                        </td>
                    </tr>
                    <tr class="rcd-TableHeader">
                        <td colspan="4">
                            Issue Request Types</td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:GridView ID="gvIssueRequestType" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="False"
                                ShowFooter="True" AllowPaging="True" OnPageIndexChanging="gvIssueRequestType_PageIndexChanging"
                                OnRowCommand="gvIssueRequestType_RowCommand" OnRowDataBound="gvIssueRequestType_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Issue Request Type ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssueRequestTypePK" runat="server" Text='<%# Bind("IssueRequestType_PK") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Request Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssueRequestType" runat="server" Text='<%# Bind("IssueRequestType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("IssueRequestType_PK") %>'
                                                CausesValidation="False" CommandName="EditIssueRequest" Text="Edit">
                                            </asp:LinkButton>--%>
                                            <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("IssueRequestType_PK") %>'
                                                CausesValidation="False" CommandName="EditIssueRequest" ImageUrl="~/Images/edit3.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("IssueRequestType_PK") %>'
                                                CausesValidation="False" CommandName="DeleteIssueRequest" Text="Delete" OnClientClick=" if(confirm('Are You Sure Want To Delete This Record ?')){return true;}else {return false;};javascript:__doPostBack('ctl00$cphMain$gvIssueRequestType','Delete$9')">
                                            </asp:LinkButton>--%>
                                            <asp:ImageButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("IssueRequestType_PK") %>'
                                                OnClientClick="showConfirm(this); return false;" CausesValidation="false" CommandName="DeleteIssueRequest"
                                                ImageUrl="~/Images/delete.jpg" ToolTip="Delete" />
                                            <cc1:ModalPopupExtender ID="md1" runat="server" BackgroundCssClass="modalBackground"
                                                BehaviorID="mdlPopup" CancelControlID="btnNo" OkControlID="btnOk" OnCancelScript="cancelClick();"
                                                OnOkScript="okClick();" PopupControlID="div" TargetControlID="div">
                                            </cc1:ModalPopupExtender>
                                            <div id="div" runat="server" class="confirm" style="display: none; text-align:center">
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
