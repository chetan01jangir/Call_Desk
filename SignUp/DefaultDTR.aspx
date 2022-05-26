<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultDTR.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vechile Claim History</title>
<script type="text/javascript">
        
    function isSpclChar(str) {
        var iChars = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?";
        for (var i = 0; i < str.length; i++) 
        {
            if (iChars.indexOf(str.charAt(i)) != -1) 
            {
                alert("The Field " + str + " has special characters. \nSpecial Characters are not allowed");
                return true;
            }
        }
    }
    
    function validate() {     

    var regNo = document.getElementById('<%=TextBox1.ClientID%>').value;
    var chassisNo = document.getElementById('<%=TextBox2.ClientID%>').value;
    var engNo = document.getElementById('<%=TextBox3.ClientID%>').value;
    var policyNo = document.getElementById('<%=TextBox4.ClientID%>').value;
    if (regNo == null || regNo == "") {
        if ((chassisNo == null || chassisNo == "") && (engNo == null || engNo == "")) {
            if ((policyNo == null || policyNo == "")) {
                alert("Please enter a valid Vechile Registration Number");                
                return false;
            }
            else {
                alert("Please enter a valid Vechile Registration Number" + "\n OR " + "Combination of Engine and Chassis Number");
                return false;
            }
        }
    }
    if (isSpclChar(regNo) || isSpclChar(chassisNo) || isSpclChar(engNo) || isSpclChar(policyNo)) {
        return false;
    }

}
</script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            font-family: Arial;
            font-size: medium;
        }
        .rcd-Gridborder
{
	background-color: #CBCFD2;
}

.rcd-GridHead
{
	background-color: #1B67A8;
	height: 20px;
	font-family: Verdana, Arial, Tahoma, Helvetica, sans-serif;
	font-size: 11px;
	font-weight: bold;
	color: #ffffff;
	text-decoration: none;
	text-align: center;
	text-transform: uppercase;
	padding: 2px 2px 2px 2px;
}

.rcd-Grid
{
	background-color: #D8E8F6;
	height: 20px;
	font-family: Arial, Tahoma,Verdana, Helvetica, sans-serif;
	font-size: 11px;
	font-weight: bold;
	color: #333333;
	text-decoration: none;
	margin-top: 5px;
	margin-bottom: 15px;
	text-align: left;
	text-transform: uppercase;
}


.rcd-GridAlt
{
	background-color: #ffffff;
	height: 20px;
	font-family: Arial, Tahoma,Verdana, Helvetica, sans-serif;
	font-size: 11px;
	font-weight: bold;
	color: #333333;
	text-decoration: none;
	text-transform: uppercase;	
}

.rcd-GridText_left
{
	font-family: Arial, Tahoma,Verdana, Helvetica, sans-serif;
	font-size: 11px;
	font-weight: normal;
	color: #333333;
	text-decoration: none;
	text-align: left;
	padding-left: 3px;
	text-transform: uppercase;
}

.rcd-GridText_Right
{
	font-family: Arial, Tahoma,Verdana, Helvetica, sans-serif;
	font-size: 11px;
	font-weight: normal;
	color: #333333;
	text-decoration: none;
	text-align: Right;
	padding-right: 3px;
	text-transform: uppercase;
}

.rcd-GridText_Center
{
	font-family: Arial, Tahoma,Verdana, Helvetica, sans-serif;
	font-size: 12px;
	font-weight: normal;
	color: #333333;
	text-decoration: none;
	text-align: Center;	
}
.clear
{
clear:both;    
}
.rounded_corners
    {
        border: 1px solid #A1DCF2;
        -webkit-border-radius: 8px;
        -moz-border-radius: 8px;
        border-radius: 8px;
        overflow: hidden;
    }
    .rounded_corners td, .rounded_corners th
    {
        border: 1px solid #A1DCF2;
        font-family: Arial;
        font-size: 10pt;
        text-align: center;
    }
    .rounded_corners table table td
    {
        border-style: none;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>
                <b>POLICY CLAIM DETAILS AS PER IIB</b></p>
                <div style="width: 100%; border: 2px solid #1B67A8;">
                <div style="float:left;width:50%;">
                  <table style="width: 100%; height: 87px;">
                <tr>
                    <td style="width: 307px">
                        Registration Number</td>
                    <td>
                        <asp:TextBox ID="TextBox1" Text="" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 307px">
                        Chassis Number</td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 307px">
                        Engine Number</td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 307px">
                        Policy Number</td>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                </tr>                
                <tr>
                    <td style="width: 307px">
                        Insurer Name(Code)</td>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="display:none">
                    <td style="width: 307px">
                        Covernote Number</td>
                    <td>
                        <asp:TextBox ID="TextBox6" Visible="false" runat="server"></asp:TextBox></td>
                </tr>                
                <tr>
                    <td colspan="2" style="width: 307px">
                    <center>
                        <asp:ImageButton Height="30px" Width="90px" ImageUrl="~/Images/SubmitBtn.png" ID="Button1"
                            runat="server" OnClientClick="javascript:return validate()" OnClick="Button1_Click" Text="Submit" />
                        <asp:ImageButton Height="30px" Width="90px" ImageUrl="~/Images/CancelBtn.png" ID="Button2"
                            runat="server" Text="Cancel" /></center>
                            </td>                    

                </tr>            
            </table>
                </div>
                <div  style="float:left;width:35%;">
                <table width="100%">
<span style="font-size: 9pt; color: blue;">                
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Tool Tip For Search</b><br/>
<b>For Claim Details:</b><br/>
    1. Vehicle Registration Number only<br/>
    2. Engine Number and Chassis Number<br/>
    3. Vehicle Registration Number and Engine Number and Chassis Number<br/>

<b>For Policy Details:</b><br/>
    1. Vehicle Registration Number and Policy Number<br/>
    2. Engine Number and Chassis Number and Policy Number<br/>
    3. Vehicle Registration Number and Insurer Code<br/>
    4. Engine Number and Chassis Number and Insurer Code<br/>
<span>
</table>
                </div>
                <div class="clear"></div>
                </div>
          


            <table style="width: 100%;">
                <tr>
                    <td colspan="2">
                        <%--<asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                    <br />--%>
                        <br />
                        <asp:PlaceHolder ID="PHldr_ParamGrid" EnableViewState="false" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
