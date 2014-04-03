<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stores.aspx.cs" Inherits="CNVP.Admin.Stores" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<script language="javascript" type="text/javascript" src="../lib/jquery/jquery-1.5.2.min.js"></script>
<script language="javascript" type="text/javascript" src="../lib/ligerUI/js/ligerui.all.js"></script>   
<script language="javascript" type="text/javascript" src="../lib/ligerUI/js/plugins/ligerGrid.js"></script>
<script language="javascript" type="text/javascript" src="../lib/ligerUI/js/plugins/ligerDialog.js"></script>
<script language="javascript" type="text/javascript" src="../lib/js/common.js"></script>   
<script language="javascript" type="text/javascript" src="../lib/js/LG.js"></script>
<script language="javascript" type="text/javascript" src="../lib/js/ligerui.expand.js"></script>  
<script language="javascript" type="text/javascript" src="../lib/json2.js"></script>
<script language="javascript" type="text/javascript" src="../lib/ligerUI/js/plugins/ligerTextBox.js"></script> 
<script language="javascript" type="text/javascript" src="../lib/js/ligerui.expand.js"></script>
<script language="javascript" type="text/javascript" src="../lib/jquery-validation/jquery.validate.min.js"></script> 
<script language="javascript" type="text/javascript" src="../lib/jquery-validation/jquery.metadata.js"></script>
<script language="javascript" type="text/javascript" src="../lib/jquery-validation/messages_cn.js"></script>
<script language="javascript" type="text/javascript" src="../lib/jquery.form.js"></script>
<script language="javascript" type="text/javascript" src="../lib/js/iconselector.js"></script> 
<link rel="stylesheet" type="text/css" href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css"/>
<link rel="stylesheet" type="text/css" href="../lib/ligerUI/skins/Gray/css/all.css"/>
<link rel="stylesheet" type="text/css" href="../lib/css/common.css"/> 
<style type="text/css">
.l-panel td.l-grid-row-cell-editing { padding-bottom: 2px;padding-top: 2px;}
</style>
</head>
<body style="padding:2px;height:100%; text-align:center; overflow:hidden" scroll="no">
<div id="maingrid" style="margin:2px;"></div>
<script language="javascript" type="text/javascript">
    var StoreID = "0";
    var toolbarOptions = {
        items: [
            { text: '增加', click: itemclick, img: "../lib/icons/silkicons/add.png" },
            { line: true },
            { text: '修改', click: itemclick, img: "../lib/icons/miniicons/page_edit.gif" },
            { line: true },
            { text: '删除', click: itemclick, img: "../lib/icons/miniicons/page_delete.gif" }
        ]
    };

    var grid = $("#maingrid").ligerGrid({
        columns: [
                { display: '门店序号', name: 'AppID', width: 65, minWidth: 65},
                { display: '门店名称', name: 'AppName', width: 260, minWidth: 260, align: 'left' },
                { display: '联系电话', name: 'AppTelPhone', width: 150, minWidth: 100, align: 'left' },
                { display: '售后电话', name: 'AppServiceTelPhone', width: 150, minWidth: 100, align: 'left' },
                { display: '救援电话', name: 'AppSosTelPhone', width: 150, minWidth: 100, align: 'left' },
                { display: '门店状态', name: 'IsLock', width: 80, minWidth: 80,
                    render: function (item) {
                        if (item.IsLock == 1) {
                            return "<a href='javascript:f_islock(0);'><img src='../lib/icons/silkicons/control_play_blue.png' title='启用状态'></a>";
                        }
                        else {
                            return "<a href='javascript:f_islock(1);'><img src='../lib/icons/silkicons/control_stop_blue.png' title='停用状态'>";
                        }
                    }
                },
                { display: '创建时间', name: 'PostTime', type: 'date', format: 'yyyy年MM月dd', width: 100, minWidth: 100 },
                { display: '配置文件', name: 'PostTime', width: 85, minWidth: 85,
                    render: function (item) {
                        return "<div style='width:100%;height:100%;text-align:center'><a href='StoresDown.aspx?AppID=" + item.AppID + "'>配置下载</a></div>";
                    }
                }
                ], dataAction: 'server', pageSize: 30, toolbar: toolbarOptions, sortName: 'AppID', width: '100%', height: '100%', heightDiff: -5, usePager: false, isScroll: true, fixedCellHeight: true, rowHeight: 25, checkbox: true, onDblClickRow: f_onDblClickRow
    });

    grid.set('url', 'Stores.aspx?Action=GetAppList');

    function itemclick(item) {
        var selected = grid.getSelected();
        var rows = grid.getCheckedRows();
        switch (item.text) {
            case "增加":
                top.f_addTab('StoresAdd', '增加门店', 'System/StoresAdd.aspx');
                break;
            case "修改":
                if (selected) {
                    top.f_addTab(null, '修改门店', 'System/StoresEdit.aspx?StoreID=' + selected.AppID);
                }
                else {
                    alert("请至少选择一条记录再执行本操作！");
                }
                break;
            case "删除":
                if (selected) {
                    if (window.confirm('你确定需要删除选择的数据吗?')) {
                        f_delete();
                    }
                }
                else {
                    alert("请至少选择一条记录再执行本操作！");
                }
                break;
        }
    }

    function f_onDblClickRow(data, rowid, rowdata) {
        top.f_addTab(null, '修改门店', 'System/StoresEdit.aspx?StoreID=' + data.AppID);
    }

    function f_islock(IsLock) {
        var rows = grid.getCheckedRows();
        var ID = "";
        $(rows).each(function () {
            ID += this.AppID + ",";
        });
        if (ID != "") {
            ID = ID.substring(0, ID.length - 1);
        }

        var params = {
            AppID: ID,
            IsLock: IsLock
        };
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "Stores.aspx?Action=AppIsLock&Time=" + (new Date().getTime()),
            data: params,
            error: function () {
                alert("系统通讯异常，请与管理员联系。");
            },
            success: function (d) {
                grid.loadData();
            }
        });
    }

    function f_delete() {
        var rows = grid.getCheckedRows();
        var ID = "";
        $(rows).each(function () {
            ID += this.AppID + ",";
        });
        if (ID != "") {
            ID = ID.substring(0, ID.length - 1);
        }

        var params = {
            AppID: ID
        };
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "Stores.aspx?Action=DelAppInfo&Time=" + (new Date().getTime()),
            data: params,
            error: function () {
                alert("系统通讯异常，请与管理员联系。");
            },
            success: function (d) {
                grid.loadData();
            }
        });
    }
</script>
</body>
</html>