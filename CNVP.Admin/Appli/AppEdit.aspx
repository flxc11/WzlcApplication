<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppEdit.aspx.cs" Inherits="CNVP.Admin.Appli.AppEdit" %>

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
    <style>
        .divmatra {
            float:left; margin-right:20px;margin-bottom:20px;
        }
    </style>
</head>
<body style="height:100%;">
<form method="post" action="AppEdit.aspx?Action=Edit" class="l-form" onsubmit="return Checkform()">
    <input type="hidden" id="ID" name="ID">
    <div class="l-group l-group-hasicon">
        <img src="../lib/icons/32X32/communication.gif"><span>基本信息</span>
    </div>

    <ul>
        <li style="width:100px;text-align:left;">姓名：<input type="hidden" name="AppID" id="AppID" value="<%=CurrentID %>" /></li>
        <li style="width:220px;text-align:left;"><div class="l-text" style="width: 218px;"><input type="text" ltype="text" name="AppName" id="AppName" value="<%=AppName %>" class="l-text-field" readonly="readonly"  style="width: 214px;">
            <div class="l-text-l"></div><div class="l-text-r"></div></div></li><li style="width:30px;"></li>
        <li style="width:100px;text-align:left;">身份证号：</li>
        <li style="width:220px;text-align:left;">
            <div class="l-text" style="width: 218px;">
                <input readonly="readonly" type="text" ltype="text" name="AppCardID" value="<%=AppCardID %>" id="AppCardID" class="l-text-field" style="width: 214px;">
            </div>
        </li>
        <li style="width:30px;"></li>
    </ul>
    <ul><li style="width:100px;text-align:left;">手机号码：</li><li style="width:220px;text-align:left;"><div class="l-text" style="width: 218px;"><input type="text" ltype="text" name="AppPhone" readonly="readonly" value="<%=AppPhone %>" id="AppPhone" ligerui="{&quot;width&quot;:218}" class="l-text-field" ligeruiid="AppPhone" style="width: 214px;"><div class="l-text-l"></div><div class="l-text-r"></div></div></li><li style="width:30px;"></li><li style="width:100px;text-align:left;">电子邮件：</li><li style="width:220px;text-align:left;"><div class="l-text" style="width: 218px;"><input type="text" readonly="readonly" ltype="text" name="AppEmail" value="<%=AppEmail %>" id="AppEmail" ligerui="{&quot;width&quot;:218}" class="l-text-field" ligeruiid="AppEmail" style="width: 214px;"><div class="l-text-l"></div><div class="l-text-r"></div></div></li><li style="width:30px;"></li></ul><ul><li style="width:100px;text-align:left;">地址：</li><li style="width:570px;text-align:left;"><div class="l-text" style="width: 568px;"><input type="text" ltype="text" name="AppAddress" value="<%=AppAddress %>" readonly="readonly" id="AppAddress" ligerui="{&quot;width&quot;:568}" class="l-text-field" ligeruiid="AppAddress" style="width: 564px;"><div class="l-text-l"></div><div class="l-text-r"></div></div></li><li style="width:30px;"></li></ul><div class="l-group l-group-hasicon"><img src="../lib/icons/32X32/communication.gif"><span>申请信息</span></div>
    <ul>
        <li style="width:100px;text-align:left;">申请类型：</li>
        <li style="width:220px;text-align:left;">
            <div class="l-text" style="width: 218px;"><input type="text" value="<%=AppType %>" readonly="readonly" name="AppType" id="AppType" ligerui="{&quot;width&quot;:218}" class="l-text-field" ligeruiid="AppType" style="width: 214px;">
            </div>
        </li>
        <li style="width:30px;"></li>
        <li style="width:100px;text-align:left;">结果需求：</li><li style="width:220px;text-align:left;"><div class="l-text" style="width: 218px;"><input readonly="readonly" type="text" ltype="text" name="AppResult" value="<%=AppResult %>" id="AppResult" ligerui="{&quot;width&quot;:218}" class="l-text-field" ligeruiid="AppResult" style="width: 214px;"><div class="l-text-l"></div><div class="l-text-r"></div></div></li><li style="width:30px;"></li>
    </ul>
    <ul>
        <li style="width:100px;text-align:left;">申请事项：</li>
        <li style="width:220px;text-align:left;">
            <div class="l-text" style="width: 218px;"><input type="text" value="<%=AppThings %>" readonly="readonly" name="AppThings" id="AppThings" ligerui="{&quot;width&quot;:218}" class="l-text-field" ligeruiid="AppType" style="width: 214px;">
            </div>
        </li>
        <li style="width:30px;"></li>
        <li style="width:100px;text-align:left;">申请时间：</li><li style="width:220px;text-align:left;"><div class="l-text" style="width: 218px;"><input readonly="readonly" type="text" ltype="text" name="PostTime" value="<%=_PostTime %>" id="PostTime" ligerui="{&quot;width&quot;:218}" class="l-text-field" ligeruiid="AppResult" style="width: 214px;"><div class="l-text-l"></div><div class="l-text-r"></div></div></li><li style="width:30px;"></li>
    </ul>
    <ul>
        <li style="width:100px;text-align:left;">申请内容：</li>
        <li style="width:575px;text-align:left;">
            <textarea ltype="textarea" name="AppContent" id="AppContent" class="l-textarea" readonly="readonly"><%=AppContent %></textarea>
        </li>
    </ul>
    <ul>
        <li style="width:100px;text-align:left;">管理员回复：</li>
        <li style="width:575px;text-align:left;">
            <textarea ltype="textarea" name="AppReply" id="AppReply" ligerui="{&quot;width&quot;:573}" class="l-textarea"><%=AppReply %></textarea></li><li style="width:30px;"></li>
    </ul>
    <ul>
        <li style="width:100px;text-align:left;">是否审核通过：</li>
        <li style="width:220px;text-align:left;">
            <input type="radio" value="1" name="IsAudit" <% if (IsAudit == "True")
                                                            { %>
                                                                
                                                            checked="checked" <%} %> />是
            <input type="radio" value="0" name="IsAudit" <% if (IsAudit == "False")

                                                            { %>
                                 
                                               
                                                            checked="checked" <%} %> />否    
        </li>
        <li style="width:30px;"></li>
        <li style="width:100px;text-align:left;">是否短信回复：</li>
        <li style="width:200px;text-align:left;">
            <input type="radio" value="1" name="IsSms" checked="checked" />是
            <input type="radio" value="0" name="IsSms" />否
        </li>
    </ul>
    <ul><li style="width:100px;text-align:left;">申请材料：</li><li style="width:575px;text-align:left;"><% =AppMaterial%></li><li style="width:30px;"></ul>
   <div style="float:left;width:100%; overflow:hidden; padding-left:100px;"> 
       <input type="submit" value="保存" class="l-dialog-btn-inner" style="width:60px;height:30px; line-height:20px; cursor:pointer" /> <input type="button" value="取消" class="l-dialog-btn-inner" style="width:60px;height:30px; line-height:20px; cursor:pointer" onclick="f_cancel()" /></div>
</form>
</body>
<script>
    function Checkform() {
        var rlst = false;
        if (/^\s*$/.test($('#AppReply').val())) {
            alert("请输入回复内容");
            $('#AppReply').focus();
        }
        else {
            rlst = true;
        }
        return rlst;
    }
    function f_cancel() {
        var win = parent || window;
        win.LG.closeAndReloadParent(null, "AppList");
    }
</script>
</html>