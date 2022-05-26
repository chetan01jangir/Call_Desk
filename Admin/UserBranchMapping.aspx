<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile"
    AutoEventWireup="true" CodeFile="UserBranchMapping.aspx.cs" EnableEventValidation="false"
    Inherits="Admin_UserBranchMapping" Title=":: Call Desk - User Branch Mapping ::" %>

<%@ Register Src="../UserControls/wucSearchmoreUserMapping.ascx" TagName="wucSearchmoreUserMapping"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

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

    <table width="100%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class=" rcd-TopHeaderBlue">
                            User & Location Mapping
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
                <asp:Label ID="lblError" runat="server" SkinID="SkinLabel"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="99%" cellpadding="1" cellspacing="1" border="0" class=" rcd-TableBorder"
        runat="server" id="tblProposalDet">
        <tr>
            <td style="width: 40%" class=" rcd-FieldTitle">
                User ID<font color='red'> *</font></td>
            <td class=" rcd-tableCell" style="width: 60%">
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
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Get Mapped Branches"
                                SkinID="buttonSkin" />
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
            <td class="rcd-FieldTitle" style="height: 18px; width: 40%">
                Select Zone<font color='red'> *</font></td>
            <td class="rcd-tableCell" style="height: 18px; width: 60%">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlZone" runat="server" SkinID="dropdownSkin">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvZone" runat="server" ControlToValidate="ddlZone"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Select zone" />
                            <cc1:ValidatorCalloutExtender ID="vceZone" TargetControlID="rfvZone" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td>
                            <cc1:CascadingDropDown ID="ccdZone" UseContextKey="true" Category="category" TargetControlID="ddlZone"
                                PromptText="Select Zone" LoadingText="Loading Text..." ServicePath="../WebServices/AdminServices.asmx"
                                ServiceMethod="GetZone" runat="server">
                            </cc1:CascadingDropDown>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class=" rcd-FieldTitle" style="width: 40%">
                Select Region<font color='red'> *</font></td>
            <td class=" rcd-tableCell" style="width: 60%">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlRegion" runat="server" SkinID="dropdownSkin">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ControlToValidate="ddlRegion"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Select region" />
                            <cc1:ValidatorCalloutExtender ID="vceRegion" TargetControlID="rfvRegion" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td>
                            <cc1:CascadingDropDown ID="cddRegion" Category="Region" ParentControlID="ddlZone"
                                TargetControlID="ddlRegion" PromptText="Select Region" LoadingText="Loading Text..."
                                ServicePath="../WebServices/AdminServices.asmx" ServiceMethod="GetRegion" runat="server">
                            </cc1:CascadingDropDown>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class=" rcd-FieldTitle" style="width: 40%">
                Select Branch<font color='red'> *</font></td>
            <td class=" rcd-tableCell" style="width: 60%">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlBranch" runat="server" SkinID="dropdownSkin">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Select branch" />
                            <cc1:ValidatorCalloutExtender ID="vceBranch" TargetControlID="rfvBranch" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td>
                            <cc1:CascadingDropDown ID="ccdBranch" Category="Branch" ParentControlID="ddlRegion"
                                TargetControlID="ddlBranch" PromptText="Select Branch" LoadingText="Loading Text..."
                                ServicePath="../WebServices/AdminServices.asmx" ServiceMethod="GetBranch" runat="server">
                            </cc1:CascadingDropDown>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="rcd-tableCellCenterAlign">
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="CheckData" OnClick="btnSave_Click"
                    SkinID="buttonSkin" />
                <asp:Button ID="btnupdate" runat="server" Text="Update" ValidationGroup="validate"
                    OnClientClick="return confirm('Are you sure you want to Update?');" SkinID="buttonSkin"
                    Visible="False" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ValidationGroup="validate123"
                    OnClick="btnCancel_Click" SkinID="buttonSkin" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="rcd-tableCellCenterAlign">
                <asp:GridView ID="gvUserBranchMapping" runat="server" AllowPaging="True" SkinID="gridviewSkin"
                    AutoGenerateColumns="False" OnPageIndexChanging="gvUserBranchMapping_PageIndexChanging"
                    OnRowCommand="gvUserBranchMapping_RowCommand" ShowFooter="True" OnRowDataBound="gvUserBranchMapping_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="User ID" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("UserBranchMappingID_PK") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="UserName" HeaderText="User Code">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BranchName" HeaderText="Branch Name">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OfficeType" HeaderText="Branch Type">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IsBranchPrimary" HeaderText="Is Default Branch">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Edit" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Edit" CommandName="lnkEdit"
                                    CommandArgument='<%# Bind("UserBranchMappingID_PK") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <%--<asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" CommandName="lnkDelete"
                                    CommandArgument='<%# Bind("UserBranchMappingID_PK") %>' OnClientClick=" if(confirm('Are You Sure Want To Delete This Record ?')){return true;}else {return false;};javascript:__doPostBack('ctl00$cphMain$gvUserBranchMapping','Delete$9')">
                                </asp:LinkButton>--%>
                                <asp:ImageButton ID="lnkbtnDelete" runat="server" CommandArgument='<%# Bind("UserBranchMappingID_PK") %>'
                                    OnClientClick="showConfirm(this); return false;" CausesValidation="false" CommandName="lnkDelete"
                                    ImageUrl="~/Images/delete.jpg" ToolTip="Delete" />
                                <cc1:ModalPopupExtender ID="md1" runat="server" BackgroundCssClass="modalBackground"
                                    BehaviorID="mdlPopup" CancelControlID="btnNo" OkControlID="btnOk" OnCancelScript="cancelClick();"
                                    OnOkScript="okClick();" PopupControlID="div" TargetControlID="div">
                                </cc1:ModalPopupExtender>
                                <div id="div" runat="server" align="center" class="confirm" style="display: none">
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
                                <%--<asp:LinkButton ID="lnkDefault" runat="server" Text="Default" CommandName="lnkDefault"
                                    CommandArgument='<%# Bind("UserBranchMappingID_PK") %>' OnClientClick=" if(confirm('Do you want to set this branch as default for user ?')){return true;}else {return false;};javascript:__doPostBack('ctl00$cphMain$gvUserBranchMapping','Delete$9')">
                                </asp:LinkButton>--%>
                                <asp:ImageButton ID="lnkDefault" runat="server" CommandArgument='<%# Bind("UserBranchMappingID_PK") %>'
                                    OnClientClick="showConfirm(this); return false;" CausesValidation="false" CommandName="lnkDefault"
                                    ImageUrl="~/Images/default.jpg" ToolTip="Default" />
                                <cc1:ModalPopupExtender ID="md2" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="btnNo1" OkControlID="btnOk1" OnCancelScript="cancelClick();"
                                    OnOkScript="okClick();" PopupControlID="div1" TargetControlID="div1">
                                </cc1:ModalPopupExtender>
                                <div id="div1" runat="server" align="center" class="confirm" style="display: none">
                                    Are you sure you want to set the branch as default?
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
</asp:Content>
