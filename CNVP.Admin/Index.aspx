<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CNVP.Admin.Index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title><%=CNVP.Config.UIConfig.Product %> - 管理中心</title>
<script type="text/javascript" language="javascript" src="lib/jquery/jquery-1.5.2.min.js"></script>
<script type="text/javascript" language="javascript" src="lib/ligerUI/js/ligerui.min.js"></script>  
<script type="text/javascript" language="javascript" src="lib/ligerUI/js/plugins/ligerTab.js"></script>
<script type="text/javascript" language="javascript" src="lib/ligerUI/js/plugins/ligerLayout.js"></script>
<script type="text/javascript" language="javascript" src="lib/js/common.js"></script>
<script type="text/javascript" language="javascript" src="lib/js/LG.js"></script>
<script type="text/javascript" language="javascript" src="lib/js/login.js"></script>
<script type="text/javascript" language="javascript" src="lib/js/thickbox.js"></script>
<script type="text/javascript" language="javascript" src="lib/js/option.js"></script>
<script type="text/javascript" language="javascript" src="lib/jquery-validation/jquery.validate.min.js"></script> 
<script type="text/javascript" language="javascript" src="lib/jquery-validation/jquery.metadata.js"></script>
<script type="text/javascript" language="javascript" src="lib/jquery-validation/messages_cn.js"></script>
<script type="text/javascript" language="javascript" src="lib/js/changepassword.js"></script>
<script type="text/javascript" language="javascript" src="lib/ligerUI/js/plugins/ligerForm.js"></script>
<link rel="stylesheet" type="text/css" href="lib/ligerUI/skins/Aqua/css/ligerui-all.css" />
<link rel="stylesheet" type="text/css" href="lib/ligerUI/skins/Gray/css/all.css" />  
<link rel="stylesheet" type="text/css" href="lib/css/common.css" />  
<link rel="stylesheet" type="text/css" href="lib/css/index.css" />
<link rel="stylesheet" type="text/css" href="lib/css/thickbox.css">
<link rel="stylesheet" type="text/css" href="lib/css/option.css">
</head>
<body style="text-align:center;background:#F0F0F0;overflow:hidden;" scroll="no">
<div id="pageloading" style="display:block;"></div> 
    <div id="topmenu" class="l-topmenu">
        <div class="l-topmenu-logo">&nbsp;</div>
        <div class="l-topmenu-welcome"> 
            <span class="l-topmenu-username"></span>欢迎回来，<asp:Literal ID="LitUserName" runat="server"></asp:Literal>&nbsp;[<a href="javascript:showTB('AdminInfo.aspx',350,180,'修改密码','parent');">修改密码</a>]&nbsp;[<a href="LoginOut.aspx">退出登录]</a>
        </div> 
    </div> 
     <div id="mainbody" class="l-mainbody" style="width:99.2%; margin:0 auto; margin-top:3px;" >
        <div position="left" title="管理中心" id="mainmenu"></div>  
        <div position="center" id="framecenter"> 
            <div tabid="home" title="我的主页"> 
                <iframe frameborder="0" name="home" id="home" src="welcome.aspx"></iframe>
            </div> 
        </div> 
    </div>
    <script type="text/javascript">
        //几个布局的对象
        var layout, tab, accordion;
        //tabid计数器，保证tabid不会重复
        var tabidcounter = 0;
        //窗口改变时的处理函数
        function f_heightChanged(options) {
            if (tab)
                tab.addHeight(options.diff);
            if (accordion && options.middleHeight - 24 > 0)
                accordion.setHeight(options.middleHeight - 24);
        }
        //关闭tab项的函数
        function f_closeTab(tabid) {
            tab.removeTabItem(tabid);
        }
        //增加tab项的函数
        function f_addTab(tabid, text, url) {
            if (!tab) return;
            /*if (!tabid) {
                tabidcounter++;
                tabid = "tabid" + tabidcounter;
            }*/
            tab.addTabItem({ tabid: tabid, text: text, url: url });
        }
        //登录
        function f_login()
        {
            LG.login();
        }
        //修改密码
        function f_changepassword()
        {
            LG.changepassword();
        }
        $(document).ready(function () {
            //菜单初始化
            $("ul.menulist li").live('click', function () {
                var jitem = $(this);
                var tabid = jitem.attr("menuno");
                var url = jitem.attr("url");
                if (!url) return;
                if (tabid) {
                    jitem.attr("tabid", tabid);

                    //给url附加menuno
                    if (url.indexOf('?') > -1) url += "&";
                    else url += "?";
                    url += "MenuNo=" + jitem.attr("menuno");
                    jitem.attr("url", url);
                }
                f_addTab(tabid, $("span:first", jitem).html(), url);
            }).live('mouseover', function () {
                var jitem = $(this);
                jitem.addClass("over");
            }).live('mouseout', function () {
                var jitem = $(this);
                jitem.removeClass("over");
            });

            //布局初始化 
            //layout
            layout = $("#mainbody").ligerLayout({ height: '100%', heightDiff: -3, leftWidth: 140, onHeightChanged: f_heightChanged, minLeftWidth: 120, allowLeftCollapse: false });
            var bodyHeight = $(".l-layout-center:first").height();
            //Tab
            tab = $("#framecenter").ligerTab({ height: bodyHeight, contextmenu: true });


            //预加载dialog的背景图片
            LG.prevDialogImage();

            var mainmenu = $("#mainmenu");

            $.getJSON('Index.aspx?Action=GetAllMenu&Time=' + (new Date().getTime()), function (menus) {
                $(menus).each(function (i, menu) {
                    var item = $('<div title="' + menu.MenuName + '"><ul class="menulist"></ul></div>');

                    $(menu.children).each(function (j, submenu) {
                        var subitem = $('<li><img/><span></span><div class="menuitem-l"></div><div class="menuitem-r"></div></li>');
                        subitem.attr({
                            url: submenu.MenuUrl,
                            menuno: submenu.MenuValue
                        });
                        $("img", subitem).attr("src", submenu.MenuIcon);
                        $("span", subitem).html(submenu.MenuName);

                        $("ul:first", item).append(subitem);
                    });
                    mainmenu.append(item);

                });

                //Accordion
                accordion = $("#mainmenu").ligerAccordion({ height: bodyHeight - 24, speed: null });

                $("#pageloading").hide();
            });
        });
        function ResetSite() {
            var params = { Action: "ResetSite" };
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "Index.aspx?Time=" + (new Date().getTime()),
                data: params,
                error: function () {
                    alert("系统通讯异常，请与管理员联系。");
                },
                success: function (d) {
                    if (d.msgCode == 0) {
                        alert(d.msgStr);
                    }
                    else {
                        alert("站点重置操作失败，请重新执行本操作。");
                    }
                }
            });
        }
    </script>
</body>
</html>