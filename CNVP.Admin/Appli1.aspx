<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Appli1.aspx.cs" Inherits="CNVP.Admin.Appli1" %>

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
                <div class="T">申请流程
                    
                </div>
                <div class="C">
                    <div class="InMainBox" style="text-align:center;">
                    <img src="/lib/images/lc.jpg" width="600" />
                        <div style="width=100%;text-align:center">
                        <input type="button" value="下一步" style="width:80px;height:30px; line-height:30px; cursor:pointer;" onclick="window.location.href='appli1-1.aspx'" /></div>
                    </div>
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
