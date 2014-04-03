<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Appli2.aspx.cs" Inherits="CNVP.Admin.Appli2" %>

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
            <div class="InMain" style="min-height:500px; _height:500px;">
                <div class="T">申请记录
                    <span style="float:right;"><%=_UserName %>, 欢迎您！ <a href="lgout.aspx">退出</a></span>
                </div>
                <div class="C">
                    <div class="InMainBox">
                        <table width="700" align="center" border="0" cellspacing="1" cellpadding="1" bgcolor="#cccccc">
                            <tr>
                                <td width="40" height="50" align="center" valign="middle" bgcolor="#FFFFFF">序号</td>
                                <td width="82" align="center" valign="middle" bgcolor="#FFFFFF">姓名</td>
                                <td width="152" align="center" valign="middle" bgcolor="#FFFFFF">申请事项</td>
                                <td width="135" align="center" valign="middle" bgcolor="#FFFFFF">申请时间</td>
                                <td width="75" align="center" valign="middle" bgcolor="#FFFFFF">申请状态</td>
                                <td width="200" align="center" valign="middle" bgcolor="#FFFFFF">管理员留言</td>
                            </tr>
                            <asp:Repeater runat="server" ID="rptList">
                                <ItemTemplate>
                                    <tr>
                                <td width="40" height="50" align="center" valign="middle" bgcolor="#FFFFFF"><%#Container.ItemIndex + 1 %></td>
                                <td width="82" align="center" valign="middle" bgcolor="#FFFFFF"><%#Eval("AppName") %></td>
                                <td width="152" align="center" valign="middle" bgcolor="#FFFFFF"><%#GetAppType(Eval("AppThings").ToString()) %></td>
                                <td width="135" align="center" valign="middle" bgcolor="#FFFFFF"><%#Convert.ToDateTime(Eval("PostTime").ToString()).ToString("yyyy-MM-dd") %></td>
                                <td width="75" align="center" valign="middle" bgcolor="#FFFFFF"><%#GetResult(Eval("IsAudit").ToString()) %></td>
                                <td width="200" align="center" valign="middle" bgcolor="#FFFFFF"><%#Eval("AppReply") %></td>
                            </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div id="paging" class="flickr right"><asp:Literal ID="LitPager" runat="server"></asp:Literal></div>
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
    <script src="Lib/jquery/jquery-1.8.0.min.js"></script>
    
</html>
