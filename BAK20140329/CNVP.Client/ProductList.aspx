<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="CNVP.Client.ProductList" %>
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
        <dd><a href="CarList.aspx" class="cx">车型</a></dd>
        <dd><a href="TypeList.aspx" class="jp">精品</a></dd>
        <dd class="current"><a href="ProductList.aspx" class="tyj">通用件</a></dd>
        <dd><a href="CartList.aspx" class="gwc">购物车</a></dd>
    </dl>
    </div>
    <div class="bottonlogo"><a href="http://www.hongxu.cn" target="_blank"><img src="Images/BottomLogo.png" /></a></div>
</td>
<td id="Part03" valign="top">
<div id="ProductList">
    <div class="Scroller">
        <div>
        <ul><li class="Header"></li></ul>
        <ul><li class="TypeInfo"></li></ul>
        <ul><li class="ProductInfo">&nbsp;精品列表</li></ul>
        <ul>
        <li>
        <div id="pullDown" style="display:none">
        <span class="pullDownIcon"></span><span class="pullDownLabel">松开立即刷新...</span>
        </div>
        <div><dl id="thelist" class="ProductList"></dl></div>
        <div id="pullUp">
    	<span class="pullUpIcon"></span><span class="pullUpLabel">松开立即刷新...</span>
        </div>
        </li></ul>
        </div>
    </div>
</div>
</td>
</tr>
</table>
<div id="ProductInfo"></div>
<div id="Loading"></div>
<script language="javascript" type="text/javascript">
    var PageNo = 1, PageSize = 20, SelectCarID = 0, SelectTypeID = '';
    var myScroll, pullDownEl, pullDownOffset, pullUpEl, pullUpOffset, generatedCount = 0;
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

        myScroll = new iScroll('ProductList', {
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

    window.onload = function () {
        $("#Loading").show();
        PageReset();

        //获取精品类别
        GetProductType(SelectTypeID);
        //获取精品车型
        GetCarList();
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
        $('#Part').css('height', (height - 100) + 'px');
        $('#Part03').css('width', (width - 60) + 'px');
        $('#ProductList').css('width', (width - 70) + 'px');

        $('#ProductInfo').css('height', (height - 95) + 'px');
        $('#ProductInfo').css('width', (width - 60) + 'px');
    }

    ///*************************************************
    //描述：获取当前位置
    //*************************************************/
    function GetLocation(SelectTypeID) {
        $.ajax({ type: "GET", dataType: "HTML", url: "ProductList.aspx?Time=" + (new Date().getTime()), data: { Action: 'GetRootPath', TypeID: SelectTypeID },
            success: function (d) {
                $('.Header').empty();
                $('当前位置：' + d).appendTo($('.Header'));
            }
        });
    }

    ///*************************************************
    //描述：获取精品类别信息
    //*************************************************/
    function GetProductType(TypeID) {
        PageNo = 1;
        SelectTypeID = TypeID;
        $('.TypeInfo').empty();
        $('.ProductList').empty();
        $('#pullUp').css('display', '');
        $("#Loading").show();

        $.ajax({ type: "GET", dataType: "JSON", url: "ProductList.aspx?Time=" + (new Date().getTime()), data: { Action: 'GetProductType', TypeID: TypeID },
            success: function (d) {
                if (d.RecordCount > 0) {
                    $.each(d.Rows, function (idx, item) {
                        var TypeID = item.TypeID;
                        switch (TypeID.length) {
                            case 10:
                                if ($('.TypeInfo').html() != "") {
                                    $('<dt class="Title"></dt><dt><a href="javascript:void()" onclick="GetProductType(\'' + item.TypeID + '\');">' + item.FullName + '</a></dt>').appendTo($('.TypeInfo'));
                                }
                                else {
                                    $('<dt><a href="javascript:void()" onclick="GetProductType(\'' + item.TypeID + '\');">' + item.FullName + '</a></dt>').appendTo($('.TypeInfo'));
                                }
                                break;
                            case 15:
                                $('<dd><a href="javascript:void()" onclick="GetProductType(\'' + item.TypeID + '\');">' + item.FullName + '</a></dd>').appendTo($('.TypeInfo'));
                                break;
                            default:
                                break;
                        }
                    });
                }
                $("#Loading").hide();
                myScroll.refresh();
            }
        });

        //获取当前位置
        GetLocation(SelectTypeID);
        //获取精品列表
        GetProductList(SelectTypeID, SelectCarID);
    }

    ///*************************************************
    //描述：获取精品车型名称
    //*************************************************/
    function GetCarList() {
        PageNo = 1;
        $("#Loading").show();

        $.ajax({ type: "GET", dataType: "JSON", url: "ProductList.aspx?Time=" + (new Date().getTime()), data: { Action: 'GetCarList' },
            success: function (d) {
                $.each(d.Rows, function (idx, item) {
                    $('<a href="javascript:void()" onclick="GetCarClick(' + item.CarID + ')" id="Car_' + item.CarID + '">' + item.CarName + '</a>').appendTo($('.CarInfo'));
                });
            }
        });
    }
    ///*************************************************
    //描述：获取选择车型名称
    //*************************************************/
    function GetCarClick(CarID) {
        PageNo = 1;
        $("#Loading").show();
        SelectCarID = CarID;

        if (($('#Car_' + CarID).hasClass("current"))) {
            SelectCarID = 0;
            $('#Car_' + CarID).removeClass("current");
        }
        else {
            $('.CarInfo a').removeClass("current");
            $('#Car_' + CarID).addClass("current");
        }

        $('.ProductList').empty();
        $('#pullUp').css('display', '');
        //获取门店精品列表
        GetProductList(SelectTypeID, SelectCarID);
    }

    ///*************************************************
    //描述：获取门店精品列表
    //*************************************************/
    function GetProductList(TypeID, CarID) {
        $.ajax({ type: "GET", dataType: "JSON", url: "ProductList.aspx?Time=" + (new Date().getTime()), data: { Action: 'GetTypeProduct', CarID: SelectCarID, TypeID: SelectTypeID, PageSize: PageSize, PageNo: PageNo },
            success: function (d) {
                if (d.PageCount > 0 && PageNo <= d.PageCount) {
                    $.each(d.Rows, function (idx, item) {
                        $('<dd><table style="width:445px;margin:0px auto" border="0" cellspacing="0" cellpadding="0"><tbody><tr><td style="width:200px;"><a href="javascript:void();" onclick="GetProductInfo(\'' + item.TypeID + '\')"><img src="' + item.ImagesUrl + '" style="border:solid 1px #ccc;width:200px;height:150px"></a></td><td style="width:245px;height:30px;line-height:150%;font-size:12px;padding:0px 5px;" valign="top">名称：<a href="javascript:void();" onclick="GetProductInfo(\'' + item.TypeID + '\')">' + item.FullName + '</a><br/>品牌：' + item.BrandName + '<br/>条码：' + item.EntryCode + '<br/>拼音码：' + item.PyCode + '</td></tr></tbody></table></dd>').appendTo($('.ProductList'));
                    });
                    PageCount = d.PageCount;
                    if (PageNo == d.PageCount) {
                        $('#pullUp').css('display', 'none');
                    }
                    PageNo = PageNo + 1;
                    myScroll.refresh();
                }
                else {
                    $('#pullUp').css('display', 'none');
                }
            }
        });

        $("#Loading").hide();
    }
</script>
</body>
</html>