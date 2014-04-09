<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Editor.ascx.cs" Inherits="CNVP.Admin.EditHX.Editor" %>
<input type="hidden" id="<%= _fid %>" name="<%= _fid %>" value="<%= _txt %>" />
<input type="hidden" id="d_savefilename" name="d_savefilename" />
<iframe ID="eWebEditor1" src="/EditHX/EWebEditor.htm?id=<%= _fid %>&style=coolblue&savefilename=d_savefilename" frameborder="0" scrolling="no" width="550" height="350"></iframe>