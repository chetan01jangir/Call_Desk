<%@ Page Language="C#" AutoEventWireup="true"
 CodeFile="MContactUs.aspx.cs" Inherits="User_ContactUs" Title=":: Call Desk - Contact Us ::" %>
<%@ Register TagPrefix="CP" TagName="TitleBar" Src="~/UserControls/MenuUserControl.ascx" %>


 <html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
  <link href="../App_Themes/mrcd.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
	<div class="MobileMenu">
	<CP:TitleBar Title="User Control Test" TextColor="red"  Padding=10 runat="server" />
	</div>
	<div class="clear"></div>
	<div>
   
 <table width="100%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        
                        <td class="rcd-TopHeaderBlue">
                            Contact Us
                        </td>                       
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
                         <tr class="rcd-TableHeader">
                            <td>System</td>
                            <td>Contact Person</td>
                            <td>Contact No (Std Code :022)</td>
                            <td style="width: 178px">Mail Id </td>
                         </tr>
						 <tr>
                             <td class="rcd-FieldTitle" width="25%">
                                POS </td>
                            <td class="rcd-tableCell" width="25%">
                            
                                <asp:Label ID="Label82" runat="server" Text="Deepali - East & West Zone" CssClass="rcd-label"></asp:Label>  
                                 <br />
                                <asp:Label ID="Label83" runat="server" Text="Prasad - South & North Zone" CssClass="rcd-label"></asp:Label>                              
                                
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%">                             
                                
                                 <asp:Label ID="Label85" runat="server" Text="30385708" CssClass="rcd-label"></asp:Label> 
                                   <br />
                                 <asp:Label ID="Label86" runat="server" Text="30385709 & 30385710" CssClass="rcd-label"></asp:Label>   
                                    
                                                                                                
                                </td>   
                             <td class="rcd-tableCell" style="width: 178px">  
                                 <asp:Label ID="Label88" runat="server" Text="deepali.chaudhari@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                   <br />          
                                 <asp:Label ID="Label89" runat="server" Text="Prasad.Dalvi@rcap.co.in" CssClass="rcd-label"></asp:Label>                                
                                 
                             </td>   
                         </tr> 
                         <tr>
                          <td class="rcd-FieldTitle" width="25%">
                                MPOS </td>
                                
                           <td class="rcd-tableCell" width="25%">                            
                            <asp:Label ID="Label78" runat="server" Text="Deepali Chaudhari" CssClass="rcd-label"></asp:Label>                              
                          </td>                             
                           <td class="rcd-FieldTitleNew" width="25%">  
                             <asp:Label ID="Label79" runat="server" Text="30385708" CssClass="rcd-label"></asp:Label> 
                           </td>
                             <td class="rcd-tableCell" style="width: 178px">  
                                 <asp:Label ID="Label80" runat="server" Text="Deepali.Chaudhari@rcap.co.in" CssClass="rcd-label"></asp:Label>
                            </td>                         
                         </tr>
						 <tr>
                             <td class="rcd-FieldTitle" width="25%">
                                ICM</td>
                            <td class="rcd-tableCell" width="25%">
                            
                                <asp:Label ID="Label7" runat="server" Text="Shailesh Ghangale" CssClass="rcd-label"></asp:Label>                              
                                 <br />
                                <asp:Label ID="Label8" runat="server" Text="Amol Shiragave" CssClass="rcd-label"></asp:Label>                              
                                <br />
                                <asp:Label ID="Label94" runat="server" Text="Madan Shedge" CssClass="rcd-label"></asp:Label> 
                                
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%">
                                 <asp:Label ID="Label9" runat="server" Text="30385731" CssClass="rcd-label"></asp:Label>   
                                 <br />
                                 <asp:Label ID="Label10" runat="server" Text="30385731" CssClass="rcd-label"></asp:Label> 
                                  <br />
                                  <asp:Label ID="Label95" runat="server" Text="30385729" CssClass="rcd-label"></asp:Label>                                                             
                                </td>   
                             <td class="rcd-tableCell" style="width: 178px">             
                                 <asp:Label ID="Label11" runat="server" Text="Shailesh.Ghangale@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                  <br />         
                                 <asp:Label ID="Label12" runat="server" Text="Amol.Shiragave@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                  <br />
                                 <asp:Label ID="Label96" runat="server" Text="Madan.shedge@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                
                             </td>   
                         </tr>
                         <tr>
                         
                            <td class="rcd-FieldTitle" width="25%">
                                Portal</td>
                            <td class="rcd-tableCell" width="25%">
                            
                               <asp:Label ID="lblFaisalName" runat="server" Text="Faisal Sakharkar" CssClass="rcd-label"></asp:Label>
                                <br />
                                <asp:Label ID="lblSagarHName" runat="server" Text="Rishikesh Chavan" CssClass="rcd-label"></asp:Label>                             
                                
                                <br />                                
                                <asp:Label ID="lblJyotiName" runat="server" Text="Jyoti Godbole" CssClass="rcd-label"></asp:Label>
                                
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%">
                                <asp:Label ID="lblFaisalLN" runat="server" Text="30385737" CssClass="rcd-label"></asp:Label> 
                                 <br />                              
                                 <asp:Label ID="lblSagarLN" runat="server" Text="30385729" CssClass="rcd-label"></asp:Label>
                                 <br />
                                <asp:Label ID="lblJyotiLN" runat="server" Text="30385737" CssClass="rcd-label"></asp:Label>                                 
                                </td>   
                              <td class="rcd-tableCell" style="width: 178px">
                                 <asp:Label ID="lblFaisalEmail" runat="server" Text="Faisal.Sakharkar@rcap.co.in" CssClass="rcd-label"></asp:Label>                        
                                  <br/>
                                 <asp:Label ID="lblSagarHEmail" runat="server" Text="rgicl.onlinesupport@rcap.co.in" CssClass="rcd-label"></asp:Label>                                
                                  <br />
                                 <asp:Label ID="lblJyotiEmail" runat="server" Text="jyoti.godbole@rcap.co.in" CssClass="rcd-label"></asp:Label>
                             </td>
                         </tr>
                         
                         <tr>
                             <td class="rcd-FieldTitle" width="25%">
                                RPAS</td>
                            <td class="rcd-tableCell" width="25%">
                            
                                <asp:Label ID="Label34" runat="server" Text="Ganesh" CssClass="rcd-label"></asp:Label>  
                                 <br />
                                <asp:Label ID="Label35" runat="server" Text="Bapuso Powar" CssClass="rcd-label"></asp:Label>                              
                                
                                                            
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%">                             
                                
                                 <asp:Label ID="Label36" runat="server" Text="30385706" CssClass="rcd-label"></asp:Label> 
                                   <br />
                                 <asp:Label ID="Label37" runat="server" Text="30385707" CssClass="rcd-label"></asp:Label>   
                                                                                                
                                </td>   
                             <td class="rcd-tableCell" style="width: 178px">  
                                 <asp:Label ID="Label38" runat="server" Text="Rgicl.Siriussupport@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                   <br />          
                                 <asp:Label ID="Label39" runat="server" Text="Bapuso.Powar@rcap.co.in" CssClass="rcd-label"></asp:Label>                                
                             </td>   
                         </tr> 
                         <tr>
                             <td class="rcd-FieldTitle" width="25%">
                                MOM</td>
                            <td class="rcd-tableCell" width="25%">
                            
                                <asp:Label ID="Label1" runat="server" Text="Rahul.Upadhyaye" CssClass="rcd-label"></asp:Label>                              
                                  <br/> 
                                 <asp:Label ID="lblmomnamesecond" runat="server" Text="Alok Asthana" CssClass="rcd-label"></asp:Label>                            
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%">
                                 <asp:Label ID="Label3" runat="server" Text="30385732" CssClass="rcd-label"></asp:Label>
                                 <br/>
                                 <asp:Label ID="lblmomcontactsecond" runat="server" Text="30385716" CssClass="rcd-label"></asp:Label>   
                                </td>   
                             <td class="rcd-tableCell" style="width: 178px">             
                                 <asp:Label ID="Label5" runat="server" Text="Rahul.Upadhyaye@RELIANCEADA.COM" CssClass="rcd-label"></asp:Label>
                                 <br/>
                                 <asp:Label ID="lblmomemailsecond" runat="server" Text="alok.asthana@rcap.co.in" CssClass="rcd-label"></asp:Label>
                             </td>   
                         </tr>
                         
                         <tr>
                             <td class="rcd-FieldTitle" width="25%">
                                Moss / Online Portal </td>
                            <td class="rcd-tableCell" width="25%">
                            
                                <asp:Label ID="Label52" runat="server" Text="Rishikesh Chavan" CssClass="rcd-label"></asp:Label>  
                                 <br />
                                <asp:Label ID="lblalok" runat="server" Text="Alok Asthana" CssClass="rcd-label"></asp:Label>                              
                                
                                                            
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%">                             
                                
                                 <asp:Label ID="Label54" runat="server" Text="30385729" CssClass="rcd-label"></asp:Label> 
                                   <br />
                                 <asp:Label ID="Label55" runat="server" Text="30385716" CssClass="rcd-label"></asp:Label>   
                                                                                                
                                </td>   
                             <td class="rcd-tableCell" style="width: 178px">  
                                 <asp:Label ID="Label56" runat="server" Text="Rgicl.OnlineSupport@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                   <br />          
                                 <asp:Label ID="Label57" runat="server" Text="alok.asthana@rcap.co.in" CssClass="rcd-label"></asp:Label>                                
                             </td>   
                         </tr> 
                         <tr>
                             <td class="rcd-FieldTitle" width="25%">
                                Genisys Configurator</td>
                            <td class="rcd-tableCell" width="25%">
                            
                                <asp:Label ID="Label28" runat="server" Text="Ramdas" CssClass="rcd-label"></asp:Label>  
                                 <br />
                                <asp:Label ID="Label29" runat="server" Text="Vivek" CssClass="rcd-label"></asp:Label>                              
                                
                                                            
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%">                             
                                
                                 <asp:Label ID="Label30" runat="server" Text="30385796" CssClass="rcd-label"></asp:Label> 
                                   <br />
                                 <asp:Label ID="Label31" runat="server" Text="30385798" CssClass="rcd-label"></asp:Label>   
                                                                                                
                                </td>   
                             <td class="rcd-tableCell" style="width: 178px">  
                                 <asp:Label ID="Label32" runat="server" Text="ramdas.misal@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                   <br />          
                                 <asp:Label ID="Label33" runat="server" Text="Vivek.Dubey@rcap.co.in" CssClass="rcd-label"></asp:Label>                                
                             </td>   
                         </tr>
                         <tr>
                             <td class="rcd-FieldTitle" width="25%">
                                Call Desk</td>
                            <td class="rcd-tableCell" width="25%">
                            
                                <asp:Label ID="lblCDName" runat="server" Text="Faisal Sakharkar" CssClass="rcd-label"></asp:Label>                              
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%">
                                 <asp:Label ID="lblCDLn" runat="server" Text="30385737" CssClass="rcd-label"></asp:Label>                                                                
                                </td>   
                             <td class="rcd-tableCell" style="width: 178px">                           
                                 <asp:Label ID="lblCDEmail" runat="server" Text="Faisal.Sakharkar@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                
                             </td>   
                         </tr> 
                         <tr>
                          <td class="rcd-FieldTitle" width="25%">
                                CMS</td>
                            <td class="rcd-tableCell" width="25%">
                            
                                 <asp:Label ID="Label91" runat="server" Text="Renuka Patil" CssClass="rcd-label"></asp:Label>                              
                                
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%">
                                  <asp:Label ID="Label92" runat="server" Text="30385732" CssClass="rcd-label"></asp:Label>                                                                
                                 
                                </td>   
                             <td class="rcd-tableCell" style="width: 178px">                           
                                 <asp:Label ID="Label93" runat="server" Text="Renuka.Patil@rcap.co.in" CssClass="rcd-label"></asp:Label>                                                                
                             </td>  
                         </tr>
                         <tr>
                             <td class="rcd-FieldTitle" width="25%" style="height: 18px">
                                TPCMS</td>
                            <td class="rcd-tableCell" width="25%" style="height: 18px">
                            
                                <asp:Label ID="lblTpName" runat="server" Text="Sushil Sonawane" CssClass="rcd-label"></asp:Label>                              
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%" style="height: 18px">
                                 <asp:Label ID="lblTpLN" runat="server" Text="30385734" CssClass="rcd-label"></asp:Label>                                                                
                                </td>   
                             <td class="rcd-tableCell" style="width: 178px; height: 18px;">                           
                                 <asp:Label ID="lblTpEmail" runat="server" Text="sunil.sonawane@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                
                             </td>   
                         </tr>
                         
                         
                           <tr>
                             <td class="rcd-FieldTitle" width="25%">
                                Kit</td>
                            <td class="rcd-tableCell" width="25%">
                            
                                <asp:Label ID="lblKitN1" runat="server" Text="Jyoti Godbole" CssClass="rcd-label"></asp:Label>                              
                                 <br />
                                <asp:Label ID="lblKitN2" runat="server" Text="Faisal Sakharkar" CssClass="rcd-label"></asp:Label>                              
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%">
                                 <asp:Label ID="lblKitLN1" runat="server" Text="30385737" CssClass="rcd-label"></asp:Label>   
                                 <br />
                                 <asp:Label ID="lblKitLN2" runat="server" Text="30385737" CssClass="rcd-label"></asp:Label> 
                                                                                              
                                </td>   
                             <td class="rcd-tableCell" style="width: 178px">             
                                 <asp:Label ID="lblKitE1" runat="server" Text="jyoti.godbole@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                  <br />         
                                 <asp:Label ID="lblKitE2" runat="server" Text="Faisal.Sakharkar@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                
                             </td>   
                         </tr>
                         
                         
                          
                         <tr>
                             <td class="rcd-FieldTitle" width="25%">
                                Commission</td>
                            <td class="rcd-tableCell" width="25%">
                            
                                <asp:Label ID="Label13" runat="server" Text="Ganesh Shenoy" CssClass="rcd-label"></asp:Label>                              
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%">
                                 <asp:Label ID="Label14" runat="server" Text="33123050" CssClass="rcd-label"></asp:Label>                                                                
                                </td>   
                             <td class="rcd-tableCell" style="width: 178px">                           
                                 <asp:Label ID="Label15" runat="server" Text="GANESH.P.SHENOY@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                
                             </td>   
                         </tr>                            
                          
                         
                          <tr>
                             <td class="rcd-FieldTitle" width="25%" style="height: 18px">
                                GPS</td>
                            <td class="rcd-tableCell" width="25%" style="height: 18px">
                            
                                <asp:Label ID="Label17" runat="server" Text="Amol Shiragave" CssClass="rcd-label"></asp:Label>  
                                 <br />
                                <asp:Label ID="Label16" runat="server" Text="Shailesh Ghangale" CssClass="rcd-label"></asp:Label>                              
                                
                                                            
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%" style="height: 18px">                             
                                
                                 <asp:Label ID="Label19" runat="server" Text="30385731" CssClass="rcd-label"></asp:Label> 
                                   <br />
                                 <asp:Label ID="Label18" runat="server" Text="30385731" CssClass="rcd-label"></asp:Label>   
                                                                                                
                                </td>   
                             <td class="rcd-tableCell" style="width: 178px; height: 18px;">  
                                 <asp:Label ID="Label21" runat="server" Text="Amol.Shiragave@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                   <br />          
                                 <asp:Label ID="Label20" runat="server" Text="Shailesh.Ghangale@rcap.co.in" CssClass="rcd-label"></asp:Label>                                
                             </td>   
                         </tr> 
                         <tr>
                             <td class="rcd-FieldTitle" width="25%" style="height: 18px">
                                Documentum</td>
                            <td class="rcd-tableCell" width="25%" style="height: 18px">
                            
                                <asp:Label ID="Label22" runat="server" Text="Madan Shedge" CssClass="rcd-label"></asp:Label>                              
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%" style="height: 18px">
                                 <asp:Label ID="Label23" runat="server" Text="30385729" CssClass="rcd-label"></asp:Label>                                                                
                                </td>   
                             <td class="rcd-tableCell" style="height: 18px; width: 178px;">                           
                                 <asp:Label ID="Label24" runat="server" Text="Rgicl.Applnsupport@rcap.co.in" CssClass="rcd-label"></asp:Label>                                
                             </td>   
                         </tr> 
                         <tr>
                             <td class="rcd-FieldTitle" width="25%" style="height: 18px">
                                XPAS</td>
                            <td class="rcd-tableCell" width="25%" style="height: 18px">
                            
                                <asp:Label ID="Label25" runat="server" Text="Jyoti Godbole" CssClass="rcd-label"></asp:Label>                              
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%" style="height: 18px">
                                 <asp:Label ID="Label26" runat="server" Text="30385737" CssClass="rcd-label"></asp:Label>                                                                
                                </td>   
                             <td class="rcd-tableCell" style="height: 18px; width: 178px;">                           
                                 <asp:Label ID="Label27" runat="server" Text="jyoti.godbole@rcap.co.in" CssClass="rcd-label"></asp:Label>                                
                             </td>   
                         </tr> 
                         
                          
                         
                         
                         <tr>
                             <td class="rcd-FieldTitle" width="25%" style="height: 18px">
                                ECS</td>
                            <td class="rcd-tableCell" width="25%" style="height: 18px">                            
                                <asp:Label ID="Label40" runat="server" Text="Bapuso Powar" CssClass="rcd-label"></asp:Label>    
                                <br /> 
                                 <asp:Label ID="Label97" runat="server" Text="Ganesh" CssClass="rcd-label"></asp:Label>
                                                       
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%" style="height: 18px">
                                 <asp:Label ID="Label41" runat="server" Text="30385707" CssClass="rcd-label"></asp:Label>  
                                 <br />  
                                 <asp:Label ID="Label98" runat="server" Text="30385706" CssClass="rcd-label"></asp:Label>
                                                                                             
                                </td>   
                             <td class="rcd-tableCell" style="height: 18px; width: 178px;">                           
                                 <asp:Label ID="Label42" runat="server" Text="Rgicl.Siriussupport@rcap.co.in" CssClass="rcd-label"></asp:Label>                                
                                 <br />
                                 <asp:Label ID="Label99" runat="server" Text="Rgicl.Siriussupport@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                 
                             </td>   
                         </tr> 
                          
                         
                          <tr>
                             <td class="rcd-FieldTitle" width="25%" style="height: 18px">
                                Credence Appl</td>
                            <td class="rcd-tableCell" width="25%" style="height: 18px">
                            
                                <asp:Label ID="Label46" runat="server" Text="Ganesh" CssClass="rcd-label"></asp:Label>  
                                 <br />
                                <asp:Label ID="Label47" runat="server" Text="Harish" CssClass="rcd-label"></asp:Label>                              
                                
                                                            
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%" style="height: 18px">                             
                                
                                 <asp:Label ID="Label48" runat="server" Text="30385706" CssClass="rcd-label"></asp:Label> 
                                   <br />
                                 <asp:Label ID="Label49" runat="server" Text="30385736" CssClass="rcd-label"></asp:Label>   
                                                                                                
                                </td>   
                             <td class="rcd-tableCell" style="width: 178px; height: 18px;">  
                                 <asp:Label ID="Label50" runat="server" Text="Rgicl.Siriussupport@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                   <br />          
                                 <asp:Label ID="Label51" runat="server" Text="Rgicl.Siriussupport@rcap.co.in" CssClass="rcd-label"></asp:Label>                                
                             </td>   
                         </tr> 
                          
                         
                           <tr>
                             <td class="rcd-FieldTitle" width="25%" style="height: 18px">
                                FAB</td>
                            <td class="rcd-tableCell" width="25%" style="height: 18px">
                            
                                <asp:Label ID="Label58" runat="server" Text="Jyoti Godbole" CssClass="rcd-label"></asp:Label>                              
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%" style="height: 18px">
                                 <asp:Label ID="Label59" runat="server" Text="30385737" CssClass="rcd-label"></asp:Label>                                                                
                                </td>   
                             <td class="rcd-tableCell" style="height: 18px; width: 178px;">                           
                                 <asp:Label ID="Label60" runat="server" Text="jyoti.godbole@rcap.co.in" CssClass="rcd-label"></asp:Label>                                
                             </td>   
                         </tr> 
                         
                           <tr>
                             <td class="rcd-FieldTitle" width="25%" style="height: 18px">
                                ICE </td>
                            <td class="rcd-tableCell" width="25%" style="height: 18px">
                                 <asp:Label ID="lblicmnamefirst" runat="server" Text="Asrar Ansari" CssClass="rcd-label"></asp:Label>
                                                           
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%" style="height: 18px">
                                  <asp:Label ID="lblicmcontfirst" runat="server" Text="65298447" CssClass="rcd-label"></asp:Label>
                                
                                </td>   
                             <td class="rcd-tableCell" style="height: 18px; width: 178px;">                           
                                  <asp:Label ID="lblicmemailfirst" runat="server" Text="support.rgicl@krishmark.com" CssClass="rcd-label"></asp:Label>
                           
                             </td>   
                         </tr> 
                         <tr>
                             <td class="rcd-FieldTitle" width="25%" style="height: 18px">
                                Motoveys</td>
                            <td class="rcd-tableCell" width="25%" style="height: 18px">
                            
                                <asp:Label ID="Label103" runat="server" Text="Vaibhav Ghanekar" CssClass="rcd-label"></asp:Label>                                 
                                                             
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%" style="height: 18px">
                                 <asp:Label ID="Label105" runat="server" Text="30385779" CssClass="rcd-label"></asp:Label> 
                                </td>   
                             <td class="rcd-tableCell" style="height: 18px; width: 178px;">                           
                                 <asp:Label ID="Label107" runat="server" Text="Motoveys.Support@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                                                
                             </td>   
                         </tr> 
                         <tr>
                             <td class="rcd-FieldTitle" width="25%" style="height: 18px">
                                Omniscan</td>
                            <td class="rcd-tableCell" width="25%" style="height: 18px">
                            
                                <asp:Label ID="lblomnname" runat="server" Text="Alok Asthana" CssClass="rcd-label"></asp:Label>                                 
                                                             
                             </td>
                             <td class="rcd-FieldTitleNew" width="25%" style="height: 18px">
                                 <asp:Label ID="lblomncontact" runat="server" Text="30385716" CssClass="rcd-label"></asp:Label> 
                                </td>   
                             <td class="rcd-tableCell" style="height: 18px; width: 178px;">                           
                                 <asp:Label ID="lblomnemail" runat="server" Text="alok.asthana@rcap.co.in" CssClass="rcd-label"></asp:Label>
                                                                
                             </td>   
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
</div>

  </form>
</body>
</html>
