using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using CNVP.Config;
using CNVP.Framework.Utils;

namespace CNVP.Framework.Helper
{
    public class DbHelper
    {
        #region "属性"
        private static string connectionString;
        public static string ConntionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }
        /// <summary>
        /// 自定义访问数据库
        /// </summary>
        /// <param name="ConnectionString">数据库链接地址</param>
        /// <param name="DataType">数据库类型</param>
        public DbHelper(string ConnectionString, string DataType)
        {
            ConntionString = ConnectionString;
            DbType = DataType;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        static DbHelper()
        {
            connectionString = DbConfig.DbConn;
            dbType = DbConfig.DbType;
        }

        /// <summary>
        /// 数据库类型 
        /// </summary>
        private static string dbType;
        public static string DbType
        {
            get
            {   
                return dbType;
            }
            set
            {   
                if (dbType != value)
                {
                    dbType = DbConfig.DbType;
                }
            }
        }
        #endregion
        #region "定义"
        private static System.Data.IDbDataParameter iDbPara(string ParaName, string DataType)
        {
            switch (DbType)
            {
                case "sqlserver":
                    return GetSqlPara(ParaName, DataType);
                case "oracle":
                    return GetOleDbPara(ParaName, DataType);
                case "access":
                    return GetOleDbPara(ParaName, DataType);
                default:
                    return GetSqlPara(ParaName, DataType);
            }
        }
        private static System.Data.SqlClient.SqlParameter GetSqlPara(string ParaName, string DataType)
        {
            switch (DataType)
            {
                case "Decimal":
                    return new System.Data.SqlClient.SqlParameter(ParaName, System.Data.SqlDbType.Decimal);
                case "Varchar":
                    return new System.Data.SqlClient.SqlParameter(ParaName, System.Data.SqlDbType.VarChar);
                case "DateTime":
                    return new System.Data.SqlClient.SqlParameter(ParaName, System.Data.SqlDbType.DateTime);
                case "Image":
                    return new System.Data.SqlClient.SqlParameter(ParaName, System.Data.SqlDbType.Image);
                case "Int":
                    return new System.Data.SqlClient.SqlParameter(ParaName, System.Data.SqlDbType.Int);
                case "Text":
                    return new System.Data.SqlClient.SqlParameter(ParaName, System.Data.SqlDbType.NText);
                default:
                    return new System.Data.SqlClient.SqlParameter(ParaName, System.Data.SqlDbType.VarChar);
            }
        }
        private static System.Data.OracleClient.OracleParameter GetOraclePara(string ParaName, string DataType)
        {
            switch (DataType)
            {
                case "Decimal":
                    return new System.Data.OracleClient.OracleParameter(ParaName, System.Data.OracleClient.OracleType.Double);
                case "Varchar":
                    return new System.Data.OracleClient.OracleParameter(ParaName, System.Data.OracleClient.OracleType.VarChar);
                case "DateTime":
                    return new System.Data.OracleClient.OracleParameter(ParaName, System.Data.OracleClient.OracleType.DateTime);
                case "Image":
                    return new System.Data.OracleClient.OracleParameter(ParaName, System.Data.OracleClient.OracleType.BFile);
                case "Int":
                    return new System.Data.OracleClient.OracleParameter(ParaName, System.Data.OracleClient.OracleType.Int32);
                case "Text":
                    return new System.Data.OracleClient.OracleParameter(ParaName, System.Data.OracleClient.OracleType.LongVarChar);
                default:
                    return new System.Data.OracleClient.OracleParameter(ParaName, System.Data.OracleClient.OracleType.VarChar);

            }
        }
        private static System.Data.OleDb.OleDbParameter GetOleDbPara(string ParaName, string DataType)
        {
            switch (DataType)
            {
                case "Decimal":
                    return new System.Data.OleDb.OleDbParameter(ParaName, OleDbType.Decimal);
                case "Varchar":
                    return new System.Data.OleDb.OleDbParameter(ParaName, OleDbType.VarChar);
                case "DateTime":
                    return new System.Data.OleDb.OleDbParameter(ParaName, OleDbType.Date);
                case "Image":
                    return new System.Data.OleDb.OleDbParameter(ParaName, OleDbType.Binary);
                case "Int":
                    return new System.Data.OleDb.OleDbParameter(ParaName, OleDbType.Integer);
                case "Text":
                    return new System.Data.OleDb.OleDbParameter(ParaName, OleDbType.VarChar);
                default:
                    return new System.Data.OleDb.OleDbParameter(ParaName, OleDbType.VarChar);
            }
        }
        private static IDbConnection GetConnection()
        {
            switch (DbType)
            {
                case "sqlserver":
                    return new System.Data.SqlClient.SqlConnection(ConntionString);
                case "oracle":
                    return new System.Data.OracleClient.OracleConnection(ConntionString);
                case "access":
                    return new System.Data.OleDb.OleDbConnection(ConntionString);
                default:
                    return new System.Data.SqlClient.SqlConnection(ConntionString);
            }
        }
        public static IDbConnection GetConnection(string ConntionString)
        {
            switch (DbType)
            {
                case "sqlserver":
                    return new System.Data.SqlClient.SqlConnection(ConntionString);
                case "oracle":
                    return new System.Data.OracleClient.OracleConnection(ConntionString);
                case "access":
                    return new System.Data.OleDb.OleDbConnection(ConntionString);
                default:
                    return new System.Data.SqlClient.SqlConnection(ConntionString);
            }
        }
        private static IDbCommand GetCommand(string Sql, IDbConnection iConn)
        {
            switch (DbType)
            {
                case "sqlserver":
                    return new System.Data.SqlClient.SqlCommand(Sql, (SqlConnection)iConn);
                case "oracle":
                    return new System.Data.OracleClient.OracleCommand(Sql, (OracleConnection)iConn);
                case "access":
                    return new System.Data.OleDb.OleDbCommand(Sql, (OleDbConnection)iConn);
                default:
                    return new System.Data.SqlClient.SqlCommand(Sql, (SqlConnection)iConn);
            }
        }
        private static IDbCommand GetCommand()
        {
            switch (DbType)
            {
                case "sqlserver":
                    return new System.Data.SqlClient.SqlCommand();
                case "oracle":
                    return new System.Data.OracleClient.OracleCommand();
                case "access":
                    return new System.Data.OleDb.OleDbCommand();
                default:
                    return new System.Data.SqlClient.SqlCommand();
            }
        }
        private static IDataAdapter GetAdapater(string Sql, IDbConnection iConn)
        {
            switch (DbType)
            {
                case "sqlserver":
                    return new System.Data.SqlClient.SqlDataAdapter(Sql, (SqlConnection)iConn);
                case "oracle":
                    return new System.Data.OracleClient.OracleDataAdapter(Sql, (OracleConnection)iConn);
                case "access":
                    return new System.Data.OleDb.OleDbDataAdapter(Sql, (OleDbConnection)iConn);
                default:
                    return new System.Data.SqlClient.SqlDataAdapter(Sql, (SqlConnection)iConn); ;
            }
        }
        private static IDataAdapter GetAdapater()
        {
            switch (DbType)
            {
                case "sqlserver":
                    return new System.Data.SqlClient.SqlDataAdapter();
                case "oracle":
                    return new System.Data.OracleClient.OracleDataAdapter();
                case "access":
                    return new System.Data.OleDb.OleDbDataAdapter();
                default:
                    return new System.Data.SqlClient.SqlDataAdapter();
            }
        }
        /// <summary>
        /// 设置不同数据库的SelectCommand
        /// </summary>
        /// <param name="iAdapter"></param>
        /// <param name="iCmd"></param>
        private static void GetSelectCommand(IDataAdapter iAdapter, IDbCommand iCmd)
        {
            switch (DbType)
            {
                case "sqlserver":
                    ((SqlDataAdapter)iAdapter).SelectCommand = (SqlCommand)iCmd;
                    break;
                case "oracle":
                    ((OracleDataAdapter)iAdapter).SelectCommand = (OracleCommand)iCmd;
                    break;
                case "access":
                    ((OleDbDataAdapter)iAdapter).SelectCommand = (OleDbCommand)iCmd;
                    break;
                default:
                    ((SqlDataAdapter)iAdapter).SelectCommand = (SqlCommand)iCmd;
                    break;
            }
        }
        private static IDataAdapter GetAdapater(IDbCommand iCmd)
        {
            switch (DbType)
            {
                case "sqlserver":
                    return new System.Data.SqlClient.SqlDataAdapter((SqlCommand)iCmd);
                case "oracle":
                    return new System.Data.OracleClient.OracleDataAdapter((OracleCommand)iCmd);
                case "access":
                    return new System.Data.OleDb.OleDbDataAdapter((OleDbCommand)iCmd);
                default:
                    return new System.Data.SqlClient.SqlDataAdapter((SqlCommand)iCmd);
            }
        }
        /// <summary>
        /// 对命令的属性（如连接、事务环境等）进行初始化。
        /// </summary>
        /// <param name="command"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        private static void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, params IDataParameter[] parameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            command.Connection = connection;
            command.CommandText = commandText;
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            if (parameters != null)
            {
                foreach (IDataParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
            }
            return;
        }
        #endregion
        #region "参数"
        /// <summary>
        /// 参数输入
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static IDataParameter MakeInParam(string paramName, DbType dbType, object value)
        {
            return MakeParam(paramName, dbType, ParameterDirection.Input, value);
        }
        /// <summary>
        /// 参数输出
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="dbType">参数类型</param>
        /// <returns></returns>
        public static IDataParameter MakeOutParam(string paramName, DbType dbType)
        {
            return MakeParam(paramName, dbType, ParameterDirection.Output, null);
        }
        /// <summary>
        /// 参数输入
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IDataParameter MakeParam(string paramName, object value)
        {
            switch (DbType)
            {
                case "sqlserver":
                    return new SqlParameter(paramName, value);
                case "oracle":
                    return new OracleParameter(paramName, value);
                case "access":
                    return new OleDbParameter(paramName, value);
                default:
                    return new SqlParameter(paramName, value);

            }
        }
        /// <summary>
        /// 参数输入输出
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="direction">输入输出</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static IDataParameter MakeParam(string paramName, DbType dbType, ParameterDirection direction, object value)
        {
            IDataParameter Param;
            Param = iDbPara(paramName, dbType.ToString());
            Param.Direction = direction;
            if (!(direction == ParameterDirection.Output && value == null))
                Param.Value = value;
            return Param;
        }
        #endregion
        #region "解析参数"
        /// <summary>
        /// 解析参数
        /// </summary>
        /// <param name="StrSql">Sql语句</param>
        /// <param name="parameters">参数值</param>
        /// <returns></returns>
        public static string AnalyzeParam(string StrSql, params IDataParameter[] parameters)
        {
            foreach (IDataParameter d in parameters)
            {
                Type t = d.Value.GetType();
                //HttpContext.Current.Response.Write(d.ToString() + "|" + t.Name);
                if (DataType.IsString(t.Name))
                {
                    StrSql = StrSql.Replace(d.ToString(), "'" + d.Value.ToString() + "'");
                }
                else
                {
                    StrSql = StrSql.Replace(d.ToString(), d.Value.ToString());
                }
                //StrSql += t.Name;
            }
            //HttpContext.Current.Response.Write(StrSql);
            //HttpContext.Current.Response.End();
            return StrSql;            
        }
        /// <summary>
        /// 生成Sql语句
        /// </summary>
        /// <param name="StrSql">Sql语句</param>
        /// <param name="Dt">数据集</param>
        /// <param name="Row">数据行</param>
        /// <returns></returns>
        public static string AnalyzeSql(string StrSql, DataTable Dt, DataRow Row)
        {
            string FieldName = string.Empty;
            string FieldValue = string.Empty;

            for (int i = 0; i < Dt.Columns.Count; i++)
            {
                //字段名称
                string _Name = Dt.Columns[i].ColumnName;
                //字段的值
                string _Value = Row[_Name].ToString();
                //字段类型
                string _Type = Dt.Columns[i].DataType.Name;
                if (!string.IsNullOrEmpty(_Value))
                {
                    FieldName += _Name + ",";
                    bool flg = DataType.IsString(_Type);
                    if (flg)
                    {
                        FieldValue += "'" + _Value + "',";
                    }
                    else
                    {
                        FieldValue += _Value + ","; 
                    }
                }
            }

            //数据字段处理
            if (!string.IsNullOrEmpty(FieldName))
            {
                FieldName = FieldName.Substring(0, FieldName.Length - 1);
            }
            if (!string.IsNullOrEmpty(FieldValue))
            {
                FieldValue = FieldValue.Substring(0, FieldValue.Length - 1);
            }

            StrSql = string.Format(StrSql, FieldName, FieldValue);
            return StrSql;            
        }
        #endregion
        #region "获取表名"
        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllTable()
        {
            string StrSql="Select Name From SysObjects Where XType='U' Order By Name";
            return ExecuteTable(StrSql);
        }
        #endregion
        #region "ExecuteNonQuery"
        /// <summary>
        /// 执行Sql语句，返回受影响的记录数
        /// </summary>
        /// <param name="Sql">Sql语句</param>
        /// <returns>受影响的记录数</returns>
        public static int ExecuteNonQuery(string commandText)
        {
            int Count = -1;
            if (string.IsNullOrEmpty(commandText))
                return Count;

            return ExecuteNonQuery(commandText, (IDataParameter[])null);
        }
        /// <summary>
        /// 执行Sql语句，返回受影响的记录数
        /// </summary>
        /// <param name="Sql">Sql语句</param>
        /// <returns>受影响的记录数</returns>
        public static int ExecuteNonQuery(string commandText, params IDataParameter[] parameters)
        {
            CommandType commandType = CommandType.Text;
            return ExecuteNonQuery(commandType, commandText, parameters);
        }
        /// <summary>
        /// 执行Sql语句，返回受影响的记录数
        /// </summary>
        /// <param name="CommandType">存储过程|表名|Sql语句(默认)</param>
        /// <param name="CommandText"></param>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType commandType, string commandText, IDataParameter[] parameters)
        {
            int Count = -1;
            if (string.IsNullOrEmpty(commandText) && parameters == null)
                return Count;

            using (IDbConnection connection = GetConnection())
            {
                using (IDbCommand command = GetCommand())
                {
                    try
                    {
                        PrepareCommand(command, connection, (IDbTransaction)null, commandType, commandText, parameters);
                        Count = command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        return Count;
                    }
                    catch
                    {
                        connection.Close();
                        Count = -1;
                        throw;
                    }
                    finally
                    {
                        command.Dispose();
                        if (connection.State != ConnectionState.Closed)
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }
        #endregion
        #region "ExecuteScalar"
        /// <summary>
        /// 获取某表，中某字段的最大值
        /// </summary>
        /// <param name="FieldName">字段</param>
        /// <param name="TableName">表名</param>
        /// <returns></returns>
        public static int GetMaxID(string FieldName, string TableName)
        {
            string SQLString = "select  max(" + FieldName + ") + 1 from " + TableName;
            object obj = ExecuteScalar(SQLString);
            if (obj == DBNull.Value || obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// 执行Sql语句，返回数值
        /// 如果执行查询操作，返回第一行第一列；
        /// 如果不是查询操作，无返回值。
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string commandText)
        {
            CommandType commandType = CommandType.Text;
            object o = ExecuteScalar(commandType, commandText);
            return o;
        }
        /// <summary>
        /// 执行Sql语句，返回数值
        /// 如果执行查询操作，返回第一行第一列；
        /// 如果不是查询操作，无返回值。
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(CommandType commandType, string commandText)
        {
            return ExecuteScalar(commandType, commandText, (IDbDataParameter[])null);
        }
        /// <summary>
        /// 执行Sql语句，返回数值
        /// 如果执行查询操作，返回第一行第一列；
        /// 如果不是查询操作，无返回值。
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string commandText, params IDataParameter[] parameters)
        {
            CommandType commandType = CommandType.Text;
            object o = ExecuteScalar(commandType, commandText, parameters);
            return o;
        }
        /// <summary>
        /// 执行Sql语句，返回数值
        /// 如果执行查询操作，返回第一行第一列；
        /// 如果不是查询操作，无返回值。
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(CommandType commandType, string commandText, params IDataParameter[] parameters)
        {
            using (IDbConnection connection = GetConnection())
            {
                using (IDbCommand command = GetCommand())
                {
                    try
                    {
                        PrepareCommand(command, connection, (IDbTransaction)null, commandType, commandText, parameters);
                        object retval = command.ExecuteScalar();
                        command.Parameters.Clear();
                        return retval;
                    }
                    catch
                    {
                        connection.Close();
                        throw;
                    }
                    finally
                    {
                        command.Dispose();
                        if (connection.State != ConnectionState.Closed)
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }
        #endregion
        #region "ExecuteReader"
        /// <summary>
        /// 执行查询语句，返回单条实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StrSql"></param>
        /// <returns></returns>
        public static T ExecuteReader<T>(string StrSql) where T:class,new()
        {
            T entity = null;
            IDataReader Reader = ExecuteReader(StrSql, null);
            entity = ModelHelper<T>.ConvertSingleModel(Reader);
            return entity;
        }
        /// <summary>
        /// 执行查询语句，返回单条实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StrSql"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public static T ExecuteReader<T>(string StrSql, params IDataParameter[] Param) where T : class,new()
        {
            T entity = null;
            IDataReader Reader = ExecuteReader(StrSql, Param);
            entity = ModelHelper<T>.ConvertSingleModel(Reader);
            return entity;
        }
        /// <summary>
        /// 执行查询语句，返回IDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>IDataReader</returns>
        public static IDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(CommandType.Text, commandText, null);
        }
        /// <summary>
        /// 执行查询语句，返回IDataReader
        /// </summary>
        /// <param name="commandText">查询语句</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(string commandText, params IDataParameter[] parameters)
        {
            return ExecuteReader(CommandType.Text, commandText, parameters);
        }
        /// <summary>
        /// 执行查询语句，返回IDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns> IDataReader </returns>
        public static IDataReader ExecuteReader(CommandType commandType, string commandText, params IDataParameter[] parameters)
        {
            System.Data.IDbConnection connection = GetConnection();
            {
                System.Data.IDbCommand command = GetCommand();
                {
                    try
                    {
                        PrepareCommand(command, connection, (IDbTransaction)null, commandType, commandText, parameters);
                        System.Data.IDataReader iReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                        command.Parameters.Clear();
                        return iReader;
                    }
                    catch (System.Exception e)
                    {
                        command.Dispose();
                        if (connection.State != ConnectionState.Closed)
                        {
                            connection.Close();
                        }
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        #endregion
        #region "ExecuteDataTable"
        /// <summary>
        /// 执行Sql语句，返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StrSql"></param>
        /// <returns></returns>
        public static List<T> ExecuteTable<T>(string StrSql) where T : class,new()
        {
            List<T> list = new List<T>();
            DataTable Dt = ExecuteTable(StrSql);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                list = ModelHelper<T>.ConvertDtModel(Dt);
            }
            return list;
        }
        /// <summary>
        /// 执行Sql语句，返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StrSql"></param>
        /// <returns></returns>
        public static List<T> ExecuteTable<T>(IDbConnection connection, string StrSql) where T : class,new()
        {
            List<T> list = new List<T>();
            DataTable Dt = ExecuteTable(connection, StrSql);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                list = ModelHelper<T>.ConvertDtModel(Dt);
            }
            return list;
        }
        /// <summary>
        /// 执行Sql语句，返回记录集
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static DataTable ExecuteTable(string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
                return null;

            return ExecuteTable(commandText, (IDataParameter[])null);
        }
        /// <summary>
        /// 执行Sql语句，返回记录集
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static DataTable ExecuteTable(IDbConnection connection, string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
                return null;

            return ExecuteTable(connection, commandText, (IDataParameter[])null);
        }
        /// <summary>
        /// 执行Sql语句，返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StrSql"></param>
        /// <returns></returns>
        public static List<T> ExecuteTable<T>(string StrSql, params IDataParameter[] Param) where T : class,new()
        {
            List<T> list = new List<T>();
            DataTable Dt = ExecuteTable(StrSql, Param);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                list = ModelHelper<T>.ConvertDtModel(Dt);
            }
            return list;
        }
        /// <summary>
        /// 执行Sql语句，返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StrSql"></param>
        /// <returns></returns>
        public static List<T> ExecuteTable<T>(IDbConnection connection, string StrSql, params IDataParameter[] Param) where T : class,new()
        {
            List<T> list = new List<T>();
            DataTable Dt = ExecuteTable(connection, StrSql, Param);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                list = ModelHelper<T>.ConvertDtModel(Dt);
            }
            return list;
        }
        /// <summary>
        /// 执行Sql语句，返回记录集
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteTable(string commandText, params IDataParameter[] parameters)
        {
            CommandType commandType = CommandType.Text;
            return ExecuteTable(commandType, commandText, parameters);  
        }
        /// <summary>
        /// 执行Sql语句，返回记录集
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteTable(IDbConnection connection, string commandText, params IDataParameter[] parameters)
        {
            CommandType commandType = CommandType.Text;
            return ExecuteTable(connection, commandType, commandText, parameters);
        }
        /// <summary>
        /// 执行Sql语句，返回记录集
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteTable(CommandType commandType, string commandText, params IDataParameter[] parameters)
        {
            using (IDbConnection connection = GetConnection())
            {
                using (IDbCommand command = GetCommand())
                {
                    try
                    {
                    PrepareCommand(command, connection, (IDbTransaction)null, commandType, commandText, parameters);
                    command.CommandText = commandText;
                    IDataAdapter adapter = GetAdapater(command);
                    DataSet Ds = new DataSet();
                    adapter.Fill(Ds);
                    command.Parameters.Clear();
                    return Ds.Tables[0];
                    }
                    catch
                    {
                        connection.Close();
                        throw;
                    }
                    finally
                    {
                        command.Dispose();
                        if (connection.State != ConnectionState.Closed)
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 执行Sql语句，返回记录集
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteTable(IDbConnection connection, CommandType commandType, string commandText, params IDataParameter[] parameters)
        {
            using (IDbCommand command = GetCommand())
            {
                try
                {
                    PrepareCommand(command, connection, (IDbTransaction)null, commandType, commandText, parameters);
                    command.CommandText = commandText;
                    IDataAdapter adapter = GetAdapater(command);
                    DataSet Ds = new DataSet();
                    adapter.Fill(Ds);
                    command.Parameters.Clear();
                    return Ds.Tables[0];
                }
                catch
                {
                    connection.Close();
                    throw;
                }
                finally
                {
                    command.Dispose();
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }
        #endregion
        #region "ExecutePage"
        /// <summary>
        /// 分页函数
        /// </summary>
        /// <param name="sqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="indexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="orderFields">排序字段以及方式如：a.OrderID desc,CnName desc</param>
        /// <param name="pageIndex">当前页的页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="pageCount">输出参数，返回查询的总页数</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public static List<T> ExecutePage<T>(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize, out int recordCount, out int pageCount, params IDataParameter[] parameters) where T:class,new()
        {
            List<T> list = null;
            using (IDbConnection connection = GetConnection())
            {
                DataTable Dt = ExecutePage(connection, sqlAllFields, sqlTablesAndWhere, indexField, orderFields, pageIndex, pageSize, out recordCount, out pageCount, parameters);
                list = ModelHelper<T>.ConvertDtModel(Dt);
            }
            return list;
        }
        /// <summary>
        /// 分页函数
        /// </summary>
        /// <param name="sqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="indexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="orderFields">排序字段以及方式如：a.OrderID desc,CnName desc</param>
        /// <param name="pageIndex">当前页的页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="pageCount">输出参数，返回查询的总页数</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public static DataTable ExecutePage(string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize, out int recordCount, out int pageCount, params IDataParameter[] parameters)
        {
            using (IDbConnection connection = GetConnection())
            {
                return ExecutePage(connection, sqlAllFields, sqlTablesAndWhere, indexField, orderFields, pageIndex, pageSize, out recordCount, out pageCount, parameters);
            }
        }
        /// <summary>
        /// 分页函数
        /// </summary>
        /// <param name="connection">数据库链接</param>
        /// <param name="sqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="indexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="orderFields">排序字段以及方式如：a.OrderID desc,CnName desc</param>
        /// <param name="pageIndex">当前页的页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="pageCount">输出参数，返回查询的总页数</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        private static DataTable ExecutePage(IDbConnection connection, string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize, out int recordCount, out int pageCount, params  IDataParameter[] parameters)
        {
            using (IDbCommand command = GetCommand())
            {
                PrepareCommand(command, connection, (IDbTransaction)null, CommandType.Text, sqlAllFields, parameters);
                string Sql = GetPageSql(command, sqlAllFields, sqlTablesAndWhere, indexField, orderFields, pageIndex, pageSize, out recordCount, out pageCount);
                command.CommandText = Sql;
                IDataAdapter adapter = GetAdapater(Sql, connection);
                GetSelectCommand(adapter, command);
                DataSet Ds = new DataSet();
                adapter.Fill(Ds);
                command.Parameters.Clear();

                return Ds.Tables[0];
            }
        }
        /// <summary>
        /// 分页函数
        /// </summary>
        /// <param name="command">Command对象</param>
        /// <param name="sqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="sqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="indexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="orderFields">排序字段以及方式如：a.OrderID desc,CnName desc</param>
        /// <param name="pageIndex">当前页的页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="pageCount">输出参数，返回查询的总页数</param>
        /// <returns></returns>
        private static string GetPageSql(IDbCommand command, string sqlAllFields, string sqlTablesAndWhere, string indexField, string orderFields, int pageIndex, int pageSize, out int recordCount, out int pageCount)
        {
            recordCount = 0;
            pageCount = 0;
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            string SqlCount = "select count(" + indexField + ") from " + sqlTablesAndWhere;
            command.CommandText = SqlCount;
            recordCount = (int)command.ExecuteScalar();
            if (recordCount % pageSize == 0)
            {
                pageCount = recordCount / pageSize;
            }
            else
            {
                pageCount = recordCount / pageSize + 1;
            }
            if (pageIndex > pageCount)
                pageIndex = pageCount;
            if (pageIndex < 1)
                pageIndex = 1;
            string Sql = null;
            if (pageIndex == 1)
            {
                Sql = "select top " + pageSize + " " + sqlAllFields + " from " + sqlTablesAndWhere + " " + orderFields;
            }
            else
            {
                Sql = "select top " + pageSize + " " + sqlAllFields + " from ";
                if (sqlTablesAndWhere.ToLower().IndexOf(" where ") > 0)
                {
                    string _where = Regex.Replace(sqlTablesAndWhere, @"\ where\ ", " where (", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    Sql += _where + ") and (";
                }
                else
                {
                    Sql += sqlTablesAndWhere + " where (";
                }
                Sql += indexField + " not in (select top " + (pageIndex - 1) * pageSize + " " + indexField + " from " + sqlTablesAndWhere + " " + orderFields;
                Sql += ")) " + orderFields;
            }
            return Sql;
        }
        #endregion
        #region "ExecuteSqlTran"
        /// <summary>
        /// 执行多条Sql语句，实现数据库事务
        /// </summary>
        /// <param name="SqlList">多条Sql语句</param>
        public static void ExecuteSqlTran(ArrayList SqlList)
        {
            using (IDbConnection connection = GetConnection())
            {
                connection.Open();
                using (IDbCommand Command = GetCommand())
                {
                    Command.Connection = connection;
                    using (IDbTransaction DbTran = connection.BeginTransaction())
                    {
                        Command.Transaction = DbTran;
                        try
                        {
                            for (int i = 0; i < SqlList.Count; i++)
                            {
                                string StrSql = SqlList[i].ToString();
                                if (!string.IsNullOrEmpty(StrSql))
                                {
                                    Command.CommandText = StrSql;
                                    Command.ExecuteNonQuery();
                                }
                            }
                            DbTran.Commit();
                        }
                        catch (Exception ex)
                        {
                            DbTran.Rollback();
                            throw new Exception(ex.Message);
                        }
                        finally
                        {
                            if (connection.State != ConnectionState.Closed)
                            {
                                connection.Close();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条Sql语句，实现数据库事务
        /// </summary>
        /// <param name="sqlList">多条Sql语句</param>        
        /// <returns></returns>
        public static int ExecuteSqlTran(System.Collections.Generic.IList<string> sqlList)
        {
            return ExecuteSqlTran(sqlList, null);
        }

        /// <summary>
        /// 执行多条Sql语句，实现数据库事务
        /// </summary>
        /// <param name="sqlList">多条Sql语句</param>
        /// <param name="paramList">参数集合</param>
        /// <returns></returns>
        public static int ExecuteSqlTran(System.Collections.Generic.IList<string> sqlList, System.Collections.Generic.IList<IDataParameter[]> paramList)
        {
            using (IDbConnection connection = GetConnection())
            {
                connection.Open();
                using (IDbCommand Command = GetCommand())
                {
                    Command.Connection = connection;
                    using (IDbTransaction DbTran = connection.BeginTransaction())
                    {
                        Command.Transaction = DbTran;
                        int result = 0;
                        try
                        {
                            for (int i = 0; i < sqlList.Count; i++)
                            {
                                string StrSql = sqlList[i].ToString();
                                if (!string.IsNullOrEmpty(StrSql))
                                {
                                    Command.CommandText = StrSql;
                                    Command.Parameters.Clear();
                                    if (paramList != null && paramList.Count > 0 && sqlList.Count == paramList.Count)
                                    {
                                        foreach (IDbDataParameter p in paramList[i])
                                        {
                                            Command.Parameters.Add(p);
                                        }
                                    }
                                    result = Command.ExecuteNonQuery();
                                }                                
                            }
                            DbTran.Commit();
                            return result;
                        }
                        catch (Exception ex)
                        {
                            DbTran.Rollback();
                            throw new Exception(ex.Message);
                        }
                        finally
                        {
                            if (connection.State != ConnectionState.Closed)
                            {
                                connection.Close();
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region "ExecuteCommandWithSplitter"
        /// <summary>
        /// 运行多条Sql语句
        /// </summary>
        /// <param name="Sql">Sql语句</param>
        public static void ExecuteCommandWithSplitterAccess(string Sql)
        {
            ExecuteCommandWithSplitterAccess(Sql, "GO;".ToUpper());// "\r\nGO\r\n");
        }
        /// <summary>
        /// 运行多条Sql语句
        /// </summary>
        /// <param name="Sql">Sql语句</param>
        /// <param name="Split">分割字符串</param>
        public static void ExecuteCommandWithSplitterAccess(string Sql, string Split)
        {
            int startPos = 0;

            do
            {
                int lastPos = Sql.IndexOf(Split, startPos);
                int len = (lastPos > startPos ? lastPos : Sql.Length) - startPos;
                string query = Sql.Substring(startPos, len);
                query = query.Replace("GO".ToUpper(), "");
                if (query.Trim().Length > 0)
                {
                    try
                    {
                        ExecuteNonQuery(query);
                    }
                    catch
                    {
                        if (lastPos == -1)
                            break;
                        else
                            startPos = lastPos + Split.Length;
                        continue;
                    }
                }
                if (lastPos == -1)
                    break;
                else
                    startPos = lastPos + Split.Length;
            } while (startPos < Sql.Length); 

        }



        /// <summary>
        /// 运行多条Sql语句
        /// </summary>
        /// <param name="Sql">Sql语句</param>
        public static void ExecuteCommandWithSplitter(string Sql)
        {
            ExecuteCommandWithSplitter(Sql, "\r\nGO\r\n");
        }
        /// <summary>
        /// 运行多条Sql语句
        /// </summary>
        /// <param name="Sql">Sql语句</param>
        /// <param name="Split">分割字符串</param>
        public static void ExecuteCommandWithSplitter(string Sql, string Split)
        {
            int startPos = 0;

            do
            {
                int lastPos = Sql.IndexOf(Split, startPos);
                int len = (lastPos > startPos ? lastPos : Sql.Length) - startPos;
                string query = Sql.Substring(startPos, len);                
                if (query.Trim().Length > 0)
                {
                    try
                    {
                        ExecuteNonQuery(query);
                    }
                    catch { ;}
                }

                if (lastPos == -1)
                    break;
                else
                    startPos = lastPos + Split.Length;
            } while (startPos < Sql.Length);

        }
        #endregion
        #region "分页显示代码"
        /// <summary>
        /// 返回页面链接参数
        /// </summary>
        /// <returns></returns>
        private static string QueryUrl()
        {
            Regex _Regex = new Regex(@"^&PageNo=\d+", RegexOptions.Compiled);
            string Str = HttpContext.Current.Request.Url.Query.Replace("?", "&");
            Str = _Regex.Replace(Str, string.Empty);
            return Str;
        }
        /// <summary>
        /// 分页函数(最简单模式)
        /// </summary>
        /// <param name="RecordCount">记录总数</param>
        /// <param name="PageCount">页面总数</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="PageNo">当前页面</param>
        /// <param name="Query">查询条件：例如：UserName=Apollo</param>
        /// <returns></returns>
        public static string GetPageNormal(int RecordCount, int PageCount, int PageSize, int PageNo)
        {
            string Str = "";

            Str = "共" + RecordCount + "条记录 页次：" + PageNo + "/" + PageCount + "页 ";
            Str += PageSize + "条/页 ";

            if (Convert.ToInt32(PageNo) < 2)
                Str += "首页 上页 ";
            else
            {
                Str += "<a href=\"?PageNo=1" + QueryUrl() + "\">首页</a> ";
                Str += "<a href=\"?PageNo=" + (PageNo - 1) + QueryUrl() + "\">上页</a> ";
            }
            if (PageCount - Convert.ToInt32(PageNo) < 1)
            {
                Str += "下页 尾页 ";
            }
            else
            {
                Str += "<a href=\"?PageNo=" + (PageNo + 1) + QueryUrl() + "\">下页</a> ";
                Str += "<a href=\"?PageNo=" + PageCount + QueryUrl() + "\">尾页</a>  ";
            }

            return Str;
        }
        /// <summary>
        /// 分页函数(仿Google分页)
        /// </summary>
        /// <param name="RecordCount">记录总数</param>
        /// <param name="PageCount">分页总数</param>
        /// <param name="PageSize">每个条数</param>
        /// <param name="PageNo">当前页码</param>
        /// <returns></returns>
        public static string GetPageGoogle(int RecordCount, int PageCount, int PageSize, int PageNo)
        {
            int Next, Pre, StartCount, EndCount = 0;
            if (PageNo < 1) { PageNo = 1; }
            Next = PageNo + 1;
            Pre = PageNo - 1;
            StartCount = (PageNo + 5) > PageCount ? PageCount - 9 : PageNo - 1;
            EndCount = PageNo < 5 ? 10 : PageNo + 5;
            if (StartCount < 1) { StartCount = 1; }
            if (PageCount < EndCount) { EndCount = PageCount; }
            string Str = "";
            Str += "共" + RecordCount + "条记录，共" + PageCount + "页&nbsp;";
            Str += PageNo > 1 ? "<a href=\"?PageNo=1" + QueryUrl() + "\">首页</a>&nbsp;<a href=\"?PageNo=" + Pre + QueryUrl() + "\">上一页</a>" : "首页 上一页";
            for (int i = StartCount; i <= EndCount; i++)
            {
                Str += PageNo == i ? "&nbsp;<font color=\"#ff0000\">" + i + "</font>" : "&nbsp;<a href=\"?PageNo=" + i + QueryUrl() + "\">" + i + "</a>";
            }
            Str += PageNo != PageCount ? "&nbsp;<a href=\"?PageNo=" + Next + QueryUrl() + "\">下一页</a>&nbsp;<a href=\"?PageNo=" + PageCount + QueryUrl() + "\">末页</a>" : " 下一页 末页";
            return Str;
        }
        #endregion
    }
}