<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Application.aspx.cs" Inherits="CNVP.Admin.Application" %>

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
    <script>
        function choose(obj) {
            var _typec = "<%=_TypeContentList %>";
            var arr = _typec.split("|");
            var _index = obj.selectedIndex;
            if (_index === 0) {
                $("#_typecnt").hide();
                $("#TypeContent").html("");
            } else {
                $("#_typecnt").show();
                $("#TypeContent").html(arr[_index - 1]);
            }
        }
    </script>
    <!--#include file="top.html"-->
    <div class="LCDA_MainArea m0a">
        <div class="w734 fr">
            <div class="InMain">
                <div class="T">在线申请
                    <span style="float:right;"><%=_AppName %>, 欢迎您！ <a href="lgout.aspx">退出</a></span>
                </div>
                <div class="C">
                    <div class="InMainBox">
            <form id="contactform" class="rounded" method="post" action="?action=reg" enctype="multipart/form-data">
                <div class="field">
                    <label for="name">申请类型：</label>
                    <input type="radio" name="app_type" value="0" id="" checked="checked" />个人
                    <input type="radio" name="app_type" value="1" id="" />单位
                </div>
            <div class="field">
            <label for="name">姓 名：</label>
            <input type="text" class="input" name="talname" id="talname" value="<%=_AppName %>" readonly="readonly" maxlength="10" />
            </div>
            <div class="field">
            <label for="name">身份证号码：</label>
            <input type="text" class="input" readonly="readonly" name="talidcard" id="talidcard" maxlength="18" value="<%=_AppCardID %>"  />
            </div>
            <div class="field">
                <label for="name">手机号码：</label>
                <input type="text" class="input" readonly="readonly" name="taltel" id="taltel" value="<%=_AppPhone %>"  />
                <p class="hint">请输入电话或手机</p>
              </div>
            <div class="field">
                <label for="name">验证码：</label>
                <input type="text" name="CheckCode" id="CheckCode" class="Checkipt" />
                <input id="btGetCheck" type="button" value="获取验证码" style="width:90px;height:30px; line-height:30px; background:#613916; text-align:center; color:#fff; display:block;cursor:pointer; border:0;" />
                <div id="GetCheck"><div id="GetCheckText"></div></div>
            </div>
            <div class="field">
            <label for="name">地址：</label>
            <input type="text" class="input" readonly="readonly" name="taladdress" id="taladdress" value="<%=_AppAddress %>" />
            </div>
            <div class="field">
            <label for="name">电子邮件：</label>
            <input type="text" class="input" readonly="readonly" name="talemail" id="talemail" value="<%=_AppEmail%>" />
            </div>
            <div class="field">
            <span class="red">*</span>
            <label for="name">申请事项：</label>
                <select id="AppType" name="AppType" style="width:280px;height:30px;" onchange="choose(this)">
                    <%=_TypeList %>
                </select>
            </div>
            <div class="field" id="_typecnt" style="display:none;">
            <span class="red">*</span>
            <label for="name">事项说明：</label>
                <textarea name="TypeContent" class="input" id="TypeContent" style="width:270px;height:100px;overflow:hidden;" readonly="readonly"></textarea>
            </div>
            <div class="field">
            <span class="red">*</span>
            <label for="name">申请内容：</label>
                <textarea name="AppContent" class="input" id="AppContent" style="width:270px;height:100px;overflow:hidden;"></textarea>
            </div>
            <div class="field">
                <label for="name">结果需求：</label>
                <input type="radio" name="app_result" value="0" id="" checked="checked" />邮件
                <input type="radio" name="app_result" value="1" id="" />快递（到付）
            </div>
            <div class="field" id="pic1">
                <span class="red">*</span>
                <label for="message">身份证正面：</label>
                <input type="file" class="input fl" id ="mfile0_0"  name="mfile0_0"  />
                <p class="hint">请上传身份证正面照片</p>
            </div>
            <div class="field" id="pic2">
                <span class="red">*</span>
                <label for="message">身份证背面：</label>
                <input type="file" class="input fl" id ="mfile0_1"  name="mfile0_1" />
                <p class="hint">请上传身份证背面照片</p>
            </div>
            <div class="field" style="display:none;" id="pic3">
                <span class="red">*</span>
                <label for="message">单位介绍信：</label>
                <input type="file" class="input fl" id ="mfile0_2"  name="mfile0_2"  />
                <p class="hint">请上传单位介绍信</p>
            </div>
            <div class="field">
                <label for="message">其它证件：</label>
                <p id="MyFile0" style="float:left;width:350px;overflow:hidden;margin:0;padding:0;">
                <input type="file" class="input fl" id ="mfile0_3"  name="mfile0_3" />
                <input type="button" value="增加" class="fl add1" onclick="addFile('MyFile0')" />
                </p>
            </div>
            <div class="field" style="width:100%;height:auto; text-align:left;"><img src="Lib/images/btnsubmit.png" onclick="checkSubmit()" style="cursor:pointer;margin-left:150px;margin-top:10px;" /></div>
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
                    <li><a href="appli2.aspx">申请记录</a></li>
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
            var $radio = $("input[type='radio'][name='app_type']");
            $radio.on("click", function () {
                if ($(this).val() === "1") {
                    $("#pic3").show();
                }
                if ($(this).val() === "0") {
                    $("#pic3").hide();
                    $("#pic3").find("input").attr("value", "");
                }
            })
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
            var app_type = $("input[type='radio']:checked").val();
            if (/^\s*$/.test($("#CheckCode").val())) {
                alert("请输入验证码！");
                $("#CheckCode").focus();
                return false;
            }
            if (/^\s*$/.test($("#AppContent").val())) {
                alert("请输入申请事项！");
                $("#AppContent").focus();
                return false;
            }
            if (/^\s*$/.test($("#mfile0_0").val())) {
                alert("请上传身份证正面照片！");
                $("#mfile0_0").focus();
                return false;
            }
            if (/^\s*$/.test($("#mfile0_1").val())) {
                alert("请上传身份证背面照片！");
                $("#mfile0_1").focus();
                return false;
            }
            if (app_type === "1") {
                if (/^\s*$/.test($("#mfile0_2").val())) {
                    alert("请上传单位介绍信图片！");
                    $("#mfile0_2").focus();
                    return false;
                }
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
        function addFile(id) {
            var inputstr = "";
            var i = 1;
            var index = id.substring(6);
            
            while (true) {
                if (document.getElementById("mfile" + index + "_" + i) == null) break;
                else i++;
            }
            console.debug(i);
            var inputid = "mfile" + index + "_" + i;
            var str = "<br /><INPUT type=\"file\" class=\"input fl\" id=\"" + inputid + "\" NAME=\"" + inputid + "\" /><INPUT id=\"r" + inputid + "\" type=\"button\" class=\"fl add1\" value=\"删除\" onclick=removeFile(\"" + inputid + "\") />";
            document.getElementById(id).insertAdjacentHTML("beforeEnd", str);

        }
        function removeFile(id) {
            var obj = document.getElementById(id);
            var obj1 = document.getElementById("r" + id);
            if (obj != null && obj1 != null) {
                //obj.removeNode(true);
                //obj1.removeNode(true);
                obj.parentNode.removeChild(obj);
                obj1.parentNode.removeChild(obj1);
            }
        }
</script>
</html>
