<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CNVP.Admin.Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title><%=CNVP.Config.UIConfig.Product %> - 管理中心</title>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<script type="text/javascript" language="javascript" src="lib/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" language="javascript" src="lib/ligerUI/js/core/base.js"></script>
<script type="text/javascript" language="javascript" src="lib/ligerUI/js/plugins/ligerDialog.js"></script>
<script type="text/javascript" language="javascript" src="lib/ligerUI/js/plugins/ligerTip.js"></script>
<script type="text/javascript" language="javascript" src="lib/ligerUI/js/plugins/ligerCheckBox.js"></script>
<script type="text/javascript" language="javascript" src="lib/ligerUI/js/plugins/ligerComboBox.js"></script>
<script type="text/javascript" language="javascript" src="lib/ligerUI/js/plugins/ligerResizable.js"></script>
<script type="text/javascript" language="javascript" src="lib/js/common.js"></script>
<script type="text/javascript" language="javascript" src="lib/js/LG.js"></script>
<link rel="stylesheet" type="text/css" href="lib/ligerUI/skins/Aqua/css/ligerui-all.css"/> 
<link rel="stylesheet" type="text/css" href="lib/ligerUI/skins/Aqua/css/ligerui-dialog.css"/>
<link rel="stylesheet" type="text/css" href="lib/ligerUI/skins/Gray/css/all.css"/> 
<link rel="stylesheet" type="text/css" href="lib/ligerUI/skins/Gray/css/dialog.css"/>
<style type="text/css">
    *{padding:0; margin:0;}
	html{background:#4974A4;}
    body{background:#4974A4;}
    #login{width:740px; margin:0 auto; font-size:12px;}
    #loginlogo{width:700px;height:60px;overflow:hidden;background:url('lib/images/login/logo.jpg') no-repeat; margin-top:85px;}
    #loginpanel{ width:729px; position:relative;height:300px;}
    .panel-h{ width:729px; height:20px; background:url('lib/images/login/panel-h.gif') no-repeat; position:absolute; top:0px; left:0px; z-index:3;}
    .panel-f{ width:729px; height:13px; background:url('lib/images/login/panel-f.gif') no-repeat; position:absolute; bottom:0px; left:0px; z-index:3;}
    .panel-c{ z-index:2;background:url('lib/images/login/panel-c.gif') repeat-y;width:729px; height:300px;}
    .panel-c-l{ position:absolute; left:60px; top:40px;}
    .panel-c-r{ position:absolute; right:20px; top:50px; width:222px; line-height:200%; text-align:left;}
    .panel-c-l h3{ color:#556A85; margin-bottom:10px;}
    .panel-c-l td{padding:7px;}
    .login-text{height:20px;line-height:20px;border:1px solid #e9e9e9;background:#f9f9f9;}
    .login-text-focus{border:1px solid #E6BF73;}
    .login-btn{width:114px;height:29px;color:#E9FFFF;line-height:29px;background:url('lib/images/login/login-btn.gif') no-repeat;border:none;cursor:pointer;font-size:14px;font-weight:bold;text-align:left;text-indent:15px}
    #txtUsername,#txtUserpass,#txtDepartment{width:220px;} 
	#txtDepartment{border:0px}
	.l-text{width:220px;height:20px;line-height:20px;background:#f9f9f9}
    #logincopyright{height:35px;line-height:35px;text-align:center;color:#FFFFFF;font-family:Verdana}
	#logincopyright a{color:#FFFFFF;font-size:13px;text-decoration:none}
</style>
<script type="text/javascript">
    var FromUrl = getQueryStringByName("FromUrl");
    if (!FromUrl) {
        FromUrl = encodeURIComponent("Index.aspx");
    }
    $(function () {
        $(".login-text").focus(function () {
            $(this).addClass("login-text-focus");
        }).blur(function () {
            $(this).removeClass("login-text-focus");
        });

        $(document).keydown(function (e) {
            if (e.keyCode == 13) {
                dologin();
            }
        });

        $("#btnLogin").click(function () {
            dologin();
        });

        $("#txtUsername").ligerTip();
        $("#txtUserpass").ligerTip();

        var params = { Action: 'GetAppList' };
        var applist;
        $.ajax({
            type: "post",
            dataType: "json",
            url: "Login.aspx?Time=" + (new Date().getTime()),
            data: params,
            success: function (d) {
                $("#txtDepartment").ligerComboBox({
                    data: d,
                    valueFieldID: 'txtAppID'
                });
                $("#txtDepartment").attr('value', d[0].text);
            }
        });

        function dologin() {
            var username = $("#txtUsername").val();
            var password = $("#txtUserpass").val();
            var appid = $("#txtAppID").val();
            if (username == "") {
                alert('您的登录帐号不能为空，请输入。');
                $("#txtUsername").focus();
                return;
            }
            if (password == "") {
                alert('您的登录密码不能为空，请输入。');
                $("#txtUserpass").focus();
                return;
            }
            $.ajax({
                type: 'post', cache: false, dataType: 'json',
                url: 'Login.aspx',
                data: [
                { name: 'Action', value: 'UserLogin' },
                { name: 'UserName', value: username },
                { name: 'UserPass', value: password },
				{ name: 'AppID', value: appid }
                ],
                success: function (d) {
                    if (d.msgCode == 1) {
                        window.location.href = decodeURIComponent(FromUrl);
                    }
                    else {
                        alert(d.msgStr);
                        $("#txtUserpass").focus();
                        return;
                    }
                },
                error: function () {
                    alert('发送系统错误,请与系统管理员联系!');
                },
                beforeSend: function () {
                    $.ligerDialog.waitting('用户正在登陆中，请稍后片刻...');
                    $("#btnLogin").attr("disabled", true);
                },
                complete: function () {
                    $.ligerDialog.closeWaitting();
                    $("#btnLogin").attr("disabled", false);
                }
            });
        }
    });
	

</script>
</head>
<body style="padding:10px;overflow:hidden" scroll="no">
<div id="login">
    <div id="loginlogo" style="background: none"></div>
    <div id="loginpanel">
        <div class="panel-h"></div>
        <div class="panel-c">
            <div class="panel-c-l">
                <table cellpadding="0" cellspacing="0">
                    <tbody>
                        <tr>
                        <td align="left" colspan="2"><h3>欢迎进入<%=CNVP.Config.UIConfig.Product %>管理中心</h3></td>
                        </tr> 
                        <tr>
                        <td align="right">账号：</td>
                        <td align="left"><input type="text" name="loginusername" id="txtUsername" maxlength="20" class="login-text" title="系统分配给您的登录帐号，例如：张三。"/></td>
                        </tr>
                        <tr>
                        <td align="right">密码：</td>
                        <td align="left"><input type="password" name="loginpassword" id="txtUserpass" maxlength="20" class="login-text"  title="建议定期修改登录密码，密码长度最大为20位。"/></td>
                        </tr>
                        <tr>
                        <td align="right"></td>
                        <td align="left"><input type="submit" id="btnLogin" value="立即进入" class="login-btn" /></td>
                        </tr> 
                    </tbody>
                </table>
            </div>
            <div class="panel-c-r">
            <p>请从左侧输入登录账号和密码登录</p>
            <p>如果遇到系统问题，请联系网络管理员。</p>
            <p>如果没有账号，请联系网站管理员。 </p>
            </div>
        </div>
        <div class="panel-f"></div>
    </div>
    <div id="logincopyright"><%=CNVP.Config.VerConfig.CopyRight %></div>
</div>
</body>
</html>