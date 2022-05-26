<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="RegionMaster.aspx.cs" Inherits="Admin_RegionMaster"
    Theme="SkinFile" Title=":: Call Desk - Add Region ::" %>

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
                            Add Region
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
                                        Zone
                                    </td>
                                    <td class="rcd-tableCell" width="60%">
                                        <asp:DropDownList ID="ddlZone" runat="server" SkinID="dropdownSkin">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvZone" runat="server" ControlToValidate="ddlZone"
                                            Display="None" ValidationGroup="CheckData" SetFocusOnError="true" ErrorMessage="Please select Zone">
                                        </asp:RequiredFieldValidator>
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
                                    <td class="rcd-FieldTitle" width="40%">
                                        Region Name</td>
                                    <td class="rcd-tableCell" width="60%">
                                        <asp:TextBox ID="txtRegion" runat="server" SkinID="textboxSkin"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ControlToValidate="txtRegion"
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter Region Name">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceRegion" TargetControlID="rfvRegion" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rcd-FieldTitle" width="40%">
                                        Region Code</td>
                                    <td class="rcd-tableCell" width="60%">
                                        <asp:TextBox ID="txtRegionCode" runat="server" SkinID="textboxSkin"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRegionCode" runat="server" ControlToValidate="txtRegionCode"
                                            Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please enter Region Code">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceRegionCode" TargetControlID="rfvRegionCode"
                                            runat="server" WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
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
                                        Region
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rcd-tableCellCenterAlign">
                                        <asp:GridView ID="gvRegion" runat="server" AllowPaging="true" SkinID="gridviewSkin"
                                            AutoGenerateColumns="false" OnPageIndexChanging="gvRegion_PageIndexChanging"
                                            OnRowCommand="gvRegion_RowCommand" OnRowDataBound="gvRegion_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="RegionID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRegionID" runat="server" Text='<%# Bind("RegionID_PK") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="ZoneID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblZoneID" runat="server" Text='<%# Bind("ZoneID_fk") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Zone Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblZoneName" runat="server" Text='<%# Bind("ZoneName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RegionCode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRegionCode" runat="server" Text='<%# Bind("RegionCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RegionName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRegionName" runat="server" Text='<%# Bind("RegionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("RegionID_PK") %>'
                                                            CausesValidation="False" CommandName="EditRegion" ImageUrl="~/Images/edit3.jpg" />
                                                        <%--<asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CausesValidation="false"
                                                            CommandName="EditRegion" CommandArgument='<%# Bind("RegionID_PK") %>'>
                                                        </asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("RegionID_PK") %>'
                                                            OnClientClick="showConfirm(this); return false;" CausesValidation="false" CommandName="DeleteRegion"
                                                            ImageUrl="~/Images/delete.jpg" ToolTip="Delete" />
                                                        <%--<asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CausesValidation="false"
                                                            CommandName="DeleteRegion" CommandArgument='<%# Bind("RegionID_PK") %>' OnClientClick="if(confirm('Are You Sure Want To Delete This Record ?')){return true;}else {return false;};javascript:__doPostBack('ctl00$cphMain$gvRegion','Delete$9')">
                                                        </asp:LinkButton>--%>
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
