<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="MRegisterCall.aspx.cs" Inherits="User_MRegisterCall" EnableEventValidation="false"
    Title=":: Call Desk - Register New Call ::" %>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="CP" TagName="TitleBar" Src="~/UserControls/MenuUserControl.ascx" %>
    <link href="../App_Themes/mrcd.css" rel="stylesheet" type="text/css" />



    <script language="javascript" type="text/javascript">
        
        function NumericCharacter(varTextBox)
        {        
            if (event.keyCode < 48 ||event.keyCode > 57)
            {         
                  event.returnValue = false;         
            }
        }
        
        function CheckFileExists()
        {
            document.getElementById('<%=rfvUpload.ClientID%>').enabled = false;
            var fileName = document.getElementById('<%=lnkFileTemplate.ClientID%>').innerText;
            if(fileName != '')
            {
                document.getElementById('<%=rfvUpload.ClientID%>').enabled = true;
            }
            else
            {
                document.getElementById('<%=rfvUpload.ClientID%>').enabled = false;
            }
        }
        
        function EndRequestHandler(sender, args)
        {
            //var hfRemark = document.getElementById("ctl00_ContentPlaceHolder1_hfRemark");
            //creatediv('newdiv', hfRemark.value, 100, 100, 720, 237);
            CheckFileExists();
        }
        
        /*
        function creatediv(id, html, width, height, left, top)
        {
            var newdiv;
            var divid = document.getElementById("newdiv");
            if(divid != null)
            {
                newdiv = divid.id;
                if (html)
                {
                    document.getElementById("newdiv").style.display = 'inline';
                    document.getElementById("newdiv").innerHTML = html;
                }
                else
                {
                    document.getElementById("newdiv").style.display = 'none';
                }
            }
            else
            {
                newdiv = document.createElement('div');
                newdiv.setAttribute('id', id);      
                
                if (width) 
                {
                    newdiv.style.width = "auto";
                }

                if (height)
                {
                    newdiv.style.height = "auto";
                }

                if ((left || top) || (left && top))
                {
                    //newdiv.style.position = "absolute";
                    newdiv.style.position = "fixed";

                    if (left)
                    {
                       newdiv.style.left = left;
                    }

                    if (top) 
                    {
                       newdiv.style.top = top;
                    }
                }
                          
                newdiv.style.background = "#F8FBFF";
                newdiv.style.border = "1px solid #000";
                newdiv.style.fontFamily = "Arial, Tahoma,Verdana, Helvetica, sans-serif";
                newdiv.style.fontSize = "11px";
                newdiv.style.zIndex = "1";
            
                if (html)
                {
                    newdiv.innerHTML = html;
                }
                else
                {
                    newdiv.innerHTML = "";
                }
                
//                var newTR = document.createElement('TR');
//                newdiv.appendChild(newTR);    
//                  
//                var lnk = document.createElement('a');                
//                if(lnk.addEventListener)
//                {
//                    lnk.addEventListener('click', (function(i){return function(){document.body.removeChild(i)}})(newdiv), false);
//                }
//                else if(lnk.attachEvent)
//                {
//                    lnk.attachEvent('onclick', (function(i){return function(){document.body.removeChild(i)}})(newdiv));
//                }
//                else
//                {
//                    lnk.onclick = (function(i){return function(){document.body.removeChild(i)}})(newdiv);
//                }
//                lnk.href = '#';
//                lnk.appendChild(document.createTextNode("Close"));
//                newdiv.appendChild(lnk);            
                                
//                var objectToAppend;
//                objectToAppend = document.createElement("img");
//                objectToAppend.setAttribute("src","../images/close.gif");                
//                 newdiv.appendChild(objectToAppend);                                   
               
                document.body.appendChild(newdiv);                
            }
        } */
        
        function ShowMyDiv()
        {
            //var divid = document.getElementById("newdiv");
            //if(divid != null)
            //{
            //    document.getElementById("newdiv").style.visibility = 'visible';
            //}
            
             var lblRemark =  document.getElementById("ctl00_ContentPlaceHolder1_lblRemark");                                    
             var hfRemark = document.getElementById("ctl00_ContentPlaceHolder1_hfRemark");               
             lblRemark.innerHTML = hfRemark.value;
            
            
            
        }
        
        function howMany()
        {
           //  var divs = document.getElementsByTagName("div");
//            var divid = document.getElementById("newdiv");
//            
//            //var file = document.getElementById('<%=lnkFileTemplate.ClientID%>');
//            var file = document.getElementById("ctl00_ContentPlaceHolder1_lnkFileTemplate");
//            
//            if(divid != null)
//            {                      
//                document.getElementById("newdiv").style.visibility = 'hidden';
//                file.style.visibility = 'hidden';
//            }
       
                   
           var lblRemark1 = document.getElementById("ctl00_ContentPlaceHolder1_lblRemark");                     
           var file = document.getElementById("ctl00_ContentPlaceHolder1_lnkFileTemplate"); 
           file.style.visibility = 'hidden'; 
           lblRemark1.innerHTML='';                        
           lblRemark1.value='';
            
            
        }        
                
        function load() 
        {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        }            
    
    </script>


<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>


<body>
 <form id="form1" runat="server">
      <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
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
                            Register New Call
                        </td>
                    </tr>
                </table>
              
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20dp">
                <asp:Label ID="lblMessage" runat="server" SkinID="SkinLabel"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
    </table>
    <div style="min-height:1200px; background-color:White">
    <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                       <tr>
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Application Type
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList CssClass="DropDownList" ID="ddlApplicationType" onchange="howMany();" runat="server"  >
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvApplicationType" runat="server" ControlToValidate="ddlApplicationType"
                                            ValidationGroup="CheckData" SetFocusOnError="true" Display="None" ErrorMessage="Select application type" />
                                        <cc1:ValidatorCalloutExtender ID="vceApplicationType" TargetControlID="rfvApplicationType"
                                            runat="server" WarningIconImageUrl="../Images/Warning1.jpg" CssClass="CustomValidator">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <cc1:CascadingDropDown ID="ccdApplication" Category="category" TargetControlID="ddlApplicationType"
                                            PromptText="Select Application" LoadingText="Loading Text..." ServicePath="../WebServices/WebService.asmx"
                                            ServiceMethod="GetApplicationTypesByBracnch" runat="server">
                                        </cc1:CascadingDropDown>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Issue / Request Type
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlIssueRequestType" runat="server" CssClass="DropDownList">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvIssueRequestType" runat="server" ControlToValidate="ddlIssueRequestType"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Select issue request type"
                                            ValidationGroup="CheckData"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceIssueRequestType" WarningIconImageUrl="../Images/Warning1.jpg"
                                            runat="server" TargetControlID="rfvIssueRequestType" CssClass="CustomValidator">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <cc1:CascadingDropDown ID="ccdIssueRequestType" Category="IssueRequestType" TargetControlID="ddlIssueRequestType"
                                            PromptText="Select Issue Request" ParentControlID="ddlApplicationType" LoadingText="Loading Text..."
                                            ServicePath="../WebServices/WebService.asmx" ServiceMethod="GetTypeofIssueRequest"
                                            runat="server">
                                        </cc1:CascadingDropDown>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Issue / Request Sub Type
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlIssueRequestSubType" AutoPostBack="true" runat="server"
                                            CssClass="DropDownList" onchange="ShowMyDiv();" OnSelectedIndexChanged="ddlIssueRequestSubType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvIssueRequestSubType" runat="server" ControlToValidate="ddlIssueRequestSubType"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Select issue request sub type"
                                            ValidationGroup="CheckData">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceIssueRequestSubType" WarningIconImageUrl="../Images/Warning1.jpg"
                                            runat="server" TargetControlID="rfvIssueRequestSubType" CssClass="CustomValidator">
                                        </cc1:ValidatorCalloutExtender>
                                        <cc1:CascadingDropDown ID="ccdIssueRequestSubType" Category="IssueRequestSubType"
                                            TargetControlID="ddlIssueRequestSubType" PromptText="Select Issue Request Sub Type"
                                            ParentControlID="ddlIssueRequestType" LoadingText="Loading Text..." ServicePath="../WebServices/WebService.asmx"
                                            ServiceMethod="GetTypeofIssueRequestSubTypeRC" runat="server">
                                        </cc1:CascadingDropDown>
                                    </td>
                                    <td>                                     
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                       <tr>                    
                      <td class="rcd-FieldTitle" style="width: 30%">
                      
                      </td>
                      <td class="rcd-tableCell" style="width: 70%">
                          <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                            <contenttemplate>
                                                <asp:Label id="lblRemark" runat="server" SkinID="SkinDivLabel" ></asp:Label>  
                                                <asp:HiddenField id="hfRemark" runat="server"></asp:HiddenField> 
                                                <asp:LinkButton id="lnkFileTemplate"  ToolTip="Click to download the sample file" runat="server" SkinID="lnkSkin" OnClick="lnkFileTemplate_Click"></asp:LinkButton> 
                                                
                                            </contenttemplate>
                                            <triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestSubType" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>                                                                                                                          
                                                <asp:PostBackTrigger ControlID="lnkFileTemplate" />                                                
                                            </triggers>
                          </asp:UpdatePanel>
                      
                      
                      </td>                    
                    
                    </tr>    
                    
                    
                    
                    
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Remark
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtRemark" runat="server" MaxLength="1000" CssClass="textbox"
                                            TextMode="MultiLine">
                                        </asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtRemark"
                                            WatermarkText="Type Remark Here" WatermarkCssClass="textboxWaterMark" />
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvtxtRemark" runat="server" ControlToValidate="txtRemark"
                                            ErrorMessage="Enter remark" SetFocusOnError="true" ValidationGroup="CheckData"
                                            Display="None" />
                                        <cc1:ValidatorCalloutExtender ID="vcetxtRemark" WarningIconImageUrl="../Images/Warning1.jpg"
                                            TargetControlID="rfvtxtRemark" runat="server" CssClass="CustomValidator">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <%--<td>
                                        <asp:Panel ID="pnlMyPanel" runat="server">
                                        </asp:Panel>
                                        <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="ddlIssueRequestSubType"
                                            PopupControlID="pnlMyPanel" Position="Bottom">
                                        </cc1:PopupControlExtender>
                                    </td>--%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Contact Number
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <asp:TextBox ID="txtContactNumber" runat="server" MaxLength="11" onkeypress="NumericCharacter(this)"
                              CssClass="textbox"  ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvContactNumber" runat="server" ControlToValidate="txtContactNumber"
                                ErrorMessage="Enter contact number" SetFocusOnError="true" ValidationGroup="CheckData"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="vceContactNumber" CssClass="CustomValidator" WarningIconImageUrl="../Images/Warning1.jpg"
                                TargetControlID="rfvContactNumber" runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtContactNumber"
                                WatermarkText="Type Contact Number" WatermarkCssClass="textboxWaterMark" />
                        </td>
                    </tr>                    
                    
                    <tr id="trTicketValue" runat="server">
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Ticket value (Premium amount)
                        </td>
                       <td class="rcd-tableCell" style="width: 70%">
                        <nobr>
                          <asp:TextBox ID="txtTicketValue" runat="server" MaxLength="15" 
                          CssClass="textbox"></asp:TextBox>                            
                          <asp:RegularExpressionValidator ID="revTicketValue"  runat="server" ControlToValidate="txtTicketValue"
                           ErrorMessage="Enter number with two decimal places only"  SetFocusOnError="true" Display="None" ValidationGroup="CheckData" ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,18}(\.\d{1,2})?$" >
                          </asp:RegularExpressionValidator>   
                          <cc1:ValidatorCalloutExtender ID="vceTicketValue" runat="server" CssClass="CustomValidator"  WarningIconImageUrl="../Images/Warning1.jpg" TargetControlID="revTicketValue">
                          </cc1:ValidatorCalloutExtender>                                                                  
                          <cc1:TextBoxWatermarkExtender ID="TBWMTicketValue" runat="server" TargetControlID="txtTicketValue"
                           WatermarkText="Type Premium Amount" WatermarkCssClass="textboxWaterMark"></cc1:TextBoxWatermarkExtender>
                          
                           
                        </td> 
                                            
                    
                    </tr>                    
                    <tr>
                        <td class="rcd-FieldTitle" style="width: 30%">
                            Upload Screenshot
                        </td>
                        <td class="rcd-tableCell" style="width: 70%">
                            <asp:FileUpload ID="fuUpLoadFile" runat="server" SkinID="FileUploaderSkin" />
                         
                            <asp:RequiredFieldValidator ID="rfvUpload" runat="server" ControlToValidate="fuUpLoadFile"
                                Display="None" SetFocusOnError="true" ValidationGroup="CheckData" ErrorMessage="Please upload the file">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceUpload" WarningIconImageUrl="../Images/Warning1.jpg"
                                TargetControlID="rfvUpload" runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <br />
                            <font style="font-style: italic; color: Maroon; font-size: smaller;">Note : Please zip
                                files and attach if you want to attach more than one file.</font>
                        </td>
                    </tr> 
                    
                     <tr>
                     <td class="rcd-tableCell" style="width: 70%" colspan="2">
                      <asp:UpdatePanel ID="uplargemarket" runat="server" UpdateMode="Conditional">
                            <contenttemplate>
                      <table width="100%" border="0" cellpadding="1" cellspacing="1" runat="server" id="tbllargemarket" visible="false">
                            <tr>
                             <td class="rcd-FieldTitle" style="width: 30%">
                                Branch Name
                             </td>
                               <td class="rcd-tableCell" style="width: 70%">
                                        <asp:DropDownList ID="ddlbranchname" runat="server" CssClass="DropDownList" AutoPostBack="true" OnSelectedIndexChanged="ddlbranchnameselectedindexchanged"  >
                                        </asp:DropDownList>
                               </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvbranchname" runat="server" ControlToValidate="ddlbranchname"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Select Branch Name"
                                            ValidationGroup="CheckData" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vcebranchname" WarningIconImageUrl="../Images/Warning1.jpg"
                                            runat="server" TargetControlID="rfvbranchname" CssClass="CustomValidator">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                            </tr>
                            <tr>
                             <td class="rcd-FieldTitle" style="width: 30%">
                                Channel
                             </td>
                               <td class="rcd-tableCell" style="width: 70%">
                                        <asp:DropDownList ID="ddlchannel" runat="server" CssClass="DropDownList" AutoPostBack="true" OnSelectedIndexChanged="ddlchannelselectedindexchanged"  >
                                        </asp:DropDownList>
                               </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvchannel" runat="server" ControlToValidate="ddlchannel"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Select Channel"
                                            ValidationGroup="CheckData" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vcechannel" WarningIconImageUrl="../Images/Warning1.jpg"
                                            runat="server" TargetControlID="rfvchannel" CssClass="CustomValidator">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                            </tr>
                             <tr>
                             <td class="rcd-FieldTitle" style="width: 30%">
                                SM
                             </td>
                               <td class="rcd-tableCell" style="width: 70%">
                                        <asp:DropDownList ID="ddlsm" runat="server" CssClass="DropDownList" OnSelectedIndexChanged="ddlsmselectedindexchanged" AutoPostBack="true" >
                                        <asp:ListItem Text="--Select--" Value="--Select--" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                               </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvsm" runat="server" ControlToValidate="ddlsm"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Select SM"
                                            ValidationGroup="CheckData" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vcesm" WarningIconImageUrl="../Images/Warning1.jpg"
                                            runat="server" TargetControlID="rfvsm" CssClass="CustomValidator">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                            </tr>
                      </table>
                      
                      </contenttemplate>
                     <triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestSubType" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>                                              
                                                                  
                                   </triggers>
                      </asp:UpdatePanel>
                     </td>
                    </tr>                  
                    
                    <tr>
                     <td colspan="2" class="rcd-tableCell">
                     
                           <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                            <contenttemplate>
                                 
                                      <table id="tblPortalIDLocked" runat="server" visible="false" width="100%" cellspacing="0" cellpadding="0">
                                           <tr>
                                                     <td class="rcd-FieldTitle" style="width: 30%">
                                                         Please enter Portal ID
                                                     </td>
                                                     <td class="rcd-tableCell" style="width: 70%">                      
                                                     
                                                      
                                                                    <asp:TextBox ID="txtPortalID" runat="server" MaxLength="15" CssClass="textbox"></asp:TextBox>
                                                                    <cc1:TextBoxWatermarkExtender ID="TWEPortalID" runat="server" TargetControlID="txtPortalID"
                                                                        WatermarkText="Enter Portal ID Here" WatermarkCssClass="textboxWaterMark" />
                                                                    
                                                                        <asp:RequiredFieldValidator ID="rfvPortalID" runat="server" ControlToValidate="txtPortalID"
                                                                            ErrorMessage="Enter Portal ID" SetFocusOnError="true" ValidationGroup="CheckData"
                                                                            Display="None" />
                                                                        <cc1:ValidatorCalloutExtender ID="vcePortalID" WarningIconImageUrl="../Images/Warning1.jpg"
                                                                            TargetControlID="rfvPortalID" runat="server" CssClass="CustomValidator">
                                                                        </cc1:ValidatorCalloutExtender>                                                            
                                                
                                                
                                                     </td> 
                                           </tr>                          
                                      </table>
                                      
                                    </contenttemplate>
                                     <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestSubType" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>                                                                                                                          
                                                  
                                       </triggers>
                                    </asp:UpdatePanel>   
                            
                                             
                        </td>    
                      </tr>
                      
                     <tr>
                      <td colspan="2" class="rcd-tableCell">
                        
                          <asp:UpdatePanel ID="UpdatePanelRN" runat="server" UpdateMode="Conditional">
                            <contenttemplate>
                                 <table id="tblPolicyNoRN" runat="server" visible="false" width="100%" cellspacing="0" cellpadding="0">
                                   <tr>
                                       <td class="rcd-FieldTitle" style="width: 30%">
                                         Please enter Policy No (Renewal Notice)
                                       </td>
                                       <td class="rcd-tableCell" style="width: 70%">
                                           <asp:TextBox ID="txtPolicyNoRN" runat="server" MaxLength="16" CssClass="textbox"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TWPolicyNoRN" runat="server" TargetControlID="txtPolicyNoRN"
                                            WatermarkText="Enter Policy No Here" WatermarkCssClass="textboxWaterMark" />
                                        
                                            <asp:RequiredFieldValidator ID="rfvPolicyNoRN" runat="server" ControlToValidate="txtPolicyNoRN"
                                                ErrorMessage="Enter Policy No Here" SetFocusOnError="true" ValidationGroup="CheckData"
                                                Display="None" />
                                            <cc1:ValidatorCalloutExtender ID="vcePolicyNoRN" WarningIconImageUrl="../Images/Warning1.jpg"
                                                TargetControlID="rfvPolicyNoRN" runat="server" CssClass="CustomValidator">
                                            </cc1:ValidatorCalloutExtender>  
                                           
                                           
                                       </td>
                                   </tr>
                                 </table>
                            </contenttemplate>
                             <triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestSubType" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>                                                                                                                          
                                                  
                             </triggers>                            
                            </asp:UpdatePanel>                            
                        
                      </td>
                    </tr> 
                    
                    <tr>
                        <td colspan="2" class="rcd-tableCellCenterAlign">
                            <asp:Button ID="btnRegisterCall" ValidationGroup="CheckData" runat="server" Text="Register Your Call"
                                SkinID="buttonSkin" OnClick="btnRegisterCall_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" SkinID="buttonSkin" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                    <td>
                    <asp:UpdatePanel ID="paneldesc" runat="server" UpdateMode="Always">
                               <ContentTemplate> 
                                        <asp:Panel ID="pnlMyPanel" runat="server" Style="display: none" Height="170px" Width="400px" CssClass="modalPopup2" >
                                        <div style="color:White;font-size:14" class="rcd-TopHeaderBlue" >
                                         For Your Information.
                                        </div>
                                        <%--<hr class="hrLine" />--%>
                                         <table cellpadding="1" cellspacing="1" width="100%" class="rcd-Instruct" style="font-size:11">
                                         <tr>
                                         <td>
                                          <p><asp:Label ID="lbldesc" runat="server"></asp:Label></p><br />
                                           </td>
                                           </tr>
                                           <tr>
                                           <td>
                                           <asp:Label ID="Label1" runat="server" Text="Do you still want to log a call ?"></asp:Label>
                                           </td>
                                           </tr>
                                           <tr>
                                           <td>
                                            <asp:Label ID="lblcontrol" runat="server"></asp:Label>
                                            </td>
                                            </tr>
                                            </table>
                                            <center>
                                           <asp:Button ID="btnyes" runat="server" Text="Yes"
                                            SkinID="buttonSkin" />
                                           <asp:Button ID="btnno" OnClick="btnno_Click" runat="server" Text="No"
                                            SkinID="buttonSkin" />
                                            </center>
                                        </asp:Panel>
                                        <cc1:ModalPopupExtender ID="popupconfirm" runat="server" TargetControlID="lblcontrol"
                                            PopupControlID="pnlMyPanel" CancelControlID="btnyes" Enabled="true" DropShadow="false"
                                             BackgroundCssClass="modalBackground1" DynamicServicePath="">
                                        </cc1:ModalPopupExtender>
                                        </ContentTemplate>
                                        <triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlIssueRequestSubType" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>                                              
                                                                  
                                   </triggers>
                                        </asp:UpdatePanel>
                                    </td> 
                    </tr>
                </table>
    </div>
    </div>
    </form>

</body>
</html>


   
    

