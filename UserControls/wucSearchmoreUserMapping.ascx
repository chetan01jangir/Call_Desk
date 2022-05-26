<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucSearchmoreUserMapping.ascx.cs" Inherits="UserControls_wucSearchmoreUserMapping" %>

<script language="javascript" type="text/javascript">
function funOpenSearch(textName,searchOption)
{
    window.open('../UserControls/SearchmoreUserMapping.aspx?TextboxName='+ textName + '&SearchType=' +  searchOption,textName,'height=445,width=400,status=no,toolbar=no,menubar=no,location=no,scrollbars=yes')
    return false
}
</script>
<asp:ImageButton ID="ImageButton1" CausesValidation="false" runat="server" ImageUrl="../Images/SearchICO.jpg" height="16" width="16"/>
