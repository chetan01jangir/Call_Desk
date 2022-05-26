<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/IMD/AgentMasterPage.master"  Theme="SkinFile"
 CodeFile="ContactUs.aspx.cs" Inherits="User_ContactUs" Title=":: Call Desk - Contact Us ::" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                    <td style="font-size:medium">Note :- IT Call Support through numbers provided in call desk will be only on bussiness Working days from 10.00 am to 6.00.pm .</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
       
        
        <tr id="trIMD" runat="server">
           <%--  <td>
             
                     <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                         <tr  class="rcd-TableHeader">
                            <td Width="15%">System</td>
                            <td Width="10%">Escalation</td>
                            <td Width="20%">SPOC</td>
                            <td Width="15%">Contact No</td>
                            <td Width="40%"style="width: 178px">Mail Id </td>
                         </tr>
                        <tr>
						<td class="rcd-FieldTitle" rowspan="9">Smart Zone/Portal</td>
                            <td class="rcd-FieldTitle" rowspan="9">Level 1</td>
                            <td class="rcd-FieldTitle">Reshma Gawande</td>
                            <td class="rcd-FieldTitle">9699769262</td>
                            <td class="rcd-FieldTitle">reshma.gawande@relianceada.com</td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">Deepali Dhas</td>
                            <td class="rcd-FieldTitle">7666531184</td>
                            <td class="rcd-FieldTitle">deepali.dhas@relianceada.com</td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">Vidya Godse</td>
                            <td class="rcd-FieldTitle">9699296294</td>
                            <td class="rcd-FieldTitle">vidya.Godse@relianceada.com</td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">Nishigandha Chawan</td>
                            <td class="rcd-FieldTitle">022-30383665</td>
                            <td class="rcd-FieldTitle">nishigandha.chawan@relianceada.com</td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">Laxman Chendwankar</td>
                            <td class="rcd-FieldTitle">022-30383660</td>
                            <td class="rcd-FieldTitle">Laxman.Chendwankar@relianceada.com</td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">Sachin Babar</td>
                            <td class="rcd-FieldTitle">9699148643</td>
                            <td class="rcd-FieldTitle">sachin.Babar@relianceada.com</td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">Suraj Sharma</td>
                            <td class="rcd-FieldTitle">022-30383660</td>
                            <td class="rcd-FieldTitle">suraj.R.Sharma@relianceada.com</td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">Priyanka Parkar</td>
                            <td class="rcd-FieldTitle">022-30383682</td>
                            <td class="rcd-FieldTitle">priyanka.parkar@relianceada.com</td>
                        </tr>
                        <tr>
                            <td class="rcd-FieldTitle">Jyoti Godbole</td>
                            <td class="rcd-FieldTitle">022-30383681/8080659103</td>
                            <td class="rcd-FieldTitle">jyoti.godbole@relianceada.com</td>
                        </tr>
                       
                          <tr>
                              <td class="rcd-FieldTitle" rowspan=5>POS / MPOS</td>
                              <td class="rcd-FieldTitle" rowspan=5>Level 1</td>
                              <td class="rcd-FieldTitle">Reshma Gawande</td>
                              <td class="rcd-FieldTitle">9699769262</td>
                              <td class="rcd-FieldTitle">reshma.gawande@relianceada.com</td>
                           </tr>
                          <tr>

							<td class="rcd-FieldTitle">Suraj Sharma</td>
							<td class="rcd-FieldTitle">022-30383660</td>
							<td class="rcd-FieldTitle">suraj.R.Sharma@relianceada.com</td>

							</tr>
						  <tr>
							<td class="rcd-FieldTitle">Laxman Chendwankar</td>
							<td class="rcd-FieldTitle">022-30383660</td>
							<td class="rcd-FieldTitle">Laxman.Chendwankar@relianceada.com</td>

							</tr>
						  <tr>

							<td class="rcd-FieldTitle">Sachin Babar</td>
							<td class="rcd-FieldTitle">9699148643</td>
							<td class="rcd-FieldTitle">sachin.Babar@relianceada.com</td>

							</tr>
						  <tr>
                              <td class="rcd-FieldTitle">Sachin Rao</td>
                              <td class="rcd-FieldTitle">022-30383647</td>
                              <td class="rcd-FieldTitle">rgicl.godbsupport@relianceada.com</td>
                           </tr>
                         
                      </table>
             
             </td>  --%>

             <td>
        <img src="../Images/CalldeskContactDetails.jpg" alt="" title="" />
        </td>
        
        </tr>
        
        
</table>

</asp:Content>