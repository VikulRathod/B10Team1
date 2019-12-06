<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LockedAccounts.aspx.cs" Inherits="_1_Authentication.LockedAccounts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: Arial">
            <asp:GridView ID="gvLockedAccounts" runat="server" AutoGenerateColumns="False" OnRowCommand="gvLockedAccounts_RowCommand">
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="User Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="LockedDateTime"
                        HeaderText="Locked Date &amp; Time" />
                    <asp:BoundField DataField="HoursElapsed" HeaderText="Hours Elapsed">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Enable">
                        <ItemTemplate>
                            <asp:Button ID="btnEnable" runat="server" CommandArgument='<%# Eval("UserName") %>'
                                Text="Enable" Enabled='<%#Convert.ToInt32(Eval("HoursElapsed")) > 24%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
