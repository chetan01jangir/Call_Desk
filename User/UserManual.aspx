<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManual.aspx.cs" Inherits="User_UserManual" MasterPageFile="~/Masters/MasterPage.master"  
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
                   <tr>
                         <td class="rcd-FieldTitle" width="98%">                                                
                             
                             To download Do's and Don'ts for details on calldesk application  Click          
                      <asp:LinkButton ID="lnkdos" runat="server" 
                      style="color:Red;font-style:normal;font-weight:bold;" Font-Underline="false" OnClick="lnkdos_Click">Here</asp:LinkButton>
                   .                                
                         </td>
                   </tr>
                   <tr>
                         <td class="rcd-FieldTitle" width="98%">                                                
                             
                             To download Smart Zone -Traning module Reset or Forgot Password  Click 
                              <asp:LinkButton ID="lnkSmartPassword" runat="server" 
                      style="color:Red;font-style:normal;font-weight:bold;" Font-Underline="false" OnClick="lnkSmartPassword_Click">Here</asp:LinkButton>
                   .                                
                         </td>
                   </tr>
                    <tr>
                         <td class="rcd-FieldTitle" width="98%">                                                
                             
                             To download Smart zone -Traning module Agent & HNIN Active & Inactive status  Click
                             <asp:LinkButton ID="lnkSmartAgentHNIN" runat="server" 
                      style="color:Red;font-style:normal;font-weight:bold;" Font-Underline="false" OnClick="lnkSmartAgentHNIN_Click">Here</asp:LinkButton>
                   .                                
                         </td>
                   </tr>
                   <tr>
                         <td class="rcd-FieldTitle" width="98%">                                                
                             
                             To download Smart Zone - Traning Module for Sub User ID Creation  Click
                             <asp:LinkButton ID="lnkSmartUserIdCreation" runat="server" 
                      style="color:Red;font-style:normal;font-weight:bold;" Font-Underline="false" OnClick="lnkSmartUserIdCreation_Click">Here</asp:LinkButton>
                   .                                
                         </td>
                   </tr>
                   <tr>
                         <td class="rcd-FieldTitle" width="98%">                                                
                             
                             To download Smart Zone - Traning module for Profile Updation for Email & Mobile number  Click
                              <asp:LinkButton ID="lnkSmartProfileUpdation" runat="server" 
                      style="color:Red;font-style:normal;font-weight:bold;" Font-Underline="false" OnClick="lnkSmartProfileUpdation_Click">Here</asp:LinkButton>
                   .                                
                         </td>
                   </tr>

                   <tr>
                         <td class="rcd-FieldTitle" width="98%">                                                
                             
                             To download Smart Zone & MOM  -Traning module Branch Mapping for SM-Vendor in MOM & Smart Zone  Click
                             <asp:LinkButton ID="lnkSmartBranchMapping" runat="server" 
                      style="color:Red;font-style:normal;font-weight:bold;" Font-Underline="false" OnClick="lnkSmartBranchMapping_Click">Here</asp:LinkButton>
                   .                                
                         </td>
                   </tr>
                   <tr>
                         <td class="rcd-FieldTitle" width="98%">
                             To download Smart Zone -Traning module for Smart Zone ID showing locked   Click
                             <asp:LinkButton ID="lnkSmartIDLocked" runat="server" 
                      style="color:Red;font-style:normal;font-weight:bold;" Font-Underline="false" OnClick="lnkSmartIDLocked_Click">Here</asp:LinkButton>
                   .                                
                         </td>
                   </tr>
               </table>         
           
           
          </td>       
       </tr>

</table>


</asp:Content>