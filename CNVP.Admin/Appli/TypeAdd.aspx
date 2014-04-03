<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TypeAdd.aspx.cs" Inherits="CNVP.Admin.Appli.TypeAdd" EnableEventValidation="false" ValidateRequest="false" %>

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
<form id="mainform" method="post" action="TypeList.aspx?Action=AddInfo"></form> 
<script type="text/javascript">
    //表单底部按钮 
    LG.setFormDefaultBtn(f_cancel, f_save);

    //创建表单结构
    var mainform = $("#mainform");
    mainform.ligerForm({
        inputWidth: 220,
        fields: [
        { name: "ID", type: "hidden" },
        { display: "事项名称", name: "TypeName", newline: true, labelWidth: 100, width: 220, space: 30, type: "text", validate: { required: true, messages: { required: '事项名称不能为空' } }, group: "基本信息", groupicon: "../lib/icons/32X32/communication.gif" },
        { display: "事项说明", name: "TypeContent", newline: true, labelWidth: 100, width: 575, space: 30, type: "textarea" }],
        toJSON: JSON2.stringify
    });
    jQuery.metadata.setType("attr", "validate");
    LG.validate(mainform);

    function f_save() {
        LG.submitForm(mainform, function (data) {
            var win = parent || window;
            if (data.IsError) {
                alert(data.Message);
            }
            else {
                win.LG.closeAndReloadParent(null, "TypeList");
            }
        });
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
