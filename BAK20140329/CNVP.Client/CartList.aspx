<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CartList.aspx.cs" Inherits="CNVP.Client.CartList" %>
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
        <dd><a href="ProductList.aspx" class="tyj">通用件</a></dd>
        <dd class="current"><a href="CartList.aspx" class="gwc">购物车</a></dd>
    </dl>
    </div>
    <div class="bottonlogo"><a href="http://www.hongxu.cn" target="_blank"><img src="Images/BottomLogo.png" /></a></div>
</td>
<td id="Part03" valign="top">
<div id="CartList">
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
    myScroll = new iScroll('CartList');
}

document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false);
document.addEventListener('DOMContentLoaded', function () { setTimeout(loaded, 1000); }, false);

window.onload = function () {
    $("#Loading").show();
    PageReset();

    //获取购物清单
    ShowCart();
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
    $('#CartList').css('width', (width - 60) + 'px'); 
}

///*************************************************
//描述：获取用户购物清单
//*************************************************/
function ShowCart() {
    $(".Content").empty();

    $.ajax({ type: "GET", dataType: "JSON", url: "CartList.aspx?Time=" + (new Date().getTime()), data: { Action: 'ShowCart' },
        success: function (d) {
            var HTML = '';
            HTML += '<table border="0" cellspacing="0" cellpadding="0" style="width:100%;position:relative;top:-5px">';
            HTML += '<tr style="background:url(Images/CartHeaderBg.png) #f7f7f7 repeat-x;">';
            HTML += '<td style="width:100px;color:#ffffff;font-size:16px;text-indent:15px;height:50px;line-height:50px" colspan="6">您的购物清单</td>';
            HTML += '</tr>';
            HTML += '<tr style="background:#f7f7f7;height:50px;border-bottom:solid 1px #c2c2c2">';
            HTML += '<td style="width:10%;border-bottom:solid 1px #c2c2c2"></td>';
            HTML += '<td style="width:25%;border-bottom:solid 1px #c2c2c2;font-size:14px;font-weight:bolder">名称</td>';
            HTML += '<td style="width:20%;border-bottom:solid 1px #c2c2c2;text-indent:12px;font-size:14px;font-weight:bolder">单价</td>';
            HTML += '<td style="width:20%;border-bottom:solid 1px #c2c2c2;text-indent:30px;font-size:14px;font-weight:bolder">数量</td>';
            HTML += '<td style="width:20%;border-bottom:solid 1px #c2c2c2;text-indent:12px;font-size:14px;font-weight:bolder">小计</td>';
            HTML += '<td style="width:5%;border-bottom:solid 1px #c2c2c2;font-size:14px;font-weight:bolder">操作</td>';
            HTML += '</tr>';

            var i = 0;
            var TotalAmount = 0;
            $.each(d, function (idx, item) {
                var SubTotal = item.ProductAmount * item.ProductPrice;
                TotalAmount += SubTotal;
                if (i % 2 == 0) {
                    HTML += '<tr style="background:#e7f9ff;">';
                }
                else {
                    HTML += '<tr style="background:#fef3d1;">';
                }
                HTML += '<td height="25" style="text-align:center;height:60px;border-bottom:solid 1px #c2c2c2;padding:5px 0px" valign="top"><img src="' + item.ProductImages + '" style="width:80px"/></td>';
                HTML += '<td height="25" style="border-bottom:solid 1px #c2c2c2;padding:5px 5px 5px 0px;">';
                if (item.IsGroup == "True") {
                    HTML += '<div style="width:100%;float:left;padding:5px 0px"><a href="javascript:void()" onclick="" style="font-size:14px;">' + item.ProductName + '</a></div>';

                    //获取精品列表
                    HTML += '<div style="float:left;width:40px;overflow:hidden;margin:2px;border:solid 1px #ccc;padding:1px"><img src="' + item.ProductImages + '" style="width:40px"/></div><div style="float:left;width:40px;overflow:hidden;margin:2px;border:solid 1px #ccc;padding:1px"><img src="' + item.ProductImages + '" style="width:40px"/></div><div style="float:left;width:40px;overflow:hidden;margin:2px;border:solid 1px #ccc;padding:1px"><img src="' + item.ProductImages + '" style="width:40px"/></div><div style="float:left;width:40px;overflow:hidden;margin:2px;border:solid 1px #ccc;padding:1px"><img src="' + item.ProductImages + '" style="width:40px"/></div><div style="float:left;width:40px;overflow:hidden;margin:2px;border:solid 1px #ccc;padding:1px"><img src="' + item.ProductImages + '" style="width:40px"/></div><div style="float:left;width:40px;overflow:hidden;margin:2px;border:solid 1px #ccc;padding:1px"><img src="' + item.ProductImages + '" style="width:40px"/></div><div style="float:left;width:40px;overflow:hidden;margin:2px;border:solid 1px #ccc;padding:1px"><img src="' + item.ProductImages + '" style="width:40px"/></div><div style="float:left;width:40px;overflow:hidden;margin:2px;border:solid 1px #ccc;padding:1px"><img src="' + item.ProductImages + '" style="width:40px"/></div><div style="float:left;width:40px;overflow:hidden;margin:2px;border:solid 1px #ccc;padding:1px"><img src="' + item.ProductImages + '" style="width:40px"/></div><div style="float:left;width:40px;overflow:hidden;margin:2px;border:solid 1px #ccc;padding:1px"><img src="' + item.ProductImages + '" style="width:40px"/></div><div style="float:left;width:40px;overflow:hidden;margin:2px;border:solid 1px #ccc;padding:1px"><img src="' + item.ProductImages + '" style="width:40px"/></div>';
                }
                else {
                    HTML += '<a href="javascript:void()" onclick="">' + item.ProductName + '</a>';
                }
                HTML += '</td>';
                HTML += '<td style="border-bottom:solid 1px #c2c2c2">￥<span>' + item.ProductPrice + '</span>元</td>';
                HTML += '<td style="border-bottom:solid 1px #c2c2c2"><a href="javascript:void();" onclick="AddProductNum(\'' + item.ProductID + '\')"><img src="Images/Addition.png" align="absmiddle"/></a>&nbsp;&nbsp;<input type="text" id="' + item.ProductID + '" name="' + item.ProductID + '" style="width:35px;text-align:center;height:20px;line-height:20px;border:solid 1px #C7C7C7;background:#F2F2F2" value="' + item.ProductAmount + '" readonly="readonly"/>&nbsp;&nbsp;<a href="javascript:void();" onclick="DecProductNum(\'' + item.ProductID + '\');"><img src="Images/Decrease.png" align="absmiddle"/></a></td>';
                HTML += '<td style="border-bottom:solid 1px #c2c2c2">￥' + changeTwoDecimal(SubTotal) + '元</td>';
                HTML += '<td style="border-bottom:solid 1px #c2c2c2"><a href="javascript:void();" onclick="DelCartItem(\'' + item.ProductID + '\');">删除</a></td>';
                HTML += '</tr>';

                i++;
            });
            HTML += '<tr>';
            HTML += '<td colspan="6" style="color:#535353;font-size:16px;height:50px;text-indent:15px;line-height:50px;"><span style="float:right">商品总价：￥<span style="color:#d02652" class="ProductTotal">' + changeTwoDecimal(TotalAmount) + '</span>&nbsp;元&nbsp;&nbsp;</span></td>';
            HTML += '</tr>';
            HTML += '<tr>';
            HTML += '<td colspan="6">';
            //加载顾客信息内容
            HTML += '<table border="0" cellspacing="0" cellpadding="0" style="width:95%;margin:0px auto;">';
            HTML += '<tr>';
            HTML += '<td colspan="4" style="border-bottom:solid 1px #c2c2c2"><span style="border:solid 1px #c2c2c2;border-bottom:0px;height:35px;line-height:35px;display:block;width:85px;text-align:center;font-size:14px;"><b>顾客档案</b></span>';
            HTML += '<td>';
            HTML += '</tr>';
            HTML += '<tr>';
            HTML += '<td colspan="4" style="height:15px;"><td>';
            HTML += '</tr>';
            HTML += '<tr>';
            HTML += '<td style="width:10%;height:50px;line-height:50px;font-size:14px;">客户名称：</td>';
            HTML += '<td style="width:40%;font-size:14px;"><input type="text" id="TxtCusName" name="TxtCusName" style="border:solid 1px #bebebe;width:85%;height:28px;line-height:28px;text-indent:5px;font-size:14px;" /></td>';
            HTML += '<td style="width:15%;font-size:14px;">联系电话：</td>';
            HTML += '<td style="width:35%;font-size:14px;"><input type="text" id="TxtTelPhone" name="TxtTelPhone" style="border:solid 1px #bebebe;width:85%;height:28px;line-height:28px;text-indent:5px;font-size:14px;" /></td>';
            HTML += '</tr>';
            HTML += '<tr>';
            HTML += '<td style="height:50px;line-height:50px;font-size:14px;">通讯地址：</td>';
            HTML += '<td><input type="text" style="border:solid 1px #bebebe;width:85%;height:28px;line-height:28px;text-indent:5px;font-size:14px;" /></td>';
            HTML += '<td>车架号码：</td>';
            HTML += '<td><input type="text" style="border:solid 1px #bebebe;width:85%;height:28px;line-height:28px;text-indent:5px;font-size:14px;" /></td>';
            HTML += '</tr>';
            HTML += '<tr>';
            HTML += '<td style="height:50px;line-height:50px;font-size:14px;">销售姓名：</td>';
            HTML += '<td><input type="text" style="border:solid 1px #bebebe;width:85%;height:28px;line-height:28px;text-indent:5px;font-size:14px;" /></td>';
            HTML += '<td>销售电话：</td>';
            HTML += '<td><input type="text" style="border:solid 1px #bebebe;width:85%;height:28px;line-height:28px;text-indent:5px;font-size:14px;" /></td>';
            HTML += '</tr>';
            HTML += '<tr>';
            HTML += '<td style="height:50px;line-height:50px;font-size:14px;"></td>';
            HTML += '<td><img src="Images/OrderBtn.png"/></td>';
            HTML += '<td></td>';
            HTML += '<td></td>';
            HTML += '</tr>';
            HTML += '<tr>';
            HTML += '<td colspan="4" style="height:15px;"><td>';
            HTML += '</tr>';
            HTML += '</table>';
            //顾客信息内容结束
            HTML += '</td>';
            HTML += '</tr>';
            HTML += '</table>';
            $(HTML).appendTo($(".Content"));
        }
    });    
    
    $("#Loading").hide();
}
///*************************************************
//描述：增加精品购买数量
//*************************************************/
function AddProductNum(ProductID) {
    $("#Loading").show();
    if (Number($("#" + ProductID).val()) < 100) {
        $("#" + ProductID).val(Number($("#" + ProductID).val()) + 1);
        var ProductNum = $("#" + ProductID).val();
        $.ajax({ type: "GET", dataType: "JSON", url: "CartList.aspx?Time=" + (new Date().getTime()), data: { Action: 'Update', ProductID: ProductID, ProductNum: ProductNum }, success: function (d) { EditOrderInfo(ProductID, ProductNum, "Add"); }
        });
    }
    else {
        alert("单件精品购买数量超出最大数量限制(最多100件)。");
        $("#Loading").hide(); 
    }
}
///*************************************************
//描述：减少精品购买数量
//*************************************************/
function DecProductNum(ProductID) {
    $("#Loading").show();
    if (Number($("#" + ProductID).val()) > 1) {
        $("#" + ProductID).val(Number($("#" + ProductID).val()) - 1);
        var ProductPrice = $("#" + ProductID).parent().siblings("td:eq(2)").find("span").html();
        var TotalAmount = $(".ProductTotal").html();
        var ProductNum = $("#" + ProductID).val();
        $.ajax({ type: "GET", dataType: "JSON", url: "CartList.aspx?Time=" + (new Date().getTime()), data: { Action: 'Update', ProductID: ProductID, ProductNum: ProductNum }, success: function (d) { EditOrderInfo(ProductID, ProductNum, "Dec"); }
        });
    }
    else {
        alert("选购精品数量不能少于1件，如果不需要该精品，请点击删除操作。");
        $("#Loading").hide(); 
    }
}
///*************************************************
//描述：更新购物车总金额
//*************************************************/
function EditOrderInfo(ProductID,ProductNum,Action) {
    var TotalAmount = $(".ProductTotal").html();
    var ProductPrice = $("#" + ProductID).parent().siblings("td:eq(2)").find("span").html();
    var SubTotal = ProductPrice * ProductNum;

    switch (Action) {
        case "Add":
            TotalAmount = (parseFloat(TotalAmount) + parseFloat(ProductPrice)).toFixed(2);
            break;
        case "Dec":
            TotalAmount = (parseFloat(TotalAmount) - parseFloat(ProductPrice)).toFixed(2);
            break;
    }

    $("#" + ProductID).parent().siblings("td:eq(3)").html("￥" + changeTwoDecimal(SubTotal) + "元");
    $(".ProductTotal").html(changeTwoDecimal(TotalAmount));

    $("#Loading").hide(); 
}
///*************************************************
//描述：删除选择精品项目
//*************************************************/
function DelCartItem(ProductID) {
    $("#Loading").show();
    $.ajax({ type: "GET", dataType: "JSON", url: "CartList.aspx?Time=" + (new Date().getTime()), data: { Action: 'Delete', ProductID: ProductID },
        success: function (d) {
            ShowCart();
            $("#Loading").hide();
        }
    });
}

///*************************************************
//描述：保留两位小数点
//*************************************************/  
function changeTwoDecimal(x)  {    
    var f_x = parseFloat(x);
    if (isNaN(f_x)){
        alert('不是一个有效的数字!~'); 
        return false;
    }    
    var f_x = Math.round(x*100)/100;
    return changeTwoDecimal_f(f_x);
};
///*************************************************
//描述：保留两位小数点
//*************************************************/
function changeTwoDecimal_f(x)  {
    var f_x = parseFloat(x);  
    if (isNaN(f_x))  {
        alert('不是一个有效的数字'); 
        return false;
    }
    var f_x = Math.round(x*100)/100;
    var s_x = f_x.toString();
    var pos_decimal = s_x.indexOf('.');
    if (pos_decimal < 0) {
        pos_decimal = s_x.length;
        s_x += '.';
    }
    while (s_x.length <= pos_decimal + 2)  {    
        s_x += '0';
    }
    return s_x;
}
</script>
</body>
</html>