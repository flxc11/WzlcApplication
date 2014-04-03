using System;
using System.Data;
using System.IO;
using System.Web;
using CNVP.Config;
using CNVP.Framework.Helper;

namespace CNVP.Framework.Utils
{
    public class FileUtils
    {
        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static bool FolderExists(string Path)
        {
            if (Directory.Exists(Path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="Path"></param>
        public static void CreateFolder(string Path)
        {
            if (Path.Length == 0) return;
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="Path"></param>
        public static void DeleteFolder(string Path)
        {
            if (Path.Length == 0) return;
            if (Directory.Exists(Path))
            {
                Directory.Delete(Path,true);
            }
        }
        /// <summary>
        /// 递归拷贝文件夹内容
        /// </summary>
        /// <param name="FormPath"></param>
        /// <param name="ToPath"></param>
        public static void CopyFolder(string FormPath, string ToPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (ToPath[ToPath.Length - 1] != Path.DirectorySeparatorChar)
                    ToPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(ToPath)) Directory.CreateDirectory(ToPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(FormPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (Directory.Exists(file))
                        CopyFolder(file, ToPath + Path.GetFileName(file));
                    // 否则直接Copy文件
                    else
                        File.Copy(file, ToPath + Path.GetFileName(file), true);
                }
            }
            catch
            {
                CNVP.Framework.Helper.LogHelper.Write("复制文件出错",string.Format("源文件{0}到{1}",FormPath,ToPath));
            }
        }
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="FormPath"></param>
        /// <param name="ToPath"></param>
        public static void CopyFile(string FormPath, string ToPath)
        {
            if (FileExists(FormPath))
            {
                SaveFile(ReadFile(FormPath),ToPath);
            }
            else
            {
                CNVP.Framework.Helper.LogHelper.Write("复制文件出错", string.Format("源文件{0}不存在。", FormPath));
            }
        }
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static bool FileExists(string Path)
        {
            if (File.Exists(Path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string ReadFile(string Path)
        {
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(Path, System.Text.Encoding.UTF8);
                string str = sr.ReadToEnd();
                sr.Close();
                return str;
            }
            catch (Exception ex)
            {
                LogHelper.Write("找不到相关文件", ex.ToString());
                return "";
            }
        }
        /// <summary>
        /// 保存文件内容
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static void SaveFile(string Str, string Path)
        {
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Path, false, System.Text.Encoding.UTF8);
                sw.Write(Str);
                sw.Close();
            }
            catch (Exception ex)
            {
                LogHelper.Write("保存文件失败", ex.ToString());
            }
        }
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string ReadFileGb2312(string Path)
        {
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(Path, System.Text.Encoding.GetEncoding("Gb2312"));
                string str = sr.ReadToEnd();
                sr.Close();
                return str;
            }
            catch (Exception ex)
            {
                LogHelper.Write("找不到相关文件", ex.ToString());
                return "";
            }
        }
        /// <summary>
        /// 保存文件内容
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static void SaveFileGb2312(string Str, string Path)
        {
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Path, false, System.Text.Encoding.GetEncoding("Gb2312"));
                sw.Write(Str);
                sw.Close();
            }
            catch (Exception ex)
            {
                LogHelper.Write("保存文件失败", ex.ToString());
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="File"></param>
        public static void DeleteFile(string File)
        {
            if (File.Length == 0) return;
            if (System.IO.File.Exists(File))
            {
                System.IO.File.Delete(File);
            }
        }
        #region "获取指定格式的文件列表"
        /// <summary>
        /// 获取指定格式的文件列表
        /// </summary>
        /// <param name="FilePath">相对路径，例如：~/</param>
        /// <param name="FileType">文件格式，例如：*.aspx</param>
        /// <returns></returns>
        public static DataTable GetAllFolder(string FilePath,string FileType)
        {
            FilePath = HttpContext.Current.Server.MapPath(FilePath);
            DataTable Dt = new DataTable();
            Dt.Columns.Add(new DataColumn("FileName", typeof(String)));
            Dt.Columns.Add(new DataColumn("FileSize", typeof(Int32)));
            Dt.Columns.Add(new DataColumn("FileCreateTime", typeof(DateTime)));
            Dt = GetAllFolder(FilePath, Dt, FilePath,FileType);
            return Dt;
        }
        /// <summary>
        /// 获取指定格式的文件列表
        /// </summary>
        /// <param name="dirPath">当前路径</param>
        /// <param name="Dt"></param>
        /// <param name="ReplacePath">替换路径</param>
        /// <param name="FileType">文件格式</param>
        /// <returns></returns>
        private static DataTable GetAllFolder(string dirPath, DataTable Dt,string ReplacePath,string FileType)
        {
            DirectoryInfo Dir = new DirectoryInfo(dirPath);
            foreach (FileInfo f in Dir.GetFiles(FileType))
            {
                string FileName = Dir + f.ToString();
                FileName =  FileName.Replace(HttpContext.Current.Server.MapPath("~/"), "").Replace("\\", "/");
                DataRow Row = Dt.NewRow();
                Row[0] = FileName;
                Row[1] = f.Length;
                Row[2] = f.LastAccessTime.ToString("yyyy-MM-dd HH:mm:ss");
                Dt.Rows.Add(Row);
            }
            foreach (DirectoryInfo d in Dir.GetDirectories())
            {
                Dt = GetAllFolder(Dir + d.ToString() + "\\", Dt, ReplacePath, FileType);
            }
            return Dt;
        }
        #endregion
    }
}