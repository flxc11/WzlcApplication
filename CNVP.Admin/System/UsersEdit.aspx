<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsersEdit.aspx.cs" Inherits="CNVP.Admin.UsersEdit" %>
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
<body style="height:100%;">
<form id="mainform" method="post" action="Users.aspx?Action=EditUserInfo"></form> 
<script type="text/javascript">
    var UserID = '<%=CurrentUserID%>';
    //表单底部按钮 
    LG.setFormDefaultBtn(f_cancel, f_save);

    //创建表单结构
    var mainform = $("#mainform");
    mainform.ligerForm({
        inputWidth: 220,
        fields: [
        { name: "UserID", type: "hidden" },
        { display: "登录帐号", name: "LoginName", newline: true, labelWidth: 100, width: 220, space: 30, type: "text", validate: { required: true, username: true, minlength: 4, maxlength: 20 }, group: "基本信息", groupicon: "../lib/icons/32X32/communication.gif" },
        { display: "隶属门店", name: "AppID", newline: false, labelWidth: 100, width: 220, space: 30, type: "select", comboboxName: "DropAppName", options: { valueFieldID: "AppID", url: "Users.aspx?Action=StoreList"} },
        { display: "登录密码", name: "UserPass", newline: true, labelWidth: 100, width: 220, space: 30, type: "password", validate: { maxlength: 30 } },
        { display: "确认密码", name: "UserPass1", newline: false, labelWidth: 100, width: 220, space: 30, type: "password", validate: { maxlength: 30, equalTo: '#UserPass', messages: {equalTo: '两次密码输入不一致。'}} },
        { display: "真实姓名", name: "UserTrueName", newline: true, labelWidth: 100, width: 220, space: 30, type: "text", validate: { maxlength: 30} },
        { display: "联系信箱", name: "UserEMail", newline: false, labelWidth: 100, width: 220, space: 30, type: "text", validate: { maxlength: 25} },
        { display: "备注说明", name: "UserRemark", newline: true, labelWidth: 100, width: 575, space: 30, type: "textarea" },
        { display: "<a href=\"javascript:top.f_addTab('Roles', '角色列表', 'System/Roles.aspx');\">隶属角色</a>", name: "RoleID", newline: true, labelWidth: 100, width: 220, space: 30, type: "select", comboboxName: "DropRoleName", options: { valueFieldID: "RoleID", url: "Users.aspx?Action=RoleList" }, group: "权限信息", groupicon: "../lib/icons/32X32/communication.gif" },
        { display: "超级用户", name: "IsAdmin", newline: false, labelWidth: 100, width: 220, space: 30, type: "select", comboboxName: "DropIsAdmin", options: { valueFieldID: "IsAdmin", url: "Users.aspx?Action=IsAdmin"} },
        { display: "用户性别", name: "UserSex", newline: true, labelWidth: 100, width: 220, space: 30, type: "select", comboboxName: "DropUserSex", options: { valueFieldID: "UserSex", url: "Users.aspx?Action=GetUserSex" }, validate: { maxlength: 30 }, group: "其他信息", groupicon: "../lib/icons/32X32/communication.gif" },
        { display: "固定电话", name: "UserTelPhone", newline: false, labelWidth: 100, width: 220, space: 30, type: "text", validate: { maxlength: 30} },
        { display: "传真号码", name: "UserFaxNumber", newline: true, labelWidth: 100, width: 220, space: 30, type: "text", validate: { maxlength: 30} },
        { display: "手机号码", name: "UserMobile", newline: false, labelWidth: 100, width: 220, space: 30, type: "text", validate: { maxlength: 30} },
        { display: "QQ号码", name: "UserQQ", newline: true, labelWidth: 100, width: 220, space: 30, type: "text", validate: { maxlength: 30} },
        { display: "用户昵称", name: "UserNickName", newline: false, labelWidth: 100, width: 220, space: 30, type: "text", validate: { maxlength: 30} },
        { display: "通讯地址", name: "UserAddRess", newline: true, labelWidth: 100, width: 575, space: 30, type: "textarea"}],
        toJSON: JSON2.stringify
    });

    jQuery.metadata.setType("attr", "validate");
    LG.validate(mainform);
    $("#LoginName").attr("disabled", "disabled");

    LG.loadForm(mainform, { ashxUrl: 'Users.aspx?Time=' + (new Date().getTime()), data: { Action: 'GetUserInfo', UserID: UserID} }, f_loaded);

    function f_loaded() {
    }
    function f_save() {
        LG.submitForm(mainform, function (data) {
            var win = parent || window;
            if (data.IsError) {
                alert(data.Message);
            }
            else {
                win.LG.closeAndReloadParent(null, "Users");
            }
        });
    }
    function f_cancel() {
        var win = parent || window;
        win.LG.closeAndReloadParent(null, "Users");
    }
    $(function () {
        if ($.browser.msie && $.browser.version == "6.0" && $("html")[0].scrollHeight > $("html").height())
            $("html").css("overflowX", "hidden");
    }); 
</script>
</body>
</html>