<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="appli1-1.aspx.cs" Inherits="CNVP.Admin.appli1_1" %>

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
                <div class="T">申请须知</div>
                <div class="C">
                    <div class="InMainBox">
                    <%=_Content %>

                        <div class="agree" style="text-align:center;">
                <input type="button" name="button" id="submit" value="同意并开始申请" style="width:130px;height:30px; line-height:30px; text-align:center; border:0; cursor:pointer;" onclick="jump()" /></div>

                    </div>
                    <script type="text/javascript">
                        var wait = 10; //设置秒数(单位秒)
                        var secs = 0;
                        for (var i = 1; i <= wait; i++) {
                            window.setTimeout("stimer(" + i + ")", i * 1000);
                        }
                        function stimer(num) {
                            if (num == wait) {
                                document.getElementById("submit").value = "同意并开始申请";
                                document.getElementById("submit").disabled = false;
                            }
                            else {
                                secs = wait - num;
                                document.getElementById("submit").value = "请先阅读申请须知 (" + secs + ")";
                                document.getElementById("submit").disabled = true;
                            }
                        }
                        function jump() {
                            window.location.href = "appsearch.aspx";
                        }
    </script>
                </div>
            </div>
        </div>
	    <div class="w220 mr8 fl">
            <div class="SubNav" style="border-bottom: solid 1px #D6C09E">
        	<div class="T">网上办事</div>
            <div class="C">
            	<ul>
                    <li><a href="application.aspx">立即申请</a></li>
                    <li><a href="appli2.aspx">查看申请记录</a></li>
            	</ul>
            </div>
        </div>
	    </div>
    </div>
    <!--#include file="bottom.html"-->
</body>
    
</html>
