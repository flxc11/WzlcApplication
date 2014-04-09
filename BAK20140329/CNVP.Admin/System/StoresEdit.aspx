<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoresEdit.aspx.cs" Inherits="CNVP.Admin.StoresEdit" %>
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
<form id="mainform" method="post" action="Stores.aspx?Action=EditAppInfo"></form> 
<script type="text/javascript">
    var AppID = '<%=CurrentAppID%>';
	//表单底部按钮 
    LG.setFormDefaultBtn(f_cancel, f_save);

    //创建表单结构
    var mainform = $("#mainform");
    mainform.ligerForm({
        inputWidth: 220,
        fields: [
        { name: "AppID", type: "hidden" },
        { display: "门店名称", name: "AppName", newline: true, labelWidth: 100, width: 220, space: 30, type: "text", validate: { required: true, maxlength: 50 }, group: "门店信息", groupicon: "../lib/icons/32X32/communication.gif" },
        { display: "门店状态", name: "IsLock", newline: false, labelWidth: 100, width: 220, space: 30, type: "select", comboboxName: "DropIsLock", options: { valueFieldID: "IsLock", url: "Stores.aspx?Action=GetAppState"} },
        { display: "门店域名", name: "AppUrl", newline: true, labelWidth: 100, width: 220, space: 30, type: "text", validate: { maxlength: 30} },
        { display: "门店地址", name: "AppAddRess", newline: false, labelWidth: 100, width: 220, space: 30, type: "text", validate: { maxlength: 25} },
        { display: "联系电话", name: "AppTelPhone", newline: true, labelWidth: 100, width: 220, space: 30, type: "text", validate: { maxlength: 25} },
        { display: "传真号码", name: "AppFaxNumber", newline: false, labelWidth: 100, width: 220, space: 30, type: "text", validate: { maxlength: 30} },
        { display: "售后电话", name: "AppServiceTelPhone", newline: true, labelWidth: 100, width: 220, space: 30, type: "text", validate: { maxlength: 30} },
        { display: "救援电话", name: "AppSOSTelPhone", newline: false, labelWidth: 100, width: 220, space: 30, type: "text", validate: { maxlength: 25} },
        { display: "通讯公钥", name: "AppPubKey", newline: true, labelWidth: 100, width: 575, space: 30, type: "textarea" },
        { display: "通讯私钥", name: "AppPriKey", newline: true, labelWidth: 100, width: 575, space: 30, type: "textarea"}],
        toJSON: JSON2.stringify
    });

    jQuery.metadata.setType("attr", "validate");
    LG.validate(mainform);
	LG.loadForm(mainform, {ashxUrl:'Stores.aspx?Time=' + (new Date().getTime()), data: {Action:'GetAppInfo', AppID: AppID} },f_loaded);
	
	function f_loaded(){
	}
    function f_save() {
        LG.submitForm(mainform, function (data) {
            var win = parent || window;
            if (data.IsError) {
                alert(data.Message);
            }
            else {
                win.LG.closeAndReloadParent(null, "Stores");
            }
        });
    }
    function f_cancel() {
        var win = parent || window;
		win.LG.closeAndReloadParent(null, "Stores");
    }	 
</script>
</body>
</html>