<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminInfo.aspx.cs" Inherits="CNVP.Admin.AdminInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<script language="javascript" type="text/javascript" src="../lib/jquery/jquery-1.5.2.min.js"></script>
<script language="javascript" type="text/javascript" src="../lib/ligerUI/js/plugins/ligerGrid.js"></script>
<link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="../lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css" />
<link href="../lib/css/common.css" rel="stylesheet" type="text/css" /> 
</head>
<body style="overflow:hidden; text-align:center">
<form id="form1" runat="server" onsubmit="return CheckForm();">
<table cellpadding="0" cellspacing="0" border="0" align="center" style="width:300px;margin:0px auto">
<tbody>
<tr>
<td colspan="3" height="15"></td>
</tr>
<tr>
<td style="width:70px" height="30">登录帐号：</td>
<td style="width:230px"><div class="l-text" style="width:220px;"><asp:TextBox ID="TxtUserName" runat="server" CssClass="l-text-field" Width="200" Enabled="false" Text=""></asp:TextBox></div></td>
</tr>
<tr>
<td height="30">登录密码：</td>
<td><div class="l-text" style="width:220px;"><asp:TextBox ID="TxtUserPass" runat="server" CssClass="l-text-field" Width="210" MaxLength="20" TextMode="Password"></asp:TextBox></div></td>
</tr>
<tr>
<td height="30">新的密码：</td>
<td ><div class="l-text" style="width:220px;"><asp:TextBox ID="TxtNewPass" runat="server" CssClass="l-text-field" Width="210" MaxLength="20" TextMode="Password"></asp:TextBox></div></td>
</tr>
<tr>
<td height="30">确认密码：</td>
<td ><div class="l-text" style="width:220px;"><asp:TextBox ID="TxtNewPass1" runat="server" CssClass="l-text-field" Width="210" MaxLength="20" TextMode="Password"></asp:TextBox></div></td>
</tr>
<tr>
<td></td>
<td style="text-align:left;height:45px"><asp:Button ID="Button1" runat="server" Text="保 存" Width="65px" CssClass="button3" OnClick="Button1_Click"/> <input type="button" value="取 消" class="button3" onclick="top.removeTB();"/></td>
</tr>
</tbody>
</table>
<script language="javascript" type="text/javascript">
    function CheckForm() {
        if ($("#TxtUserPass").val() == "") {
            alert("您的登录密码不能为空。");
            $("#TxtUserPass").focus();
            return false;
        }
        if ($("#TxtNewPass").val() == "") {
            alert("新的登录密码不能为空。");
            $("#TxtNewPass").focus();
            return false;
        }
        if ($("#TxtNewPass1").val() == "") {
            alert("确认密码不能为空。");
            $("#TxtNewPass1").focus();
            return false;
        }
        if ($("#TxtNewPass1").val() != $("#TxtNewPass").val()) {
            alert("两次密码输入不一致。");
            $("#TxtNewPass1").focus();
            return false;
        }
        return true;
    }
</script>
</form>
</body>
</html>