using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Sample.DataBase
{
    public class DBManager
    {
        /// <summary>
        /// コネクションを取得する
        /// </summary>
        /// <param name="dbName">データベース名</param>
        /// <returns>SqlConnection</returns>
        public SqlConnection GetConnection(string dbName)
        {
            SqlConnection connection;

            try
            {
                switch (dbName)
                {
                    case "Sample":
                        connection = (new System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings.Get("DbConnectionString").ToString()));
                        break;

                    default:
                        //不明なデータベース
                        connection = null;
                        break;
                }
            }
            catch
            {
                connection = null;
            }

            return connection;
        }

        /// <summary>
        /// トランザクションを開始する
        /// </summary>
        /// <param name="connection">コネクション</param>
        /// <returns>SqlTransaction</returns>
        public SqlTransaction BeginTransaction(SqlConnection connection)
        {
            SqlTransaction tran = null;

            try
            {
                tran = connection.BeginTransaction();
            }
            catch (SqlException e)
            {
                string err = e.ToString();
                tran = null;
            }
            catch (InvalidOperationException e)
            {
                string err = e.ToString();
                tran = null;
            }

            return tran;
        }

        /// <summary>
        /// トランザクションをコミットする
        /// </summary>
        /// <param name="tran">トランザクション</param>
        /// <returns>処理結果</returns>
        public Boolean CommitTransaction(SqlTransaction tran)
        {
            try
            {
                tran.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// トランザクションをロールバックする
        /// </summary>
        /// <param name="connection">トランザクション</param>
        /// <returns>処理結果</returns>
        public Boolean RoalBackTransaction(SqlTransaction tran)
        {
            try
            {
                tran.Rollback();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 設定ファイルからタイムアウト値を取得します。
        /// </summary>
        /// <returns>タイムアウト値（秒）</returns>
        public int GetTimeOut()
        {
            try
            {
                return int.Parse(ConfigurationManager.AppSettings.Get("DB_TIMEOUT").ToString());
            }
            catch
            {
                return 60;
            }
        }

    }
    
}