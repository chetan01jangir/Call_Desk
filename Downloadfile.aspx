<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Downloadfile.aspx.cs"  MasterPageFile="~/Masters/MasterPage.master" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    
   <link href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
<script src="//netdna.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
<script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
<!------ Include the above in your HEAD tag ---------->

<link href="//netdna.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css" rel="stylesheet">
<div class="container">
	<div class="row">
	<div class="alert alert-danger alert-dismissible" role="alert">
  <button type="button" onclick="this.parentNode.parentNode.removeChild(this.parentNode);" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
  <strong> <marquee><p style="font-family: Impact; font-size: 18pt">Download Zip Files by Entering Ticket No</p></marquee>
</div>
	</div>
</div>

<%--<left>--%>
<h1>
DOWNLOAD FILES
</h1>
<div>
       <asp:Label runat="server" CssClass="center-block">ENTER TICKET NO:-</asp:Label>
    
        <asp:TextBox ID="TextBox1" runat="server" Height="32px" Width="400px" CssClass="center-block"
            style="margin-bottom: 0px"></asp:TextBox>
            <br />
        <asp:Button ID="Button1" runat="server" Text="Download" CssClass="center-block"
           Height="32px" Width="400px" onclick="Button1_Click"  />
        <br />
        </div>
        <br />
        <br />

        <div runat="server" id="theDiv">
          
       <asp:Label runat="server" CssClass="center-block">UPLOAD EXCEL FOR TICKETS NO:-</asp:Label>
         
        <asp:FileUpload ID="fuUpLoadFile" runat="server" Height="32px" Width="400px" 
            style="margin-bottom: 0px; text-align: center;" CssClass="center-block" />
                <font style="font-style: italic; color: Maroon; font-size: smaller;" CssClass="center-block">Note :FILES 
            ARE STORED IN C:\Files\Today's DATE.</font>
               
       <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click"  Text="Upload" Height="32px" Width="400px"  CssClass="center-block"  />
            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Download Excel Format</asp:LinkButton>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
            <br />
       <br />
       <br />
       <br />
        </div>
       
        <br />
       
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

        <br />
    
  
    </asp:Content>

