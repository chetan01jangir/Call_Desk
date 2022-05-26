<%--<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile" AutoEventWireup="true"--%>
<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile" AutoEventWireup="true"
    CodeFile="EscalationDetailsITInfra.aspx.cs" Inherits="User_EscalationDetails" Title=":: Call Desk - Escalation Details ::" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        
                        <td class="rcd-TopHeaderBlue">
                            ESCALATION MATRIX FOR IT INFRA
                        </td>
                       
                    </tr>
                    <%-- <tr>
        <td><marquee scrollamount="3"><h3 style="color:Maroon">*You are requested to follow Escalation Matrix where TAT has expired as per “AppSupport Expected Close Date”. </h3></marquee></td>
        </tr>--%>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr> 
        
        <tr>
        <td colspan="2">
        <%--<img src="../Images/ATT00001.jpg" alt="" style="border-style:none"/>--%>
        <img src="../Images/IT-Infra-Support_Escalation-Matrix.jpg" alt="" style="border-style:none" title="" />
        </td>
        </tr>      
    
    </table>
</asp:Content>
