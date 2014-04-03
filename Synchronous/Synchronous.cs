using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using CNVP.Config;
using CNVP.Framework.Helper;
using CNVP.Framework.Utils;
using System.Collections;
using Jayrock.Json.Conversion;
using System.Xml;

namespace Synchronous
{
    public partial class Synchronous : ServiceBase
    {
        #region "同步工具"
        /// <summary>
        /// 同步工具
        /// </summary>
        public Synchronous()
        {
            InitializeComponent();
            //读取配置信息
            SetConfig();
        }
        #endregion
        #region "属性配置"
        /// <summary>
        /// 时间控制
        /// </summary>
        TimerUtils timerUtils = new TimerUtils();
        /// <summary>
        /// 操作工具
        /// </summary>
        WebUtils Tools = new WebUtils();
        /// <summary>
        /// 配置文件
        /// </summary>
        private static string FileName = Public.GetProjectPath() + "Config.ini";
        /// <summary>
        /// 间隔时间
        /// </summary>
        private int TimeSpan = 1000;
        /// <summary>
        /// 服务地址
        /// </summary>
        private string AppUrl = string.Empty;
        /// <summary>
        /// 商户序号
        /// </summary>
        private string AppID = string.Empty;
        /// <summary>
        /// 通讯私钥
        /// </summary>
        private string AppPriKey = string.Empty;
        /// <summary>
        /// 网站目录
        /// </summary>
        private string WebRoot = string.Empty;
        #endregion
        #region "开启服务"
        /// <summary>
        /// 开启服务
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            LogHelper.WriteLog("文件同步工具已被启动");
            System.Timers.Timer _Timer = new System.Timers.Timer(10);
            _Timer.Elapsed += new System.Timers.ElapsedEventHandler(this.Synchronous_Elapsed);
            _Timer.AutoReset = false;
            _Timer.Enabled = true;
        }
        #endregion
        #region "实时监控"
        /// <summary>
        /// 实时监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Synchronous_Elapsed(object sender, EventArgs e)
        {
            while (true)
            {
                //执行类别同步
                Synchronous_Type();
                //执行同步操作
                Synchronous_Get();
                //执行订单同步
                Synchronous_Order();
                //系统延时时间
                LogHelper.WriteLog("系统延时" + TimeSpan + "秒");
                this.timerUtils.Wait(Convert.ToInt32(TimeSpan));
            }
        }
        #endregion
        #region "文件同步"
        /// <summary>
        /// 文件同步
        /// </summary>
        private void Synchronous_Get()
        {
            try
            {
                //获取任务
                string Url = string.Format("{0}/OpenAPI/Synchronous.aspx?{1}", AppUrl, WebUtils.BuildQuery(GetParams("GetAllTactics", "")));

                XmlDocument Xml = new XmlDocument();
                Xml.Load(Url);
                XmlNodeList RootList = Xml.SelectNodes("HongXu/Tactics/Item");

                //解析任务
                foreach (XmlNode Xn in RootList)
                {
                    string TacticsID = Xn.Attributes["TacticsID"].InnerText;
                    string Action = Xn.Attributes["Action"].InnerText;
                    string SqlInfo = Xn.Attributes["SqlInfo"].InnerText;
                    string FilePath = Xn.Attributes["FilePath"].InnerText;

                    //执行任务
                    switch (Action)
                    {
                        case "UpdateSql":
                            UpdateSql(TacticsID, SqlInfo);
                            break;
                        case "UpdateFile":
                            UpdateFile(TacticsID, FilePath);
                            break;
                        case "DeleteFile":
                            DeleteFile(TacticsID, FilePath);
                            break;
                        case "UpdateSoft":
                            UpdateSoft(TacticsID, FilePath);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
            }
        }
        /// <summary>
        /// 精品导入
        /// </summary>
        private void Synchronous_Type()
        {
            string DbPath = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}\\App_Data\\Product.mdb;", WebRoot);
            try
            {
                DbHelper.ConntionString = DbPath;
                DataTable Dt = DbHelper.ExecuteTable("Select Count(*) As Total From " + DbConfig.Prefix + "Type");
                int Count = Convert.ToInt32(Dt.Rows[0]["Total"]);
                if (Count == 0)
                {
                    //精品导入
                    Tools.DoPost(string.Format("{0}/OpenAPI/Synchronous.aspx", AppUrl), GetParams("GetAllProduct", ""));
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
            }
        }
        /// <summary>
        /// 订单提交
        /// </summary>
        private void Synchronous_Order()
        { 
        }
        #region "设置参数"
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="Method">方法名称</param>
        /// <param name="TacticsID">任务序号</param>
        /// <returns></returns>
        private Dictionary<string, string> GetParams(string Method, string TacticsID)
        {
            string AppKey = AppID;
            string Timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string Sign = RSAHelper.EncryptString(string.Format("{0}|{1}|{2}", AppKey, Method, Timestamp), AppPriKey);

            Dictionary<string, string> Params = new Dictionary<string, string>();

            Params.Add("AppKey", AppKey);
            Params.Add("Sign", Sign);
            Params.Add("TimeStamp", Timestamp);
            Params.Add("Method", Method);
            Params.Add("TacticsID", TacticsID);

            return Params;
        }
        #endregion
        #region "执行任务"
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="TacticsID">任务序号</param>
        /// <param name="StrSql">Sql语句</param>
        private void UpdateSql(string TacticsID, string StrSql)
        {
            string DbPath = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}\\App_Data\\Product.mdb;", WebRoot);

            try
            {
                DbHelper.ConntionString = DbPath;
                DbHelper.ExecuteNonQuery(StrSql);

                //更新日志
                LogInfo(TacticsID, StrSql + "语句执行操作成功。");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="TacticsID">任务序号</param>
        /// <param name="FilePath">文件地址</param>
        private void UpdateFile(string TacticsID, string FilePath)
        {
            try
            {
                string LocalPath = FilePath.Substring(0, FilePath.LastIndexOf('/') + 1);
                LocalPath = string.Format("{0}{1}", WebRoot, LocalPath.Replace("/", "\\"));
                if (!FileUtils.FolderExists(LocalPath))
                {
                    FileUtils.CreateFolder(LocalPath);
                }

                string RomoteFile = AppUrl + FilePath;
                string LocalFile = FilePath;
                int StartPos = LocalFile.LastIndexOf('/') + 1;
                int Length = LocalFile.LastIndexOf('.') - StartPos;
                string FileName = FilePath.Substring(StartPos, Length);

                MultiDownload Down = new MultiDownload(10, RomoteFile, LocalPath);
                Down.FileName = FileName;
                Down.Start();

                //更新日志
                LogInfo(TacticsID, FilePath + "文件下载操作成功。");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="TacticsID">任务序号</param>
        /// <param name="FilePath">文件地址</param>
        private void DeleteFile(string TacticsID, string FilePath)
        {
            try
            {
                string DeleteFiles = WebRoot + FilePath.Replace('/', '\\');
                if (System.IO.File.Exists(DeleteFiles))
                {
                    //删除文件
                    System.IO.File.Delete(DeleteFiles);
                    //更新日志
                    LogInfo(TacticsID, FilePath + "文件删除操作成功。");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
            }
        }
        /// <summary>
        /// 更新程序
        /// </summary>
        /// <param name="TacticsID">任务序号</param>
        /// <param name="FilePath">文件地址</param>
        private void UpdateSoft(string TacticsID, string FilePath)
        {
            string LocalPath = string.Format("{0}\\UploadFile\\", WebRoot);

            try
            {
                string RomoteFile = AppUrl + FilePath;
                string LocalFile = FilePath;
                int StartPos = LocalFile.LastIndexOf('/');
                int Length = LocalFile.LastIndexOf('.') - StartPos;
                string FileName = FilePath.Substring(StartPos, Length);
                string LoaclPath = "D:\\";

                MultiDownload Down = new MultiDownload(10, RomoteFile, LoaclPath);
                Down.FileName = FileName;
                Down.Start();

                //更新日志
                LogInfo(TacticsID, FilePath + "文件下载操作成功。");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
            }
        }
        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="TacticsID">任务序号</param>
        /// <param name="MgsStr">日志信息</param>
        private void LogInfo(string TacticsID, string MgsStr)
        {
            //更新任务
            Tools.DoPost(string.Format("{0}/OpenAPI/Synchronous.aspx", AppUrl), GetParams("UpdateTactics", TacticsID));

            //记录日志
            LogHelper.WriteLog(MgsStr);
        }
        #endregion
        #endregion
        #region "停止服务"
        /// <summary>
        /// 停止服务
        /// </summary>
        protected override void OnStop()
        {
            LogHelper.WriteLog("文件同步工具已被停止");
        }
        #endregion
        #region "配置文件"
        /// <summary>
        /// 配置文件
        /// </summary>
        private void SetConfig()
        {
            if (!IniUtils.IsExist(FileName))
            {
                //创建配置文件
                IniUtils.CreateFile(FileName);
                //初始化配置信息
                IniUtils.WriteFile(FileName, "同步工具", "TimeSpan", "60");
                IniUtils.WriteFile(FileName, "同步工具", "AppUrl", "http://www.cnvp.com.cn/OpenAPI/Synchronous.aspx");
                IniUtils.WriteFile(FileName, "同步工具", "AppID", "2");
                IniUtils.WriteFile(FileName, "同步工具", "AppPriKey", "");
                IniUtils.WriteFile(FileName, "同步工具", "WebRoot", "");
            }
            else
            {
                TimeSpan = Convert.ToInt32(IniUtils.GetString(FileName, "同步工具", "TimeSpan"));
                AppUrl = IniUtils.GetString(FileName, "同步工具", "AppUrl");
                AppID = IniUtils.GetString(FileName, "同步工具", "AppID");
                AppPriKey = IniUtils.GetString(FileName, "同步工具", "AppPriKey");
                WebRoot = IniUtils.GetString(FileName, "同步工具", "WebRoot");
            }
        }
        #endregion
    }
}