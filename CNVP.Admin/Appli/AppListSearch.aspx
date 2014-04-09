<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppListSearch.aspx.cs" Inherits="CNVP.Admin.Appli.AppListSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<script language="javascript" type="text/javascript" src="../lib/jquery/jquery-1.5.2.min.js"></script>
<script type="text/javascript" language="javascript" src="../lib/ligerUI/js/core/base.js"></script>
<script type="text/javascript" language="javascript" src="../lib/ligerUI/js/plugins/ligerDialog.js"></script>
<script type="text/javascript" language="javascript" src="../lib/ligerUI/js/plugins/ligerTip.js"></script>
<script type="text/javascript" language="javascript" src="../lib/ligerUI/js/plugins/ligerCheckBox.js"></script>
<script type="text/javascript" language="javascript" src="../lib/ligerUI/js/plugins/ligerComboBox.js"></script>
<script type="text/javascript" language="javascript" src="../lib/ligerUI/js/plugins/ligerResizable.js"></script>
<script language="javascript" type="text/javascript" src="../lib/ligerUI/js/plugins/ligerGrid.js"></script>
<script src="../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>

<link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="../lib/css/common.css" rel="stylesheet" type="text/css" /> 

    <script type="text/javascript" language="javascript" src="../common/calendar/js/jscal2.js"></script>
<script type="text/javascript" language="javascript" src="../common/calendar/js/lang/cn.js"></script>
<link type="text/css" href="../common/calendar/css/jscal2.css" rel="stylesheet"/>
<link type="text/css" href="../common/calendar/css/border-radius.css" rel="stylesheet"/>
<link type="text/css" href="../common/calendar/css/steel/steel.css" rel="stylesheet"/>

</head>
<body style="overflow:hidden; text-align:center">
    <script>
        $(function () {
            $("#txt4").ligerDateEditor();

        })
    </script>
<form id="form1" runat="server">
<table cellpadding="0" cellspacing="0" border="0" align="center" style="width:300px;margin:0px auto">
<tbody>
<tr>
<td colspan="3" height="15"></td>
</tr>
<tr>
<td height="30">档案类型：</td>
<td><div class="l-text l-text-combobox" style="width:220px;"><asp:DropDownList ID="DropParName" runat="server" CssClass="l-text-field" Width="220"></asp:DropDownList></div></td>
</tr>
<tr>
<td height="30">开始时间：</td>
<td align="left">
    <asp:TextBox ID="StartTime" runat="server" CssClass="cms_input180px" MaxLength="50"></asp:TextBox> <span style="color:#CCCCCC">xxxx-xx-xx</span>

</td>
</tr>
<tr>
<td height="30">结束时间：</td>
<td align="left">
    <asp:TextBox ID="EndTime" runat="server" CssClass="cms_input180px" MaxLength="50"></asp:TextBox> <span style="color:#CCCCCC">xxxx-xx-xx</span>
</td>
</tr>
<tr>
<td height="30">审核状态：</td>
<td align="left"><input type="radio" name="IsAudit" value="1" />已审核 <input type="radio" name="IsAudit" value="0" />未审核</td>
</tr>
<tr>
<td height="30">结果需求：</td>
<td align="left"><input type="radio" name="IsRslt" value="0" />邮箱<input type="radio" name="IsRslt" value="1" />快递</td>
</tr>
<tr>
<td height="30">审核人：</td>
<td align="left"><div class="l-text l-text-combobox" style="width:220px;"><asp:DropDownList ID="DropDownUser" runat="server" CssClass="l-text-field" Width="220"></asp:DropDownList></div></td>
</tr>
<tr>
<td></td>
<td style="text-align:left;height:45px"> <input type="button" value="查 找" class="button3" onclick="doSearch();"/>  <input type="button" value="取 消" class="button3" onclick="    top.removeTB();"/></td>
</tr>
</tbody>
</table>
<script type="text/javascript">//<![CDATA[
    Calendar.setup({
        weekNumbers: true,
        inputField: "StartTime",
        trigger: "StartTime",
        onSelect: function () { this.hide() },
        showTime: 24,
        dateFormat: "%Y-%m-%d"
    });
    Calendar.setup({
        weekNumbers: true,
        inputField: "EndTime",
        trigger: "EndTime",
        onSelect: function () { this.hide() },
        showTime: 24,
        dateFormat: "%Y-%m-%d"
    });
    //]]></script>
    <script type="text/javascript">
        function doSearch() {
            console.log(window.parent.frames['List']);
            var TypeID = $("#Type").val();
            var StartTime = $("#StartTime").val();
            var EndTime = $("#EndTime").val();
            var IsAudit = $("input[type='radio'][name='IsAudit']:checked").val();
            var Rslt = $("input[type='radio'][name='IsRslt']:checked").val();
            var AuditMan = $("#DropDownUser").val();
            window.parent.frames['Applist'].f_onLoadProduct1(TypeID, StartTime, EndTime, IsAudit, Rslt, AuditMan);
        top.removeTB();
    }
</script>
</form>
</body>
</html>
