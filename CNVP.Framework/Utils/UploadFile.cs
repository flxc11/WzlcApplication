using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;

namespace CNVP.Framework.Utils
{
    /// <summary>
    /// 枚举类型
    /// </summary>
    public enum E_FileExt
    {
        NONE = 0,
        JPG = 255216,
        GIF = 7173,
        BMP = 6677,
        PNG = 13780,
        SWF = 6787,
        RAR = 8297,
        ZIP = 8075,
        XML = 6063,
        DOC = 208207,
        DOCX = 8075,
        ASPX = 239187,
        CS = 117115,
        SQL = 255254,
        HTML = 6063,
        EXE = 7790
    }
    /// <summary>
    /// 文件后缀名验证类
    /// </summary>
    public class UploadFileCheck
    {
        private static object obj = new object();           // 锁定对象
        private static UploadFileCheck fce;              // 验证类实体
        private List<E_FileExt> filterList = new List<E_FileExt>();   // 过滤的格式集合
        private UploadFileCheck(List<E_FileExt> _filterList)
        {
            init(_filterList);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_filterList">过滤的格式集合</param>
        private void init(List<E_FileExt> _filterList)
        {
            filterList = _filterList;
        }
        /// <summary>
        /// 单例模式主入口
        /// </summary>
        /// <param name="_filterList">过滤的格式集合</param>
        /// <returns>FileCheckExtension</returns>
        public static UploadFileCheck Start(List<E_FileExt> _filterList)
        {
            if (fce == null)
            {
                lock (obj)
                {
                    if (fce == null)
                    {
                        fce = new UploadFileCheck(_filterList);
                    }
                }
            }
            return fce;
        }
        /// <summary>
        /// 验证上传控件的文件类型
        /// </summary>
        /// <param name="fu">FileUpload</param>
        /// <returns>类型枚举</returns>
        public E_FileExt GetFileExtension(HttpPostedFile fu)
        {
            Byte[] bytesContent = new Byte[2];
            fu.InputStream.Read(bytesContent, 0, 2);
            return GetFileExtension(bytesContent);
        }
        /// <summary>
        /// 验证Byte数组的文件类型
        /// </summary>
        /// <param name="bytes">Byte[]</param>
        /// <returns>类型枚举</returns>
        public E_FileExt GetFileExtension(Byte[] bytes)
        {
            E_FileExt eFX = E_FileExt.NONE;
            MemoryStream ms = new MemoryStream(bytes);
            BinaryReader br = new BinaryReader(ms);
            string fileTop = string.Empty;
            byte buffer;
            try
            {
                buffer = br.ReadByte();
                fileTop = buffer.ToString();
                buffer = br.ReadByte();
                fileTop += buffer.ToString();
                foreach (E_FileExt efx in filterList)
                {
                    if (System.Int32.Parse(fileTop).Equals((int)efx))
                    {
                        eFX = efx;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                br.Close();
                ms.Dispose();
            }
            return eFX;
        }
        /// <summary>
        /// 验证文件后缀名是否合法
        /// </summary>
        /// <param name="fu"></param>
        /// <returns>true or false</returns>
        public bool UploadFileVerify(HttpPostedFile fu)
        {
            string fullName = fu.FileName;
            string exeName = string.Empty;
            if (fullName != string.Empty)
            {
                exeName = fullName.Substring(fullName.LastIndexOf(".") + 1);
                if (exeName.ToLower().Equals("txt") || exeName.ToLower().Equals("log"))
                {
                    return CheckIsTextFile(fu);
                }
            }
            E_FileExt efe = GetFileExtension(fu);
            if ((int)efe == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 验证文件后缀名是否合法
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>true or false</returns>
        public bool ExtensionVerify(Byte[] bytes)
        {
            E_FileExt efe = GetFileExtension(bytes);
            if ((int)efe == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 判断是否文本文件
        /// </summary>
        /// <param name="fu"></param>
        /// <returns></returns>
        public bool CheckIsTextFile(HttpPostedFile fu)
        {
            Byte[] bytesContent = new Byte[fu.ContentLength];
            fu.InputStream.Read(bytesContent, 0, fu.ContentLength);
            MemoryStream ms = new MemoryStream(bytesContent);
            bool isTextFile = true;
            try
            {
                int i = 0;
                int length = (int)ms.Length;
                byte data;
                while (i < length && isTextFile)
                {
                    data = (byte)ms.ReadByte();
                    isTextFile = (data != 0);
                    i++;
                }
                return isTextFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                }

            }
        }
    }
}