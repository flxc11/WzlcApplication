using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;

namespace CNVP.Framework.Utils
{
    /// <summary>
    /// 多线程下载类
    /// </summary>
    public class MultiDownload
    {
        #region "函数变量"
        /// <summary>
        /// 线程数量
        /// </summary>
        private int _threadNum;
        /// <summary>
        /// 文件大小
        /// </summary>
        private long _fileSize;
        /// <summary>
        /// 文件扩展名
        /// </summary>
        private string _extName;
        /// <summary>
        /// 文件地址
        /// </summary>
        private string _fileUrl;
        /// <summary>
        /// 文件名称
        /// </summary>
        private string _fileName;
        /// <summary>
        /// 保存路径
        /// </summary>
        private string _savePath;
        /// <summary>
        /// 线程完成数量
        /// </summary>
        private short _threadCompleteNum;
        /// <summary>
        /// 是否完成
        /// </summary>
        private bool _isComplete;
        /// <summary>
        /// 当前下载大小(实时的)
        /// </summary>
        private volatile int _downloadSize;
        /// <summary>
        /// 线程数组
        /// </summary>
        private Thread[] _thread;
        private List<string> _tempFiles = new List<string>();
        private object locker = new object();
        public int from, to;
        #endregion
        #region "函数属性"
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize
        {
            get
            {
                return _fileSize;
             }
        }
        /// <summary>
        /// 当前下载大小(实时的)
        /// </summary>
        public int DownloadSize
        {
            get
            {
                return _downloadSize;
            }
        }
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsComplete
        {
            get
            {
                return _isComplete;
            }
        }
        /// <summary>
        /// 线程数量
        /// </summary>
        public int ThreadNum
        {
            get
            {
                return _threadNum;
            }
        }
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath
        {
            get
            {
                return _savePath;
            }
            set
            {
                _savePath = value;
            }
        }
        #endregion
        #region "构造函数"
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="threahNum">线程数量</param>
        /// <param name="fileUrl">文件Url路径</param>
        /// <param name="savePath">本地保存路径</param>
        public MultiDownload(int threahNum, string fileUrl, string savePath)
        {
            this._threadNum = threahNum;            
            this._thread = new Thread[threahNum];
            this._fileUrl = fileUrl;
            this._savePath = savePath;
        }
        #endregion
        #region "开始下载"
        /// <summary>
        /// 开始下载
        /// </summary>
        public void Start()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_fileUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            _extName = response.ResponseUri.ToString().Substring(response.ResponseUri.ToString().LastIndexOf('.'));//获取真实扩展名
            _fileSize = response.ContentLength;
            int singelNum = (int)(_fileSize / _threadNum);      //平均分配
            int remainder = (int)(_fileSize % _threadNum);      //获取剩余的
            request.Abort();
            response.Close();
            for (int i = 0; i < _threadNum; i++)
            {
                List<int> range = new List<int>();
                range.Add(i * singelNum);
                if (remainder != 0 && (_threadNum - 1) == i)
                {
                    //剩余的交给最后一个线程
                    range.Add(i * singelNum + singelNum + remainder - 1);
                }
                else
                {
                    range.Add(i * singelNum + singelNum - 1);
                }
                //_thread[i] = new Thread(new ThreadStart(Download(range[0], range[1])));
                from = range[0]; to = range[1];//下载指定位置的数据
                _thread[i] = new Thread(new ThreadStart(Download));
                _thread[i].Name = "cnvp_{0}".Replace("{0}", Convert.ToString(i + 1));
                _thread[i].Start();
                Thread.Sleep(10);
            }
        }
        #endregion
        #region "下载文件"
        /// <summary>
        /// 下载文件
        /// </summary>
        private void Download()
        {
            Stream httpFileStream = null, localFileStram = null;
            try
            {
                //string tmpFileBlock = @"{0}\{1}_{2}.dat".Formats(_savePath, _fileName, Thread.CurrentThread.Name);
                string tmpFileBlock = @_savePath + "\\" + _fileName + "_" + Thread.CurrentThread.Name + ".dat";
                _tempFiles.Add(tmpFileBlock);
                HttpWebRequest httprequest = (HttpWebRequest)WebRequest.Create(_fileUrl);
                httprequest.AddRange(from, to);
                HttpWebResponse httpresponse = (HttpWebResponse)httprequest.GetResponse();
                httpFileStream = httpresponse.GetResponseStream();
                localFileStram = new FileStream(tmpFileBlock, FileMode.Create);
                byte[] by = new byte[5000];
                int getByteSize = httpFileStream.Read(by, 0, (int)by.Length); //Read方法将返回读入by变量中的总字节数
                while (getByteSize > 0)
                {
                    lock (locker) _downloadSize += getByteSize;
                    localFileStram.Write(by, 0, getByteSize);
                    getByteSize = httpFileStream.Read(by, 0, (int)by.Length);
                }
                lock (locker) _threadCompleteNum++;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                if (httpFileStream != null) httpFileStream.Dispose();
                if (localFileStram != null) localFileStram.Dispose();
            }
            if (_threadCompleteNum == _threadNum)
            {
                Complete();
                _isComplete = true;
            }
        }
        #endregion
        #region "合并文件"
        /// <summary>
        /// 下载完成后合并文件块
        /// </summary>
        private void Complete()
        {
            //Stream mergeFile = new FileStream(@"{0}\{1}{2}".Formats(_savePath, _fileName, _extName), FileMode.Create);
            Stream mergeFile = new FileStream(@_savePath + "\\" + _fileName + _extName, FileMode.Create);
            BinaryWriter AddWriter = new BinaryWriter(mergeFile);
            foreach (string file in _tempFiles)
            {
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    BinaryReader TempReader = new BinaryReader(fs);
                    AddWriter.Write(TempReader.ReadBytes((int)fs.Length));
                    TempReader.Close();
                }
                File.Delete(file);
            }
            AddWriter.Close();
        }
        #endregion
    }
}