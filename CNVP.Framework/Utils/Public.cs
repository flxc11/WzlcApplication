using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Web;

namespace CNVP.Framework.Utils
{
    public class Public
    {
        #region "判断网络状态"
        //定义（引用）API函数  
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int lpdwFlags, int dwReserved);
        /// <summary>
        /// 判断网络状态
        /// </summary>
        /// <returns></returns>
        public static bool IsInternet()
        {
            int lfag = 0;
            bool IsInternet;

            if (InternetGetConnectedState(out lfag, 0))
            {
                IsInternet = true;
            }
            else
            {
                IsInternet = false;
            }
            return IsInternet;
        }
        #endregion
        #region "获取程序路径"
        /// <summary>
        /// 获取程序路径
        /// </summary>
        /// <returns></returns>
        public static string GetProjectPath()
        {
            return Application.StartupPath + "\\";
        }
        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string GetMapPath(string FileName)
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                string StrPath= string.Format("/Config/{0}.config", FileName);

                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Server.MapPath(StrPath);
                }
                else //非web程序引用
                {
                    StrPath = StrPath.Replace("/", "\\");
                    if (StrPath.StartsWith("\\"))
                    {
                        StrPath = StrPath.Substring(StrPath.IndexOf('\\', 1)).TrimStart('\\');
                    }
                    return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StrPath);
                }
            }
            return FileName;
        }
        #endregion
        #region "获取用户地址"
        /// <summary>
        /// 获取用户IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetUserIP()
        {
            string _UserIP = string.Empty;
            try
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    _UserIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    _UserIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                }
            }
            catch (Exception ex)
            {
                _UserIP = ex.ToString();
            }
            return _UserIP;
        }
        #endregion
        #region "获取访问地址"
        /// <summary>
        /// 获取网站访问地址
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            string Str = string.Empty;
            HttpRequest Request = HttpContext.Current.Request;
            if (!Request.Url.IsDefaultPort)
            {
                Str = string.Format("{0}:{1}", Request.Url.Host, Request.Url.Port.ToString());
            }
            else
            {
                Str= Request.Url.Host;
            }
            return Str.ToLower();
        }
        #endregion
        #region "根据类型转换"
        /// <summary>
        /// 根据类型转换值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static object GetDefaultValue(object obj, Type type)
        {
            try
            {
                if (obj == null || obj == DBNull.Value)
                {
                    obj = default(object);
                }
                else
                {
                    if (type == typeof(String))
                        obj = obj.ToString().Trim();
                    obj = Convert.ChangeType(obj, Nullable.GetUnderlyingType(type) ?? type);
                }
                return obj;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region "过滤特殊符号"
        /// <summary>
        /// 过滤特殊特号(完全过滤)
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string FilterSql(string Str)
        {
            string[] aryReg = { "'", "\"", "\r", "\n", "<", ">", "%", "?", ",", "=", "-", "_", ";", "|", "[", "]", "&", "/" };
            if (!string.IsNullOrEmpty(Str))
            {
                foreach (string str in aryReg)
                {
                    Str = Str.Replace(str, string.Empty);
                }
                return Str;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// Json特符字符过滤，参见http://www.json.org/
        /// </summary>
        /// <param name="sourceStr">要过滤的源字符串</param>
        /// <returns>返回过滤的字符串</returns>
        public static string FilterJson(string sourceStr)
        {
            sourceStr = sourceStr.Replace("\\", "\\\\");
            sourceStr = sourceStr.Replace("\b", "\\\b");
            sourceStr = sourceStr.Replace("\t", "\\\t");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\f", "\\\f");
            sourceStr = sourceStr.Replace("\r", "\\\r");
            return sourceStr.Replace("\"", "\\\"");
        }
        #endregion
        #region "字符格式检查"
        /// <summary>
        /// 固定电话格式判断
        /// </summary>
        /// <param name="TelPhone">固定电话</param>
        /// <returns></returns>
        public static bool IsTelphone(string TelPhone)
        {
            return Regex.IsMatch(TelPhone, @"^(\d{3,4}-)?\d{6,8}$");
        }
        /// <summary>
        /// 手机号码格式判断
        /// </summary>
        /// <param name="Mobile">手机号码</param>
        /// <returns></returns>
        public static bool IsMobile(string Mobile)
        {
            return Regex.IsMatch(Mobile, @"^[1]+[3,5]+\d{9}");
        }
        /// <summary>
        /// 身份证号码判断
        /// </summary>
        /// <param name="IDCard">身份证号码</param>
        /// <returns></returns>
        public static bool IsIDCard(string IDCard)
        {
            return Regex.IsMatch(IDCard, @"(^\d{18}$)|(^\d{15}$)");
        }
        /// <summary>
        /// 数字格式判断
        /// </summary>
        /// <param name="Number">数字</param>
        /// <returns></returns>
        public static bool IsNumber(string Number)
        {
            return Regex.IsMatch(Number, @"^[0-9]*$");
        }
        /// <summary>
        /// 浮点数字判断
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static bool IsDecimal(string Number)
        {
            return Regex.IsMatch(Number, @"^\d+\.\d+$");
        }
        /// <summary>
        /// 邮政编码格式判断
        /// </summary>
        /// <param name="PostCode">邮政编码</param>
        /// <returns></returns>
        public static bool IsPostCode(string PostCode)
        {
            return Regex.IsMatch(PostCode, @"^\d{6}$");
        }
        /// <summary>
        /// 邮件地址格式判断
        /// </summary>
        /// <param name="Email">邮件地址</param>
        /// <returns></returns>
        public static bool IsEmail(string Email)
        {
            return Regex.IsMatch(Email, @"\\w{1,}@\\w{1,}\\.\\w{1,}");
        }
        #endregion
        #region "字符格式转化"
        /// <summary>
        /// 首字母转大写
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        //public static string StrToTitleCase(string Str)
        //{
        //    Str = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Str);
        //    return Str;
        //}
        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>首字母大写</returns>
        public static string GetStrUpper(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = Strings.StrConv(str, VbStrConv.ProperCase,
                    System.Globalization.CultureInfo.CurrentCulture.LCID);
            }
            return str;
        }
        #endregion
        #region "转成汉字数字"
        /// <summary>
        /// 转成汉字数字
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string ConvertToChinese(double x)
        {
            string s = x.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A"); string d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}"); return Regex.Replace(d, ".", delegate(Match m) { return "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString(); });
        }
        /// <summary>
        /// 转成汉字数字
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string ConvertToChinese(int x)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(0, "零");
            dic.Add(1, "一");
            dic.Add(2, "二");
            dic.Add(3, "三");
            dic.Add(4, "四");
            dic.Add(5, "五");
            dic.Add(6, "六");
            dic.Add(7, "七");
            dic.Add(8, "八");
            dic.Add(9, "九");

            string svalue = string.Empty;
            foreach (int key in dic.Keys)
            {
                if (key == x)
                {
                    svalue = dic[key];
                    break;
                }
            }
            return svalue;
        }
        #endregion
        #region "日期时间格式"
        /// <summary>
        /// 返回标准日期(年-月-日)
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 返回标准日期(年-月-日)
        /// </summary>
        /// <param name="Days"></param>
        /// <returns></returns>
        public static string GetDate(int Days)
        {
            return DateTime.Now.AddDays(Days).ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 返回标准日期(年-月-日)
        /// </summary>
        /// <param name="dataTime">日期时间</param>
        /// <returns></returns>
        public static string GetDate(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("yyyy-MM-dd");
                }
                catch
                {

                }
            }
            return Str;
        }
        /// <summary>
        /// 返回标准时间(时-分-秒)
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        /// <summary>
        /// 返回标准时间(年-月-日 时-分-秒)
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回标准时间(年-月-日 时-分-秒)
        /// </summary>
        /// <param name="Days"></param>
        /// <returns></returns>
        public static string GetDateTime(int Days)
        {
            return DateTime.Now.AddDays(Days).ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回标准时间(年-月-日 时-分-秒)
        /// </summary>
        /// <param name="dataTime">日期时间</param>
        /// <returns></returns>
        public static string GetDateTime(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch
                {
                }
            }
            return Str;
        }
        /// <summary>
        /// 返回标准时间(四位年)
        /// </summary>
        /// <param name="dataTime"></param>
        /// <returns></returns>
        public static string GetYear(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("yyyy");
                }
                catch
                {
                }
            }
            return Str;
        }
        /// <summary>
        /// 返回标准时间(两位月)
        /// </summary>
        /// <param name="dataTime"></param>
        /// <returns></returns>
        public static string GetMonth(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("MM");
                }
                catch
                {
                }
            }
            return Str;
        }
        /// <summary>
        /// 返回月份间隔
        /// </summary>
        /// <param name="dataTime1">起始时间</param>
        /// <param name="dataTime2">结束时间</param>
        /// <returns></returns>
        public static string GetMonth(string dataTime1, string dataTime2)
        {
            int Month = 0;
            DateTime dtbegin = Convert.ToDateTime(dataTime1);
            DateTime dtend = Convert.ToDateTime(dataTime2);
            Month = (dtend.Year - dtbegin.Year) * 12 + (dtend.Month - dtbegin.Month);
            return Month.ToString();
        }
        /// <summary>
        /// 返回标准时间(两位日)
        /// </summary>
        /// <param name="dataTime"></param>
        /// <returns></returns>
        public static string GetDay(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("dd");
                }
                catch
                {
                }
            }
            return Str;
        }
        /// <summary>
        /// 返回标准时间(两位时)
        /// </summary>
        /// <param name="dataTime"></param>
        /// <returns></returns>
        public static string GetHour(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("HH");
                }
                catch
                {
                }
            }
            return Str;
        }
        /// <summary>
        /// 返回标准时间(两位分)
        /// </summary>
        /// <param name="dataTime"></param>
        /// <returns></returns>
        public static string GetMinute(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("mm");
                }
                catch
                {
                }
            }
            return Str;
        }
        /// <summary>
        /// 返回标准时间(两位秒)
        /// </summary>
        /// <param name="dataTime"></param>
        /// <returns></returns>
        public static string GetSecond(string dataTime)
        {
            string Str = dataTime;
            if (!string.IsNullOrEmpty(dataTime))
            {
                try
                {
                    Str = Convert.ToDateTime(dataTime).ToString("ss");
                }
                catch
                {
                }
            }
            return Str;
        }
        /// <summary>
        /// 返回标准时间(年-月-日 时-分-秒-毫秒)
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }
        /// <summary>
        /// 返回相对时间
        /// </summary>
        /// <param name="fDateTime">相对时间</param>
        /// <param name="formatStr">日期时间格式</param>
        /// <returns></returns>
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
                return fDateTime;
            DateTime time = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            if (DateTime.TryParse(fDateTime, out time))
                return time.ToString(formatStr);
            else
                return "N/A";
        }
        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH:mm:ss
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回标准时间 yyyy-MM-dd
        /// </sumary>
        public static string GetStandardDate(string fDate)
        {
            return GetStandardDateTime(fDate, "yyyy-MM-dd");
        }
        /// <summary>
        /// 返回时间格式
        /// </summary>
        /// <param name="Time1">例如：20130824144324</param>
        /// <returns></returns>
        public static string GetDateDiffTime(string Time1)
        {
            string Str=Time1;
            if (Time1.Length == 14)
            {
                Str = string.Format("{0}-{1}-{2} {3}:{4}:{5}", Time1.Substring(0, 4), Time1.Substring(4, 2), Time1.Substring(6, 2), Time1.Substring(8, 2), Time1.Substring(10, 2), Time1.Substring(12, 2));
            }
            return Str;
        }
        /// <summary>
        /// 返回时间差
        /// </summary>
        /// <param name="Time1"></param>
        /// <returns></returns>
        public static int DateDiff(DateTime Time1)
        {
            return DateDiff(Time1, DateTime.Now);
        }
        /// <summary>
        /// 返回时间差
        /// </summary>
        /// <param name="Time1"></param>
        /// <param name="Time2"></param>
        /// <returns></returns>
        public static int DateDiff(DateTime Time1, DateTime Time2)
        {
            TimeSpan ts = Time2 - Time1;
            return Convert.ToInt32(ts.TotalSeconds);
        }
        #endregion
        #region "程序进度样式"
        /// <summary>
        /// 重写HTML代码(实现进度条功能)
        /// </summary>
        /// <returns></returns>
        public static void RewriteHTML()
        {
            string Str = @"<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
<title></title>
</head>
<script" + @" language=""javascript"">
//开始处理
function BeginTrans(msg) {
WriteText(""Msg1"", msg);
}
//设置进度条进度
function SetPorgressBar(msg, pos) {
ProgressBar.style.width = pos + ""%"";
WriteText(""Msg1"", msg + "" 已完成"" + pos + ""%"");
}
//处理结束
function EndTrans(msg) {
if (msg == """")
WriteText(""Msg1"", ""完成。"");
else
WriteText(""Msg1"", msg);
}
//设置时间信息
function SetTimeInfo(msg) {
WriteText(""Msg2"", msg);
}
// 更新文本显示信息
function WriteText(id, str) {
var strTag = '<font face=""Verdana, Arial, Helvetica"" size=""2"" color=""#ea9b02""><B>' + str + '</B></font>';
document.getElementById(id).innerHTML = strTag;
}
<" + @"/script>
<body>
<table align=""center"" style=""height:100%;display:none"" id=""main"">
<tr>
<td>
<div id=""Msg1"" style=""height:16px;""><font face=""Verdana, Arial, Helvetica"" size=""2"" color=""#ea9b02""><b>正在加载...</b></font></div>
<div id=""ProgressBarSide"" style=""width:380px; color:Silver;border-width:1px; border-style:Solid;"">
<div id=""ProgressBar"" align=""center"" style=""height:20px; width:0%; background-color:#316AC5;""></div>
</div>
<div id=""Msg2"" style=""height:16px;""><font face=""Verdana, Arial, Helvetica"" size=""2"" color=""#ea9b02""><b></b></font></div>
</td>
</tr>
</table>
</body>
</html>";
            HttpContext.Current.Response.Write(Str);
            HttpContext.Current.Response.Flush();
        }
        #endregion
        #region "获取汉字拼音"
        /// <summary>
        /// 获取汉字拼音
        /// </summary>
        /// <param name="hz"></param>
        /// <returns></returns>
        public static string GetFirstLetter(string hz)
        {
            string ls_second_eng = "CJWGNSPGCGNESYPBTYYZDXYKYGTDJNNJQMBSGZSCYJSYYQPGKBZGYCYWJKGKLJSWKPJQHYTWDDZLSGMRYPYWWCCKZNKYDGTTNGJEYKKZYTCJNMCYLQLYPYQFQRPZSLWBTGKJFYXJWZLTBNCXJJJJZXDTTSQZYCDXXHGCKBPHFFSSWYBGMXLPBYLLLHLXSPZMYJHSOJNGHDZQYKLGJHSGQZHXQGKEZZWYSCSCJXYEYXADZPMDSSMZJZQJYZCDJZWQJBDZBXGZNZCPWHKXHQKMWFBPBYDTJZZKQHYLYGXFPTYJYYZPSZLFCHMQSHGMXXSXJJSDCSBBQBEFSJYHWWGZKPYLQBGLDLCCTNMAYDDKSSNGYCSGXLYZAYBNPTSDKDYLHGYMYLCXPYCJNDQJWXQXFYYFJLEJBZRXCCQWQQSBNKYMGPLBMJRQCFLNYMYQMSQTRBCJTHZTQFRXQ" +
            "HXMJJCJLXQGJMSHZKBSWYEMYLTXFSYDSGLYCJQXSJNQBSCTYHBFTDCYZDJWYGHQFRXWCKQKXEBPTLPXJZSRMEBWHJLBJSLYYSMDXLCLQKXLHXJRZJMFQHXHWYWSBHTRXXGLHQHFNMNYKLDYXZPWLGGTMTCFPAJJZYLJTYANJGBJPLQGDZYQYAXBKYSECJSZNSLYZHZXLZCGHPXZHZNYTDSBCJKDLZAYFMYDLEBBGQYZKXGLDNDNYSKJSHDLYXBCGHXYPKDJMMZNGMMCLGWZSZXZJFZNMLZZTHCSYDBDLLSCDDNLKJYKJSYCJLKOHQASDKNHCSGANHDAASHTCPLCPQYBSDMPJLPCJOQLCDHJJYSPRCHNWJNLHLYYQYYWZPTCZGWWMZFFJQQQQYXACLBHKDJXDGMMYDJXZLLSYGXGKJRYWZWYCLZMSSJZLDBYDCFCXYHLXCHYZJQSFQAGMNYXPFRKSSB" +
            "JLYXYSYGLNSCMHCWWMNZJJLXXHCHSYDSTTXRYCYXBYHCSMXJSZNPWGPXXTAYBGAJCXLYSDCCWZOCWKCCSBNHCPDYZNFCYYTYCKXKYBSQKKYTQQXFCWCHCYKELZQBSQYJQCCLMTHSYWHMKTLKJLYCXWHEQQHTQHZPQSQSCFYMMDMGBWHWLGSSLYSDLMLXPTHMJHWLJZYHZJXHTXJLHXRSWLWZJCBXMHZQXSDZPMGFCSGLSXYMJSHXPJXWMYQKSMYPLRTHBXFTPMHYXLCHLHLZYLXGSSSSTCLSLDCLRPBHZHXYYFHBBGDMYCNQQWLQHJJZYWJZYEJJDHPBLQXTQKWHLCHQXAGTLXLJXMSLXHTZKZJECXJCJNMFBYCSFYWYBJZGNYSDZSQYRSLJPCLPWXSDWEJBJCBCNAYTWGMPAPCLYQPCLZXSBNMSGGFNZJJBZSFZYNDXHPLQKZCZWALSBCCJXJYZGWKYP" +
            "SGXFZFCDKHJGXDLQFSGDSLQWZKXTMHSBGZMJZRGLYJBPMLMSXLZJQQHZYJCZYDJWBMJKLDDPMJEGXYHYLXHLQYQHKYCWCJMYYXNATJHYCCXZPCQLBZWWYTWBQCMLPMYRJCCCXFPZNZZLJPLXXYZTZLGDLDCKLYRZZGQTGJHHHJLJAXFGFJZSLCFDQZLCLGJDJCSNCLLJPJQDCCLCJXMYZFTSXGCGSBRZXJQQCTZHGYQTJQQLZXJYLYLBCYAMCSTYLPDJBYREGKLZYZHLYSZQLZNWCZCLLWJQJJJKDGJZOLBBZPPGLGHTGZXYGHZMYCNQSYCYHBHGXKAMTXYXNBSKYZZGJZLQJDFCJXDYGJQJJPMGWGJJJPKQSBGBMMCJSSCLPQPDXCDYYKYFCJDDYYGYWRHJRTGZNYQLDKLJSZZGZQZJGDYKSHPZMTLCPWNJAFYZDJCNMWESCYGLBTZCGMSSLLYXQSXSBSJS" +
            "BBSGGHFJLWPMZJNLYYWDQSHZXTYYWHMCYHYWDBXBTLMSYYYFSXJCSDXXLHJHFSSXZQHFZMZCZTQCXZXRTTDJHNNYZQQMNQDMMGYYDXMJGDHCDYZBFFALLZTDLTFXMXQZDNGWQDBDCZJDXBZGSQQDDJCMBKZFFXMKDMDSYYSZCMLJDSYNSPRSKMKMPCKLGDBQTFZSWTFGGLYPLLJZHGJJGYPZLTCSMCNBTJBQFKTHBYZGKPBBYMTTSSXTBNPDKLEYCJNYCDYKZDDHQHSDZSCTARLLTKZLGECLLKJLQJAQNBDKKGHPJTZQKSECSHALQFMMGJNLYJBBTMLYZXDCJPLDLPCQDHZYCBZSCZBZMSLJFLKRZJSNFRGJHXPDHYJYBZGDLQCSEZGXLBLGYXTWMABCHECMWYJYZLLJJYHLGBDJLSLYGKDZPZXJYYZLWCXSZFGWYYDLYHCLJSCMBJHBLYZLYCBLYDPDQYSXQZB" +
            "YTDKYXJYYCNRJMDJGKLCLJBCTBJDDBBLBLCZQRPXJCGLZCSHLTOLJNMDDDLNGKAQHQHJGYKHEZNMSHRPHQQJCHGMFPRXHJGDYCHGHLYRZQLCYQJNZSQTKQJYMSZSWLCFQQQXYFGGYPTQWLMCRNFKKFSYYLQBMQAMMMYXCTPSHCPTXXZZSMPHPSHMCLMLDQFYQXSZYJDJJZZHQPDSZGLSTJBCKBXYQZJSGPSXQZQZRQTBDKYXZKHHGFLBCSMDLDGDZDBLZYYCXNNCSYBZBFGLZZXSWMSCCMQNJQSBDQSJTXXMBLTXZCLZSHZCXRQJGJYLXZFJPHYMZQQYDFQJJLZZNZJCDGZYGCTXMZYSCTLKPHTXHTLBJXJLXSCDQXCBBTJFQZFSLTJBTKQBXXJJLJCHCZDBZJDCZJDCPRNPQCJPFCZLCLZXZDMXMPHJSGZGSZZQJYLWTJPFSYASMCJBTZKYCWMYTCSJJLJCQLWZM" +
            "ALBXYFBPNLSFHTGJWEJJXXGLLJSTGSHJQLZFKCGNNDSZFDEQFHBSAQTGLLBXMMYGSZLDYDQMJJRGBJTKGDHGKBLQKBDMBYLXWCXYTTYBKMRTJZXQJBHLMHMJJZMQASLDCYXYQDLQCAFYWYXQHZ";
            string ls_second_ch = "亍丌兀丐廿卅丕亘丞鬲孬噩丨禺丿匕乇夭爻卮氐囟胤馗毓睾鼗丶亟" +
            "鼐乜乩亓芈孛啬嘏仄厍厝厣厥厮靥赝匚叵匦匮匾赜卦卣刂刈刎刭刳刿剀剌剞剡剜蒯剽劂劁劐劓冂罔亻仃仉仂仨仡仫仞伛仳伢佤仵伥伧伉伫佞佧攸佚佝佟佗伲伽佶佴侑侉侃侏佾佻侪佼侬侔俦俨俪俅俚俣俜俑俟俸倩偌俳倬倏倮倭俾倜倌倥倨偾偃偕偈偎偬偻傥傧傩傺僖儆僭僬僦僮儇儋仝氽佘佥俎龠汆籴兮巽黉馘冁夔勹匍訇匐凫夙兕亠兖亳衮袤亵脔裒禀嬴蠃羸冫冱冽冼凇冖冢冥讠讦讧讪讴讵讷诂诃诋诏诎诒诓诔诖诘诙诜诟诠诤诨诩诮诰诳诶诹诼诿谀谂谄谇谌谏谑谒谔谕谖谙谛谘谝谟谠谡谥谧谪谫谮谯谲谳谵谶卩卺阝阢阡阱阪阽阼" +
            "陂陉陔陟陧陬陲陴隈隍隗隰邗邛邝邙邬邡邴邳邶邺邸邰郏郅邾郐郄郇郓郦郢郜郗郛郫郯郾鄄鄢鄞鄣鄱鄯鄹酃酆刍奂劢劬劭劾哿勐勖勰叟燮矍廴凵凼鬯厶弁畚巯坌垩垡塾墼壅壑圩圬圪圳圹圮圯坜圻坂坩垅坫垆坼坻坨坭坶坳垭垤垌垲埏垧垴垓垠埕埘埚埙埒垸埴埯埸埤埝堋堍埽埭堀堞堙塄堠塥塬墁墉墚墀馨鼙懿艹艽艿芏芊芨芄芎芑芗芙芫芸芾芰苈苊苣芘芷芮苋苌苁芩芴芡芪芟苄苎芤苡茉苷苤茏茇苜苴苒苘茌苻苓茑茚茆茔茕苠苕茜荑荛荜茈莒茼茴茱莛荞茯荏荇荃荟荀茗荠茭茺茳荦荥荨茛荩荬荪荭荮莰荸莳莴莠莪莓莜莅荼莶莩荽莸荻" +
            "莘莞莨莺莼菁萁菥菘堇萘萋菝菽菖萜萸萑萆菔菟萏萃菸菹菪菅菀萦菰菡葜葑葚葙葳蒇蒈葺蒉葸萼葆葩葶蒌蒎萱葭蓁蓍蓐蓦蒽蓓蓊蒿蒺蓠蒡蒹蒴蒗蓥蓣蔌甍蔸蓰蔹蔟蔺蕖蔻蓿蓼蕙蕈蕨蕤蕞蕺瞢蕃蕲蕻薤薨薇薏蕹薮薜薅薹薷薰藓藁藜藿蘧蘅蘩蘖蘼廾弈夼奁耷奕奚奘匏尢尥尬尴扌扪抟抻拊拚拗拮挢拶挹捋捃掭揶捱捺掎掴捭掬掊捩掮掼揲揸揠揿揄揞揎摒揆掾摅摁搋搛搠搌搦搡摞撄摭撖摺撷撸撙撺擀擐擗擤擢攉攥攮弋忒甙弑卟叱叽叩叨叻吒吖吆呋呒呓呔呖呃吡呗呙吣吲咂咔呷呱呤咚咛咄呶呦咝哐咭哂咴哒咧咦哓哔呲咣哕咻咿哌哙哚哜咩" +
            "咪咤哝哏哞唛哧唠哽唔哳唢唣唏唑唧唪啧喏喵啉啭啁啕唿啐唼唷啖啵啶啷唳唰啜喋嗒喃喱喹喈喁喟啾嗖喑啻嗟喽喾喔喙嗪嗷嗉嘟嗑嗫嗬嗔嗦嗝嗄嗯嗥嗲嗳嗌嗍嗨嗵嗤辔嘞嘈嘌嘁嘤嘣嗾嘀嘧嘭噘嘹噗嘬噍噢噙噜噌噔嚆噤噱噫噻噼嚅嚓嚯囔囗囝囡囵囫囹囿圄圊圉圜帏帙帔帑帱帻帼帷幄幔幛幞幡岌屺岍岐岖岈岘岙岑岚岜岵岢岽岬岫岱岣峁岷峄峒峤峋峥崂崃崧崦崮崤崞崆崛嵘崾崴崽嵬嵛嵯嵝嵫嵋嵊嵩嵴嶂嶙嶝豳嶷巅彳彷徂徇徉後徕徙徜徨徭徵徼衢彡犭犰犴犷犸狃狁狎狍狒狨狯狩狲狴狷猁狳猃狺狻猗猓猡猊猞猝猕猢猹猥猬猸猱獐獍獗獠獬獯獾" +
            "舛夥飧夤夂饣饧饨饩饪饫饬饴饷饽馀馄馇馊馍馐馑馓馔馕庀庑庋庖庥庠庹庵庾庳赓廒廑廛廨廪膺忄忉忖忏怃忮怄忡忤忾怅怆忪忭忸怙怵怦怛怏怍怩怫怊怿怡恸恹恻恺恂恪恽悖悚悭悝悃悒悌悛惬悻悱惝惘惆惚悴愠愦愕愣惴愀愎愫慊慵憬憔憧憷懔懵忝隳闩闫闱闳闵闶闼闾阃阄阆阈阊阋阌阍阏阒阕阖阗阙阚丬爿戕氵汔汜汊沣沅沐沔沌汨汩汴汶沆沩泐泔沭泷泸泱泗沲泠泖泺泫泮沱泓泯泾洹洧洌浃浈洇洄洙洎洫浍洮洵洚浏浒浔洳涑浯涞涠浞涓涔浜浠浼浣渚淇淅淞渎涿淠渑淦淝淙渖涫渌涮渫湮湎湫溲湟溆湓湔渲渥湄滟溱溘滠漭滢溥溧溽溻溷滗溴滏溏滂" +
            "溟潢潆潇漤漕滹漯漶潋潴漪漉漩澉澍澌潸潲潼潺濑濉澧澹澶濂濡濮濞濠濯瀚瀣瀛瀹瀵灏灞宀宄宕宓宥宸甯骞搴寤寮褰寰蹇謇辶迓迕迥迮迤迩迦迳迨逅逄逋逦逑逍逖逡逵逶逭逯遄遑遒遐遨遘遢遛暹遴遽邂邈邃邋彐彗彖彘尻咫屐屙孱屣屦羼弪弩弭艴弼鬻屮妁妃妍妩妪妣妗姊妫妞妤姒妲妯姗妾娅娆姝娈姣姘姹娌娉娲娴娑娣娓婀婧婊婕娼婢婵胬媪媛婷婺媾嫫媲嫒嫔媸嫠嫣嫱嫖嫦嫘嫜嬉嬗嬖嬲嬷孀尕尜孚孥孳孑孓孢驵驷驸驺驿驽骀骁骅骈骊骐骒骓骖骘骛骜骝骟骠骢骣骥骧纟纡纣纥纨纩纭纰纾绀绁绂绉绋绌绐绔绗绛绠绡绨绫绮绯绱绲缍绶绺绻绾缁缂缃" +
            "缇缈缋缌缏缑缒缗缙缜缛缟缡缢缣缤缥缦缧缪缫缬缭缯缰缱缲缳缵幺畿巛甾邕玎玑玮玢玟珏珂珑玷玳珀珉珈珥珙顼琊珩珧珞玺珲琏琪瑛琦琥琨琰琮琬琛琚瑁瑜瑗瑕瑙瑷瑭瑾璜璎璀璁璇璋璞璨璩璐璧瓒璺韪韫韬杌杓杞杈杩枥枇杪杳枘枧杵枨枞枭枋杷杼柰栉柘栊柩枰栌柙枵柚枳柝栀柃枸柢栎柁柽栲栳桠桡桎桢桄桤梃栝桕桦桁桧桀栾桊桉栩梵梏桴桷梓桫棂楮棼椟椠棹椤棰椋椁楗棣椐楱椹楠楂楝榄楫榀榘楸椴槌榇榈槎榉楦楣楹榛榧榻榫榭槔榱槁槊槟榕槠榍槿樯槭樗樘橥槲橄樾檠橐橛樵檎橹樽樨橘橼檑檐檩檗檫猷獒殁殂殇殄殒殓殍殚殛殡殪轫轭轱轲轳轵轶" +
            "轸轷轹轺轼轾辁辂辄辇辋辍辎辏辘辚軎戋戗戛戟戢戡戥戤戬臧瓯瓴瓿甏甑甓攴旮旯旰昊昙杲昃昕昀炅曷昝昴昱昶昵耆晟晔晁晏晖晡晗晷暄暌暧暝暾曛曜曦曩贲贳贶贻贽赀赅赆赈赉赇赍赕赙觇觊觋觌觎觏觐觑牮犟牝牦牯牾牿犄犋犍犏犒挈挲掰搿擘耄毪毳毽毵毹氅氇氆氍氕氘氙氚氡氩氤氪氲攵敕敫牍牒牖爰虢刖肟肜肓肼朊肽肱肫肭肴肷胧胨胩胪胛胂胄胙胍胗朐胝胫胱胴胭脍脎胲胼朕脒豚脶脞脬脘脲腈腌腓腴腙腚腱腠腩腼腽腭腧塍媵膈膂膑滕膣膪臌朦臊膻臁膦欤欷欹歃歆歙飑飒飓飕飙飚殳彀毂觳斐齑斓於旆旄旃旌旎旒旖炀炜炖炝炻烀炷炫炱烨烊焐焓焖焯焱" +
            "煳煜煨煅煲煊煸煺熘熳熵熨熠燠燔燧燹爝爨灬焘煦熹戾戽扃扈扉礻祀祆祉祛祜祓祚祢祗祠祯祧祺禅禊禚禧禳忑忐怼恝恚恧恁恙恣悫愆愍慝憩憝懋懑戆肀聿沓泶淼矶矸砀砉砗砘砑斫砭砜砝砹砺砻砟砼砥砬砣砩硎硭硖硗砦硐硇硌硪碛碓碚碇碜碡碣碲碹碥磔磙磉磬磲礅磴礓礤礞礴龛黹黻黼盱眄眍盹眇眈眚眢眙眭眦眵眸睐睑睇睃睚睨睢睥睿瞍睽瞀瞌瞑瞟瞠瞰瞵瞽町畀畎畋畈畛畲畹疃罘罡罟詈罨罴罱罹羁罾盍盥蠲钅钆钇钋钊钌钍钏钐钔钗钕钚钛钜钣钤钫钪钭钬钯钰钲钴钶钷钸钹钺钼钽钿铄铈铉铊铋铌铍铎铐铑铒铕铖铗铙铘铛铞铟铠铢铤铥铧铨铪铩铫铮铯铳铴铵铷铹铼" +
            "铽铿锃锂锆锇锉锊锍锎锏锒锓锔锕锖锘锛锝锞锟锢锪锫锩锬锱锲锴锶锷锸锼锾锿镂锵镄镅镆镉镌镎镏镒镓镔镖镗镘镙镛镞镟镝镡镢镤镥镦镧镨镩镪镫镬镯镱镲镳锺矧矬雉秕秭秣秫稆嵇稃稂稞稔稹稷穑黏馥穰皈皎皓皙皤瓞瓠甬鸠鸢鸨鸩鸪鸫鸬鸲鸱鸶鸸鸷鸹鸺鸾鹁鹂鹄鹆鹇鹈鹉鹋鹌鹎鹑鹕鹗鹚鹛鹜鹞鹣鹦鹧鹨鹩鹪鹫鹬鹱鹭鹳疒疔疖疠疝疬疣疳疴疸痄疱疰痃痂痖痍痣痨痦痤痫痧瘃痱痼痿瘐瘀瘅瘌瘗瘊瘥瘘瘕瘙瘛瘼瘢瘠癀瘭瘰瘿瘵癃瘾瘳癍癞癔癜癖癫癯翊竦穸穹窀窆窈窕窦窠窬窨窭窳衤衩衲衽衿袂裆袷袼裉裢裎裣裥裱褚裼裨裾裰褡褙褓褛褊褴褫褶襁襦疋胥皲皴矜耒" +
            "耔耖耜耠耢耥耦耧耩耨耱耋耵聃聆聍聒聩聱覃顸颀颃颉颌颍颏颔颚颛颞颟颡颢颥颦虍虔虬虮虿虺虼虻蚨蚍蚋蚬蚝蚧蚣蚪蚓蚩蚶蛄蚵蛎蚰蚺蚱蚯蛉蛏蚴蛩蛱蛲蛭蛳蛐蜓蛞蛴蛟蛘蛑蜃蜇蛸蜈蜊蜍蜉蜣蜻蜞蜥蜮蜚蜾蝈蜴蜱蜩蜷蜿螂蜢蝽蝾蝻蝠蝰蝌蝮螋蝓蝣蝼蝤蝙蝥螓螯螨蟒蟆螈螅螭螗螃螫蟥螬螵螳蟋蟓螽蟑蟀蟊蟛蟪蟠蟮蠖蠓蟾蠊蠛蠡蠹蠼缶罂罄罅舐竺竽笈笃笄笕笊笫笏筇笸笪笙笮笱笠笥笤笳笾笞筘筚筅筵筌筝筠筮筻筢筲筱箐箦箧箸箬箝箨箅箪箜箢箫箴篑篁篌篝篚篥篦篪簌篾篼簏簖簋簟簪簦簸籁籀臾舁舂舄臬衄舡舢舣舭舯舨舫舸舻舳舴舾艄艉艋艏艚艟艨衾袅袈裘裟襞羝羟" +
            "羧羯羰羲籼敉粑粝粜粞粢粲粼粽糁糇糌糍糈糅糗糨艮暨羿翎翕翥翡翦翩翮翳糸絷綦綮繇纛麸麴赳趄趔趑趱赧赭豇豉酊酐酎酏酤酢酡酰酩酯酽酾酲酴酹醌醅醐醍醑醢醣醪醭醮醯醵醴醺豕鹾趸跫踅蹙蹩趵趿趼趺跄跖跗跚跞跎跏跛跆跬跷跸跣跹跻跤踉跽踔踝踟踬踮踣踯踺蹀踹踵踽踱蹉蹁蹂蹑蹒蹊蹰蹶蹼蹯蹴躅躏躔躐躜躞豸貂貊貅貘貔斛觖觞觚觜觥觫觯訾謦靓雩雳雯霆霁霈霏霎霪霭霰霾龀龃龅龆龇龈龉龊龌黾鼋鼍隹隼隽雎雒瞿雠銎銮鋈錾鍪鏊鎏鐾鑫鱿鲂鲅鲆鲇鲈稣鲋鲎鲐鲑鲒鲔鲕鲚鲛鲞鲟鲠鲡鲢鲣鲥鲦鲧鲨鲩鲫鲭鲮鲰鲱鲲鲳鲴鲵鲶鲷鲺鲻鲼鲽鳄鳅鳆鳇鳊鳋鳌鳍鳎鳏鳐鳓鳔" +
            "鳕鳗鳘鳙鳜鳝鳟鳢靼鞅鞑鞒鞔鞯鞫鞣鞲鞴骱骰骷鹘骶骺骼髁髀髅髂髋髌髑魅魃魇魉魈魍魑飨餍餮饕饔髟髡髦髯髫髻髭髹鬈鬏鬓鬟鬣麽麾縻麂麇麈麋麒鏖麝麟黛黜黝黠黟黢黩黧黥黪黯鼢鼬鼯鼹鼷鼽鼾齄";
            byte[] array = new byte[2];

            string return_py = "";
            for (int i = 0; i < hz.Length; i++)
            {
                array = System.Text.Encoding.Default.GetBytes(hz[i].ToString());
                if (array[0] < 176) //.非汉字 
                {
                    return_py += hz[i];
                }
                else if (array[0] >= 176 && array[0] <= 215) //一级汉字 
                {

                    if (hz[i].ToString().CompareTo("匝") >= 0)
                        return_py += "z";
                    else if (hz[i].ToString().CompareTo("压") >= 0)
                        return_py += "y";
                    else if (hz[i].ToString().CompareTo("昔") >= 0)
                        return_py += "x";
                    else if (hz[i].ToString().CompareTo("挖") >= 0)
                        return_py += "w";
                    else if (hz[i].ToString().CompareTo("塌") >= 0)
                        return_py += "t";
                    else if (hz[i].ToString().CompareTo("撒") >= 0)
                        return_py += "s";
                    else if (hz[i].ToString().CompareTo("然") >= 0)
                        return_py += "r";
                    else if (hz[i].ToString().CompareTo("期") >= 0)
                        return_py += "q";
                    else if (hz[i].ToString().CompareTo("啪") >= 0)
                        return_py += "p";
                    else if (hz[i].ToString().CompareTo("哦") >= 0)
                        return_py += "o";
                    else if (hz[i].ToString().CompareTo("拿") >= 0)
                        return_py += "n";
                    else if (hz[i].ToString().CompareTo("妈") >= 0)
                        return_py += "m";
                    else if (hz[i].ToString().CompareTo("垃") >= 0)
                        return_py += "l";
                    else if (hz[i].ToString().CompareTo("喀") >= 0)
                        return_py += "k";
                    else if (hz[i].ToString().CompareTo("击") >= 0)
                        return_py += "j";
                    else if (hz[i].ToString().CompareTo("哈") >= 0)
                        return_py += "h";
                    else if (hz[i].ToString().CompareTo("噶") >= 0)
                        return_py += "g";
                    else if (hz[i].ToString().CompareTo("发") >= 0)
                        return_py += "f";
                    else if (hz[i].ToString().CompareTo("蛾") >= 0)
                        return_py += "e";
                    else if (hz[i].ToString().CompareTo("搭") >= 0)
                        return_py += "d";
                    else if (hz[i].ToString().CompareTo("擦") >= 0)
                        return_py += "c";
                    else if (hz[i].ToString().CompareTo("芭") >= 0)
                        return_py += "b";
                    else if (hz[i].ToString().CompareTo("啊") >= 0)
                        return_py += "a";
                }
                else if (array[0] >= 215) //二级汉字 
                {
                    return_py += ls_second_eng.Substring(ls_second_ch.IndexOf(hz[i].ToString(), 0), 1);
                }
            }
            return return_py.ToUpper();
        } 
        #endregion
        #region "获取精品小图"
        /// <summary>
        /// 获取精品小图
        /// </summary>
        /// <param name="ImagesUrl">精品图片地址</param>
        /// <returns></returns>
        public static string GetPicture(string ImagesUrl)
        {
            if (!string.IsNullOrEmpty(ImagesUrl))
            {
                int Location = ImagesUrl.LastIndexOf('/') + 1;
                if (Location > 0)
                {
                    ImagesUrl = ImagesUrl.Substring(0, Location) + "s_" + ImagesUrl.Substring(Location, ImagesUrl.Length - Location);
                }
            }
            return ImagesUrl;
        }
        #endregion
        #region "获取唯一编码"
        /// <summary>
        /// 获取唯一编码
        /// </summary>
        public static string GetGuID()
        {
            System.Guid guid = new Guid();
            guid = Guid.NewGuid();
            string str = guid.ToString();
            return str;
        }
        #endregion
    }
}