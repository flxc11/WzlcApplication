<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarList.aspx.cs" Inherits="CNVP.Client.CarList" %>
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
        <dd><a href="GroupList.aspx" class="tc">套餐</a></dd>
        <dd class="current"><a href="CarList.aspx" class="cx">车型</a></dd>
        <dd><a href="TypeList.aspx" class="jp">精品</a></dd>
        <dd><a href="ProductList.aspx" class="tyj">通用件</a></dd>
        <dd><a href="CartList.aspx" class="gwc">购物车</a></dd>
    </dl>
    </div>
    <div class="bottonlogo"><a href="http://www.hongxu.cn" target="_blank"><img src="Images/BottomLogo.png" /></a></div>
</td>
<td id="Part02" valign="top">
    <div id="CarList">
        <div class="Scroller">
        <dl class="Content"></dl>
        </div>
    </div>
</td>
<td id="Part03" valign="top">
<table style="width:100%;padding:0px 15px" border="0" cellspacing="0" cellpadding="0">
<tr>
<td style="background: url(Images/ProductLine.png) bottom repeat-x;"><div class="CarTitle">所有车型精品</div></td>
</tr>
<tr>
<td valign="top">
<div id="CarProductType">
    <div class="Scroller">
    <dl class="ProductTypeList"></dl>
    </div>
</div>
</td>
</tr>
<tr>
<td>
<div id="CarProductInfo">
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
</td>
</tr>
</table>
</td>
</tr>
</table>
<div id="ProductInfo"></div>
<div id="Loading"></div>
<script language="javascript" type="text/javascript">
    var PageNo = 1, PageSize = 20, SelectCarID = 0, SelectTypeID;
    var myScroll, myScroll1, myScroll2, pullDownEl, pullDownOffset, pullUpEl, pullUpOffset, generatedCount = 0;

    window.onload = function () {
        $("#Loading").show();
        PageReset();
        //加载车型列表
        GetCarList();
        //加载精品列表
        GetProductList(0, '');
        //读取购物信息
        GetCartInfo();

        $('#CarProductType').css('display', 'none');
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
	    $('#Part').css('height', (height - 100) + 'px');
	    $('#Part03').css('width', (width - 280) + 'px');
	    $('#CarProductInfo').css('width', (width - 295) + 'px');

	    $('#ProductInfo').css('height', (height - 95) + 'px');
	    $('#ProductInfo').css('width', (width - 60) + 'px');  
	}

	///*************************************************
	//描述：获取门店车型列表
	//*************************************************/
	function GetCarList() {
	    $.ajax({ type: "GET", dataType: "JSON", url: "CarList.aspx?Time=" + (new Date().getTime()), data: { Action: 'GetAllCarType' },
	        success: function (d) {
	            $('<dt class="Title"></dt>').appendTo($('.Content'));
	            $.each(d.Rows, function (idx, item) {
	                $('<dd id="Car_' + item.CarID + '" onclick="GetCarProduct(' + item.CarID + ',\'' + item.CarName + '\')" class="List"><table style="width:200px;margin:0px auto" border="0" cellspacing="0" cellpadding="0"><tbody><tr><td style="width:80px;height:50px"><img src="'+ item.CarImages +'" height="50" style="border:solid 1px #ccc"></td><td style="width:120px;height:30px;line-height:150%;font-size:14px;padding:0px 5px;" valign="top">' + item.CarName + '</td></tr></tbody></table></dd><dt style="height:2px;"><img src="Images/TypeBottom.png" style="width:100%;height:2px"></dt>').appendTo($('.Content'));
	            });
	        }
	    });
	    $("#Loading").hide();
	}

	///*************************************************
	//描述：获取车型精品信息
	//*************************************************/
	function GetCarProduct(CarID, CarName) {
	    $("#Loading").show();
	    $('#Car_' + CarID).addClass("current").siblings().removeClass("current");
	    $('.CarTitle').html(CarName + '精品');

	    SelectCarID = CarID;

	    //获取车型精品类型
	    GetProductType(CarID);

	    //获取车型精品列表
	    //PageNo = 1;
	    //$('.ProductList').empty();
	    //GetProductList(CarID, '');	    
	}

	///*************************************************
	//描述：获取车型精品类型
	//*************************************************/
	function GetProductType(CarID) {
	    PageNo = 1;
	    SelectCarID = CarID;

	    $('#CarProductType').css('display', '');
	    $('#CarProductInfo').css('display', 'none');
	    $('.ProductTypeList').empty();

	    $.ajax({ type: "GET", dataType: "JSON", url: "CarList.aspx?Time=" + (new Date().getTime()), data: { Action: 'GetProductType', CarID: CarID },
	        success: function (d) {
	            if (d.RecordCount > 0) {
	                $('.ProductTypeList').empty();

	                $.each(d.Rows, function (idx, item) {
	                    var TypeID = item.TypeID;
	                    switch (TypeID.length) {
	                        case 10:
	                            $('<dt><a href="javascript:void();" onclick="GetProductList(' + CarID + ',\'' + TypeID + '\');">' + item.FullName + '</a></dt>').appendTo($('.ProductTypeList'));
	                            break;
	                        case 15:
	                            $('<dd><a href="javascript:void();" onclick="GetProductList('+CarID+',\'' + TypeID + '\');">' + item.FullName + '</a></dd>').appendTo($('.ProductTypeList'));
	                            break;
	                        default:
	                            break;
	                    }
	                });
	            }
	            $("#Loading").hide();
	            myScroll2.refresh();
	        }
	    });
	}

	///*************************************************
	//描述：获取车型精品列表
	//*************************************************/
	function GetProductList(CarID, TypeID) {
	    $('#CarProductType').css('display', 'none');
	    $('#CarProductInfo').css('display', '');
	    SelectCarID = CarID;
	    SelectTypeID = TypeID;

	    $.ajax({ type: "GET", dataType: "JSON", url: "CarList.aspx?Time=" + (new Date().getTime()), data: { Action: 'GetProductList', CarID: SelectCarID, TypeID: SelectTypeID, PageSize: PageSize, PageNo: PageNo },
	        success: function (d) {
	            if (d.PageCount > 0 && PageNo <= d.PageCount) {
	                $('.ProductList').empty();
	                $.each(d.Rows, function (idx, item) {
	                    $('<dd><table style="width:340px;margin:0px auto" border="0" cellspacing="0" cellpadding="0"><tbody><tr><td style="width:160px;height:50px"><a href="javascript:void();" onclick="GetProductInfo(\'' + item.TypeID + '\')"><img src="' + item.ImagesUrl + '" style="border:solid 1px #ccc;width:160px;height:120px;"></a></td><td style="width:180px;height:30px;line-height:150%;font-size:12px;padding:0px 5px;" valign="top">名称：<a href="javascript:void();" onclick="GetProductInfo(\'' + item.TypeID + '\')">' + item.FullName + '</a><br/>品牌：' + item.BrandName + '<br/>条码：' + item.EntryCode + '<br/>拼音码：' + item.PyCode + '</td></tr></tbody></table></dd>').appendTo($('.ProductList'));
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
	    $("#Loading").hide();
	}
</script>
<script language="javascript" type="text/javascript">
	///*************************************************
    //描述：向下拖动刷新数据
    //*************************************************/
    function pullDownAction() {
        PageNo = 1;
        $('.ProductList').empty();
        GetProductList(SelectCarID, SelectTypeID);
    }
    ///*************************************************
    //描述：向上拖动加载数据
    //*************************************************/
    function pullUpAction() {
        GetProductList(SelectCarID, SelectTypeID);
    }

    ///*************************************************
    //描述：加载下来选择样式
    //*************************************************/
    function loaded() {
        pullDownEl = document.getElementById('pullDown');
        pullDownOffset = pullDownEl.offsetHeight;
        pullUpEl = document.getElementById('pullUp');
        pullUpOffset = pullUpEl.offsetHeight;

        myScroll1 = new iScroll('CarList');
        myScroll2 = new iScroll('CarProductType'); 
        myScroll = new iScroll('CarProductInfo', {
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