<%@ Page Language="C#" ValidateRequest="false" %>
<html>
 <script runat="server">
  void btnSubmit_Click(Object sender, EventArgs e)
  {
    // If ValidateRequest is false, then 'hello' is displayed
    // If ValidateRequest is true, then ASP.NET returns an exception
    Response.Write(txtString.Text);
  }
 </script>
 <body>
  <form id="form1" runat="server">
    <asp:TextBox id="txtString" runat="server" 
                 Text="<script>alert('hello');</script>" />
    <asp:Button id="btnSubmit" runat="server"   
                OnClick="btnSubmit_Click" 
                Text="Submit" />
  </form>
 </body>
</html>