<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductImages.aspx.cs" Inherits="CNVP.Client.ProductImages" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>汽车精品展示系统</title>
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/> 
<link rel="stylesheet" type="text/css" href="Css/Global.css"/>
<link rel="stylesheet" type="text/css" href="Css/Layout.css"/>
<noscript>
<style>
	.es-carousel ul{
		display:block;
	}
</style>
</noscript>
<script id="img-wrapper-tmpl" type="text/x-jquery-tmpl">	
	<div class="rg-image-wrapper">
		{{if itemsCount > 1}}
			<div class="rg-image-nav">
				<a href="#" class="rg-image-nav-prev">Previous Image</a>
				<a href="#" class="rg-image-nav-next">Next Image</a>
			</div>
		{{/if}}
		<div class="rg-image"></div>
		<div class="rg-loading"></div>
		<div class="rg-caption-wrapper">
			<div class="rg-caption" style="display:none;">
				<p></p>
			</div>
		</div>
	</div>
</script>
</head>
<body>
<div style="height:35px;line-height:35px;background:#000000">
<a href="javascript:void();" onclick="RemoveWindows();" style="color:#FFF"><asp:Literal ID="LitFullName" runat="server"></asp:Literal></a>
<span style="float:right"><a href="javascript:void();" onclick="RemoveWindows();" style="color:#FFF">关闭窗口</a></span>
</div>
<div id="rg-gallery" class="rg-gallery">
<div class="rg-thumbs">
<div class="es-carousel-wrapper">
	<div class="es-nav">
		<span class="es-nav-prev">Previous</span>
		<span class="es-nav-next">Next</span>
	</div>
	<div class="es-carousel">
		<ul>
            <asp:Literal ID="LitImagesUrl" runat="server"></asp:Literal>
		</ul>
	</div>
</div>
</div>
</div>
        <script type="text/javascript" language="javascript" src="Js/JQuery.js"></script>
        <script type="text/javascript" language="javascript" src="Js/JQuery.Tmpl.js"></script>
        <script type="text/javascript" language="javascript" src="Js/JQuery.Easing.1.3.js"></script>
        <script type="text/javascript" language="javascript" src="Js/JQuery.ElastiSlide.js"></script>
        <script type="text/javascript" language="javascript" src="Js/Gallery.js"></script>
    </body>
</html>