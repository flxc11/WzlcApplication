<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TypeEdit.aspx.cs" Inherits="CNVP.Admin.Appli.TypeEdit" EnableEventValidation="false" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<script language="javascript" type="text/javascript" src="../lib/jquery/jquery-1.5.2.min.js"></script>
<script language="javascript" type="text/javascript" src="../lib/ligerUI/js/ligerui.min.js"></script>
<script language="javascript" type="text/javascript" src="../lib/js/common.js"></script>    
<script language="javascript" type="text/javascript" src="../lib/js/LG.js"></script>
<script language="javascript" type="text/javascript" src="../lib/jquery-validation/jquery.validate.min.js"></script> 
<script language="javascript" type="text/javascript" src="../lib/jquery-validation/jquery.metadata.js"></script>
<script language="javascript" type="text/javascript" src="../lib/jquery-validation/messages_cn.js"></script>
<script language="javascript" type="text/javascript" src="../lib/jquery.form.js"></script>
<script language="javascript" type="text/javascript" src="../lib/json2.js"></script>
<script language="javascript" type="text/javascript" src="../lib/js/validator.js"></script>
<script language="javascript" type="text/javascript" src="../lib/js/ligerui.expand.js"></script>
<script language="javascript" type="text/javascript" src="../lib/ligerUI/js/plugins/ligerFile.js"></script>
<script language="javascript" type="text/javascript" src="../lib/js/iconselector.js"></script> 
<link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="../lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css" />
<link href="../lib/css/common.css" rel="stylesheet" type="text/css" /> 
</head>
<body style="height:100%;">
<form id="mainform" method="post" class="l-form" action="TypeEdit.aspx?Action=EditInfo" onsubmit="return Checkform()">
    <input type="hidden" id="ID" name="ID" value="<%=CurrentID %>" />
    <div class="l-group l-group-hasicon">
        <img src="../lib/icons/32X32/communication.gif" /><span>基本信息</span>

    </div>
    <ul>
        <li style="width:100px;text-align:left;">档案类型名称：</li>
        <li style="width:220px;text-align:left;">
            <div class="l-text" style="width: 218px;">
                <input type="text" ltype="text" name="TypeName" id="TypeName"  class="l-text-field" value="<%=TypeName %>" style="width: 214px;"><div class="l-text-l"></div><div class="l-text-r"></div></div></li><li style="width:30px;"></li>

    </ul>
    <ul><li style="width:100px;text-align:left;">档案类型说明：</li><li style="width:570px;text-align:left;">
        <textarea ltype="textarea" name="TypeContent" id="TypeContent" style="width:550px;height:150px;text-align:left;" class="l-textarea"><%=TypeContent %></textarea></li><li style="width:30px;"></li></ul>
    <div style="float:left;width:100%; overflow:hidden; padding-left:100px;"> 
       <input type="submit" value="保存" class="l-dialog-btn-inner" style="width:60px;height:30px; line-height:20px; cursor:pointer" /> <input type="button" value="取消" class="l-dialog-btn-inner" style="width:60px;height:30px; line-height:20px; cursor:pointer" onclick="f_cancel()" /></div>
</form> 

<script type="text/javascript">
    function Checkform() {
        var rlst = false;
        if (/^\s*$/.test($('#TypeName').val())) {
            alert("请输入档案类型名称");
            $('#TypeName').focus();
        }
        else {
            rlst = true;
        }
        return rlst;
    }
    function f_cancel() {
        var win = parent || window;
        win.LG.closeAndReloadParent(null, "TypeList");
    }
    $(function () {
        if ($.browser.msie && $.browser.version == "6.0" && $("html")[0].scrollHeight > $("html").height())
            $("html").css("overflowX", "hidden");
    });
</script>
</body>
</html>
