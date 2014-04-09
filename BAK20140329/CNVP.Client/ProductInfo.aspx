<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductInfo.aspx.cs" Inherits="CNVP.Client.ProductInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>汽车精品展示系统</title>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<script language="javascript" type="text/javascript" src="Js/JQuery.js"></script>
<script language="javascript" type="text/javascript" src="Js/Common.js"></script>
<script language="javascript" type="text/javascript" src="Js/IScroll.js"></script>
<script language="javascript" type="text/javascript" src="Js/MSClass.js"></script>
<link rel="stylesheet" type="text/css" href="Css/Global.css"/>
<link rel="stylesheet" type="text/css" href="Css/Layout.css"/>
<link rel="stylesheet" type="text/css" href="Css/ScrollBar.css"/>
</head>
<body>
<table id="Part" border="0" cellspacing="0" cellpadding="0">
<tr>
<td valign="top">
<table style="width:100%;padding:0px 10px" border="0" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td style="height:35px;font-size:16px;font-weight:bolder;border-bottom:solid 1px #ccc">&nbsp;精品名称：<asp:Literal ID="LitFullName" runat="server"></asp:Literal></td>
<td style="text-align:right;border-bottom:solid 1px #ccc"><a href="javascript:void();" onclick="CloseWindows();"><img src="Images/Close.png" width="18" height="18" /></a></td>
</tr>
<tr>
<td colspan="2" style="padding-top:10px">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<tr>
<td style="width:50%">
<div><a href="javascript:void();" onclick="ProductImages('','GroupInfo')"><img src="Images/NoImages.jpg" style="width:500px" id="FlyToCart"></a></div>
<div style="padding-top:10px"><asp:Literal ID="LitImagesUrl" runat="server"></asp:Literal></div>
<script type="text/javascript">
    var MarqueeBox = new Marquee(
        {
            MSClass: ["Movie_Box", "Scroll_Container"],
            Direction: 4,
            Step: 0.4,
            Width: 480,
            Height: 60,
            Timer: 30,
            DelayTime: 1000,
            WaitTime: 100000,
            ScrollStep: 480,
            SwitchType: 0,
            AutoStart: true
        });
        $$("LeftBtn").onclick = function () { MarqueeBox.Run("Left") };
        $$("RightBtn").onclick = function () { MarqueeBox.Run("Right") };
        $$("RightBtn").className = "next";
        MarqueeBox.OnBound = function () {
        if (MarqueeBox.Bound == 2) {
            $$("LeftBtn").className = "prev";
        }
        else {
            $$("RightBtn").className = "next";
        }
    };
    MarqueeBox.UnBound = function () {
        $$("RightBtn").disabled = $$("LeftBtn").disabled = false;
        $$("RightBtn").className = "next";
        $$("LeftBtn").className = "prev";
    };
</script>
</td>
<td style="width:50%;" valign="top">
<table width="300" border="0" cellspacing="0" cellpadding="0">
<tr>
<td style="width:100px;height:25px;text-align:center">精品价格：</td>
<td style="width:220px;height:25px;color:#F00;font-weight:bolder;font-family:Arial;"><asp:Literal ID="LitGuidePrice" runat="server"></asp:Literal>元</td>
</tr>
<tr>
<td style="width:100px;height:25px;text-align:center">精品分类：</td>
<td><asp:Literal ID="LitTypeName" runat="server"></asp:Literal></td>
</tr>
<tr>
<td style="width:100px;height:25px;text-align:center">精品编号：</td>
<td><asp:Literal ID="LitUserCode" runat="server"></asp:Literal></td>
</tr>
<tr>
<td style="width:100px;height:25px;text-align:center">精品条码：</td>
<td><asp:Literal ID="LitEntryCode" runat="server"></asp:Literal></td>
</tr>
<tr>
<td style="width:100px;height:25px;text-align:center">拼音编码：</td>
<td><asp:Literal ID="LitPyCode" runat="server"></asp:Literal></td>
</tr>
<tr>
<td style="width:100px;height:25px;text-align:center">适用车型：</td>
<td><asp:Literal ID="LitCarName" runat="server"></asp:Literal></td>
</tr>
<tr>
<td style="width:100px;height:25px;text-align:center">所属品牌：</td>
<td><asp:Literal ID="LitBrandName" runat="server"></asp:Literal></td>
</tr>
<tr>
<td colspan="2" style="height:5px;"></td>
</tr>
<tr>
<td style="width:100px;height:25px;text-align:center;vertical-align:top">颜色分类：</td>
<td><asp:Literal ID="LitColorsUrl" runat="server"></asp:Literal></td>
</tr>
<tr>
<td colspan="2" style="height:5px;"></td>
</tr>
<tr>
<td style="width:100px;height:25px;text-align:center;vertical-align:top">款式分类：</td>
<td><asp:Literal ID="LitStyleUrl" runat="server"></asp:Literal></td>
</tr>
<tr>
<td style="width:100px;height:100px;text-align:center;vertical-align:top"></td>
<td><a href="javascript:void();" onclick="AddCart('<%=TypeID %>');"><img src="Images/AddCart.png" /></a></td>
</tr>
</table>
</td>
</tr>
</table>




</td>
</tr>
</tbody>
</table>
</td>
</tr>
</table>
</body>
</html>