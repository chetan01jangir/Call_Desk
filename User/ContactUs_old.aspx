<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/MasterPage.master"  Theme="SkinFile"
 CodeFile="ContactUs_old.aspx.cs" Inherits="User_ContactUs" Title=":: Call Desk - Contact Us ::" %>

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
        <tr id="trSupport" runat="server">
           <td style="height: 812px">
           
    <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
 <tr  class="rcd-TableHeader">
  <td width=132 style='height:15.0pt;width:99pt'>System</td>
  <td width=128 style='width:96pt'>Escalation</td>
  <td width=137 style='width:103pt'>Team Lead</td>
  <td width=194 style='width:146pt'>SPOC</td>
  <td width=164 style='width:123pt'>Contact No</td>
  <td width=290 style='width:218pt'>Mail Id</td>
 </tr>
 <tr>
  <td rowspan=6 class="rcd-FieldTitle">SMARTZONE</td>
  <td rowspan=5 class="rcd-FieldTitle" width=128 style='width:96pt'>Level 1</td>
  <td rowspan=5 class="rcd-FieldTitle" width=137 style='width:103pt'>Deepali Dhas</td>
  <td class="rcd-FieldTitle" width=194 style='border-left:none;width:146pt'>Deepali Dhas</td>
  <td class="rcd-FieldTitle" width=164 style='border-left:none;width:123pt'>8291268030 /
  8080659103</td>
  <td class="rcd-FieldTitle" width=290 style='border-left:none;width:218pt'>deepali.dhas@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Laxman Chendwankar</td>
  <td class="rcd-FieldTitle">022-30383660</td>
  <td class="rcd-FieldTitle">laxman.chendwankar@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Nishigandha Chawan</td>
  <td class="rcd-FieldTitle">022-30383665</td>
  <td class="rcd-FieldTitle">nishigandha.chawan@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Sachin Babar</td>
  <td class="rcd-FieldTitle">8291275817</td>
  <td class="rcd-FieldTitle">sachin.babar@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Suraj Sharma</td>
  <td class="rcd-FieldTitle">022-30383660</td>
  <td class="rcd-FieldTitle">suraj.r.sharma@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle"></td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle"></td>
  <td class="rcd-FieldTitle"></td>
 </tr>
 <tr>
  <td rowspan=4 height=80 class="rcd-FieldTitle" width=132 style='height:60.0pt;border-top:
  none;width:99pt'>ICM</td>
  <td rowspan=3 class="rcd-FieldTitle">Level 1</td>
  <td rowspan=3 class="rcd-FieldTitle">Amol
  Shiragave</td>
  <td class="rcd-FieldTitle">Amol
  Shiragave ( GreenChannel )</td>
  <td class="rcd-FieldTitle">022-30383661</td>
  <td class="rcd-FieldTitle">amol.shiragave@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Dhanashri Patil</td>
  <td class="rcd-FieldTitle">8291276370</td>
  <td class="rcd-FieldTitle">dhanashri.patil@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Deepali Dhas</td>
  <td class="rcd-FieldTitle">8291268030
  / 8080659103</td>
  <td class="rcd-FieldTitle">deepali.dhas@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Amol
  Shiragave</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">022-30383661</td>
  <td class="rcd-FieldTitle">amol.shiragave@relianceada.com</td>
 </tr>
 <tr>
  <td rowspan=5 height=100 class="rcd-FieldTitle" width=132 style='height:75.0pt;
  border-top:none;width:99pt'>MOM</td>
  <td rowspan=4 class="rcd-FieldTitle">Level 1</td>
  <td rowspan=4 class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">sachin babar<span style='mso-spacerun:yes'> </span></td>
  <td class="rcd-FieldTitle">022-30383648</td>
  <td class="rcd-FieldTitle">Sachin.Babar@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Priyanka Parkar</td>
  <td class="rcd-FieldTitle">022-30383682</td>
  <td class="rcd-FieldTitle">priyanka.parkar@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Madan Shedge</td>
  <td class="rcd-FieldTitle">022-30383664</td>
  <td class="rcd-FieldTitle">madan.shedge@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Rekha Palve</td>
  <td class="rcd-FieldTitle">022-30383681</td>
  <td class="rcd-FieldTitle">rekha.palve@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">022-30383611</td>
  <td class="rcd-FieldTitle">ramdas.misal@reliancegeneral.com</td>
 </tr>
 <tr>
  <td rowspan=5 height=100 class="rcd-FieldTitle" width=132 style='height:75.0pt;
  border-top:none;width:99pt'>I-RPAS</td>
  <td rowspan=4 class="rcd-FieldTitle">Level 1</td>
  <td rowspan=4 class="rcd-FieldTitle">Rishikesh
  Chavan</td>
  <td class="rcd-FieldTitle">Rishikesh
  Chavan</td>
  <td class="rcd-FieldTitle">8080919369</td>
  <td class="rcd-FieldTitle">rushikesh.chavan@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Ketan Sarjine</td>
  <td class="rcd-FieldTitle">022-30383679</td>
  <td class="rcd-FieldTitle" width=290 style='border-top:none;width:218pt'>ketan.a.sarjine@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Priyanka Parkar</td>
  <td class="rcd-FieldTitle" width=164 style='border-left:none;width:123pt'>022-30383682</td>
  <td class="rcd-FieldTitle">priyanka.parkar@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Amol Shiragave</td>
  <td class="rcd-FieldTitle">022-30383661</td>
  <td class="rcd-FieldTitle">amol.shiragave@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Rishikesh
  Chavan</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">8080919369</td>
  <td class="rcd-FieldTitle">rushikesh.chavan@relianceada.com</td>
 </tr>
 <tr>
  <td rowspan=3 class="rcd-FieldTitle">Genisys Configurator</td>
  <td rowspan=2 class="rcd-FieldTitle">Level 1</td>
  <td rowspan=2 class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">Nishigandha
  Chawan</td>
  <td class="rcd-FieldTitle">022-30383665</td>
  <td class="rcd-FieldTitle">nishigandha.chawan@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Ramdas Misal</td>
  <td class="rcd-FieldTitle">022-30383611</td>
  <td class="rcd-FieldTitle">ramdas.misal@reliancegeneral.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">022-30383611</td>
  <td class="rcd-FieldTitle">ramdas.misal@reliancegeneral.com</td>
 </tr>
 <tr>
  <td rowspan=3 class="rcd-FieldTitle">Moss / Online Portal</td>
  <td rowspan=2 class="rcd-FieldTitle">Level 1</td>
  <td rowspan=2 class="rcd-FieldTitle">Rishikesh
  Chavan</td>
  <td class="rcd-FieldTitle">Kisan
  Powar</td>
  <td class="rcd-FieldTitle">022-30383672</td>
  <td class="rcd-FieldTitle">kisan.powar@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Rahul Upadhyaye</td>
  <td class="rcd-FieldTitle">022-30383662</td>
  <td class="rcd-FieldTitle">rahul.upadhyaye@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Rishikesh
  Chavan</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">8080919369</td>
  <td class="rcd-FieldTitle">rushikesh.chavan@relianceada.com</td>
 </tr>
 <tr>
  <td rowspan=3 class="rcd-FieldTitle">POS / MPOS</td>
  <td rowspan=2 class="rcd-FieldTitle" style='border-top:none'>Level 1</td>
  <td rowspan=2 class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">Sachin
  Babar</td>
  <td class="rcd-FieldTitle">8291275817</td>
  <td class="rcd-FieldTitle">sachin.Babar@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Rekha Palve</td>
  <td class="rcd-FieldTitle">022-30383681</td>
  <td class="rcd-FieldTitle">rekha.palve@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" style='height:15.0pt;border-top:none;border-left:
  none'>Level 2</td>
  <td class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">022-30383611</td>
  <td class="rcd-FieldTitle">ramdas.misal@reliancegeneral.com</td>
 </tr>
 <tr>
  <td rowspan=3 class="rcd-FieldTitle">CMS / TPCMS / PQMS / RCU E-Finder</td>
  <td rowspan=2 class="rcd-FieldTitle">Level 1</td>
  <td rowspan=2 class="rcd-FieldTitle">Madan
  Shedge</td>
  <td class="rcd-FieldTitle">Madan
  Shedge</td>
  <td class="rcd-FieldTitle">022-30383664</td>
  <td class="rcd-FieldTitle">madan.shedge@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Laxman Chendwankar</td>
  <td class="rcd-FieldTitle">022-30383660</td>
  <td class="rcd-FieldTitle">laxman.chendwankar@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">022-30383611</td>
  <td class="rcd-FieldTitle">ramdas.misal@reliancegeneral.com</td>
 </tr>
 <tr>
  <td rowspan=3 class="rcd-FieldTitle">Motor Plus</td>
  <td rowspan=2 class="rcd-FieldTitle">Level 1</td>
  <td rowspan=2 class="rcd-FieldTitle">Madan
  Shedge</td>
  <td class="rcd-FieldTitle">Madan
  Shedge</td>
  <td class="rcd-FieldTitle">022-30383664</td>
  <td class="rcd-FieldTitle">madan.shedge@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Yogita Landge</td>
  <td class="rcd-FieldTitle">022-30383245</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">022-30383611</td>
  <td class="rcd-FieldTitle">ramdas.misal@reliancegeneral.com</td>
 </tr>
 <tr>
  <td rowspan=4 height=80 class="rcd-FieldTitle" width=132 style='height:60.0pt;border-top:
  none;width:99pt'>HCS</td>
  <td rowspan=3 class="rcd-FieldTitle">Level 1</td>
  <td rowspan=3 class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">Suraj
  Sharma</td>
  <td class="rcd-FieldTitle">022-30383660</td>
  <td class="rcd-FieldTitle">suraj.r.sharma@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Sachin Babar</td>
  <td class="rcd-FieldTitle">8291275817</td>
  <td class="rcd-FieldTitle">sachin.babar@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Laxman Chendwankar</td>
  <td class="rcd-FieldTitle">022-30383660</td>
  <td class="rcd-FieldTitle">laxman.chendwankar@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">022-30383611</td>
  <td class="rcd-FieldTitle">ramdas.misal@reliancegeneral.com</td>
 </tr>
 <tr>
  <td rowspan=2 height=40 class="rcd-FieldTitle" width=132 style='height:30.0pt;border-top:
  none;width:99pt'>GPS</td>
  <td class="rcd-FieldTitle" width=128 style='border-top:none;border-left:none;width:96pt'>Level
  1</td>
  <td class="rcd-FieldTitle">Amol
  Shiragave</td>
  <td class="rcd-FieldTitle">Amol
  Shiragave</td>
  <td class="rcd-FieldTitle">022-30383661</td>
  <td class="rcd-FieldTitle">amol.shiragave@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">022-30383611</td>
  <td class="rcd-FieldTitle">ramdas.misal@reliancegeneral.com</td>
 </tr>
 <tr>
  <td rowspan=2 height=40 class="rcd-FieldTitle" width=132 style='height:30.0pt;border-top:
  none;width:99pt'>Documentum</td>
  <td class="rcd-FieldTitle" width=128 style='border-top:none;border-left:none;width:96pt'>Level
  1</td>
  <td class="rcd-FieldTitle">Madan
  Shedge</td>
  <td class="rcd-FieldTitle">Madan
  Shedge</td>
  <td class="rcd-FieldTitle">022-30383664</td>
  <td class="rcd-FieldTitle">madan.shedge@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">022-30383611</td>
  <td class="rcd-FieldTitle">ramdas.misal@reliancegeneral.com</td>
 </tr>
 <tr>
  <td rowspan=4 height=80 class="rcd-FieldTitle" width=132 style='height:60.0pt;border-top:
  none;width:99pt'>XPAS</td>
  <td rowspan=3 class="rcd-FieldTitle">Level 1</td>
  <td rowspan=3 class="rcd-FieldTitle">Rishikesh
  Chavan</td>
  <td class="rcd-FieldTitle">Priyanka
  Parkar</td>
  <td class="rcd-FieldTitle">022-30383682</td>
  <td class="rcd-FieldTitle">priyanka.parkar@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Madan Shedge</td>
  <td class="rcd-FieldTitle">022-30383664</td>
  <td class="rcd-FieldTitle">madan.shedge@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Rekha Palve</td>
  <td class="rcd-FieldTitle">022-30383681</td>
  <td class="rcd-FieldTitle">rekha.palve@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Rishikesh
  Chavan</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">8080919369</td>
  <td class="rcd-FieldTitle">rushikesh.chavan@relianceada.com</td>
 </tr>
 <tr>
  <td rowspan=4 height=80 class="rcd-FieldTitle" width=132 style='height:60.0pt;border-top:
  none;width:99pt'>Omniscan</td>
  <td rowspan=3 class="rcd-FieldTitle">Level 1</td>
  <td rowspan=3 class="rcd-FieldTitle">Laxman
  Chendwankar</td>
  <td class="rcd-FieldTitle">Suraj
  Sharma</td>
  <td class="rcd-FieldTitle">022-30383660</td>
  <td class="rcd-FieldTitle">suraj.r.sharma@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Laxman Chendwankar</td>
  <td class="rcd-FieldTitle">022-30383660</td>
  <td class="rcd-FieldTitle">laxman.chendwankar@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Rahul Upadhyaye</td>
  <td class="rcd-FieldTitle">022-30383662</td>
  <td class="rcd-FieldTitle">rahul.upadhyaye@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">022-30383611</td>
  <td class="rcd-FieldTitle">ramdas.misal@reliancegeneral.com</td>
 </tr>
 <tr>
  <td rowspan=2 height=40 class="rcd-FieldTitle" width=132 style='height:30.0pt;border-top:
  none;width:99pt'>Website</td>
  <td class="rcd-FieldTitle" width=128 style='border-top:none;border-left:none;width:96pt'>Level
  1</td>
  <td class="rcd-FieldTitle">Rishikesh
  Chavan</td>
  <td class="rcd-FieldTitle">Rahul
  Upadhyaye</td>
  <td class="rcd-FieldTitle">022-30383662</td>
  <td class="rcd-FieldTitle">rahul.upadhyaye@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Rishikesh
  Chavan</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">8080919369</td>
  <td class="rcd-FieldTitle">rushikesh.chavan@relianceada.com</td>
 </tr>
 <tr>
  <td rowspan=3 height=60 class="rcd-FieldTitle" style='height:45.0pt;border-top:none'>RMPIS/RAS</td>
  <td rowspan=2 class="rcd-FieldTitle">Level 1</td>
  <td rowspan=2 class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">Rahul
  Upadhyaye</td>
  <td class="rcd-FieldTitle">022-30383662</td>
  <td class="rcd-FieldTitle">rahul.upadhyaye@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Yogita Landge</td>
  <td class="rcd-FieldTitle">022-30383245</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">022-30383611</td>
  <td class="rcd-FieldTitle">ramdas.misal@reliancegeneral.com</td>
 </tr>
 <tr>
  <td rowspan=3 class="rcd-FieldTitle">FAB</td>
  <td rowspan=2 class="rcd-FieldTitle">Level 1</td>
  <td rowspan=2 class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">Rahul
  Upadhyaye</td>
  <td class="rcd-FieldTitle">022-30383662</td>
  <td class="rcd-FieldTitle">rahul.upadhyaye@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Deepali Dhas</td>
  <td class="rcd-FieldTitle">8291268030
  / 8080659103</td>
  <td class="rcd-FieldTitle">deepali.dhas@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">022-30383611</td>
  <td class="rcd-FieldTitle">ramdas.misal@reliancegeneral.com</td>
 </tr>
 <tr>
  <td rowspan=2 height=40 class="rcd-FieldTitle" width=132 style='height:30.0pt;border-top:
  none;width:99pt'>Call Desk</td>
  <td rowspan=2 class="rcd-FieldTitle">Level 1
  and Level 2</td>
  <td rowspan=2 class="rcd-FieldTitle">Faisal
  Sakharkar</td>
  <td class="rcd-FieldTitle">Nishigandha
  Chawan</td>
  <td class="rcd-FieldTitle">022-30383665</td>
  <td class="rcd-FieldTitle">nishigandha.chawan@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" >Laxman Chendwankar</td>
  <td class="rcd-FieldTitle">022-30383660</td>
  <td class="rcd-FieldTitle">laxman.chendwankar@relianceada.com</td>
 </tr>
 <tr>
  <td rowspan=2 height=40 class="rcd-FieldTitle" width=132 style='height:30.0pt;border-top:
  none;width:99pt'>Kit</td>
  <td class="rcd-FieldTitle" width=128 style='border-top:none;border-left:none;width:96pt'>Level
  1</td>
  <td class="rcd-FieldTitle">Deepali
  Dhas</td>
  <td class="rcd-FieldTitle">Deepali
  Dhas</td>
  <td class="rcd-FieldTitle">8291268030
  / 8080659103</td>
  <td class="rcd-FieldTitle">deepali.dhas@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=128 style='height:15.0pt;border-top:none;
  border-left:none;width:96pt'>Level 2</td>
  <td class="rcd-FieldTitle">Ramdas
  Misal</td>
  <td class="rcd-FieldTitle">&nbsp;</td>
  <td class="rcd-FieldTitle">022-30383611</td>
  <td class="rcd-FieldTitle">ramdas.misal@reliancegeneral.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" width=132 style='height:15.0pt;border-top:none;
  width:99pt'>RPAS</td>
  <td class="rcd-FieldTitle" width=128 style='border-top:none;border-left:none;width:96pt'>Level
  1 and Level 2</td>
  <td class="rcd-FieldTitle">Rishikesh
  Chavan</td>
  <td class="rcd-FieldTitle">Rishikesh
  Chavan</td>
  <td class="rcd-FieldTitle">8080919369</td>
  <td class="rcd-FieldTitle">rushikesh.chavan@relianceada.com</td>
 </tr>
 <tr>
  <td class="rcd-FieldTitle" style='height:15.0pt;border-top:none'>Renewal Notice</td>
  <td class="rcd-FieldTitle" width=128 style='border-top:none;border-left:none;width:96pt'>Level
  1 and Level 2</td>
  <td class="rcd-FieldTitle">Kadambari</td>
  <td class="rcd-FieldTitle">Kadambari</td>
  <td class="rcd-FieldTitle">022-30383608</td>
  <td class="rcd-FieldTitle">kadambari.kawade@relianceada.com</td>
 </tr>

                    </table>
                  
           </td>        
        </tr>
        
        <tr id="trIMD" runat="server">
             <td>
             
                     <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                         <tr class="rcd-TableHeader">
                            <td>System</td>
                            <td>Contact Person</td>
                            <td>Contact No (Std Code :022)</td>
                            <td>Mail Id </td>
                         </tr>
                          <tr>
                             <td class="rcd-FieldTitle" width="25%">
                                IMD Portal </td>
                            <td class="rcd-tableCell" width="25%">
                            
                                <asp:Label ID="Label64" runat="server" Text="Rishikesh Chavan" CssClass="rcd-label"></asp:Label>  
                                 <br />
                                <asp:Label ID="Label65" runat="server" Text="Faisal Sakharkar" CssClass="rcd-label"></asp:Label>                              
                             
                                                            
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%">                             
                                
                                 <asp:Label ID="Label66" runat="server" Text="30385729" CssClass="rcd-label"></asp:Label> 
                                   <br />
                                 <asp:Label ID="Label67" runat="server" Text="30385737" CssClass="rcd-label"></asp:Label>   
                             
                                                                                                
                                </td>   
                             <td class="rcd-tableCell" width="25%">  
                                 <asp:Label ID="Label68" runat="server" Text="Rgicl.OnlineSupport@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                   <br />          
                                 <asp:Label ID="Label69" runat="server" Text="Faisal.Sakharkar@rcap.co.in " CssClass="rcd-label"></asp:Label>                                
                                
                             </td>   
                         </tr> 
                          <tr>
                             <td class="rcd-FieldTitle" width="25%">
                                POS </td>
                            <td class="rcd-tableCell" width="25%">
                            
                                <asp:Label ID="Label70" runat="server" Text="Ajay Pisal - North Zone" CssClass="rcd-label"></asp:Label>  
                                 <br />
                                <asp:Label ID="Label71" runat="server" Text="Gopal - South Zone" CssClass="rcd-label"></asp:Label>                              
                                <br />
                                <asp:Label ID="Label76" runat="server" Text="Prasad - East & West" CssClass="rcd-label"></asp:Label>                                                         
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%">                             
                                
                                 <asp:Label ID="Label72" runat="server" Text="30385708" CssClass="rcd-label"></asp:Label> 
                                   <br />
                                 <asp:Label ID="Label73" runat="server" Text="30385743" CssClass="rcd-label"></asp:Label>   
                                   <br />
                                 <asp:Label ID="Label77" runat="server" Text="30385709" CssClass="rcd-label"></asp:Label>   
                                                                                                
                                </td>   
                             <td class="rcd-tableCell" width="25%">  
                                 <asp:Label ID="Label74" runat="server" Text="rgicl.possupport@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                   <br />          
                                 <asp:Label ID="Label75" runat="server" Text="rgicl.godbsupport@rcap.co.in" CssClass="rcd-label"></asp:Label>                                
                                  <br />
                                 <asp:Label ID="Label81" runat="server" Text="rgicl.godbsupport@rcap.co.in" CssClass="rcd-label"></asp:Label> 
                             </td>   
                         </tr> 
                         
                         
                         
                      </table>
             
             </td>  
        
        </tr>
        
        
</table>

</asp:Content>