<%@ Page Language="C#" MasterPageFile="~/Masters/MasterPage.master" Theme="SkinFile"
    AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage_ErrorPage"
    Title=":: Call Desk - Error Page ::" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" cellpadding="1" cellspacing="1" class="rcd-TableBorder">
        <tr>
            <td >
                <asp:Label ID="lblMessage" runat="server" SkinID="SkinLabel" Text="Error has occurred please contact the administrator." ></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
