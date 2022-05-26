<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile" AutoEventWireup="true"
    CodeFile="EscalationDetails_old.aspx.cs" Inherits="User_EscalationDetails" Title=":: Call Desk - Escalation Details ::" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField ID="antiforgery" runat="server"/>    
    <table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        
                        <td class="rcd-TopHeaderBlue">
                            ESCALATION MATRIX
                        </td>
                       
                    </tr>
					 <tr>
        <td><marquee scrollamount="3"><h3 style="color:Maroon">*You are requested to follow Escalation Matrix where TAT has expired as per 'AppSupport Expected Close Date'. </h3></marquee></td>
        </tr>
		<tr>
			<td><center><b>IT Escalation Matrix </center></td>
		</tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
       <tr runat="server" id="Operation" visible="true">
            <td>
 <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                   
                    <tr  class="rcd-TableHeader">
                        <td Width="20%">
                            Level</td>
                        <td Width="20%">
                            Name of Contact Person</td>
                        <td Width="20%">
                            Contact Details</td>
                        <td Width="20%">
                            Email Id</td>
                        <td Width="20%">
                            TAT(Bussiness hours)</td>
                    </tr>
                      <tr>
                        <td class="rcd-FieldTitle">
                            Level 1</td>
                        <td class="rcd-FieldTitle" colspan="3" style="text-align: center">
                            Concern Business Services Desk Spoc</td>
                        <td class="rcd-FieldTitle">
                            8</td>
                    </tr>
                    <tr>
                        <td  class="rcd-FieldTitle" rowspan="2">
                            Level 2</td>
                        <td class="rcd-FieldTitle">
                            Ramdas Misal</td>
                        <td class="rcd-FieldTitle">
                            MB - 7303831935 / 022-30383611</td>
                        <td class="rcd-FieldTitle">
                            ramdas.misal@relianceada.com</td>
                        <td  class="rcd-FieldTitle" rowspan="2">
                            8</td>
                    </tr>
					
					
					<tr>
						<td class="rcd-FieldTitle">
                            Sanjib Kumar</td>
                        <td class="rcd-FieldTitle">
                            MB - 9699364775 / 022-30383610</td>
                        <td class="rcd-FieldTitle">
                            sanjib.kumar@relianceada.com</td>
					</tr>
                   
					
                    <tr>
                        <td  class="rcd-FieldTitle" >
                            Level 3</td>
                        <td class="rcd-FieldTitle">
                            Neeta Gawde</td>
                        <td class="rcd-FieldTitle">
                            022-33034188</td>
                        <td class="rcd-FieldTitle">
                             Neeta.Sawant@relianceada.com</td>
                        <td  class="rcd-FieldTitle" >
                            8</td>
                    </tr>
                    
                    <tr>
                        <td class="rcd-FieldTitle" rowspan="2">
                            Level 4</td>
                        <td class="rcd-FieldTitle">
                            Prashant Pandey</td>
                        <td class="rcd-FieldTitle">
                            022-33034100</td>
                        <td class="rcd-FieldTitle">
                            Prashant.Pandey@relianceada.com</td>
                        <td class="rcd-FieldTitle" rowspan="2">
                            8</td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle">
                            Sudip Banerjee</td>
                        <td class="rcd-FieldTitle">
                            022-33034070</td>
                        <td class="rcd-FieldTitle">
                            sudip.m.banerjee@relianceada.com</td>
                    </tr>
                </table>
            </td>
            
        </tr>
    </table>
	<p style="color:Maroon">Dear User, Please note that this escalation matrix is available for those call tickets, <br>where processing group is 'IT' or 'IT-RHRS' Only.</p>
</asp:Content>
