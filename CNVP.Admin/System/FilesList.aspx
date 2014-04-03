<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FilesList.aspx.cs" Inherits="CNVP.Admin.FilesList" %>
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
<script language="javascript" type="text/javascript" src="../lib/js/thickbox.js"></script>
<script language="javascript" type="text/javascript" src="../lib/js/option.js"></script>
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
<link rel="stylesheet" type="text/css" href="../lib/css/thickbox.css"/>
<link rel="stylesheet" type="text/css" href="../lib/css/option.css"/> 
<style type="text/css">
.l-panel td.l-grid-row-cell-editing { padding-bottom: 2px;padding-top: 2px;}
</style>
</head>
<body style="padding:2px;height:100%; text-align:center; overflow:hidden" scroll="no">
<div id="layout">
    <div position="left" title="资源类型" id="mainmenu">
        <ul id="maintree"></ul>
     </div>
    <div position="center" title="资源列表" id="childmenu"> 
        <form id="mainform">
        <div id="maingrid"  style="margin:2px;"></div> 
        </form>
    </div>
  </div>
  <ul class="iconlist">
  </ul>
  <script type="text/javascript">
      var ActionID = "0";
      var FilesType = "";

      var toolbarOptions = {
          items: [
            { text: '查找', click: itemclick, img: "../lib/icons/miniicons/page_edit.gif" },
            { line: true },
            { text: '删除', click: itemclick, img: "../lib/icons/miniicons/page_delete.gif" }
        ]
      };

      var tree = $("#maintree").ligerTree({
          url: 'FilesList.aspx?Action=GetFilesType',
          checkbox: false,
          nodeWidth: 140,
          textFieldName: 'Name',
          onClick: function (node) {
              FilesType = node.data.Value;
              grid.set('url', 'FilesList.aspx?Action=GetFilesList&FilesType=' + FilesType);
          }
      });

      var layout = $("#layout").ligerLayout({ leftWidth: 150, allowLeftCollapse: false });

      var grid = $("#maingrid").ligerGrid({
          columns: [
                  { display: '文件序号', name: 'FilesID', align: 'center', width: 65, minWidth: 65 },
                  { display: '资源类型', name: 'FilesType', align: 'center', width: 80, minWidth: 60,
                      render: function (item) {
                          switch (item.FilesType) {
                              case "1":
                                  return "<a href='javascript:void();' onclick='f_GridLoad(1);'>产品图片</a>";
                                  break;
                              case "2":
                                  return "<a href='javascript:void();' onclick='f_GridLoad(2);'>颜色图片</a>";
                                  break;
                              case "3":
                                  return "<a href='javascript:void();' onclick='f_GridLoad(3);'>款式图片</a>";
                                  break;
                              case "4":
                                  return "<a href='javascript:void();' onclick='f_GridLoad(4);'>套餐图片</a>";
                                  break;
                          }
                      }
                  },
                  { display: '精品名称', name: 'FullName', align: 'center', width: 180, minWidth: 60 },
                  { display: '文件名称', name: 'FilesName', align: 'center', width: 100, minWidth: 60 },
                  { display: '文件路径', name: 'FilesUrl', align: 'center', width: 280, minWidth: 60,
                      render: function (item) {
                          return "<a href='javascript:void()' onclick=\"showTBPic(\'" + item.FilesUrl + "\','" + item.FullName + "','parent')\">" + item.FilesUrl + "</a>";
                      }
                  },
                  { display: '创建时间', name: 'PostTime', align: 'center', width: 150, minWidth: 60 }
                  ], dataAction: 'server', pageSize: 20, toolbar: toolbarOptions, sortName: 'FilesID',
          width: '100%', height: '100%', heightDiff: -5, checkbox: true, usePager: true, enabledEdit: false, rowDraggable: false, whenRClickToSelect: false, rowHeight: 25
      });

      grid.set('url', 'FilesList.aspx?Action=GetFilesList');

      function f_GridLoad(FilesType,SearchType,KeyWord) {
          grid.set('url', 'FilesList.aspx?Action=GetFilesList&FilesType='+ FilesType +'&SearchType='+ SearchType +'&Keyword='+ KeyWord);
      }

      function itemclick(item) {
          var editingrow = grid.getEditingRow();
          var row = grid.getSelectedRow();
          switch (item.text) {
              case "查找":
                  showTB('System/FilesSearch.aspx', 300, 150, '查找资源', 'parent');
                  break;
              case "删除":
                  if (window.confirm('您确定需要删除选择的文件吗？')) {
                     f_delete();
                  }
                  break;
          }
      }
      function f_delete() {
          var rows = grid.getCheckedRows();
          var ID = "";
          $(rows).each(function () {
              ID += this.FilesID + ",";
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
              url: "FilesList.aspx?Action=DelFiles&Time=" + (new Date().getTime()),
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