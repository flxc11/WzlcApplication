<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TypeList.aspx.cs" Inherits="CNVP.Admin.Appli.TypeList" %>

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
<div position="center" title="申请事项" id="childmenu"> 
    <div id="maingrid" style="margin:2px;"></div> 
</div>
</div>
<script type="text/javascript">
    var layout = $("#layout").ligerLayout({ leftWidth: 220, allowLeftCollapse: false });
    $(".l-layout-left").css("overflow", "auto");
    var toolbarOptions = {
        items: [
                { text: '增加', click: itemclick, img: "../lib/icons/silkicons/add.png" },
                { line: true },
                { text: '移除', click: itemclick, img: "../lib/icons/miniicons/icon_monitor_pc.gif" }
        ]
    };
    var grid = $("#maingrid").ligerGrid({
        columns: [
                { display: '序号', name: 'ID', align: 'center', width: 65, minWidth: 65 },
                { display: '事项名称', name: 'TypeName', align: 'center', width: 150, minWidth: 60 },
                { display: '事项说明', name: 'TypeContent', align: 'center', width: 500, minWidth: 500 }
        ], dataAction: 'server', pageSize: 20, toolbar: toolbarOptions, sortName: 'ID',
        width: '100%', height: '100%', heightDiff: -5, url: 'TypeList.aspx?Action=List', checkbox: true, usePager: true, whenRClickToSelect: true, fixedCellHeight: true, rowHeight: 25,
        onDblClickRow: f_onDblClickRow
    });

    function itemclick(item) {
        var rows = grid.getCheckedRows();
        var selected = grid.getSelected();
        switch (item.text) {
            case "增加":
                top.f_addTab('TypeAdd', '增加事项', 'Appli/TypeAdd.aspx');
                break;
            case "移除":
                if (selected != null) {
                    if (window.confirm('你确定需要执行事项移除的操作吗？')) {
                        f_delete();
                    }
                }
                else {
                    alert("你至少需要选择一个事项才能执行移除操作。");
                }
                break;
        }
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
            url: "TypeList.aspx?Action=Del&Time=" + (new Date().getTime()),
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
        top.f_addTab(null, '修改事项', 'Appli/TypeEdit.aspx?ID=' + data.ID);
    }
  </script>
</body>

</html>
