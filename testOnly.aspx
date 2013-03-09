<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testOnly.aspx.cs" Inherits="EbalitWebForms.testOnly" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </div>
        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="542" CommandName="pop">LinkButton</asp:LinkButton>
    </form>
</body>
</html>
