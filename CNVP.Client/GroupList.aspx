<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupList.aspx.cs" Inherits="CNVP.Client.GroupList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>汽车精品展示系统</title>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<script language="javascript" type="text/javascript" src="Js/JQuery.js"></script>
<script language="javascript" type="text/javascript" src="Js/Common.js"></script>
<script language="javascript" type="text/javascript" src="Js/IScroll.js"></script>
<link rel="stylesheet" type="text/css" href="Css/Global.css"/>
<link rel="stylesheet" type="text/css" href="Css/Layout.css"/>
<link rel="stylesheet" type="text/css" href="Css/ScrollBar.css"/>
</head>
<body>
<table style="width:100%;height:100px" border="0" cellspacing="0" cellpadding="0">
<tr>
<td colspan="3" style="background:url(Images/HeaderBg.png) bottom repeat-x">
<div class="Logo"><a href="/"><img src="Images/Logo.png" /></a></div>
<div class="Back"><a id="A1" href="/">返回</a></div>
<div class="Search">
<dl>
    <dd class="Input"><input type="text" id="Text1" name="keyword" value=""/></dd>
    <dd class="Btn"><img src="Images/BtnSearch.png" align="absmiddle" /></dd>
</dl>
</div>
</td>
</tr>
</table>
<table id="Part" border="0" cellspacing="0" cellpadding="0">
<tr>
<td id="Part01" valign="top">
    <div class="menu">
    <dl>
        <dt></dt>
        <dd class="current"><a href="GroupList.aspx" class="tc">套餐</a></dd>
        <dd><a href="CarList.aspx" class="cx">车型</a></dd>
        <dd><a href="TypeList.aspx" class="jp">精品</a></dd>
        <dd><a href="ProductList.aspx" class="tyj">通用件</a></dd>
        <dd><a href="CartList.aspx" class="gwc">购物车</a></dd>
    </dl>
    </div>
    <div class="bottonlogo"><a href="http://www.hongxu.cn" target="_blank"><img src="Images/BottomLogo.png" /></a></div>
</td>
<td id="Part03" valign="top">
<div id="GroupList">
    <div class="Scroller">
    <ul class="Content"></ul>
    </div>
</div>
</td>
</tr>
</table>
<div id="Loading"></div>
<script language="javascript" type="text/javascript">
var myScroll;
function loaded() {
    //myScroll = new iScroll('GroupList', { scrollbarClass: 'myScrollbar' });
    myScroll = new iScroll('GroupList');
}
document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false);
document.addEventListener('DOMContentLoaded', function () { setTimeout(loaded, 500); }, false);
</script>

<script language="javascript" type="text/javascript">
    window.onload = function () {
        $('#Loading').show();
        PageReset();
        //调用套餐组合
        GetGroupList();
        //读取购物信息
        GetCartInfo();
        /*$('.menu dd').click(function () {
        $(this).addClass("current").siblings().removeClass("current");
        });*/
    }

window.onresize=function()	{
	PageReset();
}

///*************************************************
//描述：重新获取页面宽度
//*************************************************/
function PageReset()
{
    var width = $(window).width();
    var height = $(window).height();
    $('#Part').css('height', (height - 100) + 'px');
	$('#Part03').css('width', (width - 60) + 'px');
}

///*************************************************
//描述：调用所有套餐组合
//*************************************************/
function GetGroupList()
{
	var params = {
		Action: 'GetGroupList'
	};
	$.ajax({
		type: "GET",
		dataType: "html",
		url: "GroupList.aspx?Time=" + (new Date().getTime()),
		data: params,
		success: function (result) {
		    $(result).appendTo($(".Content"));
		    myScroll.refresh();
		}
    });
    $('#Loading').hide();
}
</script>
</body>
</html>