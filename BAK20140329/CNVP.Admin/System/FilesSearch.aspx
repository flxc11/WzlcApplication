<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FilesSearch.aspx.cs" Inherits="CNVP.Admin.FilesSearch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<script language="javascript" type="text/javascript" src="../lib/jquery/jquery-1.5.2.min.js"></script>
<script language="javascript" type="text/javascript" src="../lib/ligerUI/js/plugins/ligerGrid.js"></script>
<link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="../lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css" />
<link href="../lib/css/common.css" rel="stylesheet" type="text/css" /> 
</head>
<body style="overflow:hidden; text-align:center">
<form id="form1" runat="server">
<table cellpadding="0" cellspacing="0" border="0" align="center" style="width:300px;margin:0px auto">
<tbody>
<tr>
<td colspan="3" height="15"></td>
</tr>
<tr>
<td style="width:90px" height="30">资源类型：</td>
<td style="width:210px;text-align:left">
<asp:DropDownList ID="DropFilesType" runat="server" Width="180px">
<asp:ListItem Text="所有资源" Value=""></asp:ListItem>
<asp:ListItem Text="产品图片" Value="1"></asp:ListItem>
<asp:ListItem Text="颜色图片" Value="2"></asp:ListItem>
<asp:ListItem Text="款式图片" Value="3"></asp:ListItem>
<asp:ListItem Text="套餐图片" Value="4"></asp:ListItem>
</asp:DropDownList></td>
</tr>
<tr>
<td height="30">查找方式：</td>
<td style="text-align:left">
<asp:DropDownList ID="DropSearchType" runat="server" Width="180px">
<asp:ListItem Text="方式不限" Value=""></asp:ListItem>
<asp:ListItem Text="精品名称" Value="FullName"></asp:ListItem>
<asp:ListItem Text="文件名称" Value="FileName"></asp:ListItem>
</asp:DropDownList></td>
</tr>
<tr>
<td height="30">关 键 词：</td>
<td ><div class="l-text" style="width:180px;"><asp:TextBox ID="TxtKeyword" runat="server" CssClass="l-text-field" Width="175"></asp:TextBox></div></td>
</tr>
<tr>
<td></td>
<td style="text-align:left;height:45px"> <input type="button" value="确 定" class="button3" onclick="doSearch();"/> <input type="button" value="取 消" class="button3" onclick="top.removeTB();"/></td>
</tr>
</tbody>
</table>
<script language="javascript" type="text/javascript">
function doSearch() {
    var FilesType = $("#DropFilesType").val();
    var SearchType = $("#DropSearchType").val();
    var Keyword = $("#TxtKeyword").val();
    window.parent.frames['Files'].f_GridLoad(FilesType,SearchType,Keyword);
    top.removeTB();
}
</script>
</form>
</body>
</html>