<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManual.aspx.cs" Inherits="User_UserManual" MasterPageFile="~/IMD/AgentMasterPage.master"  
Theme="SkinFile" Title=":: Call Desk - User Manual ::" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<table width="98%" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <td colspan="2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        
                        <td class="rcd-TopHeaderBlue">
                            User Manual
                        </td>
                       
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
       <tr>
           <td>
               <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
                   <tr>
                         <td class="rcd-FieldTitle" width="98%">                                                
                             
                             To download user manual for details on calldesk application  Click          
                      <asp:LinkButton ID="lnkManualDownLoad" runat="server" OnClick="lnkManualDownLoad_Click"
                      style="color:Red;font-style:normal;font-weight:bold;" Font-Underline="false">Here</asp:LinkButton>
                   .                                
                         </td>
                   </tr>
               </table>
           
           
           
          </td>       
       </tr>

</table>


</asp:Content>