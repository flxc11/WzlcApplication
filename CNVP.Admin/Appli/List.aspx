<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="CNVP.Admin.Appli.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<script type="text/javascript" src="../lib/jquery/jquery-1.5.2.min.js"></script>
<script type="text/javascript" src="../lib/ligerUI/js/ligerui.all.js"></script>
<script type="text/javascript" src="../lib/ligerUI/js/plugins/ligerGrid.js"></script>
<script type="text/javascript" src="../lib/ligerUI/js/plugins/ligerResizable.js"></script>
<script type="text/javascript" src="../lib/ligerUI/js/plugins/ligerDialog.js"></script>
<script type="text/javascript" src="../lib/js/common.js"></script>   
<script type="text/javascript" src="../lib/js/LG.js"></script>
<script type="text/javascript" src="../lib/js/thickbox.js"></script>
<script type="text/javascript" src="../lib/js/option.js"></script>
<script type="text/javascript" src="../lib/js/ligerui.expand.js"></script>
<script type="text/javascript" src="../lib/json2.js"></script>
<script type="text/javascript" src="../lib/ligerUI/js/plugins/ligerTextBox.js"></script> 
<script type="text/javascript" src="../lib/js/ligerui.expand.js"></script>
<script type="text/javascript" src="../lib/jquery-validation/jquery.validate.min.js"></script> 
<script type="text/javascript" src="../lib/jquery-validation/jquery.metadata.js"></script>
<script type="text/javascript" src="../lib/jquery-validation/messages_cn.js"></script>
<script type="text/javascript" src="../lib/jquery.form.js"></script>
<script type="text/javascript" src="../lib/js/iconselector.js"></script> 
<link rel="stylesheet" type="text/css" href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css"/>
<link rel="stylesheet" type="text/css" href="../lib/ligerUI/skins/Gray/css/all.css"/>
<link rel="stylesheet" type="text/css" href="../lib/css/common.css"/>
<link rel="stylesheet" type="text/css" href="../lib/css/thickbox.css"/>
<link rel="stylesheet" type="text/css" href="../lib/css/option.css"/> 
</head>
<body style="padding:0;height:100%; text-align:center; overflow:hidden" scroll="no">
<div id="layout" style="overflow:hidden">
<div position="center" title="申请列表" id="childmenu"> 
    <div id="maingrid" style="margin:2px;"></div> 
</div>
</div>
<script type="text/javascript">
    var layout = $("#layout").ligerLayout({ leftWidth: 220, allowLeftCollapse: false });
    $(".l-layout-left").css("overflow", "auto");
    var toolbarOptions = {
        items: [
                //{ text: '查找', click: itemclick, img: "../lib/icons/silkicons/briefcase.png" },
                //{ line: true },
                { text: '移除', click: itemclick, img: "../lib/icons/miniicons/icon_monitor_pc.gif" }
        ]
    };
    var grid = $("#maingrid").ligerGrid({
        columns: [
                { display: '序号', name: 'ID', align: 'center', width: 65, minWidth: 65 },
                { display: '申请人', name: 'AppName', align: 'center', width: 150, minWidth: 60 },
                {
                    display: '申请类型', name: 'AppType', align: 'center', width: 100, minWidth: 60,
                    render: function (item) {
                        if (item.AppType === "1") {
                            return "企业";
                        }
                        else {
                            return "个人";
                        }
                    }
                },
                { display: '申请事项', name: 'AppThings', align: 'center', width: 100, minWidth: 60 },
                { display: '申请时间', name: 'PostTime', align: 'center', width: 150, minWidth: 60 },
                {
                    display: '申请状态', name: 'IsAudit', align: 'center', width: 60, minWidth: 60,
                    render: function (item) {
                        if (item.IsAudit === "True") {
                            return "<a href='javascript:f_islock(0)'><img src='../lib/icons/silkicons/accept.png' title='已审核'></a>";
                        }
                        else {
                            return "<a href='javascript:f_islock(1)'><img src='../lib/icons/silkicons/cancel.png' title='未审核'></a>";
                        }
                    }
                },
                { display: '管理员回复', name: 'AppReply', align: 'center', width: 150, minWidth: 60 }

        ], dataAction: 'server', pageSize: 20, toolbar: toolbarOptions, sortName: 'ID',
        width: '100%', height: '100%', heightDiff: -5, url: 'List.aspx?Action=AppList', checkbox: true, usePager: true,whenRClickToSelect: true, fixedCellHeight: true, rowHeight: 25,
        onDblClickRow: f_onDblClickRow
    });

    function itemclick(item) {
        var rows = grid.getCheckedRows();
        var selected = grid.getSelected();
        switch (item.text) {
            case "查找":
                showTB('Product/ProductListSearch.aspx?Type=' + action, 300, 120, '查找精品', 'parent');
                break;
            case "移除":
                if (selected != null) {
                    if (window.confirm('你确定需要执行精品移除的操作吗？')) {
                        f_delete();
                    }
                }
                else {
                    alert("你至少需要选择一个精品才能执行移除操作。");
                }
                break;
        }
    }
    function showpic() {
        var x = 10;
        var y = 20;
        
        $(".a_front").mouseenter(function (e) {
            var tooltip = '<div id="tooltip"><img src="' + this.href + '" width="300" height="180" /></div>';
            $("body").append(tooltip);
            var _x = $(window).width() - e.pageX;
            var _y = $(window).height() - e.pageY;
            if (_x < 300 && _y > 180) {
                $("#tooltip")
                .css({
                    "top": (e.pageY + y) + "px",
                    "right": (_x) + "px"
                }).show("fast");
            }
            else if (_x < 300 && _y < 180) {
                $("#tooltip")
                .css({
                    "bottom": (_y + y) + "px",
                    "right": (_x) + "px"
                }).show("fast");
            }
            else if (_x > 300 && _y < 180) {
                $("#tooltip")
                .css({
                    "bottom": (_y + y) + "px",
                    "right": (e.pageX + x) + "px"
                }).show("fast");
            }
            else {
                $("#tooltip")
                .css({
                    "top": (e.pageY + y) + "px",
                    "left": (e.pageX + x) + "px"
                }).show("fast");
            }
        }).mouseout(function () {
            $("#tooltip").remove();
        }).mousemove(function (e) {
            var _x = $(window).width() - e.pageX;
            var _y = $(window).height() - e.pageY;
            if (_x < 300 && _y > 180) {
                $("#tooltip")
                .css({
                    "top": (e.pageY + y) + "px",
                    "right": (_x) + "px"
                }).show("fast");
            }
            else if (_x < 300 && _y < 180) {
                $("#tooltip")
                .css({
                    "bottom": (_y + y) + "px",
                    "right": (_x) + "px"
                }).show("fast");
            }
            else if (_x > 300 && _y < 180) {
                $("#tooltip")
                .css({
                    "bottom": (_y + y) + "px",
                    "left": (e.pageX + x) + "px"
                }).show("fast");
            }
            else {
                $("#tooltip")
                .css({
                    "top": (e.pageY + y) + "px",
                    "left": (e.pageX + x) + "px"
                }).show("fast");
            }
        })
    }
    function f_delete() {
        var rows = grid.getCheckedRows();
        var ID = "";
        $(rows).each(function () {
            ID += this.ID + ",";
        });
        if (ID != "") {
            ID = ID.substring(0, ID.length - 1);
        }

        var params = {
            ID: ID
        };

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "List.aspx?Action=Del&Time=" + (new Date().getTime()),
            data: params,
            error: function () {
                alert("系统通讯异常，请与管理员联系。");
            },
            success: function (d) {
                grid.loadData();
            }
        });
    }
    function f_islock(IsLock) {
        var rows = grid.getCheckedRows();
        var ID = "";
        $(rows).each(function () {
            ID += this.ID + ",";
        });
        if (ID != "") {
            ID = ID.substring(0, ID.length - 1);
        }

        var params = {
            ID: ID,
            IsLock: IsLock
        };
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "List.aspx?Action=IsLock&Time=" + (new Date().getTime()),
            data: params,
            error: function () {
                alert("系统通讯异常，请与管理员联系。");
            },
            success: function (d) {
                grid.loadData();
            }
        });
    }
    function f_onDblClickRow(data, rowid, rowdata) {
        top.f_addTab(null, '申请详情', 'Appli/AppEdit.aspx?ID=' + data.ID);
    }
  </script>
</body>

</html>
