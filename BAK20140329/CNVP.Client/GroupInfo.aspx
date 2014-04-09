<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupInfo.aspx.cs" Inherits="CNVP.Client.GroupInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>汽车精品展示系统</title>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<script language="javascript" type="text/javascript" src="Js/JQuery.js"></script>
<script language="javascript" type="text/javascript" src="Js/Common.js"></script>
<script language="javascript" type="text/javascript" src="Js/IScroll.js"></script>
<script language="javascript" type="text/javascript" src="Js/ThickBox.js"></script>
<script language="javascript" type="text/javascript" src="Js/Option.js"></script>
<link rel="stylesheet" type="text/css" href="Css/Global.css"/>
<link rel="stylesheet" type="text/css" href="Css/Layout.css"/>
<link rel="stylesheet" type="text/css" href="Css/ThickBox.css"/>
<link rel="stylesheet" type="text/css" href="Css/Option.css"/>
<link rel="stylesheet" type="text/css" href="Css/ScrollBar.css"/>
</head>
<body>
<table style="width:100%;height:100px" border="0" cellspacing="0" cellpadding="0">
<tr>
<td colspan="3" style="background:url(Images/HeaderBg.png) bottom repeat-x">
<div class="Logo"><a href="/"><img src="Images/Logo.png" /></a></div>
<div class="Back"><a id="BtnBack" href="GroupList.aspx">返回</a></div>
<div class="Search">
<dl>
    <dd class="Input"><input type="text" id="keyword" name="keyword" value=""/></dd>
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
<td id="Part02" valign="top">
    <div id="GroupInfo">
        <div class="Scroller">
        <ul class="Content"></ul>
        </div>
    </div>
</td>
<td id="Part03" valign="top">
<table style="width:100%;padding:0px 15px" border="0" cellspacing="0" cellpadding="0">
<tr>
<td style="height:35px;line-height:35px;font-size:14px;font-weight:bolder"><div class="GroupName" style="float:left;width:500px;"></div><div style="float:left;width:165px;text-align:right;font-weight:normal;"><a href="" style="color:#FF0000" id="GroupAddCart">[加入购物车]</a></div></td>
</tr>
<tr>
<td>
<div>
<div style="float:left;width:500px">
<dl>
<dt class="GroupImages"></dt>
<dd style="height:35px;line-height:35px;border-bottom:solid 1px #ccc"><span class="GroupInfo"></span></dd>
<dd class="GroupRemarks" style="padding:5px 0px"></dd>
</dl>
</div>
<div id="GroupProductInfo">
    <div class="Scroller">
    <div id="pullDown">
    	<span class="pullDownIcon"></span><span class="pullDownLabel">松开立即刷新...</span>
    </div>
    <dl id="thelist" class="ProductList"></dl>
    <div id="pullUp">
    	<span class="pullUpIcon"></span><span class="pullUpLabel">松开立即刷新...</span>
    </div>
    </div>
</div>
</div>
</td>
</tr>
</table>
</td>
</tr>
</table>
<div id="ProductInfo"></div>
<div id="Loading"></div>
<script language="javascript" type="text/javascript">
	var RecordCount=0, PageCount=0, PageNo=1;
	var GroupID= '<%=Request.Params["GroupID"] %>';
	var myScroll,myScroll1, pullDownEl, pullDownOffset, pullUpEl, pullUpOffset, generatedCount = 0;
	
    window.onload = function () {
        $('#Loading').show();
        PageReset();
        GetGroupList();
        //读取购物信息
        GetCartInfo();

        $('#ProductInfo').css('display', 'none');
    }
	
    window.onresize = function () {
        PageReset();
    }
	
	///*************************************************
    //描述：设置全局宽度样式
    //*************************************************/
    function PageReset() {
		var width = $(window).width();
        var height = $(window).height();
		$('#Part').css('height',(height-100)+'px');
		$('#Part03').css('width', (width - 280) + 'px');

		$('#ProductInfo').css('height', (height - 95) + 'px');
		$('#ProductInfo').css('width', (width - 60) + 'px');
    }

    ///*************************************************
    //描述：调用所有套餐组合
    //*************************************************/
    function GetGroupList() {
        $.ajax({type: "GET",dataType: "html",url: "GroupInfo.aspx?Time=" + (new Date().getTime()),data: {Action: 'GetGroupList',GroupID: GroupID},
            success: function (result) {
                $(result).appendTo($(".Content"));
				
				//读取选择套餐信息
				GetGroupInfo(GroupID);
            }
        });
    }
	///*************************************************
    //描述：读取选择套餐信息
    //*************************************************/
	function GetGroupInfo(GroupID)
	{
		PageNo=1;
		$('.ProductList').empty();
		$('#pullUp').css('display','');
		
		$('#Loading').show();
		$('.Content dd').click(function () {
            $(this).addClass("current").siblings().removeClass("current");
        });

        $.ajax({ type: "GET", dataType: "JSON", url: "GroupInfo.aspx?Time=" + (new Date().getTime()), data: { Action: 'GetGroupInfo', GroupID: GroupID },
            success: function (d) {
                if (d.msgCode == 0) {
                    $(".GroupName").html('套餐名称：' + d.GroupName);
                    $(".GroupImages").html('<a href="javascript:void();" onclick="ProductImages(\'' + GroupID + '\',\'GroupInfo\')"><img src="' + d.GroupImages + '" style="width:500px"  id="FlyToCart"/></a>');
                    $(".GroupInfo").html('<b>套餐原价：</b>￥<span style="text-decoration:line-through">' + d.AllPrice + '</span>元&nbsp;&nbsp;<b>活动价格：</b>￥' + d.GroupPrice + '元&nbsp;&nbsp;<b>适用车型：</b>' + d.CarName);
                    $(".GroupRemarks").html("<b>套餐描述：</b>" + d.GroupRemarks);
                    $("#GroupAddCart")[0].href = "javascript:GroupAddCart('"+ GroupID +"');";

                    //获取套餐精品信息
                    GetProductList(GroupID);
                    $('#Loading').hide();
                }
            }
        });
	}
	///*************************************************
    //描述：读取套餐精品信息
    //*************************************************/
	function GetProductList(GroupID)
	{
		$('#Loading').show();
		$.ajax({ type: "GET", dataType: "JSON", url: "GroupInfo.aspx?Time=" + (new Date().getTime()), data: { Action: 'GetAllProduct', GroupID: GroupID, PageNo: PageNo, PageSize: 10 },
		    success: function (d) {
		        if (d.PageCount > 0 && PageNo <= d.PageCount) {
		            $.each(d.Rows, function (idx, item) {
		                $('<dd><a href="javascript:void();" onclick="GetProductInfo(\'' + item.TypeID + '\')"><img src="' + item.ImagesUrl + '"></a><div style="width:150px;height:25px;line-height:25px;text-align:center;overflow:hidden"><a href="javascript:void();" onclick="GetProductInfo(\'' + item.TypeID + '\')">' + item.FullName + '</a></div></dd>').appendTo($('.ProductList'));

		            });
		            PageCount = d.PageCount;
		            if (PageNo == d.PageCount) {
		                $('#pullUp').css('display', 'none');
		            }
		            PageNo = PageNo + 1;
		            myScroll.refresh();
		        }
		    }
		});
		$('#Loading').hide();
	}
</script>
<script language="javascript" type="text/javascript">
	///*************************************************
    //描述：向下拖动刷新数据
    //*************************************************/
	function pullDownAction () {
		PageNo=1;
		$('.ProductList').empty();
		$('#pullUp').css('display','');
		GetProductList(GroupID);
	}
	
	///*************************************************
    //描述：向上拖动加载数据
    //*************************************************/
	function pullUpAction () {
		GetProductList(GroupID);
	}
	
	///*************************************************
    //描述：加载下来选择样式
    //*************************************************/
	function loaded() {
		pullDownEl = document.getElementById('pullDown');
		pullDownOffset = pullDownEl.offsetHeight;
		pullUpEl = document.getElementById('pullUp');	
		pullUpOffset = pullUpEl.offsetHeight;
		
		myScroll1 = new iScroll('GroupInfo');
		 
		myScroll = new iScroll('GroupProductInfo', {
		    //scrollbarClass: 'myScrollbar',
			useTransition: true,
			topOffset: pullDownOffset,
			onRefresh: function () {
				if (pullDownEl.className.match('loading')) {
					pullDownEl.className = '';
					pullDownEl.querySelector('.pullDownLabel').innerHTML = '松开立即刷新...';
				} else if (pullUpEl.className.match('loading')) {
					pullUpEl.className = '';
					pullUpEl.querySelector('.pullUpLabel').innerHTML = '松开立即刷新...';
				}
			},
			onScrollMove: function () {
				if (this.y > 5 && !pullDownEl.className.match('flip')) {
					pullDownEl.className = 'flip';
					pullDownEl.querySelector('.pullDownLabel').innerHTML = '松开立即刷新...';
					this.minScrollY = 0;
				} else if (this.y < 5 && pullDownEl.className.match('flip')) {
					pullDownEl.className = '';
					pullDownEl.querySelector('.pullDownLabel').innerHTML = '松开立即刷新...';
					this.minScrollY = -pullDownOffset;
				} else if (this.y < (this.maxScrollY - 5) && !pullUpEl.className.match('flip')) {
					pullUpEl.className = 'flip';
					pullUpEl.querySelector('.pullUpLabel').innerHTML = '松开立即刷新...';
					this.maxScrollY = this.maxScrollY;
				} else if (this.y > (this.maxScrollY + 5) && pullUpEl.className.match('flip')) {
					pullUpEl.className = '';
					pullUpEl.querySelector('.pullUpLabel').innerHTML = '松开立即刷新...';
					this.maxScrollY = pullUpOffset;
				}
			},
			onScrollEnd: function () {
				if (pullDownEl.className.match('flip')) {
					pullDownEl.className = 'loading';
					pullDownEl.querySelector('.pullDownLabel').innerHTML = '数据刷新中...';				
					pullDownAction();
				} else if (pullUpEl.className.match('flip')) {
					pullUpEl.className = 'loading';
					pullUpEl.querySelector('.pullUpLabel').innerHTML = '数据更新中...';				
					pullUpAction();
				}
			}
		});
	}
	
    document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false);
    document.addEventListener('DOMContentLoaded', function () { setTimeout(loaded, 500); }, false);
</script>
</body>
</html>