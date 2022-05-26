<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadFiles.aspx.cs" Inherits="UploadFiles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <table >
      <tr>
        <td>
         <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </td>      
      </tr>    
      <tr>
        <td>
           <asp:FileUpload ID="fuUpLoadFile" runat="server" />
        </td>
        <td>
         <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />        
        </td>
      </tr>
      
      </table>
    
    
    
    </div>
    </form>
</body>
</html>
