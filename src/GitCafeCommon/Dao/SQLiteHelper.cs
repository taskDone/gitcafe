using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace GitCafeCommon.Dao
{
    /// <summary>
    /// SQLite帮助类，用于与SQLite数据交互
    /// </summary>
    public class SQLiteHelper : IDisposable
    {
        protected string _connectionString = null;
        protected SQLiteConnection _connection = null;
        protected SQLiteTransaction _transaction = null;
        protected bool _disposed = false;

        /// <summary>
        /// 事物
        /// </summary>
        public SQLiteTransaction Transaction
        {
            get
            {
                return _transaction;
            }
        }
        public SQLiteHelper()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            _connectionString = string.Format("Data Source ={0}", System.IO.Path.Combine(dir, "gitcafe.db")); //ConfigurationManager.ConnectionStrings["CSFConnection"].ConnectionString;
            Connect();
        }

        protected void Connect()
        {
            _connection = new SQLiteConnection(_connectionString);
            _connection.Open();
        }

        /// <summary>
        /// 执行命令，返回受影响行数
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string query, params SQLiteParameter[] parameters)
        {
            try
            {
                using (SQLiteCommand command = CreateCommand(query, parameters))
                {
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string query, params SQLiteParameter[] parameters)
        {
            using (SQLiteCommand command = CreateCommand(query, parameters))
            {
                return command.ExecuteScalar();
            }
        }

        /// <summary>
        /// 返回查询结果
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public SQLiteDataReader ExecuteReader(string query, params SQLiteParameter[] parameters)
        {
            using (SQLiteCommand command = CreateCommand(query, parameters))
            {
                return command.ExecuteReader();
            }
        }

        /// <summary>
        /// 返回查询结果
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string query, params SQLiteParameter[] parameters)
        {
            using (SQLiteCommand command = CreateCommand(query, parameters))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// 开始事物处理。用于批量数据导入
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// db.BeginTransaction();
        /// xxxxxxxx
        /// db.Commit();
        /// </example>
        public SQLiteTransaction BeginTransaction()
        {
            Rollback();
            _transaction = _connection.BeginTransaction();
            return _transaction;
        }

        /// <summary>
        /// 提交
        /// </summary>
        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction = null;
            }
        }

        /// <summary>
        /// 回滚
        /// </summary>
        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
        }

        /// <summary>
        /// 取出数据库所有表信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetSchema()
        {
            return _connection.GetSchema("TABLES");
        }

        protected SQLiteCommand CreateCommand(string query, params SQLiteParameter[] args)
        {
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }
            SQLiteCommand command = new SQLiteCommand(query, _connection);
            if (_transaction != null)
                command.Transaction = _transaction;
            command.CommandType = System.Data.CommandType.Text;
            if (args != null)
            {
                command.Parameters.AddRange(args);
            }
            return command;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_connection != null)
                    {
                        Rollback();
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }
    }
}
