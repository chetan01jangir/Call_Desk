<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/MasterPage.master"  Theme="SkinFile"
 CodeFile="ContactUs.aspx.cs" Inherits="User_ContactUs" Title=":: Call Desk - Contact Us ::" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
  <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        
                        <td class="rcd-TopHeaderBlue">
                            Contact Us
                        </td>
                       
                    </tr>
					<tr>
        <td><h3 style="color:Maroon">*Call desk support team available Between 9.30AM - 6.30PM. </h3></td>
        </tr>
		<tr>
        <td><h3 style="color:Maroon">*Lunch hour at 1.30PM - 2.15 PM. </h3></td>
        </tr>
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
        <img src="../Images/CalldeskContactDetails.jpg" alt="" title="" />
        </td>
        </tr>    
        
        
</table>

</asp:Content>