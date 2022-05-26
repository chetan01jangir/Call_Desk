<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="IMDFeedbackReport.aspx.cs" Inherits="Reports_IMDFeedbackReport" Theme="SkinFile"
    Title=":: IMD Feedback - Reports ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<asp:Panel ID="pnlNoAutho" runat="server">
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue" style="height: 22px">
                           IMD Feedback Reports
                        </td>
                    </tr>
                </table>                
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
              <asp:Label ID="Label1" runat="server" Text="You are not authorised to view this page." SkinID="SkinLabel"></asp:Label>        
            </td>        
        </tr>    
    </table>   
   </asp:Panel>

<asp:Panel ID="pnlAutho" runat="server">
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="rcd-TopHeaderBlue" style="height: 22px">
                            IMD Feedback Reports
                        </td>
                    </tr>
                </table>
                <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
                <asp:UpdatePanel id="UpdatePanel1" runat="server">
                    <contenttemplate>
                        <asp:Label ID="lblMessage" runat="server" SkinID="SkinLabel"></asp:Label>
                    </contenttemplate>
                   
                </asp:UpdatePanel>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                <tr>
                 <td class="rcd-FieldTitle" style="width: 40%">
                    IMD User ID
                  </td>
                  <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtimduserid" runat="server"></asp:TextBox>
                                    </td>
                                   <%-- <td>
                                        <asp:RequiredFieldValidator ID="rfvtxtimduserid" runat="server" ControlToValidate="txtimduserid"
                                            ErrorMessage="Enter IMD User ID" ValidationGroup="CheckData" Display="None" />
                                        <cc1:ValidatorCalloutExtender ID="vceimduserid" TargetControlID="rfvtxtimduserid" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>--%>
                                 </tr>
                            </table>
                   </td>
                  </tr>
                     <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            From Date
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img alt="Icon" style="cursor: hand" src="../Images/Calander_New.jpg" id="imgFromDate" />
                                    </td>
                                    <td>
                                        <cc1:CalendarExtender ID="ceFromDate" Format="MM/dd/yyyy" TargetControlID="txtFromDate"
                                            PopupButtonID="imgFromDate" runat="server">
                                        </cc1:CalendarExtender>
                                        <!--OnClientDateSelectionChanged="checkDate"-->
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                            ErrorMessage="Select from date" ValidationGroup="CheckData" Display="None" />
                                        <cc1:ValidatorCalloutExtender ID="vceFromDate" TargetControlID="rfvFromDate" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 40%">
                            To Date
                        </td>
                        <td class="rcd-tableCell" style="width: 60%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img alt="Icon" style="cursor: hand" src="../Images/Calander_New.jpg" id="imgToDate" />
                                    </td>
                                    <td>
                                        <cc1:CalendarExtender ID="ceToDate" Format="MM/dd/yyyy" TargetControlID="txtToDate"
                                            PopupButtonID="imgToDate" runat="server">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvtxtToDate" runat="server" ControlToValidate="txtToDate"
                                            ErrorMessage="Select to date" Font-Bold="True" Font-Size="Medium" ValidationGroup="CheckData"
                                            Display="None" />
                                        <cc1:ValidatorCalloutExtender ID="vceToDate" TargetControlID="rfvtxtToDate" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <asp:CompareValidator ID="cmpvToDate" runat="server" ControlToValidate="txtToDate"
                                            ControlToCompare="txtFromDate" ErrorMessage="To date should be greater than from date"
                                            Operator="GreaterThanEqual" Type="Date" Display="None" ValidationGroup="CheckData">
                                        </asp:CompareValidator>
                                        <cc1:ValidatorCalloutExtender ID="vcecmpvToDate" TargetControlID="cmpvToDate" runat="server"
                                            WarningIconImageUrl="../Images/Warning1.jpg">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="CheckData"
                                SkinID="buttonSkin" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" SkinID="buttonSkin" />
                        </td>
                    </tr>
                </table>
                </td>
                </tr>
        </table>
</asp:Panel>
</asp:Content>
