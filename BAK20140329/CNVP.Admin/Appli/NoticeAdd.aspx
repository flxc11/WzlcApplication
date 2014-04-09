<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeAdd.aspx.cs" Inherits="CNVP.Admin.Appli.NoticeAdd" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<script type="text/javascript" src="../lib/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" language="javascript" src="../lib/ligerUI/js/core/base.js"></script>
<script type="text/javascript" language="javascript" src="../lib/ligerUI/js/plugins/ligerDialog.js"></script>
<script type="text/javascript" language="javascript" src="../lib/ligerUI/js/plugins/ligerTip.js"></script>
<script type="text/javascript" language="javascript" src="../lib/ligerUI/js/plugins/ligerCheckBox.js"></script>
<script type="text/javascript" language="javascript" src="../lib/ligerUI/js/plugins/ligerComboBox.js"></script>
<script type="text/javascript" language="javascript" src="../lib/ligerUI/js/plugins/ligerResizable.js"></script>
<script language="javascript" type="text/javascript" src="../lib/js/thickbox.js"></script>
<script language="javascript" type="text/javascript" src="../lib/js/common.js"></script>
<script language="javascript" type="text/javascript" src="../lib/js/LG.js"></script>
<script language="javascript" type="text/javascript" src="../lib/swfupload/swfupload.js"></script>
<script language="javascript" type="text/javascript" src="../lib/swfupload/swfupload.queue.js"></script>
<script language="javascript" type="text/javascript" src="../lib/swfupload/swfupload.handlers.js"></script>
<link rel="stylesheet" type="text/css" href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" />
<link rel="stylesheet" type="text/css" href="../lib/ligerUI/skins/Gray/css/all.css"/>
<link rel="stylesheet" type="text/css" href="../lib/css/common.css"/>
    <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.all.min.js"> </script>
    <!--建议手动加在语言，避免在ie下有时因为加载语言失败导致编辑器加载失败-->
    <!--这里加载的语言文件会覆盖你在配置项目里添加的语言类型，比如你在配置项目里配置的是英文，这里加载的中文，那最后就是中文-->
    <script type="text/javascript" charset="utf-8" src="/ueditor/lang/zh-cn/zh-cn.js"></script>

    <style type="text/css">
        .clear {
            clear: both;
        }
    </style>
</head>
<body style="text-align: center; overflow-x: hidden;">
<form id="form1" runat="server">
<div id="contentTab">
<div class="tab_con" style="display:block;">
<asp:HiddenField ID="HidTypeID" runat="server" />
<table width="800" border="0" cellspacing="0" cellpadding="0">
<tr>
<td>须知项目名称：</td>
<td><asp:TextBox ID="NoticeTitle" CssClass="l-text" Width="85%" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td valign="top">详细说明：</td>
<td style="padding-top:15px;padding-bottom:15px">
    <script id="editor" type="text/plain" style="width:600px;height:300px;"></script>
    <script type="text/javascript">
        UE.getEditor('editor');
    </script>
    </td>
</tr>
</table>
</div>

<div class="tab_con">
</div>
</div>
<div class="form-bar">
<div class="form-bar-inner"><div class="l-dialog-btn"><div class="l-dialog-btn-l"></div><div class="l-dialog-btn-r"></div><div class="l-dialog-btn-inner"><a href="javascript:void()" onclick="f_save();" class="l-dialog-btn-inner">保存</a></div></div><div class="l-dialog-btn"><div class="l-dialog-btn-l"></div><div class="l-dialog-btn-r"></div><div class="l-dialog-btn-inner"><a href="javascript:void()" onclick="f_cancel();" class="l-dialog-btn-inner">取消</a></div></div></div></div>
<script type="text/javascript">

    function f_save() {
        if ($("#NoticeTitle").val().length <= 0) {
            alert("请输入项目名称");
            $("#NoticeTitle").focus();
            return false;
        } else {
            
            $("#form1").attr('action', 'NoticeAdd.aspx?Action=Save');
            $("#form1").submit();
            return true;
        }
        
    }
    function f_cancel() {
        var win = parent || window;
        win.LG.closeAndReloadParent(null, "NoticeList");
    }
</script>
</form>
</body>
</html>
