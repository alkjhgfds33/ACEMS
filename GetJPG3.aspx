<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetJPG3.aspx.cs" Inherits="GetJPG3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Timer ID="Timer1" runat="server" Interval="1000" ontick="tmrRefresh_Tick"></asp:Timer>         
 
        <div>
        </div>
    </form>
</body>
</html>
