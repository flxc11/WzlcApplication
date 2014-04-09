<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperLogs.aspx.cs" Inherits="CNVP.Admin.OperLogs" %>
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
    <div position="left" title="日志类型" id="mainmenu">
        <ul id="maintree"></ul>
     </div>
    <div position="center" title="操作日志" id="childmenu"> 
        <form id="mainform">
        <div id="maingrid"  style="margin:2px;"></div> 
        </form>
    </div>
  </div>
  <ul class="iconlist">
  </ul>
  <script type="text/javascript">
      var ActionID = "0";
      var MenuParent = "Root";

      var toolbarOptions = {
          items: [
            { text: '删除', click: itemclick, img: "../lib/icons/miniicons/page_delete.gif" },
            { line: true },
			{ text: '清空', click: itemclick, img: "../lib/icons/miniicons/page_delete.gif" }
        ]
      };

      var tree = $("#maintree").ligerTree({
          url: 'OperLogs.aspx?Action=GetLogsType',
          checkbox: false,
          nodeWidth: 140,
          textFieldName: 'Name',
          onClick: function (node) {
              MenuParent = node.data.MenuValue;
				var parms={
					LogType: node.data.Value
				};
				grid.set('parms', parms);
				grid.set('url', 'OperLogs.aspx?Action=GetAllLogs');
          }
      });

      var layout = $("#layout").ligerLayout({ leftWidth: 150, allowLeftCollapse: false });

      var grid = $("#maingrid").ligerGrid({
          columns: [
                    { display: '序号', name: 'LogsID', align: 'center', width: 65, minWidth: 65 },
                    { display: '日志内容', name: 'LogTitle', align: 'left', width: 280, minWidth: 60 },
                    { display: '门店名称', name: 'AppName', align: 'left', width: 260, minWidth: 60 },
                    { display: '登录帐号', name: 'UserName', align: 'left', width: 150, minWidth: 60 },
                    { display: '日志类型', name: 'LogType', align: 'center', width: 100, minWidth: 60 },
                    { display: '操作IP', name: 'LogIP', align: 'center', width: 150, minWidth: 60 },
                    { display: '操作时间', name: 'LogTime', align: 'center', width: 150, minWidth: 60 }
                    ], dataAction: 'server', pageSize: 20, toolbar: toolbarOptions, sortName: 'LogsID',sortOrder:'Desc',width: '100%', height: '100%', heightDiff: -5, url:'OperLogs.aspx?Action=GetAllLogs', checkbox: true, usePager: true, whenRClickToSelect: false, fixedCellHeight: false, rowHeight: 25
      });

      function itemclick(item) {
          var selected = grid.getSelected();
          switch (item.text) {
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
			  case "清空":
					if (window.confirm('你确定需要清空所有数据吗?')) {
						f_deleteAll();
					}
			  break;
          }
        }

        function f_delete() {
            var rows = grid.getCheckedRows();
            var ID = "";
            $(rows).each(function () {
                ID += this.LogsID + ",";
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
                url: "OperLogs.aspx?Action=DelLogs&Time=" + (new Date().getTime()),
                data: params,
                error: function () {
                    alert("系统通讯异常，请与管理员联系。");
                },
                success: function (d) {
                    grid.loadData();
                }
            });
        }
        function f_deleteAll() {
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "OperLogs.aspx?Action=ClearLogs&Time=" + (new Date().getTime()),
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