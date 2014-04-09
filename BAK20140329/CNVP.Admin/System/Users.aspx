<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="CNVP.Admin.Users" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<script language="javascript" type="text/javascript" src="../lib/jquery/jquery-1.5.2.min.js"></script>
<script language="javascript" type="text/javascript" src="../lib/ligerUI/js/ligerui.all.js"></script>
<script language="javascript" type="text/javascript" src="../lib/ligerUI/js/plugins/ligerGrid.js"></script>
<script language="javascript" type="text/javascript" src="../lib/ligerUI/js/plugins/ligerResizable.js"></script>
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
    <div position="center" title="用户列表" id="childmenu"> 
        <form id="mainform">
        <div id="maingrid"  style="margin:2px;"></div> 
        </form>
    </div>
  </div>
  
  <script type="text/javascript">
      var UserID;
      var MenuParent = "Root";

      var toolbarOptions = {
          items: [
            { text: '增加', click: itemclick, img: "../lib/icons/silkicons/add.png" },
            { line: true },
            { text: '修改', click: itemclick, img: "../lib/icons/miniicons/page_edit.gif" },
            { line: true },
            { text: '启用', click: itemclick, img: "../lib/icons/silkicons/control_play.png" },
            { line: true },
            { text: '禁用', click: itemclick, img: "../lib/icons/silkicons/control_stop.png" },
            { line: true },
            { text: '删除', click: itemclick, img: "../lib/icons/miniicons/page_delete.gif" }
        ]
      };

      

      var menu = $.ligerMenu({ top: 100, left: 100, width: 120, items:
             [
             { text: '增加帐号', click: itemclick, icon: 'add' },
             { text: '修改帐号', click: itemclick, icon: 'modify' },
             { text: '删除帐号', click: itemclick },
             { line: true },
             { text: '帐号启用', click: itemclick },
             { text: '帐号禁用', click: itemclick },
             { text: '密码重置', click: itemclick }
             ]
      });

      var layout = $("#layout").ligerLayout({ leftWidth: 180, allowLeftCollapse: false });

      var grid = $("#maingrid").ligerGrid({
          columns: [
                { display: '序号', name: 'UserID', align: 'center', width: 65, minWidth: 65, frozen: true },
                { display: '登录帐号', name: 'UserName', align: 'center', width: 100, minWidth: 60, frozen: true },
                { display: '真实姓名', name: 'UserTrueName', align: 'center', width: 100, minWidth: 60, frozen: true },
                { display: '联系电话', name: 'UserTelPhone', align: 'center', width: 120, minWidth: 60 },
                { display: '手机号码', name: 'UserMobile', align: 'center', width: 120, minWidth: 60 },
                { display: '联系邮箱', name: 'UserEmail', align: 'center', width: 200, minWidth: 60 },
                { display: '状态', name: 'IsLock', align: 'center', width: 60, minWidth: 60,
                    render: function (item) {
                        if (item.IsLock == 1) {
                            return "<a href='javascript:f_islock(0)'><img src='../lib/icons/silkicons/control_play_blue.png' title='启用状态'></a>";
                        }
                        else {
                            return "<a href='javascript:f_islock(1)'><img src='../lib/icons/silkicons/control_stop_blue.png' title='停用状态'></a>";
                        }
                    }
                },
                { display: '登录次数', name: 'LoginNum', align: 'center', width: 100, minWidth: 60 },
                { display: '登录时间', name: 'LoginTime', align: 'center', width: 150, minWidth: 60 },
                { display: '登录IP', name: 'LoginIP', align: 'center', width: 150, minWidth: 60 },
                { display: '创建时间', name: 'PostTime', align: 'center', width: 150, minWidth: 60 }
                ], dataAction: 'server', pageSize: 20, toolbar: toolbarOptions, sortName: 'A.UserID',
          width: '100%', height: '100%', heightDiff: -5, url:'Users.aspx?Action=GetUsersList', checkbox: true, frozen: true, usePager: true, whenRClickToSelect: true, fixedCellHeight: true, rowHeight: 25,onDblClickRow: f_onDblClickRow,
          onContextmenu: function (parm, e) {
              UserID = parm.data.UserID;
              menu.show({ top: e.pageY, left: e.pageX });
              return false;
          }
      });

      function f_delete() {
          var rows = grid.getCheckedRows();
          var ID = "";
          $(rows).each(function () {
              ID += this.UserID + ",";
          });
          if (ID != "") {
              ID = ID.substring(0, ID.length - 1);
          }

          var params = {
              UserID: ID
          };

          $.ajax({
              type: "POST",
              dataType: "json",
              url: "Users.aspx?Action=DelUserInfo&Time=" + (new Date().getTime()),
              data: params,
              error: function () {
                  alert("系统通讯异常，请与管理员联系。");
              },
              success: function (d) {
                  grid.loadData();
              }
          });
      }
      function f_islock(IsLock)
      {
          var rows = grid.getCheckedRows();
          var ID = "";
          $(rows).each(function () {
              ID += this.UserID + ",";
          });
          if (ID != "") {
              ID = ID.substring(0, ID.length - 1);
          }

          var params = {
              UserID: ID,
              IsLock: IsLock
          };
          $.ajax({
              type: "POST",
              dataType: "json",
              url: "Users.aspx?Action=IsLock&Time=" + (new Date().getTime()),
              data: params,
              error: function () {
                  alert("系统通讯异常，请与管理员联系。");
              },
              success: function (d) {
                  grid.loadData();
              }
          });
      }
      function f_isReset(UserID) {
          var params = {
              UserID: UserID
          };
          $.ajax({
              type: "POST",
              dataType: "json",
              url: "Users.aspx?Action=IsReset&Time=" + (new Date().getTime()),
              data: params,
              error: function () {
                  alert("系统通讯异常，请与管理员联系。");
              },
              success: function (d) {
                  alert(d.Message);
                  grid.loadData();
              }
          });
      }

      function itemclick(item) {
          var rows = grid.getCheckedRows();
          var selected = grid.getSelected();
          switch (item.text) {
              case "增加":
              case "增加帐号":
                  menu.hide();
                  top.f_addTab('UsersAdd', '增加帐号', 'System/UsersAdd.aspx');
                  break;
              case "修改":
              case "修改帐号":
                  if (selected) {
                      menu.hide();
                      if (rows.length > 1) {
                          top.f_addTab(null, '修改帐号', 'System/UsersEdit.aspx?UserID=' + UserID);
                      }
                      else {
                          top.f_addTab(null, '修改帐号', 'System/UsersEdit.aspx?UserID=' + selected.UserID);
                      }
                  }
                  else {
                      alert("请至少选择一条记录再执行本操作！");
                  }
                  break;
              case "删除":
              case "删除帐号":
                  if (selected) {
                      menu.hide();
                      if (window.confirm('你确定需要删除选择的数据吗?')) {
                          f_delete();
                      }
                  }
                  else {
                      alert("请至少选择一条记录再执行本操作！");
                  }
                  break;
              case "启用":
              case "帐号启用":
                  if (selected) {
                      f_islock(1);
                  }
                  else {
                      alert("请至少选择一条记录再执行本操作！");
                  }
                  break;
              case "禁用":
              case "帐号禁用":
                  if (selected) {
                      f_islock(0);
                  }
                  else {
                      alert("请至少选择一条记录再执行本操作！");
                  }
                  break;
              case "密码重置":
                  if (selected) {
                      menu.hide();
                      if (rows.length > 1) {
                          f_isReset(UserID);
                      }
                      else {
                          f_isReset(selected.UserID);
                      }
                  }
                  else {
                      alert("请至少选择一条记录再执行本操作！");
                  }
                  break;
          }
      }

      function f_onDblClickRow(data, rowid, rowdata) {
          top.f_addTab(null, '修改帐号', 'System/UsersEdit.aspx?UserID=' + data.UserID);
      }
  </script>
</body>
</html>