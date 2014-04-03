<%Option Explicit%>
<!--#Include file="Config.Asp"-->
<%
Dim sAction,sStyle
sAction = Lcase(Request("Action"))
sStyle = Lcase(Request("Style"))
IF sAction="config" then
	Dim i,aCurrStyle
	For i = 1 To Ubound(aStyle)
		aCurrStyle = Split(aStyle(i), "|||")
		IF sStyle = Trim(aCurrStyle(0)) Then
			Call ShowStyle(i)
			Exit For
		End IF
	Next
End IF
Sub ShowStyle(StyleID)
	Dim sStyleName, sFixWidth, sSkin, sStyleUploadDir, sStyleWidth, sStyleHeight, sStyleMemo, sStyleStateFlag, sStyleDetectFromWord, sStyleInitMode, sStyleBaseUrl, sStyleUploadObject, sStyleAutoDir, sStyleBaseHref, sStyleContentPath, sStyleAutoRemote, sStyleShowBorder, sStyleAllowBrowse
	Dim sSLTFlag, sSLTMinSize, sSLTOkSize, sSYWZFlag, sSYText, sSYFontColor, sSYFontSize, sSYFontName, sSYPicPath, sSLTSYObject, sSLTSYExt, sSYWZMinWidth, sSYShadowColor, sSYShadowOffset, sSYWZMinHeight, sSYWZPosition, sSYWZTextWidth, sSYWZTextHeight, sSYWZPaddingH, sSYWZPaddingV, sSYTPFlag, sSYTPMinWidth, sSYTPMinHeight, sSYTPPosition, sSYTPPaddingH, sSYTPPaddingV, sSYTPImageWidth, sSYTPImageHeight, sSYTPOpacity, sCusDirFlag
	Dim sStyleFileExt, sStyleFlashExt, sStyleImageExt, sStyleMediaExt, sStyleRemoteExt, sStyleLocalExt, sStyleFileSize, sStyleFlashSize, sStyleImageSize, sStyleMediaSize, sStyleRemoteSize, sStyleLocalSize
	Dim sToolBarID, sToolBarName, sToolBarOrder, sToolBarButton
	Dim sSBCode, sSBEdit, sSBText, sSBView
	Dim sEnterMode, sAreaCssMode
	Dim nStyleID, sCurrStyle
	If IsNumeric(StyleID) = True Then
		nStyleID = Clng(StyleID)
		If nStyleID <= Ubound(aStyle) Then
		sCurrStyle = Split(aStyle(nStyleID), "|||")
		End IF		
	End IF
	
	sStyleName = sCurrStyle(0)
	sFixWidth = sCurrStyle(1)
	sSkin = sCurrStyle(2)
	sStyleUploadDir = sCurrStyle(3)
	sStyleBaseHref = sCurrStyle(22)
	sStyleContentPath = sCurrStyle(23)
	sStyleWidth = sCurrStyle(4)
	sStyleHeight = sCurrStyle(5)
	sStyleMemo = sCurrStyle(26)
	sStyleFileExt = sCurrStyle(6)
	sStyleFlashExt = sCurrStyle(7)
	sStyleImageExt = sCurrStyle(8)
	sStyleMediaExt = sCurrStyle(9)
	sStyleRemoteExt = sCurrStyle(10)
	sStyleFileSize = sCurrStyle(11)
	sStyleFlashSize = sCurrStyle(12)
	sStyleImageSize = sCurrStyle(13)
	sStyleMediaSize = sCurrStyle(14)
	sStyleRemoteSize = sCurrStyle(15)
	sStyleStateFlag = sCurrStyle(16)
	sSBCode = sCurrStyle(62)
	sSBEdit = sCurrStyle(63)
	sSBText = sCurrStyle(64)
	sSBView = sCurrStyle(65)
	sEnterMode = sCurrStyle(66)
	sAreaCssMode = sCurrStyle(67)
	sStyleAutoRemote = sCurrStyle(24)
	sStyleShowBorder = sCurrStyle(25)
	sStyleUploadObject = sCurrStyle(20)
	sStyleAutoDir = sCurrStyle(21)
	sStyleDetectFromWord = sCurrStyle(17)
	sStyleInitMode = sCurrStyle(18)
	sStyleBaseUrl = sCurrStyle(19)
	sSLTFlag = sCurrStyle(29)
	sSLTMinSize = sCurrStyle(30)
	sSLTOkSize = sCurrStyle(31)
	sSYWZFlag = sCurrStyle(32)
	sSYText = sCurrStyle(33)
	sSYFontColor = sCurrStyle(34)
	sSYFontSize = sCurrStyle(35)
	sSYFontName = sCurrStyle(36)
	sSYPicPath = sCurrStyle(37)
	sSLTSYObject = sCurrStyle(38)
	sSLTSYExt = sCurrStyle(39)
	sSYWZMinWidth = sCurrStyle(40)
	sSYShadowColor = sCurrStyle(41)
	sSYShadowOffset = sCurrStyle(42)
	sStyleAllowBrowse = sCurrStyle(43)
	sStyleLocalExt = sCurrStyle(44)
	sStyleLocalSize = sCurrStyle(45)
	sSYWZMinHeight = sCurrStyle(46)
	sSYWZPosition = sCurrStyle(47)
	sSYWZTextWidth = sCurrStyle(48)
	sSYWZTextHeight = sCurrStyle(49)
	sSYWZPaddingH = sCurrStyle(50)
	sSYWZPaddingV = sCurrStyle(51)
	sSYTPFlag = sCurrStyle(52)
	sSYTPMinWidth = sCurrStyle(53)
	sSYTPMinHeight = sCurrStyle(54)
	sSYTPPosition = sCurrStyle(55)
	sSYTPPaddingH = sCurrStyle(56)
	sSYTPPaddingV = sCurrStyle(57)
	sSYTPImageWidth = sCurrStyle(58)
	sSYTPImageHeight = sCurrStyle(59)
	sSYTPOpacity = sCurrStyle(60)
	sCusDirFlag = sCurrStyle(61)

	Response.write "config.FixWidth = '" & sFixWidth & "';" & VbCrlf
	Response.write "config.UploadUrl = '" & sStyleUploadDir & "';" & VbCrlf
	Response.write "config.InitMode = '" & sStyleInitMode & "';" & VbCrlf
	Response.write "config.AutoDetectPaste = '1';" & VbCrlf
	Response.write "config.BaseUrl = '" & sStyleBaseUrl & "';" & VbCrlf
	Response.write "config.BaseHref = '"& sStyleBaseHref &"';" & VbCrlf
	Response.write "config.AutoRemote = '" & sStyleAutoRemote & "';" & VbCrlf
	Response.write "config.ShowBorder = '" & sStyleShowBorder & "';" & VbCrlf
	Response.write "config.StateFlag = '" & sStyleStateFlag & "';" & VbCrlf
	Response.write "config.SBCode = '" & sSBCode & "';" & VbCrlf
	Response.write "config.SBEdit = '" & sSBEdit & "';" & VbCrlf
	Response.write "config.SBText = '" & sSBText & "';" & VbCrlf
	Response.write "config.SBView = '" & sSBView & "';" & VbCrlf
	Response.write "config.EnterMode = '" & sEnterMode & "';" & VbCrlf
	Response.write "config.Skin = '" & sSkin & "';" & VbCrlf
	Response.write "config.AutoDetectLanguage = '1';" & VbCrlf
	Response.write "config.DefaultLanguage = 'zh-cn';" & VbCrlf
	Response.write "config.AllowBrowse = '" & sStyleAllowBrowse & "';" & VbCrlf
	Response.write "config.AllowImageSize = '" & sStyleImageSize & "';" & VbCrlf
	Response.write "config.AllowFlashSize = '" & sStyleFlashSize & "';" & VbCrlf
	Response.write "config.AllowMediaSize = '" & sStyleMediaSize & "';" & VbCrlf
	Response.write "config.AllowFileSize = '" & sStyleFileSize & "';" & VbCrlf
	Response.write "config.AllowRemoteSize = '" & sStyleRemoteSize & "';" & VbCrlf
	Response.write "config.AllowLocalSize = '" & sStyleLocalSize & "';" & VbCrlf
	Response.write "config.AllowImageExt = '" & sStyleImageExt & "';" & VbCrlf
	Response.write "config.AllowFlashExt = '" & sStyleFlashExt & "';" & VbCrlf
	Response.write "config.AllowMediaExt = '" & sStyleMediaExt & "';" & VbCrlf
	Response.write "config.AllowFileExt = '" & sStyleFileExt & "';" & VbCrlf
	Response.write "config.AllowRemoteExt = '" & sStyleRemoteExt & "';" & VbCrlf
	Response.write "config.AllowLocalExt = '" & sStyleLocalExt & "';" & VbCrlf
	Response.write "config.AreaCssMode = '" & sAreaCssMode & "';" & VbCrlf
	Response.write "config.SYWZFlag = '" & sSYWZFlag & "';" & VbCrlf
	Response.write "config.SYTPFlag = '" & sSYTPFlag & "';" & VbCrlf
	Response.write "config.HttpFlag = '0';" & VbCrlf
	Response.write "config.L = 'ok';" & VbCrlf
	Response.write "config.ServerExt = 'asp';" & VbCrlf
	
	Response.write "config.Toolbars = [" & VbCrlf
	Dim ToolBarNum,sToolBarNum,Toolbari,sToolbari,arrToolbar,strToolBar,TempStrToolBar
	TempStrToolBar = ""
	ToolBarNum = 0
	sToolBarNum = 1
	'得出工具栏总数
	For Toolbari = 1 to Ubound(aToolbar)
		arrToolbar = Split(aToolbar(Toolbari),"|||")
		IF Clng(StyleID) = Clng(arrToolbar(0)) Then
		ToolBarNum = ToolBarNum + 1
		End IF
	Next
	'输出工具栏
	For Toolbari = 1 To Ubound(aToolbar)
		strToolBar = ""
		arrToolbar = Split(aToolbar(Toolbari),"|||")
		IF Clng(StyleID) = Clng(Trim(arrToolbar(0))) Then
			strToolBar = arrToolbar(1)
			TempStrToolBar = TempStrToolBar & "['" & Replace(strToolBar,"|","', '") & "']"
			IF sToolBarNum < ToolBarNum Then
				TempStrToolBar = TempStrToolBar & ","&VbCrlf
			Else
				TempStrToolBar = TempStrToolBar & VbCrlf
			End IF
			sToolBarNum = sToolBarNum + 1
		End IF
	Next
	Response.Write TempStrToolBar
	Response.write "];" & VbCrlf
End Sub
%>