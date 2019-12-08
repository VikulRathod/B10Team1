<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="Store.Presentation.Welcome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome Page</h1>
            <asp:Button ID="Button1" runat="server" Text="Change Password" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
