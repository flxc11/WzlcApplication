<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CNVP.Admin.Menu" %>
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
<div id="layout">
    <div position="left" title="管理中心" id="mainmenu">
        <ul id="maintree"></ul>
     </div>
    <div position="center" title="菜单列表" id="childmenu"> 
        <form id="mainform">
        <div id="maingrid"  style="margin:2px;"></div> 
        </form>
    </div>
</div>
  <script type="text/javascript">
      var ActionID = "0";
      var MenuParent = "Root";

      var toolbarOptions = {
          items: [
            { text: '增加', click: itemclick, img: "../lib/icons/silkicons/add.png" },
            { line: true },
            { text: '修改', click: itemclick, img: "../lib/icons/miniicons/page_edit.gif" },
            { line: true },
            { text: '保存', click: itemclick, img: "../lib/icons/silkicons/page_save.png" },
            { line: true },
            { text: '取消', click: itemclick, img: "../lib/icons/silkicons/cancel.png" },
            { line: true },
            { text: '删除', click: itemclick, img: "../lib/icons/miniicons/page_delete.gif" }
        ]
      };

      var tree = $("#maintree").ligerTree({
          url: 'Menu.aspx?Action=GetAllTopMenu',
          checkbox: false,
          textFieldName: 'MenuName',
          onClick: function (node) {
              MenuParent = node.data.MenuValue;
              grid.set('url', 'Menu.aspx?Action=GetGridMenu&MenuParent=' + node.data.MenuValue);
              f_getRoute(MenuParent);
          }
      });
      var menu = $.ligerMenu({ top: 100, left: 100, width: 120, items:
             [
             { text: '增加菜单', click: itemclick, icon: 'add' },
             { text: '修改菜单', click: itemclick, icon: 'modify' },
             { text: '删除菜单', click: itemclick },
             { line: true },
             { text: '上移一位', click: itemclick },
             { text: '下移一位', click: itemclick }
             ]
      });

      var layout = $("#layout").ligerLayout({ leftWidth: 140, isLeftCollapse: false, allowLeftCollapse: false });

      var grid = $("#maingrid").ligerGrid({
          columns: [
                  { display: '序号', name: 'MenuID', align: 'center', width: '5%', minWidth: 65 },
                  { display: '菜单名称', name: 'MenuName', align: 'left', width: '20%', minWidth: 60, validate: { required: true }, editor: { type: 'text' }
                  },
                  { display: '链接地址', name: 'MenuUrl', align: 'left', width: '30%', minWidth: 60, validate: { required: true }, editor: { type: 'text' }
                  },
                  { display: '权限值', name: 'MenuValue', align: 'left', width: '15%', minWidth: 60, editor: { type: 'text' }
                  },
                  { display: '排序值', name: 'MenuOrder', align: 'center', width: '5%', minWidth: 60, validate: { required: true }, editor: { type: 'text' }
                  },
                  { display: '菜单图标', name: 'MenuIcon', align: 'center', width: '15%', minWidth: 100, editor: { type: 'select',
                   ext:
                    function (rowdata) {
                        return {
                            onBeforeOpen: function () {
                                currentComboBox = this;
                                f_openIconsWin();
                                return false;
                            },
                            render: function () {
                                return rowdata.MenuIcon;
                            }
                        };
                    }
                  },
                  render: function (item) {
                        return "<div style='width:100%;height:100%;text-align:center'><img src='../" + item.MenuIcon + "' /></div>";
                    }
                  }
                ], dataAction: 'server', pageSize: 20, toolbar: toolbarOptions, sortName: 'MenuID',
          width: '100%', height: '100%', heightDiff: -5, checkbox: false, usePager: false, enabledEdit: true, clickToEdit: false, rowDraggable: true, whenRClickToSelect: true, fixedCellHeight: true, rowHeight: 25,
          onContextmenu: function (parm, e) {
              ActionID = parm.data.MenuID;
              menu.show({ top: e.pageY, left: e.pageX });
              return false;
          },
          rowDraggingRender: function (rows) {
              return rows[0].MenuName;
          },
          onAfterAddRow: f_onAfterAddRow,
          onAfterEdit: f_onAfterEdit,
          onRowDragDrop: f_onRowDragDrop,
          onDblClickRow: f_onDblClickRow
      });

      grid.set('url', 'Menu.aspx?Action=GetGridMenu');

      function f_onAfterAddRow(e) {

      }

      var centerHeader=$(".l-layout-center > .l-layout-header");
      function f_onDblClickRow(data, rowid, rowdata) {
          if (data.IsLeaf == 1) {
              MenuParent = data.MenuValue;
              grid.set('url', 'Menu.aspx?Action=GetGridMenu&MenuParent=' + data.MenuValue);
              //获取导航路径
              f_getRoute(MenuParent);
          }
      }
      function f_getRoute(MenuValue) {
          $.ajax({
              type: "POST",
              dataType: "html",
              url: "Menu.aspx?Action=GetRoute&Time=" + (new Date().getTime()),
              data: { MenuValue: MenuValue },
              success: function (result) {
                  centerHeader.html(result);
              }
          });
      }

      function f_onLoadGridMenu(MenuValue) {
          grid.set('url', 'Menu.aspx?Action=GetGridMenu&MenuParent=' + MenuValue);
      }
      function f_onAfterEdit(e) {
          var MenuID = 0;
          var Action = "EditMenu";
          MenuID = grid.getRow(e.rowindex).MenuID;
          if (MenuID == null) {
              Action = "AddMenu";
          }
          var params = {
              MenuID: MenuID,
              MenuName: grid.getRow(e.rowindex).MenuName,
              MenuValue: grid.getRow(e.rowindex).MenuValue,
              MenuUrl: grid.getRow(e.rowindex).MenuUrl,
              MenuIcon:grid.getRow(e.rowindex).MenuIcon,
              MenuParent: MenuParent
          };
          $.ajax({
              type: "POST",
              dataType: "json",
              url: "Menu.aspx?Action=" + Action + "&Time=" + (new Date().getTime()),
              data: params,
              error: function () {
                  alert("系统通讯异常，请与管理员联系。");
              },
              success: function (d) {
                  grid.loadData();
              }
          });
      }
      function f_onRowDragDrop(e) {
          var ID = "";
          $(grid.rows).each(function () {
              ID += this.MenuID + ",";
          });
          if (ID != "") {
              ID = ID.substring(0, ID.length - 1);
          }

          var params = {
              MenuID: ID,
              MenuParent: MenuParent
          };

          $.ajax({
              type: "POST",
              dataType: "json",
              url: "Menu.aspx?Action=UpdateSort&Time=" + (new Date().getTime()),
              data: params,
              error: function () {
                  alert("系统通讯异常，请与管理员联系。");
              },
              success: function (d) {
                  grid.loadData();
              }
          });
      }
      function f_onMoveUp(ID) {
          var params = {
              MenuID: ID,
              MenuParent: MenuParent
          };

          $.ajax({
              type: "POST",
              dataType: "json",
              url: "Menu.aspx?Action=UpSort&Time=" + (new Date().getTime()),
              data: params,
              error: function () {
                  alert("系统通讯异常，请与管理员联系。");
              },
              success: function (d) {
                  grid.loadData();
              }
          });
      }
      function f_onMoveDown(ID) {
          var params = {
              MenuID: ID,
              MenuParent: MenuParent
          };

          $.ajax({
              type: "POST",
              dataType: "json",
              url: "Menu.aspx?Action=DownSort&Time=" + (new Date().getTime()),
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
              ID += this.MenuID + ",";
          });
          if (ID != "") {
              ID = ID.substring(0, ID.length - 1);
          }

          var params = {
              MenuID: ID
          };

          $.ajax({
              type: "POST",
              dataType: "json",
              url: "Menu.aspx?Action=DelMenu&Time=" + (new Date().getTime()),
              data: params,
              error: function () {
                  alert("系统通讯异常，请与管理员联系。");
              },
              success: function (d) {
                  grid.loadData();
              }
          });


      }

      function itemclick(item) {
          var editingrow = grid.getEditingRow();
          var row = grid.getSelectedRow();
          switch (item.text) {
              case "增加":
              case "增加菜单":
                  grid.addEditRow();
                  break;
              case "修改":
              case "修改菜单":
                  $(grid.getCheckedRows()).each(function () {
                      grid.beginEdit(this);
                  });
                  break;
              case "保存":
                  grid.endEdit();
                  break;
              case "取消":
                  grid.cancelEdit();
                  grid.loadData();
                  break;
              case "删除":
              case "删除菜单":
                  if (window.confirm('你确定需要删除选择的数据吗?')) {
                      f_delete();
                  }
                  break;
              case "上移一位":
                  f_onMoveUp(row.MenuID);
                  break;
              case "下移一位":
                  f_onMoveDown(row.MenuID);
                  break;
          }
      }
  </script>
</body>
</html>