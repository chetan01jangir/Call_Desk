<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login"
    MasterPageFile="~/Masters/LoginMasterPage.master" Theme="SkinFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server">
        <script language="javascript" type="text/javascript">
            var xmlHttp;
            function srvTime() {
                try {
                    //FF, Opera, Safari, Chrome
                    xmlHttp = new XMLHttpRequest();
                }
                catch (err1) {
                    //IE
                    try {
                        xmlHttp = new ActiveXObject('Msxml2.XMLHTTP');
                    }
                    catch (err2) {
                        try {
                            xmlHttp = new ActiveXObject('Microsoft.XMLHTTP');
                        }
                        catch (eerr3) {
                            //AJAX not supported, use CPU time.
                            alert("AJAX not supported");
                        }
                    }
                }
                xmlHttp.open('HEAD', window.location.href.toString(), false);
                xmlHttp.setRequestHeader("Content-Type", "text/html");
                xmlHttp.send('');
                return xmlHttp.getResponseHeader("Date");
            }

            function CheckUserName() {
                var st = srvTime();
                var today = new Date(st);
                var str = today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate() + "  " + today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
                document.getElementById("ctl00_ContentPlaceHolder1_abc").value = str;
                if (document.getElementById("ctl00_ContentPlaceHolder1_lgLogin_UserName").value == "") {
                    document.getElementById("ctl00_ContentPlaceHolder1_lgLogin_UserName").focus();
                    alert("Enter the Username");
                    return false;
                }
                else {
                    return true;
                }
            }

            function VisibleFalse() {
                //document.getElementById("ctl00_ContentPlaceHolder1_btnOk").disabled = true;
                //alert('hi');
                document.getElementById("ctl00_ContentPlaceHolder1_btnOk").style.display = "none";
                //__doPostBack('ctl00_ContentPlaceHolder1_btnOk','OnClick'); 
                return true;
            }

            function OpenPopUp() {
                var w = window.open('CreateIMDPOSUser.aspx?', 'PopUpWindowName', 'width=700,left=150,top=100,height=600,titlebar=no, menubar=no, resizable=yes, scrollbars = yes')
                w.focus();

            }
        </script>
        <div>
            <%--<marquee style="color:White;">
	<font size="5">
<b>Take Away (Jul'2016)  : In Smart Zone Commission Statement make live for IRDA agent.</b>
</font>

</marquee>--%>
            <div id="divDispayMsg" runat="server" style="width: 100%; color: White; font-size: 16px;
                background-color: #0094c0; height: 20px; padding-top: 3px">
                <marquee onmouseover="this.setAttribute('scrollamount', 0, 0);" onmouseout="this.setAttribute('scrollamount', 2, 0);"
                    direction="left" scrollamount="4" style="color: White; font-style: normal; font-weight: bold;"> 
                <asp:Label ID="lblDisplayMsg" runat="server" Text=""></asp:Label></marquee>
            </div>
            <table style="width: 100%; height: 575px;" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <td id="tdMsg" runat="server" style="color: White; font-style: normal; font-weight: bold;">
                        <%--  Please note that Calldesk will not be available on 25-Aug-2014 from 8:00 PM to 10:00 PM for system maintainence activity.<br /> So plan accordingly.--%>
                        Dear All , due to some structural changes any call getting logged in call desk FOR
                        MOM application - New Mapping , Remapping , De-mapping from 29th March 3 pm to 1st
                        April 2018 eod, will not be considered by IT for action. This information is already
                        send on pan india by strategy team .
                    </td>
                </tr>
                <tr id="trMarquee" runat="server">
                    <td>
                        <marquee onmouseover="this.setAttribute('scrollamount', 0, 0);" onmouseout="this.setAttribute('scrollamount', 2, 0);"
                            behavior="alternate" direction="left" scrollamount="2" style="color: White; font-style: normal;
                            font-weight: bold;">
               Reliance welcomes all the Agents to access Call desk and log a call for any query in POS and PORTAL. <br /> 
               Click          
              <asp:LinkButton ID="lnkManualDownLoad" runat="server" OnClick="lnkManualDownLoad_Click"
              style="color:Red;font-style:normal;font-weight:bold;" Font-Underline="false">Here</asp:LinkButton>
              to download the Process Document to access Call desk.
              </marquee>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label runat="server" ID="lblMessage" Style="color: AntiqueWhite;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<img src="Images/CalldeskMobileApproval3.jpg" style="border-style:none" />--%>
                        <%-- <br />
            <span style="font-size:28px; font-weight:bold; display:block; color:#ffb432">IRPAS and CORPORATE PORTAL</span>
            <span style="color:White"><b>"For IRPAS and CORPORATE PORTAL"</b> related issues please register ticket under Application type <b>"IRPAS"</b></span>--%>
                    </td>
                    <td align="center" valign="middle">
                        <div style="color: White; width: 500px">
                            <p style="font-size: 20px">
                                <marquee onmouseover="this.setAttribute('scrollamount', 0, 0);" onmouseout="this.setAttribute('scrollamount', 2, 0);"
                                    direction="left" scrollamount="4" style="color: White; font-style: normal; font-weight: bold;">            
            <%--<asp:Label ID="lblDisplayMsg" runat="server" Text=""></asp:Label>--%></marquee>
                            </p>
                        </div>
                        <table width="540" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img src="Images/toppanel.gif" align="absmiddle" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" class="ricm-LoginBkg" style="background: url(images/loginpixel.gif);
                                    background-repeat: repeat-x; height: 120px; border-right: #3B83CF 1px solid;
                                    border-left: #3B83CF 1px solid;">
                                    <asp:HiddenField ID="abc" Value="12321" runat="server" />
                                    <asp:Login TextBoxStyle-BorderStyle="solid" OnAuthenticate="OnAuthenticate" TextBoxStyle-BorderWidth="1px"
                                        ID="lgLogin" runat="server" BackColor="Transparent" BorderColor="#E6E2D8" BorderPadding="4"
                                        BorderStyle="Solid" BorderWidth="0px" DisplayRememberMe="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="#333333" DestinationPageUrl="Default.aspx" Height="90px"
                                        Width="195px" OnLoginError="lgLogin_LoginError">
                                        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
                                        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                                        <TextBoxStyle Font-Size="Larger" BorderStyle="Solid" BorderWidth="1px" />
                                        <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
                                        <LabelStyle Wrap="False" />
                                        <LayoutTemplate>
                                            <table border="0" cellpadding="4" cellspacing="0" style="border-collapse: collapse">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" style="width: 250px; height: 105px">
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <asp:Label ID="Label1" runat="server" AssociatedControlID="Label1" Font-Bold="true"
                                                                                ForeColor="blue">Note: Use your Windows User Id/Password</asp:Label>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="white-space: nowrap; height: 40px;">
                                                                                <asp:HiddenField ID="hdnPopUpTest" runat="server" />
                                                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="ricm-Loginlabel">User Name</asp:Label>
                                                                            </td>
                                                                            <td style="height: 40px">
                                                                                <asp:TextBox ID="UserName" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Size="Larger"
                                                                                    CssClass="ricm-txtboxvalue1" Width="150px">
                                                                                </asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                                                    ErrorMessage="User Name is required." Display="None" ToolTip="User Name is required."
                                                                                    ValidationGroup="lgLogin">*
                                                                                </asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="white-space: nowrap; height: 41px;">
                                                                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="ricm-Loginlabel">Password</asp:Label>
                                                                            </td>
                                                                            <td style="height: 41px">
                                                                                <input type="password" id="prevent_autofill" style="display: none;" />
                                                                                <asp:TextBox ID="Password" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Size="Larger"
                                                                                    CssClass="ricm-txtboxvalue1" TextMode="Password" Width="150px">
                                                                                </asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                                                    ErrorMessage="Password is required." ToolTip="Password is required." Display="None"
                                                                                    ValidationGroup="lgLogin">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Image ID="Image1" runat="server" CssClass="ricm-txtboxvalue1" BorderStyle="Solid"
                                                                                    BorderWidth="1.1px" Width="151px" Style="display: flex; margin-left: 0px" />
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;<asp:ImageButton ID="imgRefresh" runat="server" AlternateText="Refresh" ImageAlign="right"
                                                                                    ImageUrl="~/Images/ChangeVertical.jpg" ToolTip="Refresh captcha image." OnClick="ImageButton_Click"
                                                                                    Height="20" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="white-space: nowrap">
                                                                                <asp:Label ID="lblCapchaCode" runat="server" CssClass="ricm-Loginlabel">Image Code&nbsp;&nbsp;</asp:Label>
                                                                            </td>
                                                                            <td colspan="2" style="padding-top: 6px;">
                                                                                <asp:TextBox ID="txtimgcode" autocomplete="OFF" runat="server" BorderStyle="Solid"
                                                                                    BorderWidth="1px" Font-Size="Larger" CssClass="ricm-txtboxvalue1" Width="150px"
                                                                                    MaxLength="10"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvimagecode" runat="server" ControlToValidate="txtimgcode"
                                                                                    ErrorMessage="Capcha code is required." ToolTip="Capcha code is required." ValidationGroup="lgLogin">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2" style="color: red;">
                                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="2">
                                                                    <asp:Button ID="LoginButton" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC"
                                                                        BorderStyle="Solid" BorderWidth="1px" CommandName="Login" Font-Names="Verdana"
                                                                        Font-Size="0.8em" ForeColor="#284775" Text="Log In" ValidationGroup="lgLogin"
                                                                        OnClick="LoginButton_Click" />
                                                                    <asp:LinkButton ID="lnkForgotPwd" Text="Forgot Password?" OnClientClick="javascript:return CheckUserName();"
                                                                        runat="server" OnClick="lnkForgotPwd_Click">                                                                
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkSignUp" runat="server" CssClass="tooltip" OnClick="lnkSignUp_Click">
                                                                SignUp
                                                                <span>POS/Portal-IMD Users Please Register here.</span>
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                    </asp:Login>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%-- <a href="../TestPopup.aspx" style="color:White"> SignUp</a>--%>
                                    <%-- <a href="SignUp/CreateIMDPOSUser.aspx" style="color:White" >SignUp</a>    --%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img src="images/Bottom_Banner.jpg" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divConfirm" runat="server" class="ConfirmDIVBG" style="top: 125px; left: 204px;
                                        text-align: center; width: 575px; height: 316px; z-index: 1000; position: absolute;
                                        display: none;">
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblEmailMessage" runat="server" />
                                                    <asp:Label ID="lblUserEmail" Font-Bold="true" ForeColor="red" Font-Underline="true"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="50%">
                                                    <asp:Button ID="btnOk" SkinID="buttonSkin" runat="server" OnClientClick="javascript:return VisibleFalse();"
                                                        Text="OK" OnClick="btnOk_Click" />
                                                </td>
                                                <td align="left" width="50%">
                                                    <asp:Button ID="btnCancel" SkinID="buttonSkin" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
