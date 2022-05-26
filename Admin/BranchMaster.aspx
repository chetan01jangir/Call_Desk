<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="BranchMaster.aspx.cs" Inherits="Admin_BranchMasteraspx"
    Theme="SkinFile" Title=":: Call Desk - Add Branch ::" %>

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
                            Add Branch
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
                                        Zone
                                    </td>
                                    <td class="rcd-tableCell" width="60%">
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
                                    <td class="rcd-FieldTitle" width="40%">
                                        Branch Name
                                    </td>
                                    <td class="rcd-tableCell" width="60%">
                                        <asp:TextBox ID="txtBranchName" runat="server" SkinID="textboxSkin"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="txtBranchName"
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter Branch Name">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceBranch" TargetControlID="rfvBranch" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" width="40%">
                                        Branch Code
                                    </td>
                                    <td class="rcd-tableCell" width="60%">
                                        <asp:TextBox ID="txtBranchCode" runat="server" SkinID="textboxSkin"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvBranchCode" runat="server" ControlToValidate="txtBranchCode"
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter Branch Code">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceBranchCode" TargetControlID="rfvBranchCode"
                                            runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" width="40%">
                                        Office Type
                                    </td>
                                    <td class="rcd-tableCell" width="60%">
                                        <asp:DropDownList ID="ddlOfficeType" runat="server" SkinID="dropdownSkin">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvOfficeType" runat="server" InitialValue="0" ControlToValidate="ddlOfficeType"
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please select Office Type">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceOfficeType" TargetControlID="rfvOfficeType"
                                            runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
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
                                        Branch
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:GridView ID="gvBranch" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="false"
                                            AllowPaging="true" PageSize="10" OnPageIndexChanging="gvBranch_PageIndexChanging"
                                            OnRowCommand="gvBranch_RowCommand" OnRowDataBound="gvBranch_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="BranchID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranchID" runat="server" Text='<%# Bind("BranchID_PK") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Zone Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblZoneName" runat="server" Text='<%# Bind("ZoneName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Region Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRegionName" runat="server" Text='<%# Bind("RegionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BranchCode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranchCode" runat="server" Text='<%# Bind("BranchCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BranchName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranchName" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OfficeType">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfficeType" runat="server" Text='<%# Bind("OfficeType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("BranchID_PK") %>'
                                                            CausesValidation="False" CommandName="EditRegion" ImageUrl="~/Images/edit3.jpg" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("BranchID_PK") %>'
                                                            OnClientClick="showConfirm(this); return false;" CausesValidation="false" CommandName="DeleteRegion"
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
