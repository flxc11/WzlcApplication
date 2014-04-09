<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppReg.aspx.cs" Inherits="CNVP.Admin.AppReg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="Lib/css/form.css" />
    <link rel="stylesheet" href="Lib/easyUI/themes/default/easyui.css" />
    <style type="text/css">
.red {
	color: #F00;
	height:20px;
	line-height:20px;
}
.fl {
	float:left;
}
.add1 {
	width:30px;
	height:20px;
	line-height:20px;
	text-align:center;
}
</style>
</head>
<body>
    <!--#include file="top.html"-->
    <div class="LCDA_MainArea m0a">
        <div class="w734 fr">
            <div class="InMain">
                <div class="T">在线注册</div>
                <div class="C">
                    <div class="InMainBox">
            <form id="contactform" class="rounded" method="post" action="?action=reg" enctype="multipart/form-data">
            <div class="field">
            <span class="red">*</span>
            <label for="name">姓 名：</label>
            <input type="text" class="input easyui-validatebox" missingMessage="姓名不能为空" name="talname" id="talname" required="true" maxlength="10" />
            <p class="hint">请输入姓名</p>
            </div>
            <div class="field">
            <span class="red">*</span>
            <label for="name">身份证号码：</label>
            <input type="text" class="input easyui-validatebox" name="talidcard" id="talidcard"  required="true" missingMessage="身份证号码不能为空" maxlength="18" validtype="checkIDCard"  />
            <p class="hint">请输入身份证号码</p>
            </div>
            <div class="field">
            <span class="red">*</span>
                <label for="name">手机号码：</label>
                <input type="text" class="input easyui-validatebox" name="taltel" id="taltel"  required="true" missingMessage="电话或手机不能为空" maxlength="30" validtype="checkPhone"  />
                <p class="hint">请输入电话或手机</p>
              </div>
            <div class="field">
                <label for="name">验证码：</label>
                <input type="text" name="CheckCode" id="CheckCode" class="Checkipt" />
                <input id="btGetCheck" type="button" value="获取验证码" style="width:90px;height:30px; line-height:30px; background:#613916; text-align:center; color:#fff; display:block;cursor:pointer; border:0;" />
                <div id="GetCheck"><div id="GetCheckText"></div></div>
            </div>
            <div class="field">
            <span class="red">*</span>
            <label for="name">地址：</label>
            <input type="text" class="input easyui-validatebox" name="taladdress" id="taladdress" maxlength="100" required="true" missingMessage="地址不能为空"  />
            <p class="hint">请输入地址</p>
            </div>
            <div class="field">
            <span class="red">*</span>
            <label for="name">电子邮件：</label>
            <input type="text" class="input easyui-validatebox" name="talemail" id="talemail" maxlength="100" required="true" missingMessage="电子邮件不能为空" validtype="email"  />
            <p class="hint">请输入电子邮件</p>
            </div>            <div class="field" style="width:100%;height:auto; text-align:left;"><img src="Lib/images/btnsubmit.png" onclick="checkSubmit()" style="cursor:pointer;margin-left:150px;margin-top:10px;" /></div>
            </form>
                    </div>
                </div>
            </div>
        </div>
	    <div class="w220 mr8 fl">
            <div class="SubNav" style="border-bottom: solid 1px #D6C09E">
        	<div class="T">网上办事</div>
            <div class="C">
            	<ul>
                    <li><a href="application.aspx">在线申请</a></li>
            	</ul>
            </div>
        </div>
	    </div>
    </div>
    <!--#include file="bottom.html"-->
</body>
    <script src="Lib/jquery/jquery-1.8.0.min.js"></script>
    <script src="Lib/js/jquery.easyui.min.js"></script>
    <script src="Lib/easyUI/locale/easyui-lang-zh_CN.js"></script>
    <script src="Lib/js/JScripts.js"></script>
    <script type="text/javascript">//<![CDATA[
        $(function () {
            $("#contactform").attr("action", "?action=reg");
            $.extend($.fn.validatebox.defaults.rules, {
                checkPhone: {
                    validator: function (value) {
                        var regu = /^1[3|4|5|8][0-9]\d{8}$/;
                        var re = new RegExp(regu);
                        return re.test(value);
                    },
                    message: '请输入正确的手机号码'
                },
                checkIDCard: {
                    validator: function (value) {
                        //					var regu =/(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|x|x)$)/;
                        //					var re = new regexp(regu);  
                        //					console.info(re.test(value));        
                        //					return re.test(value);

                        //console.info(ischinaidcard(value));
                        return isChinaIDCard(value);
                        //if(!ischinaidcard(value)) {
                        //						return false;
                        //					}      
                    },
                    message: '请输入正确的身份证号码'
                }
            });
        })
        function checkSubmit() {
            var app_type = $("input[type='radio'][name='app_type']:checked").val();
            if (/^\s*$/.test($("#talname").val())) {
                alert("请输入姓名");
                $("#talname").focus();
                return false;
            }
            if (/^\s*$/.test($("#talidcard").val())) {
                alert("请输入身份证号码");
                $("#talidcard").focus();
                return false;
            }
            if (/^\s*$/.test($("#taltel").val())) {
                alert("请输入电话号码");
                $("#taltel").focus();
                return false;
            }
            if (/^\s*$/.test($("#CheckCode").val())) {
                alert("请输入验证码");
                $("#CheckCode").focus();
                return false;
            }
            if (/^\s*$/.test($("#taladdress").val())) {
                alert("请输入地址");
                $("#taladdress").focus();
                return false;
            }
            if (/^\s*$/.test($("#talemail").val())) {
                alert("请输入电子邮件");
                $("#talemail").focus();
                return false;
            }
            if ($("#contactform").form("validate")) {
                $("#contactform").submit();
            } else {
                $.messager.alert("信息", "输入不合法，请检查后再提交！")
            }
        }

        $("#btGetCheck").click(function () {
            var $userPhone = $("#taltel");
            if (!/^1[358]{1}[0-9]{9}/.test($userPhone.val())) {
                alert("手机号码格式有误，请重新输入!");
                $userPhone.focus();
                return false;
            } else {
                $.ajax({
                    type: "post",
                    dataType: "json",
                    url: "Ajax.aspx?Action=SendSms&UserPhone=" + $userPhone.val() + "",
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(XMLHttpRequest.status);
                        alert(XMLHttpRequest.readyState);
                        alert(textStatus);
                    },
                    success: function (d) {
                        if (d.rslt == "0") {

                        } else {
                            alert("验证码获取失败，请重新获取!");
                            time(o);
                        }
                    }
                });
            }
            time(this);
            //var html = "数字校验码短信已经免费发送到您的手机";
            //html += "<br />如果1分钟内没有收到校验码短信，请点击按钮重新获取。";
            //$("#GetCheckText").html(html);

        });
        var wait = 60;
        function time(o) {
            if (wait == 0) {
                o.removeAttribute("disabled");
                o.value = "获取验证码";
                wait = 60;
            } else {
                o.setAttribute("disabled", true);
                o.value = "重新发送(" + wait + "秒)";
                wait--;
                setTimeout(function () {
                    time(o)
                },
            1000)
            }
        }
</script>
</html>
