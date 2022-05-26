<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserConfirmation.aspx.cs" Inherits="User_Confirmation" MasterPageFile="~/Masters/MasterPage.master"  
Theme="SkinFile" Title=":: Call Desk - User Confirmation ::" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
<script language="javascript" type="text/javascript">

    function Savedata() {
        var x;
        //if (confirm("Do you really want to close the call!") == true) {
        if (confirm("Please click on ok to confirm that the given issue stands resolved.") == true) {
            return true;
        } else {
            return false;
        }

    }

</script>
   <table width="100%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue">
                            User Confirmation
                        </td>
                    </tr>
                </table>
                <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
                
                
            </td>
        </tr>
        </table>
        <table> 
        <tr id="trTicketNo" runat="server">
        
       
                               <td class="rcd-FieldTitle" style="width: 40%">
                            Ticket Number
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <asp:TextBox ID="txtTicketNumber" runat="server" SkinID="textboxSkin"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTicketNo" runat="server" ControlToValidate="txtTicketNumber"
                                ErrorMessage="Enter ticket number" Font-Bold="True" Font-Size="Medium" ValidationGroup="CheckData"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="vceTicketNo" TargetControlID="rfvTicketNo" runat="server"
                                WarningIconImageUrl="../Images/Warning1.jpg">
                            </cc1:ValidatorCalloutExtender>
                            <%--<cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtTicketNumber"
                                WatermarkText="Type Ticket Number Here" WatermarkCssClass="rcd-txtboxvaluewatermark" />--%>
                        </td>
                        </tr>
                        </table>

            <table>       

        <tr>
            <td colspan="2" style="height: 20px;">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">

                <table>
                <tr>
                <td>
                <asp:Button ID="btnGetdetail" ValidationGroup="CheckData" runat="server" Text="Track Call"
                                SkinID="buttonSkin" OnClick="BtnGetdetail_Click" />
                              

                <asp:Button ID="BtnTrackCall"  runat="server" Text="Refresh"
                                SkinID="buttonSkin" OnClick="BtnTrackCall_Click" />
                </td>
                </tr>
                    <tr>
                        <td>
                          <asp:GridView ID="gvCallDetails" runat="server"  AutoGenerateColumns="False"
                                        SkinID="gridviewCustomer" AllowPaging="True" OnPageIndexChanging="gvCallDetails_PageIndexChanging"
                                        OnRowCommand="gvCallDetails_RowCommand" 
                                        >
                                        <Columns>
                                            <asp:TemplateField HeaderText="View Details" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgLetter" runat="server" ImageUrl="~/images/icon-details.gif" />
            
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ticket Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTicketNo"  runat="server" Text='<%# Bind("TicketNumberPK") %>'></asp:Label>
  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Application">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApplication" runat="server" Text='<%# Bind("ApplicationName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIssueRequestType" runat="server" Text='<%# Bind("IssueRequestType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIssueRequestSubType" runat="server" Text='<%# Bind("IssueRequestSubType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Call Date" HeaderStyle-ForeColor="White" SortExpression="CallDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCallDate" runat="server" Text='<%# Eval("CallDate") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle ForeColor="White" />
                                            </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Expected Close Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpectedClosureDate" runat="server" Text='<%# Eval("ExpectedCloseDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Call Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCallStatus" runat="server" Text='<%# Bind("CallStatus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
 
                                              <%--<asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkclose" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="User Confirmation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSelect" runat="server" onclientclick="return Savedata()" CausesValidation="False" 
                                                        CommandArgument='<%# Eval("TicketNumberPK") %>'
                                                        CommandName="lnkDetails" Text="Close"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reopen/Viewdetails">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkreopen" runat="server"  CausesValidation="False" CommandArgument='<%# Eval("TicketNumberPK") %>'
                                                        CommandName="lnkreopen" Text="Reopen/Details" ToolTip="Click to reopen call or view details."></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                  
                                   
                                        </Columns>
                                    </asp:GridView>
                            <asp:Panel ID="pnlAddPopUP" runat="server">
                            </asp:Panel>
                        </td>
                    </tr>
                </table>

                <script language="javascript" type="text/javascript">
                    SearchCriteria();
                </script>

            </td>
        </tr>
    </table>

</asp:Content>