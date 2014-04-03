<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RolesAdd.aspx.cs" Inherits="CNVP.Admin.RolesAdd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
<body style="height:100%;overflow:hidden" scroll="no">
<form id="mainform" method="post" action="Roles.aspx?Action=AddRoles"></form> 
<script type="text/javascript">
    //表单底部按钮 
    LG.setFormDefaultBtn(f_cancel, f_save);

    //创建表单结构
    var mainform = $("#mainform");
    mainform.ligerForm({
        inputWidth: 220,
        fields: [
        { name: "RoleID", type: "hidden" },
        { display: "角色名称", name: "RoleName", newline: true, labelWidth: 100, width: 220, space: 30, type: "text", validate: { required: true, maxlength: 50 }, group: "角色信息", groupicon: "../lib/icons/32X32/communication.gif" },
        { display: "角色说明", name: "RoleRemark", newline: true, labelWidth: 100, width: 575, space: 30, type: "textarea" }],
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
                win.LG.closeAndReloadParent(null, "Roles");
            }
        });
    }
    function f_cancel() {
        var win = parent || window;
        win.LG.closeAndReloadParent(null, "Roles");
    }	 
</script>
</body>
</html>