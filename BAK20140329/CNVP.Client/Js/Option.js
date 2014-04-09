/*************************************************
描述：设定为全选状态
应用：列表页面的全选按钮
作者：Apollo
时间：2009-11-13 
*************************************************/
function CheckAll(obj)
{
	obj = eval(obj);
	for (var i=0; i<obj.elements.length; i++){
	    if (obj.elements[i].type == "checkbox" && obj.elements[i].name == "ID" && obj.elements[i].disabled == false) {
	        if (obj.elements[i].checked == true)
	            obj.elements[i].checked = false;
	        else
	            obj.elements[i].checked = true;
	    }
    }
}
/*************************************************
描述：设定为全选状态
应用：角色授权的全选按钮
作者：Apollo
时间：2009-11-13 
*************************************************/
function CheckAll1(obj)
{
	obj = eval(obj);
	for (var i=0; i<obj.elements.length; i++){
	    if (obj.elements[i].type == "checkbox" && (obj.elements[i].name != "ChkISLock")) {
	        if (obj.elements[i].checked == true)
	            obj.elements[i].checked = false;
	        else
	            obj.elements[i].checked = true;
	    }
    }
}
/*************************************************
描述：设定为全选状态
应用：角色授权的全选按钮
作者：Apollo
时间：2009-11-13 
*************************************************/
function CheckAll2(obj)
{
	obj = eval(obj);
	for (var i=0; i<obj.elements.length; i++){
	    if (obj.elements[i].type == "checkbox" && (obj.elements[i].name != "ChkSubColumn") && (obj.elements[i].name != "ChkColumnAdmin") && (obj.elements[i].name != "ChkColumnLeft") && (obj.elements[i].name != "ChkAssociateID")) {
	        if (obj.elements[i].checked == true)
	            obj.elements[i].checked = false;
	        else
	            obj.elements[i].checked = true;
	    }
    }
}
/*************************************************
描述：设定为全选状态
应用：角色授权的全选按钮
作者：Apollo
时间：2009-11-13 
*************************************************/
function CheckAll3(obj)
{
	$("[name='ChkAssociateID']").each(function()      //反选
	{
		if($(this).attr("checked"))
		{
			$(this).removeAttr("checked");
		}
		else
		{
			$(this).attr("checked",'true');
		}
	})
}
/*************************************************
描述：获取全选CheckBox的值
作者：Apollo
时间：2009-11-13 
*************************************************/
function GetCheckAll(obj) {
    obj = eval(obj);
    var str_ids = "";
    for (var i = 0; i < obj.elements.length; i++) {
        if (obj.elements[i].type == "checkbox" && obj.elements[i].checked == true && (obj.elements[i].name != "IS.CheckBox") && (obj.elements[i].name != "ChkISLock")) {
            str_ids += obj.elements[i].value + ",";
        }
    }
    return str_ids.substring(0, str_ids.length - 1);
}
/*************************************************
描述：父类选择之后自动全选\反选子类
作者：Apollo
时间：2009-11-13 
*************************************************/
function CheckSelect(obj,name)
{
	obj = eval(obj);
	var _length=name.length+1;
	for (var i=0; i<obj.elements.length; i++){
		if (obj.elements[i].name.substring(0,_length)==name+"_"){
			if ($("#"+name).attr("checked")==true){
				obj.elements[i].checked = true;
			}
			else{
				obj.elements[i].checked = false;
			}
		}
	}
}
/*************************************************
描述：子类选择之后自动勾选父类
作者：Apollo
时间：2009-11-13 
*************************************************/
function CheckSubSelect(obj,name)
{
	obj = eval(obj);
	var _length=name.length+1;
	var flg=false;
	for (var i=0; i<obj.elements.length; i++){
		if (obj.elements[i].name.substring(0,_length)==name+"_"){
			if(obj.elements[i].checked==true){
				flg=true;
				break;
			}
		}
	}
	if (flg){
		$("#"+name).attr("checked",true);
	}
	else{
		$("#"+name).attr("checked",false);
	}
}
/*************************************************
描述：选择一项从A列表到B列表
作者：Apollo
时间：2009-11-15 
*************************************************/
function SelectedOptions(e1, e2){
	for(var i=0;i<e1.options.length;i++){
		if(e1.options[i].selected){
			var e = e1.options[i];
			e2.options.add(new Option(e.text, e.value));
			e1.remove(i);
			i=i-1
		}
	}
}
/*************************************************
描述：选择全部A列表到B列表
作者：Apollo
时间：2009-11-15 
*************************************************/
function AllSelectedOptions(e1, e2){
	for(var i=0;i<e1.options.length;i++){
		var e = e1.options[i];
		e2.options.add(new Option(e.text, e.value));
		e1.remove(i);
		i=i-1
	}
}
/*************************************************
描述：获取列表框的值
作者：Apollo
时间：2009-11-15 
*************************************************/
function GetSelectValue(e) {
    var s_id = "";
    for (var i = 0; i < e.options.length; i++) {
        s_id += e.options[i].value + ",";
    }
    return s_id.substring(0, s_id.length - 1);
}
/*************************************************
描述：创建提示图层
作者：Apollo
时间：2009-11-17 
*************************************************/
function ShowTip(str) {
    var tmp_div = document.createElement("DIV");
    if (tmp_div) {
        tmp_div.className = "div_tip";
        tmp_div.name = "div_tip";
        tmp_div.id = "div_tip";
        str = (str) ? str : "操作正在进行中，请稍候...";
        tmp_div.innerHTML += "<span class=\"tip_image\">&nbsp;</span><span class=\"tip_content\">" + str + "</span>";
        document.body.appendChild(tmp_div);
    }
}
/*************************************************
描述：关闭提示图层
作者：Apollo
时间：2009-11-17 
*************************************************/
function ClearTip() {
    var tmp_div = document.getElementById("div_tip");
    if (tmp_div)
        document.body.removeChild(tmp_div);
}
/*************************************************
描述：提交表单之前出现提示图层
作者：Apollo
时间：2009-11-17 
*************************************************/
function DoSubmit(obj) {
    ShowTip();
    obj.submit();
}
/*************************************************
描述：搜索表单提交
作者：Apollo
时间：2009-11-18
*************************************************/
function DoSearch(url,element,value)
{
	ShowTip();
	if (url=='')
	{
		url+=location.protocol;
		url+="//";
		url+=location.hostname;
		url+=location.pathname;
	}
	else
	{
		url=window.location.href;	
	}
	window.location.href=url+'?'+element+'='+escape(value);
}
/*************************************************
描述：Ajax方法提交参数
作者：Apollo
时间：2009-12-01
*************************************************/
function DoPost(Url,ID,action) {
    $.ajax({
        type: "post",
        dataType: "json",
        data: "ID=" + ID,
        url: Url+"?Action=" + action + "&Time=" + (new Date().getTime()),
        error: function() {
            ShowTip("服务运行异常，请联系系统管理员！");
            setTimeout('window.location.reload()', 1000);
        },
        success: function(d) {
            ShowTip(d.returnval);
            setTimeout('window.location.reload()', 1000);
        }
    });
}
/*************************************************
描述：Ajax方法提交参数
作者：Apollo
时间：2009-12-01
*************************************************/
function DoPostForCache(Url, ID, action) {
    $.ajax({
        type: "post",
        dataType: "json",
        data: "ID=" + ID,
        url: Url + "?Action=" + action + "&Time=" + (new Date().getTime()),
        error: function () {
            alert("服务运行异常，请联系系统管理员！");
        },
        success: function (d) {
            alert(d.returnval);
        }
    });
}
/*************************************************
描述：高级检索
作者：Apollo
时间：2010-02-25
*************************************************/
function showObj(id,flag){
	var obj = eval("document.getElementById(\""+id + "\")");
	var showflag;
	if (flag){
		showflag = "block";
	}
	else{
		showflag = "none";
	}
	obj.style.display = showflag;
}
/*************************************************
描述：高级检索
作者：Apollo
时间：2010-02-25
*************************************************/
function showQueDetail(flag){
	if( flag ){
		var obj = eval("document.getElementById(\"div_quedetail\")");
		obj.style.right = "8px";
		obj.style.top = "75px";
	}
	showObj("div_quedetail",flag);
}
/*************************************************
描述：显示浮动操作层,主接口
作者：Apollo
时间：2009-11-20 
*************************************************/
function showTBInterface(url,title,path){
	title = title || "内容管理系统";
	var obj = getTBPath(path);
	var groupflag = getTBConfig("outGroupFlag",path);
	obj.TB_show(title, url, groupflag , obj);
}
/*************************************************
描述：显示浮动操作层
作者：Apollo
时间：2009-11-20 
*************************************************/
function showTB(url,width,height,title,path){
	str = (url.indexOf("?") >= 0) ? "&" : "?";
	showTBInterface(url+str+"TB_iframe=true&height="+height+"&width="+width,title,path);
}
/*************************************************          
描述：显示浮动图片层
作者：Apollo
时间：2009-11-20 
*************************************************/
function showTBPic(url,title,path){
	showTBInterface(url,title,path);
}
/*************************************************
描述：显示浮动动画层
作者：Apollo
时间：2009-11-20 
*************************************************/
function showTBSwf(url,width,height,title,path){
	str = (url.indexOf("?") >= 0) ? "&" : "?";
	showTBInterface(url+str+"TB_flash=true&height="+height+"&width="+width,title,path);
}
/*************************************************
描述：显示浮动操作层
作者：Apollo
时间：2009-11-20 
*************************************************/
function showTBMedia(url,width,height,title,path){
	str = (url.indexOf("?") >= 0) ? "&" : "?";
	showTBInterface(url+str+"TB_media=true&height="+height+"&width="+width,title,path);
}
/*************************************************
描述：显示浮动分页组
作者：Apollo
时间：2009-11-20 
*************************************************/
function showTBGroup(title,url,ary,path){
	//打开组开关
	setTBConfig("outGroupFlag",true,path);
	//设置自定义组数据
	setTBConfig("outGroupAry",ary,path);
	//显示接口
	showTBInterface(url,title,path);
}
/*************************************************
描述：清除浮动操作层
作者：Apollo
返回值：path     显示的路径
	   retval   返回值
时间：2009-11-20
*************************************************/
function removeTB(path){
	if( getTBConfig("attachEventFlag",path) ){
		doTBAttachEvent(path);
	}
	var obj = getTBPath(path);
	obj.TB_remove();
}
/*************************************************
描述：返回属性值
作者：Apollo
时间：2009-11-20 
*************************************************/
function getTBConfig(name,path){
	var obj = getTBPath(path);
	return obj.TB_getConfig(name);
}
/*************************************************
描述：设置属性值
作者：Apollo
返回值：path	    显示的路径
  	   retval	返回值
时间：2009-11-20 
*************************************************/
function setTBConfig( name, value, path ){
	//当有附加事件时，自动打开附加事件开关
	if( name == "attachEvent" && value )
		setTBConfig("attachEventFlag", true, path);
	//当有并列事件时，自动打开并列事件开关
	if( name == "inEvent" && value )
		setTBConfig("inEventFlag", true, path);
	var obj = getTBPath(path);
	obj.TB_setConfig(name,value);
}
/*************************************************
描述：做事件
作者：Apollo
返回值：path	    显示的路径
  	   retval	返回值
时间：2009-11-20 
*************************************************/
function doTBAttachEvent(path){
	var obj = getTBPath(path);
	obj.TB_doEvent();
}
/*************************************************
描述：获取显示默认框架对象
作者：Apollo
返回值：path	    显示的路径
  	   retval	返回值
时间：2009-11-20
*************************************************/
function getTBPath(path){
	path = path || "top";
	var obj = eval(path);
	if(!obj){
		alert("对话框框架对象为空，请刷新或者关闭浏览器重新登录。");
		return false;
	}
	return obj;
}
/*************************************************
描述：鼠标拖动效果部分代码开始
作者：Apollo
时间：2009-11-20
*************************************************/
var drag_x0=0,drag_y0=0,drag_x1=0,drag_y1=0;
var moveable=false;
//开始拖动;
function startDrag(obj){
	//锁定标题栏;
	obj.setCapture();
	//定义对象;
	var win = obj.parentNode;
	//记录鼠标和层位置;
	drag_x0 = event.clientX;
	drag_y0 = event.clientY;
	drag_x1 = parseInt(win.style.left);
	drag_y1 = parseInt(win.style.top);
	moveable = true;
}
//拖动;
function drag(obj){
	var win = obj.parentNode;
	if(moveable){
		win.style.left = drag_x1 + event.clientX - drag_x0;
		win.style.top = drag_y1 + event.clientY - drag_y0;
	}
}
//停止拖动;
function stopDrag(obj){
	//放开标题栏;
	var win = obj.parentNode;
	var t = win.style.top;
	t = parseInt(t.substring(0,t.length - 2));
	if( t < 0 ){
		win.style.top = 5;
	}
	if( top ){
		var h = parseInt(top.document.body.offsetHeight);
	}else{
		var h = parseInt(top.document.body.offsetHeight);
	}
	if( t >= h ){
		win.style.top = 5;
	}
	obj.releaseCapture();
	moveable = false;
}
/*************************************************
描述：鼠标拖动效果部分代码结束
作者：Apollo
时间：2009-11-20
*************************************************/
/****************************************************************************                                                            *
*时间    ：2002-03-15                                                        *
*传入参数：0：24小时；1：18:00——6:00；2：6:00——18:00                       *
*传出参数：                                                                  *
*功能说明：把传入的4位时间数字转换成tt:ss的5位标准时间格式，并进行4舍5入         *
*算法说明：                                                                  *
*修改记录：                                                                  *
*****************************************************************************/
function fFormatTime(that, type) {

    var strTime = that.value;

    //如果已经是标准的5位时间格式，则不进行其它处理
    if (strTime.length == 5) {
        return;
    }

    //如果是''，则不进行其它处理
    if (strTime == '') {
        return;
    }

    //排除异常的输入格式
    if ((strTime.length < 4) || (strTime.indexOf(":") >= 0) || (strTime.substring(2, 5) > "60")) {

        alert("您输入的时间不正确，请核实。");
        that.value = "";
        return;
    }
    //校验输入的时间是否在0000——2400之间，并且位数必须是4位
    if ((strTime < "0000") || (strTime > "2400")) {
        alert("您输入的时间不正确，请重新输入。");
        that.value = "";
        return;
    }


    if (type == 1) {

        //校验输入的时间是否在1800——0600之间，并且位数必须是4位
        if ((!((strTime >= "1800" && strTime <= "2400") || (strTime >= "0000" && strTime <= "0600")))) {
            alert("您输入的夜时间不正确，请重新输入。");
            that.value = "";
            return;
        }
    }
    else if (type == 2) {

        //校验输入的时间是否在0600——1800之间，并且位数必须是4位
        if ((strTime < "0600") || (strTime > "1800")) {
            alert("您输入的日时间不正确，请重新输入。");
            that.value = "";
            return;
        }
    }

    strTime = fFormatTimeCommon(strTime);  //进行时间的公共处理处理

    //把传入的4位时间数字转换成tt:ss的5位标准时间格式(ttss——>tt:ss)
    that.value = strTime.substring(0, 2) + ":" + strTime.substring(2, 4);
}

/****************************************************************************                                                           *
*时间    ：2002-06-04                                                        *
*传入参数：4位的时间                                                          *
*传出参数：                                                                  *
*功能说明：对4位的时间进行处理                                                *
*算法说明：                                                                  *
*修改记录：                                                                  *
*****************************************************************************/
function fFormatTimeCommon(strTime) {

    strTime = fRoundTime(strTime);  //对时间进行四舍五入

    if (strTime.substring(0, 2) == '24')    //如果时间是24点——>0点
    {
        strTime = "00" + strTime.substring(2, strTime.length);
    }

    return strTime;
}

/****************************************************************************                                                          *
*时间    ：2002-04-26                                                        *
*传入参数：4位时间                                                           *
*传出参数：                                                                  *
*功能说明：把传入的4位时间数字进行4舍5入                                       *
*算法说明：                                                                  *
*修改记录：                                                                  *
*****************************************************************************/
function fRoundTime(strTime) {

    //对输入的时间(分)进行4舍5入
    if (strTime.substring(3, 4) < 5)  //4舍
    {
        strTime = strTime.substring(0, 3) + '0';
    }
    else  //5入
    {
        //如果10分位 < 5，则4舍；如果10位分是5则小时需进位，分变为00
        if (strTime.substring(2, 3) < 5)  //10秒
        {
            //alert("10分位 < 5：" + strTime.substring(2,3));
            var Temp = parseInt(strTime.substring(2, 3)) + 1;  //取10分
            strTime = strTime.substring(0, 2) + Temp + '0';
        }
        else  //如果10分是5则小时需进位
        {
            var a = strTime.substring(0, 2);

            var Temp = strTime.substring(0, 2) * 1 + 1;  //取小时

            Temp = Temp + "";

            if (Temp.length < 2)  //如果小时的长度 < 2，前面需要补0
            {
                Temp = "0" + Temp;
            }

            strTime = Temp + '00';
        }
    }

    return strTime;
}
function gotoback(url) {
    url = encodeURI(url);
    location.href = url;
}

////////时间格式化
function Format() {
    this.jsjava_class = "jsjava.text.Format";
}
function DateFormat() {
    this.jsjava_class = "jsjava.text.DateFormat";
}
DateFormat.prototype = new Format();
DateFormat.prototype.constructor = DateFormat;
DateFormat.zh_cn_month2 = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
DateFormat.zh_cn_month3 = ["\u4e00\u6708", "\u4e8c\u6708", "\u4e09\u6708", "\u56db\u6708", "\u4e94\u6708", "\u516d\u6708", "\u4e03\u6708", "\u516b\u6708", "\u4e5d\u6708", "\u5341\u6708", "\u5341\u4e00\u6708", "\u5341\u4e8c\u6708", ];
DateFormat.zh_cn_month4 = ["\u4e00\u6708", "\u4e8c\u6708", "\u4e09\u6708", "\u56db\u6708", "\u4e94\u6708", "\u516d\u6708", "\u4e03\u6708", "\u516b\u6708", "\u4e5d\u6708", "\u5341\u6708", "\u5341\u4e00\u6708", "\u5341\u4e8c\u6708", ];
DateFormat.us_en_month4 = ["Janu", "Febr", "Marc", "Apri", "May", "Juhn", "July", "Augu", "Sept", "Octo", "Nove", "Dece"];
DateFormat.us_en_month3 = ["Jan", "Feb", "Mar", "Apr", "May", "Juh", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
DateFormat.us_en_month2 = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
DateFormat.zh_cn_week = ["\u661f\u671f\u65e5", "\u661f\u671f\u4e00", "\u661f\u671f\u4e8c", "\u661f\u671f\u4e09", "\u661f\u671f\u56db", "\u661f\u671f\u4e94", "\u661f\u671f\u516d"];
DateFormat.zh_cn_am = "\u4e0b\u5348";
DateFormat.zh_cn_pm = "\u4e0a\u5348";
DateFormat.language = (navigator.userLanguage == undefined ? navigator.language : navigator.userLanguage).replace("-", "_").toLowerCase();
/**
* 时间格式化，不足位补0
* @param date 时间
*/
DateFormat.prototype.format = function(date) {
    var year4 = date.getFullYear();
    var year2 = year4.toString().substring(2);
    var pattern = this.pattern;
    pattern = pattern.replace(/yyyy/, year4);
    pattern = pattern.replace(/yy/, year2);
    var month = date.getMonth();
    pattern = pattern.replace(/MMMM/, eval("DateFormat." + DateFormat.language + "_month4[month]"));
    pattern = pattern.replace(/MMM/, eval("DateFormat." + DateFormat.language + "_month3[month]"));
    pattern = pattern.replace(/MM/, eval("DateFormat." + DateFormat.language + "_month2[month]"));
    var dayOfMonth = date.getDate();
    var dayOfMonth2 = dayOfMonth;
    var dayOfMonthLength = dayOfMonth.toString().length;
    if (dayOfMonthLength == 1) {
        dayOfMonth2 = "0" + dayOfMonth;
    }
    pattern = pattern.replace(/dd/, dayOfMonth2);
    pattern = pattern.replace(/d/, dayOfMonth);
    var hours = date.getHours();
    var hours2 = hours;
    var hoursLength = hours.toString().length;
    if (hoursLength == 1) {
        hours2 = "0" + hours;
    }
    pattern = pattern.replace(/hh/, hours2);
    pattern = pattern.replace(/h/, hours);
    var minutes = date.getMinutes();
    var minutes2 = minutes;
    var minutesLength = minutes.toString().length;
    if (minutesLength == 1) {
        minutes2 = "0" + minutes;
    }
    pattern = pattern.replace(/mm/, minutes2);
    pattern = pattern.replace(/m/, minutes);
    var seconds = date.getSeconds();
    var seconds2 = seconds;
    var secondsLength = seconds.toString().length;
    if (secondsLength == 1) {
        seconds2 = "0" + seconds;
    }
    pattern = pattern.replace(/ss/, seconds2);
    pattern = pattern.replace(/s/, seconds);
    var milliSeconds = date.getMilliseconds();
    pattern = pattern.replace(/S+/, milliSeconds);
    var day = date.getDay();
    pattern = pattern.replace(/E+/, eval("DateFormat." + DateFormat.language + "_week[day]"));
    if (hours > 12) {
        pattern = pattern.replace(/a+/, eval("DateFormat." + DateFormat.language + "_am"));
    } else {
        pattern = pattern.replace(/a+/, eval("DateFormat." + DateFormat.language + "_pm"));
    }
    var kHours = hours;
    if (kHours == 0) {
        kHours = 24;
    }
    var kHours2 = kHours;
    var kHoursLength = kHours.toString().length;
    if (kHoursLength == 1) {
        kHours2 = "0" + kHours;
    }
    pattern = pattern.replace(/kk/, kHours2);
    pattern = pattern.replace(/k/, kHours);
    var KHours = hours;
    if (hours > 11) {
        KHours = hours - 12;
    }
    var KHours2 = KHours;
    var KHoursLength = KHours.toString().length;
    if (KHoursLength == 1) {
        KHours2 = "0" + KHours;
    }
    pattern = pattern.replace(/KK/, KHours2);
    pattern = pattern.replace(/K/, KHours);
    var hHours = KHours;
    if (hHours == 0) {
        hHours = 12;
    }
    var hHours2 = hHours;
    var hHoursLength = hHours.toString().length;
    if (KHoursLength == 1) {
        hHours2 = "0" + hHours;
    }
    pattern = pattern.replace(/hh/, hHours2);
    pattern = pattern.replace(/h/, hHours);
    return pattern;

};

function SimpleDateFormat() {
    this.jsjava_class = "jsjava.text.SimpleDateFormat";
}
SimpleDateFormat.prototype = new DateFormat();
SimpleDateFormat.prototype.constructor = SimpleDateFormat;
SimpleDateFormat.prototype.applyPattern = function(pattern) {
    this.pattern = pattern;
 
};

/*************************************************
描述：仅允许输入日期格式
作者：Apollo
时间：2009-11-20
参数：obj       输入框
      objValue  输入的值
      type      时间类型 date为日期型，其他为时间型
      must      返回true：验证通过；返回false：日期非法
*************************************************/
function CheckDate(obj, objValue, type, must) {
    var error = "日期非法，请输入正确的日期，如：2010-01-01 01:01:01";

    if (must == false) {
        if (objValue == "" || objValue == "0000-00-00" || objValue == "0000-00-00 00:00:00")
            return true;
    }
    if (type == "date") {
        var space = objValue.indexOf(" ");
        if (space > -1)
            objValue = objValue.substring(0, objValue.indexOf(" "));
        var pat_hd = /^(\d{4}-((0[1-9]{1})|(1[0-2]{1})|([1-9]{1}))-((0[1-9]{1})|([1-2]{1}[0-9]{1})|(3[0-1]{1}|([1-9]{1})))){1}?$/;
        error = "日期非法，请输入正确的日期，如：2010-01-01";
    } else {
        var pat_hd = /^(\d{4}-((0[1-9]{1})|(1[0-2]{1})|([1-9]{1}))-((0[1-9]{1})|([1-2]{1}[0-9]{1})|(3[0-1]{1}|([1-9]{1})))){1}(\s(((0\d{1})|(1\d{1})|(2[0-4]{1})|(\d{1})):(([0-5]{1}\d{1})|(\d{1})):(([0-5]{1}\d{1})|(\d{1}))))?$/;
    }

    try {
        if (!pat_hd.test(objValue)) throw error;

        var oDate;

        var date;
        var arr_hd;
        var df = new SimpleDateFormat(); //注意2.0版本中使用其子类SimpleDateFormat
        if (type == "date") {
            oDate = objValue;
            arr_hd = oDate.split("-");
            df.applyPattern("yyyy-MM-dd");
            date = new Date(arr_hd[0], arr_hd[1] - 1, arr_hd[2]);
        } else {
            var arr_dt = objValue.split(" ");
            if (arr_dt[0] == '') throw error;
            oDate = arr_dt[0];
            var arr_ht = ["0", "0", "0"];
            if (arr_dt[1] && arr_dt[1] != '') {
                var oTime = arr_dt[1];
                arr_ht = oTime.split(":");
            }
            arr_hd = oDate.split("-");
            df.applyPattern("yyyy-MM-dd hh:mm:ss");
            date = new Date(arr_hd[0], arr_hd[1] - 1, arr_hd[2], arr_ht[0], arr_ht[1], arr_ht[2]);
        }
        var str = df.format(date);
        obj.value = str;
        return true;
    } catch (ex) {
        alert(ex);
        obj.value = "";
        obj.focus();
        return false;
    }
}