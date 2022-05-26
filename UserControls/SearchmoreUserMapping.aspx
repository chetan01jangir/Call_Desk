<%@ Page Language="C#" AutoEventWireup="true" Theme="SkinFile" CodeFile="SearchmoreUserMapping.aspx.cs"
    Inherits="UserControls_SearchmoreUserMapping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../App_Themes/SkinFile/SkinFile.skin" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function funSetCode(varControl,varData)
        {
            window.opener.document.getElementById(varControl).value=varData;
            window.opener.document.getElementById(varControl).focus();
            window.close();
        }
    </script>

    <title>::Call Desk - Find User ::</title>
</head>
<body style="margin-top: 0; margin-bottom: 0; margin-left: 0; margin-right: 0; background-image: url(../Images/BackGroundImage.JPG);">
    <form id="form1" runat="server">
        <div>
            <table border="0" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="ricm-CellSpace" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="ricm-TopHeaderRed">
                                </td>
                                <td class="ricm-TopHeaderBlue">
                                    Search Criteria
                                </td>
                                <td style="width: 6%" class="ricm-TopHeaderRight">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="3" class="ricm-CellSpace">
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" valign="top" style="background-color: White;" colspan="3">
                                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="height: 40px">
                                                <table border="0" cellpadding="1" cellspacing="0" width="100%" align="left" id="tblOne">
                                                    <tr>
                                                        <td style="height: 40px">
                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddlCriteria" runat="server" SkinID="dropdownSkin">
                                                                            <asp:ListItem Value="1">Code (=)</asp:ListItem>
                                                                            <asp:ListItem Selected="True" Value="2">Name (Like)</asp:ListItem>
                                                                        </asp:DropDownList>&nbsp;
                                                                        <asp:TextBox ID="txtValue" runat="server" SkinID="textboxSkin"></asp:TextBox>
                                                                        <asp:Button ID="cmdSearch" runat="server" Text="Search" SkinID="buttonSkin" OnClick="cmdSearch_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 0px; height: 16px;" colspan="3">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="tr1">
                    <td colspan="2">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="ricm-CellSpace">
                    </td>
                </tr>
                <tr runat="server" id="tr2">
                    <td colspan="2">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 29;">
                                </td>
                                <td width="100%" valign="top" style="background-color: White;">
                                    <table border="0" width="100%" cellpadding="0" cellspacing="0" align="center">
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="True" SkinID="labelSkin"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="gvData" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="False"
                                                    AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging" OnRowDataBound="gvData_RowDataBound"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Code">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkCode" runat="server"><%#Eval("Code")%></asp:LinkButton>
                                                                <asp:Label ID="lblCode" runat="server" Visible="false" Text='<%#Eval("Code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbDesignation" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Branch">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbBranchName" runat="server" Text='<%#Eval("BranchName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 400px;">
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td style="border: 0px;" colspan="3">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <!--&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; -->
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
