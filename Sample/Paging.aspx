<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Paging.aspx.cs" Inherits="Sample.Paging" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ページングサンプル</title>
    <link rel="stylesheet" type="text/css" href="css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="title">
        ページングサンプル
    </div>
<br />
<table id="table-01">
<tr>
<th>Type</th>
<td>
    <asp:DropDownList ID="ddlType" runat="server" Height="25px" Width="200px" 
        AutoPostBack="True" CssClass="base" 
        onselectedindexchanged="ddlType_SelectedIndexChanged">
        <asp:ListItem>絞込みなし</asp:ListItem>
        <asp:ListItem>A</asp:ListItem>
        <asp:ListItem>B</asp:ListItem>
        <asp:ListItem>C</asp:ListItem>
        <asp:ListItem>D</asp:ListItem>
        <asp:ListItem>E</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr>
<th>ID</th>
<td>
    <asp:TextBox ID="txtIDFrom" runat="server" Width="150px" 
        AutoPostBack="True" CssClass="base" ontextchanged="txtIDFrom_TextChanged"></asp:TextBox>
    <asp:Label ID="Label4" runat="server" Text=" ～ " CssClass="base" 
        Font-Bold="False"></asp:Label>
    <asp:TextBox ID="txtIDTo" runat="server" Width="150px" AutoPostBack="True" 
        CssClass="base" ontextchanged="txtIDTo_TextChanged" ></asp:TextBox>
    <asp:Label ID="labErrID" runat="server" ForeColor="Red"></asp:Label>
</td>
</tr>
</table>
<br />
<asp:Label ID="labCount" runat="server" CssClass="base"></asp:Label>
    <br />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" CellPadding="5" CssClass="GridViewBase" 
        DataSourceID="CustomerDataSource" EnableModelValidation="True" PageSize="15">
        <Columns>
            <asp:BoundField DataField="RowNo" HeaderText="列番号" />
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name">
            <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="Type" HeaderText="Type" />
            <asp:BoundField DataField="Description" HeaderText="Description">
            <ItemStyle Width="300px" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle CssClass="GridViewHeader" />
        <PagerStyle CssClass="GridViewPager" />
    </asp:GridView>
    <asp:ObjectDataSource ID="CustomerDataSource" runat="server" 
        EnablePaging="True" SelectCountMethod="GetCount" SelectMethod="GetCustomer" 
        TypeName="Sample.Paging">
        <SelectParameters>
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
<br />


    </form>
</body>
</html>
