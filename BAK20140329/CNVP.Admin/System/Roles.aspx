<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="CNVP.Admin.Roles" %>
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
</head>
<body style="padding:2px;height:100%; text-align:center; overflow:hidden" scroll="no">
<div id="layout" style="margin:2px; margin-right:3px;">
    <div position="center" id="mainmenu" title="角色列表"> 
        <div id="rolegrid" style="margin:2px auto;"></div>
    </div>
    <div position="bottom" title="权限列表">
        <div id="rightgrid" style="margin:2px auto;"></div>
    </div>
</div>
<script type="text/javascript">

    //覆盖本页面grid的loading效果
    LG.overrideGridLoading();

    var layout = $("#layout").ligerLayout({
        bottomHeight: 3 * $(window).height() / 5,
        heightDiff: -6,
        onEndResize: updateGridHeight,
        onHeightChanged: updateGridHeight
    });
    var bottomHeader = $(".l-layout-bottom > .l-layout-header:first");

    var selectedRoleID;

    //权限 保存按钮
    var toolbarRolesOptions = {
        items: [
            { text: '增加', click: itemclick, img: "../lib/icons/silkicons/add.png" },
            { line: true },
            { text: '修改', click: itemclick, img: "../lib/icons/miniicons/page_edit.gif" },
            { line: true },
            { text: '删除', click: itemclick, img: "../lib/icons/miniicons/page_delete.gif" }
        ]
    };

    var gridRole = $("#rolegrid").ligerGrid({
        columns:
                [
                { display: '角色名称', name: 'RoleName', width: 200, align: 'left' },
                { display: '角色描述', name: 'RoleRemark', width: 450, align: 'left' }
                ], showToggleColBtn: false, width: '99%', height: 200, rowHeight: 20, fixedCellHeight: true,
        columnWidth: 100, frozen: false, sortName: 'RoleID', usePager: false, checkbox: false, rownumbers: true, toolbar: toolbarRolesOptions, url: 'Roles.aspx?Action=RoleList'
        });
        gridRole.bind('SelectRow', function (rowdata) {
            selectedRoleID = rowdata.RoleID;
            bottomHeader.html("设置角色【" + rowdata.RoleName + "】的权限");
            gridRight.reRender();

            //加载角色设置
            LG.ajax({
                loading: '正在加载角色权限中...',
                ashxUrl: 'Roles.aspx',
                data: { Action: 'GetPermission', RoleID: selectedRoleID },
                success: function (data) {
                    var rows = gridRight.rows;
                    for (var i = 0, l = rows.length; i < l; i++) {
                        checkPermit(rows[i], data);
                    }
                }
            });

            //判断是否有权限
            function checkPermit(rowdata, data) {
                for (var i = 0, l = data.length; i < l; i++) {
                    if (data[i].OperateCode == rowdata.MenuValue) {
                        gridRight.select(rowdata);
                    }
                }
            }
        });

    function itemclick(item) {
        var selected = gridRole.getSelected();
        var rows = gridRole.getCheckedRows();
        switch (item.text) {
            case "增加":
                top.f_addTab('RolesAdd', '增加角色', 'System/RolesAdd.aspx');
                break;
            case "修改":
                if (selected) {
                    if (rows.length > 1) {
                        top.f_addTab('RolesEdit', '编辑角色', 'System/RolesEdit.aspx?RoleID=' + selectedRoleID);
                    }
                    else {
                        top.f_addTab('RolesEdit', '编辑角色', 'System/RolesEdit.aspx?RoleID=' + selected.RoleID);
                    }
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
                break;
        }
    };

    //权限 保存按钮
    var toolbarOptions = {
        items: [
            { text: '保存', click: f_save, img: "../lib/icons/silkicons/page_save.png" }
        ]
    };

    //权限
    var gridRight = $("#rightgrid").ligerGrid({
        columns:
                [
                { display: '菜单名称', name: 'MenuName', align: 'left', width: 200, minWidth: 60 },
                { display: '权限值', name: 'MenuValue', align: 'left', width: 200, minWidth: 60 },
                { display: '导航路径', name: 'MenuUrl', align: 'left', width: 260, minWidth: 60 },
                { display: '图标', name: 'MenuIcon', align: 'left', width: 60, minWidth: 60,
                    render: function (item) {
                        return "<div style='width:100%;height:100%;text-align:center'><img src='../" + item.MenuIcon + "' /></div>";
                    }
                }
                ], showToggleColBtn: false, width: '99%', height: 200, rowHeight: 20, fixedCellHeight: true,
        columnWidth: 100, frozen: false, usePager: false, checkbox: true, rownumbers: true, toolbar: toolbarOptions,
        tree: { columnName: 'MenuName' },
        onCheckRow: f_isCheck,
        data: []
    });

    function f_isCheck(checked, data, rowid, rowdata) {
        var rootRow = gridRight.getParent(rowdata);

        var flg = false;
        var childRow = gridRight.getChildren(rootRow, false);
        for (var i = 0; i < childRow.length; i++) {
            if (gridRight.isSelected(childRow[i])) {
                flg = true;
                break;
            }
        }

        if (flg) {
            gridRight.select(rootRow);
        }
        else {
            gridRight.unselect(rootRow);
        }
    }

    //加载菜单权限
    LG.ajax({
        loading: '正在加载菜单按钮中...',
        ashxUrl: 'Roles.aspx',
        data: { Action: 'MenuList' },
        success: function (data) {
            gridRight.set('data', { Rows: data });
        }
    });

    function f_delete() {
        var rows = gridRole.getCheckedRows();
        var ID = "";
        $(rows).each(function () {
            ID += this.RoleID + ",";
        });
        if (ID != "") {
            ID = ID.substring(0, ID.length - 1);
        }

        var params = {
            RoleID: ID
        };
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "Roles.aspx?Action=DelRoles&Time=" + (new Date().getTime()),
            data: params,
            error: function () {
                alert("系统通讯异常，请与管理员联系。");
            },
            success: function (d) {
                gridRole.loadData();
                gridRight.loadData();
            }
        });
    }

    function f_save() {
        if (!selectedRoleID) {
            alert("请先选择一个用户角色组再进行本操作！");
            return;
        }
        var Operate = "";
        for (var i = 0, l = gridRight.rows.length; i < l; i++) {
            var row = gridRight.rows[i];
            if (gridRight.isSelected(row)) {
                Operate += row.MenuValue + ",";
            }
        }
        if (Operate != "") {
            Operate = Operate.substring(0, Operate.length - 1);
        }

        $.ajax({
            type: "post",
            dataType: "json",
            url: "Roles.aspx?Time=" + (new Date().getTime()),
            data: {
                Action: 'SavePermission',
                RoleID: selectedRoleID,
                Operate: Operate
            },
            success: function (d) {
                alert("恭喜，角色权限设置操作成功！");
                gridRole.loadData();
                gridRight.loadData();
            }
        });
    }

    function updateGridHeight() {
        var topHeight = $("#layout > .l-layout-center").height();
        var bottomHeight = $("#layout > .l-layout-bottom").height();
        if (gridRole)
            gridRole.set('height', topHeight - 35);
        if (gridRight)
            gridRight.set('height', bottomHeight - 35);
    }
    updateGridHeight();
    </script>
</body>
</html>